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

namespace TravelOnline.NewPage
{
    public partial class CruiseDetail : System.Web.UI.Page
    {
        public string BodyId = "outbound", tag = "tg", Pics, Tags, Preference, CruisesRoute, RoomNo1, RoomNo2;
        public string LineId, BreadCrumb, linetype, LineName, LineFeature, Price, begindate, LineDays;
        public string PlanDateJason = "", RouteFeature, LineViews, ContractInfos;
        public int province, lineclass;
        public string[] ViewsArray;
        public string RouteBigImg = "", RouteSmallImg = "";
        public string PriceIn, PriceIn1, PriceOut, OwnExpense, Attentions, Shopping;
        public string NavDays = "", Routes = "", hide = "hide", VisaFileHide = "", VisaFileInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LineId = Request.QueryString["id"];
            if (MyConvert.ConToInt(LineId) == 0) Response.Redirect("~/index.html", true);
            //Response.Write(LineId);
            LoadLineInfo();
        }

        protected void LoadLineInfo()
        {

            string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", LineId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                linetype = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();
                lineclass = MyConvert.ConToInt(DS.Tables[0].Rows[0]["LineClass"].ToString());
                province = MyConvert.ConToInt(DS.Tables[0].Rows[0]["FirstDestination"].ToString());
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LineName = LineName.Replace(",", "");
                LineName = LineName.Replace("'", "");
                LineName = LineName.Replace("@", "");
                LineName = LineName.Replace("<", "");
                LineName = LineName.Replace(">", "");
                LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "");
                begindate = string.Format("{0:yyyy-MM-dd}", MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["PlanDate"].ToString()));
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
                //优惠促销信息
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0) Preference = "在线预订立减 " + DS.Tables[0].Rows[0]["wwwyh"].ToString() + " 元/人";

                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "2") tag = "tz";
                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "3") tag = "td";

                Tags = "<div class='trip-class yl'>邮轮</div>";

                Pics = "/images/none.gif";
                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);

                BreadCrumb = "<a href=\"/cruise.html\">邮轮旅游</a><span>></span>";
                BodyId = "cruises";

                BreadCrumb += string.Format("<a href=\"/search.html?s={1}-0-0-0-0-0-0-0\">{0}</a><span>></span>", DS.Tables[0].Rows[0]["LineClass"].ToString(), lineclass);
                BreadCrumb += "<h1>" + LineName + "</h1>";

                if (linetype == "outbound") ContractInfos = "<a target=\"_blank\" href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</a>";
                if (linetype == "outbound" && lineclass == 851) ContractInfos = "<a target=\"_blank\" href=\"/Upload/大陆居民赴台湾地区旅游合同.doc\">大陆居民赴台湾地区旅游合同范本</a>";

                VisaFileHide = "<li><a href=\"#qzxx\">签证信息</a></li>";
                GetVisaFiles(DS.Tables[0].Rows[0]["VisaId"].ToString());

                if (VisaFileInfo == "")
                {
                    hide = "hide";
                    VisaFileHide = "";
                }
                
                if (RouteBigImg.Length < 5)
                {
                    RouteBigImg = string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" /></a></li>", "/images/none.gif");
                    RouteSmallImg = string.Format("<li class=\"active\"><img src=\"{0}\" /></li>", "/images/none.gif");
                }

                CruisesRoomList(LineId);
                CruisesRoute = CreateCruisesRoute(LineId);
                ReadRouteXML();

            }
        }

        protected void CruisesRoomList(string id)
        {
            if (Convert.ToString(Session["Online_UserCompany"]).Length > 0)
            {
                GetCruisesRoomList(id, Convert.ToString(Session["Online_UserCompany"]));
                if (RoomNo1 == "") GetCruisesRoomList(id, "0");
            }
            else
            {
                GetCruisesRoomList(id, "0");
            }
        }

        protected void GetCruisesRoomList(string lineid, string companyid)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder Strings1 = new StringBuilder();
            string recommend = ""; //(nums-sellroom)>=0 and 
            string SqlQueryText = "SELECT *,(nums-sellroom) as haveroom from View_CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' order by typeid";
            if (companyid != "0") SqlQueryText = "SELECT *,(nums-sellroom) as haveroom from View_CR_RoomAllot where companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string T_Price, C_Price;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    recommend = "<dd class='t7' tag='" + DS.Tables[0].Rows[i]["id"].ToString() + "'><div class='btn'><a href='javascript:;' class='xuan'>选择房间</a></div></dd>";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["haveroom"].ToString()) <= 0) recommend = "<dd class='t7' tag='0'><div class='btn'><a href='javascript:;' class='shouwan'>已售罄</a></div></dd>";
                    T_Price = "--";// &yen;
                    C_Price = "--";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = "<span><em>￥</em>" + DS.Tables[0].Rows[i]["thirdprice"].ToString() + "</span>";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = "<span><em>￥</em>" + DS.Tables[0].Rows[i]["childprice"].ToString() + "</span>";

                    if (i == 0)
                    {
                        Strings1.Append(string.Format("<li class=\"cur\">{0}</li>", DS.Tables[0].Rows[i]["typename"].ToString()));
                        Strings.Append(string.Format("<div class='room'>", ""));
                    }
                    else
                    {
                        if (DS.Tables[0].Rows[i]["typeid"].ToString() != DS.Tables[0].Rows[i - 1]["typeid"].ToString())
                        {
                            Strings1.Append(string.Format("<li>{0}</li>", DS.Tables[0].Rows[i]["typename"].ToString()));
                            Strings.Append("</div>");
                            Strings.Append(string.Format("<div class='room hide'>", ""));
                        }
                    }

                    Strings.Append(string.Format(@"<div class='choose-box clearfix'>
                            	<dl class='sdl'>
                                	<dd class='t1'>
                                    	<div class='cangfang' tag='{6}'><b></b>{0}<div class='cf-detail'></div></div>
                                    </dd>
                                    <dd class='t2'>{1}</dd>
                                    <dd class='t3'>{2}</dd>
                                    <dd class='t4 price'><span><em>￥</em>{3}</span></dd>
                                    <dd class='t5 price'>{4}</dd>
                                    <dd class='t6 price'>{5}</dd>
                                    {7}
                                    <div class='choose'></div>
                                    <div class='clear'></div>
                                </dl>
                            </div>",
                        DS.Tables[0].Rows[i]["roomname"].ToString(),
                        DS.Tables[0].Rows[i]["area"].ToString(),
                        DS.Tables[0].Rows[i]["berth"].ToString(),
                        DS.Tables[0].Rows[i]["price"].ToString(),
                        T_Price,
                        C_Price,
                        DS.Tables[0].Rows[i]["roomid"].ToString(),
                        recommend
                    ));
                    if (i == DS.Tables[0].Rows.Count - 1) Strings.Append("</div>");
                }
                RoomNo1 = Strings1.ToString();
                RoomNo2 = Strings.ToString();
            }
            else
            {
                RoomNo1 = "";
                RoomNo2 = "";
            }
        }

        public static string CreateCruisesRoute(string lineid)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("NewCruisesRouteString{0}", lineid)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText;
                SqlQueryText = string.Format("select * from CR_Route where lineid='{0}'", lineid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<tr><td>第{0}天</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", 
                        DS.Tables[0].Rows[i]["days"], 
                        DS.Tables[0].Rows[i]["harbour"], 
                        DS.Tables[0].Rows[i]["arrive"], 
                        DS.Tables[0].Rows[i]["sail"]));
                }
                HttpContext.Current.Cache.Insert(string.Format("NewCruisesRouteString{0}", lineid), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("NewCruisesRouteString{0}", lineid)]);
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
                    hide = "";
                    VisaFileHide = "<li><a href=\"#qzxx\">签证信息</a></li>";
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
        
        protected void ReadRouteXML()
        {
            DataSet DS = new DataSet();
            DS.Clear();
            string SqlQueryText = string.Format("select * from CR_Visit where sellflag='0' and lineid='{0}' order by days,id", LineId);
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, LineId);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = "<li>" + x.SelectSingleNode("Feature").InnerText.Replace("\n", "</li><li>") + "</li>";
                    PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</li><li>");
                    PriceIn = "<li>" + PriceIn + "</li>";
                    if (PriceIn.Length < 10) PriceIn = "无";

                    PriceIn1 = "<p>" + x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</p><p>") + "</p>";
                    if (PriceIn1.Length < 9) PriceIn1 = "无";
                    PriceOut = "<p>" + x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "</p><p>") + "</p>";
                    if (PriceOut.Length < 9) PriceOut = "无";
                    OwnExpense = "<p>" + x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "</p><p>") + "</p>";
                    if (OwnExpense.Length < 9) OwnExpense = "无";
                    Attentions = "<p>" + x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</p><p>") + "</p>";
                    if (Attentions.Length < 9) Attentions = "无";
                    Shopping = "<p>" + x.SelectSingleNode("Shopping").InnerText.Replace("\n", "</p><p>") + "</p>";
                    if (Shopping.Length < 9) Shopping = "无";

                    XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                    int PicCount = 0;
                    string class1 = " class=\"pink\"", class2 = "", Views = "";

                    string[] sArray;
                    string[] ArrayId = new string[0];
                    string[] ArrayName = new string[0];
                    string[] ArrayPic = new string[0];
                    string[] ArrayDays = new string[0];
                    string[] ArrayIntro = new string[0];
                    int ViewCount = 0;
                    //图片匹配
                    if (ViewsArray != null)
                    {
                        ArrayId = new string[ViewsArray.Length];
                        ArrayName = new string[ViewsArray.Length];
                        ArrayPic = new string[ViewsArray.Length];
                        ArrayDays = new string[ViewsArray.Length];
                        ArrayIntro = new string[ViewsArray.Length];
                        for (int ii = 0; ii < ViewsArray.Length - 1; ii++)
                        {
                            sArray = ViewsArray[ii].Split('|');
                            ArrayId[ii] = sArray[0].ToString();
                            ArrayName[ii] = sArray[1].ToString();
                            ArrayPic[ii] = sArray[2].ToString();
                            ArrayDays[ii] = sArray[3].ToString();
                            ArrayIntro[ii] = sArray[4].ToString();
                        }
                    }
                    //行程景点图片需要生成
                    //string Views = "<li><a href=\"javascript:;\"><img src=\"../image/detail/d1.png\" alt=\"\" title=\"\" target=\"_blank\"><span>函馆生鲜市场</span></a></li>";

                    for (int i = 0; i < elemList.Count; i++)
                    {
                        ViewCount = 0;
                        Views = "";
                        if (ViewsArray != null)
                        {
                            for (int a = 0; a < ArrayName.Length - 1; a++)
                            {
                                if (ArrayName[a].ToString() != "OK")
                                {
                                    if (MyConvert.ConToInt(ArrayDays[a]) == i + 1)
                                    {
                                        if (ViewCount < 3)
                                        {
                                            Views += string.Format("<li><a href=\"javascript:;\"><img src=\"{1}\" alt=\"\"><span>{2}</span></a></li>", ArrayId[a].ToString(), ArrayPic[a].ToString(), ArrayName[a].ToString());
                                        }
                                        ViewCount++;
                                        ArrayName[a] = "OK";
                                    }
                                }
                            }
                        }

                        string picpath, pic2, pic3;
                        if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 10)
                        {
                            picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, elemList[i].SelectSingleNode("Pics").InnerText);
                            pic2 = "/Images/none.gif";
                            pic3 = "/Images/none.gif";
                            try
                            {
                                pic2 = string.Format("/Images/Views/{0}/M_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                pic3 = string.Format("/Images/Views/{0}/S_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                            }
                            catch
                            { }
                            if (System.IO.File.Exists(picpath) == true)
                            {
                                if (PicCount == 0)
                                {
                                    RouteBigImg += string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" /></a></li>", pic2);
                                    RouteSmallImg += string.Format("<li class=\"active\"><img src=\"{0}\" /></li>", pic3);
                                }
                                else
                                {
                                    RouteBigImg += string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" /></a></li>", pic2);
                                    RouteSmallImg += string.Format("<li><img src=\"{0}\" /></li>", pic3);
                                }
                                PicCount++;
                            }
                        }

                        if (i > 0) class1 = "";
                        if (i > 0) class2 = " mrt";
                        //NavDays += string.Format("<a{0} href=\"#{1}day\">{2}</a>", class1, i, elemList[i].SelectSingleNode("daterank").InnerText);
                        NavDays += string.Format("<li{0}><a href=\"#{1}day\">{2}</a></li>", class1, i, elemList[i].SelectSingleNode("daterank").InnerText);

                        Routes += string.Format(@"
                            <div id='{0}day' class='day-list{1}'>
	                            <div class='day-tit'>
		                            <h5>{2}</h5>
		                            <h4>{3}</h4>
	                            </div>
	                            <div class='day-content'>
		                            <ul>
			                            <li class='clearfix'>
				                            <i class='traffic'></i>
				                            <span>交通</span>
				                            <p class='txt'>{4}</p>
			                            </li>
			                            <li class='clearfix'>
				                            <i class='dinner'></i>
				                            <span>用餐</span>
				                            <p>{5}</p>
			                            </li>
			                            <li class='clearfix'>
				                            <i class='hotel'></i>
				                            <span>住宿</span>
				                            <p class='txt'>{6}</p>
			                            </li>
			                            <li class='clearfix'>
				                            <i class='trip'></i>
				                            <span>行程</span>
				                            <p class='txt'>{7}</p>
			                            </li>
		                            </ul>
                                    <div class='pic-content c-pic'>
                                        <ul class='clearfix'>{8}</ul>
                                        {9}
                                    </div>
	                            </div>
                            </div>
                            ",
                            i,
                            class2,
                            elemList[i].SelectSingleNode("daterank").InnerText,
                            elemList[i].SelectSingleNode("rname").InnerText,
                            elemList[i].SelectSingleNode("bus").InnerText,
                            elemList[i].SelectSingleNode("dinner").InnerText,
                            elemList[i].SelectSingleNode("room").InnerText,
                            elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>"),
                            Views,
                            GetView(DS.Tables[0], i + 1)
                            );

                    }
                    
                }
            }
        }

        protected string GetView(DataTable dt, int days)
        {
            StringBuilder Strings = new StringBuilder();
            DataRow[] drs = dt.Select("days=" + days);
            if (drs.Count() > 0)
            {
                Strings.Append("<div class=\"shore\"><div class=\"click-bnt\">展开详情<i></i></div><div class=\"sight-box\">");
                int i = 0;
                foreach (DataRow dr in drs)
                {
                    Strings.Append(string.Format(@"
                        <div class='sight clearfix'>
                            <h2>{0}</h2>
                            <dl>
                                <dd><span>观光费用</span><em>￥{4}</em></dd>
                                <dd><span>停靠时间</span>{1}</dd>
                                <dd><span>游览时间</span>{2}</dd>
                                <dd><span>用餐</span>{3}</dd>
                            </dl>
                            <p>{5}</p>
                        </div>",
                        dr["visitname"].ToString(),
                        dr["stay"].ToString(),
                        dr["sight"].ToString(),
                        dr["dinner"].ToString(),
                        dr["price"].ToString(),
                        dr["intro"].ToString().Replace("\n", "<br/><br/>"),
                        dr["id"].ToString()
                    ));
                    i++;
                }
                Strings.Append("</div></div>");

            }

            return Strings.ToString();
        }

    }
}