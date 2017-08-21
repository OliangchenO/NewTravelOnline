using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class LoginNow : System.Web.UI.Page
    {
        public Guid ucode;
        public string LoginUserEmail = "", loginflag, hide="hide";
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            loginflag = Request.QueryString["flag"];
            if (loginflag == "neworder") hide = "";
            if (loginflag == "checkCoupon") hide = "hide";
            if (loginflag == "checkIntegral") hide = "hide";
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

    }
}