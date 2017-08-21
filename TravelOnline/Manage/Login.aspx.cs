using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Manage
{
    public partial class Login : System.Web.UI.Page
    {
        public Guid ucode;
        public string ManagerLoginName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["loginname"].Value)));
            //Response.End();
            //Response.AppendHeader("Refresh", "0"); 不停的自动刷新

            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (!Page.IsPostBack)
            {
                ucode = System.Guid.NewGuid();
                Response.Cookies.Add(new HttpCookie("CheckCode", ""));

                if (Request.Cookies["loginname"] != null)
                {
                    ManagerLoginName = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["loginname"].Value));
                }
            }
        }
    }
}