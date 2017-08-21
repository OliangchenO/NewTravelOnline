using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Common;
using TravelOnline.Destination.Class;
using TravelOnline.Class.Travel;

namespace TravelOnline.Destination
{
    public partial class summary : System.Web.UI.Page
    {
        public string flag, id, pagetitle, cname, ename, dtype, map_x, map_y, map_size, MapScript, MapScriptFile;
        public string HotDestination, HotPlaceView, breadcrumb, NavString, NewsContent, contentname, PlacePicUrl, PlaceList, DestinationLineList, PlaceJournalList, PlaceTitle, PlaceUrl, LineUrl;
        public string SightTitle, SightRecommend, SightList, ViewList, JournalsList, BottomPages, MapContent, BaiduMapName;
        public string ShowPic = "no", SummaryHide = "hide", PlaceHide = "hide", PicHide = "hide", SightHide = "hide", SightRecommendHide = "hide", ViewListHide = "hide", MapHide = "hide", BaiduMapHide = "hide";
        public int ClassLevel = 0, page;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            flag = Request.QueryString["flag"];
            page = MyConvert.ConToInt(Request.QueryString["page"]);
            if (page == 0) page = 1;

            if (id != null)
            {
                LoadInfo();
            }
            else
            {

            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_Destination where id='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["id"].ToString();
                pagetitle = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                cname = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                ename = DS.Tables[0].Rows[0]["Ename"].ToString();
                ClassLevel = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ClassLevel"].ToString());
                dtype = DS.Tables[0].Rows[0]["dtype"].ToString();

                map_x = DS.Tables[0].Rows[0]["map_x"].ToString();
                map_y = DS.Tables[0].Rows[0]["map_y"].ToString();
                map_size = DS.Tables[0].Rows[0]["map_size"].ToString();

                //MisClassId = MyDataBaseComm.getScalar(string.Format("select top 1 MisClassId from OL_ProductType where ProductType='{0}' and Destinationid like '%,{1},%'", dtype, id));
                HotDestination = PlaceClass.DestinationCache("PlaceHotDestination", "", "", 45, "");
                HotPlaceView = PlaceClass.DestinationCache("PlaceHotView", "", "", 45, "");
                breadcrumb = "";
                if (ClassLevel == 2)
                {
                    PlaceTitle = cname + "热门目的地";
                    PlaceUrl = cname + "目的地";
                    breadcrumb += string.Format("<li><a href=\"/place/{0}.html\">{1}</a> <span class=\"divider\">/</span></li>", id, cname);
                }
                if (ClassLevel > 2)
                {
                    PlaceTitle = cname + "热门景点";
                    PlaceUrl = cname + "景点";
                    SqlQueryText = string.Format("select * from OL_Destination where ClassLevel>1 and id in ({0}) order by ClassLevel", DS.Tables[0].Rows[0]["ClassPath"].ToString());
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                        {
                            breadcrumb += string.Format("<li><a href=\"/place/{0}.html\">{1}</a> <span class=\"divider\">/</span></li>", DS1.Tables[0].Rows[i]["id"].ToString(), DS1.Tables[0].Rows[i]["DestinationName"].ToString());
                        }
                    }
                    breadcrumb += string.Format("<li><a href=\"/place/{0}.html\">{1}</a> <span class=\"divider\">/</span></li>", id, cname);
                }

                if (flag == "place")
                {
                    LineUrl = "/" + dtype.ToLower() + "";
                    
                    PlaceHide = "";
                    StringBuilder Strings = new StringBuilder();
                    if (ClassLevel == 2) SqlQueryText = "SELECT top 6 * from OL_ViewPic where desid in (select id from OL_Destination where ClassPath like '" + DS.Tables[0].Rows[0]["ClassPath"].ToString() + "," + id + "%') order by newId()";
                    if (ClassLevel > 2) SqlQueryText = "SELECT top 6 * from OL_ViewPic where desid = '" + id + "' order by newId()";

                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        Strings.Append("<ul>");
                        int ranks = 1;
                        string ThumbSrc = "", PicUrl = "";
                        string[] sArray;
                        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                        {
                            sArray = DS1.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                            PicUrl = string.Format("src=\"/Upload/View/{0}/{1}/M_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                            ThumbSrc = string.Format("thumb=\"/Upload/View/{0}/{1}/S_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                            Strings.Append(string.Format("<li><a href=\"#{0}\"><img {1} {2} alt=\"{3}\" text=\"{3}\" /></a></li>",
                                ranks + i,
                                PicUrl,
                                ThumbSrc,
                                DS1.Tables[0].Rows[i]["picname"].ToString()
                            ));
                        }
                        Strings.Append("</ul>");
                        PlacePicUrl = Strings.ToString();
                        ShowPic = "yes";
                    }

