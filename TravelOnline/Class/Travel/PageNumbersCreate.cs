using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TravelOnline.Class.Travel
{
    public class PageNumbersCreate
    {
        //生成上一页下一页翻页式样Json
        public static string CreateTopPage(int rows, int currpage, int pagecount)
        {
            StringBuilder Pages = new StringBuilder();
            Pages.Append(string.Format("<DIV class=\\\"pagin pagin-m fr\\\"><SPAN class=text>共{0}条线路</SPAN><SPAN class=text>{1}/{2}</SPAN>", rows, currpage, pagecount));

            if (pagecount == 0 || pagecount == 1)
            {
                Pages.Append("<SPAN class=prev-disabled>上一页<B></B></SPAN><SPAN class=next-disabled>下一页<B></B></SPAN>");
            }
            else
            {
                if (currpage == 1)
                {
                    Pages.Append(string.Format("<SPAN class=prev-disabled>上一页<B></B></SPAN><a  class=next href=\\\"javascript:void(0);\\\" onclick=\\\"GoToPage('{0}')\\\">下一页<b></b></a>", currpage + 1));
                }
                else if (currpage == pagecount)
                {
                    Pages.Append(string.Format("<a class=prev href=\\\"javascript:void(0);\\\" onclick=\\\"GoToPage('{0}')\\\">上一页<b></b></a><SPAN class=next-disabled>下一页<B></B></SPAN>", currpage - 1));
                }
                else
                {
                    Pages.Append(string.Format("<a class=prev href=\\\"javascript:void(0);\\\" onclick=\\\"GoToPage('{0}')\\\">上一页<b></b></a><a  class=next href=\\\"javascript:void(0);\\\" onclick=\\\"GoToPage('{1}')\\\">下一页<b></b></a>", currpage - 1, currpage + 1));
                }
            }
            Pages.Append("</DIV>");
            Pages.Append("");
            Pages.Append("");
            return Pages.ToString();
        }

        //生成数字翻页式样Json
        public static string CreateBottomPage(int rows, int currpage, int pagecount)
        {
            StringBuilder Pages = new StringBuilder();
            string Forward, Backwards;
            if (pagecount == 1) return "<div class=\\\"pagin fr\\\"></div>";

            if (currpage == 1)
            {
                Forward = "";
            }
            else
            {
                Forward = string.Format("<a class=prev href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">上一页<b></b></a>", currpage - 1);
            }

            if (currpage == pagecount || pagecount==0)
            {
                Backwards = "";
            }
            else
            {
                Backwards = string.Format("<a class=next href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">下一页<b></b></a>", currpage + 1);
            }

            if (pagecount > 9)
            {
                if (currpage <= 6)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<a href=\\\"javascript:void(0);\\\" class=current>{0}</a>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", i));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", pagecount));
                }
                else if (currpage >= (pagecount - 5))
                {
                    Pages.Append("<a href=\\\"#\\\" onclick=\\\"GoToPage('1')\\\">1<b></b></a>");
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (pagecount - 6); i <= pagecount; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<a href=\\\"javascript:void(0);\\\" class=current>{0}</a>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", i));
                        }
                    }
                }
                else
                {
                    Pages.Append("<a href=\\\"#\\\" onclick=\\\"GoToPage('1')\\\">1<b></b></a>");
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (currpage - 2); i <= (currpage + 2); i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<a href=\\\"javascript:void(0);\\\" class=current>{0}</a>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", i));
                        }

                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", pagecount));
                }
            }
            else
            {
                for (int i = 1; i <= pagecount; i++)
                {
                    if (currpage == i)
                    {
                        Pages.Append(string.Format("<a href=\\\"javascript:void(0);\\\" class=current>{0}</a>", i));
                    }
                    else
                    {
                        Pages.Append(string.Format("<a href=\\\"#\\\" onclick=\\\"GoToPage('{0}')\\\">{0}<b></b></a>", i));
                    }
                }
            }
            return string.Format("<div class=\\\"pagin fr\\\">{0}{1}{2}</div>", Forward, Pages.ToString(), Backwards);
        }
    }
}