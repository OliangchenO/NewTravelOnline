using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.WeChat
{
    public partial class main_m : System.Web.UI.Page
    {
        public string flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["flag"].ToString();
            Session["UserFrom"] = flag;
        }
    }
}