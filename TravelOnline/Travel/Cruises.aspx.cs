using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using TravelOnline.Class.Travel;

namespace TravelOnline.Travel
{
    public partial class Cruises : System.Web.UI.Page
    {
        public string OutBoundAffiche, OutBoundProductHtml, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            OutBoundAffiche = CreateAfficheHtml.CreateAffiche("Cruises");

            if (Convert.ToString(Cache["CruisesProductHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("Cruises");
            }
            OutBoundProductHtml = Convert.ToString(Cache["CruisesProductHtml"]);

            LineOnHotSale = LinePreferences.LineOnHotSale("Cruises");
        }
    }
}