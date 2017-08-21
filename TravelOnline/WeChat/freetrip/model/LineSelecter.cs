using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class LineSelecter
    {
        private int MisLineId { get; set; }//线路编号
        public string LineNameLike { get; set;}//线路名称
        public string linetype { get; set; }//线路类型
        public string lineclass { get; set; }
        public string lineclassname { get; set; }
        public int filter { get; set; }//排序字段： 2：按行程天数asc  3：按行程天数desc  4：按价格asc  5：按价格desc
        public string dest { get; set; }//目的地
        public int pagesize { get; set; }//一页显示数量
        public int pages { get; set; }//显示页数
        public string searchval { get; set; }//搜索内容
    }
}