using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TActivity
    {
        public int id { get; set; }//活动id
        public string name { get; set; }//活动名称
        public string context { get; set; }//活动内容
        public string color { get; set; } //活动颜色
        public string key { get; set; }//活动关键字
        public string tradingAreaId { get; set; }//商圈id
    }
}