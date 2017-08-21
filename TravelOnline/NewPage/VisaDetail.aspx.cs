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
    public partial class VisaDetail : System.Web.UI.Page
    {
        public string BodyId = "outbound", tag = "tg", FirstImg = "/Images/none.gif";
        public string LineId, BreadCrumb, linetype, LineName, LineFeature, Price, begindate, LineDays, firstDestination;
        public string Visa_1, Visa_2, Visa_3, RouteFeature, hide = "hide";
        public int lineclass;
        public string Visa1, Visa2, Visa3, Visa4, Visa5, Visa6, Visa1Hide = "hide", Visa2Hide = "hide", Visa3Hide = "hide", Visa4Hide = "hide", Visa5Hide = "hide", Visa6Hide = "hide";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LineId = Request.QueryString["id"];
            if (MyConvert.ConToInt(LineId) == 0) Response.Redirect("~/index.html", true);
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
                lineclass = MyConvert.ConToInt(DS.Tables[0].Rows[0]["LineClass"].ToString());
                firstDestination = DS.Tables[0].Rows[0]["FirstDestination"].ToString();
                linetype = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LineName = LineName.Replace(",", "");
                LineName = LineName.Replace("'", "");
                LineName = LineName.Replace("@", "");
                LineName = LineName.Replace("<", "");
                LineName = LineName.Replace(">", "");
                LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "");

                begindate = DS.Tables[0].Rows[0]["Pdates"].ToString();
                if (begindate.Length > 3)
                {
                    Visa_1 = begindate.Split("$".ToCharArray())[1];
                    Visa_2 = begindate.Split("$".ToCharArray())[0];
                    Visa_3 = begindate.Split("$".ToCharArray())[2];
                }

                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length > 5) FirstImg = "/images/shadow/" + DS.Tables[0].Rows[0]["Pics"].ToString();

                BreadCrumb = "<a href=\"/visa.html\">签证</a><span>></span>";
                BreadCrumb += "<h1>" + LineName + "</h1>";
                BodyId = "visa";

                ReadVisaXML();
                
            }
        }

        protected void ReadVisaXML()
        {
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, LineId);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    RouteFeature = x.SelectSingleNode("Memo").InnerText.Replace("\n", "<br>");
                    if (RouteFeature.Length > 2) hide = "";

                    StringBuilder PicString = new StringBuilder();
                    XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        //<dt>有效因私护照</dt>
                        //            <dd class="d1">（原件1份）</dd>
                        //            <dd class="d2">有效期需超出在美停留期至少6个月以上；</dd>

                        Strings.Clear();
                        Strings.Append(string.Format("<dt>{0}</dt>", elemList[i].SelectSingleNode("VisaName").InnerText));
                        if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0 || elemList[i].SelectSingleNode("V2").InnerText.Length != 0)
                        {
                            Strings.Append("<dd class=\"d1\">");
                            if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0) Strings.Append(string.Format("原件:{0} ", elemList[i].SelectSingleNode("V1").InnerText));
                            if (elemList[i].SelectSingleNode("V2").InnerText.Length != 0) Strings.Append(string.Format("复印件:{0}", elemList[i].SelectSingleNode("V2").InnerText));
                            Strings.Append("</dd>");
                        }
                        else
                        {
                            Strings.Append("<dd class=\"d1\">&nbsp;</dd>");
                        }
                        Strings.Append(string.Format("<dd class=\"d2\">{0}&nbsp;</dd>", elemList[i].SelectSingleNode("VisaContent").InnerText.Replace("\n", "<br>")));
                        
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