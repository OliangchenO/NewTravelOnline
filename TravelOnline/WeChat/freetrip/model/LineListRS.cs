using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class LineListRS
    {
       public List<LineList> lineList { set; get; }
       public int pageCount { set; get; }
       public int currpage { set; get; }
    }
}