using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.tour
{
    public partial class freetour : System.Web.UI.Page
    {
        public string SlidePicHtml, FreeTour_3, FreeTour_T;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToString(Cache["SlidePic_FreeTour_New"]) == "") SlidePic.GreateSlideCache("FreeTour_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_FreeTour_New"]);

            if (Convert.ToString(Cache["SlidePic_FreeTour_3"]) == "") SlidePic.GreateThreePicCache("FreeTour_3");
            FreeTour_3 = Convert.ToString(Cache["SlidePic_FreeTour_3"]);

            if (Convert.ToString(Cache["SlidePic_FreeTour_T"]) == "") SlidePic.GreateTongLanCache("FreeTour_T");
            FreeTour_T = Convert.ToString(Cache["SlidePic_FreeTour_T"]);
        }
    }
}