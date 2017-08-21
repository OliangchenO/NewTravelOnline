using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using TravelOnline.Class.Manage;
using System.IO;

namespace TravelOnline.Class.Travel
{
    public class LineListPageSerch
    {
        public static string GetPagesSqlQueryText(string fieldlist, string condition, string pkey, string tablename, string sort, string sortname, int pagesize, int cpage)
        {
            return string.Format("select top {0} {1} from {2} where {3} and {4} not in (select top {5} {4} from {2} where {3} order by {6} {7}) order by {6} {7}", pagesize, fieldlist, tablename, condition, pkey, pagesize * (cpage - 1), sortname, sort);
        }

        public static string GetPagesCounts(string pkey, string tablename, string condition)
        {
            return Convert.ToString(MyDataBaseComm.getScalar(string.Format("select count({0}) from {1} where {2} ", pkey, tablename, condition)));
        }

        public static string GetLinesPageList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            string tags;

            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    string DetailInfos = "";
                    string Pics = "/Images/none.gif";
                    string pdates = DS.Tables[0].Rows[i]["Pdates"].ToString();
                    string url = string.Format("\\\"/{0}/{1}/{2}.html\\\"", DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"]);

                    if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa")
                    {
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length > 5) Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                        }
                        catch
                        { }
                        if (pdates.Length > 3) DetailInfos = string.Format("<div class=pp><SPAN>有效期：</SPAN>{0}&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>停留时间：</SPAN>{1}&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>工作日：</SPAN>{2}</div></dd>", pdates.Split("$".ToCharArray())[0], pdates.Split("$".ToCharArray())[1], pdates.Split("$".ToCharArray())[2]);
                    }
                    else
                    {
                        if (pdates.Length > 50) pdates = pdates.Substring(0, 35);
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length ==24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        }
                        catch
                        { }
                        DetailInfos = string.Format("<div class=pp><SPAN>行程：</SPAN>{0}天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>{1}...</div></dd>", DS.Tables[0].Rows[i]["LineDays"], pdates.Replace(",", ", "));
                    }

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
                        catch (Exception exception2)
                        {
                            SaveErrorToLog(exception2.Message, exception2.Message);
                        }                        
                    }
                    
                    Strings.Append(string.Format("<dl><dt><A href={0} target=_blank><IMG onerror=\\\"this.src='/Images/none.gif'\\\" src=\\\"{1}\\\" width=150></A></dt>", url, Pics));
                    Strings.Append(string.Format("<dd><div class=p-name><A href={0} target=_blank>{1} {3}</A></div><div class=ps>{2}</div>", url, DS.Tables[0].Rows[i]["LineName"], DS.Tables[0].Rows[i]["LineFeature"], tags));
                    Strings.Append(DetailInfos);

                    Strings.Append(string.Format("<dt><div class=p-price>￥{0}</div><div class=pd><font color=#ff6600>编号：</font>{1}</div>", DS.Tables[0].Rows[i]["Price"], DS.Tables[0].Rows[i]["MisLineId"]));
                    Strings.Append(string.Format("<DIV class=btns_s><INPUT id=collectbtn class=btn-coll onclick=feed_collect('{0}'); value=收藏 type=button>", DS.Tables[0].Rows[i]["MisLineId"]));
                    Strings.Append(string.Format("<INPUT class=btn-comp onclick=\\\"addToCompare(this,{0},'{1}')\\\" value=对比 type=button></DIV></dt></dl>", DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"]));
                }
            }
            else
            {
                Strings.Append("<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>");
            }           
            
            return Strings.ToString();
        }

        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(DateTime.Now.ToString() + ":");
                writer.WriteLine(inErrorlog);
                writer.WriteLine(inSQL);
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public static string GetNewsInfoPageList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div><a href=\"/showinfo/{0}.html\" target=\"_blank\">{1}</a></div><span>{2:yyyy-MM-dd hh:ss:mm}</span><div class=line></div></li>", DS.Tables[0].Rows[i]["id"], DS.Tables[0].Rows[i]["AfficheName"], DS.Tables[0].Rows[i]["EditTime"]));
                }
                Strings.Append("</ul>");
            }
            else
            {
                Strings.Append("<SPAN style=\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>");
            }

            return Strings.ToString();
        }

        public static string GetNewsPageList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div><a href=\\\"/News/{0}.html\\\">{1}</a></div><span>{2:yyyy-MM-dd hh:ss:mm}</span><div class=line></div></li>", DS.Tables[0].Rows[i]["id"], DS.Tables[0].Rows[i]["AfficheName"], DS.Tables[0].Rows[i]["EditTime"]));
                }
                Strings.Append("</ul>");
            }
            else
            {
                Strings.Append("<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>");
            }

            return Strings.ToString();
        }

        public static string GetJournalPageList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div><a href=\\\"/journal/{0}.html\\\">{1}</a></div><span>{2:yyyy-MM-dd hh:ss:mm}</span><div class=line></div></li>", DS.Tables[0].Rows[i]["id"], DS.Tables[0].Rows[i]["title"], DS.Tables[0].Rows[i]["inputdate"]));
                }
                Strings.Append("</ul>");
            }
            else
            {
                Strings.Append("<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>");
            }

            return Strings.ToString();
        }

        public static string GetNewJournalPageList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div><a href=\"/showjournal/{0}.html\" target=\"_blank\">{1}</a></div><span>{2:yyyy-MM-dd hh:ss:mm}</span><div class=line></div></li>", DS.Tables[0].Rows[i]["id"], DS.Tables[0].Rows[i]["title"], DS.Tables[0].Rows[i]["inputdate"]));
                }
                Strings.Append("</ul>");
            }
            else
            {
                Strings.Append("<SPAN style=\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\">没有查询到任何数据，请重新搜索！</SPAN>");
            }

            return Strings.ToString();
        }
        
    }
}