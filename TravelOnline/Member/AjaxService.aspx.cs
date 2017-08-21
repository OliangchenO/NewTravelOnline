using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.LoginUsers;
using System.Text;
using TravelOnline.EncryptCode;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using TravelOnline.Class.Common;
using TravelOnline.GetCombineKeys;
using System.IO;

namespace TravelOnline.Member
{
    public partial class AjaxService : System.Web.UI.Page
    {
        public string username, MemberName, LoginMobile, UserType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            switch (Request.QueryString["action"])
            {
                case "EmailLogin":
                    CheckEmail();
                    break;
                case "MobileLogin":
                    CheckMobile();
                    break;
                case "SendSMS":
                    SendSMS("");
                    break;
                case "SendRegSMS":
                    SendSMS(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()));
                    break;
                case "ResetPassword":
                    if (Convert.ToString(Session["Online_UserId"]).Length == 0)
                    {
                        Response.Write("({\"error\":\"<i></i>尚未登录\"})");
                        Response.End();
                    }
                    EditPass_Change();
                    break;
                case "Register":
                    Register();
                    break;
                case "SHBankRegister":
                    SHBankReg();
                    break;
                case "RegisterYoung":
                    RegisterYoung();
                    break;
                default:
                    Response.Write("({\"error\":\"传递的参数不正确\"})");
                    Response.End();
                    break;
            }
        }

        protected void Register()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='MobileLogin' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["Phoneyzm"].Trim()));

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
            }
            else
            {
                Response.Write("({\"error\":\"<i></i>手机号或短信验证码错误\"})");
                Response.End();
            }

            CheckRegEmail();
            CheckRegMobile();

            String AutoId = Convert.ToString(CombineKeys.NewComb());
            LoginUser.RegistUser RUser = new LoginUser.RegistUser
            {
                Id = AutoId,
                UserEmail = MyDataBaseComm.StripSQLInjection(Request.Form["email"].Trim()),
                UserName = "新用户",
                Mobile = MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()),
                LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["password"].Trim(), 32)
            };

            if (LoginUser.LoginUser_Sql(RUser, "Regist") != true)
            {
                Response.Write("({\"error\":\"<i></i>注册失败\"})");
                Response.End();
            }
            else
            {
                Session["Online_UserId"] = AutoId;
                Session["Online_UserEmail"] = Request.Form["email"].Trim();
                Session["Online_UserName"] = "新用户";
                Session["Online_UserDept"] = "";
                Session["Online_UserCompany"] = "";
                Session["Online_RebateFlag"] = "";
                Session["Online_YJDept"] = "";
                username = Request.Form["email"].Trim();
                MemberName = "新用户";

                Response.Write("({\"success\":\"注册成功\"})");
                Response.End();
            }

        }

        protected void CheckRegEmail()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", MyDataBaseComm.StripSQLInjection(Request.Form["email"].Trim()));
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"email\":\"<i></i>邮件地址已被注册\"})");
                Response.End();
            }
        }

        protected void CheckRegMobile()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where Mobile='{0}'", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()));
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"mobilePhone\":\"<i></i>手机号码已经存在\"})");
                Response.End();
            }
        }

        protected void CheckRegCardID()
        {
            string SqlQueryText = string.Format("select top 1 CardID from OL_LoginUser where CardID='{0}'", MyDataBaseComm.StripSQLInjection(Request.Form["CardID"].Trim()));
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"error\":\"<i></i>卡号以注册过！\"})");
                Response.End();
            }

        }

        //检验用户身份
        protected void CheckEmail()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("(UserEmail='{0}' or Mobile='{0}' or cardId = '{0}') and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(Request.Form["UserName"].Trim()), SecurityCode.Md5_Encrypt(Request.Form["PassWord"].Trim(), 32));
            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            string LoginIp = GetIpaddress();

            int logincount = 0;

            if (Request.Cookies["pagerows"] != null)
            {
                logincount = MyConvert.ConToInt(Convert.ToString(Request.Cookies["pagerows"].Value));
            }

            if (logincount > 9)
            {
                Response.Write("({\"error\":\"<i></i>登录次数太多，请稍后再试\"})");
                Response.End();
            }
            if (RUser != null)
            {
                Session["Online_UserId"] = RUser.Id;
                Session["Online_UserEmail"] = RUser.UserEmail;
                Session["Online_UserMobile"] = RUser.Mobile;
                Session["Online_UserName"] = RUser.UserName;
                Session["Online_UserDept"] = RUser.Deptid;
                Session["Online_UserCompany"] = RUser.Companyid;
                Session["Online_RebateFlag"] = RUser.RebateFlag;
                Session["Online_YJDept"] = RUser.YJDeptName;
                Session["Online_UserType"] = RUser.UserType;
                if ("CCAJ".Equals(RUser.UserType) || "CCAK".Equals(RUser.UserType) || "CCPT".Equals(RUser.UserType) || "CCBB".Equals(RUser.UserType))
                {
                    Session["userType"] = "ShYinHangRegUser";
                }
                username = Request.Form["UserName"].Trim();
                MemberName = RUser.UserName;
                SetUserCookies();

                SqlQueryText = string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}',LoginIp='{2}' where id='{0}'", RUser.Id, DateTime.Now.ToString(), LoginIp);
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                string url = "/index.html";
                if (Request.Cookies["orderurl"] != null) url = "/order.html?id=12345678";
                if (Request.Cookies["redirectUrl"] != null)
                {
                    string val = Convert.ToString(Request.Cookies["redirectUrl"].Value);
                    if (!val.Equals("expired"))
                    {
                        url = val;
                    }
                    Request.Cookies.Remove("redirectUrl");
                }
                Response.Write("({\"success\":\"" + url + "\"})");
            }
            else
            {
                logincount++;
                HttpCookie cookie = default(HttpCookie);
                cookie = new HttpCookie("pagerows", logincount.ToString());
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                Response.Write("({\"error\":\"<i></i>登录名不存在或密码不正确\"})");

            }
            RUser = null;
            Response.End();
        }

        //检验用户身份
        protected void CheckMobile()
        {
            int logincount = 0;

            if (Request.Cookies["pagerows"] != null)
            {
                logincount = MyConvert.ConToInt(Convert.ToString(Request.Cookies["pagerows"].Value));
            }

            if (logincount > 9)
            {
                Response.Write("({\"error\":\"<i></i>登录次数太多，请稍后再试\"})");
                Response.End();
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='MobileLogin' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["MPassWord"].Trim()));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    Response.Write("({\"error\":\"<i></i>动态密码已过有效期，请重新发送\"})");
                    Response.End();
                }
            }
            else
            {
                logincount++;
                HttpCookie cookie = default(HttpCookie);
                cookie = new HttpCookie("pagerows", logincount.ToString());
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

                Response.Write("({\"error\":\"<i></i>手机号或动态密码不存在\"})");
                Response.End();
            }


            LoginUser.RegistUser RUser = new LoginUser.RegistUser();

            SqlQueryText = string.Format("Mobile='{0}'", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()));
            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            string LoginIp = GetIpaddress();

            if (RUser != null)
            {
                Session["Online_UserId"] = RUser.Id;
                Session["Online_UserEmail"] = RUser.UserEmail;
                Session["Online_UserName"] = RUser.UserName;
                Session["Online_UserDept"] = RUser.Deptid;
                Session["Online_UserCompany"] = RUser.Companyid;
                Session["Online_RebateFlag"] = RUser.RebateFlag;
                Session["Online_YJDept"] = RUser.YJDeptName;
                Session["Online_UserType"] = RUser.UserType;
                username = Request.Form["mobilePhone"].Trim();
                LoginMobile = Request.Form["mobilePhone"].Trim();
                MemberName = RUser.UserName;
                SetMobileCookies();

                SqlQueryText = string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}',LoginIp='{2}' where id='{0}'", RUser.Id, DateTime.Now.ToString(), LoginIp);
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                string url = "/index.html";
                if (Request.Cookies["orderurl"] != null) url = "/order.html?id=12345678";
                if (Request.Cookies["redirectUrl"] != null)
                {
                    string val = Convert.ToString(Request.Cookies["redirectUrl"].Value);
                    if (!val.Equals("expired"))
                    {
                        url = val;
                    }
                }
                Response.Write("({\"success\":\"" + url + "\"})");
            }
            else
            {
                Response.Write("({\"error\":\"<i></i>手机号不存\"})");

            }
            RUser = null;
            Response.End();
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

        //设置cookie
        protected void SetUserCookies()
        {
            HttpCookie cookie = default(HttpCookie);
            if (Request.Form["chkAutoLogin"] != null)
            {
                cookie = new HttpCookie("UserLoginMail", username);
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("UserLoginMobile", LoginMobile);
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("MemberName", HttpUtility.UrlEncode(MemberName));
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);
            }
            else
            {
                cookie = new HttpCookie("UserLoginMail", "");
                cookie.Path = "/";
                //cookie.Expires = DateTime.Now.AddDays(0);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("UserLoginMobile", "");
                cookie.Path = "/";
                Response.Cookies.Add(cookie);
            }
        }

        //设置cookie
        protected void SetMobileCookies()
        {
            HttpCookie cookie = default(HttpCookie);
            if (Request.Form["chkAMobileLogin"] != null)
            {
                cookie = new HttpCookie("UserLoginMobile", LoginMobile);
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("MemberName", HttpUtility.UrlEncode(MemberName));
                cookie.Path = "/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);
            }
            else
            {
                cookie = new HttpCookie("UserLoginMobile", "");
                cookie.Path = "/";
                Response.Cookies.Add(cookie);
            }
        }


        protected void SendSMS(string checkmobile)
        {
            if (checkmobile != "")
            {
                string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where Mobile='{0}'", checkmobile);
                if (MyDataBaseComm.getScalar(SqlQueryText) != null)
                {
                    Response.Write("({\"error\":\"<i></i>手机号码已经存在！\"})");
                    Response.End();
                }
            }

            if (Request.QueryString["code"].ToLower() != Session["CheckCode"].ToString().ToLower())
            {

                Response.Write("({\"error\":\"<i></i>验证码错误！\"})");
                Response.End();
            }

            string formRandom = Request.Form["random"].ToString();
            string postRandom = Request.QueryString["r"].ToString();
            if (!formRandom.Equals(postRandom))
            {
                Response.Write("({\"error\":\"<i></i>恶意调用发短信接口！\"})");
                Response.End();
            }

            string SendCount = MyDataBaseComm.getScalar("select count(id) from OL_SmsSend where sendtime>GETDATE()-1 and flag='MobileLogin' and mobile='" + MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()) + "'");
            if (MyConvert.ConToInt(SendCount) > 5)
            {
                Response.Write("({\"error\":\"<i></i>您的手机号码发送次数过多\"})");
                Response.End();
            }

            string ip = GetClientIP();

            if (ip != null)
            {
                string SendNum = MyDataBaseComm.getScalar("select count(id) from OL_SmsSend where sendtime>GETDATE()-1 and flag='MobileLogin' and ip='" + ip + "'");
                if (MyConvert.ConToInt(SendNum) > 30)
                {
                    SaveErrorToLog("发送多次发送验证码ip：" + ip);
                    Response.Write("({\"error\":\"<i></i>此IP发送次数过多\"})");
                    Response.End();
                }
            }

            string Verify = MyConvert.CreateNumberVerifyCode(4);
            string smscontent = "您的动态密码是：" + Verify + "，请在30分钟内完成验证，谢谢您的支持！【上海青旅】";
            string sendresult = SendSms.SendSMS_ztsms(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), smscontent);
            if (sendresult == "ok")
            {
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime,ip) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    System.Guid.NewGuid(),
                    MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()),
                    smscontent,
                    "0",
                    "MobileLogin",
                    Verify,
                    DateTime.Now.ToString(),
                    ip
                );



                //短信监控触发邮箱警告！！！Start
                string strSqlQuery = "select count(id) from dbo.OL_SmsSend where sendtime >CONVERT(varchar(100), GETDATE(), 23)";
                string SendCountNum = MyDataBaseComm.getScalar(strSqlQuery);
                if (MyConvert.ConToInt(SendCountNum) != 0 && MyConvert.ConToInt(SendCountNum) % 500 == 0)
                {
                    //发送邮箱
                    StringBuilder Strings = new StringBuilder();

                    Strings.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                    Strings.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><title>忘记密码提示</title></head>");
                    Strings.Append("<body>");
                    Strings.Append("<table width=\"650\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: 1px solid #999999;\">");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"font-size: 12px; line-height: 20px; text-align: left;\">");
                    Strings.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style='width: 465.0pt; height: 72px; margin-left: 7.5pt; border: none; border-bottom: solid #333333 1.0pt'>");
                    Strings.Append("<tr>");
                    Strings.Append("<td width=\"192\" style='width: 144.0pt; height: 40px; border: none; padding: 20px 0px 0px 0px;'>");
                    Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\"><img border=\"0\" id=\"_x0000_i1025\" src=\"http://www.scyts.com/Images/logo.gif\" alt=\"上海中国青年旅行社\" /></a>");
                    Strings.Append("</td>");
                    Strings.Append("<td width=\"428\" height=\"40\" valign=\"bottom\" align=\"right\" style='width: 321.0pt; border: none; padding: 0cm 0cm 2.5pt 0cm; font-size: 12px;'>");
                    Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\">上海中国青年旅行社首页</a>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("<table width=\"620\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-left: 10px;\">");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"font-size: 12px; line-height: 25px; padding-top: 10px;\">");
                    Strings.Append(string.Format("<strong>您好:今天{0}短信平台已经发送{1}条短信！</strong>", DateTime.Now.ToString("yyyy-MM-dd"), MyConvert.ConToInt(SendCountNum).ToString()));
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td align=\"left\" style='border: none; padding: 1.5pt 0cm 7.5pt 0cm; border-top: dashed #999999 1.0pt'>");
                    Strings.Append("<p style='line-height: 15.0pt'>");
                    Strings.Append("<span style=\"font-size: 9.0pt; color: #999999\">您之所以收到这封邮件，是因为您曾经注册成为上海中国青年旅行社网站的用户。<br />");
                    Strings.Append("本邮件由系统自动发出，请勿直接回复！</p>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("</body>");
                    Strings.Append("</html>");

                    List<string> MailTo = new List<string>();
                    MailTo.Add(ConfigurationManager.AppSettings["SmsSendMail"]);
                    string result = SendEmailClass.SendMail(MailTo.ToArray(), "短信平台监控通知", Strings.ToString(), 1, 2, "null");
                    if (result != "0")
                    {
                        SaveErrorToLog("短信监控：邮箱发送失败！！");
                    }


                    //发送短信
                    string smsMessage = "今天短信以发送：" + MyConvert.ConToInt(SendCountNum).ToString() + "，请注意！【上海青旅】";
                    string smsMessagesendresult = SendSms.SendSMS_ztsms(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), smsMessage);
                    if (smsMessagesendresult != "ok")
                    {
                        SaveErrorToLog("短信监控：短信发送失败！！");
                    }
                }
                //短信监控触发邮箱警告！！！End


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
                SaveErrorToLog("注册短信发送失败：" + sendresult);
                Response.Write("({\"error\":\"<i></i>发送失败，请稍后再试\"})");
                Response.End();
            }

        }


        protected void EditPass_Change()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser { Id = Convert.ToString(Session["Online_UserId"]), LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32) };
            if (LoginUser.LoginUser_Sql(RUser, "PassWord") != true)
            {
                Response.Write("({\"error\":\"<i></i>密码修改失败\"})");
            }
            else
            {
                Response.Write("({\"success\":\"密码修改成功\"})");
            }
            RUser = null;
            Response.End();

        }

        //上海银行用户注册
        protected void SHBankReg()
        {
            //string CardID = MyDataBaseComm.StripSQLInjection(Request.Form["CardID"].Trim());

            //卡号检查
            CheckRegCardID();
            CheckCardBin();

            String AutoId = Convert.ToString(CombineKeys.NewComb());
            LoginUser.RegistUser RUser = new LoginUser.RegistUser
            {
                Id = AutoId,
                UserName = "新用户",
                LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["password"].Trim(), 32),
                CardID = MyDataBaseComm.StripSQLInjection(Request.Form["CardID"].Trim()),
                UserType = UserType

            };

            if (LoginUser.LoginUser_Sql(RUser, "Regist") != true)
            {
                Response.Write("({\"error\":\"<i></i>注册失败\"})");
                Response.End();
            }
            else
            {
                Session["Online_UserId"] = AutoId;
                Session["Online_UserName"] = "新用户";
                Session["Online_UserDept"] = "";
                Session["Online_UserCompany"] = "";
                Session["Online_RebateFlag"] = "";
                Session["Online_YJDept"] = "";
                Session["UserType"] = "ShYinHangRegUser";
                MemberName = "新用户";

                Response.Write("({\"success\":\"注册成功\"})");
                Response.End();
            }

        }

        protected void CheckCardBin()
        {
            string CardType = MyDataBaseComm.StripSQLInjection(Request.Form["CardID"].Trim()).Substring(0, 6);
            int CardNum = Convert.ToInt32(MyDataBaseComm.StripSQLInjection(Request.Form["CardID"].Trim()).Substring(8, 9));
            List<string[]> CardBin = new List<string[]>();
            CardBin.Add(new string[] { "CCAJ", "美好生活卡", "622892", "400000001", "500000000" });
            CardBin.Add(new string[] { "CCAK", "美好生活卡（补助）", "622892", "500000001", "511000000" });
            CardBin.Add(new string[] { "CCPT", "美好生活卡IC无折卡", "620522", "300000001", "330000000" });
            CardBin.Add(new string[] { "CCBB", "美好生活卡IC配折卡", "620522", "450000001", "480000000" });
            string a = "";
            //循环行
            //循环列
            for (int i = 0; i < CardBin[i].Length; i++)
            {
                if (CardType == CardBin[i][2] && CardNum >= Convert.ToInt32(CardBin[i][3]) && CardNum <= Convert.ToInt32(CardBin[i][4]))
                {
                    UserType = CardBin[i][0];
                    return;
                }
            }
            if (UserType == null)
            {
                Response.Write("({\"error\":\"<i></i>输入的卡号非指定卡号，请确认！\"})");
                Response.End();
            }


        }

        protected void RegisterYoung()
        {
            CheckRegMobile();

            String AutoId = Convert.ToString(CombineKeys.NewComb());
            LoginUser.RegistUser RUser = new LoginUser.RegistUser
            {
                Id = AutoId,
                UserEmail = MyDataBaseComm.StripSQLInjection(Request.Form["email"].Trim()),
                UserName = MyDataBaseComm.StripSQLInjection(Request.Form["leaderName"].Trim()),
                CompanyName = MyDataBaseComm.StripSQLInjection(Request.Form["companyName"].Trim()),
                Mobile = MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()),
                LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["password"].Trim(), 32),
                UserType = "UnUpload"
            };

            if (LoginUser.LoginUser_Sql(RUser, "Regist") != true)
            {
                Response.Write("({\"error\":\"<i></i>注册失败\"})");
                Response.End();
            }
            else
            {
                Session["Online_UserId"] = AutoId;
                Session["Online_UserEmail"] = Request.Form["email"].Trim();
                Session["Online_UserName"] = Request.Form["leaderName"].Trim();
                Session["Online_UserDept"] = "";
                Session["Online_UserCompany"] = Request.Form["companyName"].Trim();
                Session["Online_RebateFlag"] = "";
                Session["Online_UserType"] = "UnUpload";
                Session["Online_YJDept"] = "";
                username = Request.Form["leaderName"].Trim();
                MemberName = "新用户";

                Response.Write("({\"success\":\"注册成功\"})");
                Response.End();
            }

        }

        private static void SaveErrorToLog(string infos)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\NewOrderLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(infos);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
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