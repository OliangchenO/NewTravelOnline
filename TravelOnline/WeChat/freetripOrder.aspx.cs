using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Purchase;

namespace TravelOnline.WeChat
{
    public partial class freetripOrder : System.Web.UI.Page
    {
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            Response.Cookies.Add(new HttpCookie("CheckCode", ""));
        }
    }
}