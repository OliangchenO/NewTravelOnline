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
    public partial class OutBound : System.Web.UI.Page
    {
        public string OutBoundAffiche, OutBoundProductHtml, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            OutBoundAffiche = CreateAfficheHtml.CreateAffiche("OutBound");

            if (Convert.ToString(Cache["OutBoundProductHtml"]) == "")
            {
                CreateProductClass.ProductClassCreateNow("OutBound");
            }
            OutBoundProductHtml = Convert.ToString(Cache["OutBoundProductHtml"]);

            LineOnHotSale = LinePreferences.LineOnHotSale("OutBound");

            //if (Convert.ToString(Cache["LineSendAllOutBound"]) == "")
            //{
            //    LinePreferences.LineSendAll("OutBound");
            //}
            //LineSendAll = Convert.ToString(Cache["LineSendAllOutBound"]);
        }
    }
}