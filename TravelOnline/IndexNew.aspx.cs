using Belinda.Jasp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.BLL;
using TravelOnline.Models;
using TravelOnline.NewPage.Class;

namespace TravelOnline
{
    public partial class IndexNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Slide()%>
        public object GetIndex_Ad_Slide(string types)
        {
            try
            {
                var query = Query.GetOL_FlashAD(types, 6);
                DataTable dt = UIHelper.ListToDataTable(query);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    string styles = " class=\"current\"";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0) { styles = string.Empty; }
                        dt.Rows[i]["styles"] = styles;
                        if (i == dt.Rows.Count - 1)
                        {
                            dt.Rows[i]["styles"] = " class=\"no-border\"";
                        }
                        else
                        {
                            dt.Rows[i]["styles"] = styles;
                        }
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Sell()%>
        public object GetIndex_Line_Sell(string types, int top)
        {
            try
            {
                var query = Query.GetSpecialTopic(types, top);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                JSONArray ArrJson2 = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string typeid = dt.Rows[0]["LineType"].ToString();
                    string destid = dt.Rows[0]["Destinationid"].ToString();
                    DataTable dt2 = dt.Rows[0].Table;
                    ArrJson2 = Data.GetJsonList(dt2);
                    ObJson.Add("SpecialTopic", ArrJson2);
                    string sql = string.Format("select * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    //string sql = string.Format("select top 3 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 3 * FROM View_SpecialLineTemp where 1=1";
                            //string sqlstr = "SELECT top 3 * FROM View_SpecialLineTemp_New where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                                //sqlstr += " and LineType in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                //sqlstr += " and Destination =" + destid + "";
                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            dt.Clear();
                            dt = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                        }
                    }
                    string Pics = "/Images/none.gif";
                    string styles = string.Empty;
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    dt.Columns.Add(new DataColumn("rowIndex", typeof(int)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        //Pics==PhotoPath
                        //LineType==Types
                        if (dt.Rows[i]["Pics"].ToString().Length == 24)
                        { Pics = string.Format("/images/views/{0}/m_{1}", dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]); }
                        if (dt.Rows[i]["LineType"].ToString() == "Visa")
                        { Pics = string.Format("/images/shadow/{0}", dt.Rows[i]["Pics"].ToString()); }
                        if (i == dt.Rows.Count - 1)
                        { styles = " class=\"no-mr\""; }
                        dt.Rows[i]["styles"] = styles;
                        dt.Rows[i]["Pics"] = Pics;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                        dt.Rows[i]["rowIndex"] = i;
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                    ObJson.Add("total", dt.Rows.Count);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Banner()%>
        public object GetIndex_Ad_Banner(string types)
        {
            try
            {
                var query = Query.GetOL_FlashAD(types, 5);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string styles = "under - ad";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == dt.Rows.Count - 1)
                        {
                            dt.Rows[i]["styles"] = "under-ad mrt";
                        }
                        else
                        {
                            dt.Rows[i]["styles"] = styles;
                        }
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Season()%>
        public object GetIndex_Ad_Season(string types)
        {
            try
            {
                var query = Query.GetOL_FlashAD(types, 1);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                if (query.Count > 0)
                {
                    ArrJson = Data.GetJsonList(UIHelper.ListToDataTable(query));
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Season()%>
        public object GetIndex_Line_Season(string types)
        {
            try
            {
                var query = Query.GetSpecialTopic(types, 1);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string typeid = dt.Rows[0]["LineType"].ToString();
                    string destid = dt.Rows[0]["Destinationid"].ToString();
                    string sql = string.Format("select top 12 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    //string sql = string.Format("select top 12 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 12 * FROM View_SpecialLineTemp where 1=1";
                            //string sqlstr = "SELECT top 12 * FROM View_SpecialLineTemp_New where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                                //sqlstr += " and LineType in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                //sqlstr += " and Destination =" + destid + "";
                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            dt.Clear();
                            dt = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                        }
                    }
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    dt.Columns.Add(new DataColumn("_planstyle", typeof(string)));
                    dt.Columns.Add(new DataColumn("indexJ", typeof(int)));
                    dt.Columns.Add(new DataColumn("rowIndex", typeof(int)));
                    dt.Columns.Add(new DataColumn("tname", typeof(string)));
                    string Pics = "/Images/none.gif";
                    string styles = string.Empty;
                    string planstyle = string.Empty;
                    int j = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        //Pics==PhotoPath
                        //LineType==Types
                        //PlanType==TravelType
                        if (dt.Rows[i]["Pics"].ToString().Length == 24)
                            Pics = string.Format("/images/views/{0}/m_{1}", dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (dt.Rows[i]["LineType"].ToString() == "Visa")
                            Pics = string.Format("/images/shadow/{0}", dt.Rows[i]["Pics"].ToString());
                        if (j == 1) { styles = " class=\"mr\""; } else { styles = string.Empty; }
                        switch (dt.Rows[i]["PlanType"].ToString())
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
                        j++;
                        dt.Rows[i]["Pics"] = Pics;
                        dt.Rows[i]["styles"] = styles;
                        dt.Rows[i]["_planstyle"] = planstyle;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                        dt.Rows[i]["tname"] = GetPlanType(dt.Rows[i]["PlanType"].ToString());
                        dt.Rows[i]["indexJ"] = j;
                        dt.Rows[i]["rowIndex"] = i;
                        if (j == 3)
                        {
                            j = 0;
                        }
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                    ObJson.Add("total", dt.Rows.Count);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_FreeTour || Index_Outbound || Index_Inland")%>
        public object GetIndex_Line_Tab(string linetype)
        {
            try
            {
                var query = Query.GetSpecialTopic(linetype, 0);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string styles = " class=\"current\"";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        dt.Rows[i]["styles"] = styles;
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_FreeTour || Index_Outbound || Index_Inland")%>
        public object GetIndex_Line_List(string linetype)
        {
            try
            {
                var query = Query.GetSpecialTopic(linetype, 0);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                JSONArray ArrJson2 = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string styles = " current";
                    string Pics = "/Images/none.gif";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    dt.Columns.Add(new DataColumn("_childRowCount", typeof(int)));
                    dt.Columns.Add(new DataColumn("flag", typeof(bool)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string typeid = dt.Rows[i]["LineType"].ToString();
                        string destid = dt.Rows[i]["Destinationid"].ToString();
                        string sql = string.Format("select top 10 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[i]["id"].ToString());
                        //string sql = string.Format("select top 10 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[i]["id"].ToString());
                        DataTable dt2 = MyDataBaseComm.getDataSet(sql).Tables[0];
                        if (dt2.Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top 10 * FROM View_SpecialLineTemp where 1=1";
                                //string sqlstr = "SELECT top 10 * FROM View_SpecialLineTemp_New where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                    //sqlstr += " and LineType in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                    //sqlstr += " and Destination =" + destid + "";
                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                dt2.Clear();
                                dt2 = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                            }
                        }
                        if (i != 0) styles = "";
                        dt.Rows[i]["styles"] = styles;
                        dt.Rows[i]["_childRowCount"] = dt2.Rows.Count;
                        if (dt.Rows[i]["Url"].ToString().Length > 5) { dt.Rows[i]["flag"] = true; } else { dt.Rows[i]["flag"] = false; }
                        string style1 = "";
                        dt2.Columns.Add(new DataColumn("_parentId", typeof(int)));
                        dt2.Columns.Add(new DataColumn("style1", typeof(string)));
                        dt2.Columns.Add(new DataColumn("rowIndex", typeof(int)));
                        dt2.Columns.Add(new DataColumn("tname", typeof(string)));
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (j == 3)
                            {
                                style1 = " class=\"no-mrr\"";
                            }
                            else
                            {
                                style1 = "";
                            }
                            //string PlanType = "跟团游";
                            //switch (dt2.Rows[j]["PlanType"].ToString())
                            //{
                            //    case "2":
                            //        PlanType = "自由行";
                            //        break;
                            //    case "3":
                            //        PlanType = "当地游";
                            //        break;
                            //    case "4":
                            //        PlanType = "签证";
                            //        break;
                            //}
                            if (j < 4)
                            {
                                Pics = "/images/none.gif";
                                //Pics==PhotoPath
                                //LineType==Types
                                //PlanType==TravelType
                                if (dt2.Rows[j]["Pics"].ToString().Length == 24)
                                    Pics = string.Format("/images/views/{0}/m_{1}", dt2.Rows[j]["Pics"].ToString().Split("/".ToCharArray())[0], dt2.Rows[j]["Pics"].ToString().Split("/".ToCharArray())[1]);
                                if (dt2.Rows[j]["LineType"].ToString() == "Visa")
                                    Pics = string.Format("/images/shadow/{0}", dt2.Rows[j]["Pics"].ToString());
                            }
                            dt2.Rows[j]["Pics"] = Pics;
                            dt2.Rows[j]["Price"] = dt2.Rows[j]["Price"].ToString().Replace(".00", "");
                            dt2.Rows[j]["tname"] = GetPlanType(dt2.Rows[j]["PlanType"].ToString());
                            dt2.Rows[j]["_parentId"] = dt.Rows[i]["Id"];
                            dt2.Rows[j]["rowIndex"] = j;
                            dt2.Rows[j]["style1"] = style1;
                        }
                        ArrJson2.AddRange(Data.GetJsonList(dt2));
                    }
                    ArrJson.AddRange(Data.GetJsonList(dt));
                }
                ObJson.Add("rows", ArrJson);
                ObJson.Add("childRows", ArrJson2);
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_OtherTab("Index_Cruise")%>
        public object GetIndex_OtherTab(string linetype)
        {
            try
            {
                var query = Query.GetSpecialTopic(linetype, 0);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string styles = " class=\"current action\"";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0) styles = "";
                        dt.Rows[i]["styles"] = styles;
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_OtherLine_List("Index_Cruise")%>
        public object GetIndex_OtherLine_List(string linetype)
        {
            try
            {
                var query = Query.GetSpecialTopic(linetype, 0);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                JSONArray ArrJson2 = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string styles = " action";
                    string Pics = "/Images/none.gif";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    dt.Columns.Add(new DataColumn("flag", typeof(bool)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string typeid = dt.Rows[i]["LineType"].ToString();
                        string destid = dt.Rows[i]["Destinationid"].ToString();
                        string sql = string.Format("select top 4 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[i]["id"].ToString());
                        //string sql = string.Format("select top 4 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[i]["id"].ToString());
                        DataTable dt2 = MyDataBaseComm.getDataSet(sql).Tables[0];
                        if (dt2.Rows.Count == 0)
                        {
                            if (typeid.Length > 2 || destid.Length > 3)
                            {
                                string sqlstr = "SELECT top 4 * FROM View_SpecialLineTemp where 1=1";
                                //string sqlstr = "SELECT top 4 * FROM View_SpecialLineTemp_New where 1=1";
                                if (typeid.Length > 2)
                                {
                                    sqlstr += " and lineclass in (" + typeid + ")";
                                    //sqlstr += " and LineType in (" + typeid + ")";
                                }
                                if (destid.Length > 3)
                                {
                                    sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                    //sqlstr += " and Destination =" + destid + "";
                                }
                                sqlstr += " order by TopEnd desc,EditTime desc";
                                dt2.Clear();
                                dt2 = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                            }
                        }
                        if (i != 0) styles = "";
                        dt.Rows[i]["styles"] = styles;
                        if (dt.Rows[i]["Url"].ToString().Length > 5) { dt.Rows[i]["flag"] = true; } else { dt.Rows[i]["flag"] = false; }
                        string style1 = string.Empty;
                        dt2.Columns.Add(new DataColumn("style1", typeof(string)));
                        dt2.Columns.Add(new DataColumn("_parentId", typeof(int)));
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (j == dt2.Rows.Count - 1)
                            {
                                style1 = " class=\"no-mrr\"";
                            }
                            Pics = "/images/none.gif";
                            //Pics==PhotoPath
                            //LineType==Types
                            if (dt2.Rows[j]["Pics"].ToString().Length == 24)
                                Pics = string.Format("/images/views/{0}/m_{1}", dt2.Rows[j]["Pics"].ToString().Split("/".ToCharArray())[0], dt2.Rows[j]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (dt2.Rows[j]["LineType"].ToString() == "Visa")
                                Pics = string.Format("/images/shadow/{0}", dt2.Rows[j]["Pics"].ToString());
                            dt2.Rows[j]["style1"] = style1;
                            dt2.Rows[j]["Pics"] = Pics;
                            dt2.Rows[j]["_parentId"] = dt.Rows[i]["Id"];
                            dt2.Rows[j]["Price"] = dt2.Rows[j]["Price"].ToString().Replace(".00", "");
                        }
                        ArrJson2.AddRange(Data.GetJsonList(dt2));
                    }
                    ArrJson.AddRange(Data.GetJsonList(dt));
                }
                ObJson.Add("rows", ArrJson);
                ObJson.Add("childRows", ArrJson2);
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region <%=TravelOnline.NewPage.Class.CacheClass.Index_Visa()%>
        public object GetIndex_Visa(string linetype)
        {
            try
            {
                var query = Query.GetSpecialTopic(linetype, 1);
                JSONObject ObJson = new JSONObject();
                JSONArray ArrJson = new JSONArray();
                DataTable dt = UIHelper.ListToDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    string typeid = dt.Rows[0]["LineType"].ToString();
                    string destid = dt.Rows[0]["Destinationid"].ToString();
                    string sql = string.Format("select top 9 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    //string sql = string.Format("select top 9 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", dt.Rows[0]["ID"].ToString());
                    dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 9 * FROM View_SpecialLineTemp where 1=1";
                            //string sqlstr = "SELECT top 9 * FROM View_SpecialLineTemp_New where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                                //sqlstr += " and LineType in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                //sqlstr += " and Destination =" + destid + "";
                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            dt.Clear();
                            dt = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                        }
                    }
                    string Pics = "/Images/none.gif";
                    string styles = "no-mrr ";
                    dt.Columns.Add(new DataColumn("styles", typeof(string)));
                    dt.Columns.Add(new DataColumn("rowIndex", typeof(int)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        //Pics==PhotoPath
                        //LineType==Types
                        if (dt.Rows[i]["Pics"].ToString().Length == 24)
                            Pics = string.Format("/images/views/{0}/m_{1}", dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        if (dt.Rows[i]["LineType"].ToString() == "Visa")
                            Pics = string.Format("/images/shadow/{0}", dt.Rows[i]["Pics"].ToString());
                        styles = "";
                        if (i == 2 || i == 5 || i == 8) styles = "no-mrr ";
                        dt.Rows[i]["styles"] = styles;
                        dt.Rows[i]["Pics"] = Pics;
                        dt.Rows[i]["rowIndex"] = i;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region Famous
        public string GetIndex_Famous(int top)
        {
            var cache = Convert.ToString(HttpContext.Current.Cache["Index_Famous"]);
            if (string.IsNullOrEmpty(cache))
            {
                List<OL_Line> request = DataClass.Famous(top);
                foreach (OL_Line item in request)
                {
                    if (item.Pics.Length == 24)
                    {
                        item.Pics = string.Format("/images/views/{0}/m_{1}", item.Pics.Split("/".ToCharArray())[0], item.Pics.Split("/".ToCharArray())[1]);
                    }
                    else
                    {
                        item.Pics = "/images/none.gif";
                    }
                    item.PlanType = GetPlanType(item.PlanType);
                }
                cache = new JavaScriptSerializer().Serialize(request);
                HttpContext.Current.Cache.Insert("Index_Famous", cache);
            }
            return cache;
        }
        #endregion

        private static string GetPlanType(string flag)
        {
            string tname = "跟团游";
            if (flag == "1") tname = "跟团游";
            if (flag == "2") tname = "自由行";
            if (flag == "3") tname = "当地游";
            if (flag == "4") tname = "签证";
            return tname;
        }
    }
}