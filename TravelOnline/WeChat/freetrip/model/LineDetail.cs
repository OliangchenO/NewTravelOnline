using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class LineDetail
    {
        public string Id { get; set; }
        public int MisLineId { get; set; }//线路编号
        public string LineName { get; set; }//线路名称
        public int LineDays { get; set; }//行程天数
        public decimal Price { get; set; }//线路金额
        public string LineType { get; set; }//线路类型
        public string LineFeature { get; set; }//线路特色
        public string DestinationInfo { get; set; }//目的地概览
        public List<TrafficInfo> TrafficInfo { get; set; }//交通信息
        public List<string> hotelInfo { get; set; }//酒店信息
        public DateTime PlanDate { get; set; }
        public int Preferences { get; set; }//线路优惠
        public int Sale { get; set; }
        public DateTime EditTime { get; set; }
        public List<string> Pics { get; set; }//图片地址
        public string Pdates { get; set; }//开班日期
        public int wwwyh { get; set; }
        public int Planid { get; set; }//班次id
        public string BigPics { get; set; }//首图地址
        public string Feature { get; set; }//特色推荐
        public string PriceIn { get; set; }//费用包含
        public string PriceOut { get; set; }//费用不包含
        public string OwnExpense { get; set; }//自费项目
        public string Attentions { get; set; }//注意事项
        public string Shopping { get; set; }//购物商店
        public string Destinationid { get; set; }//目的地id
        public string Destination { get; set; }//目的地名称
        public List<RouteInfo> routeInfoList { get; set; }//行程信息
        public List<TradingArea> tradingArea { get; set; }//商圈信息
        public List<TActivity> activity { get; set; }//活动信息
        public List<TCoupon> coupon { get; set; }//优惠券信息
        public List<TStore> store { get; set; }//商城信息

    }
}