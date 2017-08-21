using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class RegNow : System.Web.UI.Page
    {
        public Guid ucode;
        public string loginflag = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            loginflag = Request.QueryString["flag"];
            ucode = System.Guid.NewGuid();
            Response.Cookies.Add(new HttpCookie("CheckCode", ""));
        }
    }
}