using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;

namespace TravelOnline.tour
{
    public class LineRecommend
    {
        //首页名牌
        public static string IndexPageCountryCache(string CacheType, string LineType, string LineClass, int RowNums)
        {
            if (Convert.ToString(HttpContext.Current.Cache[CacheType + "_" + LineClass]) == "")
            {
                HttpContext.Current.Cache.Insert(CacheType + "_" + LineClass, CreateCountry(CacheType, LineType, LineClass, RowNums));
            }
            return Convert.ToString(HttpContext.Current.Cache[CacheType + "_" + LineClass]);
        }

        private static string CreateCountry(string CacheType, string LineType, string LineClass, int RowNums)
        {
            string desids = MyDataBaseComm.getScalar("select Destinationid from OL_ProductType where MisClassId='" + LineClass + "'and ProductType='" + LineType + "'");
            if (desids != null)
            {
                desids = "0" + desids + "0";
            }
            else
            {
                desids = "0";
            }
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("SELECT top {0} id,ParentId,DestinationName,ClassLevel from OL_Destination where (ClassLevel='2' or ClassLevel='3') and id in ({1}) order by ClassLevel,id", RowNums, desids);
            //return SqlQueryText;
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (DS.Tables[0].Rows[i]["ClassLevel"].ToString() == "2")
                    {
                        Strings.Append(string.Format("<a target=\"_blank\" href=\"/{2}/{3}-{0}-0-0-0-0-0-0-0-1.html\">{1}</a>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString(), LineType, LineClass));
                    }
                    else
                    {
                        Strings.Append(string.Format("<a target=\"_blank\" href=\"/{2}/{3}-{4}-{0}-0-0-0-0-0-0-1.html\">{1}</a>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString(), LineType, LineClass, DS.Tables[0].Rows[i]["ParentId"].ToString()));
                    }
                    if (i < DS.Tables[0].Rows.Count-1) Strings.Append(" / ");
                }
                //英国 / 法国 / 德国 / 意大利 / 瑞典 / 奥地利 / 葡萄牙 / 希腊 / 丹麦 / 芬兰 / 捷克 / 瑞士 / 西班牙 / 挪威
            }
            return Strings.ToString();
        }

        //首页名牌
        public static string LineRecommendCache(string CacheType, string RecommType, string RecommFlag, string PicFlag, string LineType, string LineClass, string Province, string LineId, int RowNums)
        {
            if (Convert.ToString(HttpContext.Current.Cache[CacheType]) == "")
            {
                HttpContext.Current.Cache.Insert(CacheType, CreateNewRecommend(CacheType, RecommType, RecommFlag, PicFlag, LineType, LineClass, Province, LineId, RowNums));
            }
            return Convert.ToString(HttpContext.Current.Cache[CacheType]);
        }

        private static string CreateNewRecommend(string CacheType, string RecommType, string RecommFlag, string PicFlag, string LineType, string LineClass, string Province, string LineId, int RowNums)
        {
            //<div class="product_zoomfix"> 
            //    <a class="product" target="_blank" href="" >
            //        <img src="img/grey.gif" data-original="files/14_159-m.jpg"  alt="余杭仙山谷“皮划艇激浪漂流”+葡萄采摘纯玩巴士一日游" height="105px" width="200px">
            //        <span class="price_box"><strong class="place">杭州</strong> <strong class="price"><dfn>&#165;</dfn>138<em>起</em></strong></span>
            //        <span class="name_wrap">
            //            <span class="product_name" title="11余杭仙山谷“皮划艇激浪漂流”+葡萄采摘纯玩巴士一日游">11余杭仙山谷“皮划艇激浪漂流”+葡萄采摘纯玩巴士一余杭仙山谷“皮划艇激浪漂流”+葡萄采摘纯玩巴士一余杭仙山谷“皮划艇激浪漂流”+葡萄采摘纯玩巴士一日游</span>
            //            <span class="product_explain" title="天天低价">天天低价</span>
            //        </span>
            //     </a> 
            //</div>
            //<div class="product_zoomfix big_pic"> <a class="product" target="_blank" href=""> <span class="big_img_box"><img src="img/1.jpg" /></span> <span class="price_box"><strong class="place">香港</strong> <strong class="price"><dfn>&#165;</dfn>1908<em>起</em></strong> </span> <span class="name_wrap"> <span class="product_name" title="香港4日自由行·机票+酒店自助游">香港4日自由行·机票+酒店自助游</span>  </span> </a> </div>
    
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "";
            string Pics = "/Images/none.gif";
            string mp_pic = "";

            SqlQueryText = string.Format("SELECT top {0} MisLineId,LineName,Price,Pics,BigPics,IndexRecom,famous,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination FROM OL_Line where Sale='0' and Price>0 and PlanDate>'{1}'", RowNums, DateTime.Today.ToString());
            if (LineType != "") SqlQueryText += " and LineType='" + LineType + "' ";
            if (Province != "")
            {
                SqlQueryText += " and Destinationid like '%," + Province + ",%' ";
                if (LineId != "") SqlQueryText += " and MisLineId <> '" + LineId + "' ";
            }
            if (LineClass != "") SqlQueryText += " and LineClass='" + LineClass + "' ";
            SqlQueryText += string.Format(" and ({0}='{1}' or {0}='0') order by {0} desc,NewSortTime desc", RecommType, RecommFlag);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Pics = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    if (LineType == "visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                    if (PicFlag == "BigPic" && i == 0)
                    {
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[0]["BigPics"].ToString().Length > 10) Pics = DS.Tables[0].Rows[i]["BigPics"].ToString();
                        Strings.Append("<div class=\"product_zoomfix big_pic\">");
                        Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/line/{0}.html\" >", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        Strings.Append("<span class=\"big_img_box\">");
                        Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append("</span>");
                    }
                    else
                    {
                        Strings.Append("<div class=\"product_zoomfix\">");
                        Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/line/{0}.html\" >", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                    }
                    mp_pic = "";
                    if (CacheType == "Index_Famous")
                    {
                        if (DS.Tables[0].Rows[i]["famous"].ToString().Length > 0) mp_pic = string.Format("<img src=\"/img/mp_{0}.png\" style=\"width:80px;height:30px\">", DS.Tables[0].Rows[i]["famous"].ToString());
                    }
                    Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong> <strong class=\"price\"><dfn>&#165;</dfn>{1}<em>起</em></strong></span>", DS.Tables[0].Rows[i]["Destination"].ToString(), DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    Strings.Append("<span class=\"name_wrap\">");
                    Strings.Append(string.Format("<span class=\"product_explain\">{0}</span>", mp_pic));
                    Strings.Append(string.Format("<span class=\"product_name\" title=\"{0}\">{0}</span>", DS.Tables[0].Rows[i]["LineName"].ToString()));
                    Strings.Append("</span></a></div>");
                }
            }
            else
            {
                Strings.Append("&nbsp;");
            }
            return Strings.ToString();
        }


