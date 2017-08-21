using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;

namespace TravelOnline.tour
{
    public partial class ajaxservice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            switch (Request.QueryString["action"])
            {
                case "CruisesRoomList":
                    CruisesRoomList(Request.QueryString["id"]);
                    break;
                case "LineCount":
                    LinePageViewCount(Request.QueryString["id"]);
                    break;
                case "Favorite":
                    Favorite(Request.QueryString["id"]);
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        protected void Favorite(string id)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                Response.Write("{\"success\":0,\"content\":\"您没有登录，请登录后再操作！\"}");
                Response.End();
            }
            int lineid = MyConvert.ConToInt(id);
            if (lineid > 0)
            {
                string SqlQueryText = string.Format("select top 1 id from OL_Favorite where lineid='{0}' and uid='{1}'", lineid, Convert.ToString(Session["Online_UserId"]));
                if (MyDataBaseComm.getScalar(SqlQueryText) == null)
                {
                    SqlQueryText = string.Format("insert into OL_Favorite (uid,lineid,linename,price,inputdate) values ('{0}','{1}','{2}','{3}','{4}')",
                       Convert.ToString(Session["Online_UserId"]),
                       lineid,
                       "",
                       "0",
                       DateTime.Now.ToString()
                    );
                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        Response.Write("{\"success\":0,\"content\":\"收藏成功！\"}");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"success\":0,\"content\":\"收藏失败，请稍后再试！\"}");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("{\"success\":0,\"content\":\"您已经收藏了该线路！\"}");
                    Response.End();
                }
            }
            else
            {
                Response.Write("{\"success\":0,\"content\":\"传递的线路信息有误，收藏失败！\"}");
                Response.End();
            }
            
        }

        protected void LinePageViewCount(string id)
        {
            string SqlQueryText = string.Format("update OL_Line set PV=PV+1 where MisLineId='{0}'", MyConvert.ConToInt(id));
            MyDataBaseComm.ExcuteSql(SqlQueryText);
        }

        protected void CruisesRoomList(string id)
        {
            string countent = "";
            if (Convert.ToString(Session["Online_UserCompany"]).Length > 0)
            {
                
                if (Convert.ToString(HttpContext.Current.Cache["CruisesRoomList_" + id + "_" + Session["Online_UserCompany"]]) == "")
                {
                    countent = GetCruisesRoomList(id, Convert.ToString(Session["Online_UserCompany"]));
                    if (countent == "没有可供预订的舱型") countent = GetCruisesRoomList(id, "0");
                }
                else
                {
                    if (Convert.ToString(HttpContext.Current.Cache["CruisesRoomList_" + id + "_" + Session["Online_UserCompany"]]) == "没有可供预订的舱型")
                    {
                        countent = GetCruisesRoomList(id, "0");
                    }
                    else
                    {
                        countent = GetCruisesRoomList(id, Convert.ToString(Session["Online_UserCompany"]));
                    }
                }
            }
            else
            {
                countent = GetCruisesRoomList(id, "0");
            }
            StringBuilder Strings = new StringBuilder();
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"content\":\"{0}\"", countent));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }

        protected string GetCruisesRoomList(string lineid,string companyid)
        {
            if (Convert.ToString(HttpContext.Current.Cache["CruisesRoomList_" + lineid + "_" + companyid]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                StringBuilder Strings1 = new StringBuilder();
                string recommend = ""; //(nums-sellroom)>=0 and 
                string SqlQueryText = "SELECT * from View_CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' order by typeid";
                if (companyid != "0") SqlQueryText = "SELECT * from View_CR_RoomAllot where companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings1.Append("<div class=roomnav><ul>");
                    Strings.Append("<div id=tabdiv>");
                    string T_Price, C_Price;
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        recommend = "";
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["recommend"].ToString()) > 0) recommend = "<img class=yltags src=\\\"/images/cruises/" + DS.Tables[0].Rows[i]["recommend"].ToString() + ".jpg\\\">";
                        T_Price = "--";// &yen;
                        C_Price = "--";
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();
                        if (i == 0)
                        {
                            Strings1.Append(string.Format("<li><a class=\\\"selected\\\" href=\\\"#room{0}\\\">{1}</a></li>", DS.Tables[0].Rows[i]["typeid"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString()));
                            Strings.Append(string.Format("<div class=\\\"room\\\" id=\\\"room{0}\\\">", DS.Tables[0].Rows[i]["typeid"].ToString()));
                            Strings.Append("<table style=\\\"width: 100%;\\\" border=\\\"0\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\"><tr class=rtit><td width=\\\"32%\\\">客舱类型</td><td width=\\\"10%\\\">客舱面积</td><td width=\\\"10%\\\">甲板层</td><td width=\\\"8%\\\">最多可住</td><td class=al width=\\\"11%\\\">第1、2人价格</td><td class=al width=\\\"10%\\\">第3成人价</td><td class=al width=\\\"10%\\\">第3儿童价</td><td width=\\\"8%\\\">&nbsp;</td></tr>");
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\\\"javascript:;\\\" onclick=\\\"RoomClick({6})\\\" title=\\\"{0}\\\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\\\"center\\\"><a class=\\\"btn btn-warning\\\" href=\\\"javascript:void(0);\\\" onclick=\\\"RoomClick({6})\\\">选择房间</a></td></tr><tr class=\\\"hide htr\\\" id=h{6}><td colspan=\\\"8\\\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price,
                                recommend
                            ));
                            if (i == DS.Tables[0].Rows.Count - 1)
                            {
                                Strings.Append("</table></div>");
                            }
                        }
                        else
                        {
                            if (DS.Tables[0].Rows[i]["typeid"].ToString() != DS.Tables[0].Rows[i - 1]["typeid"].ToString())
                            {
                                //Strings1.Append(string.Format("<li>{0}<span></span></li>", DS.Tables[0].Rows[i]["typename"].ToString()));
                                Strings1.Append(string.Format("<li><a href=\\\"#room{0}\\\">{1}</a></li>", DS.Tables[0].Rows[i]["typeid"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString()));
                            
                                Strings.Append("</table></div>");
                                Strings.Append(string.Format("<div class=\\\"room hide\\\" id=\\\"room{0}\\\">", DS.Tables[0].Rows[i]["typeid"].ToString()));
                                Strings.Append("<table style=\\\"width: 100%;\\\" border=\\\"0\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\"><tr class=rtit><td width=\\\"32%\\\">客舱类型</td><td width=\\\"10%\\\">客舱面积</td><td width=\\\"10%\\\">甲板层</td><td width=\\\"8%\\\">最多可住</td><td class=al width=\\\"11%\\\">第1、2人价格</td><td class=al width=\\\"10%\\\">第3成人价</td><td class=al width=\\\"10%\\\">第3儿童价</td><td width=\\\"8%\\\">&nbsp;</td></tr>");
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\\\"javascript:;\\\" onclick=\\\"RoomClick({6})\\\" title=\\\"{0}\\\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\\\"center\\\"><a class=\\\"btn btn-warning\\\" href=\\\"javascript:void(0);\\\" onclick=\\\"RoomClick({6})\\\">选择房间</a></td></tr><tr class=\\\"hide htr\\\" id=h{6}><td colspan=\\\"8\\\"><div id=show{6}></div></td></tr>",
                                    DS.Tables[0].Rows[i]["roomname"].ToString(),
                                    DS.Tables[0].Rows[i]["area"].ToString(),
                                    DS.Tables[0].Rows[i]["deck"].ToString(),
                                    DS.Tables[0].Rows[i]["berth"].ToString(),
                                    DS.Tables[0].Rows[i]["price"].ToString(),
                                    T_Price,
                                    DS.Tables[0].Rows[i]["id"].ToString(),
                                    C_Price,
                                    recommend
                                ));
                            }
                            else
                            {
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\\\"javascript:;\\\" onclick=\\\"RoomClick({6})\\\" title=\\\"{0}\\\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\\\"center\\\"><a class=\\\"btn btn-warning\\\" href=\\\"javascript:void(0);\\\" onclick=\\\"RoomClick({6})\\\">选择房间</a></td></tr><tr class=\\\"hide htr\\\" id=h{6}><td colspan=\\\"8\\\"><div id=show{6}></div></td></tr>",
                                    DS.Tables[0].Rows[i]["roomname"].ToString(),
                                    DS.Tables[0].Rows[i]["area"].ToString(),
                                    DS.Tables[0].Rows[i]["deck"].ToString(),
                                    DS.Tables[0].Rows[i]["berth"].ToString(),
                                    DS.Tables[0].Rows[i]["price"].ToString(),
                                    T_Price,
                                    DS.Tables[0].Rows[i]["id"].ToString(),
                                    C_Price,
                                    recommend
                                ));
                            }
                            if (i == DS.Tables[0].Rows.Count - 1)
                            {
                                Strings.Append("</table></div>");
                            }
                        }

                    }

                    Strings1.Append("</ul></div>");
                    Strings.Append("</div>");
                    Strings1.Append(Strings.ToString());
                }
                else
                {
                    Strings1.Append("没有可供预订的舱型");
                }
                HttpContext.Current.Cache.Insert("CruisesRoomList_" + lineid + "_" + companyid, Strings1.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["CruisesRoomList_" + lineid + "_" + companyid]);
        }


    }
}