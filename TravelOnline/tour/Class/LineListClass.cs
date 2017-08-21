using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TravelOnline.tour
{
    public class LineListClass
    {
        public static string GetLineProvinceIdByName(string pname)
        {
            if (pname == "") return "";
            string did = "";
            did = MyDataBaseComm.getScalar("select id from OL_Destination where DestinationName like '%" + pname + "%'");
            if (did == null) return "";
            if (Convert.ToString(HttpContext.Current.Cache["GetProvinceIdByName" + pname]) == "")
            {
                HttpContext.Current.Cache.Insert("GetProvinceIdByName" + pname, did);
            }
            return Convert.ToString(HttpContext.Current.Cache["GetProvinceIdByName" + pname]);
        }

        public static string CreatePageSortStyle(int sort, string firststring, string laststring)
        {
            StringBuilder Strings = new StringBuilder();
            if (sort == 0)
            {
                Strings.Append(string.Format("<dd class=\"curr\"><a href=\"javascript:void(0);\">推荐</a><b></b></dd>"));
            }
            else
            {
                Strings.Append(string.Format("<dd><a href=\"{0}{2}{1}\">推荐</a><b></b></dd>", firststring, laststring, "0"));
            }

            if (sort == 1 || sort == 2)
            {
                if (sort == 1) Strings.Append(string.Format("<dd class=\"price curr up\"><a href=\"{0}{2}{1}\">价格</a><b></b></dd>", firststring, laststring, "2"));
                if (sort == 2) Strings.Append(string.Format("<dd class=\"price curr down\"><a href=\"{0}{2}{1}\">价格</a><b></b></dd>", firststring, laststring, "1"));
            }
            else
            {
                Strings.Append(string.Format("<dd><a href=\"{0}{2}{1}\">价格</a><b></b></dd>", firststring, laststring, "1"));
            }

            if (sort == 3 || sort == 4)
            {
                if (sort == 3) Strings.Append(string.Format("<dd class=\"price curr up\"><a href=\"{0}{2}{1}\">旅游天数</a><b></b></dd>", firststring, laststring, "4"));
                if (sort == 4) Strings.Append(string.Format("<dd class=\"price curr down\"><a href=\"{0}{2}{1}\">旅游天数</a><b></b></dd>", firststring, laststring, "3"));
            }
            else
            {
                Strings.Append(string.Format("<dd><a href=\"{0}{2}{1}\">旅游天数</a><b></b></dd>", firststring, laststring, "3"));
            }
            return Strings.ToString();

        }

        public static string CreateTopPageStyle(int rowcount, int currpage, int pagecount, string firststring, string laststring)
        {
            StringBuilder Pages = new StringBuilder();
            if (pagecount == 0 || pagecount == 1)
            {
                Pages.Append("<div class=\"pagin pagin-m\"><span class=\"text\"><i>1</i>/1</span><SPAN class=prev-disabled>上一页</SPAN><SPAN class=next-disabled>下一页</SPAN>");
            }
            else
            {
                if (currpage == 1)
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{0}</i>/{1}</span><SPAN class=prev-disabled>上一页</SPAN><a  class=next href=\"{2}{4}{3}\">下一页</a>", currpage, pagecount, firststring, laststring, currpage + 1));
                }
                else if (currpage == pagecount)
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{0}</i>/{1}</span><a class=prev href=\"{2}{4}{3}\">上一页</a><SPAN class=next-disabled>下一页</SPAN>", currpage, pagecount, firststring, laststring, currpage - 1));
                }
                else
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{0}</i>/{1}</span><a class=prev href=\"{2}{4}{3}\">上一页</a><a class=next href=\"{2}{5}{3}\">下一页</a>", currpage, pagecount, firststring, laststring, currpage - 1, currpage + 1));
                }
            }
            Pages.Append("</DIV>");
            Pages.Append(string.Format("<div class=\"total\"><span>共<strong>{0}</strong>条线路</span></div>", rowcount));
            Pages.Append("<span class=\"clr\"></span>");
            Pages.Append("");
            return Pages.ToString();
        }

        public static string CreateBottomPageStyle(int currpage, int pagecount, string firststring, string laststring)
        {
            StringBuilder Pages = new StringBuilder();
            string Forward, Backwards;
            if (pagecount == 1) return "<div class=\"pagin fr\"></div>";
            Forward = "";
            Backwards = "";
            if (pagecount > 5)
            {
                if (currpage == 1)
                {
                    Forward = "";
                }
                else
                {
                    Forward = string.Format("<a class=prev href=\"{0}{2}{1}\">上一页</a>", firststring, laststring, currpage - 1);
                }

                if (currpage == pagecount || pagecount == 0)
                {
                    Backwards = "";
                }
                else
                {
                    Backwards = string.Format("<a class=next class=prev href=\"{0}{2}{1}\">下一页</a>", firststring, laststring, currpage + 1);
                }
            }


            if (pagecount > 9)
            {
                if (currpage <= 6)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, i));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, pagecount));

                }
                else if (currpage >= (pagecount - 5))
                {
                    Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, "1"));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (pagecount - 6); i <= pagecount; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, i));
                        }
                    }
                }
                else
                {
                    Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, "1"));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (currpage - 2); i <= (currpage + 2); i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, i));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, pagecount));
                }
            }
            else
            {
                for (int i = 1; i <= pagecount; i++)
                {
                    if (currpage == i)
                    {
                        Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                    }
                    else
                    {
                        Pages.Append(string.Format("<a class=prev href=\"{0}{2}{1}\">{2}</a>", firststring, laststring, i));
                    }
                }
            }
            return string.Format("<div class=\"pagin fr\">{0}{1}{2}</div>", Forward, Pages.ToString(), Backwards);
        }



        public static string CreateTitleString(string linetype)
        {
            string TitleString = "";
            switch (linetype)
            {
                case "outbound":
                    TitleString = " 出境旅游";
                    break;
                case "inland":
                    TitleString = " 国内旅游";
                    break;
                case "freetour":
                    TitleString = " 自由行旅游";
                    break;
                case "cruises":
                    TitleString = " 邮轮旅游";
                    break;
                case "visa":
                    TitleString = " 签证办理";
                    break;
            }
            return TitleString;
        }
        public static string CreateBreadCrumb(string linetype, int lineclass, int province, int city, int level)
        {
            string BreadCrumb = "<li><a href=\"/\">首页</a> <span class=\"divider\">/</span></li>";
            switch (linetype)
            {
                case "outbound":
                    BreadCrumb += "<li><a href=\"/outbound.html\">出境旅游</a> <span class=\"divider\">/</span></li>";
                    break;
                case "inland":
                    BreadCrumb += "<li><a href=\"/inland.html\">国内旅游</a> <span class=\"divider\">/</span></li>";
                    break;
                case "freetour":
                    BreadCrumb += "<li><a href=\"/freetour.html\">自由行</a> <span class=\"divider\">/</span></li>";
                    break;
                case "cruises":
                    BreadCrumb += "<li><a href=\"/cruises.html\">邮轮旅游</a> <span class=\"divider\">/</span></li>";
                    break;
                case "visa":
                    BreadCrumb += "<li><a href=\"/visa.html\">签证</a> <span class=\"divider\">/</span></li>";
                    break;
            }

            if (province != 0)
            {
                if (lineclass != 0) BreadCrumb += string.Format("<li><a href=\"/{0}/{1}-0-0-0-0-0-0-0-0-1.html\">{2}</a> <span class=\"divider\">/</span></li>", linetype, lineclass, GetLineClassTitleString(lineclass));
                if (city != 0)
                {
                    BreadCrumb += string.Format("<li><a href=\"/{0}/{1}-{2}-0-0-0-0-0-0-0-1.html\">{3}</a> <span class=\"divider\">/</span></li>", linetype, lineclass, province, GetLineProvinceTitleString(province));
                    BreadCrumb += string.Format("<li class=\"active\">{0}</li>", GetLineProvinceTitleString(city));
                }
                else
                {
                    if (level == 3) BreadCrumb += string.Format("<li><a href=\"/{0}/{1}-{2}-0-0-0-0-0-0-0-1.html\">{3}</a> <span class=\"divider\">/</span></li>", linetype, lineclass, province, GetLineProvinceTitleString(province));
                    if (level == 4) BreadCrumb += string.Format("<li class=\"active\">{0}</li>", GetLineProvinceTitleString(province));
                }
            }
            else
            {
                //if (lineclass != 0) BreadCrumb += string.Format("<li class=\"active\">{0}</li>", GetLineClassTitleString(lineclass));
                if (lineclass != 0) BreadCrumb += string.Format("<li><a href=\"/{0}/{1}-0-0-0-0-0-0-0-0-1.html\">{2}</a> <span class=\"divider\">/</span></li>", linetype, lineclass, GetLineClassTitleString(lineclass));
            }
            return BreadCrumb;

        }
        public static string CreateLineListString(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            string tags, mp_pic;

            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    string DetailInfos = "";
                    string Pics = "/images/none.gif";
                    string pdates = DS.Tables[0].Rows[i]["Pdates"].ToString();
                    string url = string.Format("/line/{0}.html", DS.Tables[0].Rows[i]["MisLineId"]);

                    if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa")
                    {
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length > 5) Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                        }
                        catch
                        { }
                        if (pdates.Length > 3) DetailInfos = string.Format("<SPAN>有效期：</SPAN>{0}&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>停留时间：</SPAN>{1}&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>工作日：</SPAN>{2}", pdates.Split("$".ToCharArray())[0], pdates.Split("$".ToCharArray())[1], pdates.Split("$".ToCharArray())[2]);
                    }
                    else
                    {
                        if (pdates.Length > 50) pdates = pdates.Substring(0, 35);
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        }
                        catch
                        { }
                        DetailInfos = string.Format("<span class=\"label label-warning\">{0}天</span>出发班期：{1}...", DS.Tables[0].Rows[i]["LineDays"].ToString(), pdates.Replace(",", ", "));
                    }
                    mp_pic = "";
                    tags = "";
                    if (DS.Tables[0].Rows[i]["Tags"].ToString().Length > 5)
                    {
                        try
                        {
                            string[] arr = DS.Tables[0].Rows[i]["Tags"].ToString().Split(',');
                            for (int l = 0; l < arr.Length - 1; l++)
                            {
                                if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src=\"/images/" + arr[l].ToString() + ".jpg\">";
                            }
                        }
                        catch
                        {
                            //SaveErrorToLog(exception2.Message, exception2.Message);
                        }
                    }
                    if (DS.Tables[0].Rows[i]["famous"].ToString().Length > 0) mp_pic = string.Format("<img src=\"/img/mp_{0}.png\" title=\"青旅名牌产品\" class=\"mp_pic\">", DS.Tables[0].Rows[i]["famous"].ToString());
                
                    //<dl>
                    //    <dt><a href="/OutBound/227/12132.html" target="_blank"><img onerror="this.src='/Images/none.gif'" src="/Images/Views/200905/M_0905271614621.jpg" width="100"></a></dt>
                    //    <dd>
                    //        <div style="width:860px;height: 80px;">
                    //            <div style="FLOAT: left;width:710px;height: 80px;">
                    //                <div class="p-name" style=""><a href="/OutBound/231/6765.html" target="_blank">畅游日本~东京箱根京都大阪六日精选游畅游日本</a><span class="label label-success">热卖</span><span class="label label-important">纯玩</span><span class="badge">1</span></div>
                    //                <div>日本常规行程，适合第一次去日本的客人</div>
                    //                <div><span class="label label-warning">6天</span>出发班期：10/16, 10/23, 10/30...</div>
                    //            </div>
                    //            <div style="padding-left:5px;FLOAT: right;width:130px;height: 80px;border-left:1px solid #DDDDDD;">
                    //                <div class="p-price">￥5480<em>起</em></div>
                    //                <div class="pd">编号：12322</div>
                    //                <div class="pd"><a href="#" class="btn btn-small btn-success">收藏</a> <a href="#" class="btn btn-small btn-warning">预定</a></div>
                    //            </div>
                    //        </div>
                    //    </dd>
                    //</dl>

                    //<span class=\"label label-success\">热卖</span><span class=\"label label-important\">纯玩</span><span class=\"badge\">1</span>
                    Strings.Append(string.Format("<dl><dt><a href=\"{0}\" target=\"_blank\"><img src=\"/images/none.gif\" data-original=\"{1}\" alt=\"{2}\"></a></dt>", url, Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                    Strings.Append("<dd><div class=\"line\"><div class=\"linename\">");
                    Strings.Append(string.Format("<div class=\"p-name\"><a href=\"{0}\" target=\"_blank\">{1}</a>{2} {3}</div>", url, DS.Tables[0].Rows[i]["LineName"].ToString(), mp_pic, tags));
                    Strings.Append(string.Format("<div class=\"p-feature\">{0}&nbsp;</div><div>{1}</div></div>", DS.Tables[0].Rows[i]["LineFeature"].ToString(), DetailInfos));

                    Strings.Append("<div class=\"lineprice\">");
                    Strings.Append(string.Format("<div class=\"p-price\">￥{0}<em>起</em></div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    Strings.Append(string.Format("<div class=\"pd\">编号：{0}</div>", DS.Tables[0].Rows[i]["MisLineId"]));
                    Strings.Append(string.Format("<div class=\"pd\"><a href=\"javascript:\" onclick=\"favorite('{1}')\" class=\"btn btn-small btn-success\">收藏</a> <a href=\"{0}\" target=\"_blank\" class=\"btn btn-small btn-warning\">预定</a></div>", url, DS.Tables[0].Rows[i]["MisLineId"]));
                    Strings.Append("</div></div>");
                    Strings.Append("</dd></dl>");
                }
            }
            else
            {
                Strings.Append("<SPAN style=\"LINE-HEIGHT: 60px; PADDING-LEFT: 20px; color: #009900; font-size: 14px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>");
            }

            return Strings.ToString();
        }


        //生成数字翻页式样Json
        public static string CreateLineListBottomPage(int rows, int currpage, int pagecount, string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            StringBuilder Pages = new StringBuilder();
            string Forward, Backwards;
            if (pagecount == 1) return "<div class=\"pagin fr\"></div>";
            Forward = "";
            Backwards = "";
            if (pagecount > 5)
            {
                if (currpage == 1)
                {
                    Forward = "";
                }
                else
                {
                    Forward = string.Format("<a class=prev href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">上一页</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page - 1);
                }

                if (currpage == pagecount || pagecount == 0)
                {
                    Backwards = "";
                }
                else
                {
                    Backwards = string.Format("<a class=next href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">下一页</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page + 1);
                }
            }
            

            if (pagecount > 9)
            {
                if (currpage <= 6)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, i));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, pagecount));
                        
                }
                else if (currpage >= (pagecount - 5))
                {
                    Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, "1"));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (pagecount - 6); i <= pagecount; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, i));
                        }
                    }
                }
                else
                {
                    Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, "1"));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (currpage - 2); i <= (currpage + 2); i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, i));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, pagecount));
                }
            }
            else
            {
                for (int i = 1; i <= pagecount; i++)
                {
                    if (currpage == i)
                    {
                        Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                    }
                    else
                    {
                        Pages.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">{10}</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, i));
                    }
                }
            }
            return string.Format("<div class=\"pagin fr\">{0}{1}{2}</div>", Forward, Pages.ToString(), Backwards);
        }

        //生成上一页下一页翻页式样
        public static string CreateLineListTopPage(int rows, int currpage, int pagecount, string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            //<div class="pagin pagin-m">
            //    <span class="text"><i>1</i>/6</span>
            //    <span class="prev-disabled">上一页</span>
            //    <a class="next" href="">下一页</a>
            //</div>
            
            StringBuilder Pages = new StringBuilder();
            if (pagecount == 0 || pagecount == 1)
            {
                Pages.Append("<div class=\"pagin pagin-m\"><span class=\"text\"><i>1</i>/1</span><SPAN class=prev-disabled>上一页</SPAN><SPAN class=next-disabled>下一页</SPAN>");
            }
            else
            {
                if (currpage == 1)
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{11}</i>/{12}</span><SPAN class=prev-disabled>上一页</SPAN><a  class=next href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">下一页</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page + 1, currpage, pagecount));
                }
                else if (currpage == pagecount)
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{11}</i>/{12}</span><a class=prev href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">上一页</a><SPAN class=next-disabled>下一页</SPAN>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page - 1, currpage, pagecount));
                }
                else
                {
                    Pages.Append(string.Format("<div class=\"pagin pagin-m\"><span class=\"text\"><i>{12}</i>/{13}</span><a class=prev href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}.html\">上一页</a><a  class=next href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{11}.html\">下一页</a>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page - 1, page + 1, currpage, pagecount));
                }
            }
            Pages.Append("</DIV>");

            //<div class="total"><span>共<strong>201</strong>条线路</span></div>
            //<span class="clr"></span>
            Pages.Append(string.Format("<div class=\"total\"><span>共<strong>{0}</strong>条线路</span></div>", rows));
            Pages.Append("<span class=\"clr\"></span>");
            Pages.Append("");
            return Pages.ToString();
        }

        public static string GetLineClassTitleString(int id)
        {
            if (Convert.ToString(HttpContext.Current.Cache["LineClassTitleString" + id]) == "")
            {
                HttpContext.Current.Cache.Insert("LineClassTitleString" + id, MyDataBaseComm.getScalar("select ProductName from OL_ProductType where MisClassId='" + id + "'"));
            }
            return Convert.ToString(HttpContext.Current.Cache["LineClassTitleString" + id]);
        }

        public static string GetLineDayTitleString(int id)
        {
            string[] info = { "全部", "一日", "两日", "三日", "四日", "五日", "六日", "七日", "八日", "九日", "十日以上" };
            return info[id];
        }

        public static string GetLinePriceTitleString(int id)
        {
            string[] info = { "全部", "1-499元", "500-999元", "1000-1999元", "2000-3999元", "4000-5999元", "6000-7999元", "8000-9999元", "10000元以上" };
            return info[id];
        }
        
        public static string GetLineProvinceTitleString(int id)
        {
            if (id == 0) return "";
            if (Convert.ToString(HttpContext.Current.Cache["LineProvinceTitleString" + id]) == "")
            {
                HttpContext.Current.Cache.Insert("LineProvinceTitleString" + id, MyDataBaseComm.getScalar("select DestinationName from OL_Destination where Id='" + id + "'").ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["LineProvinceTitleString" + id]);
        }

        public static string GetLineListProvinceSort(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            if (Convert.ToString(HttpContext.Current.Cache["LineListProvinceSort" + lineclass]) == "")
            {
                string Destinationid = MyDataBaseComm.getScalar("select top 1 '0' + Destinationid + '0' from OL_ProductType where MisClassId='" + lineclass + "'");
                string db_info = "", db_infoid = "";
                string SqlQueryText = "select id,DestinationName from View_Destination where linecount>0 and ClassLevel='2' and id in (" + Destinationid + ")";
                if (Destinationid.Length < 4)
                {
                    SqlQueryText = "select id,DestinationName from View_Destination where linecount>0 and ClassLevel='2' and id in (select FirstDestination from OL_Line where Sale='0' and Price>0 and PlanDate>'" + DateTime.Today.ToString() + "' and lineclass='" + lineclass + "' group by FirstDestination)";
                }
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                
                db_info += "全部";
                db_infoid += "0";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    db_info += "," + DS.Tables[0].Rows[i]["DestinationName"].ToString();
                    db_infoid += "," + DS.Tables[0].Rows[i]["id"].ToString();
                }
                HttpContext.Current.Cache.Insert("LineListProvinceSort" + lineclass, db_info);
                HttpContext.Current.Cache.Insert("LineListProvinceSortId" + lineclass, db_infoid);
            }

            StringBuilder Strings = new StringBuilder();
            string[] info = Convert.ToString(HttpContext.Current.Cache["LineListProvinceSort" + lineclass]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["LineListProvinceSortId" + lineclass]).Split(",".ToCharArray());
            for (int i = 0; i < info.Length; i++)
            {
                if (province == MyConvert.ConToInt(infoid[i]))
                {
                    Strings.Append(string.Format("<a href=\"javascript:void(0);\" class=\"curr\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html\">{10}</a>", linetype, lineclass, infoid[i], "0", price, day, topic, reserve1, reserve2, sort, info[i]));
                }
            }
            return Strings.ToString();
        }

        public static string GetLineListCitySort(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            if (Convert.ToString(HttpContext.Current.Cache["LineListCitySort" + province]) == "")
            {
                string db_info = "", db_infoid = "";
                string SqlQueryText = "select id,DestinationName from View_Destination where linecount>0 and ClassLevel='3' and ParentId='" + province + "'";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                db_info += "全部";
                db_infoid += "0";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    db_info += "," + DS.Tables[0].Rows[i]["DestinationName"].ToString();
                    db_infoid += "," + DS.Tables[0].Rows[i]["id"].ToString();
                }
                HttpContext.Current.Cache.Insert("LineListCitySort" + province, db_info);
                HttpContext.Current.Cache.Insert("LineListCitySortId" + province, db_infoid);
            }

            StringBuilder Strings = new StringBuilder();
            string[] info = Convert.ToString(HttpContext.Current.Cache["LineListCitySort" + province]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["LineListCitySortId" + province]).Split(",".ToCharArray());
            for (int i = 0; i < info.Length; i++)
            {
                if (city == MyConvert.ConToInt(infoid[i]))
                {
                    Strings.Append(string.Format("<a href=\"javascript:void(0);\" class=\"curr\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html\">{10}</a>", linetype, lineclass, province, infoid[i], price, day, topic, reserve1, reserve2, sort, info[i]));
                }
            }
            return Strings.ToString();
        }


        public static string GetLineListPriceSort(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            StringBuilder Strings = new StringBuilder();
            string[] info = { "全部", "1-499", "500-999", "1000-1999", "2000-3999", "4000-5999", "6000-7999", "8000-9999", "10000元以上" };
            string[] infoid = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            for (int i = 0; i < info.Length; i++)
            {
                if (price==i)
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"curr\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html\">{10}</a>", linetype, lineclass, province, city, i, day, topic, reserve1, reserve2, sort, info[i]));
                }
            }
            return Strings.ToString();
        }

        public static string GetLineListDaySort(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            StringBuilder Strings = new StringBuilder();
            string[] info = { "全部", "一天", "两天", "三天", "四天", "五天", "六天", "七天", "八天", "九天", "十天以上" };
            string[] infoid = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            for (int i = 0; i < info.Length; i++)
            {
                if (day == i)
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"curr\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html\">{10}</a>", linetype, lineclass, province, city, price, i, topic, reserve1, reserve2, sort, info[i]));
                }
            }
            return Strings.ToString();
        }

        public static string GetLineListTopicSort(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            if (Convert.ToString(HttpContext.Current.Cache["LineListTopicSort"]) == "")
            {
                string db_info="", db_infoid="";
                string SqlQueryText = "select AdName,MisClassId from OL_FlashAD where AdFlag='Topic' order by AdSort";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                db_info += "全部";
                db_infoid += "0";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    db_info += "," + DS.Tables[0].Rows[i]["AdName"].ToString();
                    db_infoid += "," + DS.Tables[0].Rows[i]["MisClassId"].ToString();
                }
                HttpContext.Current.Cache.Insert("LineListTopicSort", db_info);
                HttpContext.Current.Cache.Insert("LineListTopicSortId", db_infoid);
            }

            StringBuilder Strings = new StringBuilder();
            string[] info = Convert.ToString(HttpContext.Current.Cache["LineListTopicSort"]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["LineListTopicSortId"]).Split(",".ToCharArray());
            for (int i = 0; i < info.Length; i++)
            {
                if (topic == MyConvert.ConToInt(infoid[i]))
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"curr\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html\">{10}</a>", linetype, lineclass, province, city, price, day, infoid[i], reserve1, reserve2, sort, info[i]));
                }
            }
            return Strings.ToString();
        }

        public static string GetLineSortString(string linetype, int lineclass, int province, int city, int price, int day, int topic, int reserve1, int reserve2, int sort, int page)
        {
            StringBuilder Strings = new StringBuilder();
            if (sort == 0)
            {
                Strings.Append(string.Format("<dd class=\"curr\"><a rel=\"nofollow\" href=\"javascript:void(0);\">推荐</a><b></b></dd>"));
            }
            else
            {
                Strings.Append(string.Format("<dd><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-0-{9}.html\">推荐</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
            }

            if (sort == 1 || sort == 2)
            {
                if (sort == 1) Strings.Append(string.Format("<dd class=\"price curr up\"><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-2-{9}.html\">价格</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
                if (sort == 2) Strings.Append(string.Format("<dd class=\"price curr down\"><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-1-{9}.html\">价格</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
            }
            else
            {
                Strings.Append(string.Format("<dd><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-1-{9}.html\">价格</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
            }

            if (sort == 3 || sort == 4)
            {
                if (sort == 3) Strings.Append(string.Format("<dd class=\"price curr up\"><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-4-{9}.html\">旅游天数</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
                if (sort == 4) Strings.Append(string.Format("<dd class=\"price curr down\"><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-3-{9}.html\">旅游天数</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
            }
            else
            {
                Strings.Append(string.Format("<dd><a rel=\"nofollow\" href=\"/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-3-{9}.html\">旅游天数</a><b></b></dd>", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, page));
            }
            return Strings.ToString();
        }

        //生成数字翻页式样Json
        public static string CreateNewsListBottomPage(int rows, int currpage, int pagecount, string infotype, int page,string pagetype)
        {
            StringBuilder Pages = new StringBuilder();
            string Forward, Backwards;
            if (pagecount == 1) return "<div class=\"pagin fr\"></div>";
            Forward = "";
            Backwards = "";
            if (pagecount > 5)
            {
                if (currpage == 1)
                {
                    Forward = "";
                }
                else
                {
                    Forward = string.Format("<a class=prev href=\"/{2}{0}/{1}.html\">上一页</a>", infotype, page - 1, pagetype);
                }

                if (currpage == pagecount || pagecount == 0)
                {
                    Backwards = "";
                }
                else
                {
                    Backwards = string.Format("<a class=next href=\"/{2}{0}/{1}.html\">下一页</a>", infotype, page + 1, pagetype);
                }
            }


            if (pagecount > 9)
            {
                if (currpage <= 6)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, i, pagetype));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, pagecount, pagetype));

                }
                else if (currpage >= (pagecount - 5))
                {
                    Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, "1", pagetype));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (pagecount - 6); i <= pagecount; i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, i, pagetype));
                        }
                    }
                }
                else
                {
                    Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, "1", pagetype));
                    Pages.Append("<span class=text>…</span>");
                    for (int i = (currpage - 2); i <= (currpage + 2); i++)
                    {
                        if (currpage == i)
                        {
                            Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                        }
                        else
                        {
                            Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, i, pagetype));
                        }
                    }
                    Pages.Append("<span class=text>…</span>");
                    Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, pagecount, pagetype));
                }
            }
            else
            {
                for (int i = 1; i <= pagecount; i++)
                {
                    if (currpage == i)
                    {
                        Pages.Append(string.Format("<span class=\"current\">{0}</span>", i));
                    }
                    else
                    {
                        Pages.Append(string.Format("<a href=\"/{2}{0}/{1}.html\">{1}</a>", infotype, i, pagetype));
                    }
                }
            }
            return string.Format("<div class=\"pagin fr\">{0}{1}{2}</div>", Forward, Pages.ToString(), Backwards);
        }
    }
}