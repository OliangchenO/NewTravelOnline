using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Manage;

namespace TravelOnline.Manage
{
    public partial class LoginService : System.Web.UI.Page
    {
        public string username, LoginName;
    
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
            ManageUsers.UserClass RUser = new ManageUsers.UserClass();
            string SqlQueryText;
            SqlQueryText = string.Format("LoginName='{0}' and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(Request.Form["managename"].Trim()), SecurityCode.Md5_Encrypt(Request.Form["loginpwd"].Trim(), 32));

            RUser = ManageUsers.ManageUseDetail(SqlQueryText);
            if (RUser != null)
            {
                Session["Manager_UserId"] = RUser.Id;
                Session["Manager_UserRight"] = RUser.RightInfos;
                Session["Manager_UserName"] = RUser.UserName;
                username = RUser.UserName;
                LoginName = RUser.LoginName;
            }
            else
            {
                Response.Write("({\"username\":\"用户不存在或密码不正确\"})");
                Response.End();
            }

            RUser = null;
        }

        //设置cookie
        protected void SetUserCookies()
        {
            HttpCookie cookie = default(HttpCookie);
            if (Request.Form["chkRememberUsername"] != null)
            {
                cookie = new HttpCookie("loginname", LoginName);
                cookie.Path = "/manage/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);

                cookie = new HttpCookie("loginname", LoginName);
                cookie.Path = "/Manage/";
                cookie.Expires = DateTime.Now.AddDays(90);
                Response.Cookies.Add(cookie);
            }
            else
            {
                cookie = new HttpCookie("loginname", LoginName);
                Response.Cookies.Add(cookie);
            }
            
            cookie = new HttpCookie("username", HttpUtility.UrlEncode(username));
            cookie.Path = "/manage/";
            cookie.Expires = DateTime.Now.AddDays(90);
            Response.Cookies.Add(cookie);

            cookie = new HttpCookie("username", HttpUtility.UrlEncode(username));
            cookie.Path = "/Manage/";
            cookie.Expires = DateTime.Now.AddDays(90);
            Response.Cookies.Add(cookie);

            cookie = new HttpCookie("OnlyOnlinePay", "");
            cookie.Expires = DateTime.Now.AddDays(-10);
            Response.Cookies.Add(cookie);

            cookie = new HttpCookie("OnlyIcbcPay", "");
            cookie.Expires = DateTime.Now.AddDays(-10);
            Response.Cookies.Add(cookie);
        }                
    }
}