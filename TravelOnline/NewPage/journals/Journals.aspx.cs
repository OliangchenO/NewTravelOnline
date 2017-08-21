using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Models;
using TravelOnline.NewPage.Class;

namespace TravelOnline.NewPage.journals
{
    public partial class Journals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public static string Second_Ad_Slide(string linetype, int top)
        {
            var cache = "";
            //var cache = Convert.ToString(HttpContext.Current.Cache["N_S_Journal_Slide_" + linetype]);
            if (string.IsNullOrEmpty(cache))
            {
                List<OL_FlashAD> request = DataClass.Second_Ad_Slide(linetype, top);
                cache = new JavaScriptSerializer().Serialize(request);
                HttpContext.Current.Cache.Insert("N_S_Journal_Slide_" + linetype, cache);
            }
            return cache;
        }
        public static string Second_Line_Sell(string linetype, int top)
        {
            var cache = Convert.ToString(HttpContext.Current.Cache["Second_Line_Sell_" + linetype]);
            if (string.IsNullOrEmpty(cache))
            {
                SpecialTopic request = DataClass.Second_Line_Sell(linetype, top);
                cache = new JavaScriptSerializer().Serialize(request);
                HttpContext.Current.Cache.Insert("Second_Line_Sell_" + linetype, cache);
            }
            return cache;
        }
        public static string Recom_Journals_List(string recomtype, int top)
        {
            var cache = "";
            //var cache = Convert.ToString(HttpContext.Current.Cache["Recom_Journals_List_" + recomtype]);
            if (string.IsNullOrEmpty(cache))
            {
                List<OL_Journal> request = DataClass.Recom_Journals_List(recomtype, top);
                cache = new JavaScriptSerializer().Serialize(request);
                HttpContext.Current.Cache.Insert("Recom_Journals_List_" + recomtype, cache);
            }
            return cache;
        }
    }
}