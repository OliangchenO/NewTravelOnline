using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.tour
{
    public partial class outbound : System.Web.UI.Page
    {
        public string SlidePicHtml, OutBound_3, OutBound_T;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToString(Cache["SlidePic_OutBound_New"]) == "") SlidePic.GreateSlideCache("OutBound_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_OutBound_New"]);

            if (Convert.ToString(Cache["SlidePic_OutBound_3"]) == "") SlidePic.GreateThreePicCache("OutBound_3");
            OutBound_3 = Convert.ToString(Cache["SlidePic_OutBound_3"]);

            if (Convert.ToString(Cache["SlidePic_OutBound_T"]) == "") SlidePic.GreateTongLanCache("OutBound_T");
            OutBound_T = Convert.ToString(Cache["SlidePic_OutBound_T"]);


        }
    }
}