using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Management
{
    public partial class EditPassWord : System.Web.UI.Page
    {
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                ucode = System.Guid.NewGuid();
            }
            else
            {
                Response.Redirect("/manage/Login.aspx", true);
            }

        }
    }
}