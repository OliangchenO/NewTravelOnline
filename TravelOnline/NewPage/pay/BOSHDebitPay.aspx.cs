using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.RequestHandlerFacade;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;  

namespace TravelOnline.NewPage.pay
{
    public partial class BOSHDebitPay : System.Web.UI.Page
    {
        public string OrderId,AutoId,LineName, YeE, PayType = "",INS,planid;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            YeE = Request.QueryString["amt"].Replace(".00", "");
            
            LoadTempOrder();

        }

        protected void LoadTempOrder()
        {
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

                //支付接口---start
                RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
                Rq.amount = YeE;
                Rq.orderId = MyConvert.GetTimeStamp()+AutoId;
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
                
                foreach(string param in postParams){
                    int index = param.IndexOf("=");
                    string key = param.Substring(0,index);
                    string value = param.Substring(index+1, param.Length-index-1);
                    html.Append("<input type=\"hidden\" name=\"" + key + "\" id=\"" + key + "\" value=\"" + value
                    + "\">\n");
                }
                html.Append("</form>\n");
                Response.Write(html);
                
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

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受    
            return true;
        }  
    
    }
}