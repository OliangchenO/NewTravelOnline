using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeChat.freetrip.interfaces;
using TravelOnline.WeChat.freetrip.model;

namespace TravelOnline.WeChat.freetrip
{
    public partial class shopdetail : System.Web.UI.Page
    {
        public string shopDetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tradingAreaId = Request.QueryString["id"].ToString();
            shopDetail = TradingAreaService.getShopDetail(tradingAreaId);
        }
    }
}