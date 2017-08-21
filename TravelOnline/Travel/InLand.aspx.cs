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
    public partial class InLand : System.Web.UI.Page
    {
        public string InLandAffiche, InLandProductHtml, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            InLandAffiche = CreateAfficheHtml.CreateAffiche("InLand");

            if (Convert.ToString(Cache["InLandProductHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("InLand");
            }
            InLandProductHtml = Convert.ToString(Cache["InLandProductHtml"]);

            LineOnHotSale = LinePreferences.LineOnHotSale("InLand");

            //if (Convert.ToString(Cache["LineSendAllInLand"]) == "")
            //{
            //    LinePreferences.LineSendAll("InLand");
            //}
            //LineSendAll = Convert.ToString(Cache["LineSendAllInLand"]);
        }
    }
}