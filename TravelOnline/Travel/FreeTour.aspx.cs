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
    public partial class FreeTour : System.Web.UI.Page
    {
        public string OutBoundAffiche, OutBoundProductHtml, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            OutBoundAffiche = CreateAfficheHtml.CreateAffiche("FreeTour");

            if (Convert.ToString(Cache["FreeTourProductHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("FreeTour");
            }
            OutBoundProductHtml = Convert.ToString(Cache["FreeTourProductHtml"]);

            LineOnHotSale = LinePreferences.LineOnHotSale("FreeTour");
        }
    }
}