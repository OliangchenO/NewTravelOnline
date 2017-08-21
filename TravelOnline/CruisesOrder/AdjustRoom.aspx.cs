using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.Class.Travel;

namespace TravelOnline.CruisesOrder
{
    public partial class AdjustRoom : System.Web.UI.Page
    {
        public string TitleName, AutoId, hide, CancelInfo, QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, shipid, RoomList, CruisesRoomString;
        //public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap;
        public string Flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            Flag = Request.QueryString["flag"];
            if (Convert.ToString(Session["Online_UserDept"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                switch (Flag)
                {
                    case "adjust":
                        TitleName = "舱房调整";
                        break;
                    case "cancel":
                        TitleName = "舱房取消";
                        break;
                    case "Retreat":
                        TitleName = "订单取消";
                        break;
                    default:
                        TitleName = "舱房调整";
                        break;
                }
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select id from OL_OrderApply where flag='0' and orderid='{0}'", QueryId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("您已经提交过申请，在审核通过之前，同一订单只能申请一次");
                Response.End();
            }

            SqlQueryText = string.Format("select *,(select top 1 cancel from CR_Confirm where lineid=OL_Order.lineid) as cancel from OL_Order where OrderId='{0}'", QueryId, Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "8")
                {
                    Response.Write("已经删除的订单不能操作！");
                    Response.End();
                }
                if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
                {
                }
                else
                {
                    if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                }
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                NumsInfo = string.Format(" 预订人数：{0}成人", Adults);
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                RoomList = GetRoomList();

                if (Flag == "adjust")
                {
                    hide = "hide";
                    CruisesRoomString = CompanyCruisesRoomGet(DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["ordercompany"].ToString());
                }
                else 
                {
                    CancelInfo = DS.Tables[0].Rows[0]["cancel"].ToString().Replace("\n", "<br>");
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected string GetRoomList()
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();

            DataSet DS1 = new DataSet();
            DS1.Clear();

            string SqlQueryText;
            SqlQueryText = "select * from CR_RoomList where orderflag='0' and OrderId='" + QueryId + "' order by allotid";
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Flag == "cancel")
                {
                    if (DS.Tables[0].Rows.Count < 2)
                    {
                        Response.Write("当前有效舱房少于2间，请使用订单取消功能！");
                        Response.End();
                    }
                }
                SqlQueryText = "select * from OL_GuestInfo where OrderId='" + QueryId + "'";
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                Strings.Append("<div id=OrderRoomList class=\"m detail\">");
                Strings.Append("<UL class=tab><LI class=curr>房间和人数<SPAN></SPAN></LI></UL>");
                Strings.Append("<div class=\"mc tabcon borders01\">");
                Strings.Append("<div class=roomdivlist>");
                //Strings.Append("<DIV class=roomHead>请选择您要调整或取消的舱房</DIV>");
                Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
                Strings.Append("<tr class=tit1>");
                Strings.Append("<td width=\"20%\">房间类型</td>");
                Strings.Append("<td width=\"30%\">客人姓名</td>");
                Strings.Append("<td width=\"5%\">人数</td>");
                Strings.Append("<td width=\"5%\">成人</td>");
                Strings.Append("<td width=\"5%\">儿童</td>");
                Strings.Append("<td width=\"10%\">第1、2人价格</td>");
                Strings.Append("<td width=\"10%\">第3成人价</td>");
                Strings.Append("<td width=\"10%\">第3儿童价</td>");
                Strings.Append("<td width=\"5%\">选择</td>");
                Strings.Append("</tr>");
                string h1 = "";
                if (Flag == "Retreat") h1 = "hide";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<tr><td>{0}</td><td>{10}</td><td>{1}</td><td>{2}</td><td>{3}</td><td class=tdn>&yen;{4}</td><td class=tdn>&yen;{5}</td><td class=tdn>&yen;{6}</td><td><input id=\"Radio{7}\" type=\"radio\" name=\"rooms\" value=\"{7}\" berth=\"{8}\" price=\"{9}\" roomid=\"{11}\" class=\"{12}\"/></td></tr>",
                        DS.Tables[0].Rows[i]["roomname"].ToString(),
                        DS.Tables[0].Rows[i]["peoples"].ToString(),
                        DS.Tables[0].Rows[i]["adults"].ToString(),
                        DS.Tables[0].Rows[i]["childs"].ToString(),
                        DS.Tables[0].Rows[i]["price"].ToString(),
                        DS.Tables[0].Rows[i]["thirdprice"].ToString(),
                        DS.Tables[0].Rows[i]["childprice"].ToString(),
                        DS.Tables[0].Rows[i]["id"].ToString(),
                        DS.Tables[0].Rows[i]["berth"].ToString(),
                        DS.Tables[0].Rows[i]["price"].ToString(),
                        GetTouristName(DS.Tables[0].Rows[i]["id"].ToString(), DS1.Tables[0]),
                        DS.Tables[0].Rows[i]["roomid"].ToString(),
                        h1
                    ));

                }

