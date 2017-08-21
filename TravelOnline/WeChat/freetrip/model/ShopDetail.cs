using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class ShopDetail
    {
        public List<TradingArea> tradingArea { get; set; }//商圈信息
        public List<TActivity> activity { get; set; }//活动信息
        public List<TCoupon> coupon { get; set; }//优惠券信息
        public List<TStore> store { get; set; }//店铺信息
    }
}