using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Class.Travel
{
    public class LineClass
    {
        //浏览计数
        public static void LinePageViewCount(string LineId)
        {
            string SqlQueryText = string.Format("update OL_Line set PV=PV+1 where MisLineId='{0}'", LineId);
            MyDataBaseComm.ExcuteSql(SqlQueryText);
        }

        public static void ClearCache(string LineClass)
        {
            HttpContext.Current.Cache.Insert(string.Format("LineDestination{0}", LineClass), "");
        }

        public static void ClearRecommendCache(string LineClass)
        {
            HttpContext.Current.Cache.Insert(string.Format("LineRecommendSort{0}", LineClass), "");
            HttpContext.Current.Cache.Insert(string.Format("LineSpecialRecommend{0}", LineClass), "");
            HttpContext.Current.Cache.Insert(string.Format("LineListSpecialRecommend{0}", LineClass), "");
        }

        //线路列表js生成（首页和二级页面）
        public static void CreateLineList(string LineType, string LineClass)
        {
            StringBuilder Strings1 = new StringBuilder();//首页
            StringBuilder Strings2 = new StringBuilder();//二级页面

            Strings1.Append("GetProductList({\"Result\":\"");
            Strings1.Append("<div class=pleft>");
            Strings1.Append(LineSpecialRecommendCache(LineClass));
            Strings1.Append(LineDestinationCache(LineType, LineClass));
            Strings1.Append("</div>");

            Strings2.Append("GetProductList({\"Result\":\"");
            Strings2.Append(LineListSpecialRecommend(LineClass));

            Strings1.Append("<div class=pright><div class=pright-list><UL>");
            Strings2.Append("<div class=pright-op><div class=pright-list><UL>");
            //and Recommend=0 and PlanDate>'{1}' 
            string SqlQueryText = string.Format("select top 10 MisLineId,LineType,LineName,Price,LineFeature,Tags from OL_Line where Sale='0' and LineClass='{0}' and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            string tags;
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                tags = "";
                if (DS.Tables[0].Rows[i]["Tags"].ToString().Length > 5)
                {
                    try
                    {
                        string[] arr = DS.Tables[0].Rows[i]["Tags"].ToString().Split(',');
                        for (int l = 0; l < arr.Length - 1; l++)
                        {
                            if (arr[l].ToString().Length > 2) tags += "<IMG class=pictags src=\\\"/images/" + arr[l].ToString() + ".jpg\\\">";
                        }
                    }
                    catch
                    { }
                }

                Strings1.Append(string.Format("<LI><DIV class=p-name>·<A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3} {5}</A></DIV><div class=pl-price>￥{4}</div></LI>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"], DS.Tables[0].Rows[i]["Price"], tags));
                Strings2.Append(string.Format("<LI><DIV class=p-sname>·<A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3} {6}</A><SPAN>{4}</SPAN></DIV><div class=pp-price>￥{5}</div></LI>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"], DS.Tables[0].Rows[i]["LineFeature"].ToString(), DS.Tables[0].Rows[i]["Price"], tags));

            }

            Strings1.Append(string.Format("<LI><DIV class=p-name>&nbsp</DIV><div class=pp-price><A class=f12 href=\\\"/{0}/{1}-0.html\\\">更多线路 &gt;&gt;</A></div></LI>", LineType, LineClass));
                
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    Strings1.Append(string.Format("<LI><DIV class=p-name>·<A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3}</A></DIV><div class=pl-price>￥{4}</div></LI>", DS.Tables[0].Rows[0]["LineType"], LineClass, DS.Tables[0].Rows[0]["MisLineId"], DS.Tables[0].Rows[0]["LineName"], DS.Tables[0].Rows[0]["Price"]));
            //    Strings2.Append(string.Format("<LI><DIV class=p-sname>·<A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3}</A><SPAN>{4}</SPAN></DIV><div class=pp-price>￥{5}</div></LI>", DS.Tables[0].Rows[0]["LineType"], LineClass, DS.Tables[0].Rows[0]["MisLineId"], DS.Tables[0].Rows[0]["LineName"], DS.Tables[0].Rows[0]["LineFeature"].ToString(), DS.Tables[0].Rows[0]["Price"]));
            //}
            Strings1.Append("</UL></div></div>");
            Strings1.Append(string.Format("\",\"ReferenceType\":\"{0}\"", LineClass));
            Strings1.Append("})");

            Strings2.Append("</UL></div></div>");
            Strings2.Append(string.Format("\",\"ReferenceType\":\"{0}\"", LineClass));
            Strings2.Append("})");

            SaveScriptToFile.SaveScript(Strings1.ToString(), "ProductList", LineClass);
            SaveScriptToFile.SaveScript(Strings2.ToString(), "ProductDetail", LineClass);
        }

        public static void CreateVisaList(string LineClass)
        {
            StringBuilder Strings1 = new StringBuilder();
            
            Strings1.Append("GetProductList({\"Result\":\"");
            Strings1.Append("<div class=\\\"m plist\\\">");
            Strings1.Append("<div class=mc>");
            Strings1.Append("<ul class=list-h> ");

            string Pic="";
            string Url = "";
            string LineFeature = "";
            string SqlQueryText = string.Format("select top 4 MisLineId,Pics,LineType,LineName,Price,LineFeature from OL_Line where Sale='0' and LineClass='{0}' and Price>0 order by EditTime desc", LineClass);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                LineFeature = "";
                Pic = string.Format("\\\"/images/shadow/{0}\\\"", DS.Tables[0].Rows[i]["Pics"].ToString());
                Url = string.Format("\\\"/Visa/{0}/{1}.html\\\"", LineClass, DS.Tables[0].Rows[i]["MisLineId"]);
                if (DS.Tables[0].Rows[i]["LineFeature"].ToString().Length > 1) LineFeature = string.Format("<FONT color=#409115>{0}</FONT>", DS.Tables[0].Rows[i]["LineFeature"].ToString());
                Strings1.Append(string.Format("<LI><DIV class=p-img><A href={0} target=_blank><IMG src={1} width=130 height=100></A></DIV><DIV class=p-name><A href={0} target=_blank>{2} {3}</A></DIV><DIV class=p-price>青旅价：<STRONG>￥{4}</STRONG></DIV></LI>", Url, Pic, DS.Tables[0].Rows[i]["LineName"], LineFeature, DS.Tables[0].Rows[i]["Price"]));
            }
            Strings1.Append("</UL></div></div>");
            Strings1.Append(string.Format("\",\"ReferenceType\":\"{0}\"", LineClass));
            Strings1.Append("})");
            SaveScriptToFile.SaveScript(Strings1.ToString(), "ProductDetail", LineClass);            
        }

        public static string LineSpecialRecommendCache(string LineClass)
        {
            //string.Format("LineSpecialRecommend{0}", LineClass) string.Format("LineListSpecialRecommend{0}", LineClass)
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineSpecialRecommend{0}", LineClass)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=p-title_tj>特别推荐</div>");//and PlanDate>'{1}' 
                string SqlQueryText = string.Format("select top 1 LineType,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineClass='{0}' and Recommend=1 and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string Pics = "/Images/none.gif";
                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Strings.Append(string.Format("<div class=p-img><A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank><IMG onerror=\\\"this.src='/Images/none.gif'\\\" src=\\\"{3}\\\" width=150></A></div>", DS.Tables[0].Rows[0]["LineType"], LineClass, DS.Tables[0].Rows[0]["MisLineId"], Pics));
                    Strings.Append(string.Format("<div class=p-names><A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3}</A></div>", DS.Tables[0].Rows[0]["LineType"], LineClass, DS.Tables[0].Rows[0]["MisLineId"], DS.Tables[0].Rows[0]["LineName"]));
                    Strings.Append(string.Format("<div class=p-price>青旅价：<STRONG>￥{0}</STRONG></div>", DS.Tables[0].Rows[0]["Price"].ToString()));
                }
                HttpContext.Current.Cache.Insert(string.Format("LineSpecialRecommend{0}", LineClass), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineSpecialRecommend{0}", LineClass)]);
        }

        public static string LineListSpecialRecommend(string LineClass)
        {
            //string.Format("LineListSpecialRecommend{0}", LineClass)
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineListSpecialRecommend{0}", LineClass)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=pleft>");//and PlanDate>'{1}' 
                string SqlQueryText = string.Format("select top 2 LineType,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineClass='{0}' and Recommend=1 and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    string Pics = "/Images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length ==24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Strings.Append(string.Format("<div class=p-img><A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank><IMG onerror=\\\"this.src='/Images/none.gif'\\\" src=\\\"{3}\\\" width=150></A></div>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], Pics));
                    Strings.Append(string.Format("<div class=p-names><A href=\\\"/{0}/{1}/{2}.html\\\" target=_blank>{3}</A></div>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"]));
                    Strings.Append(string.Format("<div class=p-price>青旅价：<STRONG>￥{0}</STRONG></div>", DS.Tables[0].Rows[i]["Price"].ToString()));
                }
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineListSpecialRecommend{0}", LineClass), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineListSpecialRecommend{0}", LineClass)]);
        }

        public static string LineDestinationCache(string LineType, string LineClass)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineDestination{0}", LineClass)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                //MyDataBaseComm.getScalar(SqlQueryText) string.Format("LineDestination{0}", LineClass)
                string classname = Convert.ToString(MyDataBaseComm.getScalar(string.Format("select top 1 ProductName from OL_ProductType where ProductName='{0}'", LineClass)));
                Strings.Append(string.Format("<div class=p-list><div class=p-title>{0}目的地</div>", classname));
                string SqlQueryText = string.Format("SELECT ProductName,MisClassId FROM OL_ProductClass where ParentId in (select id from OL_ProductClass where ProductName='{0}') order by ParentId,ProductSort", LineClass);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<div class=arealist><DL><DD>");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<EM><A href=\\\"/{0}/{1}-{2}.html\\\">{3}</A></EM>", LineType, LineClass, DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS.Tables[0].Rows[i]["ProductName"].ToString()));
                    }
                    Strings.Append("</DD></DL></div>");
                }
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineDestination{0}", LineClass), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineDestination{0}", LineClass)]);
        }
    }
}