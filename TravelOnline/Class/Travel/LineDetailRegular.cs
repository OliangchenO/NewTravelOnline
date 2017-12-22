using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace TravelOnline.Class.Travel
{
    public class LineDetailRegular
    {
        public static String OrderProcess()
        {
            if (Convert.ToString(HttpContext.Current.Cache["RegularOrderProcess"]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=\"m select\">");
                Strings.Append("<div class=mt><H1></H1><STRONG>在线预订流程</STRONG><div class=extra></div></div>");
                Strings.Append("<div class=content><IMG src=\"/images/order_step.jpg\"></div>");
                //Strings.Append("<div class=content><IMG src=\"/images/order_step1.jpg\"></div>");
                //Strings.Append("<div class=content><IMG src=\"/images/order_step2.jpg\"></div>");
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert("RegularOrderProcess", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["RegularOrderProcess"]);
        }

        public static String PayInfos()
        {
            if (Convert.ToString(HttpContext.Current.Cache["RegularPayInfos"]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=\"m select\"><div class=mt><H1></H1><STRONG>付款方式</STRONG><div class=extra></div></div>");
                Strings.Append("<DL class=fore><DT>在线支付：</DT><DD><div>支付宝、快钱等多种在线支付方式，供您选择。</div></DD></DL>");
                Strings.Append("<DL><DT>门店付款：</DT><DD><div>您可以到距离你最近的门店，完成付款。</div></DD></DL>");
                //Strings.Append("<DL><DT>上门收款：</DT><DD><div>预订时留下您的详细联系方式，我们会派出业务人员上门拜访，完成收款。</div></DD></DL>");
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert("RegularPayInfos", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["RegularPayInfos"]);
        }

        public static String InLandContract()
        {
            return "<DL><DT>合同范本：</DT><DD><div><A target=_blank href=\"/Upload/上海市国内旅游合同示范文本.doc\">上海市国内旅游合同范本</A></div></DD></DL>";
        }

        public static String OutBoundContract()
        {
            return "<DL><DT>合同范本：</DT><DD><div><A target=_blank href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A></div></DD></DL>";//<div><A href=\"#\">上海中国青年旅行社出境押金协议</A><div>
        }

        public static string ContractInfos(string Flag)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("RegularContract{0}", Flag)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=\"m select\"><div class=mt><H1></H1><STRONG>签约方式</STRONG><div class=extra></div></div>");
                Strings.Append("<DL class=fore> <DT>传真签约：</DT> <DD><div>双方在合同上签字盖章后，通过传真进行签约。</div> </DD></DL>");
                Strings.Append("<DL><DT>快递签约：</DT><DD><div>我们把盖章合同快递到您，您签字后快递回我们公司，完成签约。</div></DD></DL>");
                //Strings.Append("<DL><DT>上门签约：</DT><DD><div>预订时留下您的详细联系方式，我们会派出业务人员上门拜访，完成签约。</div></DD></DL>");
                //Strings.Append("<DL><DT>门店签约：</DT><DD><div><A href=\"/Service/ContactUs\">上海中国青年旅行社门市网店</A></div></DD></DL>");
                switch (Flag)
                {
                    case "InLand":
                        Strings.Append(InLandContract());
                        break;
                    case "OutBound":
                        Strings.Append(OutBoundContract());
                        break;
                    default:
                        break;
                }
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("RegularContract{0}", Flag), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("RegularContract{0}", Flag)]);
        }

        public static string LineDetailSortCreate()
        {
            if (Convert.ToString(HttpContext.Current.Cache["LineDetailSortCache"]) == "")
            {

                StringBuilder Strings = new StringBuilder();
                Strings.Append("<DL id=_price><DT>价格：</DT><DD>");
                Strings.Append("<DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV><DIV><A tag=1 href=\"javascript:void(0);\">1-499</A></DIV>");
                Strings.Append("<DIV><A tag=2 href=\"javascript:void(0);\">500-999</A></DIV><DIV><A tag=3 href=\"javascript:void(0);\">1000-1999</A></DIV>");
                Strings.Append("<DIV><A tag=4 href=\"javascript:void(0);\">2000-3999</A></DIV><DIV><A tag=5 href=\"javascript:void(0);\">4000-5999</A></DIV>");
                Strings.Append("<DIV><A tag=6 href=\"javascript:void(0);\">6000-7999</A></DIV><DIV><A tag=7 href=\"javascript:void(0);\">8000-9999</A></DIV>");
                Strings.Append("<DIV><A tag=8 href=\"javascript:void(0);\">10000元以上</A></DIV></DD></DL>");

                Strings.Append("<DL id=_day><DT>天数：</DT><DD>");
                Strings.Append("<DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV><DIV><A tag=1 href=\"javascript:void(0);\">一天</A></DIV>");
                Strings.Append("<DIV><A tag=2 href=\"javascript:void(0);\">两天</A></DIV><DIV><A tag=3 href=\"javascript:void(0);\">三天</A></DIV>");
                Strings.Append("<DIV><A tag=4 href=\"javascript:void(0);\">四天</A></DIV><DIV><A tag=5 href=\"javascript:void(0);\">五天</A></DIV>");
                Strings.Append("<DIV><A tag=6 href=\"javascript:void(0);\">六天</A></DIV><DIV><A tag=7 href=\"javascript:void(0);\">七天</A></DIV>");
                Strings.Append("<DIV><A tag=8 href=\"javascript:void(0);\">八天</A></DIV><DIV><A tag=9 href=\"javascript:void(0);\">九天</A></DIV>");
                Strings.Append("<DIV><A tag=10 href=\"javascript:void(0);\">十天</A></DIV><DIV><A tag=11 href=\"javascript:void(0);\">十天以上</A></DIV>");
                Strings.Append("</DD></DL>");

                string SqlQueryText = "select AdName,MisClassId from OL_FlashAD where AdFlag='Topic' order by AdSort";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Strings.Append("<DL id=_topic><DT>主题：</DT><DD><DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<DIV><A tag={0} href=\"javascript:void(0);\">{1}</A></DIV>", DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS.Tables[0].Rows[i]["AdName"].ToString()));
                }
                Strings.Append("</DD></DL>");


                HttpContext.Current.Cache.Insert("LineDetailSortCache", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["LineDetailSortCache"]);
        }

        public static string VisaDetailSortCreate()
        {
            if (Convert.ToString(HttpContext.Current.Cache["VisaDetailSortCache"]) == "")
            {

                StringBuilder Strings = new StringBuilder();
                Strings.Append("<DL id=_price><DT>价格：</DT><DD>");
                Strings.Append("<DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV><DIV><A tag=1 href=\"javascript:void(0);\">1-499</A></DIV>");
                Strings.Append("<DIV><A tag=2 href=\"javascript:void(0);\">500-999</A></DIV><DIV><A tag=3 href=\"javascript:void(0);\">1000-1999</A></DIV>");
                Strings.Append("<DIV><A tag=4 href=\"javascript:void(0);\">2000-3999</A></DIV><DIV><A tag=5 href=\"javascript:void(0);\">4000-5999</A></DIV>");
                Strings.Append("<DIV><A tag=6 href=\"javascript:void(0);\">6000-7999</A></DIV><DIV><A tag=7 href=\"javascript:void(0);\">8000-9999</A></DIV>");
                Strings.Append("<DIV><A tag=8 href=\"javascript:void(0);\">10000元以上</A></DIV></DD></DL>");

                HttpContext.Current.Cache.Insert("VisaDetailSortCache", Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache["VisaDetailSortCache"]);
        }

        public static string VisaAreaSortCreate(string Flag)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineAreaSort{0}", Flag)]) == "")
            {

                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = string.Format("SELECT ProductName,MisClassId FROM OL_ProductClass where ParentId in (select id from OL_ProductClass where MisClassId='{0}') order by ParentId,ProductSort", Flag);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Strings.Append("<DL id=_Visa class=fore><DT>办签国家：</DT><DD><DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<DIV><A tag={0} href=\"javascript:void(0);\">{1}</A></DIV>", DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS.Tables[0].Rows[i]["ProductName"].ToString()));
                }
                Strings.Append("</DD></DL>");


                HttpContext.Current.Cache.Insert(string.Format("LineAreaSort{0}", Flag), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineAreaSort{0}", Flag)]);
        }

        public static string LineAreaSortCreate(string Flag)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineAreaSort{0}", Flag)]) == "")
            {

                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = string.Format("SELECT ProductName,MisClassId FROM OL_ProductClass where ParentId in (select id from OL_ProductClass where MisClassId='{0}') order by ParentId,ProductSort", Flag);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Strings.Append("<DL id=_area class=fore><DT>目的地：</DT><DD><DIV><A tag=0 class=curr href=\"javascript:void(0);\">全部</A></DIV>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<DIV><A tag={0} href=\"javascript:void(0);\">{1}</A></DIV>", DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS.Tables[0].Rows[i]["ProductName"].ToString()));
                }
                Strings.Append("</DD></DL>");


                HttpContext.Current.Cache.Insert(string.Format("LineAreaSort{0}", Flag), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineAreaSort{0}", Flag)]);
        }

        public static string LineRecommendSortCreate(string LineClass)
        {
            //string.Format("LineListSpecialRecommend{0}", LineClass)
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineRecommendSort{0}", LineClass)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = string.Format("select top 3 LineType,MisLineId,LineName,Price,Pics,(select top 1 ProductName from OL_ProductType where ProductName=OL_Line.LineClass) as TypeName from OL_Line where Sale='0' and LineClass='{0}' and Recommend=1 and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    SqlQueryText = string.Format("select top 3 LineType,MisLineId,LineName,Price,Pics,(select top 1 ProductName from OL_ProductType where ProductName=OL_Line.LineClass) as TypeName from OL_Line where Sale='0' and LineClass='{0}' and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                }
                Strings.Append("<DIV id=hotsale>");
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append(string.Format("<DIV class=mt><H2>{0}旅游专家推荐</H2></DIV>", DS.Tables[0].Rows[0]["TypeName"].ToString()));
                    Strings.Append("<DIV class=\"mc list-h\">");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        string Pics = "/Images/none.gif";
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length ==24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        }
                        catch
                        { }
                        Strings.Append(string.Format("<dl><dt><A href=\"/{0}/{1}/{2}.html\" target=_blank><IMG onerror=\"this.src='/Images/none.gif'\" src=\"{3}\" width=120></A></dt>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], Pics));
                        Strings.Append(string.Format("<dd><div class=p-name><A href=\"/{0}/{1}/{2}.html\" target=_blank>{3}</A></div>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"]));
                        Strings.Append(string.Format("<div class=p-price>￥{0}</div>", DS.Tables[0].Rows[i]["Price"].ToString()));
                        Strings.Append(string.Format("<div><A class=btns href=\"/{0}/{1}/{2}.html\" target=_blank>立即预订</a></div></dd></dl>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"]));
                    }
                    Strings.Append("</div>");

                }
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineRecommendSort{0}", LineClass), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineRecommendSort{0}", LineClass)]);
        }

        public static string LineRecommendBigCreate(string LineClass)
        {
            //string.Format("LineListSpecialRecommend{0}", LineClass)
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineRecommendSort{0}", LineClass)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = string.Format("select top 3 LineType,MisLineId,LineName,Price,Pics,(select top 1 ProductName from OL_ProductType where ProductName=OL_Line.LineClass) as TypeName from OL_Line where Sale='0' and LineType='{0}' and Recommend=1 and Price>0 and PlanDate>='{1}' order by EditTime desc", LineClass, DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Strings.Append("<DIV id=hotsale>");
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string classname;
                    if (LineClass == "OutBound")
                    {
                        classname = "出境";
                    }
                    else 
                    {
                        classname = "国内";
                    }
                    Strings.Append(string.Format("<DIV class=mt><H2>{0}旅游专家推荐</H2></DIV>", classname));
                    Strings.Append("<DIV class=\"mc list-h\">");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        string Pics = "/Images/none.gif";
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length ==24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        }
                        catch
                        { }
                        Strings.Append(string.Format("<dl><dt><A href=\"/{0}/{1}/{2}.html\" target=_blank><IMG onerror=\"this.src='/Images/none.gif'\" src=\"{3}\" width=120></A></dt>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], Pics));
                        Strings.Append(string.Format("<dd><div class=p-name><A href=\"/{0}/{1}/{2}.html\" target=_blank>{3}</A></div>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"]));
                        Strings.Append(string.Format("<div class=p-price>￥{0}</div>", DS.Tables[0].Rows[i]["Price"].ToString()));
                        Strings.Append(string.Format("<div><A class=btns href=\"/line/{1}.html\" target=_blank>立即预订</a></div></dd></dl>", DS.Tables[0].Rows[i]["LineType"], LineClass, DS.Tables[0].Rows[i]["MisLineId"]));
                    }
                    Strings.Append("</div>");

                }
                Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineRecommendSort{0}", LineClass), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineRecommendSort{0}", LineClass)]);
        }

    }
}