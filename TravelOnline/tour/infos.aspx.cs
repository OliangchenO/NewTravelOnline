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
    public partial class infos : System.Web.UI.Page
    {
        public string pagetype, infotype, id, serchtype, Recommend, RecommendList, JournalLineList, JournalLineRecommend, NewsTitle, NewsTime, NewsContent, NewsUser, Keywords;
        int page;
        public string BreadCrumb, TitleString, TopPages, BottomPages, ListResult, JournaRecomm, province;
        public string CookieViewHide = "hide", JournaListHide = "hide", JournaHide = "hide", NewsHide = "hide", NewsListHide = "hide", ServiceHide = "hide", NewsContentHide = "hide";
        protected void Page_Load(object sender, EventArgs e)
        {
            pagetype = Request.QueryString["pagetype"];
            infotype = Request.QueryString["infotype"];
            id = Request.QueryString["id"];
            page = MyConvert.ConToInt(Request.QueryString["page"]);

            if (page == 0) page = 1;
            Keywords = TravelOnline.Class.Common.PublicPageKeyWords.OutBoundKeywords;

            BreadCrumb = "<li><a href=\"/\">首页</a> <span class=\"divider\">/</span></li>";
            if (pagetype == "info")
            {
                GetBreadCrumb(infotype.Replace("_news", ""));
                BreadCrumb += "<li class=\"active\">快讯列表</li>";
                serchtype = infotype.Replace("_news", "");
                Recommend = serchtype;
                if (serchtype == "index") Recommend = "outbound";
                NewsHide = "";
                NewsListHide = "";
                NewsListCreate();
                RecommendList = TravelOnline.tour.LineRecommend.LineRecommendCache("LineListRecommend_" + Recommend, "NewRecom", "3", "SmallPic", Recommend, "", "", "", 4);
            }

            if (pagetype == "service")
            {
                GetBreadCrumb(infotype);
                ServiceInfo(serchtype);
                ServiceHide = "";
                NewsContentHide = "";
                NewsTitle = TitleString;
            }

            if (pagetype == "showinfo")
            {
                LoadNewsInfo();
                NewsHide = "";
                NewsContentHide = "";
            }

            if (pagetype == "journallist")
            {
                infotype = "journallist";
                BreadCrumb += "<li class=\"active\">游记和攻略</li>";
                TitleString = "游记和攻略";
                JournaListHide = "";
                NewsListHide = "";
                CookieViewHide = "";
                Recommend = "outbound";
                RecommendList = TravelOnline.tour.LineRecommend.LineRecommendCache("LineListRecommend_" + Recommend, "NewRecom", "3", "SmallPic", Recommend, "", "", "", 4);
                JournalLineRecommend = TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("LeftLinePreferences", "OutBound", "", 3);
                JournalListCreate();
            }

            if (pagetype == "showjournal")
            {
                infotype = "journallist";
                BreadCrumb += "<li><a href=\"/journallist.html\">游记和攻略</a> <span class=\"divider\">/</span></li>";
                LoadJournalInfo();
                NewsContentHide = "";
                CookieViewHide = "";
                JournaHide = "";
                JournaRecomm = TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("JournalDetailRecomm", province, id, 4); //游记推荐
                if (JournaRecomm == "") JournaRecomm = TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("JournalDetailRecomm", "all", id, 4);
                if (JournaRecomm == "") JournaHide = "hide";
            }

        }

        protected void GetBreadCrumb(string bctype)
        {
            switch (bctype)//
            {
                case "index":
                    BreadCrumb += "<li><a href=\"/info/index_news.html\">青旅快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "青旅快讯";
                    break;
                case "outbound":
                    BreadCrumb += "<li><a href=\"/info/outbound_news.html\">出国旅游快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "出国旅游快讯";
                    break;
                case "inland":
                    BreadCrumb += "<li><a href=\"/info/inland_news.html\">国内旅游快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "国内旅游快讯";
                    break;
                case "freetour":
                    BreadCrumb += "<li><a href=\"/info/freetour_news.html\">自由行快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "自由行快讯";
                    break;
                case "cruises":
                    BreadCrumb += "<li><a href=\"/info/cruises_news.html\">邮轮旅游快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "邮轮旅游快讯";
                    break;
                case "visa":
                    BreadCrumb += "<li><a href=\"/info/visa_news.html\">签证快讯</a> <span class=\"divider\">/</span></li>";
                    TitleString = "签证快讯";
                    break;
                case "aboutus":
                    BreadCrumb += "<li><a href=\"/service/aboutus.html\">关于我们</a> <span class=\"divider\">/</span></li>";
                    TitleString = "关于我们";
                    serchtype = "Service7";
                    break;
                case "contactus":
                    BreadCrumb += "<li><a href=\"/service/contactus.html\">联系我们</a> <span class=\"divider\">/</span></li>";
                    TitleString = "联系我们";
                    serchtype = "Service1";
                    break;
                case "joinus":
                    BreadCrumb += "<li><a href=\"/service/joinus.html\">人才招聘</a> <span class=\"divider\">/</span></li>";
                    TitleString = "人才招聘";
                    serchtype = "Service2";
                    break;
                case "partner":
                    BreadCrumb += "<li><a href=\"/service/partner.html\">同行合作</a> <span class=\"divider\">/</span></li>";
                    TitleString = "同行合作";
                    serchtype = "Service3";
                    break;
                case "advertising":
                    BreadCrumb += "<li><a href=\"/service/advertising.html\">广告服务</a> <span class=\"divider\">/</span></li>";
                    TitleString = "广告服务";
                    serchtype = "Service4";
                    break;
            }
        }

        protected void ServiceInfo(string types)
        {
            string SqlQueryText = string.Format("select * from OL_Affiche where AfficheType='{0}' order by EditTime desc", types);
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {

                    Strings.Append(string.Format("<DIV class=mtc><STRONG>{0}</STRONG><DIV class=extra></DIV></DIV>", DS.Tables[0].Rows[i]["AfficheName"].ToString()));
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[i]["AfficheContent"].ToString()));
                }
                NewsContent = Strings.ToString();
            }
            else
            {
                NewsContent = "没有找到任何内容！";
            }
        }

        protected void LoadNewsInfo()
        {
            string SqlQueryText = string.Format("select top 1 * from OL_Affiche where Id='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                NewsTitle = DS.Tables[0].Rows[0]["AfficheName"].ToString();
                NewsTime = DS.Tables[0].Rows[0]["EditTime"].ToString();
                NewsContent = DS.Tables[0].Rows[0]["AfficheContent"].ToString();
                GetBreadCrumb(DS.Tables[0].Rows[0]["AfficheType"].ToString().ToLower());
                BreadCrumb += "<li class=\"active\">" + NewsTitle + "</li>";
                TitleString = NewsTitle;
            }
            else
            {
                NewsContent = "没有找到任何内容！";
            }
        }

        protected void LoadJournalInfo()
        {
            string SqlQueryText = "";
            if (Request.QueryString["flag"] == "audit")
            {
                SqlQueryText = string.Format("select top 1 *,(select UserName from OL_LoginUser where id=OL_Journal.userid) as NewsUser from OL_Journal where Id='{0}'", id);
            }
            else
            {
                SqlQueryText = string.Format("select top 1 *,(select UserName from OL_LoginUser where id=OL_Journal.userid) as NewsUser from OL_Journal where flag='1' and Id='{0}'", id);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                JournalLineList = TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("LeftJournalLinePreferences", DS.Tables[0].Rows[0]["FirstDestination"].ToString(), DS.Tables[0].Rows[0]["FirstDestination"].ToString(), 6);
                if (DS.Tables[0].Rows[0]["seo"].ToString().Length > 2)
                {
                    Keywords = DS.Tables[0].Rows[0]["seo"].ToString();
                    TitleString = DS.Tables[0].Rows[0]["seo"].ToString();
                }
                else
                {
                    Keywords = DS.Tables[0].Rows[0]["title"].ToString();
                    TitleString = DS.Tables[0].Rows[0]["title"].ToString();
                }
                NewsTitle = DS.Tables[0].Rows[0]["title"].ToString();
                NewsTime = DS.Tables[0].Rows[0]["inputdate"].ToString();
                NewsContent = DS.Tables[0].Rows[0]["seocontent"].ToString();
                NewsUser = DS.Tables[0].Rows[0]["NewsUser"].ToString();
                NewsTime = "作者：" + NewsUser + " &nbsp;&nbsp;&nbsp;&nbsp;发表时间：" + NewsTime;
                BreadCrumb += "<li class=\"active\">" + NewsTitle + "</li>";
                province = DS.Tables[0].Rows[0]["FirstDestination"].ToString();
            }
            else
            {
                NewsContent = "没有找到任何内容！";
            }
        }

        //列表结果及页码导航生成
        protected void NewsListCreate()
        {

            string fieldlist = "id,AfficheName,EditTime";
            string condition = " AfficheFlag='1' and AfficheType='" + serchtype + "'";
            string pkey = "id";
            string sort = "";
            string sortname = "EditTime";
            sort = "desc";
            string tablename = "OL_Affiche";
            int pagesize = 20;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            BottomPages = LineListClass.CreateNewsListBottomPage(rowcount, currpage, PageCount, infotype, page, "info/");

            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                ListResult = LineListPageSerch.GetNewsInfoPageList(SqlQueryText);
            }
            else
            {
                ListResult = "<SPAN style=\"LINE-HEIGHT: 60px; PADDING-LEFT: 20px; color: #009900; font-size: 14px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>";
            }
        }

        //列表结果及页码导航生成
        protected void JournalListCreate()
        {

            //string fieldlist = "id,linename as title,EditTime as inputdate";
            //string condition = " 1=1";
            //string pkey = "id";
            //string sort = "";
            //string sortname = "EditTime";
            //sort = "desc";
            //string tablename = "OL_Line";

            string fieldlist = "id,title,inputdate";
            string condition = " Flag='1'";
            string pkey = "id";
            string sort = "";
            string sortname = "inputdate";
            sort = "desc";
            string tablename = "OL_Journal";
            int pagesize = 20;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            BottomPages = LineListClass.CreateNewsListBottomPage(rowcount, currpage, PageCount, infotype, page, "");

            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                ListResult = LineListPageSerch.GetNewJournalPageList(SqlQueryText);
            }
            else
            {
                ListResult = "<SPAN style=\"LINE-HEIGHT: 60px; PADDING-LEFT: 20px; color: #009900; font-size: 14px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>";
            }
        }


    }
}