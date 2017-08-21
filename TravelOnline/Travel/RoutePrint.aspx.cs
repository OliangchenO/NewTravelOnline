using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Xml;

namespace TravelOnline.Travel
{
    public partial class RoutePrint : System.Web.UI.Page
    {
        public string Action, Cid;
        public string path;
        public string LineName, RouteList;
        public string RouteFeature, Traffic, Hotel, Scenery, Foods, Guide, Insure, Others, PriceIn, PriceOut, OwnExpense, Attentions, Shopping;
                        
        protected void Page_Load(object sender, EventArgs e)
        {
            Action = Request.QueryString["Action"];
            Cid = Request.QueryString["Cid"];
            LineRoute();
        }

        protected void LineRoute()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select MisLineId,LineName from OL_Line where MisLineId='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                string OrderXml = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, DS.Tables[0].Rows[0]["MisLineId"].ToString());
                if (System.IO.File.Exists(OrderXml) != true)
                {
                    Response.Write("没有找到行程数据，不能查看！");
                    Response.End();
                }
                else
                {
                    StringBuilder Strings = new StringBuilder();
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.Load(OrderXml);
                    XmlNode x = XmlDoc.SelectSingleNode("//Route");
                    if (x != null)
                    {
                        RouteFeature = x.SelectSingleNode("Feature").InnerText.Replace("\n", "<br>");
                        if (x.SelectSingleNode("Traffic").InnerText.Length > 1) Traffic = "<b>旅游交通：</b>" + x.SelectSingleNode("Traffic").InnerText + "<br>";
                        if (x.SelectSingleNode("Hotel").InnerText.Length > 1) Hotel = "<b>住宿标准：</b>" + x.SelectSingleNode("Hotel").InnerText + "<br>";
                        if (x.SelectSingleNode("Scenery").InnerText.Length > 1) Scenery = "<b>景点门票：</b>" + x.SelectSingleNode("Scenery").InnerText + "<br>";
                        if (x.SelectSingleNode("Foods").InnerText.Length > 1) Foods = "<b>用餐标准：</b>" + x.SelectSingleNode("Foods").InnerText + "<br>";
                        if (x.SelectSingleNode("Guide").InnerText.Length > 1) Guide = "<b>导游服务：</b>" + x.SelectSingleNode("Guide").InnerText + "<br>";
                        if (x.SelectSingleNode("Insure").InnerText.Length > 1) Insure = "<b>保险服务：</b>" + x.SelectSingleNode("Insure").InnerText + "<br>";
                        if (x.SelectSingleNode("Others").InnerText.Length > 1) Others = "<b>其他服务：</b>" + x.SelectSingleNode("Others").InnerText + "<br>";

                        PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "<br>");
                        PriceOut = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "<br>");
                        OwnExpense = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "<br>");
                        Attentions = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "<br>");
                        Shopping = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "<br>");

                        StringBuilder RS = new StringBuilder();
                        XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");

                        RS.Append("<table class=\"rtab\" width=\"99%\" cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" align=\"center\">");
                        RS.Append("<tr><td class=\"rctdtitle\" width=\"8%\">日序</td><td class=\"rctdtitle\" width=\"68%\">行程内容</td><td class=\"rctdtitle\" width=\"10%\">用餐</td><td class=\"rctdtitle\" width=\"15%\">住宿</td></tr>");
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            RS.Append("<tr>");
                            RS.Append("<td class=\"rctd\">");
                            RS.Append(elemList[i].SelectSingleNode("daterank").InnerText);
                            RS.Append("</td>");

                            RS.Append("<td class=\"rctd1\"><b>");
                            RS.Append(elemList[i].SelectSingleNode("rname").InnerText);
                            RS.Append("&nbsp;");
                            RS.Append(elemList[i].SelectSingleNode("bus").InnerText);
                            RS.Append("</b><br>");
                            RS.Append(elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>"));
                            RS.Append("</td>");

                            RS.Append("<td class=\"rctd\">");
                            RS.Append(elemList[i].SelectSingleNode("dinner").InnerText);
                            RS.Append("&nbsp;</td>");
                            RS.Append("<td class=\"rctd\">");
                            RS.Append(elemList[i].SelectSingleNode("room").InnerText);
                            RS.Append("&nbsp;</td>");
                            RS.Append("</tr>");
                        }
                        RS.Append("</table>");
                        RouteList = RS.ToString();
                    }
                }
            }
        }
    }
}