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
    public partial class NewsList : System.Web.UI.Page
    {
        public string LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/info/index_news.html");
            Response.End();

            //id = Request.QueryString["Id"];
            LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            //LoadInfo();
        }
    }
}