using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class Login : System.Web.UI.Page
    {
        public Guid ucode;
        public string LoginUserEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            Response.Redirect("/member/login.html");

            if (!Page.IsPostBack)
            {
                ucode = System.Guid.NewGuid();
                //Response.Write(u);
                Response.Cookies.Add(new HttpCookie("CheckCode", ""));

                if (Request.Cookies["UserLoginMail"] != null)
                {
                    LoginUserEmail = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["UserLoginMail"].Value));
                }
            }
        }

       
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if (Request.Cookies["CheckCode"] == null)
        //    {
        //        Label1.Text = "您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。";
        //        Label1.Visible = true;
        //        return;
        //    }

        //    if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
        //    {
        //        Label1.Text = "<font color=red>对不起，验证码错误！</font>" + Request.Cookies["CheckCode"].Value;
        //        Label1.Visible = true;
        //        return;
        //    }
        //    else
        //    {
        //        Label1.Text = "<font color=green>恭喜，验证码输入正确！</font>";
        //        Label1.Visible = true;
        //    }

        //}
    }
}