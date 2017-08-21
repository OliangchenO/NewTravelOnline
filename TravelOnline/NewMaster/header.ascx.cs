using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.NewMaster
{
    public partial class header : System.Web.UI.UserControl
    {
        public Guid ucode;
        public string Infos = "", username;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            username = Convert.ToString(Session["Online_UserName"]);
        }
        
    }
}