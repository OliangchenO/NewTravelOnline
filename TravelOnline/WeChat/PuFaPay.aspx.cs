using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeiXinPay;

namespace TravelOnline.WeChat
{
    public partial class PuFaPay : System.Web.UI.Page
    {
        public static string total_fee { get; set; }//参数金额

        public static string OrderID { get; set; }//guid订单号

        public static string out_trade_no { get; set; }//订单号6位数

        public static string Pays { get; set; }

        public static string yfk { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            total_fee = Request.QueryString["total_fee"];
            OrderID = Request.QueryString["OrderID"];
            out_trade_no = Request.QueryString["out_trade_no"];
        }

        private void GoPay()
        {
            string url = "/newpage/AjaxService.aspx?action=PuFaPay";
            Log.Debug("PuFaPay.aspxUrl:", url);
            Response.Redirect(url);
        }
    }
}