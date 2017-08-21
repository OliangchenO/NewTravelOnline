using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.LoginUsers;

namespace TravelOnline.WeChat
{
    public partial class Fx_editinfo : System.Web.UI.Page
    {
        public LoginUser.RegistUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Session["Fx_Login"] && Session["Fx_Login"]!="Y")
            {
                user = LoginUser.LoginFxUser("Id='" + Session["Fx_UserId"].ToString() + "'");
            }
            else
            {
                Response.Redirect("/WeChat/Fx_login.aspx");
            }
        }
    }
}