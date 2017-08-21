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

namespace TravelOnline.Travel
{
    public partial class VisaDetail : System.Web.UI.Page
    {
        public string Category, id, ProducType, LineOnHotSale, Map1;
        public string LineName, LineFeature, LinePrice, ImgList, FirstImg, pdates;
        public string RegularContractInfos, RegularPayInfos, RegularOrderProcess;
        public string RouteInfos, RouteFeature, RouteServiceInfos, RoutePriceInfos, RouteAttentionsInfos;
        public string VisaLi, VisaCt, VisaDiv;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Status = "301 Moved Permanently";
            id = Request.QueryString["Id"];
            Response.AddHeader("Location", "/line/" + id + ".html");
            Response.End();

            //Category = Request.QueryString["Category"];
            //ProducType = Request.QueryString["ProducType"];
            //if (MyConvert.ConToInt(id) == 0) Response.Redirect("~/index.html", true);
            //Response.Redirect("/line/" + id + ".html", true);

            LoadLineInfo();
            ReadRouteXML();
            LineClass.LinePageViewCount(id);
            LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            //RegularContractInfos = LineDetailRegular.ContractInfos(ProducType);
            RegularPayInfos = LineDetailRegular.PayInfos();
            RegularOrderProcess = LineDetailRegular.OrderProcess();
        }

        protected void LoadLineInfo()
        {
            string SqlQueryText = string.Format("select top 1 *,(select ProductName from OL_ProductType where misclassid=OL_Line.LineClass) as ProductName from OL_Line where Sale='0' and MisLineId='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["LineType"].ToString() != ProducType) Response.Redirect("~/index.html", true);
                Map1 = string.Format("<a href = \"/{0}/{1}-0.html\">{2}</a>", ProducType, DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["ProductName"].ToString());
                
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LinePrice = DS.Tables[0].Rows[0]["Price"].ToString();
                LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                pdates = DS.Tables[0].Rows[0]["Pdates"].ToString();
                if (pdates.Length > 3) pdates = string.Format("有效期：{0}&nbsp;&nbsp;&nbsp;&nbsp;停留时间：{1}&nbsp;&nbsp;&nbsp;&nbsp;工作日：{2}<br>", pdates.Split("$".ToCharArray())[0], pdates.Split("$".ToCharArray())[1], pdates.Split("$".ToCharArray())[2]);
                //LineFeature = pdates;
                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length > 5)
                {
                    FirstImg = string.Format("<IMG onerror=\"this.src='/Images/none.gif'\" src=\"/images/shadow/{0}\" width=320 height=240 jqimg=\"/images/shadow/{0}\">", DS.Tables[0].Rows[0]["Pics"].ToString());
                    ImgList = string.Format("<LI><IMG onerror=\"this.src='/Images/none.gif'\" src=\"/images/shadow/{0}\" width=62 height=50></LI>", DS.Tables[0].Rows[0]["Pics"].ToString());
                }
                else
                {
                    FirstImg = "<IMG src=\"/Images/none.gif\" width=320 height=240 jqimg=\"/Images/none.gif\">";
                    ImgList = "<LI><IMG src=\"/Images/none.gif\" width=62 height=50></LI>";
                }
                //Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());                        
                
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected void ReadRouteXML()
        {
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, id);
            //If System.IO.File.Exists(Server.MapPath(filesurl)) = True Then
            if (System.IO.File.Exists(path) == true)
            {
                string Li1 = "", Li2 = "", Li3 = "", Li4 = "", Li5 = "", Li6 = "";
                string Ct1 = "", Ct2 = "", Ct3 = "", Ct4 = "", Ct5 = "", Ct6 = "";
                string Dv1 = "", Dv2 = "", Dv3 = "", Dv4 = "", Dv5 = "", Dv6 = "";
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
                        Strings.Append(string.Format("<DL><DT>{0}：</DT><DD>", elemList[i].SelectSingleNode("VisaName").InnerText));
                        Strings.Append(string.Format("<div>{0}</div>", elemList[i].SelectSingleNode("VisaContent").InnerText.Replace("\n", "<br>")));
                        if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0)
                        {
                            Strings.Append(string.Format("<div class=files>原件：{0} &nbsp;&nbsp;&nbsp;&nbsp;复印件：{1}</div>", elemList[i].SelectSingleNode("V1").InnerText,elemList[i].SelectSingleNode("V2").InnerText));
                        }
                        Strings.Append("</DD></DL>");
                        Strings.Append("");
                        switch (elemList[i].SelectSingleNode("Flag").InnerText)
                        {
                            case "1":
                                Ct1 += Strings.ToString();
                                break;
                            case "2":
                                Ct2 += Strings.ToString();
                                break;
                            case "3":
                                Ct3 += Strings.ToString();
                                break;
                            case "4":
                                Ct4 += Strings.ToString();
                                break;
                            case "5":
                                Ct5 += Strings.ToString();
                                break;
                            case "6":
                                Ct6 += Strings.ToString();
                                break;
                            default:
                                break;
                        }
                    }

