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
    public partial class Visa : System.Web.UI.Page
    {
        public string OutBoundAffiche, VisaProductHtml, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            OutBoundAffiche = CreateAfficheHtml.CreateAffiche("Visa");

            if (Convert.ToString(Cache["VisaProductHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("Visa");
            }
            VisaProductHtml = Convert.ToString(Cache["VisaProductHtml"]);

            LineOnHotSale = LinePreferences.LineOnHotSale("OutBound");
        }
    }
}