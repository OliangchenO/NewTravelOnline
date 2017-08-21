using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TravelOnline.Class.NewClass
{
    public class TopMenu
    {
        public static String GetTopMenuString(string MenuType)
        {
            StringBuilder Strings = new StringBuilder();
            //string m1 = "", m2 = "", m3 = "", m4 = "", m5 = "", m6 = "";
            //switch (MenuType)
            //{
            //    case "Index":
            //        m1 = " class=\"active\"";
            //        break;
            //    case "OutBound":
            //        m2 = " class=\"active\"";
            //        break;
            //    case "InLand":
            //        m3 = " class=\"active\"";
            //        break;
            //    case "FreeTour":
            //        m4 = " class=\"active\"";
            //        break;
            //    case "Cruises":
            //        m5 = " class=\"active\"";
            //        break;
            //    case "Visa":
            //        m6 = " class=\"active\"";
            //        break;
            //    default:
            //        break;
            //}

            Strings.Append("<div id=\"menu\">");
            Strings.Append("<div class=\"container\">");
            Strings.Append("<div class=\"row\">");
            Strings.Append("<div class=\"span12\">");
            Strings.Append("<ul class=\"nav nav-menu\" style=\"margin-bottom: 0px;\">");
            if (MenuType == "Index")
            {
                Strings.Append("<li id=topmenu class=\"active\"><a style=\"WIDTH: 156px;\" href=\"javascript:void(0);\">旅游目的地</a></li>");
            }
            else
            {
                Strings.Append("<li id=topmenu class=\"active\"><a style=\"WIDTH: 100px;\" href=\"javascript:void(0);\">旅游目的地<i style=\"margin-left: 5px;\" class=\"icon-chevron-down icon-white\"></i></a></li>");
                Strings.Append("<li><a href=\"/\">首页</a></li>");
            }
            //Strings.Append("<li" + m1 + "><a href=\"/\">首页</a></li>");
            Strings.Append("<li><a href=\"outbound.aspx\">出国旅游</a></li>");
            Strings.Append("<li><a href=\"inland.aspx\">国内旅游</a></li>");
            Strings.Append("<li><a href=\"freetour.aspx\">自由行</a></li>");
            Strings.Append("<li><a href=\"cruises.aspx\">邮轮旅游</a></li>");
            Strings.Append("<li><a href=\"visa.aspx\">签证</a></li>");
            Strings.Append("</ul>");
            Strings.Append("</div>");
            Strings.Append("</div>");
            Strings.Append("</div>");
            Strings.Append("</div>");
            return Strings.ToString();
        }
    }
}