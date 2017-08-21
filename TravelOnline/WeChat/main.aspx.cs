using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeiXinPay;

namespace TravelOnline.WeChat
{
    public partial class main : System.Web.UI.Page
    {
        public string Fx_UserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Fx_UserId"] != null)
            {
                Fx_UserId = Session["Fx_UserId"].ToString();
            }
            else
            {
                Fx_UserId = null;
            }
        }

    }
}