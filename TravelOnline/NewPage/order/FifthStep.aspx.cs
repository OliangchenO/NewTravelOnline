using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Configuration;
using TravelOnline.Class.Common;

namespace TravelOnline.NewPage.order
{
    public partial class FifthStep : System.Web.UI.Page
    {
        public string OrderId, username, AutoId, pay;
        public int Integral;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            username = Convert.ToString(Session["Online_UserName"]);
            if (!IsPostBack)
            {
                LoadOrder();
            }
        }

        protected void LoadOrder()
        {
            string SqlQueryText = string.Format("select LineID,OrderId,AutoId,ordermobile,(select top 1 PayPrice from OL_PayMent where OrderId=OL_Order.OrderId order by PayTime desc) as pay from OL_Order where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                pay = DS.Tables[0].Rows[0]["pay"].ToString();
                Integral = (int)((MyConvert.ConToDec(pay) * MyConvert.ConToDec("0.01")) - MyConvert.ConToDec("0.5"));
                string LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
                string orderMobile = DS.Tables[0].Rows[0]["ordermobile"].ToString();
                if (Convert.ToString(ConfigurationManager.AppSettings["prebook"]).IndexOf("," + LineId + ",") > -1)
                {
                    if (null != orderMobile && orderMobile != "")
                    {
                        string SendCount = MyDataBaseComm.getScalar("select count(1) from OL_SmsSend flag='PrebookPay' and mobile='" + orderMobile + "' and extinfo = '" + AutoId + "'");
                        if (SendCount == null && pay != "")
                        {
                            //发送短信
                            string smsMessage = "您已成功支付定金：" + pay + "元，请在活动当天付清余款！【上海青旅】";
                            string smsMessagesendresult = SendSms.SendSMS_ztsms(orderMobile, smsMessage);
                            string ip = GetClientIP();
                            if (smsMessagesendresult == "ok")
                            {
                                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime,ip) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                    System.Guid.NewGuid(),
                                    orderMobile,
                                    smsMessage,
                                    "0",
                                    "PrebookPay",
                                    AutoId,
                                    DateTime.Now.ToString(),
                                    ip
                                );
                            }
                        }
                    }
                }
            }
        }

        public static string GetClientIP()
        {
            //获得IP地址 
            string hostname;
            System.Net.IPHostEntry localhost;
            hostname = System.Net.Dns.GetHostName();
            localhost = System.Net.Dns.GetHostEntry(hostname);
            string ip = localhost.AddressList[0].ToString();
            int i = 1;
            while (ip.Contains(":"))
            {
                if (i == localhost.AddressList.Length)
                {
                    ip = "";
                    break;
                }
                ip = localhost.AddressList[i].ToString();
                if (ip.Contains(":"))
                {
                    i++;
                }
                else
                    break;
            }
            return ip;

        }

    }
}