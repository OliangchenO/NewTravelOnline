using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.tour;
using System.Configuration;

namespace TravelOnline.NewPage
{
    public partial class search : System.Web.UI.Page
    {
        private int iDisplayLength = 10;
        private string RecordCount = "0";
        private string Sort = "0";
        private string SortField = "";
        private string SortExtend = "";
        private string FieldList = "";
        private StringBuilder sb = new StringBuilder();
        private DataSet DS = new DataSet();
        //seach
        //0 业务类型
        //0 全部 1跟团游、2自由行 3 当地游
        //0 目的地
        //0 城市 
        //0 行程天数 
        //0 旅游主题 
        //0 排序 1天数 2 价格
        //0 全部 1特价优惠
        //1 页码
        //key 搜索关键字
        //p1 p2 价格范围
        //d1 d2 出发日期

        //search？s=0-0-0-0-0-0-0-0-1&key=aa&p1=&p2=
        public int s_lineclass, s_planflag, s_destination,s_city,s_day,s_topic,s_sort,s_sell,s_page;
        public string s_query, s_key, s_d1, s_d2, desid, s_special, s_plantype, des_type="",ThisTitle="",CityName, ContryName, areaid, s_type;
        public int s_p1, s_p2;
        public string nav_tab0, nav_tab1, nav_tab2, nav_tab3;
        public string SortCss, SerchKey;
        public string Tab_PlanType, Tab_Destination, Tab_City, Tab_Topic, Tab_Days;
        public string KeyUrlQuery, KeyUrlQuery1, UrlQuery, UrlQuery1, LineListString, PageCount = "0", current_page = "0", Journal = "";
        public string SerchPageName = "search.html";
        public string BodyId = "outbound";
        public string BreadCrumbName = "出境旅游";
        public string DestinationName = "", SearchName = "";
        public string SearchDesName = "";
        public string BreadCrumb = "<a href=\"/outbound.html\">出境旅游</a>", youluntab="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] Query = new string[8];
            Query[0] = "0";
            Query[1] = "0";
            Query[2] = "0";
            Query[3] = "0";
            Query[4] = "0";
            Query[5] = "0";
            Query[6] = "0";
            Query[7] = "0";

            try
            {
                Query[0] = Request.QueryString["s"].Split("-".ToCharArray())[0];
                Query[1] = Request.QueryString["s"].Split("-".ToCharArray())[1];
                Query[2] = Request.QueryString["s"].Split("-".ToCharArray())[2];
                Query[3] = Request.QueryString["s"].Split("-".ToCharArray())[3];
                Query[4] = Request.QueryString["s"].Split("-".ToCharArray())[4];
                Query[5] = Request.QueryString["s"].Split("-".ToCharArray())[5];
                Query[6] = Request.QueryString["s"].Split("-".ToCharArray())[6];
                Query[7] = Request.QueryString["s"].Split("-".ToCharArray())[7];
            }
            catch
            {}
            s_type = Request.QueryString["type"];
            s_lineclass = MyConvert.ConToInt(Query[0]);//0 业务类型
            s_planflag = MyConvert.ConToInt(Query[1]);//0 全部 1跟团游、2自由行 3 当地游
            s_destination = MyConvert.ConToInt(Query[2]);//0 目的地
            s_city = MyConvert.ConToInt(Query[3]);//0 城市 
            s_day = MyConvert.ConToInt(Query[4]);//0 行程天数
            s_topic = MyConvert.ConToInt(Query[5]);//0 旅游主题 
            s_sort = MyConvert.ConToInt(Query[6]); //0 排序 12天数 34价格
            s_sell = MyConvert.ConToInt(Query[7]);//特价
            //s_page = MyConvert.ConToInt(Query[7]);//1 页码
            s_page = MyConvert.ConToInt(Request.QueryString["p"]);
            if (s_page == 0) s_page = 1;
            current_page = (s_page - 1).ToString();

            s_plantype = Request.QueryString["c"];
            s_special = Request.QueryString["t"];
            s_key = Request.QueryString["key"];
            s_key = Server.UrlDecode(s_key);
            //Server.UrlEncode(Request.QueryString["key"]);
            s_d1 = Request.QueryString["d1"];
            s_d2 = Request.QueryString["d2"];

            s_p1 = MyConvert.ConToInt(Request.QueryString["p1"]);
            s_p2 = MyConvert.ConToInt(Request.QueryString["p2"]);