        //左侧一周热卖排行，特惠推荐，游记等
        public static string LeftLineRecommendSellCache(string CacheType, string LineType, string LineClass, int RowNums)
        {
            if (Convert.ToString(HttpContext.Current.Cache[CacheType + LineClass]) == "")
            {
                //LeftLineHotSell 一周热卖排行  LeftLinePreferences 特惠推荐  LeftLineJournal 攻略推荐
                if (CacheType == "LeftLineJournal" || CacheType == "JournalDetailRecomm")
                {
                    HttpContext.Current.Cache.Insert(CacheType + LineClass, CreateLeftJournal(CacheType, LineType, LineClass, RowNums));
                }
                else
                { 
                    HttpContext.Current.Cache.Insert(CacheType + LineClass, CreateLeftRecommend(CacheType, LineType, LineClass, RowNums));
                }
                
            }
            return Convert.ToString(HttpContext.Current.Cache[CacheType + LineClass]);
        }

        //左侧一周热卖排行，特惠推荐，游记等
        public static string DestinationLineSellCache(string CacheType,string desid, int RowNums)
        {
            if (Convert.ToString(HttpContext.Current.Cache[CacheType + desid]) == "")
            {
                HttpContext.Current.Cache.Insert(CacheType + desid, CreateLeftRecommend(CacheType, desid, desid, RowNums));

            }
            return Convert.ToString(HttpContext.Current.Cache[CacheType + desid]);
        }

