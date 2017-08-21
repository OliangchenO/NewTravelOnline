using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.Class.Travel;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using TravelOnline.Class.Purchase;


using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Common;

namespace TravelOnline.Travel
{
    public partial class ShipDetail : System.Web.UI.Page
    {
        public Guid ucode;
        public string BodyId, Category, id, ProducType, LineOnHotSale, Map1, Map2, PlanScripts, titleinfo, planid, begindate, Preference;
        public string LineName, LinePrice, ImgList, FirstImg, Pics, MsPrice;
        public string RegularContractInfos, RegularPayInfos, RegularOrderProcess;
        public string RouteInfos, RouteFeature, RouteServiceInfos, RoutePriceInfos, RouteAttentionsInfos;
        public string PlanString, RoomString, TripsString, CruisesPicString, CruisesRoomString;
        public string Journal;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Status = "301 Moved Permanently";
            id = Request.QueryString["Id"];
            Response.AddHeader("Location", "/line/" + id + ".html");
            Response.End();

            //强制刷新页面，不允许从缓存中读取
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            //Response.Expires = 0;
            //Response.CacheControl = "no-cache";
            //Response.AppendHeader("Pragma", "No-Cache");

            //Category = Request.QueryString["Category"];
            //id = Request.QueryString["Id"];
            //ProducType = Request.QueryString["ProducType"];

            if (MyConvert.ConToInt(id) == 0) Response.Redirect("~/index.html", true);
            Response.Redirect("/line/" + id + ".html", true);

            switch (ProducType)
            {
                case "OutBound":
                    BodyId = "pop";
                    Map1 = "<a href = \"/OutBound.html\">出境旅游</a>";
                    titleinfo = "出境旅游";
                    break;
                case "InLand":
                    BodyId = "tuan";
                    Map1 = "<a href = \"/InLand.html\">国内旅游</a>";
                    titleinfo = "国内旅游";
                    break;
                case "FreeTour":
                    BodyId = "auction";
                    Map1 = "<a href = \"/FreeTour.html\">自由行</a>";
                    titleinfo = "自由行";
                    break;
                case "Cruises":
                    BodyId = "read";
                    Map1 = "<a href = \"/Cruises.html\">邮轮旅游</a>";
                    titleinfo = "邮轮旅游";
                    break;
                case "Visa":
                    BodyId = "category";
                    Map1 = "<a href = \"/Visa.html\">签证办理</a>";
                    titleinfo = "签证办理";
                    break;
                default:
                    BodyId = "pop";
                    break;
            }
            if (MyConvert.ConToInt(id) == 0) Response.Redirect("~/index.html", true);
            LoadLineInfo();
            ReadRouteXML();
            LineClass.LinePageViewCount(id);
            LineOnHotSale = LinePreferences.LineOnHotSale(Category);
            RegularContractInfos = LineDetailRegular.ContractInfos(ProducType);
            RegularPayInfos = LineDetailRegular.PayInfos();
            RegularOrderProcess = LineDetailRegular.OrderProcess();
            //PlanString, RoomString;
            //PurchaseClass.CruisesPlanInfo PlanInfo = new PurchaseClass.CruisesPlanInfo();
            //PlanInfo = PurchaseClass.GetCrusesPlanList(id);
            //PlanString = PlanInfo.CruisesPlanInfo_PlanList;
            //RoomString = PlanInfo.CruisesPlanInfo_RoomList;
            //if (Request.Cookies["CallCenterOrderId"] != null)
            //{
            //    string ManagerLoginName = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["CallCenterOrderId"].Value));
            //    Response.Write("ccid=" + ManagerLoginName);
            //}
        }

