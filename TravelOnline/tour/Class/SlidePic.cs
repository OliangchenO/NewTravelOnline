using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;

namespace TravelOnline.tour
{
    public class SlidePic
    {
        public static void GreateHotAreaCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 10 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            if (AdFlag == "FirendLink") SqlQueryText = string.Format("select LinkUrl as AdPageUrl,LinkName as AdName from OL_FriendLink order by rankid desc", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append(string.Format("<a href=\"{0}\" target=_blank>{1}</a>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void FirendLinkCache(string AdFlag, string LinkType)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select LinkUrl as AdPageUrl,LinkName as AdName from OL_FriendLink where LinkType='{0}' order by rankid desc", LinkType);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append(string.Format("<a href=\"{0}\" target=_blank>{1}</a>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void GreateHotTextCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 4 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append(string.Format("<li><a href=\"{0}\" target=_blank>{1}</a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void GreateSlideCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 8 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<li _src="url(/img/01.jpg)" style="background:#E2025E center 0 no-repeat;"><a target="_blank" href="http://www.16sucai.com"></a></li>
                
                AdScriptr.Append("<li _src=\"url(");
                AdScriptr.Append(DS.Tables[0].Rows[i]["AdPicUrl"].ToString());
                AdScriptr.Append(")\"");
                if (DS.Tables[0].Rows[i]["BackGround"].ToString().Length == 6)
                {
                    AdScriptr.Append(" style=\"background:#" + DS.Tables[0].Rows[i]["BackGround"].ToString() + " center 0 no-repeat;\"");
                }
                else
                {
                    AdScriptr.Append(" style=\"background:#F7F7F7 center 0 no-repeat;\"");
                }
                AdScriptr.Append("><a id=\"" + DS.Tables[0].Rows[i]["AdName"].ToString() + "\" target=\"_blank\" href=\"");
                AdScriptr.Append(DS.Tables[0].Rows[i]["AdPageUrl"].ToString());
                AdScriptr.Append("\"></a></li>");
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }


        public static void GreateSmallSlideCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 5 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("<li _src=\"url(");
                AdScriptr.Append(DS.Tables[0].Rows[i]["AdPicUrl"].ToString());
                AdScriptr.Append(")\" <a target=\"_blank\" href=\"");
                AdScriptr.Append(DS.Tables[0].Rows[i]["AdPageUrl"].ToString());
                AdScriptr.Append("\"></a></li>");
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void GreateAnnouncementCache()
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = "select top 5 * from OL_Affiche order by EditTime desc";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<li><a href="#">测试文字</a>1</li>
                AdScriptr.Append(string.Format("<li><a href=\"/showinfo/{0}.html\" target=_blank>{1}</a></li>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["AfficheName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("Announcement", AdScriptr.ToString());
        }

        public static void GreateCitieCache()
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 7 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", "Citie");
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<li><a href="#">测试文字</a>1</li>
                if (i == 0) AdScriptr.Append(string.Format("<li class=citie_01><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"300px\" height=\"100px\" /><img src=\"{2}\" width=\"300px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 1) AdScriptr.Append(string.Format("<li class=citie_02><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"140px\" height=\"100px\" /><img src=\"{2}\" width=\"140px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 2) AdScriptr.Append(string.Format("<li class=citie_03><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"300px\" height=\"100px\" /><img src=\"{2}\" width=\"300px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 3) AdScriptr.Append(string.Format("<li class=citie_04><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"300px\" height=\"100px\" /><img src=\"{2}\" width=\"300px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 4) AdScriptr.Append(string.Format("<li class=citie_05><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"300px\" height=\"100px\" /><img src=\"{2}\" width=\"300px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 5) AdScriptr.Append(string.Format("<li class=citie_06><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"140px\" height=\"100px\" /><img src=\"{2}\" width=\"140px\" height=\"100px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                if (i == 6) AdScriptr.Append(string.Format("<li class=citie_07><a href=\"{0}\"><img class=citie_img src=\"{1}\" width=\"225px\" height=\"205px\" /><img src=\"{2}\" width=\"225px\" height=\"205px\"/></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_Citie", AdScriptr.ToString());
        }

        public static void GreateThreePicCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 3 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            if (AdFlag == "Partner") SqlQueryText = string.Format("select * from OL_FlashAD where AdFlag='{0}' order by AdSort", AdFlag);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<li><div class="ulike_r2_item"><a target="_blank" href="http://" title="AOC"><img src="http://img1.icson.com/ICSONAD/201309/1_big20130924160740750113.jpg" alt="AOC"></a></div></li>
                if (AdFlag == "Partner")
                {
                    AdScriptr.Append(string.Format("<li><a title=\"{2}\" target=\"_blank\" href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"></a></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                }
                else
                {
                    AdScriptr.Append(string.Format("<li><div class=\"ulike_r2_item\"><a target=\"_blank\" href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"></a></div></li>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                }
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void GreateTongLanCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 1 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<a target="_blank" href="http://" title="AOC"><img src="/img/21122.jpg" alt="明基DQ1" /></a>
                AdScriptr.Append(string.Format("<a target=\"_blank\" href=\"{0}\" ><img src=\"{1}\" alt=\"{2}\" /></a>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }

        public static void GreateShipRecommCache(string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            string SqlQueryText = string.Format("select top 2 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //<div class=\"boatpic\"><a href=\"javascript:\" title=""><img src=\"/img/boat.jpg\" alt=\"\" /></a></div>
                AdScriptr.Append(string.Format("<div class=\"boatpic\"><a target=\"_blank\" href=\"{0}\" ><img src=\"{1}\" alt=\"{2}\" /></a></div>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert("SlidePic_" + AdFlag, AdScriptr.ToString());
        }
    }
}