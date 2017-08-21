using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class LineList
    {
        public int MisLineId { get; set; }//线路id，用于拼装线路地址
        public string LineName { get; set; }//线路名称
        public decimal Price { get; set; }//线路价格
        public string LineFeature { get; set; }//线路特色
        public int Preferences { get; set; }//促销信息
        public string Pics { get; set; }//图片地址
        public int wwwyh { get; set; }//在线支付优惠
        public int LineDays { get; set; }//行程天数
        public List<TradingArea> tradingArea { get; set; }//商圈信息
    }
}