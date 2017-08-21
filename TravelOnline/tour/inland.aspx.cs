using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.tour
{
    public partial class inland : System.Web.UI.Page
    {
        public string SlidePicHtml, InLand_3, InLand_T;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToString(Cache["SlidePic_InLand_New"]) == "") SlidePic.GreateSlideCache("InLand_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_InLand_New"]);

            if (Convert.ToString(Cache["SlidePic_InLand_3"]) == "") SlidePic.GreateThreePicCache("InLand_3");
            InLand_3 = Convert.ToString(Cache["SlidePic_InLand_3"]);

            if (Convert.ToString(Cache["SlidePic_InLand_T"]) == "") SlidePic.GreateTongLanCache("InLand_T");
            InLand_T = Convert.ToString(Cache["SlidePic_InLand_T"]);
        }
    }
}