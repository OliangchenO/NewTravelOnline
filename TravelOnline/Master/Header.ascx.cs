using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;

namespace TravelOnline.Master
{
    public partial class Header : System.Web.UI.UserControl
    {
        public Guid ucode;
        public string Infos = "", username;
        public string ReportTop;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            username = Convert.ToString(Session["Online_UserName"]);

            //if (Convert.ToString(Cache["ReportTopCss"]) == "")
            //{
            //    GetProductClass.GetReportTopCss();
            //}
            //ReportTop = Convert.ToString(Cache["ReportTopCss"]);
        }
    }
}