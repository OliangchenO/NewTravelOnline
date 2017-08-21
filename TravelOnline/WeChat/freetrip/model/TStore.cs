using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TStore
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pic { get; set; }
        public string link { get; set; }
        public string context { get; set; }
        public string tradingAreaId { get; set; }
    }
}