            //查询条件生成
            CreateSort(s_sort);
            string notshow = ConfigurationManager.AppSettings["NotShow"];
            if (notshow!=null) sb.Append(string.Format("MisLineId != '{0}' and ", notshow));
            if (s_lineclass > 0) sb.Append(string.Format("LineClass='{0}' and ", s_lineclass));
            if (s_planflag > 0) sb.Append(string.Format("PlanType='{0}' and ", s_planflag));
            if (s_city > 0)
            {
                //sb.Append(string.Format("MisLineId in (select lineid from LineDest where destid='{0}') and ", s_city));
                SearchDesName = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id = " + s_city);
                sb.Append(string.Format("Destinationid like '%,{0},%' and ", s_city));
            }
            else
            {
                //if (s_destination > 0) sb.Append(string.Format("MisLineId in (select lineid from LineDest where destid='{0}') and ", s_destination));
                if (s_destination > 0)
                {
                    sb.Append(string.Format("Destinationid like '%,{0},%' and ", s_destination));
                    SearchDesName = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id = " + s_destination);
                }
            }
            if (s_day > 5)
            {
                sb.Append(string.Format("LineDays>='6' and ", ""));
            }
            else
            {
                if (s_day > 0) sb.Append(string.Format("LineDays='{0}' and ", s_day));
            }
            if (s_topic > 0) sb.Append(string.Format("Topic='{0}' and ", s_topic));

            KeyUrlQuery = "";
            //关键字，价格范围，出发日期范围

