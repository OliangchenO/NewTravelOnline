using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TradingArea
    {
        public int id { get; set; }//商圈id
        public string name { get; set; }//商圈名称
        public string flag { get; set; }//商圈标示
        public string detail { get; set; }//商圈详情
        public string pic { get; set; }//商圈图片
        public string destid { get; set; }//目的地id
        public string destname { get; set; }//目的地名称
    }
}