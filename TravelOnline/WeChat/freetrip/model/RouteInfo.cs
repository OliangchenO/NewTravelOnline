using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class RouteInfo
    {
        public string daterank { set; get; }//天数
        public string route { set; get; }//行程
        public string rname { set; get; }//酒店信息
        public string bus { set; get; }//交通
        public string dinner { set; get; }//用餐
        public string room { set; get; }//住宿
        public string pic { set; get; }//图片
    }
}