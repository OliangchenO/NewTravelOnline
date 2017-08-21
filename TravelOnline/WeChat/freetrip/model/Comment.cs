using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class Comment
    {
        public int id { get; set; }//点评id
        public string orderId { get; set; }//订单id
        public string lineId { get; set; }//线路id
        public string lineName { get; set; }//线路名称
        public DateTime beginDate { get; set; }//出发如期
        public string price { get; set; }//金额
        public string linePic { get; set; }//线路主图
        public string userId { get; set; }//用户id
        public string userName { get; set; }//用户名
        public string rank { get; set; }//评级
        public string title { get; set; }//评价标题
        public string pic { get; set; }//图片地址
        public string context { get; set; }//评价内容
        public string commentStatus { get; set; }//点评状态
        public string auditStatus { get; set; }//审核状态
        public DateTime commentTime { get; set; }//评论时间
    }
}