using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using System.Text.RegularExpressions;
using TravelOnline.Class.Travel;
using TravelOnline.Destination.Class;

namespace TravelOnline.tour
{
    public partial class line : System.Web.UI.Page
    {
        public string VisaFileInfo = "", VisaFileHide = "hide", linetype, LineFlag, tags, mp_pic = "", CruisesRoomListString, RelativeLine, RelativeLineHide = "hide", LineViews, LineViewsHide = "hide";
        public int lineclass, province;
        public string Preference, RouteFeature, PlanDateJason, PlanDateString, RouteServiceInfos, PriceIn, PriceOut, OwnExpense, Attentions, Shopping, RouteInfos, CruisesRoute, CruisesPicUrl;//行程
        public string id, begindate, shipid, BreadCrumb, TitleString, LineName, LineFeature, Price, FirstImg, PicString;//线路信息
        public string CreateCalendar = "no", CanSell = "yes", NavString, LargeButtomString, SmallButtomString, ContractInfos, FirstPic;
        public string StopSellHide, PrintHide, CruisesRouteHide = "hide", CalendarHide = "hide", PlanDateHide = "hide", RouteFeatureHide = "hide", RouteServiceInfosHide = "hide", PlanDateDropHide = "hide", VisaFeatureHide = "hide";
        public string LineListInfo, VisaListInfo, VisaFeature, Visa1, Visa2, Visa3, Visa4, Visa5, Visa6, Visa1Hide = "hide", Visa2Hide = "hide", Visa3Hide = "hide", Visa4Hide = "hide", Visa5Hide = "hide", Visa6Hide = "hide";
        public string[] ViewsArray;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];

            if (MyConvert.ConToInt(id) == 0) Response.Redirect("~/index.html", true);
            //是否生成日历 CreateCalendar  是否可以销售CanSell
            if (id == "14120")
            {
                LargeButtomString = "<a id=\"zxyd\" href=\"javascript:\" style=\"margin-left:30px;\" onclick=\"Order()\" class=\"btn btn-large btn-warning\">在线预订</a>";
            }
            else
            {
                LargeButtomString = "<a href=\"javascript:\" style=\"margin-left:30px;\" onclick=\"Order()\" class=\"btn btn-large btn-warning\">在线预订</a>";
            }
            
