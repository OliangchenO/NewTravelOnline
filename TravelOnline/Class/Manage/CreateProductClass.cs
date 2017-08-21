using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.IO;

namespace TravelOnline.Class.Manage
{
    public class CreateProductClass
    {
        public class CreateString
        {
            public string String1 { get; set; }
            public string String2 { get; set; }
        }

        public static void ClearCache(string Flag)
        {
            HttpContext.Current.Cache.Insert(string.Format("Index{0}Html", Flag), "");
            HttpContext.Current.Cache.Insert(string.Format("{0}ProductHtml", Flag), "");

            //if (Flag == "InLand" || Flag == "OutBound")
            //{ 
            //}
        }

        public static void ProductClassCreateNow(string Flag)
        {
            string SqlQueryText = "";
            SqlQueryText = string.Format("select * from OL_ProductType where ProductType='{0}' order by ProductSort", Flag);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            CreateString strings = new CreateString();
            strings.String1 = "<UL class=tab></UL>";
            if (DS.Tables[0].Rows.Count > 0)
            {
                strings = CreateClass(DS, Flag);
            }
            DS.Clear();
            DS.Dispose();
            HttpContext.Current.Cache.Insert(string.Format("Index{0}Html", Flag), strings.String1);
            HttpContext.Current.Cache.Insert(string.Format("{0}ProductHtml", Flag), strings.String2);
        }

        private static CreateString CreateClass(DataSet Datas, string Flag)
        {
            StringBuilder Script1 = new StringBuilder();
            StringBuilder Script2 = new StringBuilder();
            StringBuilder Script3 = new StringBuilder();

            StringBuilder JsScript = new StringBuilder();

            CreateString strings = new CreateString();

            string ClassName = "";

            if (Flag == "Visa")
            {
                ClassName = "";
            }
            else if (Flag == "FreeTour")
            {
                ClassName = "";
            }
            else
            {
                ClassName = "专区";
            }


            string class1, class2;
            for (int i = 0; i < Datas.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    class1 = "class=curr ";
                    class2 = "class=\"mc tabcon\"";
                }
                else
                {
                    class1 = "";
                    class2 = "class=\"mc tabcon hide\"";
                }
                //首页产品列表
                Script1.Append(string.Format("<LI {0}data=\"{1}\"><SPAN></SPAN>{2}</LI>", class1, Datas.Tables[0].Rows[i]["MisClassId"].ToString(), Datas.Tables[0].Rows[i]["ProductName"].ToString()));
                Script2.Append(string.Format("<DIV id=\"{0}\" {1}><DIV class=iloading>正在加载中，请稍候...</DIV></DIV>", Datas.Tables[0].Rows[i]["MisClassId"].ToString(), class2));
                
                //二级页面产品列表
                Script3.Append(string.Format("<DIV class=special_t class=mt><H2>{0}{3}</H2><DIV class=extra><A href=\"/{2}/{1}-0.html\">更多&gt;&gt;</A></DIV></DIV>", Datas.Tables[0].Rows[i]["ProductName"].ToString(), Datas.Tables[0].Rows[i]["MisClassId"].ToString(), Flag, ClassName));
                Script3.Append(string.Format("<DIV class=\"m special\"><DIV id=\"{0}\" class=mc><DIV class=iloading>正在加载中，请稍候...</DIV></DIV></DIV>", Datas.Tables[0].Rows[i]["MisClassId"].ToString()));
            
                //加载脚本
                JsScript.Append("mlazyload({defObj: \"#");
                JsScript.Append(Datas.Tables[0].Rows[i]["MisClassId"].ToString());
                JsScript.Append("\",fn: function () {url = \"/Js/ProductDetail/");
                JsScript.Append(Datas.Tables[0].Rows[i]["MisClassId"].ToString());
                JsScript.Append(".js\";$.getJSONP(url, GetProductList);}});");
            }
            Datas.Clear();
            Datas.Dispose();
            SaveScriptToFile.SaveScript(JsScript.ToString(), "ProductDetail", Flag);
            strings.String1= string.Format("<UL class=tab>{0}</UL>{1}", Script1.ToString(), Script2.ToString());
            strings.String2 = Script3.ToString();
            return strings;
        }

