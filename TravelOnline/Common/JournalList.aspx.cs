using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Travel;

namespace TravelOnline.Common
{
    public partial class JournalList : System.Web.UI.Page
    {
        public string LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/journallist.html");
            Response.End();

            //id = Request.QueryString["Id"]; /journallist.html
            //LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            //LoadInfo();
        }
    }
}