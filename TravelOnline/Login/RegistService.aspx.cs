using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.GetCombineKeys;
using TravelOnline.LoginUsers;
using TravelOnline.EncryptCode;

namespace TravelOnline.Login
{
    public partial class RegistService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            
            CheckPassWord();
            CheckAuthcode();
            CheckEmail();
            CheckMobile();
            String AutoId = Convert.ToString(CombineKeys.NewComb());

            LoginUser.RegistUser RUser = new LoginUser.RegistUser { 
                Id = AutoId, 
                UserEmail = Request.Form["mail"].Trim(), 
                UserName = Request.Form["username"].Trim(), 
                Mobile = Request.Form["mobile"].Trim(), 
                LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32) };

            if (LoginUser.LoginUser_Sql(RUser, "Regist") != true)
            {
                Response.Write("({\"info\":\"注册失败\"})");
                Response.End();
            }

            LoginUser.RegistUser RUser_load = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("UserEmail='{0}' and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(Request.Form["mail"].Trim()), SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32));

            RUser_load = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser_load != null)
            {
                Session["Online_UserId"] = RUser_load.Id;
                Session["Online_UserName"] = RUser_load.UserName;
                Session["Online_UserDept"] = RUser_load.Deptid;
                Session["Online_UserCompany"] = RUser_load.Companyid;
                Session["Online_RebateFlag"] = RUser_load.RebateFlag;
                Session["Online_YJDept"] = RUser_load.YJDeptName;
                //string ips =  Page.Request.UserHostAddress;
                //SqlQueryText = string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}' where id='{0}'", RUser.Id, DateTime.Now.ToString());
                //MyDataBaseComm.ExcuteSql(SqlQueryText);
            }
            else
            {
                Session["Online_UserId"] = AutoId;
                Session["Online_UserName"] = HttpUtility.UrlEncode(Request.Form["username"]);
            }

            

            HttpCookie cookie = default(HttpCookie);
            cookie = new HttpCookie("UserLoginMail", HttpUtility.UrlEncode(Request.Form["mail"].Trim()));
            //cookie.Values.Add("UserMail", HttpUtility.UrlEncode(Request.Form["mail"].Trim()));
            cookie.Path = "/login/";
            cookie.Expires = DateTime.Now.AddDays(90);
            Response.Cookies.Add(cookie);

            cookie = new HttpCookie("MemberName", HttpUtility.UrlEncode(Request.Form["username"].Trim()));
            cookie.Path = "/login/";
            cookie.Expires = DateTime.Now.AddDays(90);
            Response.Cookies.Add(cookie);
           
            Response.Write("({\"success\":1})");
            Response.End();
        }

        protected void CheckEmail()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", Request.Form["mail"].Trim());
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"info\":\"邮件地址已被注册\"})");
                Response.End();
            }
        }

        protected void CheckMobile()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_LoginUser where Mobile='{0}'", Request.Form["mobile"].Trim());
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"info\":\"手机号码已经存在\"})");
                Response.End();
            }
        }

        protected void CheckPassWord()
        {
            if (String.Compare(Request.Form["pwd"].Trim(), Request.Form["pwd2"].Trim(), true) != 0)
            {
                Response.Write("({\"info\":\"两次输入的密码不一致\"})");
                Response.End();
            }
        }

        protected void CheckAuthcode()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                if (String.Compare(Session["CheckCode"].ToString(), Request.Form["authcode"].Trim(), true) != 0)
                {

                    Response.Write("({\"info\":\"验证码错误\"})");
                    Response.End();
                }
            }
        }

    }
}