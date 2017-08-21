using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Travel;

namespace TravelOnline.Common
{
    public partial class Search : System.Web.UI.Page
    {
        public string keyword, pdate, theme, ThirdType;
        public string LineOnHotSale, LineSendAll, LineDetailSort, LineAreaSort, LineRecommendSort;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            keyword = Server.UrlEncode(Request.QueryString["keyword"]);
            pdate = Request.QueryString["pdate"];
            theme = Request.QueryString["theme"];
            ThirdType = Request.QueryString["ThirdType"];
            LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            LineSendAll = LinePreferences.LineSendAll("Index");
            LineDetailSort = LineDetailRegular.LineDetailSortCreate();
        }
    }
}