        public static string IndexFreeTourCreate()
        {
            if (Convert.ToString(HttpContext.Current.Cache["IndexFreeTour"]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText;
                SqlQueryText = string.Format("select top 13 LineType,LineClass,Preferences,Recommend,LineFeature,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineType='FreeTour' and Price>0 and PlanDate>='{0}' order by PV desc, EditTime desc", DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                string Pic = "";
                string Url = "";
                string LineFeature = "";
                string Colors = "#000000";
                    
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    LineFeature = "";
                    Pic = "/Images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pic = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Url = string.Format("\"/FreeTour/{0}/{1}.html\"", DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["MisLineId"]);
                    if (DS.Tables[0].Rows[i]["LineFeature"].ToString().Length > 1) LineFeature = string.Format("<FONT color=#409115>{0}</FONT>", DS.Tables[0].Rows[i]["LineFeature"].ToString());
                    Colors = "#000000";
                    if (DS.Tables[0].Rows[i]["Preferences"].ToString() == "1") Colors = "#ff6600";
                    if (DS.Tables[0].Rows[i]["Recommend"].ToString() == "1") Colors = "green";
                    if (i < 4)
                    {
                        if (i == 0) Strings.Append("<UL class=madding-2>");
                        Strings.Append(string.Format("<LI><A href={0} target=_blank><IMG onerror=\"this.src='/Images/none.gif'\" src={1} width=80 height=60><DIV>{2}</DIV><DIV>{3}</DIV></A><div class=pl-price>￥{4}</div></LI>", Url, Pic, DS.Tables[0].Rows[i]["LineName"], LineFeature, DS.Tables[0].Rows[i]["Price"]));
                        if (i == 1) Strings.Append("</UL><UL class=madding-2>");
                        if (i == 3) Strings.Append("</UL><SPAN class=clr></SPAN><UL class=\"mc list-h madding-3\">");
                    }
                    else
                    {
                        Strings.Append(string.Format("<LI>·<A href={0} target=_blank><FONT color={1}>{2}</FONT></A></LI>", Url, Colors, DS.Tables[0].Rows[i]["LineName"]));
                    }
                }
                Strings.Append("</UL>");
                HttpContext.Current.Cache.Insert("IndexFreeTour", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["IndexFreeTour"]);
        }

        public static string IndexCruisesCreate()
        {
            if (Convert.ToString(HttpContext.Current.Cache["IndexCruises"]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText;
                SqlQueryText = string.Format("select top 3 LineFeature,LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineType='Cruises' and Recommend='1' and Price>0 and PlanDate>='{0}' order by PV desc, EditTime desc", DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                string Pic = "";
                string Url = "";
                string LineFeature = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    LineFeature = "";
                    Pic = "/Images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pic = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Url = string.Format("\"/Cruises/{0}/{1}.html\"", DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"]);
                    if (DS.Tables[0].Rows[i]["LineFeature"].ToString().Length > 1) LineFeature = string.Format("<FONT color=#ff6600>{0}</FONT>", DS.Tables[0].Rows[i]["LineFeature"].ToString());
                    Strings.Append(string.Format("<LI><DIV class=p-img><A href={0} target=_blank><IMG onerror=\"this.src='/Images/none.gif'\" src={1} width=150 height=113></A></DIV><DIV class=p-name><A href={0} target=_blank>{2} {3}</A></DIV><DIV class=p-price>青旅价：<STRONG>￥{4}</STRONG></DIV></LI>", Url, Pic, DS.Tables[0].Rows[i]["LineName"], LineFeature, DS.Tables[0].Rows[i]["Price"]));
                }
                HttpContext.Current.Cache.Insert("IndexCruises", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["IndexCruises"]);
        }

        public static string IndexVisaCreate()
        {
            if (Convert.ToString(HttpContext.Current.Cache["IndexVisa"]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText;
                SqlQueryText = "select top 6 LineFeature,LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineType='Visa' and Recommend='1' and Price>0 order by PV desc, EditTime desc";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                string Pic = "";
                string Url = "";
                string LineFeature = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    LineFeature = "";
                    Pic = string.Format("\"/images/shadow/{0}\"", DS.Tables[0].Rows[i]["Pics"].ToString());
                    Url = string.Format("\"/Visa/{0}/{1}.html\"", DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"]);
                    if (DS.Tables[0].Rows[i]["LineFeature"].ToString().Length > 1) LineFeature = string.Format("<FONT color=#ff6600>{0}</FONT>", DS.Tables[0].Rows[i]["LineFeature"].ToString());
                    Strings.Append(string.Format("<LI><DIV class=p-img><A href={0} target=_blank><IMG src={1} width=130 height=100></A></DIV><DIV class=p-name><A href={0} target=_blank>{2} {3}</A></DIV><DIV class=p-price>青旅价：<STRONG>￥{4}</STRONG></DIV></LI>", Url, Pic, DS.Tables[0].Rows[i]["LineName"], LineFeature, DS.Tables[0].Rows[i]["Price"]));
                }
                HttpContext.Current.Cache.Insert("IndexVisa", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["IndexVisa"]);
        }

        //private static void SaveScriptbak(string JsScript, string Flag)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\ProductDetail\" + Flag + ".js";

        //    try
        //    {
        //        StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
        //        writer.WriteLine(JsScript);
        //        writer.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        JsScript = exception.Message;
        //    }
        //}
    }
}