                Strings.Append("</table>");
                Strings.Append("</div>");
                Strings.Append("</div>");
                Strings.Append("</div>");
            }
            return Strings.ToString();
        }

        protected string GetTouristName(string ids, DataTable dt)
        {
            string guestname = "";
            DataRow[] drs = dt.Select("listid='" + ids + "'");
            foreach (DataRow dr in drs)
            {
                guestname += dr["GuestName"].ToString() + "、";
                
            }
            if (guestname.Length > 0) guestname = guestname.Substring(0, guestname.Length - 1);
            return guestname;
        }


        protected string CompanyCruisesRoomGet(string lineid, string companyid)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder Strings1 = new StringBuilder();
            string SqlQueryText; 

            if (MyConvert.ConToInt(companyid) == 0)
            {
                SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and lineid='" + lineid + "' and allotflag='0' order by typeid";
            }
            else
            {
                string AllotCount = MyDataBaseComm.getScalar("select count(id) from CR_RoomAllot where companyid='" + companyid + "' and lineid='" + lineid + "'");
                if (Convert.ToInt32(AllotCount) > 0)
                {
                    SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";
                }
                else
                {
                    SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and lineid='" + lineid + "' and allotflag='0' order by typeid";
            
                }
                //SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings1.Append("<UL class=hoteltab>");
                Strings.Append("<div id=tabdiv>");
                string T_Price, C_Price;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    T_Price = "--";// &yen;
                    C_Price = "--";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();
                    if (i == 0)
                    {
                        Strings1.Append(string.Format("<LI class=curr>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                        Strings.Append("<div class=\"tabcon borders\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">变更为此舱</td></tr>");
                        Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><input id=\"RadioSelect{6}\" type=\"radio\" name=\"RoomsSelect\" value=\"{6}\" berth=\"{3}\" price=\"{4}\" roomid=\"{8}\" /></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                            DS.Tables[0].Rows[i]["roomname"].ToString(),
                            DS.Tables[0].Rows[i]["area"].ToString(),
                            DS.Tables[0].Rows[i]["deck"].ToString(),
                            DS.Tables[0].Rows[i]["berth"].ToString(),
                            DS.Tables[0].Rows[i]["price"].ToString(),
                            T_Price,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            C_Price,
                            DS.Tables[0].Rows[i]["roomid"].ToString()
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
                            Strings1.Append(string.Format("<LI>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                            Strings.Append("</table></div>");
                            //Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"28%\">客舱类型</td><td width=\"13%\">客舱面积</td><td width=\"15%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"12%\">第1、2人价格</td><td class=al width=\"12%\">第3、4人价格</td><td width=\"8%\">&nbsp;</td></tr>");
                            Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">变更为此舱</td></tr>");
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><input id=\"RadioSelect{6}\" type=\"radio\" name=\"RoomsSelect\" value=\"{6}\" berth=\"{3}\" price=\"{4}\" roomid=\"{8}\" /></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price,
                                DS.Tables[0].Rows[i]["roomid"].ToString()
                            ));
                        }
                        else
                        {
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><input id=\"RadioSelect{6}\" type=\"radio\" name=\"RoomsSelect\" value=\"{6}\" berth=\"{3}\" price=\"{4}\" roomid=\"{8}\" /></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price,
                                DS.Tables[0].Rows[i]["roomid"].ToString()
                            ));
                        }
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Strings.Append("</table></div>");
                        }
                    }

                }

                Strings1.Append("</UL>");
                Strings.Append("</div>");
                Strings1.Append(Strings.ToString());
            }
            else
            {
                Strings1.Append("None");
            }
            return Strings1.ToString();
        }
        
    }
}