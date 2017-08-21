using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Users
{
    public partial class ChangePassWord : System.Web.UI.Page
    {
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                ucode = System.Guid.NewGuid();
            }
            else
            {
                Response.Redirect("/login/login.aspx", true);
            }

        }
    }
}