            SmallButtomString = "<a id=\"nav_order\" href=\"javascript:\" onclick=\"Order()\" class=\"btn btn-warning hide\" style=\"margin-top:4px;margin-right:10px;float:right;\">在线预订</a>";
            LoadLineInfo();
        }

        protected void LoadLineInfo()
        {
            string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //参数提取
                linetype = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();
                lineclass = MyConvert.ConToInt(DS.Tables[0].Rows[0]["LineClass"].ToString());
                province = MyConvert.ConToInt(DS.Tables[0].Rows[0]["FirstDestination"].ToString());
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LineName = LineName.Replace(",", "");
                LineName = LineName.Replace("'", "");
                LineName = LineName.Replace("@", "");
                LineName = LineName.Replace("<", "");
                LineName = LineName.Replace(">", "");
                LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString() + " ";
                Price = DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "");
                begindate = string.Format("{0:yyyy-MM-dd}", MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["PlanDate"].ToString()));
                FirstPic = "/images/none.gif";
                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) FirstPic = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);

                tags = "";
                if (DS.Tables[0].Rows[0]["Tags"].ToString().Length > 5)
                {
                    try
                    {
                        string[] arr = DS.Tables[0].Rows[0]["Tags"].ToString().Split(',');
                        for (int l = 0; l < arr.Length - 1; l++)
                        {
                            if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src=\"/images/" + arr[l].ToString() + ".jpg\">";
                        }
                    }
                    catch
                    { }
                }

                //景点导览
                if (DS.Tables[0].Rows[0]["viewids"].ToString().Length > 2)
                {
                    LineViewsHide = "";
                    LineViews = PlaceClass.DestinationCache("LineViewsPic", DS.Tables[0].Rows[0]["viewids"].ToString(), id, 0,"");
                    
                    if (Convert.ToString(Cache["LineViewsPicArray_" + id]) != "")
                    {
                        ViewsArray = Convert.ToString(Cache["LineViewsPicArray_" + id]).Split('$');
                        //Response.Write(ViewsArray.Length);
                    }
                }


                if (DS.Tables[0].Rows[0]["famous"].ToString().Length > 0) mp_pic = string.Format("<img src=\"/img/mp_{0}.png\" title=\"青旅名牌产品\" class=\"mp_pic\">", DS.Tables[0].Rows[0]["famous"].ToString());

                if (linetype == "outbound") ContractInfos = "<li><strong>合同范本：</strong><a target=\"_blank\" href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</a></li>";
                if (linetype == "outbound" && lineclass == 851) ContractInfos = "<li><strong>合同范本：</strong><a target=\"_blank\" href=\"/Upload/大陆居民赴台湾地区旅游合同.doc\">大陆居民赴台湾地区旅游合同范本</a></li>";

                if (linetype == "inland") ContractInfos = "<li><strong>合同范本：</strong><a target=\"_blank\" href=\"/Upload/上海市国内旅游合同示范文本.doc\">上海市国内旅游合同范本</a></li>";

                if (linetype != "inland")
                {
                    GetVisaFiles(DS.Tables[0].Rows[0]["VisaId"].ToString());
                    if (VisaFileInfo.Length > 5) VisaFileHide = "";
                }

                //优惠促销信息
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0) Preference = "在线支付即享优惠 " + DS.Tables[0].Rows[0]["wwwyh"].ToString() + " 元/人";

                //LineFlag 1、2、3  1为正常线路 2为邮轮包船 3为签证
                LineFlag = "1";
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Shipid"].ToString()) > 0) LineFlag = "2";
                if (linetype == "visa") LineFlag = "3";

                //面包屑
                BreadCrumb = LineListClass.CreateBreadCrumb(linetype, lineclass, province, 0, 3);
                BreadCrumb += string.Format("<li class=\"active\">{0}</li>", LineName);

                //title
                TitleString = LineName + " ";
                TitleString += LineListClass.GetLineClassTitleString(lineclass) + " ";
                if (LineFlag != "3") TitleString += LineListClass.GetLineProvinceTitleString(province) + " ";
                TitleString += LineListClass.CreateTitleString(linetype) + " ";

                //提取计划出发日期jason
                if (LineFlag == "1")
                {
                    CreatePlanDateJason();
                    ReadRouteXML();
                    VisaListInfo = "hide";
                }

                if (LineFlag == "2")
                {
                    SmallButtomString = "<span id=\"nav_order\"></span>";
                    StopSellHide = "hide";
                    CruisesRouteHide = "";
                    ReadRouteXML();
                    CruisesRoute = LinePreferences.CruisesRouteString(id);
                    CruisesPicUrl = LinePreferences.NewCruisesPicUrl(DS.Tables[0].Rows[0]["Shipid"].ToString());
                    VisaListInfo = "hide";
                    CruisesRoomListString = CruisesRoomList(id);
                }

                if (LineFlag == "3")
                {
                    VisaFeature = DS.Tables[0].Rows[0]["Pdates"].ToString();
                    if (VisaFeature.Length > 3) VisaFeature = string.Format("有效期：{0}&nbsp;&nbsp;&nbsp;&nbsp;停留时间：{1}&nbsp;&nbsp;&nbsp;&nbsp;工作日：{2}<br>", VisaFeature.Split("$".ToCharArray())[0], VisaFeature.Split("$".ToCharArray())[1], VisaFeature.Split("$".ToCharArray())[2]);

                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length > 5)
                    {
                        FirstImg = "/images/shadow/" + DS.Tables[0].Rows[0]["Pics"].ToString();
                        PicString = string.Format("<li class=\"current\"><img src-org=\"{0}\" src=\"{0}\" alt=\"{0}\" onerror=\"this.src='/Images/none.gif'\"></li>", FirstImg, FirstImg, LineName);
                    }
                    else
                    {
                        FirstImg = "/Images/none.gif";
                        PicString = string.Format("<li class=\"current\"><img src-org=\"{0}\" src=\"{0}\" alt=\"{0}\" onerror=\"this.src='/Images/none.gif'\"></li>", FirstImg, FirstImg, LineName);
                    }
                    FirstPic = FirstImg;

                    ReadVisaXML();
                    LineListInfo = "hide";
                    PrintHide = "hide";
                    VisaFeatureHide = "";

                    //FirstPic = pic3;
                }
                //CalendarHide = "";
                //CanSell = "yes";
                //生成nav导航
                NavString = "";
                if (LineFlag == "1" || LineFlag == "2")
                {
                    if (CalendarHide == "" || PlanDateHide == "") NavString += "<li class=\"current\"><a href=\"#plan_calendar\">出发日期</a></li>";
                    if (NavString == "")
                    {
                        if (LineFlag == "2")
                        {
                            NavString += "<li class=\"current\"><a href=\"#room_order\">舱房预订</a></li><li><a href=\"#route_info\">行程安排</a></li>";
                        }
                        else
                        {
                            NavString += "<li class=\"current\"><a href=\"#route_info\">行程安排</a></li>";
                        }
                    }
                    else
                    {
                        NavString += "<li><a href=\"#route_info\">行程安排</a></li>";
                    }

                    NavString += "<li><a href=\"#price_info\">费用描述</a></li>";
                    NavString += "<li><a href=\"#memo_info\">温馨提醒</a></li>";
                    NavString += "<li><a href=\"#order_service\">预订须知</a></li>";

                }
                else
                {
                    NavString += "<li class=\"current\"><a href=\"#visa_info\">所需材料</a></li>";
                    NavString += "<li><a href=\"#order_service\">预订须知</a></li>";
                }

                //不能销售的线路，最终设置参数, 生成按钮禁用式样等等
                if (CanSell == "no")
                {
                    LargeButtomString = "<a href=\"#\" style=\"margin-left:30px;\" class=\"btn btn-large disabled\">线路停售</a>";
                    SmallButtomString = "<span id=\"nav_order\"></span>";
                    StopSellHide = "hide";
                    PlanDateJason = "";
                    PlanDateString = "";
                }

                RelativeLine = LineRecommend.LineRecommendCache("LineDetailRecommend_" + linetype + "_" + id.ToString(), "NewRecom", "3", "SmallPic", linetype, lineclass.ToString(), province.ToString(), id.ToString(), 4);
                if (RelativeLine.Length > 10) RelativeLineHide = "";
            }
            else
            {
                Response.Write("没有找到您查询的线路！");
                Response.End();
            }
        }

        protected void GetVisaFiles(string visaid)
        {
            if (visaid.Length > 1)
            {
                try
                {
                    string[] visas = Regex.Split(visaid, ",", RegexOptions.IgnoreCase);
                    //VisaFileInfo = visas.Length.ToString();
                    int i = 0;
                    for (i = 0; i < visas.Length; i++)
                    {
                        if (visas[i].ToString().Length > 0)
                        {
                            string path = string.Format(@"{0}OfficeFiles\WWW_File\{1}\", AppDomain.CurrentDomain.BaseDirectory, visas[i].ToString());
                            if (System.IO.Directory.Exists(path) == true)
                            {
                                DirectoryInfo Dir = new DirectoryInfo(path);
                                foreach (FileInfo f in Dir.GetFiles("*.*")) //查找文件  <img src='/images/file.gif'></img>
                                {
                                    VisaFileInfo += string.Format("<A class=visafile target=_blank href=\"/OfficeFiles/WWW_File/{0}/{1}\"><i class=\"icon-file\"></i>{1}</A> ", visas[i].ToString(), f.Name);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    VisaFileInfo = "";
                }
            }
            else
            {
                VisaFileInfo = "";
            }


        }

        protected string CruisesRoomList(string id)
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
            return countent;
        }

        protected string GetCruisesRoomList(string lineid, string companyid)
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
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["recommend"].ToString()) > 0) recommend = "<img class=yltags src=\"/images/cruises/" + DS.Tables[0].Rows[i]["recommend"].ToString() + ".jpg\">";
                        T_Price = "--";// &yen;
                        C_Price = "--";
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();
                        string taiwan99 = "";
                        if (id == "12509")
                        {
                            if (DS.Tables[0].Rows[i]["typename"].ToString() == "内舱房") taiwan99 = "id=\"neicangfang\"";
                            if (DS.Tables[0].Rows[i]["typename"].ToString() == "皇家大道景观房") taiwan99 = "id=\"huangjiafang\"";
                            if (DS.Tables[0].Rows[i]["typename"].ToString() == "海景房") taiwan99 = "id=\"haijingfang\"";
                            if (DS.Tables[0].Rows[i]["typename"].ToString() == "露台房") taiwan99 = "id=\"lutaifang\"";
                            if (DS.Tables[0].Rows[i]["typename"].ToString() == "套房") taiwan99 = "id=\"taofang\"";
                        
                        }
                        if (i == 0)
                        {
                            Strings1.Append(string.Format("<li><a {2} class=\"selected\" href=\"#room{0}\">{1}</a></li>", DS.Tables[0].Rows[i]["typeid"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString(), taiwan99));
                            Strings.Append(string.Format("<div class=\"room\" id=\"room{0}\">", DS.Tables[0].Rows[i]["typeid"].ToString()));
                            Strings.Append("<table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=rtit><td width=\"32%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"10%\">甲板层</td><td width=\"8%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3、4成人</td><td class=al width=\"10%\">第3、4儿童</td><td width=\"8%\">&nbsp;</td></tr>");
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=\"btn btn-warning\" href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
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
                                Strings1.Append(string.Format("<li><a {2} href=\"#room{0}\">{1}</a></li>", DS.Tables[0].Rows[i]["typeid"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString(), taiwan99));

                                Strings.Append("</table></div>");
                                Strings.Append(string.Format("<div class=\"room hide\" id=\"room{0}\">", DS.Tables[0].Rows[i]["typeid"].ToString()));
                                Strings.Append("<table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=rtit><td width=\"32%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"10%\">甲板层</td><td width=\"8%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3、4成人</td><td class=al width=\"10%\">第3、4儿童</td><td width=\"8%\">&nbsp;</td></tr>");
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=\"btn btn-warning\" href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
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
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=\"btn btn-warning\" href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
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
                    //Strings1.Clear();
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

        //创建出发日期的jason
        protected void CreatePlanDateJason()
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            try
            {
                //PlanDateJason, PlanDateString 提取两个数据，一个是开班日期jason，一个是已生成的少于4个计划的html
                string[] ListInfo = Regex.Split(rsp.OnlinePlanDateCreate(UpPassWord, id), @"\@\@", RegexOptions.IgnoreCase);
                PlanDateJason = ListInfo[0];
                PlanDateString = ListInfo[1];
                CalendarHide = "";
                PlanDateDropHide = "";
                PlanDateHide = "hide";
                CreateCalendar = "yes";
                if (PlanDateString.Length > 10)
                {
                    PlanDateHide = "";
                    CalendarHide = "hide";
                    CreateCalendar = "no";

                }
                if (PlanDateJason.Length < 20)
                {
                    CanSell = "no";
                    CreateCalendar = "no";
                    PlanDateHide = "hide";
                    CalendarHide = "hide";
                    PlanDateDropHide = "hide";
                }

                //else
                //{
                //    CalendarHide = "";
                //    PlanDateDropHide = "";
                //    PlanDateHide = "hide";
                //    CreateCalendar = "yes";
                //}
            }
            catch
            {
                CanSell = "no";
            }
        }

        protected void ReadRouteXML()
        {
            DataSet DS = new DataSet();
            DS.Clear();
            if (LineFlag == "2")
            {
                string SqlQueryText = string.Format("select * from CR_Visit where sellflag='0' and lineid='{0}' order by days,id", id);
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
            }
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, id);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = x.SelectSingleNode("Feature").InnerText.Replace("\n", "<br>");
                    if (RouteFeature.Length > 0) RouteFeatureHide = "";

                    if (x.SelectSingleNode("Traffic").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>旅游交通：</strong>{0}</li>", x.SelectSingleNode("Traffic").InnerText));
                    if (x.SelectSingleNode("Hotel").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>住宿标准：</strong>{0}</li>", x.SelectSingleNode("Hotel").InnerText));
                    if (x.SelectSingleNode("Scenery").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>景点门票：</strong>{0}</li>", x.SelectSingleNode("Scenery").InnerText));
                    if (x.SelectSingleNode("Foods").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>用餐标准：</strong>{0}</li>", x.SelectSingleNode("Foods").InnerText));
                    if (x.SelectSingleNode("Guide").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>导游服务：</strong>{0}</li>", x.SelectSingleNode("Guide").InnerText));
                    if (x.SelectSingleNode("Insure").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>保险服务：</strong>{0}</li>", x.SelectSingleNode("Insure").InnerText));
                    if (x.SelectSingleNode("Others").InnerText.Length > 0) Strings.Append(string.Format("<li><strong>其他服务：</strong>{0}</li>", x.SelectSingleNode("Others").InnerText));
                    RouteServiceInfos = Strings.ToString();
                    if (RouteServiceInfos.Length > 2) RouteServiceInfosHide = "";
                    Strings.Clear();

                    PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "<br>");
                    if (PriceIn.Length == 0) PriceIn = "无";
                    PriceOut = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "<br>");
                    if (PriceOut.Length == 0) PriceOut = "无";
                    OwnExpense = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "<br>");
                    if (OwnExpense.Length == 0) OwnExpense = "无";
                    Attentions = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "<br>");
                    if (Attentions.Length == 0) Attentions = "无";
                    Shopping = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "<br>");
                    if (Shopping.Length == 0) Shopping = "无";

                    FirstImg = "/Images/none.gif";
                    PicString = string.Format("<li class=\"current\"><img src-org=\"/Images/none.gif\" src=\"/Images/none.gif\" alt=\"{0}\" onerror=\"this.src='/Images/none.gif'\"></li>", LineName);

                    string RouteDetail = "";
                    string[] sArray;
                    string[] ArrayId = new string[0];
                    string[] ArrayName = new string[0];
                    string[] ArrayPic = new string[0];
                    string[] ArrayIntro = new string[0];
                    int ViewCount = 0;
                    StringBuilder PicStrings = new StringBuilder();
                    //图片匹配
                    if (ViewsArray != null)
                    {
                        ArrayId = new string[ViewsArray.Length];
                        ArrayName = new string[ViewsArray.Length];
                        ArrayPic = new string[ViewsArray.Length];
                        ArrayIntro = new string[ViewsArray.Length];
                        for (int ii = 0; ii < ViewsArray.Length-1; ii++)
                        {
                            sArray = ViewsArray[ii].Split('|');
                            ArrayId[ii] = sArray[0].ToString();
                            ArrayName[ii] = sArray[1].ToString();
                            ArrayPic[ii] = sArray[2].ToString();
                            ArrayIntro[ii] = sArray[3].ToString();
                        }
                    }

                    XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                    int PicCount = 0;
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        string picpath, pic2, pic3;
                        if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 10)
                        {
                            picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, elemList[i].SelectSingleNode("Pics").InnerText);
                            //pic1 = "/Images/none.gif";
                            pic2 = "/Images/none.gif";
                            pic3 = "/Images/none.gif";
                            try
                            {
                                //pic1 = string.Format("/Images/Views/{0}", elemList[i].SelectSingleNode("Pics").InnerText);
                                pic2 = string.Format("/Images/Views/{0}/M_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                pic3 = string.Format("/Images/Views/{0}/S_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                            }
                            catch
                            { }
                            if (System.IO.File.Exists(picpath) == true)
                            {
                                if (PicCount == 0)
                                {
                                    FirstImg = pic2;
                                    PicString = string.Format("<li class=\"current\"><img src-org=\"{0}\" src=\"{0}\" alt=\"{0}\" onerror=\"this.src='/Images/none.gif'\"></li>", pic2, pic3, LineName);
                                }
                                else
                                {
                                    PicString += string.Format("<li><img src-org=\"{0}\" src=\"{0}\" alt=\"{0}\" onerror=\"this.src='/Images/none.gif'\"></li>", pic2, pic3, LineName);
                                }
                                PicCount++;
                            }
                        }
                        else
                        {
                            pic2 = "/Images/none.gif";
                        }
                        
                        Strings.Append(string.Format("<dl><dt><IMG src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{2}\" /></dt><dd><div class=\"p-name\"><em>{1}</em>{2}</div>", pic2, elemList[i].SelectSingleNode("daterank").InnerText, elemList[i].SelectSingleNode("rname").InnerText));
                        Strings.Append("<div class=\"alert stander\"><ul>");
                        Strings.Append(string.Format("<li>交通：<span>{0}</span></li>", elemList[i].SelectSingleNode("bus").InnerText));
                        Strings.Append(string.Format("<li>用餐：<span>{0}</span></li>", elemList[i].SelectSingleNode("dinner").InnerText));
                        Strings.Append(string.Format("<li>住宿：<span>{0}</span></li>", elemList[i].SelectSingleNode("room").InnerText));
                        Strings.Append("</UL></div>");
                        RouteDetail = elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>");
                        ViewCount = 0;
                        PicStrings.Clear();
                        if (ViewsArray != null)
                        {
                            for (int a = 0; a < ArrayName.Length-1; a++)
                            {
                                if (ArrayName[a].ToString() != "OK")
                                {
                                    if (RouteDetail.IndexOf("【" + ArrayName[a].ToString() + "】") > -1)
                                    {
                                        RouteDetail = RouteDetail.Replace("【" + ArrayName[a].ToString() + "】", string.Format("【<a class=\"product\" target=\"_blank\" href=\"/sightdetail/{0}.html\">{1}</a>】", ArrayId[a].ToString(), ArrayName[a].ToString()));
                                        if (ViewCount < 5)
                                        {
                                            PicStrings.Append("<li>");
                                            PicStrings.Append(string.Format("<a href=\"/sightdetail/{0}.html\" target=\"_blank\"><img src=\"/images/none.gif\" data-original=\"{1}\" alt=\"{2}\"></a>", ArrayId[a].ToString(), ArrayPic[a].ToString(), ArrayName[a].ToString()));
                                            PicStrings.Append(string.Format("<dl><dt><i class=\"sight\"></i><a href=\"/sightdetail/{0}.html\" target=\"_blank\">{1}</a></dt>", ArrayId[a].ToString(), ArrayName[a].ToString()));
                                            PicStrings.Append(string.Format("<dd>{0}</dd></dl></li>", ArrayIntro[a].ToString()));
                                        }
                                        ViewCount++;
                                        ArrayName[a] = "OK";
                                    }
                                }
                            }
                        }
                                        
                        Strings.Append(string.Format("<div class=\"route\">{0}</div>", RouteDetail));
                        if (ViewCount > 0) Strings.Append(string.Format("<div class=\"view_list\"><ul>{0}</ul></div>", PicStrings.ToString()));

                        if (LineFlag == "2")
                        {
                            Strings.Append(string.Format("<div class=\"route\">{0}</div>", GetView(DS.Tables[0], i + 1)));
                        }

                        Strings.Append("</dd></dl>");
                        Strings.Append("");
                    }
                    RouteInfos = Strings.ToString();
                    Strings.Clear();

                }
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
                    Strings.Append(string.Format("<tr><td>{0}&nbsp;</td><td>{1}&nbsp;</td><td>{2}&nbsp;</td><td>{3}&nbsp;</td><td class=price>&yen;{4}</td><td><a href=\"javascript:void(0);\" onclick=\"ShowSight({6})\">详情</a></td></tr><tr class=\"viewtr hide\" id=Sight{6}><td colspan=\"6\"><div class=Sight>{5}</div></td></tr>",
                        dr["visitname"].ToString(),
                        dr["stay"].ToString(),
                        dr["sight"].ToString(),
                        dr["dinner"].ToString(),
                        dr["price"].ToString(),
                        dr["intro"].ToString().Replace("\n", "<br>"),
                        dr["id"].ToString()
                    ));
                    i++;
                }
                Strings.Append("</table></div>");

            }

            return Strings.ToString();
        }

        protected void ReadVisaXML()
        {
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, id);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = x.SelectSingleNode("Memo").InnerText.Replace("\n", "<br>");

                    StringBuilder PicString = new StringBuilder();
                    XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        Strings.Clear();
                        Strings.Append(string.Format("<dl><dt>{0}：</dt><dd>", elemList[i].SelectSingleNode("VisaName").InnerText));
                        Strings.Append(string.Format("<div>{0}</div>", elemList[i].SelectSingleNode("VisaContent").InnerText.Replace("\n", "<br>")));
                        if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0 || elemList[i].SelectSingleNode("V2").InnerText.Length != 0)
                        {
                            Strings.Append("<div class=files>");
                            if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0) Strings.Append(string.Format("原件：{0} &nbsp;&nbsp;&nbsp;&nbsp;", elemList[i].SelectSingleNode("V1").InnerText));
                            if (elemList[i].SelectSingleNode("V2").InnerText.Length != 0) Strings.Append(string.Format("复印件：{0}", elemList[i].SelectSingleNode("V2").InnerText));
                            Strings.Append("</div>");
                        }
                        Strings.Append("</dd></dl>");
                        Strings.Append("");
                        switch (elemList[i].SelectSingleNode("Flag").InnerText)
                        {
                            case "1":
                                Visa1 += Strings.ToString();
                                Visa1Hide = "";
                                break;
                            case "2":
                                Visa2 += Strings.ToString();
                                Visa2Hide = "";
                                break;
                            case "3":
                                Visa3 += Strings.ToString();
                                Visa3Hide = "";
                                break;
                            case "4":
                                Visa4 += Strings.ToString();
                                Visa4Hide = "";
                                break;
                            case "5":
                                Visa5 += Strings.ToString();
                                Visa5Hide = "";
                                break;
                            case "6":
                                Visa6 += Strings.ToString();
                                Visa6Hide = "";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }


    }
}