        private static string CreateLeftRecommend(string CacheType, string LineType, string LineClass, int RowNums)
        {
            LineType = LineType.ToLower();
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "";
            string Pics = "/Images/none.gif";

            DateTime date = DateTime.Today;
            date = date.AddDays(-7);

            SqlQueryText = string.Format("SELECT top {0} MisLineId,LineName,Price,Pics,LineDays FROM View_WeekSellCount where Sale='0' and Price>0 and PlanDate>'{1}'", RowNums, DateTime.Today.ToString());
            if (CacheType == "LeftJournalLinePreferences" || CacheType == "DestinationLineSell")
            {
                SqlQueryText += " and Destinationid like '%," + LineType + ",%' and LineType<>'Visa' order by Preferences desc,pv desc";
            }
            else
            { 
                if (LineType != "") SqlQueryText += " and LineType='" + LineType + "' ";
                if (LineClass != "") SqlQueryText += " and LineClass='" + LineClass + "' ";
                if (CacheType == "LeftLineHotSell") SqlQueryText += " order by sellcount desc,pv desc";
                if (CacheType == "LeftLinePreferences") SqlQueryText += " and Preferences in (0,1) order by Preferences desc,pv desc";
            }
            
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int ranks = 1;
                string css, display, daystring;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ranks = i + 1;
                    css = "";
                    display = "none";
                    Pics = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    if (LineType == "visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());

                    if (CacheType == "LeftLineHotSell")
                    {
                        if (ranks == 1)
                        {
                            css = "fore1 fore";
                            display = "block";
                        }
                        if (ranks == 2) css = "fore2";
                        if (ranks == 3) css = "fore3";
                        if (LineType == "visa")
                        {
                            daystring = "";
                        }
                        else
                        {
                            daystring = string.Format("<div class=\"p-day\" style=\"display: {0};\"><span class=\"label label-warning\">{1} 天 </span></div>", display, DS.Tables[0].Rows[i]["LineDays"].ToString());
                        }
                        Strings.Append(string.Format("<li class=\"{6}\"><span class=\"fno\">{0}</span><div class=\"p-img\" style=\"display: {7};\"><a target=\"_blank\" href=\"/line/{1}.html\"><img alt=\"{2}\" onerror=\"this.src='/Images/none.gif'\" src=\"{3}\" width=\"60px\" height=\"50px\"></a></div><div class=\"p-price\" style=\"display: {7};\">&yen;{4}<em>起</em></div>{5}<div class=\"p-name\"><a target=\"_blank\" href=\"/line/{1}.html\">{2}</a></div></li>",
                            ranks,
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            Pics,
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""),
                            daystring,
                            css,
                            display
                        ));
                    }

                    if (CacheType == "LeftLinePreferences" || CacheType == "LeftJournalLinePreferences" || CacheType == "DestinationLineSell")
                    {//src=\"/images/none.gif\" data-original=\"{2}\" 
                        Strings.Append(string.Format("<li><div class=\"p-img\"><a href=\"/line/{0}.html\" title=\"{1}\" target=\"_blank\"><img width=\"120\" height=\"90\" src=\"{2}\" alt=\"{1}\"></a></div><div class=\"rate\"><a href=\"/line/{0}.html\" title=\"{1}\" target=\"_blank\">{1}</a></div><div class=\"p-price\"><strong>￥{3}<em>起</em></strong></div></li>",
                            DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                            DS.Tables[0].Rows[i]["LineName"].ToString(),
                            Pics,
                            DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")
                        ));
                    }
                }

            }
            return Strings.ToString();
        }

        private static string CreateLeftJournal(string CacheType, string types, string province, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "";
            SqlQueryText = string.Format("SELECT top {0} id,title,(select DestinationName from OL_Destination where id=OL_Journal.FirstDestination) as dname,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl from OL_Journal where flag='1' and Destinationid like '%,{1},%' order by id desc", RowNums, province);
            if (CacheType == "JournalDetailRecomm") string.Format("SELECT top {0} id,title,(select DestinationName from OL_Destination where id=OL_Journal.FirstDestination) as dname,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl from OL_Journal where flag='1' and id<>'{2}' and Destinationid like '%,{1},%' order by id desc", RowNums, types, province);
            if (types == "all") SqlQueryText = string.Format("SELECT top {0} id,title,(select DestinationName from OL_Destination where id=OL_Journal.FirstDestination) as dname,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl from OL_Journal where flag='1' and id<>'{2}' order by newId()", RowNums, types, province);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (CacheType == "JournalDetailRecomm")
                    {
                        ThumbSrc = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["coverpicurl"].ToString().Length > 10)
                        {
                            sArray = DS.Tables[0].Rows[i]["coverpicurl"].ToString().Split('.');
                            ThumbSrc = string.Format("{0}_T300.{1}", sArray[0].ToString(), sArray[1].ToString());
                        }
                        else
                        {
                            if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                            {
                                sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('.');
                                ThumbSrc = string.Format("{0}_T300.{1}", sArray[0].ToString(), sArray[1].ToString());
                            }
                        }
                        Strings.Append("<div class=\"product_zoomfix\">");
                        Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/showjournal/{0}.html\" >", DS.Tables[0].Rows[i]["id"].ToString()));
                        Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", ThumbSrc, DS.Tables[0].Rows[i]["title"].ToString()));
                        Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["dname"].ToString()));
                        Strings.Append(string.Format("<span class=\"name_wrap\"><span class=\"product_explain\">&nbsp;</span><span class=\"product_name\" title=\"{0}\">{0}</span></span>", DS.Tables[0].Rows[i]["title"].ToString()));
                        Strings.Append("</a></div>");
                    }
                    else
                    {
                        Strings.Append(string.Format("<li><span class=\"fno\">{2}</span><div class=\"jname\"><a href=\"/showjournal/{0}.html\" title=\"{1}\" target=\"_blank\">{1}</a></div></li>",
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            DS.Tables[0].Rows[i]["title"].ToString(),
                            i + 1
                        ));
                    }
                    
                }
            }
            return Strings.ToString();
        }

    }
}