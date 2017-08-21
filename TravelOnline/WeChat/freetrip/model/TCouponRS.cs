using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TCouponRS
    {
        public int id { get; set; }//优惠券id
        public DateTime starDate { get; set; }//开始时间
        public DateTime endDate { get; set; }//结束时间
        public string tradingAreaId { get; set; }//商圈id
    }
}