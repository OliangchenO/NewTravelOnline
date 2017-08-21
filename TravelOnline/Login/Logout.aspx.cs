using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            Session.Abandon();

            //Response.Cookies.Add(new HttpCookie("UserLoginInfos", ""));
            //HttpCookie cookie = default(HttpCookie);
            //cookie = new HttpCookie("UserLoginInfos","");
            //cookie.Path = "/Login/";
            //cookie.Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies.Add(cookie);
            //if (Request.Cookies["pageurl"] != null)
            //{
            //    Response.Redirect(Request.Cookies["pageurl"].ToString(), true);
            //}
            //else
            //{
            //    Response.Redirect("~/index.html", true);
            //}  
            
        }
    }
}