                    if (Ct1.Length>10 )
                    {
                        Li1 = "<LI data=\"d-visa1\">身份证明<span></span></LI>";
                        Ct1 = string.Format("<div id=\"d-visa1-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>身份证明</STRONG><div class=extra></div></div>{0}</div></div>", Ct1);
                        Dv1 = "<div id=\"d-visa1\" class=\"mc tabcon hide\"></div>";
                    }
                    if (Ct2.Length > 10)
                    {
                        Li2 = "<LI data=\"d-visa2\">资产证明<span></span></LI>";
                        Ct2 = string.Format("<div id=\"d-visa2-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>资产证明</STRONG><div class=extra></div></div>{0}</div></div>", Ct2);
                        Dv2 = "<div id=\"d-visa2\" class=\"mc tabcon hide\"></div>";
                    }
                    if (Ct3.Length > 10)
                    {
                        Li3 = "<LI data=\"d-visa3\">工作证明<span></span></LI>";
                        Ct3 = string.Format("<div id=\"d-visa3-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>工作证明</STRONG><div class=extra></div></div>{0}</div></div>", Ct3);
                        Dv3 = "<div id=\"d-visa3\" class=\"mc tabcon hide\"></div>";
                    }
                    if (Ct4.Length > 10)
                    {
                        Li4 = "<LI data=\"d-visa4\">学生及儿童<span></span></LI>";
                        Ct4 = string.Format("<div id=\"d-visa4-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>学生及儿童所需材料</STRONG><div class=extra></div></div>{0}</div></div>", Ct4);
                        Dv4 = "<div id=\"d-visa4\" class=\"mc tabcon hide\"></div>";
                    }
                    if (Ct5.Length > 10)
                    {
                        Li5 = "<LI data=\"d-visa5\">老人<span></span></LI>";
                        Ct5 = string.Format("<div id=\"d-visa5-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>老人所需材料</STRONG><div class=extra></div></div>{0}</div></div>", Ct5);
                        Dv5 = "<div id=\"d-visa5\" class=\"mc tabcon hide\"></div>";
                    }
                    if (Ct6.Length > 10)
                    {
                        Li6 = "<LI data=\"d-visa6\">其他<span></span></LI>";
                        Ct6 = string.Format("<div id=\"d-visa6-ct\"><div class=\"m Visa\"><div class=mt><H1></H1><STRONG>其他所需材料</STRONG><div class=extra></div></div>{0}</div></div>", Ct6);
                        Dv6 = "<div id=\"d-visa6\" class=\"mc tabcon hide\"></div>";
                    }
                    
                    VisaLi = string.Format("{0}{1}{2}{3}{4}{5}", Li1, Li2, Li3, Li4, Li5, Li6);
                    VisaCt = string.Format("{0}{1}{2}{3}{4}{5}", Ct1, Ct2, Ct3, Ct4, Ct5, Ct6);
                    VisaDiv = string.Format("{0}{1}{2}{3}{4}{5}", Dv1, Dv2, Dv3, Dv4, Dv5, Dv6);
                }
            }
        }
    }
}