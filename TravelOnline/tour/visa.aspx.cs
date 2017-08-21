using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.tour
{
    public partial class visa : System.Web.UI.Page
    {
        public string SlidePicHtml, Visa_3, Visa_T;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToString(Cache["SlidePic_Visa_New"]) == "") SlidePic.GreateSlideCache("Visa_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_Visa_New"]);

            if (Convert.ToString(Cache["SlidePic_Visa_3"]) == "") SlidePic.GreateThreePicCache("Visa_3");
            Visa_3 = Convert.ToString(Cache["SlidePic_Visa_3"]);

            if (Convert.ToString(Cache["SlidePic_Visa_T"]) == "") SlidePic.GreateTongLanCache("Visa_T");
            Visa_T = Convert.ToString(Cache["SlidePic_Visa_T"]);

        }
    }
}