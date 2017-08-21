using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TravelOnline.Class.Travel
{
    public class LineSortCreate
    {
        //生成排序筛选Json
        public static string CreateSort(int sorts)
        {
            StringBuilder Pages = new StringBuilder();
            Pages.Append("<UL class=\\\"item tab\\\">");
            switch (sorts)
            {
                case 1:
                    Pages.Append("<LI><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('0')\\\">推荐</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI class='price curr up'><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('2')\\\">价格</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('3')\\\">旅游天数</A><SPAN></SPAN></LI>");
                    break;
                case 2:
                    Pages.Append("<LI><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('0')\\\">推荐</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI class='price curr down'><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('1')\\\">价格</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('3')\\\">旅游天数</A><SPAN></SPAN></LI>");
                    break;
                case 3:
                    Pages.Append("<LI><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('0')\\\">推荐</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('1')\\\">价格</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI class='price curr up'><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('4')\\\">旅游天数</A><SPAN></SPAN></LI>");
                    break;
                case 4:
                    Pages.Append("<LI><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('0')\\\">推荐</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('1')\\\">价格</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI class='price curr down'><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('3')\\\">旅游天数</A><SPAN></SPAN></LI>");
                    break;
                default:
                    Pages.Append("<LI class=curr><A href=\\\"javascript:void(0);\\\">推荐</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('1')\\\">价格</A><SPAN></SPAN></LI>");
                    Pages.Append("<LI><B></B><A href=\\\"javascript:void(0);\\\" onclick=\\\"SortNow('3')\\\">旅游天数</A><SPAN></SPAN></LI>");
                    break;
            }
            Pages.Append("</UL>");
            Pages.Append("");
            Pages.Append("");
            return Pages.ToString();
        }        
    }
}