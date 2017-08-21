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

namespace TravelOnline.Login
{
    public partial class LoginService : System.Web.UI.Page
    {
        public string username, MemberName;

        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            CheckAuthcode();
            CheckEmail();
            SetUserCookies();
                        
            Response.Write("({\"success\":1})");
            Response.End();
        }

        protected void CheckAuthcode()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                Response.Write("({\"authcode\":\"验证码错误\"})");
                Response.End();
            }
        }

        //检验用户身份
        protected void CheckEmail()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("(UserEmail='{0}' or Mobile='{0}' or cardId = '{0}') and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(Request.Form["loginname"].Trim()), SecurityCode.Md5_Encrypt(Request.Form["loginpwd"].Trim(), 32));

            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser != null)
            {
                Session["Online_UserId"] = RUser.Id;
                Session["Online_UserEmail"] = RUser.UserEmail;
                Session["Online_UserName"] = RUser.UserName;
                Session["Online_UserMobile"] = RUser.Mobile;
                Session["Online_UserDept"] = RUser.Deptid;
                Session["Online_UserCompany"] = RUser.Companyid;
                Session["Online_RebateFlag"] = RUser.RebateFlag;
                Session["Online_YJDept"] = RUser.YJDeptName;
                Session["Online_UserType"] = RUser.UserType;
                if ("CCAJ".Equals(RUser.UserType) || "CCAK".Equals(RUser.UserType) || "CCPT".Equals(RUser.UserType) || "CCBB".Equals(RUser.UserType))
                {
                    Session["userType"] = "ShYinHangRegUser";
                }
                username = Request.Form["loginname"].Trim();
                MemberName = RUser.UserName;
                //string ips =  Page.Request.UserHostAddress;
                SqlQueryText = string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}' where id='{0}'", RUser.Id, DateTime.Now.ToString());
                MyDataBaseComm.ExcuteSql(SqlQueryText);
            }
            else
            {
                Response.Write("({\"username\":\"用户不存在或密码不正确\"})");
                Response.End();
            }

            RUser = null;
            //if (Request.Form["loginname"].Trim().ToUpper() == "11@11.COM")
            //{
            //    Response.Write("({\"username\":\"用户不存在\"})");
            //    Response.End();
            //}
            //else
            //{ 
            //    Session["Online_UserId"] = "1211";            
            //}
        }

        //设置cookie
        protected void SetUserCookies()
        {
            HttpCookie cookie = default(HttpCookie);
            if (Request.Form["chkRememberUsername"] != null)
            {
                cookie = new HttpCookie("UserLoginMail", username);
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
                cookie = new HttpCookie("UserLoginMail", username);
                cookie.Path = "/";
                //cookie.Expires = DateTime.Now.AddDays(0);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("MemberName", HttpUtility.UrlEncode(MemberName));
                cookie.Path = "/";
                //cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);
            }

            //if (Request.Form["chkRememberMe"] != null)
            //{
            //    cookie = new HttpCookie("UserLoginInfos", HttpUtility.UrlEncode(Request.Form["loginpwd"].Trim()));
            //    cookie.Path = "/Login/";
            //    cookie.Expires = DateTime.Now.AddDays(90);
            //    Response.Cookies.Add(cookie);
            //}
            //else
            //{
            //    cookie = new HttpCookie("UserLoginInfos", "");
            //    cookie.Path = "/Login/";
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(cookie);
            //}
        }                
    }
}