            if (MyConvert.ConToInt(s_plantype) > 0) //1跟团游 2自由行 3邮轮 4签证
            {
                KeyUrlQuery += "&c=" + s_plantype;
                if (s_plantype == "1") sb.Append("PlanType='1' and ");
                if (s_plantype == "2") sb.Append("PlanType='2' and ");
                if (s_plantype == "3") sb.Append("LineType='Cruises' and ");
                if (s_plantype == "4") sb.Append("LineType='Visa' and ");
            }
            if (s_p1 > 0)
            {
                KeyUrlQuery += "&p1=" + s_p1;
                sb.Append(string.Format("Price>='{0}' and ", s_p1));
            }
            if (s_p2 > 0)
            {
                KeyUrlQuery += "&p2=" + s_p2;
                sb.Append(string.Format("Price<='{0}' and ", s_p2));
            }
            if (s_d1 != null && s_d2 != null)
            {
                KeyUrlQuery += "&d1=" + s_d1;
                KeyUrlQuery += "&d2=" + s_d2;
                KeyUrlQuery1 += "&d1=" + s_d1;
                KeyUrlQuery1 += "&d2=" + s_d2;
                sb.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate>='{0}' and begindate<='{1}') and ", s_d1, s_d2));
            }
            else
            {
                if (s_d1 != null)
                {
                    KeyUrlQuery += "&d1=" + s_d1;
                    KeyUrlQuery1 += "&d1=" + s_d1;
                    sb.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate>='{0}') and ", s_d1));
                }
                if (s_d2 != null)
                {
                    KeyUrlQuery += "&d2=" + s_d2;
                    KeyUrlQuery1 += "&d2=" + s_d2;
                    sb.Append(string.Format("MisLineId in (select lineid from ol_plan where begindate<='{0}') and ", s_d2));
                }
            }

            if (MyConvert.ConToInt(s_special) > 0)
            {
                string SqlQueryText = string.Format("select top 1 id from SpecialLine where Stid='{0}'", s_special);
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    SqlQueryText = string.Format("select * from SpecialTopic where id='{0}'", s_special);
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                        string destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                           if (typeid.Length > 2)
                            {
                                sb.Append(string.Format("lineclass in ({0}) and ", typeid));
                            }
                            if (destid.Length > 3)
                            {
                                sb.Append(string.Format("MisLineId in (select lineid from linedest where destid in ({0}) and ", destid));

                            }
                            KeyUrlQuery += "&t=" + s_special;
                            KeyUrlQuery1 += "&t=" + s_special;
                        }
                    }
                    
                }
                else
                {
                    sb.Append(string.Format("MisLineId in (select lineid from SpecialLine where Stid='{0}') and ", s_special));
                    KeyUrlQuery += "&t=" + s_special;
                    KeyUrlQuery1 += "&t=" + s_special;
                }
            }

            if (s_key != null)
            {
                KeyUrlQuery += "&key=" + Server.UrlEncode(s_key);
                KeyUrlQuery1 += "&key=" + Server.UrlEncode(s_key);
                desid = MyDataBaseComm.getScalar("select id from OL_Destination where DestinationName like '%" + s_key + "%'");
                //查询区域
                string SqlQueryText = String.Format("select MisClassId from OL_ProductType where ProductName like '%" + s_key + "%'");
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                StringBuilder areaString=new StringBuilder();
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                    {
                        areaid = DS1.Tables[0].Rows[i]["MisClassId"].ToString();
                        areaString.Append(string.Format(" LineClass='{0}'", areaid));
                        if (i != DS1.Tables[0].Rows.Count-1)
                        {
                            areaString.Append(" or");
                        }
                    }
                }
                else
                {
                    areaString = null;
                }

                if (areaString != null)
                {
                    if (desid != null)
                    {
                        SearchDesName = s_key;
                        if (s_destination == 0)
                        {
                            sb.Append(string.Format("(Destinationid like '%,{0},%' or ", desid));
                            try
                            {
                                Convert.ToInt32(s_key);
                                sb.Append(string.Format("(LineName like '%{0}%' or MisLineId = '{0}' or {1}) and ", s_key, areaString.ToString()));
                            }
                            catch (Exception)
                            {
                                sb.Append(string.Format("LineName like '%{0}%' or {1}) and ", s_key, areaString.ToString()));
                            }
                        }

                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(s_key);
                            sb.Append(string.Format("(LineName like '%{0}%' or MisLineId = '{0}') and ", s_key));
                        }
                        catch (Exception)
                        {
                            sb.Append(string.Format("LineName like '%{0}%' and ", s_key));
                        }
                    }
                }
                else
                {
                    if (desid != null)
                    {
                        SearchDesName = s_key;
                        if (s_destination == 0)
                        {
                            sb.Append(string.Format("(Destinationid like '%,{0},%' or ", desid));
                            try
                            {
                                Convert.ToInt32(s_key);
                                sb.Append(string.Format("(LineName like '%{0}%' or MisLineId = '{0}')) and ", s_key));
                            }
                            catch (Exception ex)
                            {
                                sb.Append(string.Format("LineName like '%{0}%') and ", s_key));
                            }
                        }

                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(s_key);
                            sb.Append(string.Format("(LineName like '%{0}%' or MisLineId = '{0}') and ", s_key));
                        }
                        catch (Exception ex)
                        {
                            sb.Append(string.Format("LineName like '%{0}%' and ", s_key));
                        }
                    }
                }
                
                Tab_Destination = CreateDestinationTab(s_key, "key");
                //if (s_destination > 0) Tab_City = CreateCityTab(s_destination.ToString());
            }
            else
            {
                if (MyConvert.ConToInt(s_special) == 0)
                {
                    //目的地及城市标签
                    Tab_Destination = CreateDestinationTab(s_lineclass.ToString(), "");
                    if (s_destination > 0) Tab_City = CreateCityTab(s_destination.ToString());
                    if (Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_CityCount_" + s_destination.ToString()]) == "0") Tab_City = "";
                }
            }

            //seach-nav 式样生成
            //Tab_PlanType = CreatePlanCss(s_planflag);

            //排序生成
            SortCss = CreateSortCss(s_sort);

            //天数及主题生成
            Tab_Days = CreateDaysTab();
            Tab_Topic = CreateTopicTab();

            UrlQuery = string.Format("{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}", SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
            UrlQuery1 = string.Format("{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}", SerchPageName, s_page, KeyUrlQuery1, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);

            string daystring = "";
            if (s_day > 0) daystring = s_day + "天";
            if (ContryName == "全部") ContryName = "";
            ThisTitle = "上海到" + ContryName + CityName + daystring + "旅游团线路报价_线路推荐 - 上海中国青年旅行社"; //上海到(目的地)（城市）（天数）旅游团线路报价_线路推荐 - 上海中国青年旅行社
            

            int InLandCount = 0;
            sb.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}'", DateTime.Today.ToString()));//
            //if (KeyUrlQuery == "" && s_lineclass==0) sb.Append(string.Format(" and MisLineId='0'", DateTime.Today.ToString()));
            FieldList = "*,(CASE PlanType WHEN 1 THEN '跟团游' WHEN 2 THEN '自由行' WHEN 3 THEN '周边游' WHEN 4 THEN '签证' ELSE '跟团游' END) AS PlanTypeName";
            FieldList += ",(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = dbo.OL_Line.FirstDestination)) AS DestinationName";
            FieldList += ",(select max(preferAmount) from OL_Preferential where (Lineid=dbo.OL_Line.MisLineid and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate()))) AS preferAmount";
            DS = MyDataBaseComm.getDataSetFromProcedures("OL_Line", "id", FieldList, sb.ToString(), SortField, SortExtend, "2", iDisplayLength, s_page, Sort, out RecordCount);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Journal = TravelOnline.NewPage.Class.CacheClass.CreateLeftJournal(DS.Tables[0].Rows[0]["FirstDestination"].ToString(), 6);
                BodyId = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();
                if (SearchDesName == "")
                {
                    BreadCrumbName = "上海到" + DS.Tables[0].Rows[0]["DestinationName"].ToString() + "旅游";
                }
                else
                {
                    BreadCrumbName = "上海到" + SearchDesName + "旅游";
                }

                if (s_destination==14||"上海".Equals(s_key))
                {
                    BreadCrumbName = "上海旅游";
                }

                DestinationName = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                switch (DS.Tables[0].Rows[0]["LineType"].ToString())
                {
                    case "InLand":
                        BreadCrumb = "<a href=\"/inland.html\">国内旅游</a>";
                        break;
                    case "OutBound":
                        BreadCrumb = "<a href=\"/outbound.html\">出境旅游</a>";
                        break;
                    case "FreeTour":
                        BreadCrumb = "<a href=\"/freetour.html\">自由行</a>";
                        break;
                    case "Cruises":
                        BreadCrumb = "<a href=\"/cruise.html\">邮轮旅游</a>";
                        break;
                    case "Visa":
                        BreadCrumb = "<a href=\"/visa.html\">签证</a>";
                        BreadCrumbName = "签证办理";
                        break;
                    default:
                        BreadCrumb = "<a href=\"/outbound.html\">出境旅游</a>";
                        break;
                }
                
                PageCount = RecordCount; 
                StringBuilder Strings = new StringBuilder();
                LineListString = "";
                string pdates = "";
                string Pics = "/Images/none.gif";
                string styles = "", styles1 = "";
                
                if (s_page == 1 && s_sort == 0 && MyConvert.ConToInt(RecordCount) > 3)
                {
                    
                    Strings.Append(string.Format(@"
                        <div class='searchresult_product01'>
					        <div class='recommend clearfix'>
						        <span class='icon txt'>青旅推荐</span>
					                </div>
                                    <ul class='product_list'>",""
                    ));
                   
                    for (int i = 0; i < 2; i++)
                    {
                        styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "1") styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "2") styles = " class=\"\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "3") styles = " class=\"td\"";
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") styles = " class=\"qz\"";

                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Cruises")
                        {
                            //youluntab = "1";
                            styles = " class=\"yl\"";
                            DS.Tables[0].Rows[i]["PlanTypeName"] = "邮轮";
                        }

                        string tags;
                        tags = "";
                        if (DS.Tables[0].Rows[i]["Tags"].ToString().Length > 5)
                        {
                            try
                            {
                                string[] arr = DS.Tables[0].Rows[i]["Tags"].ToString().Split(',');
                                for (int l = 0; l < arr.Length - 1; l++)
                                {
                                    if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src='/images/" + arr[l].ToString() + ".jpg'>";
                                }
                            }
                            catch
                            { }
                        }

                        StringBuilder specialOffers = new StringBuilder();//优惠信息
                        if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0 || (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0")) || Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                        {
                            specialOffers.Append("<div class='discount'><div id='js_down' class='down'>");

                            if (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0"))
                            {
                                specialOffers.Append(string.Format("<div class='type'><em>优惠</em>网上支付立减{0}元</div>", DS.Tables[0].Rows[i]["wwwyh"].ToString()));
                            }
                            if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0)
                            {
                                specialOffers.Append(string.Format("<div class='type'><em class='cu'>促销</em>早定早优惠立减{0}元</div>", DS.Tables[0].Rows[i]["preferAmount"].ToString()));
                            }
                            string LineId = DS.Tables[0].Rows[i]["MisLineId"].ToString();
                            int price = Convert.ToInt32(DS.Tables[0].Rows[i]["Price"]);
                            if (Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                            {
                                if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && price > 1500)
                                {

                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["specialBanklj"])));
                                }
                                else
                                {
                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["normalBanklj"])));
                                }
                            }
                            specialOffers.Append("</div></div>");
                        }

                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "InLand") InLandCount++;

                        pdates = DS.Tables[0].Rows[i]["Pdates"].ToString();
                        if (pdates.Length > 50) pdates = pdates.Substring(0, 35);

                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length > 10) {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Contains(","))
                            {
                                string[] imgs = DS.Tables[0].Rows[i]["Pics"].ToString().Split(',');
                                Pics = string.Format("http://shql.palmyou.com/file/picture/{0}", imgs[0]);
                            }else
                            {
                                Pics = string.Format("http://shql.palmyou.com/file/picture/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                            }
                        } 
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                        Strings.Append(string.Format(@"
						    <li class='pro_li {8}clearfix'>
							    <div class='pro_img'>
								    <a href='/line/{9}.html' target='_blank' title=''><img style='width:300px;height:160px' src='/images/none.gif' data-original='{0}' alt='{2}' /></a>
							    </div>
							    <div class='pro_tit_text'>
								    <div class='title_pro'>
									    <a href='/line/{9}.html' title='{2}' target='_blank'>{2}</a>
									    <span{7}>{6}</span>
                                        {10}
								    </div>
                                    {11}
								    <p class='comment'>{4}</p>
								    <p class='date'>出发日期：  {5}</p>
								    <a href='/line/{9}.html' target='_blank' class='more'>更多</a>
							    </div>
                                
							    <div class='pro_price'>
								    <div class='price-box'>
									    <span>¥</span>{3}<b>起</b>
								    </div>
								    <a href='/line/{9}.html' target='_blank' class='list_btn'>查看详情</a>
							    </div>
						    </li>",
                            Pics,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            DS.Tables[0].Rows[i]["LineFeature"].ToString(),
                            pdates,
                            DS.Tables[0].Rows[i]["PlanTypeName"].ToString(),
                            styles,
                            styles1,
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            tags,
                            specialOffers.ToString()
                         ));


                        if (i == 0)
                        {
                            styles1 = "mb ";
                            Strings.Append("<div class=\"division\"></div>");
                        }
                    }
                    Strings.Append("</ul></div>");
                    for (int i = 2; i < DS.Tables[0].Rows.Count; i++)
                    {
                        styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "1") styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "2") styles = " class=\"\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "3") styles = " class=\"td\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "4") styles = " class=\"qz\"";

                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Cruises")
                        {
                            styles = " class=\"yl\"";
                            DS.Tables[0].Rows[i]["PlanTypeName"] = "邮轮";
                        }
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "InLand") InLandCount++;

                        pdates = DS.Tables[0].Rows[i]["Pdates"].ToString();
                        if (pdates.Length > 50) pdates = pdates.Substring(0, 35);
                        string tags;
                        tags = "";
                        if (DS.Tables[0].Rows[i]["Tags"].ToString().Length > 5)
                        {
                            try
                            {
                                string[] arr = DS.Tables[0].Rows[i]["Tags"].ToString().Split(',');
                                for (int l = 0; l < arr.Length - 1; l++)
                                {
                                    if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src='/images/" + arr[l].ToString() + ".jpg'>";
                                }
                            }
                            catch
                            { }
                        }

                        StringBuilder specialOffers = new StringBuilder();//优惠信息
                        if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0 || (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0")) || Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                        {
                            specialOffers.Append("<div class='discount'><div id='js_down' class='down'>");

                            if (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0"))
                            {
                                specialOffers.Append(string.Format("<div class='type'><em>优惠</em>网上支付立减{0}元</div>", DS.Tables[0].Rows[i]["wwwyh"].ToString()));
                            }
                            if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0)
                            {
                                specialOffers.Append(string.Format("<div class='type'><em class='cu'>促销</em>早定早优惠立减{0}元</div>", DS.Tables[0].Rows[i]["preferAmount"].ToString()));
                            }
                            string LineId = DS.Tables[0].Rows[i]["MisLineId"].ToString();
                            int price = Convert.ToInt32(DS.Tables[0].Rows[i]["Price"]);
                            if (Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                            {
                                if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && price > 1500)
                                {

                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["specialBanklj"])));
                                }
                                else
                                {
                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["normalBanklj"])));
                                }
                            }
                            specialOffers.Append("</div></div>");
                        }

                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length > 10)
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Contains(","))
                            {
                                string[] imgs = DS.Tables[0].Rows[i]["Pics"].ToString().Split(',');
                                Pics = string.Format("http://shql.palmyou.com/file/picture/{0}", imgs[0]);
                            }
                            else
                            {
                                Pics = string.Format("http://shql.palmyou.com/file/picture/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                            }
                        }
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                        Strings.Append(string.Format(@"
                            <div class='searchresult_product02'>
					            <ul class='product_list'>
						            <li class='pro_li clearfix'>
							            <div class='pro_img'>
								            <a href='/line/{8}.html' target='_blank' title=''><img style='width:300px;height:160px' src='/images/none.gif' data-original='{0}' alt='{2}' /></a>
							            </div>
							            <div class='pro_tit_text'>
								            <div class='title_pro'>
									            <a href='/line/{8}.html' title='{2}' target='_blank'>{2}</a>
									            <span{7}>{6}</span>
                                                {9}
								            </div>
                                            {10}
								            <p class='comment'>{4}</p>
								            <p class='date'>出发日期：  {5}</p>
								            <a href='/line/{8}.html' target='_blank' class='more'>更多</a>
							            </div>
							            <div class='pro_price'>
								            <div class='price-box'>
									            <span>¥</span>{3}<b>起</b>
								            </div>
								            <a href='/line/{8}.html' target='_blank' class='list_btn'>查看详情</a>
							            </div>
						            </li>
					            </ul>
				            </div>",
                            Pics,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            DS.Tables[0].Rows[i]["LineFeature"].ToString(),
                            pdates,
                            DS.Tables[0].Rows[i]["PlanTypeName"].ToString(),
                            styles,
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            tags,
                            specialOffers.ToString()
                         ));
                    }
                }
                else
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "1") styles = " class=\"tg\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "2") styles = " class=\"\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "3") styles = " class=\"td\"";
                        if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "4") styles = " class=\"qz\"";

                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Cruises")
                        {
                            styles = " class=\"yl\"";
                            DS.Tables[0].Rows[i]["PlanTypeName"] = "邮轮";
                        }
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "InLand") InLandCount++;
                        
                        pdates = DS.Tables[0].Rows[i]["Pdates"].ToString();
                        if (pdates.Length > 50) pdates = pdates.Substring(0, 35);

                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                        string tags;
                        tags = "";
                        if (DS.Tables[0].Rows[i]["Tags"].ToString().Length > 5)
                        {
                            try
                            {
                                string[] arr = DS.Tables[0].Rows[i]["Tags"].ToString().Split(',');
                                for (int l = 0; l < arr.Length - 1; l++)
                                {
                                    if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src='/images/" + arr[l].ToString() + ".jpg'>";
                                }
                            }
                            catch
                            { }
                        }
                        StringBuilder specialOffers = new StringBuilder();//优惠信息
                        if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0 || (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0")) || Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                        {
                            specialOffers.Append("<div class='discount'><div id='js_down' class='down'>");

                            if (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0"))
                            {
                                specialOffers.Append(string.Format("<div class='type'><em>优惠</em>网上支付立减{0}元</div>", DS.Tables[0].Rows[i]["wwwyh"].ToString()));
                            }
                            if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0)
                            {
                                specialOffers.Append(string.Format("<div class='type'><em class='cu'>促销</em>早定早优惠立减{0}元</div>", DS.Tables[0].Rows[i]["preferAmount"].ToString()));
                            }
                            string LineId = DS.Tables[0].Rows[i]["MisLineId"].ToString();
                            int price = Convert.ToInt32(DS.Tables[0].Rows[i]["Price"]);
                            if (Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                            {
                                if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && price > 1500)
                                {

                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["specialBanklj"])));
                                }
                                else
                                {
                                    specialOffers.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["normalBanklj"])));
                                }
                            }
                            specialOffers.Append("</div></div>");
                        }

                        Strings.Append(string.Format(@"
                            <div class='searchresult_product02'>
					            <ul class='product_list'>
						            <li class='pro_li clearfix'>
							            <div class='pro_img'>
								            <a href='/line/{8}.html' target='_blank' title=''><img style='width:300px;height:160px' src='/images/none.gif' data-original='{0}' alt='{2}' /></a>
							            </div>
							            <div class='pro_tit_text'>
								            <div class='title_pro'>
									            <a href='/line/{8}.html' title='{2}' target='_blank'>{2}</a>
									            <span{7}>{6}</span>
                                                {9}
								            </div>
                                            {10}
								            <p class='comment'>{4}</p>
								            <p class='date'>出发日期：  {5}</p>
								            <a href='/line/{8}.html' target='_blank' class='more'>更多</a>
							            </div>
							            <div class='pro_price'>
								            <div class='price-box'>
									            <span>¥</span>{3}<b>起</b>
								            </div>
								            <a href='/line/{8}.html' target='_blank' class='list_btn'>查看详情</a>
							            </div>
						            </li>
					            </ul>
				            </div>",
                            Pics,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            DS.Tables[0].Rows[i]["LineFeature"].ToString(),
                            pdates,
                            DS.Tables[0].Rows[i]["PlanTypeName"].ToString(),
                            styles,
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            tags,
                            specialOffers.ToString()
                         ));
                    }
                }
                LineListString = Strings.ToString();
               //Response.Write(RecordCount);
            }
            //seach-nav 式样生成
            SearchName = DestinationName;
            if (s_key != null || s_key != "") SearchName = s_key;
            if (s_lineclass > 0) des_type = "gn";
            if (InLandCount > 0) des_type = "gn";
            if (LineListString == "" || LineListString == null) des_type = "";
            if (youluntab == "") Tab_PlanType = CreatePlanCss(s_planflag);
            
        }

        protected string CreateTopicTab()
        {
            StringBuilder Strings = new StringBuilder();
            if (Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_TopicName"]) == "")
            {
                string db_info = "", db_infoid = "";
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
                HttpContext.Current.Cache.Insert("NewSearch_Line_TopicId", db_info);
                HttpContext.Current.Cache.Insert("NewSearch_Line_TopicName", db_infoid);
            }

            string[] info = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_TopicId"]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_TopicName"]).Split(",".ToCharArray());
            for (int i = 0; i < info.Length; i++)
            {
                if (s_topic.ToString() == infoid[i])
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"color\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}\">{11}</a>", SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, infoid[i], s_sort, s_sell, info[i]));
                }
            }
            return Strings.ToString();
        }


        protected string CreateDaysTab()
        {
            StringBuilder Strings = new StringBuilder();
            string[] info = { "全部", "1日", "2日", "3日", "4日", "5日", "5日以上" };
            string[] infoid = { "0", "1", "2", "3", "4", "5", "6"};
            for (int i = 0; i < info.Length; i++)
            {
                if (s_day == i)
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"color\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}\">{11}</a>", SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, i, s_topic, s_sort, s_sell, info[i]));
                }
            }
            return Strings.ToString();
        }

        protected string CreateDestinationTab(string doflag,string key)
        {
            StringBuilder Strings = new StringBuilder();
            if (Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_DestinationName_" + doflag]) == "")
            {
                string db_info = "", db_infoid = "";
                string SqlQueryText;
                if (key == "key")
                {
                    SqlQueryText = string.Format("select id,DestinationName from OL_Destination where ClassLevel='2' and id in (select destid from View_LineDest where LineName like '%{0}%' group by destid)", doflag);
                }
                else
                {
                    SqlQueryText = string.Format("select id,DestinationName from OL_Destination where ClassLevel='2' and id in (select destid from View_LineDest where LineClass='{0}' group by destid)", doflag);
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
                HttpContext.Current.Cache.Insert("NewSearch_Line_DestinationId_" + doflag, db_info);
                HttpContext.Current.Cache.Insert("NewSearch_Line_DestinationName_" + doflag, db_infoid);
            }

            string[] info = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_DestinationId_" + doflag]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_DestinationName_" + doflag]).Split(",".ToCharArray());

            Strings.Append("<dl class=\"clearfix\"><dt>目 &nbsp;的 &nbsp;地：</dt><dd class='clearfix fl'>");
            for (int i = 0; i < info.Length; i++)
            {
                if (s_destination.ToString() == infoid[i])
                {
                    ContryName = info[i];
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"color\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}\">{11}</a>", SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, infoid[i], "0", s_day, s_topic, s_sort, s_sell, info[i]));
                }
            }
            Strings.Append("</dd></dl>");
            return Strings.ToString();
        }

        protected string CreateCityTab(string doflag)
        {
            StringBuilder Strings = new StringBuilder();
            if (Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_CityName_" + doflag]) == "")
            {
                string db_info = "", db_infoid = "";
                string SqlQueryText = string.Format("select id,DestinationName from OL_Destination where ClassLevel='3' and id in (select destid from View_LineDest where ParentId='{0}' group by destid)", doflag);
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
                HttpContext.Current.Cache.Insert("NewSearch_Line_CityCount_" + doflag, DS.Tables[0].Rows.Count);
                HttpContext.Current.Cache.Insert("NewSearch_Line_CityId_" + doflag, db_info);
                HttpContext.Current.Cache.Insert("NewSearch_Line_CityName_" + doflag, db_infoid);
            }
            
            string[] info = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_CityId_" + doflag]).Split(",".ToCharArray());
            string[] infoid = Convert.ToString(HttpContext.Current.Cache["NewSearch_Line_CityName_" + doflag]).Split(",".ToCharArray());
            for (int i = 0; i < info.Length; i++)
            {
                if (s_city.ToString() == infoid[i])
                {
                    CityName = info[i];
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"javascript:void(0);\" class=\"color\">{0}</a>", info[i]));
                }
                else
                {
                    Strings.Append(string.Format("<a rel=\"nofollow\" href=\"{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}\">{11}</a>", SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, infoid[i], s_day, s_topic, s_sort, s_sell, info[i]));
                }
            }
            return "<dl class=\"clearfix\"><dt>城<span></span>市：</dt><dd class='clearfix fl'>" + Strings.ToString() + "</dd></dl>";
        }

        protected string CreatePlanCss(int doflag)
        {
            string info = "";
            
            if (des_type == "gn")
            {
                switch (doflag)
                {
                    case 1:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li class='current'>跟团游</li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    case 2:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li class='current'>自由行</li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    case 3:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li class='current'>当地游</li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    default:
                        info = string.Format(@"
                        <li class='current'>全部</li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                }
            }
            else
            {
                switch (doflag)
                {
                    case 1:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li class='current'>跟团游</li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
                        <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-4-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>签证</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    case 2:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li class='current'>自由行</li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-4-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>签证</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    case 3:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li class='current'>当地游</li>
					    <li><a href='{0}?p=1{2}&s={3}-4-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>签证</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    case 4:
                        info = string.Format(@"
                        <li><a href='{0}?p=1{2}&s={3}-0-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>全部</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>
					    <li class='current'>签证</li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                    default:
                        info = string.Format(@"
                        <li class='current'>全部</li>
					    <li><a href='{0}?p=1{2}&s={3}-1-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>跟团游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-2-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>自由行</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-3-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>当地游</a></li>
					    <li><a href='{0}?p=1{2}&s={3}-4-{5}-{6}-{7}-{8}-{9}-{10}' rel='nofollow' class=''>签证</a></li>",
                            SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, s_sort, s_sell);
                        break;
                }
            }
            return info;
        }

        protected void CreateSort(int doflag)
        {
            switch (doflag)
            {
                case 1:
                    SortField = "LineDays";
                    Sort = "ASC";
                    break;
                case 2:
                    SortField = "LineDays";
                    Sort = "DESC";
                    break;
                case 3:
                    SortField = "Price";
                    Sort = "ASC";
                    break;
                case 4:
                    SortField = "Price";
                    Sort = "DESC";
                    break;
                default:
                    SortField = "TopEnd";
                    Sort = "DESC";
                    SortExtend = ",EditTime DESC";
                    break;
            }
        }

        protected string CreateSortCss(int doflag)
        {
            string info = "";
            switch (doflag)
            {
                case 1:
                    info = string.Format(@"
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-0-{10}' rel='nofollow' class=''>热门推荐</a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-2-{10}' rel='nofollow' class='switch01 color'><span>天数</span><i class='pos51 pos71'></i></a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-3-{10}' rel='nofollow' id='price_Change' class='switch02 relative-box'><span>价格</span><i class='pos51'></i></a>",
                        SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, "0", s_sell);
                    break;
                case 2:
                    info = string.Format(@"
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-0-{10}' rel='nofollow' class=''>热门推荐</a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-1-{10}' rel='nofollow' class='switch01 color'><span>天数</span><i class='pos51'></i></a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-3-{10}' rel='nofollow' id='price_Change' class='switch02 relative-box'><span>价格</span><i class='pos51'></i></a>",
                        SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, "0", s_sell);
                    break;
                case 3:
                    info = string.Format(@"
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-0-{10}' rel='nofollow' class=''>热门推荐</a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-1-{10}' rel='nofollow' class='switch01'><span>天数</span><i class='pos51'></i></a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-4-{10}' rel='nofollow' id='price_Change' class='switch02 relative-box color'><span>价格</span><i class='pos51 pos71'></i></a>",
                        SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, "0", s_sell);
                    break;
                case 4:
                    info = string.Format(@"
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-0-{10}' rel='nofollow' class=''>热门推荐</a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-1-{10}' rel='nofollow' class='switch01'><span>天数</span><i class='pos51'></i></a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-3-{10}' rel='nofollow' id='price_Change' class='switch02 relative-box color'><span>价格</span><i class='pos51'></i></a>",
                        SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, "0", s_sell);
                    break;
                default:
                    info = string.Format(@"
                        <a href='javascript:;' rel='nofollow' class='color'>热门推荐</a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-1-{10}' rel='nofollow' class='switch01'><span>天数</span><i class='pos51'></i></a>
                        <a href='{0}?p=1{2}&s={3}-{4}-{5}-{6}-{7}-{8}-3-{10}' rel='nofollow' id='price_Change' class='switch02 relative-box'><span>价格</span><i class='pos51'></i></a>",
                        SerchPageName, s_page, KeyUrlQuery, s_lineclass, s_planflag, s_destination, s_city, s_day, s_topic, "0", s_sell);
                    break;
            }
            return info;
        }
    }
}