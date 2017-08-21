using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.LoginUsers;
using System.Text;
using TravelOnline.EncryptCode;
using TravelOnline.GetCombineKeys;
using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Login
{
    public partial class AjaxService : System.Web.UI.Page
    {
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
                case "CheckAuthcode":
                    //验证码判断
                    CheckAuthcode();
                    break;
                case "CheckUemail":
                    //邮件地址注册判断
                    CheckUemail();
                    break;
                case "FindEmail":
                    //邮件地址找回密码
                    FindEmail();
                    break;
                case "SendEmail":
                    //邮件地址找回密码
                    SendEmail();
                    break;
                case "RestePwd":
                    //找回密码
                    RestePwd();
                    break;
                case "ChangePassWord":
                    if (Convert.ToString(Session["Online_UserId"]).Length == 0)
                    {
                        Response.Write("({\"logout\":\"尚未登录\"})");
                        Response.End();
                    }
                    EditPass_CheckAuthcode();
                    EditPass_CheckPassWord();
                    EditPass_Change();
                    //修改密码
                    //sqlstr = sqlstr + " planok='1' and ";
                    break;
                case "EditInfo":
                    //if (Convert.ToString(Session["Online_UserId"]).Length == 0)
                    //{
                    //    Response.Write("({\"logout\":\"尚未登录\"})");
                    //    Response.End();
                    //}
                    EditPass_CheckAuthcode();
                    SaveUserInfo();
                    //已满
                    //sqlstr = sqlstr + " (orderseats = seats) and ";
                    break;
                case "IsLogin":
                    //验证码判断
                    CheckIsLogin();
                    break;
                case "SendAuthcodeSMS":
                    //发送积分会员注册短信
                    SendAuthcodeSMS();
                    break;
                case "JoinInfo":
                    //加入积分会员
                    JoinInfo();
                    break;
                case "NotMemberOrderSubmit":
                    //非会员下单
                    NotMemberOrderSubmit();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }

        protected void JoinInfo()
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                Response.Write("({\"success\":1,\"content\":\"尚未登录！\"})");
                Response.End();
            }
            if (MyDataBaseComm.getScalar("select id from OL_Member where uid='" + Convert.ToString(Session["Online_UserId"]) + "'") != null)
            {
                Response.Write("({\"success\":1,\"content\":\"您已经是积分会员，不能再次提交信息！\"})");
                Response.End();
            }

            string SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='MemberVerify' and uid='{0}' order by sendtime desc", Convert.ToString(Session["Online_UserId"]));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    Response.Write("({\"success\":1,\"content\":\"验证码已过有效期，请重新发送！\"})");
                    Response.End();
                }
                if (DS.Tables[0].Rows[0]["mobile"].ToString() != Request.Form["mobile"].Trim())
                {
                    Response.Write("({\"success\":1,\"content\":\"您的手机号码与验证手机不符，请检查！\"})");
                    Response.End();
                }
                if (DS.Tables[0].Rows[0]["extinfo"].ToString() != Request.Form["authcode"].Trim())
                {
                    Response.Write("({\"success\":1,\"content\":\"验证码错误，请检查！\"})");
                    Response.End();
                }
            }
            else
            {
                Response.Write("({\"success\":1,\"content\":\"验证码不存在，请重新发送！\"})");
                Response.End();
            }

            SqlQueryText = string.Format("insert into OL_Member (uid,name,birthday,mobile,email,Sex,Address,joindate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                Convert.ToString(Session["Online_UserId"]),
                Request.Form["truename"].Trim(),
                Request.Form["birthdayYear"].Trim() + "-" + Request.Form["birthdayMonth"].Trim() + "-" + Request.Form["birthdayDay"].Trim(),
                Request.Form["mobile"].Trim(),
                Request.Form["email"].Trim(),
                MyConvert.ConToInt(Request.Form["sex"].Trim()),
                Request.Form["address"].Trim(),
                DateTime.Now.ToString()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                LoginUser.RegistUser RUser = new LoginUser.RegistUser
                {
                    Id = Convert.ToString(Session["Online_UserId"]),
                    UserName = Request.Form["truename"].Trim(),
                    Mobile = Request.Form["mobile"].Trim(),
                    Address = Request.Form["address"].Trim(),
                    Sex = MyConvert.ConToInt(Request.Form["sex"].Trim()),
                    BirtyDay = Request.Form["birthdayYear"].Trim() + "-" + Request.Form["birthdayMonth"].Trim() + "-" + Request.Form["birthdayDay"].Trim()
                };
                LoginUser.LoginUser_Sql(RUser, "EditInfo");
                RUser = null;

                Response.Write("({\"success\":0,\"content\":\"您已经成功加入积分会员！\"})");
                Response.End();
            }
            else
            {
                Response.Write("({\"success\":1,\"content\":\"会员信息提交失败，请稍后再试！\"})");
                Response.End();
            }
            
        }

        protected void SendAuthcodeSMS()
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                Response.Write("{\"success\":1,\"content\":\"尚未登录！\"}");
                Response.End();
            }
                    
            if (MyDataBaseComm.getScalar("select id from OL_Member where uid='" + Convert.ToString(Session["Online_UserId"]) + "'") != null)
            {
                Response.Write("{\"success\":1,\"content\":\"您已经加入了积分会员，不能发送验证短信！\"}");
                Response.End();
            }

            if (MyDataBaseComm.getScalar("select mobile from OL_Member where mobile='" + Request.QueryString["mobile"] + "'") != null)
            {
                Response.Write("{\"success\":1,\"content\":\"您的手机号码已经被注册，请换另一个号码！\"}");
                Response.End();
            }

            string SendCount = MyDataBaseComm.getScalar("select count(id) from OL_SmsSend where flag='MemberVerify' and mobile='" + Request.QueryString["mobile"] + "'").ToString();
            if (MyConvert.ConToInt(SendCount)>10)
            {
                Response.Write("{\"success\":1,\"content\":\"您的手机号码发送次数过多，如果您仍未收到验证短信，请更换另一个号码！\"}");
                Response.End();
            }

            string Verify = MyConvert.CreateNumberVerifyCode(6);
            string smscontent = "欢迎您注册上海青旅积分会员，您的验证码是：" + Verify + "，请在30分钟内完成验证，谢谢您的支持！【上海青旅】";
            string sendresult = SendSms.SendSMS_ztsms(Request.QueryString["mobile"], smscontent);
            if (sendresult == "ok")
            {
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    Convert.ToString(Session["Online_UserId"]),
                    Request.QueryString["mobile"],
                    smscontent,
                    "0",
                    "MemberVerify",
                    Verify,
                    DateTime.Now.ToString()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":0,\"content\":\"短信发送成功，如未收到，请60秒后重发！\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"success\":1,\"content\":\"短信发送失败，请稍后再试\"}");
                    Response.End();
                }
            }
            else
            {
                Response.Write("{\"success\":1,\"content\":\"短信发送失败，请稍后再试\"}");//" + sendresult + "
                Response.End();
            }

        }

        protected void RestePwd()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                Response.Write("({\"info\":\"验证码错误\"})");
                Response.End();
            }

            string NewPass = "";
            string SqlQueryText = string.Format("select * from OL_FindPWD where findid='{0}'", Request.Form["uid"].Trim());

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddHours(-2);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["SaveTime"]) < rp_times)
                {
                    Response.Write("({\"info\":\"密码重设链接已失效\"})");
                    Response.End();
                }

                List<string> Sql = new List<string>();

                NewPass = SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32);
                Sql.Add(string.Format("update OL_LoginUser set LoginPassWord ='{1}' where Id='{0}'", DS.Tables[0].Rows[0]["uid"].ToString(), NewPass));

                rp_times = rp_times.AddHours(-12);
                Sql.Add(string.Format("update OL_FindPWD set SaveTime ='{1}' where findid='{0}'", Request.Form["uid"].Trim(), rp_times));

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    Response.Write("({\"success\":\"1\"})");
                }
                else
                {
                    Response.Write("({\"info\":\"密码重设失败\"})");
                    Response.End();
                }
            }
            else
            {
                Response.Write("({\"info\":\"密码重设失败\"})");
                Response.End();
            }

        }

        protected void SendEmail()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                Response.Write("({\"info\":\"验证码错误\"})");
                Response.End();
            }

            //string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", Request.Form["mail"].Trim());
            //string uid = MyDataBaseComm.getScalar(SqlQueryText);

            string SqlQueryText = string.Format("select top 1 id,UserName,UserEmail from OL_LoginUser where UserEmail='{0}'", Request.Form["mail"].Trim());
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //DS.Tables[0].Rows[0]["LogTime"].ToString()
                String AutoId = Convert.ToString(CombineKeys.NewComb());

                SqlQueryText = string.Format("insert into OL_FindPWD (uid,findid,SaveTime,UserEmail) values ('{0}','{1}','{2}','{3}')", DS.Tables[0].Rows[0]["id"].ToString(), AutoId, DateTime.Now.ToString(), Request.Form["mail"].Trim());
                    
                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {

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
                    Strings.Append(string.Format("<strong>尊敬的{0}，您好:</strong>", DS.Tables[0].Rows[0]["UserName"].ToString()));
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"line-height: 20px; padding-top: 0px; font-size: 12px;\">");
                    Strings.Append("您在上海中国青年旅行社网站（www.scyts.com）点击了“忘记密码”按钮，故系统自动为您发送了这封邮件。您可以点击以下链接修改您的密码：<br />");
                    Strings.Append(string.Format("<a href=\"http://www.scyts.com/login/ResetPassword.aspx?UID={0}\" target=\"_blank\">http://www.scyts.com/login/ResetPassword.aspx?UID={0}</a>", AutoId));
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"line-height: 20px; padding-top: 8px; font-size: 12px;\">");
                    Strings.Append("此链接有效期为两个小时，请在两小时内点击链接进行修改。如果您不需要修改密码，或者您从未点击过“忘记密码”按钮，请忽略本邮件。");
                    Strings.Append("<br />");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"line-height: 20px; padding-top: 2px; font-size: 12px;\">");
                    Strings.Append("<p><br>");
                    Strings.Append("如有任何疑问，请联系上海中国青年旅行社客服，客服热线：<span lang=\"EN-US\" xml:lang=\"EN-US\">4006-777-666</span></p>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"line-height: 40px; font-size: 12px\">");
                    Strings.Append("<strong>欢迎您再次到上海中国青年旅行社预订旅游产品，祝您旅途愉快！<span lang=\"EN-US\" xml:lang=\"EN-US\"></span></strong>");
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
                    MailTo.Add(DS.Tables[0].Rows[0]["UserEmail"].ToString());
                    string result = SendEmailClass.SendMail(MailTo.ToArray(), "用户密码找回(www.scyts.com)", Strings.ToString(), 1, 2, "null");
                    if (result == "0")
                    {
                        Response.Write("({\"success\":\"" + AutoId + "\"})");
                    }
                    else
                    {
                        Response.Write("({\"info\":\"提交失败\"})");
                    }
                    
                }
                else
                {
                    Response.Write("({\"info\":\"提交失败\"})");
                }
            }
            else
            {
                Response.Write("({\"info\":\"邮件地址不存在\"})");
                Response.End();
            }

        }

        protected void FindEmail()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", Request.QueryString["str"].Trim());
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void CheckIsLogin()
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void SaveUserInfo()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser { 
                Id = Convert.ToString(Session["Online_UserId"]),
                UserName = Request.Form["truename"].Trim(),
                Tel = Request.Form["tel"].Trim(),
                Mobile = Request.Form["mobile"].Trim(),
                Address = Request.Form["address"].Trim(),
                ZipCode = Request.Form["zipcode"].Trim(),
                Marriage = MyConvert.ConToInt(Request.Form["marriage"].Trim()),
                Income = MyConvert.ConToInt(Request.Form["income"].Trim()),
                Hobby = Request.Form["remark"].Trim(),
                Career = MyConvert.ConToInt(Request.Form["career"].Trim()),
                Sex = MyConvert.ConToInt(Request.Form["sex"].Trim()),
                BirtyDay = Request.Form["birthdayYear"].Trim() + "-" + Request.Form["birthdayMonth"].Trim() + "-" + Request.Form["birthdayDay"].Trim()
            };

            if (LoginUser.LoginUser_Sql(RUser, "EditInfo") != true)
            {
                Response.Write("({\"info\":\"信息修改失败\"})");
            }
            else
            {
                Response.Write("({\"success\":\"信息修改成功\"})");
            }
            RUser = null;
            Response.End();
        }

        protected void EditPass_CheckAuthcode()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                Response.Write("({\"authcode\":\"验证码错误\"})");
                Response.End();
            }
        }

        protected void EditPass_CheckPassWord()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where Id='{0}' and LoginPassWord='{1}'", Convert.ToString(Session["Online_UserId"]), SecurityCode.Md5_Encrypt(Request.Form["loginpwd"].Trim(), 32));
            if (MyDataBaseComm.getScalar(SqlQueryText) == null)
            {
                Response.Write("({\"pwd\":\"旧登录密码不正确\"})");
                Response.End();
            }
        }

        protected void EditPass_Change()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser { Id = Convert.ToString(Session["Online_UserId"]), LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32) };
            if (LoginUser.LoginUser_Sql(RUser, "PassWord") != true)
            {
                Response.Write("({\"info\":\"密码修改失败\"})");
            }
            else
            {
                Response.Write("({\"success\":\"密码修改成功\"})");
            }
            RUser = null;
            Response.End();
            
        }

        protected void CheckAuthcode()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.QueryString["str"].Trim(), true) != 0)
            {
                Response.Write("{\"success\":1}");
            }
            else
            {
                Response.Write("{\"success\":0}");
            }
            Response.End();
        }

        protected void CheckUemail()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", Request.QueryString["str"].Trim());
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":1}");
            }
            else
            {
                Response.Write("{\"success\":0}");
            }
            Response.End();
        }

        protected void NotMemberOrderSubmit()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='NotMemberOrderSubmit' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["Phoneyzm"].Trim()));

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
                SqlQueryText = string.Format("select * from OL_LoginUser where ThirdPartyType='NotMember' and ThirdPartyID = '{0}'", MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()));
                DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Session["Online_UserId"] = DS.Tables[0].Rows[0]["Id"];
                    Session["Online_UserEmail"] = DS.Tables[0].Rows[0]["UserEmail"];
                    Session["Online_UserName"] = DS.Tables[0].Rows[0]["UserName"];
                    Session["Online_ThirdPartyID"] = DS.Tables[0].Rows[0]["ThirdPartyID"];
                    Session["Online_ThirdPartyType"] = DS.Tables[0].Rows[0]["ThirdPartyType"];
                }
                else
                {
                    string id = Convert.ToString(CombineKeys.NewComb());
                    LoginUser.RegistUser RUser = new LoginUser.RegistUser
                    {
                        Id = id,
                        UserName = "新用户",
                        ThirdPartyID = MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()),
                        ThirdPartyType = "NotMember"
                    };
                    if (LoginUser.LoginUser_Sql(RUser, "NotMember") != true)
                    {
                        Response.Write("({\"error\":\"<i></i>提交失败\"})");
                        Response.End();
                    }
                    else
                    {
                        Session["Online_UserId"] = id;
                        Session["Online_UserEmail"] = "";
                        Session["Online_UserName"] = "新用户";
                        Session["Online_ThirdPartyID"] = MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim());
                        Session["Online_ThirdPartyType"] = "NotMember";
                    }

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