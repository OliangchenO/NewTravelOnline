using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.tour;
using System.Net;

namespace TravelOnline
{
    public partial class index : System.Web.UI.Page
    {
        //public string TopMenuString;
        public string SlidePicHtml, Announcement, Citie, OutBound_Small, InLand_Small, Index_Up_3, Index_Down_3;
        public string LeftHot_OutBound, LeftHot_InLand, Hot_FreeTour, LeftArea_OutBound, LeftArea_InLand, FirendLink, PartnerLink;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (TravelOnline.Class.Common.PublicPageKeyWords.NewPage == "1")
            //{
            //    Server.Transfer("indexnew.aspx");
            //}

            if (Convert.ToString(Cache["SlidePic_Index_New"]) == "") SlidePic.GreateSlideCache("Index_New");
            SlidePicHtml = Convert.ToString(Cache["SlidePic_Index_New"]);

            if (Convert.ToString(Cache["SlidePic_OutBound_Small"]) == "") SlidePic.GreateSlideCache("OutBound_Small");
            OutBound_Small = Convert.ToString(Cache["SlidePic_OutBound_Small"]);

            if (Convert.ToString(Cache["SlidePic_InLand_Small"]) == "") SlidePic.GreateSlideCache("InLand_Small");
            InLand_Small = Convert.ToString(Cache["SlidePic_InLand_Small"]);

            if (Convert.ToString(Cache["Announcement"]) == "") SlidePic.GreateAnnouncementCache();
            Announcement = Convert.ToString(Cache["Announcement"]);

            if (Convert.ToString(Cache["SlidePic_Citie"]) == "") SlidePic.GreateCitieCache();
            Citie = Convert.ToString(Cache["SlidePic_Citie"]);

            if (Convert.ToString(Cache["SlidePic_Index_Up_3"]) == "") SlidePic.GreateThreePicCache("Index_Up_3");
            Index_Up_3 = Convert.ToString(Cache["SlidePic_Index_Up_3"]);

            if (Convert.ToString(Cache["SlidePic_Index_Down_3"]) == "") SlidePic.GreateThreePicCache("Index_Down_3");
            Index_Down_3 = Convert.ToString(Cache["SlidePic_Index_Down_3"]);

            if (Convert.ToString(Cache["SlidePic_Hot_FreeTour"]) == "") SlidePic.GreateTongLanCache("Hot_FreeTour");
            Hot_FreeTour = Convert.ToString(Cache["SlidePic_Hot_FreeTour"]);

            if (Convert.ToString(Cache["SlidePic_LeftHot_OutBound"]) == "") SlidePic.GreateHotTextCache("LeftHot_OutBound");
            LeftHot_OutBound = Convert.ToString(Cache["SlidePic_LeftHot_OutBound"]);

            if (Convert.ToString(Cache["SlidePic_LeftHot_InLand"]) == "") SlidePic.GreateHotTextCache("LeftHot_InLand");
            LeftHot_InLand = Convert.ToString(Cache["SlidePic_LeftHot_InLand"]);

            if (Convert.ToString(Cache["SlidePic_LeftArea_OutBound"]) == "") SlidePic.GreateHotAreaCache("LeftArea_OutBound");
            LeftArea_OutBound = Convert.ToString(Cache["SlidePic_LeftArea_OutBound"]);

            if (Convert.ToString(Cache["SlidePic_LeftArea_InLand"]) == "") SlidePic.GreateHotAreaCache("LeftArea_InLand");
            LeftArea_InLand = Convert.ToString(Cache["SlidePic_LeftArea_InLand"]);

            if (Convert.ToString(Cache["SlidePic_IndexFirendLink"]) == "") SlidePic.FirendLinkCache("IndexFirendLink", "1");
            FirendLink = Convert.ToString(Cache["SlidePic_IndexFirendLink"]);

            if (Convert.ToString(Cache["SlidePic_Partner"]) == "") SlidePic.GreateThreePicCache("Partner");
            PartnerLink = Convert.ToString(Cache["SlidePic_Partner"]);
        }
    }
}