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
    public partial class line : System.Web.UI.Page
    {
        public string BodyId = "outbound", tag = "tg", Pics, Tags, Preference;
        public string LineId, BreadCrumb, linetype, LineName, LineFeature, Price="0", begindate, LineDays, PlanId, Sale, isPreBook, isSeckill="0", canSale="1";
        public string PlanDateJason="", RouteFeature, LineViews, ContractInfos;
        public int province, lineclass;
        public string[] ViewsArray;
        public string RouteBigImg="", RouteSmallImg="";
        public string PriceIn, PriceIn1, PriceOut, OwnExpense, Attentions, Shopping;
        public string NavDays = "", Routes = "", hide = "hide", VisaFileHide="", VisaFileInfo = "";
        public string seckillNum;
        protected void Page_Load(object sender, EventArgs e)
        {
            LineId = Request.QueryString["id"];
            if (MyConvert.ConToInt(LineId) == 0) Response.Redirect("~/index.html", true);
            //Response.Write(LineId);
            LoadLineInfo();
        }

        protected void LoadLineInfo()
        {
            string SqlQueryText = string.Format("select top 1 *,(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = OL_Line.FirstDestination)) AS DestName from OL_Line where MisLineId='{0}'", LineId);
            string notshow = ConfigurationManager.AppSettings["NotShow"];
            if (notshow != null)
            {
                SqlQueryText = string.Format("select top 1 *,(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = OL_Line.FirstDestination)) AS DestName from OL_Line where MisLineId='{0}' and MisLineId !='{1}'", LineId, notshow);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                linetype = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();

                if (linetype == "visa")
                {
                    Server.Transfer("/newpage/VisaDetail.aspx?id=" + LineId);
                }
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Shipid"].ToString()) > 0)
                {
                    Server.Transfer("/newpage/CruiseDetail.aspx?id=" + LineId);
                }
                lineclass = MyConvert.ConToInt(DS.Tables[0].Rows[0]["LineClass"].ToString());
                province = MyConvert.ConToInt(DS.Tables[0].Rows[0]["FirstDestination"].ToString());
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LineName = LineName.Replace(",", "");
                LineName = LineName.Replace("'", "");
                LineName = LineName.Replace("@", "");
                LineName = LineName.Replace("<", "");
                LineName = LineName.Replace(">", "");
                LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                PlanId = DS.Tables[0].Rows[0]["Planid"].ToString();
                Sale = DS.Tables[0].Rows[0]["sale"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "");
                begindate = string.Format("{0:yyyy-MM-dd}", MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["PlanDate"].ToString()));
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
                //优惠促销信息
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0) Preference = "在线预订立减 " + DS.Tables[0].Rows[0]["wwwyh"].ToString() + " 元/人";


                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "2") tag = "tz";
                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "3") tag = "td";

                Tags = "<div class='trip-class blue'>跟团游</div>";
                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "2") Tags = "<div class='trip-class orange'>自由行</div>";
                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "3") Tags = "<div class='trip-class td'>当地游</div>";
                if (DS.Tables[0].Rows[0]["PlanType"].ToString() == "4") Tags = "<div class='trip-class qz'>签证</div>";
                if (DS.Tables[0].Rows[0]["LineType"].ToString() == "Cruises") Tags = "<div class='trip-class yl'>邮轮</div>";

                Pics = "/images/none.gif";
                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        

                //景点导览
                //if (DS.Tables[0].Rows[0]["viewids"].ToString().Length > 2)
                //{
                //    LineViews = PlaceClass.DestinationCache("LineViewsPic", DS.Tables[0].Rows[0]["viewids"].ToString(), LineId, 0, "");

                //    if (Convert.ToString(Cache["LineViewsPicArray_" + LineId]) != "")
                //    {
                //        ViewsArray = Convert.ToString(Cache["LineViewsPicArray_" + LineId]).Split('$');
                //    }
                //}


                switch (linetype)
                {
                    case "inland":
                        BreadCrumb = "<a href=\"/inland.html\">国内旅游</a><span>></span>";
                        BodyId = "inland";
                        break;
                    case "outbound":
                        BreadCrumb = "<a href=\"/outbound.html\">出境旅游</a><span>></span>";
                        BodyId = "outbound";
                        break;
                    case "freetour":
                        BreadCrumb = "<a href=\"/freetour.html\">自由行</a><span>></span>";
                        BodyId = "freetour";
                        break;
                    case "cruises":
                        BreadCrumb = "<a href=\"/cruise.html\">邮轮旅游</a><span>></span>";
                        BodyId = "cruises";
                        break;
                    case "visa":
                        BreadCrumb = "<a href=\"/visa.html\">签证</a><span>></span>";
                        BodyId = "visa";
                        //Response.Redirect("line/" + LineId + ".html");
                        //Response.End();
                        break;
                    default:
                        BreadCrumb = "<a href=\"/outbound.html\">出境旅游</a><span>></span>";
                        break;
                }
                BreadCrumb += string.Format("<a href=\"/search.html?s={2}-0-{0}-0-0-0-1-0\">{1}</a><span>></span>", province, DS.Tables[0].Rows[0]["DestName"].ToString(), DS.Tables[0].Rows[0]["LineClass"].ToString());
                BreadCrumb += "<h1>" + LineName + "</h1>";

                if (linetype == "outbound") ContractInfos = "<a target=\"_blank\" href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</a>";
                if (linetype == "outbound" && lineclass == 851) ContractInfos = "<a target=\"_blank\" href=\"/Upload/大陆居民赴台湾地区旅游合同.doc\">大陆居民赴台湾地区旅游合同范本</a>";

                if (linetype == "inland") ContractInfos = "<a target=\"_blank\" href=\"/Upload/上海市国内旅游合同示范文本.doc\">上海市国内旅游合同范本</a>";

                if (linetype != "inland")
                {
                    VisaFileHide = "<li><a href=\"#qzxx\">签证信息</a></li>";
                    GetVisaFiles(DS.Tables[0].Rows[0]["VisaId"].ToString());
                }

                if (VisaFileInfo == "")
                {
                    hide = "hide";
                    VisaFileHide = "";
                }

                CreatePlanDateJason();

                LineViews = TravelOnline.Destination.Class.PlaceClass.NewLineViewsPic(LineId);
                //Response.Write(LineViews);
                if (LineViews.Length>10)
                {
                    ViewsArray = LineViews.Split('$');
                }
                ReadRouteXML();

                if (RouteBigImg.Length < 5)
                {
                    RouteBigImg = string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" alt=\"\" /></a></li>", "/images/none.gif");
                    RouteSmallImg = string.Format("<li class=\"active\"><img src=\"{0}\" alt=\"\" /></li>", "/images/none.gif");
                }
                if (PlanDateJason.Length < 10)
                {
                    PlanDateJason = string.Format("var defaultStartDate = '{0:yyyy-MM-dd}'; var defaultEndDate = '{0:yyyy-MM-dd}';", DateTime.Today);
                    PlanDateJason += " var json = [{ 'planid': '0', 'date': '2014-12-31', 'price': '0', 'content': '', 'route': '0'}];";
                }

                if (Convert.ToString(ConfigurationManager.AppSettings["seckill"]).IndexOf("," + LineId + ",") > -1)
                {
                    canSale = "0";
                    isSeckill = "1";
                    SqlQueryText = string.Format("select * from OL_SecKillLine where MisLineId='{0}'", LineId);
                    DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        DateTime beginDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["beginDate"].ToString());
                        DateTime endDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["endDate"].ToString());
                        DateTime now = DateTime.Now;
                        if (now.CompareTo(beginDate) >= 0 && now.CompareTo(endDate) <= 0)
                        {
                            canSale = "1";
                        }
                    }
                }
                if (Convert.ToString(ConfigurationManager.AppSettings["ZhongChouHuoDong"]).IndexOf("," + LineId + ",") > -1)
                {
                    SqlQueryText = string.Format("select sum(ordernums) as sumordernums  from (select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order) as T where LineId='{0}' and pay>0", LineId);
                    DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        seckillNum = DS.Tables[0].Rows[0]["sumordernums"].ToString();
                    }
                }
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
                    hide = "";
                    VisaFileHide="<li><a href=\"#qzxx\">签证信息</a></li>";
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

        //创建出发日期的jason
        protected void CreatePlanDateJason()
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            try
            {
                //PlanDateJason, PlanDateString 提取两个数据，一个是开班日期jason，一个是已生成的少于4个计划的html
                //LineId = "17222";
                string[] ListInfo = Regex.Split(rsp.OnlinePlanDateCreate(UpPassWord, LineId), @"\@\@", RegexOptions.IgnoreCase);
                PlanDateJason = ListInfo[0];
            }
            catch
            {
                
            }
        }

        protected void ReadRouteXML()
        {
            DataSet DS = new DataSet();
            DS.Clear();
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, LineId);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = "<li>" + x.SelectSingleNode("Feature").InnerText.Replace("\n", "</li><li>")+ "</li>";
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
                    //Views = "<li><a href=\"javascript:;\"><img src=\"../image/detail/d1.png\" alt=\"\" title=\"\" target=\"_blank\"><span>函馆生鲜市场</span></a></li>";
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
                                pic2 = string.Format("/Images/Views/{0}/{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                pic3 = string.Format("/Images/Views/{0}/S_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                            }
                            catch
                            { }
                            if (System.IO.File.Exists(picpath) == true)
                            {
                                if (PicCount == 0)
                                {
                                    RouteBigImg += string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" alt=\"\"/></a></li>", pic2);
                                    RouteSmallImg += string.Format("<li class=\"active\"><img src=\"{0}\" alt=\"\" /></li>", pic3);
                                }
                                else
                                {
                                    RouteBigImg += string.Format("<li><a href=\"javascript:;\"><img src=\"{0}\" alt=\"\"/></a></li>", pic2);
                                    RouteSmallImg += string.Format("<li><img src=\"{0}\" alt=\"\" /></li>", pic3);
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
                                    <div class='pic-content'>
                                        <ul class='clearfix'>
                                        {8}
                                        </ul>
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
                            Views
                            );
                    }

                }
            }
        }
    }
}