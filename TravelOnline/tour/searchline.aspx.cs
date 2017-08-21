using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Travel;

namespace TravelOnline.tour
{
    public partial class searchline : System.Web.UI.Page
    {
        public string key, date, dateend, type, day, topic, thirdtype;
        public int sort, page, p1, p2, destination;
        public int rows, pagecount, rowcount;
        public string SortString, firststring;
        public string BreadCrumb, TitleString, TopPages, BottomPages, ListResult, texts, hide;
        protected void Page_Load(object sender, EventArgs e)
        {
            hide = "hide";
            key = Request.QueryString["key"] + "";
            date = Request.QueryString["date"];
            dateend = Request.QueryString["dateend"];
            p1 = MyConvert.ConToInt(Request.QueryString["p1"]);
            p2 = MyConvert.ConToInt(Request.QueryString["p2"]);
            type = Request.QueryString["type"];
            day = Request.QueryString["day"];
            sort = MyConvert.ConToInt(Request.QueryString["sort"]);
            page = MyConvert.ConToInt(Request.QueryString["page"]);
            topic = Request.QueryString["topic"];
            thirdtype = Request.QueryString["thirdtype"];
            destination = MyConvert.ConToInt(Request.QueryString["destination"]);

            if (page == 0) page = 1;

            firststring = "/search.html?t=1";
            if (key != "") firststring += "&key=" + Server.UrlEncode(key);
            if (date != null) firststring += "&date=" + date;
            if (dateend != null) firststring += "&dateend=" + dateend;
            if (p1 != 0) firststring += "&p1=" + p1;
            if (p2 != 0) firststring += "&p2=" + p2;
            if (type != null) firststring += "&type=" + type;
            if (day != null) firststring += "&day=" + day;
            if (topic != null) firststring += "&topic=" + topic;
            if (destination != 0) firststring += "&destination=" + destination;
            if (thirdtype != null) firststring += "&thirdtype=" + thirdtype;
            //if (firststring.Length > 2)
            //{
            //    firststring = firststring.Substring(1, firststring.Length);
            //}
            //firststring = "/search.html?" + firststring;

            TitleString = "旅游产品搜索";
            BreadCrumb = "<li><a href=\"/\">首页</a> <span class=\"divider\">/</span></li><li class=\"active\">旅游产品搜索</li>";
            SortString = LineListClass.CreatePageSortStyle(sort, firststring + "&sort=", "&page=" + page.ToString());
            LineListCreate();
        }

        //线路列表结果及页码导航生成
        protected void LineListCreate()
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}' and ", DateTime.Today.ToString()));
            
            if (key != "")
            {
                string desid = LineListClass.GetLineProvinceIdByName(key);
                string deststring = "";
                if (desid != "") deststring = " or Destinationid like '%," + desid + ",%'";
                
                if (MyConvert.ConToInt(key) != 0)
                {
                    Strings.Append(string.Format("MisLineId='{0}' and ", key));
                }
                else
                {
                    Strings.Append(string.Format("(LineName like '%{0}%' {1}) and ", key, deststring));
                }
            }
            if (destination != 0) Strings.Append(string.Format("Destinationid like '%,{0},%' and ", destination));

            if (p1 != 0) Strings.Append(string.Format("Price>{0} and ", p1));
            if (p2 != 0) Strings.Append(string.Format("Price<{0} and ", p2));

            if (MyConvert.ConToInt(topic) != 0) Strings.Append(string.Format("topic='{0}' and ", topic));
            if (MyConvert.ConToInt(thirdtype) != 0) Strings.Append("tags like '%," + thirdtype + ",%' and ");
            
            //if (date != null) Strings.Append(string.Format("PlanDate>='{0}' and ", date));
            //if (dateend != null) Strings.Append(string.Format("PlanDate>='{0}' and ", date));
            if (date != null && dateend != null)
            {
                Strings.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate>='{0}' and begindate<='{1}') and ", date, dateend));
            }
            else
            {
                if (date != null) Strings.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate>='{0}') and ", date));
                if (dateend != null) Strings.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate<='{0}') and ", dateend));
            }

            if (day != null) Strings.Append(string.Format("LineDays in ({0}) and ", day));

            if (type != null)
            {
                type = type.Replace("1", "'outbound'");
                type = type.Replace("2", "'inland'");
                type = type.Replace("3", "'freetour'");
                type = type.Replace("4", "'cruises'");
                type = type.Replace("5", "'visa'");
                Strings.Append(string.Format("LineType in ({0}) and ", type));
            }

            string fieldlist = "*";
            Strings.Append("1=1 ");
            //查询条件结束

            string condition = Strings.ToString();
            string pkey = "id";
            string sortflag = "";
            string sortname = "Price";
            string tablename = "OL_Line";
            int pagesize = 15;
            rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            pagecount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            switch (sort)
            {
                case 1:
                    sortname = "Price";
                    sortflag = "asc";
                    break;
                case 2:
                    sortname = "Price";
                    sortflag = "desc";
                    break;
                case 3:
                    sortname = "LineDays";
                    sortflag = "asc";
                    break;
                case 4:
                    sortname = "LineDays";
                    sortflag = "desc";
                    break;
                default:
                    sortname = "pv,id desc";// desc,EditTime desc
                    break;
            }

            TopPages = LineListClass.CreateTopPageStyle(rowcount, page, pagecount, firststring + "&sort=" + sort.ToString() + "&page=", "");
            BottomPages = LineListClass.CreateBottomPageStyle(page, pagecount, firststring + "&sort=" + sort.ToString() + "&page=", "");
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, page);
                //ListResult = SqlQueryText;
                ListResult = LineListClass.CreateLineListString(SqlQueryText);
            }
            else
            {
                ListResult = "<SPAN style=\"LINE-HEIGHT: 60px; PADDING-LEFT: 20px; color: #009900; font-size: 14px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>";
            }
            Strings.Clear();
        }

        
    }
}