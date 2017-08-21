using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.IO;

namespace TravelOnline.Class.Manage
{
    public class GetProductClass
    {
        public static StringBuilder Json;
        public static string parentmisid;

        public class ProductParent
        {
            public string ClassPath { get; set; }
            public string ClassList { get; set; }
            public int ClassLevel { get; set; }
        }

        public static void GetReportTopCss()
        {
            string SqlQueryText = "select id from OL_ProductClass where ParentId='0'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            SqlQueryText = "";
            if (DS.Tables[0].Rows.Count > 0)
            {
                int tops = (33 * DS.Tables[0].Rows.Count);
                SqlQueryText = "<style>.r-report {MARGIN-TOP: " + tops + "px;}.allsort .subitem {MIN-HEIGHT: " + tops + "px;}</style>";
            }
            HttpContext.Current.Cache.Insert("ReportTopCss", SqlQueryText);
        }

        public static ProductParent GetParentInfo(string id)
        {
            string SqlQueryText = string.Format("select * from OL_ProductClass where Id='{0}'", id);
            ProductParent ParentDetail = new ProductParent();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ParentDetail.ClassPath = DS.Tables[0].Rows[0]["ClassPath"].ToString();
                ParentDetail.ClassList = DS.Tables[0].Rows[0]["ClassList"].ToString();
                ParentDetail.ClassLevel = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ClassLevel"].ToString());
                DS.Dispose();
                return ParentDetail;
            }
            else
            {
                return null;
            }
        }//OL_UserRight数据查询

