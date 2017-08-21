using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.tour
{
    public partial class cruises : System.Web.UI.Page
    {
        public string SlidePicHtml, Cruises_3, Cruises_T, ShipRecomm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Cache["SlidePic_Cruises_New"]) == "") SlidePic.GreateSlideCache("Cruises_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_Cruises_New"]);

            if (Convert.ToString(Cache["SlidePic_Cruises_3"]) == "") SlidePic.GreateThreePicCache("Cruises_3");
            Cruises_3 = Convert.ToString(Cache["SlidePic_Cruises_3"]);

            if (Convert.ToString(Cache["SlidePic_Cruises_T"]) == "") SlidePic.GreateTongLanCache("Cruises_T");
            Cruises_T = Convert.ToString(Cache["SlidePic_Cruises_T"]);

            if (Convert.ToString(Cache["SlidePic_CruisesShip"]) == "") SlidePic.GreateShipRecommCache("CruisesShip");
            ShipRecomm = Convert.ToString(Cache["SlidePic_CruisesShip"]);

        }
    }
}