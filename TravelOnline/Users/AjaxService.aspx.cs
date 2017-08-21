using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using System.Text;
using TravelOnline.Class.Purchase;
using TravelOnline.Class.Common;
using TravelOnline.GetCombineKeys;
using TravelOnline.LoginUsers;

namespace TravelOnline.Users
{
    public partial class AjaxService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"].ToString().Equals("SendNotMemSMS"))
            {
                SendNotMemSMS(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["authcode"].Trim()), MyDataBaseComm.StripSQLInjection(Request.QueryString["flag"].Trim()));
            }
            else if (Request.QueryString["action"].ToString().Equals("validPhoneyzm"))
            {
                validPhoneyzm();
            }
            if (Convert.ToString(Session["Online_UserId"]).Length != 0 || Convert.ToString(Session["Manager_UserId"]).Length != 0)
            {}
            else {
                Response.Write("{\"success\":1}");
                Response.End();
            }

            switch (Request.QueryString["action"])
            {
                case "OrderLogInfo":
                    OrderLogInfo();
                    break;
                case "BranchMapInfo":
                    BranchMapInfo();
                    break;
                case "CancelOrder":
                    CancelOrder();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        public void CancelOrder()
        {
            StringBuilder Strings = new StringBuilder();

            string SqlQueryText = string.Format("select * from OL_Order where OrderId= '{0}'", Request.QueryString["OrderId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1")
                {
                    Strings.Append("只有占位订单才可以取消！");
                    Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
                    Response.End();
                }

                if (DS.Tables[0].Rows[0]["OrderUser"].ToString() != Convert.ToString(Session["Online_UserId"]))
                {
                    Strings.Append("只有您预订的订单才可以取消！");
                    Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
                    Response.End();
                }
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    string Result = rsp.OnlineOrderAdjust(UpPassWord, DS.Tables[0].Rows[0]["OrderId"].ToString());
                    if (Result == "OK")
                    {
                        List<string> Sql = new List<string>();
                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS.Tables[0].Rows[0]["OrderId"].ToString(), DateTime.Now.ToString(), "您取消了订单！"));
                        Sql.Add(string.Format("update OL_Order set OrderFlag='8' where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        //取消订单时，把对应使用优惠券和积分一起取消
                        Sql.Add(string.Format("update Pre_Ticket set flag=0 where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        Sql.Add(string.Format("delete OL_Integral where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        string[] SqlQuery = Sql.ToArray();
                        MyDataBaseComm.Transaction(SqlQuery);
                        Strings.Append("OK");
                    }
                }
                catch
                {
                    Strings.Append("订单取消失败，请稍后再试！");
                }
            }
            else
            {
                Strings.Append("订单取消失败，您的订单不存在！");
            }
            Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
        }

        public void BranchMapInfo()
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append( PurchaseClass.GetBranch(MyConvert.ConToInt(Request.QueryString["BranchId"]), "BranchMap"));            
            Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
        }

        public void OrderLogInfo()
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_OrderLog where OrderId='{0}' order by LogTime", Request.QueryString["OrderId"]);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            Strings.Append("<ul id=logul>");
            Strings.Append("<li>订单状态记录</li>");
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li class=logtit>{0}</li><li>{1}</li>", DS.Tables[0].Rows[i]["LogTime"].ToString(), DS.Tables[0].Rows[i]["LogContent"].ToString()));
                }

            }
            else
            {
                Strings.Append("<li>抱歉，暂无任何状态记录！</li>");
            }
            Strings.Append("</ul>");
            Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
        }

        protected void SendNotMemSMS(string checkmobile, string authcode, string flag)
        {
            string Verify = MyConvert.CreateNumberVerifyCode(6);
            string smscontent = "";
            string smstype = "";
            if ("ordersearch".Equals(flag))
            {
                smscontent = "【上海青旅】尊敬的上海青旅用户，您正在上海青旅官网查询订单，短信验证码为：" + Verify + "，谢谢您的支持，祝您旅途愉快！【上海青旅】";
                smstype = "NotMemberOrderSearch";
            }
            else if("ordersubmit".Equals(flag))
            {
                smscontent = "【上海青旅】尊敬的上海青旅用户，您正在上海青旅官网预订旅游产品，短信验证码为：" + Verify + "，谢谢您的支持，祝您旅途愉快！【上海青旅】";
                smstype = "NotMemberOrderSubmit";
            }
            
            string sendresult = SendSms.SendSMS_ztsms(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), smscontent);
            if (sendresult == "ok")
            {
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    System.Guid.NewGuid(),
                    MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()),
                    smscontent,
                    "0",
                    smstype,
                    Verify,
                    DateTime.Now.ToString()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"发送成功，如未收到，请60秒后重发\"})");
                    Response.End();
                }
                else
                {
                    Response.Write("({\"error\":\"<i></i>动态密码发送失败，请稍后再试\"})");
                    Response.End();
                }
            }
            else
            {
                Response.Write("({\"error\":\"<i></i>发送失败，请稍后再试\"})");
                Response.End();
            }
        }

        protected void validPhoneyzm()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='NotMemberOrderSearch' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["Phoneyzm"].Trim()));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    Response.Write("({\"error\":\"<i></i>短信验证码已过有效期，请重新发送\"})");
                    Response.End();
                }
                Response.Write("({\"success\":1})");
                Response.End();
            }
            else
            {
                Response.Write("({\"error\":\"<i></i>手机号或短信验证码错误\"})");
                Response.End();
            }
        }
    }
}