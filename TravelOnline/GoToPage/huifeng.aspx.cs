using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.GoToPage
{
    public partial class huifeng : System.Web.UI.Page
    {
        string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Url = string.Format("~/line3/{0}.html", Request.QueryString["lineid"]);
            HttpCookie cookie = default(HttpCookie);
            cookie = new HttpCookie("HuiFeng2014", HttpUtility.UrlEncode(Request.QueryString["lineid"]));
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookie);
            Response.Redirect(Url, true);
        }
    }
}