                    if (ClassLevel == 2) PlaceList = PlaceClass.DestinationCache("PlaceList", "", id, 10, "");
                    if (ClassLevel > 2) PlaceList = PlaceClass.DestinationCache("PlaceViewList", "", id, 10, "");
                    DestinationLineList = PlaceClass.DestinationCache("PlaceLineList", dtype, id, 10, "");
                    PlaceJournalList = PlaceClass.DestinationCache("PlaceJournalList", "", id, 10, "");
                }

                if (flag == "summary")
                {
                    contentname = cname + "旅行指南";
                    breadcrumb += "<li class=\"active\">旅行指南</li>";
                }

                if (flag == "traffic")
                {
                    contentname = cname + "交通信息";
                    breadcrumb += "<li class=\"active\">交通信息</li>";
                }

                if (flag == "sight")
                {
                    if (ClassLevel == 2)
                    {
                        contentname = cname + "各地景点";
                        SightTitle = cname + "景点特别推荐";
                        if (page == 1)
                        {
                            SightRecommendHide = "";
                            SightRecommend = PlaceClass.DestinationCache("PlaceSightRecommend", "", DS.Tables[0].Rows[0]["ClassPath"].ToString() + "," + id, 3, "");
                            if (SightRecommend.Length < 5) SightRecommendHide = "hide";
                        } 
                        SightListCreate();
                        SightHide = "";
                    }

                    if (ClassLevel > 2)
                    { 
                        contentname = cname + "景点";
                        SightTitle = cname + "简介";
                        if (page == 1)
                        {
                            SightRecommendHide = "";
                            SightRecommend = MyDataBaseComm.getScalar("select top 1 contents from OL_Summary where typename='简介' and desid='" + id + "'");
                            if (SightRecommend == null) SightRecommendHide = "hide";
                        }
                        SightHide = "";
                        ViewListHide = "";
                        ViewListCreate();

                        //ViewList
                    }
                        
                    breadcrumb += "<li class=\"active\">景点介绍</li>";

                    
                }

                if (flag == "journals")
                {
                    contentname = cname + "游记攻略";
                    breadcrumb += "<li class=\"active\">游记攻略</li>";
                    SightTitle = cname + "最佳旅游时间";
                    if (page == 1)
                    {
                        SightRecommendHide = "";
                        SightRecommend = MyDataBaseComm.getScalar("select top 1 contents from OL_Summary where typename='最佳旅游时间' and desid='" + id + "'");
                        if (SightRecommend == null) SightRecommendHide = "hide";
                    }
                    SightHide = "";
                    JournalListCreate();
                    //SightList
                }
                if (flag == "placemap")
                {
                    contentname = cname + "区域分布";
                    breadcrumb += "<li class=\"active\">地图</li>";
                    MapHide = "";
                    MapContent = MyDataBaseComm.getScalar("select top 1 contents from OL_Summary where typename='区域分布' and desid='" + id + "'");
                    if (MapContent == null) MapHide = "hide";

                    if (map_x.Length > 2)
                    {
                        if (MyConvert.ConToInt(map_size) == 0) map_size = "12";
                        BaiduMapName = cname + "详细地图";
                        BaiduMapHide = "";
                        MapScriptFile = "<script type=\"text/javascript\" src=\"http://api.map.baidu.com/api?v=2.0&ak=1hBsSkjOVNVr7WwRs0tqwMTl\"></script>";
                        MapScript = string.Format("<script type=\"text/javascript\">var map = new BMap.Map(\"allmap\");map.centerAndZoom(new BMap.Point({0}, {1}), {2});map.addControl(new BMap.NavigationControl());map.addControl(new BMap.ScaleControl());map.addControl(new BMap.OverviewMapControl());map.enableScrollWheelZoom();map.addControl(new BMap.MapTypeControl());</script>", map_x, map_y, map_size);
                    }
                    
                }

                if (flag == "summary" || flag == "traffic")
                {
                    SummaryHide = "";
                    LoadSummary();
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        //列表结果及页码导航生成
        protected void JournalListCreate()
        {
            string fieldlist = "id";
            //string condition = " flag='1' and Destinationid like '%," + id + ",%'";
            string condition = string.Format(" flag='1' and (Destinationid like '%,{0},%' or parentid like '%,{0},%')", id);
            string pkey = "id";
            string sort = "";
            string sortname = "id";
            sort = "ASC";
            string tablename = "OL_Journal";
            int pagesize = 10;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            BottomPages = TravelOnline.tour.LineListClass.CreateNewsListBottomPage(rowcount, currpage, PageCount, id, page, "journals/");

            fieldlist = "id,title,contents,inputdate,(select UserName from OL_LoginUser where id=OL_Journal.userid) as NewsUser,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl";
            string SqlQueryText = "";

            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                SightList = PlaceClass.DestinationCache("PlaceAllJournalList", SqlQueryText, id, page, "_" + page.ToString());
            }


        }

        //列表结果及页码导航生成
        protected void SightListCreate()
        {
            string fieldlist = "id";
            string condition = " ParentId='" + id + "'";
            string pkey = "id";
            string sort = "";
            string sortname = "id";
            sort = "ASC";
            string tablename = "OL_Destination";
            int pagesize = 10;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            BottomPages = TravelOnline.tour.LineListClass.CreateNewsListBottomPage(rowcount, currpage, PageCount, id, page, "sight/");

            fieldlist = "id,DestinationName,(select top 1 picurl from OL_ViewPic where desid=OL_Destination.id order by newId()) as picurl";
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                SightList = PlaceClass.DestinationCache("PlaceSightList", SqlQueryText, id, page, "_" + page.ToString());
            }
        }

        //列表结果及页码导航生成
        protected void ViewListCreate()
        {
            string fieldlist = "id";
            string condition = " desid='" + id + "'";
            string pkey = "id";
            string sort = "";
            string sortname = "id";
            sort = "ASC";
            string tablename = "OL_View";
            int pagesize = 12;
            int currpage = page;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            BottomPages = TravelOnline.tour.LineListClass.CreateNewsListBottomPage(rowcount, currpage, PageCount, id, page, "sight/");

            fieldlist = "id,viewname,left(intro,40) as intro,(select top 1 picurl from OL_ViewPic where viewid=OL_View.id order by newId()) as picurl";
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                ViewList = PlaceClass.DestinationCache("PlaceAllViewList", SqlQueryText, id, page, "_" + page.ToString());
            }

        }

        protected void LoadSummary()
        {
            StringBuilder Strings = new StringBuilder();
            NavString = "";
            string SqlQueryText = "";
            if (flag == "summary") SqlQueryText = string.Format("select *,(select sort from InitData where id=OL_Summary.typeid) as sort from OL_Summary where flag='0' and desid='{0}' order by sort", id);
            if (flag == "traffic") SqlQueryText = string.Format("select *,(select sort from InitData where id=OL_Summary.typeid) as sort from OL_Summary where flag='1' and desid='{0}' order by sort", id);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string css = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        css = " class=\"current\"";
                    }
                    else
                    {
                        css = "";
                    }
                    NavString += string.Format("<li{2}><a href=\"#summarys{0}\">{1}</a></li>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString(), css);
                    Strings.Append(string.Format("<DIV class=mtc id=\"summarys{0}\"><STRONG>{1}</STRONG><DIV class=extra></DIV></DIV>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["typename"].ToString()));
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[i]["contents"].ToString()));
                }
                NewsContent = Strings.ToString();
                //NavString += "<li><a href=\"#price_info\">费用描述</a></li>";
            }
        }


    }
}