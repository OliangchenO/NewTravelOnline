﻿using System;
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

namespace TravelOnline.Travel
{
    public partial class CruisesDetail : System.Web.UI.Page
    {
        public string BodyId, Category, id, ProducType, LineOnHotSale, Map1, Map2, PlanScripts, titleinfo;
        public string LineName, LinePrice, ImgList, FirstImg, Pics, MsPrice;
        public string RegularContractInfos, RegularPayInfos, RegularOrderProcess;
        public string RouteInfos, RouteFeature, RouteServiceInfos, RoutePriceInfos, RouteAttentionsInfos;
        public string PlanString, RoomString;
        public string Journal;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Status = "301 Moved Permanently";
            id = Request.QueryString["Id"];
            Response.AddHeader("Location", "/line/" + id + ".html");
            Response.End();

            //Response.Buffer = true;
            //Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            //Response.Expires = 0;
            //Response.CacheControl = "no-cache";
            //Response.AppendHeader("Pragma", "No-Cache");

            //Category = Request.QueryString["Category"];
            //id = Request.QueryString["Id"];
            //ProducType = Request.QueryString["ProducType"];
            //if (MyConvert.ConToInt(id) == 0) Response.Redirect("~/index.html", true);
            //Response.Redirect("/line/" + id + ".html", true);

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
            PurchaseClass.CruisesPlanInfo PlanInfo = new PurchaseClass.CruisesPlanInfo();
            PlanInfo = PurchaseClass.GetCrusesPlanList(id);
            PlanString = PlanInfo.CruisesPlanInfo_PlanList;
            RoomString = PlanInfo.CruisesPlanInfo_RoomList;
        }

        protected void LoadLineInfo()
        {
            //string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            //TravelOnlineService rsp = new TravelOnlineService();
            //rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            //try
            //{
            //    PlanScripts = rsp.OnlinePlanDateCreate(UpPassWord, id);
            //    if (PlanScripts == "Error")
            //    {
            //        PlanScripts = "<script type=\"text/javascript\" src=\"/Js/PlanDate/" + id + ".js\"></script>";
            //    }
            //    else
            //    {
            //        PlanScripts = "<SCRIPT type=\"text/javascript\">" + PlanScripts + "</SCRIPT>";
            //    }
            //}
            //catch
            //{
            //    PlanScripts = "<script type=\"text/javascript\" src=\"/Js/PlanDate/" + id + ".js\"></script>";
            //}

            string SqlQueryText = string.Format("select top 1 *,(select ProductName from OL_ProductType where misclassid=OL_Line.LineClass) as ProductName from OL_Line where MisLineId='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Shipid"].ToString()) > 0)
                {
                    //Response.Write("asdasdasdsa");
                    Response.Redirect(string.Format("~/Ship/{0}/{1}.html", Category, id), true);
                    Response.End();
                }
                else {
                    //Response.Write("2321321321");
                }
                Journal = LinePreferences.LineJournal(DS.Tables[0].Rows[0]["FirstDestination"].ToString());

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

                ///
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
            }
            else
            {

            }
        }

        protected void ReadRouteXML()
        {
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
    }
}