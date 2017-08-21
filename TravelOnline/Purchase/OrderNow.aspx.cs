using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using TravelOnline.GetCombineKeys;
using System.Data;

using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Xml;



namespace TravelOnline.Purchase
{
    public partial class OrderNow : System.Web.UI.Page
    {
        public string lineid, planid, begindate, nums, etnums, Seats, OrderInfo, routeflag,planno;
        public string step1, step2, step3, Btn1, SfId;
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            step2 = "style=\"DISPLAY:none\"";
            step3 = "style=\"DISPLAY:none\"";
            lineid = Request.QueryString["lineid"];
            planid = Request.QueryString["planid"];
            begindate = Request.QueryString["begindate"];
            ucode = CombineKeys.NewComb(); //System.Guid.NewGuid();

            string SqlQueryText = string.Format("select top 1 Sale,SfId from OL_Line where MisLineId='{0}'", lineid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                SfId = DS.Tables[0].Rows[0]["SfId"].ToString();

                if (Request.Cookies["OnlyOnlinePay"] != null || Request.Cookies["OnlyIcbcPay"] != null)
                {
                    
                }
                else
                {
                    if (DS.Tables[0].Rows[0]["Sale"].ToString() == "1")
                    {
                        //step1 = "style=\"DISPLAY:none\"";
                        //step3 = "";
                        //Btn1 = "style=\"DISPLAY:none\"";
                        //return;
                    }
                }
                
            }
            else
            {
                step1 = "style=\"DISPLAY:none\"";
                step3 = "";
                Btn1 = "style=\"DISPLAY:none\"";
                return;
            }

            PlanSeats GetPlan = new PlanSeats();

            if (planid == "0")
            {
                GetPlan.Seats = "99";
                GetPlan.Route = "99";
                GetPlan.StopDate = "";
            }
            else
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, begindate);
                }
                catch
                {
                    GetPlan.Seats = "0";
                    GetPlan.Route = "99";
                    GetPlan.StopDate = "";
                }
            }

            if (MyConvert.ConToInt(SfId) > 0)
            {

                //try
                //{
                //    Uri ourUri = new Uri("http://211.152.32.8/b2b/travelscheduledetails/getxml/travelline/" + SfId);
                //    WebRequest myWebRequest = WebRequest.Create(ourUri);

                //    WebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

                //    //Dim stream As New StreamReader(myWebResponse.GetResponseStream(), Encoding.[Default])
                //    StreamReader stream = new StreamReader(myWebResponse.GetResponseStream(), Encoding.Default);
                //    string resultStr = stream.ReadToEnd();

                //    XmlDocument cnpXmlDoc = new XmlDocument();
                //    cnpXmlDoc.LoadXml(resultStr);

                //    XmlNode x = cnpXmlDoc.SelectSingleNode("//Plan");

                //    if ((x != null))
                //    {
                //        XmlNode x1 = cnpXmlDoc.SelectSingleNode("//PlanList");
                //        XmlNodeList elemList = x1.SelectNodes("//PlanDetail");

                //        int i = 0;
                //        for (i = 0; i < elemList.Count; i++)
                //        {
                //            if (planid == elemList[i].SelectSingleNode("Planid").InnerText)
                //            {
                //                GetPlan.Seats = elemList[i].SelectSingleNode("Seats").InnerText;
                //                GetPlan.Route = "99";
                //                GetPlan.BeginDate = elemList[i].SelectSingleNode("BeginDate").InnerText;
                //                GetPlan.StopDate = elemList[i].SelectSingleNode("BeginDate").InnerText;
                //                GetPlan.onlineseat = elemList[i].SelectSingleNode("Seats").InnerText;
                //                GetPlan.PlanNo = "";
                //            }
                //        }
                //    }
                //}
                //catch
                //{
                //    GetPlan.Seats = "0";
                //    GetPlan.Route = "99";
                //    GetPlan.StopDate = "";
                //}

            }
            else
            {
                //if (planid == "0")
                //{
                //    GetPlan.Seats = "99";
                //    GetPlan.Route = "99";
                //    GetPlan.StopDate = "";
                //}
                //else
                //{
                //    string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                //    TravelOnlineService rsp = new TravelOnlineService();
                //    rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                //    try
                //    {
                //        GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, begindate);
                //    }
                //    catch
                //    {
                //        GetPlan.Seats = "0";
                //        GetPlan.Route = "99";
                //        GetPlan.StopDate = "";
                //    }
                //}
            }            

            Seats = GetPlan.Seats;
            String RouteInfo = "";
            DateTime today = DateTime.Today;
            planno = GetPlan.PlanNo;

            //routeflag = GetPlan.Route;
            if (GetPlan.Route == "0")
            {
                RouteInfo = "<br>&nbsp;  <A href=\"ShowRoute.aspx?id=" + planid + "\" target=_blank>所选出发日期行程有变更，点击查看</A>";
                routeflag = "0";
            }
            else
            {
                routeflag = "1";
            }

                
            if (GetPlan.StopDate.Length > 0)
            {
                DateTime rp_times = DateTime.Now;
                //Response.Write(rp_times.Hour + "/" + Convert.ToDateTime(GetPlan.StopDate).ToString() + "/" + today.ToString());
                if (Convert.ToDateTime(GetPlan.StopDate) < today)
                {
                    step1 = "style=\"DISPLAY:none\"";
                    step3 = "";
                    Btn1 = "style=\"DISPLAY:none\"";
                    return;
                }
                if (Convert.ToDateTime(GetPlan.StopDate) == today)
                {
                    
                    if (rp_times.Hour >= 17)
                    {
                        step1 = "style=\"DISPLAY:none\"";
                        step3 = "";
                        Btn1 = "style=\"DISPLAY:none\"";
                        return;
                    }
                }
            }

            if (MyConvert.ConToInt(Seats) > 5)
            {
                OrderInfo = "可报名人数：>5";
            }
            else
            {
                if (MyConvert.ConToInt(Seats) == 0)
                {
                    step1 = "style=\"DISPLAY:none\"";
                    step2 = "";
                    Btn1 = "style=\"DISPLAY:none\"";
                }
                OrderInfo = string.Format("可报名人数：{0}", Seats);
            }

            nums = Request.QueryString["nums"];
            etnums = Request.QueryString["etnums"];
            if (planid == "0")
            {
                OrderInfo = "可报名人数：9， 您的预订单需后台确认";
            }
            if (GetPlan.onlineseat == "1") OrderInfo += "， 您的预订单需后台确认";
            OrderInfo = string.Format("&nbsp;  出发日期：{0} &nbsp; {1} {2}", begindate, OrderInfo, RouteInfo);
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Seats = "0";
        }
    }
}