        protected void LoadLineInfo()
        {
            string SqlQueryText = string.Format("select top 1 *,(select ProductName from OL_ProductType where misclassid=OL_Line.LineClass) as ProductName from OL_Line where MisLineId='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Journal = LinePreferences.LineJournal(DS.Tables[0].Rows[0]["FirstDestination"].ToString());

                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0)
                {
                    Preference = "<SPAN class=GreenSpan>在线支付即享优惠 " + DS.Tables[0].Rows[0]["wwwyh"].ToString() + " 元/人</SPAN>";
                }

                planid = DS.Tables[0].Rows[0]["Planid"].ToString();
                begindate = string.Format("{0:yyyy-MM-dd}", MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["PlanDate"].ToString()));
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    PlanScripts = rsp.OnlinePlanDateCreate(UpPassWord, id);
                    if (PlanScripts == "Error")
                    {
                        PlanScripts = "<script type=\"text/javascript\" src=\"/Js/PlanDate/" + id + ".js\"></script>";
                    }
                    else
                    {
                        PlanScripts = "<SCRIPT type=\"text/javascript\">" + PlanScripts + "</SCRIPT>";
                    }
                }
                catch
                {
                    PlanScripts = "<script type=\"text/javascript\" src=\"/Js/PlanDate/" + id + ".js\"></script>";
                }


                string picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, DS.Tables[0].Rows[0]["Pics"].ToString());
                if (System.IO.File.Exists(picpath) == true)
                {
                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) Pics = string.Format("http://www.scyts.com/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                }
                if (DS.Tables[0].Rows[0]["LineType"].ToString() != ProducType) Response.Redirect("~/index.html", true);
                Map2 = string.Format("<a href = \"/{0}/{1}-0.html\">{2}</a>", ProducType, DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["ProductName"].ToString());
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LinePrice = DS.Tables[0].Rows[0]["Price"].ToString();
                MsPrice = DS.Tables[0].Rows[0]["MsPrice"].ToString();

                TripsString = LinePreferences.CruisesRouteString(id);
                CruisesPicString = LinePreferences.CruisesPicUrl(DS.Tables[0].Rows[0]["Shipid"].ToString());

                
                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    string AllotCount = MyDataBaseComm.getScalar("select count(id) from CR_RoomAllot where sellflag='0' and companyid='" + Convert.ToString(Session["Online_UserCompany"]) + "' and lineid='" + id + "'");
                    if (Convert.ToInt32(AllotCount) > 0)
                    {
                        CruisesRoomString = LinePreferences.CompanyCruisesRoomGet(id, Convert.ToString(Session["Online_UserCompany"]));
                    }
                    else
                    {
                        CruisesRoomString = LinePreferences.CruisesRoomStringGet(id);
                    }

                    //CruisesRoomString = LinePreferences.CompanyCruisesRoomGet(id, Convert.ToString(Session["Online_UserCompany"]));
                    //if (CruisesRoomString.Length < 10) CruisesRoomString = LinePreferences.CruisesRoomStringGet(id);
                    //Response.Write("aaaaaaaa");
                }
                else
                {
                    CruisesRoomString = LinePreferences.CruisesRoomStringGet(id);
                    //Response.Write("bbbbbbb");
                }
                
            }
            else
            {

            }
        }

        protected void ReadRouteXML()
        {
            string SqlQueryText = string.Format("select * from CR_Visit where lineid='{0}' order by days", id);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, id);
            //If System.IO.File.Exists(Server.MapPath(filesurl)) = True Then
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = x.SelectSingleNode("Feature").InnerText.Replace("\n", "<br>");
                    //chr.Replace("\n","<br>");
                    Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>服务标准</STRONG><DIV class=extra></DIV></DIV><DIV class=tj>");
                    if (x.SelectSingleNode("Traffic").InnerText.Length > 0) Strings.Append(string.Format("旅游交通：{0}<br>", x.SelectSingleNode("Traffic").InnerText));
                    if (x.SelectSingleNode("Hotel").InnerText.Length > 0) Strings.Append(string.Format("住宿标准：{0}<br>", x.SelectSingleNode("Hotel").InnerText));
                    if (x.SelectSingleNode("Scenery").InnerText.Length > 0) Strings.Append(string.Format("景点门票：{0}<br>", x.SelectSingleNode("Scenery").InnerText));
                    if (x.SelectSingleNode("Foods").InnerText.Length > 0) Strings.Append(string.Format("用餐标准：{0}<br>", x.SelectSingleNode("Foods").InnerText));
                    if (x.SelectSingleNode("Guide").InnerText.Length > 0) Strings.Append(string.Format("导游服务：{0}<br>", x.SelectSingleNode("Guide").InnerText));
                    if (x.SelectSingleNode("Insure").InnerText.Length > 0) Strings.Append(string.Format("保险服务：{0}<br>", x.SelectSingleNode("Insure").InnerText));
                    if (x.SelectSingleNode("Others").InnerText.Length > 0) Strings.Append(string.Format("其他服务：{0}<br>", x.SelectSingleNode("Others").InnerText));
                    Strings.Append("</DIV></DIV>");
                    RouteServiceInfos = Strings.ToString();
                    Strings.Clear();

                    if (x.SelectSingleNode("PriceIn").InnerText.Length > 0)
                    {
                        Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>价格包含</STRONG><DIV class=extra></DIV></DIV><DIV class=tj>");
                        Strings.Append(x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "<br>"));
                        Strings.Append("</DIV></DIV>");
                    }
                    if (x.SelectSingleNode("PriceOut").InnerText.Length > 0)
                    {
                        Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>价格不含</STRONG><DIV class=extra></DIV></DIV><DIV class=tj>");
                        Strings.Append(x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "<br>"));
                        Strings.Append("</DIV></DIV>");
                    }
                    if (x.SelectSingleNode("OwnExpense").InnerText.Length > 0)
                    {
                        Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>自费项目</STRONG><DIV class=extra></DIV></DIV><DIV class=tj>");
                        Strings.Append(x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "<br>"));
                        Strings.Append("</DIV></DIV>");
                    }
                    RoutePriceInfos = Strings.ToString();
                    Strings.Clear();

                    if (x.SelectSingleNode("Attentions").InnerText.Length > 0)
                    {
                        Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>注意事项</STRONG><DIV class=extra></DIV></DIV><DIV class=tj id=specialmemo>");
                        Strings.Append(x.SelectSingleNode("Attentions").InnerText.Replace("\n", "<br>"));
                        Strings.Append("</DIV></DIV>");
                    }
                    if (x.SelectSingleNode("Shopping").InnerText.Length > 0)
                    {
                        Strings.Append("<DIV class=\"m select\"><DIV class=mt><H1></H1><STRONG>购物商店</STRONG><DIV class=extra></DIV></DIV><DIV class=tj>");
                        Strings.Append(x.SelectSingleNode("Shopping").InnerText.Replace("\n", "<br>"));
                        Strings.Append("</DIV></DIV>");
                    }
                    RouteAttentionsInfos = Strings.ToString();
                    Strings.Clear();

                    StringBuilder PicString = new StringBuilder();
                    XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                    int PicCount = 0;
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        string picpath, pic1, pic2, pic3;
                        if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 10)
                        {
                            picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, elemList[i].SelectSingleNode("Pics").InnerText);
                            pic1 = "/Images/none.gif";
                            pic2 = "/Images/none.gif";
                            pic3 = "/Images/none.gif";
                            try
                            {
                                pic1 = string.Format("/Images/Views/{0}", elemList[i].SelectSingleNode("Pics").InnerText);
                                pic2 = string.Format("/Images/Views/{0}/M_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                pic3 = string.Format("/Images/Views/{0}/S_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                            }
                            catch
                            { }
                            if (System.IO.File.Exists(picpath) == true)
                            {
                                if (PicCount == 0)
                                {
                                    FirstImg = string.Format("<IMG onerror=\"this.src='/Images/none.gif'\" src=\"{0}\" width=320 height=240 jqimg=\"{1}\">", pic2, pic1);
                                }
                                PicString.Append(string.Format("<LI><IMG onerror=\"this.src='/Images/none.gif'\" src=\"{0}\" width=62 height=50></LI>", pic3));
                                PicCount++;
                            }
                        }
                        else
                        {
                            pic2 = "/Images/none.gif";
                        }

                        Strings.Append(string.Format("<dl><dt><IMG src=\"{0}\" onerror=\"this.src='/Images/none.gif'\"/></dt>", pic2));
                        Strings.Append(string.Format("<dd><div class=p-name><SPAN class=dayrank>{0}</SPAN>{1}</div>", elemList[i].SelectSingleNode("daterank").InnerText, elemList[i].SelectSingleNode("rname").InnerText));
                        Strings.Append("<div class=stander><UL>");
                        Strings.Append(string.Format("<LI>交通：<SPAN>{0}</SPAN></LI>", elemList[i].SelectSingleNode("bus").InnerText));
                        Strings.Append(string.Format("<LI>用餐：<SPAN>{0}</SPAN></LI>", elemList[i].SelectSingleNode("dinner").InnerText));
                        Strings.Append(string.Format("<LI>住宿：<SPAN>{0}</LI>", elemList[i].SelectSingleNode("room").InnerText));
                        Strings.Append("</UL></div>");
                        Strings.Append(string.Format("<div class=ps>{0}</div>", elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>")));

                        //string aa = "<div class=Viewdiv><DIV class=ViewHead>福冈岸上观光</DIV><table id='RoomSelectList' style='width: 100%;'><tr class=tit><td width='30%'>观光线路</td><td width='10%'>停靠时间</td><td width='10%'>游览时间</td><td width='10%'>用餐</td><td width='10%'>价格</td></tr><tr><td width='30%'>龙头岩名宿村</td><td width='10%'>8点</td><td width='10%'>7小时</td><td width='10%'>韩国烧烤</td><td width='10%'>价格</td></tr></table></div>";
                        Strings.Append(string.Format("<div class=ps>{0}</div>", GetView(DS.Tables[0], i + 1)));

                        Strings.Append("</dd></dl>");
                        Strings.Append("");
                    }
                    RouteInfos = Strings.ToString();
                    Strings.Clear();

                    if (PicString.ToString().Length > 10)
                    {
                        ImgList = PicString.ToString();
                    }
                    else
                    {
                        FirstImg = "<IMG src=\"/Images/none.gif\" width=320 height=240 jqimg=\"/Images/none.gif\">";
                        ImgList = "<LI><IMG src=\"/Images/none.gif\" width=62 height=50></LI>";
                    }
                }
                // 图片不存在 onerror="this.src='http://www.360buy.com/images/none/none_50.gif'"
                //LineName = "文件存在";
            }
            else
            {
                //LineName = "文件不存在";
            }
        }

        protected string GetView(DataTable dt, int days)
        {
            StringBuilder Strings = new StringBuilder();
            DataRow[] drs = dt.Select("days=" + days);
            if (drs.Count() > 0)
            {
                int i = 0;
                foreach (DataRow dr in drs)
                {
                    if (i == 0)
                    {
                        Strings.Append("<div class=Viewdiv><DIV class=ViewHead>" + dr["vtitle"].ToString() + "</DIV><table id='RoomSelectList' style='width: 100%;'><tr class=tit><td width='30%'>观光线路</td><td width='15%'>停靠时间</td><td width='15%'>游览时间</td><td width='15%'>用餐</td><td width='15%'>价格</td><td width='10%'>&nbsp;</td></tr>");
                    }
                    Strings.Append(string.Format("<tr><td>{0}&nbsp;</td><td>{1}&nbsp;</td><td>{2}&nbsp;</td><td>{3}&nbsp;</td><td class=pcount>&yen;{4}</td><td><a href=\"javascript:void(0);\" onclick=\"ShowSight({6})\">详情</a></td></tr><tr class=\"hide viewtr\" id=Sight{6}><td colspan=\"6\"><div class=Sight>{5}</div></td></tr>",
                        dr["visitname"].ToString(),
                        dr["stay"].ToString(),
                        dr["sight"].ToString(),
                        dr["dinner"].ToString(),
                        dr["price"].ToString(),
                        dr["intro"].ToString(),
                        dr["id"].ToString()
                    ));
                    i++;
                }
                Strings.Append("</table></div>");
                        
            }
            
            return Strings.ToString();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    ucode = CombineKeys.NewComb();

        //    PurchaseClass.LineClass LineInfos = new PurchaseClass.LineClass();
        //    LineInfos = PurchaseClass.LineDetail(Request.Form["TB_LineId"]);
        //    if (LineInfos != null)
        //    {
        //        List<string> Sql = new List<string>();

        //        string SqlQueryText = string.Format("select *,(select count(id) from CR_RoomList where orderflag='0' and allotid=CR_RoomAllot.id) as sellroom from CR_RoomAllot where id in ({0}) and lineid='{1}'", Request.Form["RS_ID"], Request.Form["TB_LineId"]);
        //        DataSet DS = new DataSet();
        //        DS.Clear();
        //        DS = MyDataBaseComm.getDataSet(SqlQueryText);

        //        //TB_LineId=2043&TB_BeginDate=2011-09-11&AllPeople=3&AllAdult=2&AllChilds=1&AllRoom=1&AllPrice=700&RS_ID=11&RS_CR=2&RS_ET=1&RS_ROOM=1&RS_PRICE=700
        //        string[] AllotId = Request.Form["RS_ID"].Split(',');
        //        string[] AllRoom = Request.Form["RS_ROOM"].Split(',');
        //        string[] AllMen = Request.Form["RS_NUM"].Split(',');
        //        string[] AllAdult = Request.Form["RS_CR"].Split(',');
        //        string[] AllChild = Request.Form["RS_ET"].Split(',');
        //        string[] AllPrice = Request.Form["RS_PRICE"].Split(',');

        //        SqlQueryText = "";
        //        //判断是否有舱位,计算舱位价格，保存到舱位价格表
        //        for (int i = 0; i < AllotId.Length - 1; i++)
        //        {
        //            Sql.Add(GetRoomSqlstr(DS.Tables[0], AllotId[i], AllPrice[i], Convert.ToInt32(AllRoom[i]), AllMen[i], AllAdult[i], AllChild[i]));
        //            SqlQueryText += GetRoomSqlstr(DS.Tables[0], AllotId[i], AllPrice[i], Convert.ToInt32(AllRoom[i]), AllMen[i], AllAdult[i], AllChild[i]) + "$$$";
        //        }

        //        Response.Write(Request.Form["RS_ID"] + SqlQueryText);
        //        Response.End();

        //        //保存订单信息，返回订单uid
        //        Sql.Add(string.Format("insert into OL_TempOrder (OrderId,ProductType,ProductClass,LineID,PlanId,LineName,BeginDate,OrderNums,Adults,Childs,OrderTime,OrderUser,DeptId,LineDays,RouteFlag,PlanNo,shipid,orderdept,ordercompany,ProductNum) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",
        //                ucode,
        //                LineInfos.LineType,
        //                LineInfos.LinesClass,
        //                LineInfos.LineId,
        //                LineInfos.Planid,
        //                LineInfos.LineName,
        //                MyConvert.ConToDate(Request.Form["TB_BeginDate"]),
        //                Request.Form["AllPeople"],
        //                Request.Form["AllAdult"],
        //                Request.Form["AllChilds"],
        //                DateTime.Now.ToString(),
        //                Convert.ToString(Session["Online_UserId"]),
        //                LineInfos.Deptid,
        //                LineInfos.LineDays,
        //                "1",
        //                "",
        //                LineInfos.Shipid,
        //                Convert.ToString(Session["Online_UserDept"]),
        //                Convert.ToString(Session["Online_UserCompany"]),
        //                Request.Form["AllRoom"]
        //        ));

        //        string[] SqlQueryList = Sql.ToArray();
        //        if (MyDataBaseComm.Transaction(SqlQueryList) == true)
        //        {
        //            Response.Write("({\"success\":\"" + ucode + "\"})");
        //        }
        //        else
        //        {
        //            Response.Write("({\"error\":\"订单预定失败，请稍后再试\"})");
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("({\"error\":\"旅游线路加载错误\"})");
        //    }
        //}

        //protected string GetRoomSqlstr(DataTable dt, string allotid, string price, int allroom, string allmen, string adult, string childs)
        //{
        //    string sql = "";
        //    DataRow[] drs = dt.Select("id=" + allotid);
        //    if (drs.Count() > 0)
        //    {
        //        foreach (DataRow dr in drs)
        //        {
        //            int haveroom = Convert.ToInt32(dr["nums"].ToString()) - Convert.ToInt32(dr["sellroom"].ToString());
        //            if (haveroom < allroom)
        //            {
        //                Response.Write("({\"error\":\"" + dr["roomname"].ToString() + "房间剩余数量不足\"})");
        //                Response.End();
        //            }
        //            else
        //            {
        //                sql = string.Format("insert into CR_RoomOrder (OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,adult,childs,peoples,rooms,gather) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
        //                    ucode,
        //                    dr["lineid"].ToString(),
        //                    dr["shipid"].ToString(),
        //                    dr["id"].ToString(),
        //                    dr["roomid"].ToString(),
        //                    dr["price"].ToString(),
        //                    dr["thirdprice"].ToString(),
        //                    dr["childprice"].ToString(),
        //                    dr["rebate"].ToString(),
        //                    dr["thirdrebate"].ToString(),
        //                    dr["childrebate"].ToString(),
        //                    dr["adult"].ToString(),
        //                    dr["childs"].ToString(),
        //                    dr["allmen"].ToString(),
        //                    dr["allroom"].ToString(),
        //                    dr["price"].ToString()
        //                );
        //                //OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,
        //                //adult,childs,peoples,rooms,gather
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("({\"error\":\"您选择的房型不存在，请重新选择\"})");
        //        Response.End();
        //    }

        //    return sql;
        //}

    }
}