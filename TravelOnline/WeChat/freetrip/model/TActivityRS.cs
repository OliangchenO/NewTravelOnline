using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class TActivityRS
    {
        public int id { get; set; }//活动id
        public string name { get; set; }//活动名称
        public string tradingAreaId { get; set; }//商圈id
    }
}