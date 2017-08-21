using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TrafficInfo
    {

        public string name { get; set; }
        public string from { get; set; }//出发点
        public string to { get; set; }//目的地
        public string context { get; set; }//交通信息

    }
}