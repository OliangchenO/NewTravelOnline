using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TCoupon
    {
        public int id { get; set; }//优惠券id
        public DateTime starDate { get; set; }//开始时间
        public DateTime endDate { get; set; }//结束时间
        public string context { get; set; }//优惠券内容
        public string barCode { get; set; }//优惠券条形码
        public string barCodeImg { get; set; }//优惠券条形码图形
        public string tradingAreaId { get; set; }//商圈id

    }
}