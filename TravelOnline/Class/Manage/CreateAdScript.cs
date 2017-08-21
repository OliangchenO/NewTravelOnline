using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.IO;

namespace TravelOnline.Class.Manage
{
    public class CreateAdScript
    {
        public static void AdScriptCreateNow(string AdFlag)
        {
            string SqlQueryText = "";
            switch (AdFlag)
            {
                case "Index":
                    SqlQueryText = string.Format("select top 6 * from OL_FlashAD where AdFlag='{0}' order by AdSort", AdFlag);
                    break;
                case "Topic":
                    HttpContext.Current.Cache.Insert("LineDetailSortCache", "");
                    SqlQueryText = string.Format("select * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", AdFlag);
                    break;
                default:
                    SqlQueryText = string.Format("select top 10 * from OL_FlashAD where AdFlag='{0}' order by id desc", AdFlag);
                    break;
            }
            if (SqlQueryText.Length > 10) SelectFromAd(SqlQueryText, AdFlag);
        }

        public static void SelectFromAd(string SqlQueryText, string AdFlag)
        {
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            SqlQueryText = "";
            if (DS.Tables[0].Rows.Count > 0)
            {
                switch (AdFlag)
                {
                    case "Index":
                        SqlQueryText = IndexAdScript(DS);
                        break;
                    case "OutBound":
                        SqlQueryText = OutBoundAdScript(DS);
                        break;
                    case "InLand":
                        SqlQueryText = OutBoundAdScript(DS);
                        break;
                    case "FreeTour":
                        SqlQueryText = OutBoundAdScript(DS);
                        break;
                    case "Cruises":
                        SqlQueryText = OutBoundAdScript(DS);
                        break;
                    case "Visa":
                        SqlQueryText = OutBoundAdScript(DS);
                        break;
                    case "Topic":
                        CreateTopicTravelCache(DS);
                        break;
                    default:
                        string ads = AdFlag.Substring(0, 2);
                        if (ads == "IC")
                        {
                            SqlQueryText = BannerAdScript2(DS, AdFlag);
                        }
                        else
                        {
                            SqlQueryText = BannerAdScript(DS, AdFlag);
                        }
                        break;
                }
            }
            if (SqlQueryText.Length > 10)
            {
                //SaveAdScript(SqlQueryText, AdFlag);
                SaveScriptToFile.SaveScript(SqlQueryText, "AD", AdFlag);
            }
            DS.Clear();
            DS.Dispose();  
        }

        //private static void SaveAdScript(string AdString, string AdFlag)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\AD\" + AdFlag + ".js";

        //    try
        //    {
        //        StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
        //        writer.WriteLine(AdString);
        //        writer.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        AdString = exception.Message;
        //    }
        //}

