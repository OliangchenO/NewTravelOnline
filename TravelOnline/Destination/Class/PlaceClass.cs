using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TravelOnline.Destination.Class
{
    public class PlaceClass
    {
        public static string DestinationCache(string CacheType, string LineType, string id, int RowNums, string SecType)
        {
            if (Convert.ToString(HttpContext.Current.Cache[CacheType + "_" + id + SecType]) == "")
            {
                switch (CacheType)
                {
                    case "PlaceList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceList(id, RowNums));
                        break;
                    case "PlaceLineList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceLineList(LineType, id, RowNums));
                        break;
                    case "PlaceJournalList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceJournalList(id, RowNums));
                        break;
                    case "PlaceViewList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceViewList(id, RowNums));
                        break;
                    case "PlaceSightRecommend":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, SightRecommend(id, RowNums));
                        break;
                    case "PlaceSightList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceSightList(LineType, id));
                        break;
                    case "PlaceAllViewList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceAllViewList(LineType, id));
                        break;
                    case "PlaceAllJournalList":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceAllJournalList(LineType, id));
                        break;
                    case "PlaceHotDestination":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceHotDestination(RowNums));
                        break;
                    case "PlaceHotView":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, PlaceHotView(RowNums));
                        break;
                    case "LineViewsPic":
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id, LineViewsPic(id,LineType));
                        break;
                    default:
                        HttpContext.Current.Cache.Insert(CacheType + "_" + id + SecType, "");
                        break;
                }
            }
            return Convert.ToString(HttpContext.Current.Cache[CacheType + "_" + id + SecType]);
        }

        public static String GetSeoLinkKeyWord(string contents,string ranks)
        {
            int linkcount = 0;
            int indexno = 0;
            int oldindex = 0;
            int stringlen = 0;
            string SqlQueryText = string.Format("select keyword,url from SeoLink where rank>'{0}' order by rank desc,keylength desc", ranks);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    indexno = -1;
                    oldindex = -1;
                    
                    if (linkcount < 5)
                    {
                        indexno = contents.IndexOf(DS.Tables[0].Rows[i]["keyword"].ToString());
                        oldindex = contents.IndexOf(">" + DS.Tables[0].Rows[i]["keyword"].ToString());
                        if (indexno > -1 && oldindex == -1)
                        {
                            //contents = contents.Replace(DS.Tables[0].Rows[i]["keyword"].ToString(), string.Format("<a class=\"seolink\" target=\"_blank\" href=\"{0}\">{1}</a>", DS.Tables[0].Rows[i]["url"].ToString(), DS.Tables[0].Rows[i]["keyword"].ToString()));
                            stringlen = DS.Tables[0].Rows[i]["keyword"].ToString().Length;
                            contents = contents.Substring(0, indexno) + string.Format("<a class=\"seolink\" target=\"_blank\" href=\"{0}\">{1}</a>", DS.Tables[0].Rows[i]["url"].ToString(), DS.Tables[0].Rows[i]["keyword"].ToString()) + contents.Substring(indexno + stringlen);
                            linkcount++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return contents;

        }

        public static String NewLineViewsPic(string lineid)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["NewLineViewsPicArray_" + lineid]);
            if (infos == "")
            {
                StringBuilder Strings = new StringBuilder();
                StringBuilder Strings1 = new StringBuilder();
                string SqlQueryText = string.Format("select viewid,days,(select viewname from OL_View where id=ViewDest.viewid) as viewname,(select left(intro,30) from OL_View where id=ViewDest.viewid) as intro,(select top 1 picurl from OL_ViewPic where viewid=ViewDest.viewid order by newId()) as picurl from ViewDest where lineid='{0}' order by days", lineid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string ThumbSrc = "", intro = "";
                    string[] sArray;
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        ThumbSrc = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                        {
                            sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                            ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                        }
                        intro = DS.Tables[0].Rows[i]["intro"].ToString();
                        if (intro.Length > 26) intro = intro.Substring(0, 25) + "...";
                        //Strings.Append("<div class=\"product_zoomfix\">");
                        //Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/sightdetail/{0}.html\" >", DS.Tables[0].Rows[i]["id"].ToString()));
                        //Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", ThumbSrc, DS.Tables[0].Rows[i]["viewname"].ToString()));
                        //Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["viewname"].ToString()));
                        //Strings.Append("</a></div>");

                        Strings1.Append(string.Format("{0}|", DS.Tables[0].Rows[i]["viewid"].ToString()));
                        Strings1.Append(string.Format("{0}|", DS.Tables[0].Rows[i]["viewname"].ToString()));
                        Strings1.Append(string.Format("{0}|", ThumbSrc));
                        Strings1.Append(string.Format("{0}|", DS.Tables[0].Rows[i]["days"].ToString()));
                        Strings1.Append(string.Format("{0}$", intro));
                    }
                }
                else
                {
                    Strings1.Append("none");
                }
                infos = Strings1.ToString();
                HttpContext.Current.Cache.Insert("NewLineViewsPicArray_" + lineid, infos);
            }
            return infos;
        }

        public static String LineViewsPic(string id, string viewids)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder Strings1 = new StringBuilder();
            string SqlQueryText = string.Format("select id,viewname,left(intro,30) as intro,(select top 1 picurl from OL_ViewPic where viewid=OL_View.id order by newId()) as picurl from OL_View where id in (0{0}0)", viewids);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "", intro = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    {
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }
                    intro = DS.Tables[0].Rows[i]["intro"].ToString();
                    if (intro.Length > 26) intro = intro.Substring(0, 25) + "...";
                    Strings.Append("<div class=\"product_zoomfix\">");
                    Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/sightdetail/{0}.html\" >", DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", ThumbSrc, DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append("</a></div>");

                    Strings1.Append(string.Format("{0}|", DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings1.Append(string.Format("{0}|", DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings1.Append(string.Format("{0}|", ThumbSrc));
                    Strings1.Append(string.Format("{0}$", intro));
                }
                HttpContext.Current.Cache.Insert("LineViewsPicArray_" + id, Strings1.ToString());
            }
            return Strings.ToString();
        }

        public static String PlaceHotView(int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,viewname from OL_View order by newId()", RowNums);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<a href=\"/sightdetail/{0}.html\" target=\"_blank\">{1}</a>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["viewname"].ToString()));
                }
            }
            return Strings.ToString();
        }

        public static String PlaceHotDestination(int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,DestinationName from OL_Destination where ClassLevel>2 and hotflag='1' order by newId()", RowNums);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<a href=\"/place/{0}.html\" target=\"_blank\">{1}</a>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                }
            }
            return Strings.ToString();
        }

        public static String GetJournalDesParentId(string id)
        {
            if (id.Length < 3) return "";
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select ParentId from OL_Destination where id in (0{0}0) group by ParentId", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append(",");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("{0},", DS.Tables[0].Rows[i]["ParentId"].ToString()));
                }
            }
            return Strings.ToString();
        }

        private static string PlaceAllJournalList(string LineType, string id)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(LineType);

            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                string intro = "";
                //string regexstr = @"<img[^>]*>";// @"<[^>]*>";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
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
                    intro = MyConvert.ParseTags(DS.Tables[0].Rows[i]["contents"].ToString());
                    //intro = Regex.Replace(DS.Tables[0].Rows[i]["contents"].ToString(), regexstr, string.Empty, RegexOptions.IgnoreCase);

                    if (intro.Length > 95) intro = intro.Substring(0, 95) + "...";

                    Strings.Append("<div class=\"csingle_sight\">");
                    Strings.Append(string.Format("<div class=\"cityimg\"><a href=\"/showjournal/{0}.html\" target=\"_blank\"><img src=\"/images/none.gif\" data-original=\"{1}\"></a></div>", DS.Tables[0].Rows[i]["id"].ToString(), ThumbSrc));
                    Strings.Append("<dl>");
                    Strings.Append(string.Format("<dt><a href=\"/showjournal/{0}.html\" target=\"_blank\">{1}</a></dt><dd class=\"content\">{2}</dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["title"].ToString(), intro));
                    Strings.Append(string.Format("<dd class=user>{0} <i>{1:yyyy-MM-dd}</i></dd>", DS.Tables[0].Rows[i]["NewsUser"].ToString(), DS.Tables[0].Rows[i]["inputdate"]));
                    Strings.Append("</dl></div>");
                }
            }
            return Strings.ToString();
        }

        private static string PlaceAllViewList(string LineType, string id)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(LineType);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                Strings.Append("<ul>");
                string intro = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    {
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }
                    intro = DS.Tables[0].Rows[i]["intro"].ToString();
                    if (intro.Length > 36) intro = intro.Substring(0, 33) + "...";
                    Strings.Append("<li>");
                    Strings.Append(string.Format("<a href=\"/sightdetail/{0}.html\" target=\"_blank\"><img src=\"/images/none.gif\" data-original=\"{1}\" alt=\"{2}\"></a>", DS.Tables[0].Rows[i]["id"].ToString(), ThumbSrc, DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<dl><dt><i class=\"sight\"></i><a href=\"/sightdetail/{0}.html\" target=\"_blank\">{1}</a></dt>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<dd>{0}</dd></dl></li>", intro));
                }
                Strings.Append("</ul>");
            }
            return Strings.ToString();
        }

        private static string PlaceSightList(string LineType, string id)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "";
            SqlQueryText = "select id,desid,viewname from OL_View where desid in (select id from OL_Destination where ParentId='" + id + "')";
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(LineType);
            
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    {
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }
                    
                    Strings.Append("<div class=\"csingle_sight\">");
                    Strings.Append(string.Format("<div class=\"cityimg\"><a href=\"/place/{0}.html\" target=\"_blank\"><img src=\"/images/none.gif\" data-original=\"{1}\" alt=\"{2}\"></a><i></i><span>{2}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), ThumbSrc, DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                    Strings.Append("<dl>");
                    Strings.Append(string.Format("<dt><a href=\"/place/{0}.html\" target=\"_blank\">{1}</a></dt><dd class=\"ellipsis\">推荐景点：{2}</dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString(), GetViewName(DS.Tables[0].Rows[i]["id"].ToString(), DS1.Tables[0])));
                    Strings.Append(string.Format("<dd><a href=\"/sight/{0}.html\" target=\"_blank\">{1}所有景点</a></dd>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                    Strings.Append("</dl></div>");
                }
            }
            return Strings.ToString();
        }

        public static string GetViewName(string ids, DataTable dt)
        {
            int rows = 0;
            string visit = "";
            DataRow[] drs = dt.Select("desid='" + ids + "'");
            foreach (DataRow dr in drs)
            {
                if (rows < 9)
                {
                    visit += string.Format("<a href=\"/sightdetail/{0}.html\" target=\"_blank\">{1}</a>、", dr["id"].ToString(), dr["viewname"].ToString());
                }
                rows += 1;
            }

            if (visit.Length>5) visit = visit.Substring(0, visit.Length - 1);
            return visit;
        }

        private static string SightRecommend(string id, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,viewname,left(intro,40) as intro,(select top 1 picurl from OL_ViewPic where viewid=OL_View.id order by newId()) as picurl from OL_View where desid in (select id from OL_Destination where ClassPath='{1}' or ClassPath like '{1},%') order by newId()", RowNums, id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                Strings.Append("<ul>");
                string intro = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    {
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }
                    intro = DS.Tables[0].Rows[i]["intro"].ToString();
                    if (intro.Length > 36) intro = intro.Substring(0, 34) + "...";
                    Strings.Append("<li>");
                    Strings.Append(string.Format("<a href=\"/sightdetail/{0}.html\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\"></a>", DS.Tables[0].Rows[i]["id"].ToString(), ThumbSrc, DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<dl><dt><i class=\"sight\"></i><a href=\"/sightdetail/{0}.html\" target=\"_blank\">{1}</a></dt>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<dd>{0}</dd></dl></li>", intro));
                }
                Strings.Append("</ul>");
            }
            return Strings.ToString();
        }


        private static string PlaceViewList(string id, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,viewname,(select top 1 picurl from OL_ViewPic where viewid=OL_View.id) as picurl from OL_View where desid='{1}'", RowNums, id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    {
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }

                    Strings.Append("<div class=\"product_zoomfix\">");
                    Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/sightdetail/{0}.html\" >", DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", ThumbSrc, DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["viewname"].ToString()));
                    Strings.Append("</a></div>");
                }
            }
            return Strings.ToString();
        }

        private static string PlaceJournalList(string id, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,title,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl from OL_Journal where flag='1' and (Destinationid like '%,{1},%' or parentid like '%,{1},%')", RowNums, id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count == 0)
            //{
            //    SqlQueryText = string.Format("select top {0} id,title,(select uploadname from OL_JournalImg where id=OL_Journal.coverid) as coverpicurl,(select top 1 uploadname from OL_JournalImg where journalid=OL_Journal.uid) as picurl from OL_Journal where FirstDestination in (select id from OL_Destination where (id={1} or ParentId={1}))", RowNums, id);
            //    DS.Clear();
            //    DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //}
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
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
                    //Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["title"].ToString()));
                    Strings.Append(string.Format("<span class=\"name_wrap\"><span class=\"product_name\" title=\"{0}\">{0}</span></span>", DS.Tables[0].Rows[i]["title"].ToString()));
                    Strings.Append("</a></div>");
                }
            }
            return Strings.ToString();
        }

        private static string PlaceList(string id, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select top {0} id,DestinationName,(select top 1 picurl from OL_ViewPic where desid=OL_Destination.id) as picurl from OL_Destination where ParentId='{1}'", RowNums, id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ThumbSrc = "";
                string[] sArray;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ThumbSrc = "/images/none.gif";
                    if (DS.Tables[0].Rows[i]["picurl"].ToString().Length > 10)
                    { 
                        sArray = DS.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        ThumbSrc = string.Format("/Upload/View/{0}/{1}/S_{2}", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                    }

                    Strings.Append("<div class=\"product_zoomfix\">");
                    Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/place/{0}.html\" >", DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", ThumbSrc, DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                    Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong></span>", DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                    Strings.Append("</a></div>");
                }
            }
            return Strings.ToString();
        }
        
        private static string PlaceLineList(string LineType, string Id, int RowNums)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "";
            string Pics = "/Images/none.gif";
            //string mp_pic = "";

            SqlQueryText = string.Format("SELECT top {0} MisLineId,LineName,Price,Pics,BigPics,IndexRecom,famous,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination, '' as Dest FROM OL_Line where Sale='0' and Price>0 and PlanDate>'{1}'", RowNums, DateTime.Today.ToString());
            SqlQueryText += " and LineType<>'Visa' and Destinationid like '%," + Id + ",%' order by newId()";

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

                    Strings.Append("<div class=\"product_zoomfix\">");
                    Strings.Append(string.Format("<a class=\"product\" target=\"_blank\" href=\"/line/{0}.html\" >", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                    Strings.Append(string.Format("<img src=\"/images/none.gif\" data-original=\"{0}\" alt=\"{1}\">", Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                    
                    //mp_pic = "";
                    Strings.Append(string.Format("<span class=\"price_box\"><strong class=\"place\">{0}</strong> <strong class=\"price\"><dfn>&#165;</dfn>{1}<em>起</em></strong></span>", DS.Tables[0].Rows[i]["Dest"].ToString(), DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    Strings.Append("<span class=\"name_wrap\">");
                    //Strings.Append(string.Format("<span class=\"product_explain\">{0}</span>", mp_pic));
                    Strings.Append(string.Format("<span class=\"product_name\" title=\"{0}\">{0}</span>", DS.Tables[0].Rows[i]["LineName"].ToString()));
                    Strings.Append("</span></a></div>");
                }
            }
            return Strings.ToString();
        }
    }
}