using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class CommentRS
    {
        public int id { get; set; }//点评id
        public string orderId { get; set; }//订单id
        public string lineId { get; set; }//线路id
        public string userId { get; set; }//用户id
        public string rank { get; set; }//评级
        public string commentStatus { get; set; }//点评状态 UNCOMMENT:未点评，COMMENTED:已点评
        public string auditStatus { get; set; }//审核状态 UNAUDIT:未审核，AUDITED:已审核
        public string startDate { get; set; }//起始时间
        public string endDate { get; set; }//结束时间
        public int pagesize { get; set; }//每页条数
        public int currpage { get; set; }//当前页数
    }
}