using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using TravelOnline.Class.Travel;

namespace TravelOnline
{
    public partial class Default : System.Web.UI.Page
    {
        public string IndexAffiche, IndexTopicTravel, IndexOutBoundHtml, IndexInLandHtml, LineOnHotSale,FriendLink;
        public string IndexFreeTour, IndexCruises, IndexVisa;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/", true);

            IndexAffiche = CreateAfficheHtml.CreateAffiche("Index");

            if (Convert.ToString(Cache["IndexTopicTravelHtml"]) == "")
            {
                CreateAdScript.AdScriptCreateNow("Topic");
            }
            IndexTopicTravel = Convert.ToString(Cache["IndexTopicTravelHtml"]);

            if (Convert.ToString(Cache["IndexOutBoundHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("OutBound");
            }
            IndexOutBoundHtml = Convert.ToString(Cache["IndexOutBoundHtml"]);

            if (Convert.ToString(Cache["IndexInLandHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("InLand");
            }
            IndexInLandHtml = Convert.ToString(Cache["IndexInLandHtml"]);


            FriendLink = CreateAfficheHtml.CreateFriendLink();


            LineOnHotSale = LinePreferences.LineOnHotSale("Index");

            IndexFreeTour = CreateProductClass.IndexFreeTourCreate();
            IndexCruises = CreateProductClass.IndexCruisesCreate();
            IndexVisa = CreateProductClass.IndexVisaCreate();
            //IndexVisa = TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle;
        }
    }
}