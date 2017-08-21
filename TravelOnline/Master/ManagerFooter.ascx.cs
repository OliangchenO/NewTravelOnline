using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Master
{
    public partial class ManagerFooter : System.Web.UI.UserControl
    {
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
        }
    }
}