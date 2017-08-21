using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class RegistSuccess : System.Web.UI.Page
    {
        public string LoginUserEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                if (Request.Cookies["UserLoginMail"] != null)
                {
                    LoginUserEmail = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["UserLoginMail"].Value));
                }
            }
            else
            {
                Response.Redirect("Login.aspx", true);
            }
        }
        //this.Button1.Attributes.Add("onclick", "javascript:return check_null();");
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    Response.Write("aabb");
        //}
    }
}