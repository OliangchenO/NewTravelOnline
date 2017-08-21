using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Member
{
    public partial class Login : System.Web.UI.Page
    {
        public string LoginUser, LoginMobile;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserLoginMail"] != null)
            {
                LoginUser = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["UserLoginMail"].Value));
            }

            if (Request.Cookies["UserLoginMobile"] != null)
            {
                LoginMobile = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["UserLoginMobile"].Value));
            }
        }
    }
}