        public static ProductParent GetDestinationParentInfo(string id)
        {
            string SqlQueryText = string.Format("select * from OL_Destination where Id='{0}'", id);
            ProductParent ParentDetail = new ProductParent();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ParentDetail.ClassPath = DS.Tables[0].Rows[0]["ClassPath"].ToString();
                ParentDetail.ClassList = DS.Tables[0].Rows[0]["ClassList"].ToString();
                ParentDetail.ClassLevel = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ClassLevel"].ToString());
                DS.Dispose();
                return ParentDetail;
            }
            else
            {
                return null;
            }
        }

        public static string getParentClassListById(string id)
        {
            string SqlQueryText = string.Format("select top 1 ClassList from OL_ProductClass where Id='{0}'", id);
            return MyDataBaseComm.getScalar(SqlQueryText);
        }

        public static string getParentDestinationById(string id)
        {
            string SqlQueryText = string.Format("select top 1 ClassList from OL_Destination where Id='{0}'", id);
            return MyDataBaseComm.getScalar(SqlQueryText);
        }

        public static string getDestinationParentClassListById(string id)
        {
            string SqlQueryText = string.Format("select top 1 ClassList from OL_Destination where Id='{0}'", id);
            return MyDataBaseComm.getScalar(SqlQueryText);
        }

        public static void BindSortList()
        {
            HttpContext.Current.Cache.Insert("ReportTopCss", "");

            string SqlQueryText = "select id,parentId,ProductName,ProductType,ClassPath,ProductUrl,MisClassId,ClassLevel from OL_ProductClass order by ProductSort";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Json = new StringBuilder();
                Json.Append("(function(){var id=document.getElementById(\"_JD_SORTLIST\");if(id){id.innerHTML=\"");
                DataTable dt = DS.Tables[0];
                DataRow[] drs = dt.Select("parentid=0");
                int i = 0;
                foreach (DataRow dr in drs)
                {
                    string classname;
                    //if (i == 0)
                    if (dt.Rows.IndexOf(dr) == 0)
                    {
                        classname = "item fore";
                    }
                    else
                    {
                        classname = "item";
                    }
                    Json.Append(string.Format("<DIV class='{0}'><SPAN><H3><A href='{1}'>{2}</A></H3><S></S></SPAN><DIV class=i-mc><DIV class=close onclick=\\\"$(this).parent().parent().removeClass('hover')\\\"></DIV><DIV class=subitem>", classname, dr["ProductUrl"].ToString(), dr["ProductName"].ToString()));
                    BindSort(dr["id"].ToString(), dt);
                    Json.Append("</DIV><DIV id=JD_sort_k class=fr><DIV class=iloading>正在加载中，请稍候...</DIV></DIV></DIV></DIV>");
                    i++;
                }
            }

            Json.Append("\";}})();");
            if (Json.Length > 10)
            {
                //SaveAdScript(Json.ToString(), "SortList.js");
                SaveScriptToFile.SaveScript(Json.ToString(), "ProductClass", "SortList");

            }
        }

        public static void BindSort(string parentid, DataTable dt)
        {
            DataRow[] drs = dt.Select("parentId=" + parentid);

            foreach (DataRow dr in drs)
            {
                string classname="";
                if (dt.Rows.IndexOf(dr) == 0)
                {
                    classname = " class=fore";
                }
                
                if (dr["ClassLevel"].ToString() == "2")
                {
                    parentmisid = dr["MisClassId"].ToString();
                    if (dr["ProductUrl"].ToString().Length > 5)
                    {
                        Json.Append(string.Format("<DL{0}><DT><A href='{1}'>{2}</A></DT><DD> <EM><A href='{0}'>{1}</A></EM>", classname, dr["ProductUrl"].ToString(), dr["ProductName"].ToString()));
                    }
                    else
                    {
                        Json.Append(string.Format("<DL{0}><DT><A href='/{1}/{2}-0.html'>{3}</A></DT><DD>", classname, dr["ProductType"].ToString(), dr["MisClassId"].ToString(), dr["ProductName"].ToString()));
                    }

                    //Json.Append(string.Format("<DL{0}><DT>{1}</DT><DD>", classname, dr["ProductName"].ToString()));
                    BindSort(dr["id"].ToString(), dt);
                    Json.Append("</DD></DL>");
                }
                else
                {
                    if (dr["ProductUrl"].ToString().Length > 5)
                    {
                        Json.Append(string.Format("<EM><A href='{0}'>{1}</A></EM>", dr["ProductUrl"].ToString(), dr["ProductName"].ToString()));
                    }
                    else 
                    {
                        Json.Append(string.Format("<EM><A href='/{0}/{1}-{2}.html'>{3}</A></EM>", dr["ProductType"].ToString(), parentmisid, dr["MisClassId"].ToString(), dr["ProductName"].ToString()));
                    }
                }
            }
        }

        public static void BindClassName()
        {
            string SqlQueryText = "select id,parentId,ProductName,ClassPath from OL_ProductClass order by ProductSort,id";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Json = new StringBuilder();
                Json.Append("var data = {'0':{");
                DataTable dt = DS.Tables[0];
                DataRow[] drs = dt.Select("parentid=0");
                int i = 0;
                foreach (DataRow dr in drs)
                {
                    //'0':{1:'北京市',108:'河北省',4825:'台湾省'},
                    //'0,1':{2:'北京市'},
                    //'0,1,2':{3:'东城区',4:'西城区',5:'崇文区',6:'宣武区',7:'朝阳区',8:'丰台区',9:'石景山区',10:'海淀区',11:'门头沟区',12:'房山区',13:'通州区',14:'顺义区',15:'昌平区',16:'大兴区',17:'怀柔区',18:'平谷区',19:'密云县',20:'延庆县',21:'延庆镇'},
                    Json.Append(string.Format("{0}:'{1}'", dr["id"].ToString(), dr["ProductName"].ToString()));

                    //最后一行
                    //if (dt.Rows.IndexOf(dr) == dt.Rows.Count - 1)
                    if (i == drs.Count()-1)
                    {
                        Json.Append("}");
                    }
                    else
                    {
                        Json.Append(",");
                    }
                    i++;
                }
                foreach (DataRow dr in drs)
                {
                    BindNode(dr["id"].ToString(), dt);
                }
            }
            Json.Append("}");
            if (Json.Length > 10)
            {
                //SaveAdScript(Json.ToString(), "ProductClass.js");
                SaveScriptToFile.SaveScript(Json.ToString(), "ProductClass", "ProductClass");
            }
        }

        public static void BindNode(string parentid, DataTable dt)
        {
            DataRow[] drs = dt.Select("parentId=" + parentid);
            int i = 0;
            foreach (DataRow dr in drs)
            {
                if (i == 0)
                {
                    Json.Append(string.Format(",'{0}':", dr["ClassPath"].ToString()));
                    Json.Append("{");
                }
                Json.Append(string.Format("{0}:'{1}'", dr["id"].ToString(), dr["ProductName"].ToString()));
                if (i == drs.Count()-1)
                {
                    Json.Append("}");
                }
                else
                {
                    Json.Append(",");
                }
                i++;
            }
            foreach (DataRow dr in drs)
            {
                BindNode(dr["id"].ToString(), dt);
            }
        }

        //private static void SaveAdScript(string AdString,string FileName)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\ProductClass\" + FileName;

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

        public static void BindDestinationClassName()
        {
            string SqlQueryText = "select id,parentId,DestinationName,ClassPath from OL_Destination order by ClassPath,id";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Json = new StringBuilder();
                Json.Append("var data = {'0':{");
                DataTable dt = DS.Tables[0];
                DataRow[] drs = dt.Select("parentid=0");
                int i = 0;
                foreach (DataRow dr in drs)
                {
                    //'0':{1:'北京市',108:'河北省',4825:'台湾省'},
                    //'0,1':{2:'北京市'},
                    //'0,1,2':{3:'东城区',4:'西城区',5:'崇文区',6:'宣武区',7:'朝阳区',8:'丰台区',9:'石景山区',10:'海淀区',11:'门头沟区',12:'房山区',13:'通州区',14:'顺义区',15:'昌平区',16:'大兴区',17:'怀柔区',18:'平谷区',19:'密云县',20:'延庆县',21:'延庆镇'},
                    Json.Append(string.Format("{0}:'{1}'", dr["id"].ToString(), dr["DestinationName"].ToString()));

                    //最后一行
                    //if (dt.Rows.IndexOf(dr) == dt.Rows.Count - 1)
                    if (i == drs.Count() - 1)
                    {
                        Json.Append("}");
                    }
                    else
                    {
                        Json.Append(",");
                    }
                    i++;
                }
                foreach (DataRow dr in drs)
                {
                    BindDestinationNode(dr["id"].ToString(), dt);
                }
            }
            Json.Append("}");
            if (Json.Length > 10)
            {
                SaveScriptToFile.SaveScript(Json.ToString(), "Destination", "DestinationClass");
            }
            //return Json.ToString();
        }

        public static void BindDestinationNode(string parentid, DataTable dt)
        {
            DataRow[] drs = dt.Select("parentId=" + parentid);
            int i = 0;
            foreach (DataRow dr in drs)
            {
                if (i == 0)
                {
                    Json.Append(string.Format(",'{0}':", dr["ClassPath"].ToString()));
                    Json.Append("{");
                }
                Json.Append(string.Format("{0}:'{1}'", dr["id"].ToString(), dr["DestinationName"].ToString()));
                if (i == drs.Count() - 1)
                {
                    Json.Append("}");
                }
                else
                {
                    Json.Append(",");
                }
                i++;
            }
            foreach (DataRow dr in drs)
            {
                BindDestinationNode(dr["id"].ToString(), dt);
            }
        }
    }
}