        public static string IndexAdScript(DataSet AdDate)
        {
            StringBuilder AdScriptr = new StringBuilder();
            AdScriptr.Append("$(\"#slide\").jdSlide({width: (screen.width >= 1200) ? 766 : 546,height: 270,type: \"string\",pics: [");

            for (int i = 0; i < AdDate.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("{src: (screen.width >= 1200) ? \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPicUrl"].ToString());
                AdScriptr.Append("\" : \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdSecPicUrl"].ToString());
                AdScriptr.Append("\",href: \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPageUrl"].ToString());
                AdScriptr.Append("\",alt: \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdName"].ToString());
                AdScriptr.Append("\",breviary: \"#\",type: \"img\"}");
                if (i < (AdDate.Tables[0].Rows.Count - 1))
                {
                    AdScriptr.Append(" , ");
                }
            }
            AdScriptr.Append("]})");
            AdDate.Clear();
            AdDate.Dispose();
            return AdScriptr.ToString();
        }

        public static string OutBoundAdScript(DataSet AdDate)
        {
            StringBuilder AdScriptr = new StringBuilder();
            AdScriptr.Append("$(\"#slide\").jdSlide({width: (screen.width >= 1200) ? 766 : 546,height:200,pics: [");
            for (int i = 0; i < AdDate.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("{src: (screen.width >= 1200) ? \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPicUrl"].ToString());
                AdScriptr.Append("\" : \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdSecPicUrl"].ToString());
                AdScriptr.Append("\",");
                AdScriptr.Append("href: \"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPageUrl"].ToString());
                AdScriptr.Append("\",alt: \"\",breviary: \"#\",type: \"img\"}");
                if (i < (AdDate.Tables[0].Rows.Count - 1))
                {
                    AdScriptr.Append(" , ");
                }
            }
            AdScriptr.Append("]})");
            AdDate.Clear();
            AdDate.Dispose();
            return AdScriptr.ToString();
        }

        public static string BannerAdScript(DataSet AdDate, string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            AdScriptr.Append("(function () { var ads = [");
            for (int i = 0; i < AdDate.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("{");
                AdScriptr.Append(string.Format("width: 211,width2: 0,height: 90,url: \"{0}\",url2: \"\",alt: \"{1}\",link: \"{2}\"", AdDate.Tables[0].Rows[i]["AdPicUrl"].ToString(), AdDate.Tables[0].Rows[i]["AdName"].ToString(), AdDate.Tables[0].Rows[i]["AdPageUrl"].ToString()));
                AdScriptr.Append("}");
                if (i < (AdDate.Tables[0].Rows.Count - 1))
                {
                    AdScriptr.Append(",");
                }
            }
            AdScriptr.Append("];var rate = [1];asyncScript(function () {");
            AdScriptr.Append(string.Format("setRandomAds(ads, rate, \"{0}\", false);", AdFlag));
            AdScriptr.Append("})})();");
            AdDate.Clear();
            AdDate.Dispose();
            return AdScriptr.ToString();
        }

        public static string BannerAdScript2(DataSet AdDate, string AdFlag)
        {
            StringBuilder AdScriptr = new StringBuilder();
            AdScriptr.Append("(function () { var ads = [");
            for (int i = 0; i < AdDate.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("{");
                AdScriptr.Append(string.Format("width: 546,width2: 766,height: 120,url: \"{0}\",", AdDate.Tables[0].Rows[i]["AdSecPicUrl"].ToString()));
                AdScriptr.Append(string.Format("url2: \"{0}\",alt: \"{1}\",link: \"{2}\"", AdDate.Tables[0].Rows[i]["AdPicUrl"].ToString(), AdDate.Tables[0].Rows[i]["AdName"].ToString(), AdDate.Tables[0].Rows[i]["AdPageUrl"].ToString()));
                AdScriptr.Append("}");
                if (i < (AdDate.Tables[0].Rows.Count - 1))
                {
                    AdScriptr.Append(",");
                }
            }
            AdScriptr.Append("];var rate = [1];asyncScript(function () {");
            AdScriptr.Append(string.Format("setRandomAds(ads, rate, \"{0}\", true);", AdFlag));
            AdScriptr.Append("})})();");
            AdDate.Clear();
            AdDate.Dispose();
            return AdScriptr.ToString();
        }

        public static void CreateTopicTravelCache(DataSet AdDate)
        {
            StringBuilder AdScriptr = new StringBuilder();
            for (int i = 0; i < AdDate.Tables[0].Rows.Count; i++)
            {
                AdScriptr.Append("<LI><A href=\"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPageUrl"].ToString());
                AdScriptr.Append("\" target=_blank><IMG alt=");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdName"].ToString());
                AdScriptr.Append(" src=\"");
                AdScriptr.Append(AdDate.Tables[0].Rows[i]["AdPicUrl"].ToString());
                AdScriptr.Append("\" width=88 height=31></A></LI>");
            }
            AdDate.Clear();
            AdDate.Dispose();
            HttpContext.Current.Cache.Insert("IndexTopicTravelHtml", AdScriptr.ToString());
        }
    }
}