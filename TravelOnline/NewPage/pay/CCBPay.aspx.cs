using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

using System.IO;
using System.Xml;
using TravelOnline.TravelMisWebService;
using TravelOnline.RequestHandlerFacade;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace TravelOnline.NewPage.pay
{
    public partial class CCBPay : System.Web.UI.Page
    {
        public string OrderId,AutoId,LineName, YeE, PayType = "",INS,planid,BankName,LineID,Price,ProductType;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            PayType = Request.QueryString["p"];
            YeE = Request.QueryString["amt"].Replace(".00", "");
            INS = Request.QueryString["INS"];
            BankName = Request.QueryString["BankName"];
            LoadTempOrder();

        }

        protected void LoadTempOrder()
        {
            //点击支付按钮时加判断订单状态by毛寅珅
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "2")
                {
                    Response.Write("订单状态异常，无法支付！");
                    Response.Redirect(string.Format("http://www.scyts.com/OrderView/{0}.html", OrderId), true);
                }
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                LineID = DS.Tables[0].Rows[0]["LineID"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString();
                ProductType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                switch (BankName)
                {
                    case "CCB":
                        GoCCBPay();
                        break;
                    case "CCBINS":
                        GoCCBINSPay();
                        break;
                    case "SHBK":
                        GoSHBKPay();
                        break;
                    case "SHRCB":
                        GoSHRCBPay();
                        break;
                    case "CHINAPAY":
                        GoChinaPay();
                        break;
                }
            }
            else
            {
                Response.Write("支付参数错误，请重试！");
            }
        }

        public static string GetIpaddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result)) result = request.ServerVariables["REMOTE_ADDR"];
            //if (string.IsNullOrEmpty(result)) result = request.ServerVariables.Get("REMOTE_ADDR").ToString();
            if (string.IsNullOrEmpty(result)) result = request.UserHostAddress;
            if (string.IsNullOrEmpty(result)) result = "0.0.0.0";
            return result;
        }

        public void GoCCBPay()
        {
            //支付接口---start
            TravelOnline.RequestHandlerFacade.RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            Rq.amount = YeE;
            Rq.orderId = MyConvert.GetTimeStamp() +AutoId;
            Rq.channel = "CCB";//分期：CCBINS,非分期：CCB
            Rq.gateway = "CCB";
            Rq.productInfo = LineName;
            Rq.surferIp = GetIpaddress();
            //Rq.returnUrl = Convert.ToString(ConfigurationManager.AppSettings["CCBReturnUrl"]);
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            TravelOnline.RequestHandlerFacade.RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            Response.Redirect(Rs.requestUrl);
            //支付接口---end
        }

        public void GoCCBINSPay()
        {
            //支付接口---start
            TravelOnline.RequestHandlerFacade.RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            //if (MyConvert.ConToDec(Price) >= 3000 && MyConvert.ConToDec(YeE) >= 3000)
            //{
            //    decimal lijian;
            //    if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineID + ",") > -1)
            //    {
            //        lijian = MyConvert.ConToDec(ConfigurationManager.AppSettings["specialpfqje"].ToString());
            //    }
            //    else
            //    {
            //        lijian = MyConvert.ConToDec(ConfigurationManager.AppSettings["pfqje"].ToString());
            //    }
            //    YeE = (MyConvert.ConToDec(YeE) - lijian).ToString();
            //}
            YeE = MyConvert.ConToDec(YeE).ToString();
            Rq.amount = YeE;
            Rq.orderId = MyConvert.GetTimeStamp() + AutoId;
            Rq.channel = "CCBINS";//分期：CCBINS非分期：CCB
            Rq.installment = "3";
            if (INS != null)
            {
                Rq.installment = INS;//分期数值
            }
            Rq.gateway = "CCB";
            Rq.productInfo = LineName;
            Rq.surferIp = GetIpaddress();
            //Rq.returnUrl = Convert.ToString(ConfigurationManager.AppSettings["CCBReturnUrl"]);
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            TravelOnline.RequestHandlerFacade.RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            Response.Redirect(Rs.requestUrl);
            //支付接口---end
        }

        public void GoSHBKPay()
        {
            //支付接口---start
            RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            Rq.amount = YeE;
            Rq.orderId = MyConvert.GetTimeStamp() + AutoId;
            Rq.channel = "BOSH";
            Rq.gateway = "BOSHDEBIT";
            Rq.productInfo = LineName;
            Rq.surferIp = GetIpaddress();
            Rq.returnUrl = Convert.ToString(ConfigurationManager.AppSettings["BOSHReturnUrl"]);
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            string[] ruquestUrl = Rs.requestUrl.Split('?');
            string postString = ruquestUrl[1];
            string[] postParams = postString.Split('&');
            string url = ruquestUrl[0];
            StringBuilder html = new StringBuilder();
            html.Append("<script language=\"javascript\">window.onload=function(){document.pay_form.submit();}</script>\n");
            html.Append("<form id=\"pay_form\" name=\"pay_form\" action=\"").Append(url).Append(
                        "\" method=\"post\">\n");

            foreach (string param in postParams)
            {
                int index = param.IndexOf("=");
                string key = param.Substring(0, index);
                string value = param.Substring(index + 1, param.Length - index - 1);
                html.Append("<input type=\"hidden\" name=\"" + key + "\" id=\"" + key + "\" value=\"" + value
                + "\">\n");
            }
            html.Append("</form>\n");
            Response.Write(html);
        }

        public void GoSHRCBPay()
        {
            //支付接口---start
            RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            Rq.amount = YeE;
            Rq.orderId = MyConvert.GetTimeStamp() + AutoId;
            Rq.channel = "SHRCB";
            Rq.gateway = "SHRCB";
            Rq.productInfo = LineName;
            Rq.surferIp = GetIpaddress();
            Rq.returnUrl = Convert.ToString(ConfigurationManager.AppSettings["SHRCBReturnUrl"]);
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            string url = Rs.requestUrl;
            Response.Write(url);
        }

        public void GoChinaPay()
        {
            //支付接口---start
            RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            Rq.amount = YeE;
            Rq.orderId = MyConvert.GetTimeStamp() + AutoId;
            Rq.channel = "ChinaPay";
            Rq.gateway = "ChinaPay";
            Rq.productInfo = LineName;
            Rq.surferIp = getIp();
            //Rq.surferIp = "180.168.167.214";
            Rq.returnUrl = Convert.ToString(ConfigurationManager.AppSettings["CHINAPAYReturnUrl"]);
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            string url = Rs.requestUrl;
            Response.Write(url);
        }


        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受    
            return true;
        }
        private static string getIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}