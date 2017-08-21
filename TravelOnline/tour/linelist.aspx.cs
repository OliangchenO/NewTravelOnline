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
    public partial class linelist : System.Web.UI.Page
    {
        public string linetype;
        public int lineclass,province,city,price,day,topic,reserve1,reserve2,sort,page;
        public string Province_Sort, City_Sort, Price_Sort, Day_Sort, Topic_Sort, Sort_String, ResetSort;
        public string BreadCrumb, TitleString, TopPages, BottomPages, ListResult, texts, hide;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Redirect = "";
            //只传入4个参数，转向到全部参数页面
            if (Request.QueryString["price"] == null)
            {
                Redirect = "yes";
            }
            hide = "hide";
            linetype = Request.QueryString["linetype"];
            lineclass = MyConvert.ConToInt(Request.QueryString["lineclass"]);
            province = MyConvert.ConToInt(Request.QueryString["province"]);
            city = MyConvert.ConToInt(Request.QueryString["city"]);
            price = MyConvert.ConToInt(Request.QueryString["price"]);
            day = MyConvert.ConToInt(Request.QueryString["day"]);
            topic = MyConvert.ConToInt(Request.QueryString["topic"]);
            reserve1 = MyConvert.ConToInt(Request.QueryString["reserve1"]);
            reserve2 = MyConvert.ConToInt(Request.QueryString["reserve2"]);
            sort = MyConvert.ConToInt(Request.QueryString["sort"]);
            page = MyConvert.ConToInt(Request.QueryString["page"]);

            if (page == 0) page = 1;

            string url = string.Format("/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html", linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort);
            if (Redirect == "yes") Response.Redirect(url, true);

            string main_tit = "", lineclass_tit = "", province_tit = "", city_tit = "", day_tit = "", price_tit = "";
            switch (linetype)
            {
                case "outbound":
                    main_tit = "_出境旅游";
                    break;
                case "inland":
                    main_tit = "_国内旅游";
                    break;
                case "freetour":
                    main_tit = "_自由行旅游";
                    break;
                case "cruises":
                    main_tit = "_邮轮旅游";
                    break;
                case "visa":
                    main_tit = "_签证办理";
                    break;
            }

            if (lineclass != 0) lineclass_tit = LineListClass.GetLineClassTitleString(lineclass) + "";//上海到某地旅游价格_上海去某地旅游线路报价_某地旅游团 旅游景点大全_某地风景区线路推荐 
            //上海到某地旅游线路报价_上海去某地旅游价格_某地旅游团 - 上海青旅官网
            switch (linetype)
            {
                case "outbound":
                    TitleString = "上海到" + lineclass_tit + "旅游线路报价_上海去" + lineclass_tit + "旅游价格_" + lineclass_tit + "旅游团";
                    break;
                case "inland":
                    TitleString = "上海到" + lineclass_tit + "旅游价格.线路报价_" + lineclass_tit + "旅游线路推荐";
                    break;
                case "freetour":
                    TitleString = lineclass_tit + "自由行_上海去" + lineclass_tit + "自助游路线推荐.报价";
                    break;
                case "cruises":
                    TitleString = lineclass_tit + "邮轮旅游_上海去" + lineclass_tit + "邮轮旅行";
                    break;
                case "visa":
                    TitleString = "上海代办" + lineclass_tit + "签证_" + lineclass_tit + "个人旅游签证办理";
                    break;
            }

            if (province != 0)
            {
                lineclass_tit = "";
                province_tit = LineListClass.GetLineProvinceTitleString(province) + "";
                //TitleString = "上海到" + province_tit + "旅游价格_上海去" + province_tit + "旅游线路报价_" + province_tit + "旅游团_" + province_tit + "旅游景点大全_" + province_tit + "风景区线路推荐";
                switch (linetype)
                {
                    case "outbound":
                        TitleString = "上海到" + province_tit + "旅游线路报价_上海去" + province_tit + "旅游价格_" + province_tit + "旅游团";
                        break;
                    case "inland":
                        TitleString = "上海到" + province_tit + "旅游价格.线路报价_" + province_tit + "旅游线路推荐";
                        break;
                    case "freetour":
                        TitleString = province_tit + "自由行_上海去" + province_tit + "自助游路线推荐.报价";
                        break;
                    case "cruises":
                        TitleString = province_tit + "邮轮旅游_上海去" + province_tit + "邮轮旅行";
                        break;
                    case "visa":
                        TitleString = "上海代办" + province_tit + "签证_" + province_tit + "个人旅游签证办理";
                        break;
                }
            }
            
            if (city != 0)
            {
                lineclass_tit = "";
                city_tit = LineListClass.GetLineProvinceTitleString(city) + "";
                //TitleString = "上海到" + province_tit + city_tit + "旅游价格_上海去" + province_tit + city_tit + "旅游线路报价_" + province_tit + city_tit + "旅游团_" + province_tit + city_tit + "旅游景点大全_" + province_tit + city_tit + "风景区线路推荐";
                switch (linetype)
                {
                    case "outbound":
                        TitleString = "上海到" + city_tit + "旅游线路报价_上海去" + city_tit + "旅游价格_" + city_tit + "旅游团";
                        break;
                    case "inland":
                        TitleString = "上海到" + city_tit + "旅游价格.线路报价_" + city_tit + "旅游线路推荐";
                        break;
                    case "freetour":
                        TitleString = city_tit + "自由行_上海去" + city_tit + "自助游路线推荐.报价";
                        break;
                    case "cruises":
                        TitleString = city_tit + "邮轮旅游_上海去" + city_tit + "邮轮旅行";
                        break;
                    case "visa":
                        TitleString = "上海代办" + city_tit + "签证_" + city_tit + "个人旅游签证办理";
                        break;
                }
            }
            
            if (day != 0)
            {
                day_tit = LineListClass.GetLineDayTitleString(day) + "";
                TitleString = "上海到" + lineclass_tit + province_tit + city_tit + day_tit + "游价格_上海去" + lineclass_tit + province_tit + city_tit + day_tit + "旅游线路报价_" + lineclass_tit + province_tit + city_tit + day_tit + "旅游团";
            }

            if (price != 0)
            {
                price_tit = LineListClass.GetLinePriceTitleString(price) + "";
                TitleString = "上海到" + lineclass_tit + province_tit + city_tit + day_tit + "" + price_tit + "旅游价格_上海去" + lineclass_tit + province_tit + city_tit + day_tit + "" + price_tit + "旅游线路报价_" + lineclass_tit + province_tit + city_tit + day_tit + "" + price_tit + "旅游团";
            }
           
            //if (linetype == "visa") TitleString = "上海代办" + lineclass_tit + province_tit + city_tit + "签证_上海办理" + lineclass_tit + province_tit + city_tit + "个人旅游签证";
            //TitleAndBreadCrumb();  
            TitleString += main_tit;//LineListClass.CreateTitleString(linetype);
            BreadCrumb = LineListClass.CreateBreadCrumb(linetype, lineclass, province, city, 4);
            SortCreate();
            LineListCreate();
            texts = linetype + "／" + lineclass + "／" + province + "／" + city + "／" + price + "／" + day + "／" + topic + "／" + reserve1 + "／" + reserve2 + "／" + sort;


        }

        //线路列表结果及页码导航生成
        protected void LineListCreate()
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}' and ", DateTime.Today.ToString()));

            if (topic != 0) Strings.Append(string.Format("Topic='{0}' and ", topic));
            if (day > 0 && day < 10) Strings.Append(string.Format("LineDays = '{0}' and ", day));
            if (day == 10) Strings.Append(string.Format("LineDays >= '{0}' and ", day));
            if (lineclass != 0) Strings.Append(string.Format("LineClass='{0}' and ", lineclass));
            if (city != 0)
            {
                Strings.Append(string.Format("Destinationid like '%,{0},%' and ", city));
            }
            else
            {
                if (province != 0) Strings.Append(string.Format("Destinationid like '%,{0},%' and ", province));
            }
            switch (price)
            {
                case 0:
                    break;
                case 1:
                    Strings.Append("Price<500 and ");
                    break;
                case 2:
                    Strings.Append("Price>=500 and Price<1000 and ");
                    break;
                case 3:
                    Strings.Append("Price>=1000 and Price<2000 and ");
                    break;
                case 4:
                    Strings.Append("Price>=2000 and Price<4000 and ");
                    break;
                case 5:
                    Strings.Append("Price>=4000 and Price<6000 and ");
                    break;
                case 6:
                    Strings.Append("Price>=6000 and Price<8000 and ");
                    break;
                case 7:
                    Strings.Append("Price>=8000 and Price<10000 and ");
                    break;
                case 8:
                    Strings.Append("Price>=10000 and ");
                    break;
                default:
                    break;
            }

            string fieldlist = "*";
            Strings.Append("1=1 ");
            //查询条件结束

            int SortType = sort;
            string condition = Strings.ToString();
            string pkey = "id";
            string sortflag = "";
            string sortname = "Price";
            string tablename = "OL_Line";
            int pagesize = 15;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            switch (SortType)
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
                    sortname = "TopEnd desc,EditTime desc";// desc,EditTime desc
                    break;
            }

            TopPages = LineListClass.CreateLineListTopPage(rowcount, currpage, PageCount, linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            BottomPages = LineListClass.CreateLineListBottomPage(rowcount, currpage, PageCount, linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = LineListClass.CreateLineListString(SqlQueryText);
            }
            else
            {
                ListResult = "<SPAN style=\"LINE-HEIGHT: 60px; PADDING-LEFT: 20px; color: #009900; font-size: 14px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>";
            }
            Strings.Clear();
        }

        //筛选条件及排序
        protected void SortCreate()
        {
            //重置排序
            ResetSort = string.Format("/{0}/{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-1.html", linetype, lineclass, "0", "0", "0", "0", "0", "0", "0", "0");
            
            Province_Sort = LineListClass.GetLineListProvinceSort(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            if (province > 0)
            {
                hide = "";
                City_Sort = LineListClass.GetLineListCitySort(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
                if (City_Sort.Length < 50) hide = "hide";
            }
            Price_Sort = LineListClass.GetLineListPriceSort(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            Day_Sort = LineListClass.GetLineListDaySort(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            Topic_Sort = LineListClass.GetLineListTopicSort(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);
            Sort_String = LineListClass.GetLineSortString(linetype, lineclass, province, city, price, day, topic, reserve1, reserve2, sort, page);

            //<a href="" class="curr">全部</a><a href="">澳门</a>
            //<a href="" class="curr">全部</a><a href="">1-499</a><a href="">500-999</a><a href="">1000-1999</a><a href="">2000-3999</a><a href="">4000-5999</a><a href="">6000-7999</a><a href="">8000-9999</a><a href="">10000元以上</a>
            //<a href="" class="curr">全部</a><a href="">一天</a><a href="">两天</a><a href="">三天</a><a href="">四天</a><a href="">五天</a><a href="">六天</a><a href="">七天</a><a href="">八天</a><a href="">九天</a><a href="">十天以上</a>
            //<a href="" class="curr">全部</a><a href="">澳门</a>

            //<dd class="curr"><a href="">推荐</a><b></b></dd>
            //<dd class="price curr down"><b></b><a href="" rel="nofollow">价格</a><b></b></dd>
            //<dd class="price curr up"><a href="" rel="nofollow">旅游天数</a><b></b></dd>

            //<li><a href="/index.aspx">首页</a> <span class="divider">/</span></li>
            //<li><a href="#">Library</a> <span class="divider">/</span></li>
            //<li class="active">Data</li>
        }

    }
}