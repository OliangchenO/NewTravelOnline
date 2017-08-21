using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TravelOnline.NewPage.Class
{
    public class CacheClass
    {
        public static string Index_Ad_Slide()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_Index_Slide"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 5 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort,EditTime desc", "N_Index_Slide");
                StringBuilder String1 = new StringBuilder();
                StringBuilder String2 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    String1.Append("<div id=\"indexTopbanner\" class=\"banner\">");
                    String1.Append("<ul class=\"clearfix\">");
                    String2.Append("<div id=\"bannerGoods\" class=\"bannertxt absolute-box\">");
                    String2.Append("<ul class=\"clearfix\">");
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<li{0}><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></li>", styles, DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                        if (i == DS.Tables[0].Rows.Count-1)
                        {
                            String2.Append(string.Format("<li class=\"no-border\"><p>{1}</p></li>", "", DS.Tables[0].Rows[i]["AdName"].ToString()));
                        }
                        else
                        {
                            String2.Append(string.Format("<li{0}><p>{1}</p></li>", styles, DS.Tables[0].Rows[i]["AdName"].ToString()));
                        }
                    }
                    String1.Append("</ul>");
                    String1.Append("</div>");
                    String2.Append("</ul>");
                    String2.Append("</div>");
                }
                infos = String1.ToString() + String2.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Ad_Index_Slide", infos);
            }
            return infos;
        }

        public static string Index_Ad_Banner()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_Index_Banner"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 3 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort,EditTime desc", "N_Index_Banner");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count >0)
                {
                    //String1.Append("<div class='move-ad'>");
                    //String1.Append("<ul id='js_move_ad' class='clearfix'>");
                    //String1.Append(string.Format("<li class='cur'><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></li>", "", DS.Tables[0].Rows[0]["AdPageUrl"].ToString(), DS.Tables[0].Rows[0]["AdPicUrl"].ToString(), DS.Tables[0].Rows[0]["AdName"].ToString()));
                    //String1.Append(string.Format("<li><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></li>", "", DS.Tables[0].Rows[1]["AdPageUrl"].ToString(), DS.Tables[0].Rows[1]["AdPicUrl"].ToString(), DS.Tables[0].Rows[1]["AdName"].ToString()));
                    //String1.Append("</ul>");
                    //String1.Append("<ol id='js_move_ad_num' class='clearfix'><li class='circle act'>1</li><li class='circle'>2</li></ol>");
                    //String1.Append("</div>");
                    String1.Append(string.Format("<div class='under-ad'><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></div>", "", DS.Tables[0].Rows[0]["AdPageUrl"].ToString(), DS.Tables[0].Rows[0]["AdPicUrl"].ToString(), DS.Tables[0].Rows[0]["AdName"].ToString()));
                    String1.Append(string.Format("<div class='under-ad'><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></div>", "", DS.Tables[0].Rows[1]["AdPageUrl"].ToString(), DS.Tables[0].Rows[1]["AdPicUrl"].ToString(), DS.Tables[0].Rows[1]["AdName"].ToString()));
                    String1.Append(string.Format("<div class='under-ad mrt'><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></div>", "", DS.Tables[0].Rows[2]["AdPageUrl"].ToString(), DS.Tables[0].Rows[2]["AdPicUrl"].ToString(), DS.Tables[0].Rows[2]["AdName"].ToString()));

                    //string styles = " class=\"mb\"";
                    //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    //{
                    //    if (i != 0) styles = "";
                    //    String1.Append(string.Format("<a{0} href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a>", styles, DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                    //}
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Ad_Index_Banner", infos);
            }
            return infos;
        }

        public static string Index_Ad_Season()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_Index_Season"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort,EditTime desc", "N_Index_Season");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    String1.Append(string.Format("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" title=\"{2}\"></a>", DS.Tables[0].Rows[0]["AdPageUrl"].ToString(), DS.Tables[0].Rows[0]["AdPicUrl"].ToString(), DS.Tables[0].Rows[0]["AdName"].ToString()));
                    
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Ad_Index_Season", infos);
            }
            return infos;
        }

        public static string Index_News()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_Index_News"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 4 * from OL_Affiche where affichetype='{0}' order by EditTime desc", "N_News");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string styles = " class=\"colorf00\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<p><a{0} href=\"javascript\" title=\"{2}\"><span>{3} .</span>{2}</a></p>", styles, DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["AfficheName"].ToString(), i+1));
                    }
                }
                infos = "<div class=\"notice\"><h2 class=\"fb\">最新资讯</h2>" + String1.ToString() + "</div>";
                HttpContext.Current.Cache.Insert("NewPage_Ad_Index_News", infos);
            }
            return infos;
        }

        public static string Index_Line_Sell()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_ST_Index_Sell"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", "Index_Sell");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    String1.Append(string.Format("<h2><span>特价精选</span><a href=\"{0}\" target=\"_blank\">更多特价产品>></a></h2><div class=\"empty\"></div>", DS.Tables[0].Rows[0]["Url"].ToString()));
                    SqlQueryText = string.Format("select top 4 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);

                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 4 * FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }


                    String1.Append("<div id=\"topsaleImg\" class=\"product_list\"><ul class=\"clearfix\">");
                    String1.Append("");

                    string Pics = "/Images/none.gif";
                    string styles = "";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                        if (i == DS.Tables[0].Rows.Count-1) styles = " class=\"no-mr\"";
                        String1.Append(string.Format("<li{0}><dl class=\"clearfix\"><dt class=\"relative-box\">", styles));
                        String1.Append(string.Format("<a href=\"/line/{3}.html\" target=\"_blank\"><img src=\"{0}\" alt=\"{2}\" title=\"{2}\"><div class=\"salemask\"></div></a>", Pics, DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        String1.Append(string.Format("<p><span title=\"{0}\">{0}</span></p></dt>", DS.Tables[0].Rows[i]["LineName"].ToString()));
                        String1.Append("<div class=\"bg clearfix\"><dd class=\"fl cost\">");
                        String1.Append(string.Format("<p class=\"fb\"><span>¥</span>{0}<i class=\"relative-box\">起</i></p></dd>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        String1.Append(string.Format("<dd class=\"fl buying\"><a href=\"/line/{1}.html\" target=\"_blank\">立即抢购</a></dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        String1.Append("</div></dl></li>");
                    }
                    String1.Append("</ul></div>");
                    String1.Append("");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_ST_Index_Sell", infos);
            }
            return infos;
        }

        public static string Index_Line_Season()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_ST_Index_Season"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", "Index_Season");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    SqlQueryText = string.Format("select top 12 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 12 * FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }
                    String1.Append("<li class=\"clearfix\">");
                    String1.Append("");

                    string Pics = "/Images/none.gif";
                    string styles = "";
                    string planstyle = "";
                    int ii = 0;
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                        styles = "";
                        if (ii == 1) styles = " class=\"mr\"";
                        switch (DS.Tables[0].Rows[i]["PlanType"].ToString())
                        {
                            case "1":
                                planstyle = "blue";
                                break;
                            case "2":
                                planstyle = "orange";
                                break;
                            case "3":
                                planstyle = "green";
                                break;
                            case "4":
                                planstyle = "red";
                                break;
                            default:
                                planstyle = "";
                                break;
                        }
                        String1.Append(string.Format("<dl{0}>", styles));
                        String1.Append(string.Format("<dt><a href=\"/line/{3}.html\" title=\"{2}\" target=\"_blank\"><img src=\"{0}\" alt=\"{2}\"></a></dt>", Pics, DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        String1.Append(string.Format("<dd class=\"tit\"><a href=\"/line/{2}.html\" title=\"{1}\" target=\"_blank\">{1}</a></dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        String1.Append(string.Format("<p>{0}</p>", DS.Tables[0].Rows[i]["LineFeature"].ToString()));

                        String1.Append("<div class=\"msg clearfix\">");
                        String1.Append(string.Format("<div class=\"theme {0}\">{1}</div>", planstyle, DS.Tables[0].Rows[i]["PlanTypeName"].ToString()));
                        String1.Append(string.Format("<div class=\"price\"><span>¥</span>{0} <b>起</b></div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        String1.Append("</div></dl>");
                        ii++;
                        if (ii == 3)
                        {
                            ii = 0;
                            if (i != DS.Tables[0].Rows.Count - 1) String1.Append("<li class=\"clearfix\">");
                        }
                    }
                    String1.Append("</li>");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_ST_Index_Season", infos);
            }
            return infos;
        }

        public static string Index_Line_Tab(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Index_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select Cname from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    //<li class="current">推荐</li>
                    //<li>港澳台</li>
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<li{0}>{1}</li>", styles, DS.Tables[0].Rows[i]["Cname"].ToString()));
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Index_" + linetype, infos);
            }
            return infos;
        }

        public static string Index_Line_List(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Index_Line_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    

                    string styles = " current";
                    string Pics = "/Images/none.gif";
                    for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                    {
                        typeid = DS1.Tables[0].Rows[ii]["LineType"].ToString();
                        destid = DS1.Tables[0].Rows[ii]["Destinationid"].ToString();
                        SqlQueryText = string.Format("select top 10 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS1.Tables[0].Rows[ii]["id"].ToString());
                        DataSet DS = new DataSet();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);

                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top 10 * FROM View_SpecialLineTemp where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                DS.Clear();
                                DS = MyDataBaseComm.getDataSet(sqlstr);
                            }
                        }

                        if (ii != 0) styles = "";
                        String1.Append(string.Format("<ul id=\"cjProduct_img\" class=\"product-list{0} fl clearfix\">", styles));
                        string style1 = "";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (i == 3)
                            {
                                style1 = " class=\"no-mrr\"";
                            }
                            else
                            {
                                style1 = "";
                            }

                            string PlanType = "跟团游";
                            if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "2") PlanType = "自由行";
                            if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "3") PlanType = "当地游";
                            if (DS.Tables[0].Rows[i]["PlanType"].ToString() == "4") PlanType = "签证";

                            if (i < 4)
                            {
                                Pics = "/images/none.gif";
                                if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                                if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                                String1.Append(string.Format("<li{0}><dl><dt class=\"relative-box\">", style1));
                                String1.Append(string.Format("<a href=\"/line/{3}.html\" target=\"_blank\"><img src=\"{0}\" alt=\"{2}\" title=\"{2}\"></a>", Pics, DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                                String1.Append("<p class=\"absolute-box fl clearfix\">");
                                String1.Append(string.Format("<span class=\"fl\" title=\"{0}\">{0}</span>", PlanType)); //DS.Tables[0].Rows[i]["Destination"].ToString()
                                String1.Append(string.Format("<b class=\"cost rl\"><span>¥</span>{0}<i>起</i></b></p>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                                String1.Append(string.Format("</dt><dd><a href=\"/line/{2}.html\" target=\"_blank\">{1}</a></dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                                String1.Append("</dl></li>");
                            }
                            else
                            {
                                if (i == 4) String1.Append("<div class=\"line fl\"></div><div class=\"details fl clearfix relative-box\"><div class=\"details-l fl\">");
                                if (i == 7) String1.Append("</div><div class=\"details-m fl\"></div><div class=\"details-l fl\">");
                                String1.Append(string.Format("<a class=\"column clearfix\" href=\"/line/{2}.html\" title=\"\" target=\"_blank\"><span class=\"fl\">{1}</span>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                                String1.Append(string.Format("<p class=\"cost rl\"><b>¥</b>{0}<i class=\"relative-box\">起</i></p></a>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                            }

                            //if (DS.Tables[0].Rows.Count == 4) String1.Append("<div class=\"line fl\"></div><div class=\"details fl clearfix relative-box\"><div class=\"details-l fl\">");
                            
                        }
                        if (DS.Tables[0].Rows.Count <= 4)
                        {
                            String1.Append("<div class=\"line fl\"></div><div class=\"details fl clearfix relative-box\"><div class=\"details-l fl\">");
                        }
                        if (DS.Tables[0].Rows.Count == 0) String1.Append("<div><div>");
                        if (DS1.Tables[0].Rows[ii]["Url"].ToString().Length > 5)
                        {
                            String1.Append(string.Format("</div><a class=\"more hide absolute-box\" href=\"{0}\" target=\"_blank\">更多{1}线路>></a></div>", DS1.Tables[0].Rows[ii]["Url"].ToString(), DS1.Tables[0].Rows[ii]["Cname"].ToString()));
                        }
                        else
                        {
                            String1.Append("</div></div>");
                        }
                        String1.Append("</ul>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Index_Line_" + linetype, infos);
            }
            return infos;

        }

        public static string Index_OtherTab(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Index_Other_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select Cname from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string styles = " class=\"current action\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<dl{0}>{1}</dl>", styles, DS.Tables[0].Rows[i]["Cname"].ToString()));
                    }
                    String1.Append("</li>");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Index_Other_" + linetype, infos);
            }
            return infos;
        }

        public static string Index_OtherLine_List(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Index_OtherLine_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    string styles = " action";
                    string Pics = "/Images/none.gif";
                    for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                    {
                        typeid = DS1.Tables[0].Rows[ii]["LineType"].ToString();
                        destid = DS1.Tables[0].Rows[ii]["Destinationid"].ToString();
                        SqlQueryText = string.Format("select top 4 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS1.Tables[0].Rows[ii]["id"].ToString());
                        DataSet DS = new DataSet();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);

                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top 4 * FROM View_SpecialLineTemp where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                DS.Clear();
                                DS = MyDataBaseComm.getDataSet(sqlstr);
                            }
                        }

                        if (ii != 0) styles = "";
                        String1.Append(string.Format("<ul class=\"clearfix{0}\">", styles));
                        string style1 = "";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (i == DS.Tables[0].Rows.Count-1)
                            {
                                style1 = " class=\"no-mrr\"";
                            }

                            Pics = "/images/none.gif";
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                            String1.Append(string.Format("<li{0}><dl><dt class=\"relative-box\">", style1));
                            String1.Append(string.Format("<a href=\"/line/{3}.html\" target=\"_blank\"><img src=\"{0}\" alt=\"{2}\" title=\"{2}\"></a>", Pics, DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                            String1.Append("<p class=\"absolute-box fl clearfix\">");
                            String1.Append(string.Format("<span class=\"fl\" title=\"{0}\">{0}</span>", DS.Tables[0].Rows[i]["Destination"].ToString()));
                            String1.Append(string.Format("<b class=\"cost rl\"><span>¥</span>{0}<i>起</i></b></p>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                            String1.Append(string.Format("</dt><dd><a href=\"/line/{2}.html\" target=\"_blank\">{1}</a></dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString(), DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                            String1.Append("</dl></li>");

                        }
                        if (DS1.Tables[0].Rows[ii]["Url"].ToString().Length > 5)
                        {
                            String1.Append(string.Format("<a class=\"more hide absolute-box\" href=\"{0}\" target=\"_blank\">更多{1}线路>></a>", DS1.Tables[0].Rows[ii]["Url"].ToString(), DS1.Tables[0].Rows[ii]["Cname"].ToString()));
                        }
                        String1.Append("</ul>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Index_OtherLine_" + linetype, infos);
            }
            return infos;

        }

        public static string Index_Visa()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Index_Visa"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", "Index_Visa");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    
                    SqlQueryText = string.Format("select top 9 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 9 * FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }
                    string Pics = "/Images/none.gif";
                    string styles = "no-mrr ";
                    String1.Append("<div class=\"column\">");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                        
                        styles = "";
                        if (i == 2 || i == 5 || i == 8) styles = "no-mrr ";
                        if (i == 3) String1.Append("</div><div class=\"column\">");
                        if (i == 6) String1.Append("</div><div class=\"column\">");
                        String1.Append(string.Format(@"<a class='{4}clearfix' href='/line/{5}.html' target='_blank'>
                            <dt><img style='width: 90px;height:70px' src='{0}' alt='' title='{2}'></dt>
                            <dd class='txt'>{2}</dd>
                            <dd class='cost'><span>¥</span> {3}</dd></a>", 
                            Pics, 
                            DS.Tables[0].Rows[i]["id"].ToString(), 
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            styles,
                            DS.Tables[0].Rows[i]["MisLineId"].ToString()
                         ));
                    }
                    String1.Append("</div>");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Index_Visa", infos);
            }
            return infos;
        }
        
        public static string Second_Ad_Slide(string types,string flag)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_Second_Slide_" + types]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 5 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort,EditTime desc", types);
                StringBuilder String1 = new StringBuilder();
                StringBuilder String2 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    String1.Append("<ul id=\"indexTopbanner\" class=\"banner-list-box clearfix\">");
                    String2.Append("<ol id=\"bannerGoods\" class=\"absolute-box clearfix\">");
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<li{0}><a href=\"{1}\" target=\"_blank\"><img src=\"{2}\" alt=\"{3}\" title=\"{3}\"></a></li>", styles, DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                        String2.Append(string.Format("<li{0}></li>", styles));
                    }
                    String1.Append("</ul>");
                    String2.Append("</ol>");
                }
                if (flag == "cruise")
                {
                    infos = "<div class=\"banner-box cruise-banner-box relative-box\">" + String1.ToString() + String2.ToString() + "</div>";
                }
                else
                {
                    infos = "<div class=\"banner-box relative-box rl\">" + String1.ToString() + String2.ToString() + "</div>";
                }
                HttpContext.Current.Cache.Insert("NewPage_Ad_Second_Slide_" + types, infos);
            }
            return infos;
        }

        public static string Second_Hot(string types)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Hot_Second_" + types]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", types);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();

                    SqlQueryText = string.Format("select top 6 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 6 * FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }
                    int ii = 0;
                    String1.Append("<ul>");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        ii = i + 1;
                        if (ii > 4) ii = 4;
                        String1.Append(string.Format(@"<li><b class='top{0}'>{1}</b>
                            <a class='clearfix' href='/line/{5}.html' title='' target='_blank'>
                            <p>{3}</p>
                            <div class='cost rl'><span>¥</span>{4}</div>
                            </a></li>",
                            ii,
                            i + 1,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            DS.Tables[0].Rows[i]["MisLineId"].ToString()
                         ));
                    }
                    String1.Append("</ul>");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Hot_Second_" + types, infos);
            }
            return infos;
        }

        public static string Second_Line_Sell(string types)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Second_Sell_" + types]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", types);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();

                    String1.Append(string.Format("<div class=\"sale-main\"><h2 class=\"relative-box\">特惠精选<a href=\"{0}\" target=\"_blank\">更多特价产品>></a></h2>", DS.Tables[0].Rows[0]["Url"].ToString()));
                    SqlQueryText = string.Format("select top 4 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 4 * FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }

                    String1.Append("<div id=\"topsaleImg\" class=\"product-wrap clearfix\">");

                    string Pics = "/Images/none.gif";
                    string styles = "";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                        
                        styles = "";
                        if (i == 1||i == 3) styles = "no-mrr ";
                        String1.Append(string.Format(@"
                            <li>
                                <dl class='{0}clearfix'>
                                    <dt class='relative-box fl'>
                                        <a href='/line/{5}.html' title='' target='_blank'>
                                            <img style='width:180px;height:125px' src='{1}' alt='{3}' />
                                            <div class='salemask'></div>
                                        </a>
                                    </dt>
                                    <div class='products rl'>
                                        <dd><a href='/line/{5}.html' title='' target='_blank'>{3}</a></dd>
                                        <div class='cost clearfix'>
                                            <i class='fl'></i>
                                            <p class='rl'><span>¥</span>{4}<b>起</b></p>
                                        </div>
                                        <dd class='buying products-btn'><a class='rl' href='/line/{5}.html' target='_blank'>查看详细</a></dd>
                                    </div>
                                </dl>
                            </li>",
                            styles,
                            Pics,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            DS.Tables[0].Rows[i]["MisLineId"].ToString()
                         ));
                    }
                    String1.Append("</div></div>");
                    String1.Append("");
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Second_Sell_" + types, infos);
            }
            return infos;
        }

        public static string Second_Line_Tab(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Second_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select Cname from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    //<li class="current">推荐</li>
                    //<li>港澳台</li>
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<li{0}>{1}</li>", styles, DS.Tables[0].Rows[i]["Cname"].ToString()));
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Second_" + linetype, infos);
            }
            return infos;
        }

        public static string Second_Line_List(string linetype,string rows)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Second_Line_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    string styles = " current";
                    string Pics = "/Images/none.gif";
                    
                    for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                    {
                        typeid = DS1.Tables[0].Rows[ii]["LineType"].ToString();
                        destid = DS1.Tables[0].Rows[ii]["Destinationid"].ToString();
                        SqlQueryText = string.Format("select top {1} * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS1.Tables[0].Rows[ii]["id"].ToString(), rows);
                        DataSet DS = new DataSet();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top " + rows + " * FROM View_SpecialLineTemp where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                DS.Clear();
                                DS = MyDataBaseComm.getDataSet(sqlstr);
                            }
                        }

                        if (ii != 0) styles = "";

                        String1.Append(string.Format("<ul id=\"changeProduct_list\" class=\"product-list{0} clearfix\">", styles));
                        string style1 = "";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (i == 3 || i == 7)
                            {
                                style1 = " class=\"no-mrr\"";
                            }
                            else
                            {
                                style1 = "";
                            }

                            //<a href='line.html?id={6}' title='{3}' target='_blank'>
                            //                    <img style='width:205px;height:140px' src='{1}' alt='{3}' />
                            //                    <div class='recommend'>
                            //                        <span>推荐理由：</span>
                            //                        <p>{5}</p>
                            //                        <div class='opacity-bg'></div>
                            //                    </div>
                            //                </a>
                                            

                            Pics = "/images/none.gif";
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                            String1.Append(string.Format(@"
                                <li{0}>
                                    <dl>
                                        <dt>
                                            <p class='clearfix'>
										        <span>{7}</span>
										        <b class='cost rl'><span>¥</span>{4}<i>起</i></b>
									        </p>
									        <a href='/line/{6}.html' target='_blank'>
										        <img src='{1}' title='{3}'/>
									        </a>
                                        </dt>
                                        <dd>
                                            <a href='/line/{6}.html' title='' target='_blank'>{3}</a>
                                        </dd>
                                    </dl>
                                </li>",
                                style1,
                                Pics,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                DS.Tables[0].Rows[i]["LineName"].ToString(),
                                DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                                DS.Tables[0].Rows[i]["LineFeature"].ToString(),
                                DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                                GetPlanType(DS.Tables[0].Rows[i]["PlanType"].ToString())
                             ));

                            if (i == 3 && DS.Tables[0].Rows.Count > 4) String1.Append("<div class=\"line\"></div>");
                        }
                        
                        if (DS1.Tables[0].Rows[ii]["Url"].ToString().Length > 5)
                        {
                            String1.Append(string.Format("<a class=\"more\" href=\"{0}\" target=\"_blank\">更多{1}线路>></a>", DS1.Tables[0].Rows[ii]["Url"].ToString(), DS1.Tables[0].Rows[ii]["Cname"].ToString()));
                        }
                        String1.Append("</ul>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Second_Line_" + linetype, infos);
            }
            return infos;

        }

        private static string GetPlanType(string flag)
        {
            string tname = "跟团游";
            if (flag == "1") tname = "跟团游";
            if (flag == "2") tname = "自由行";
            if (flag == "3") tname = "当地游";
            if (flag == "4") tname = "签证";
            return tname;
        }

        public static string Second_Cruise_Tab(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Second_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select Cname from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    //<li class="current">推荐</li>
                    //<li>港澳台</li>
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format("<li class ='current'><span{0}></span>{1}</li>", styles, DS.Tables[0].Rows[i]["Cname"].ToString()));
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Second_" + linetype, infos);
            }
            return infos;
        }

        public static string Second_Cruise_List(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Second_Line_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    string styles = " current";
                    string Pics = "/Images/none.gif";
                    for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                    {
                        typeid = DS1.Tables[0].Rows[ii]["LineType"].ToString();
                        destid = DS1.Tables[0].Rows[ii]["Destinationid"].ToString();
                        SqlQueryText = string.Format("select top 9 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS1.Tables[0].Rows[ii]["id"].ToString());
                        DataSet DS = new DataSet();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top 9 * FROM View_SpecialLineTemp where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                DS.Clear();
                                DS = MyDataBaseComm.getDataSet(sqlstr);
                            }
                        }
                        if (ii != 0) styles = "";
                        String1.Append(string.Format("<ul id=\"changeProduct_list\" class=\"product-list{0} clearfix\">", styles));
                        string style1 = "";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (i == 1 || i == 4 || i == 7)
                            {
                                style1 = " class=\"malr\"";
                            }
                            else
                            {
                                style1 = "";
                            }

                            Pics = "/images/none.gif";
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                            String1.Append(string.Format(@"
                                <li{0}>
						            <dl>
							            <dt>
								            <a href='/line/{7}.html' target='_blank'>
									            <img style='width:332px;height:180px' src='{1}' alt='' title='{3}' />
								            </a>
							            </dt>
							            <dd>
								            <div class='name'>
									            <a href='/line/{7}.html' title='' target='_blank'>{3}</a>
								            </div>
								            <div class='special' title=''>
									            {5}
								            </div>
								            <div class='msg clearfix'>
									            <p>开船日期：{6:yyyy-MM-dd}</p>
									            <div class='price'>
										            <span>¥</span>{4}<b>起</b>
									            </div>
								            </div>
							            </dd>
						            </dl>
					            </li>",
                                style1,
                                Pics,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                DS.Tables[0].Rows[i]["LineName"].ToString(),
                                DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                                DS.Tables[0].Rows[i]["LineFeature"].ToString(),
                                DS.Tables[0].Rows[i]["PlanDate"].ToString(),
                                DS.Tables[0].Rows[i]["MisLineId"].ToString()
                             ));

                            if (i == 3 && DS.Tables[0].Rows.Count > 4) String1.Append("<div class=\"line\"></div>");
                            if (i == 6 && DS.Tables[0].Rows.Count > 7) String1.Append("<div class=\"line\"></div>");
                        }

                        if (DS1.Tables[0].Rows[ii]["Url"].ToString().Length > 5)
                        {
                            String1.Append(string.Format("<a class=\"more\" href=\"{0}\" target=\"_blank\">更多{1}线路>></a>", DS1.Tables[0].Rows[ii]["Url"].ToString(), DS1.Tables[0].Rows[ii]["Cname"].ToString()));
                        }
                        String1.Append("</ul>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Second_Line_" + linetype, infos);
            }
            return infos;

        }

        //third
        public static string Third_Visa_Tab(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Third_Visa_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select Cname from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    //<li class="current">推荐</li>
                    //<li>港澳台</li>
                    string styles = " class=\"current\"";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        String1.Append(string.Format(@"
                            <li{0}><a href='#state_{1}'>{2}</a></li>",
                            styles,
                            i,
                            DS.Tables[0].Rows[i]["Cname"].ToString()
                        ));
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Third_Visa_" + linetype, infos);
            }
            return infos;
        }

        public static string Third_Visa_List(string linetype)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Tab_Third_Visa_List_" + linetype]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", linetype);
                StringBuilder String1 = new StringBuilder();
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    string styles = " class=\"mrr\"";
                    string Pics = "/Images/none.gif";
                    for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                    {
                        typeid = DS1.Tables[0].Rows[ii]["LineType"].ToString();
                        destid = DS1.Tables[0].Rows[ii]["Destinationid"].ToString();
                        SqlQueryText = string.Format("select * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS1.Tables[0].Rows[ii]["id"].ToString());
                        DataSet DS = new DataSet();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT * FROM View_SpecialLineTemp where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                DS.Clear();
                                DS = MyDataBaseComm.getDataSet(sqlstr);
                            }
                        }

                        String1.Append(string.Format(@"
                            <div id='state_{0}' name='state_{0}' class='state'>
                                <h3>{1}</h3>
                                <div class='s-content'>", 
                            ii,
                            DS1.Tables[0].Rows[ii]["Cname"].ToString()
                        ));
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if ((i+1) % 5 == 0)
                            {
                                styles = " class=\"mrr\"";
                            }
                            else
                            {
                                styles = "";
                            }
                            Pics = "/images/none.gif";
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                            String1.Append(string.Format(@"
                                <a href='/line/{4}.html' target='_blank'{0}>
                                    <img style='width:160px;height:106px' src='{1}' alt='{3}' title='{3}'>
                                    <em>{3}</em>
                                </a>",
                                styles,
                                Pics,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                DS.Tables[0].Rows[i]["LineName"].ToString(),
                                DS.Tables[0].Rows[i]["MisLineId"].ToString()
                             ));
                        }
                        String1.Append("</div></div>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Tab_Third_Visa_List_" + linetype, infos);
            }
            return infos;

        }

        public static string LineList_Ad_Season()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_Ad_LineList_Season"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort,EditTime desc", "N_S_List_Season");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    String1.Append(string.Format("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" title=\"{2}\"></a>", DS.Tables[0].Rows[0]["AdPageUrl"].ToString(), DS.Tables[0].Rows[0]["AdPicUrl"].ToString(), DS.Tables[0].Rows[0]["AdName"].ToString()));

                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("NewPage_Ad_LineList_Season", infos);
            }
            return infos;
        }

        public static string CreateLeftJournal(string province, int RowNums)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_LeftJournal_" + province]);
            if (infos == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = "";
                SqlQueryText = string.Format("SELECT top {0} id,title from OL_Journal where flag='1' and Destinationid like '%,{1},%' order by id desc", RowNums, province);

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<i></i><a href=\"/showjournal/{0}.html\" title=\"{1}\" target=\"_blank\">{1}</a><br>",
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["title"].ToString()
                        ));
                    }
                }
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("NewPage_LeftJournal_" + province, infos);
            }
            return infos;
        }

        public static string GuessYouLike(string lineclass, int RowNums)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_GuessYouLike_" + lineclass]);
            if (infos == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = "";
                SqlQueryText = string.Format("SELECT top {0} MisLineId,LineName,Price,(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = dbo.OL_Line.FirstDestination)) AS DestinationName from OL_Line where Sale='0' and Price>0 and PlanDate>='{2}' and LineClass='{1}'", RowNums, lineclass, DateTime.Today.ToString());


                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format(@"
                            <li>
	                            <a href='/line/{0}.html' title='{1}' target='_blank'>
		                            <h5>{2}</h5>
		                            <p class=linename>{1}</p>
		                            <p class='price'>
			                            <span class='mrr'>￥</span>
			                            {3}
			                            <i>起</i>
		                            </p>
	                            </a>
                            </li>",
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["DestinationName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")
                        ));
                    }
                }
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("NewPage_GuessYouLike_" + lineclass, infos);
            }
            return infos;
        }

        public static string GuessYouLike(string lineclass, string linetype, string firstDestination, int RowNums)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewPage_GuessYouLike_" + firstDestination]);
            if (infos == "") { 
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = "";
                SqlQueryText = string.Format("SELECT top {0} MisLineId,LineName,Price,(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = dbo.OL_Line.FirstDestination)) AS DestinationName from OL_Line where Sale='0' and Price>0 and PlanDate>='{2}' and LineClass='{1}'", RowNums, lineclass, DateTime.Today.ToString());
                if (linetype.Equals("visa"))
                {
                    string destinationName = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id =" + firstDestination);
                    string SqlQueryText1 = string.Format("SELECT top {0} MisLineId,LineName,Price,(SELECT DestinationName FROM dbo.OL_Destination WHERE (Id = dbo.OL_Line.FirstDestination)) AS DestinationName from OL_Line where (dbo.OL_Line.Destination like '%{2}%' or dbo.OL_Line.LineName like '%{2}%') and Sale='0' and Price>0 and PlanDate>='{3}' and linetype<>'{1}'", RowNums, linetype, destinationName, DateTime.Today.ToString());
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText1);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                        {
                            Strings.Append(string.Format(@"
                            <li>
	                            <a href='/line/{0}.html' title='{1}' target='_blank'>
		                            <h5>{2}</h5>
		                            <p class=linename>{1}</p>
		                            <p class='price'>
			                            <span class='mrr'>￥</span>
			                            {3}
			                            <i>起</i>
		                            </p>
	                            </a>
                            </li>",
                                DS1.Tables[0].Rows[i]["MisLineId"].ToString(),
                                DS1.Tables[0].Rows[i]["LineName"].ToString(),
                                DS1.Tables[0].Rows[i]["DestinationName"].ToString(),
                                DS1.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")
                            ));
                        }
                        infos = Strings.ToString();
                        HttpContext.Current.Cache.Insert("NewPage_GuessYouLike_" + firstDestination, infos);
                        return infos;
                    }
                }

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format(@"
                            <li>
	                            <a href='/line/{0}.html' title='{1}' target='_blank'>
		                            <h5>{2}</h5>
		                            <p class=linename>{1}</p>
		                            <p class='price'>
			                            <span class='mrr'>￥</span>
			                            {3}
			                            <i>起</i>
		                            </p>
	                            </a>
                            </li>",
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            DS.Tables[0].Rows[i]["DestinationName"].ToString(),
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")
                        ));
                    }
                }
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("NewPage_GuessYouLike_" + firstDestination, infos);
            }
            return infos;
        }
    
    }
}