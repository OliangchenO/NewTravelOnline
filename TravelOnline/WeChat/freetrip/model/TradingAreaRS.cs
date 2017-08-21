using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TradingAreaRS
    {
        public int id { get; set; }//商圈id
        public string name { get; set; }//商圈名称
        public string flag { get; set; }//商圈标示
        public string destid { get; set; }//目的地id
    }
}