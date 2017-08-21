using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelOnline.Models;

namespace TravelOnline.NewPage.Class
{
    public class DataClass
    {
        //banner
        public static List<OL_FlashAD> Second_Ad_Slide(string types, int top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<OL_FlashAD> request = (from p in db.OL_FlashAD where p.AdFlag == types && p.HideFlag == "0" orderby p.AdSort, p.EditTime descending select p).Take(top).ToList();
                return request;
            }
        }
        //特价
        public static SpecialTopic Second_Line_Sell(string types, int top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                SpecialTopic request = (from p in db.SpecialTopic where p.Types == types orderby p.SortNum, p.EditTime descending select p).First();
                if (request != null)
                {
                    //List<View_SpecialLine> viewSpecialLine = (from p in db.View_SpecialLine where p.Stid == request.Id orderby p.SortNum, p.EditTime descending select p).Take(top).ToList();
                    List<View_SpecialLine_New> viewSpecialLine = (from p in db.View_SpecialLine_New where p.Stid == request.Id orderby p.SortNum, p.EditTime descending select p).Take(top).ToList();
                    if (viewSpecialLine?.Count > 0)
                    {
                        request.SpecialLineList = InitViewSpecialLine(viewSpecialLine);
                    }
                    else
                    {
                        var line_type_list = request.LineType == null ? new List<string>() : request.LineType.Split(',').ToList();
                        var destination_id_list = request.Destinationid == null ? new List<string>() : request.Destinationid.Split(',').ToList();
                        //List<View_SpecialLineTemp> viewSpecialLineTemp = (from p in db.View_SpecialLineTemp
                        //                                                  where line_type_list.Contains(p.LineClass.ToString())
                        //                                                  && (from o in db.LineDest
                        //                                                      where destination_id_list.Contains(o.destid.HasValue ? o.destid.Value.ToString() : "")
                        //                                                      select o.lineid).ToList().Contains(p.MisLineId)
                        //                                                  orderby p.TopEnd, p.EditTime descending
                        //                                                  select p).Take(top).ToList();
                        List<View_SpecialLineTemp_New> viewSpecialLineTemp = (from p in db.View_SpecialLineTemp_New
                                                                              where line_type_list.Contains(p.LineType.ToString())
                                                                          && (from o in db.LineDest
                                                                              where destination_id_list.Contains(o.destid.HasValue ? o.destid.Value.ToString() : "")
                                                                              select o.lineid).ToList().Contains(p.ErpID)
                                                                              select p).Take(top).ToList();
                        request.SpecialLineList = InitViewSpecialLineTemp(viewSpecialLineTemp);
                    }
                }
                return request;
            }
        }

        public static List<SpecialTopic> Second_Line_List(string linetype, int? top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<SpecialTopic> request = (from p in db.SpecialTopic where p.Types == linetype orderby p.SortNum, p.EditTime descending select p).ToList();
                foreach (SpecialTopic item in request)
                {
                    //List<View_SpecialLine> viewSpecialLine = top.HasValue ?
                    //    (from p in db.View_SpecialLine where p.Stid == item.Id orderby p.SortNum, p.EditTime descending select p).Take(top.Value).ToList()
                    //    : (from p in db.View_SpecialLine where p.Stid == item.Id orderby p.SortNum, p.EditTime descending select p).ToList();
                    List<View_SpecialLine_New> viewSpecialLine = top.HasValue ?
                        (from p in db.View_SpecialLine_New where p.Stid == item.Id orderby p.SortNum, p.EditTime descending select p).Take(top.Value).ToList()
                        : (from p in db.View_SpecialLine_New where p.Stid == item.Id orderby p.SortNum, p.EditTime descending select p).ToList();
                    if (viewSpecialLine?.Count > 0)
                    {
                        item.SpecialLineList = InitViewSpecialLine(viewSpecialLine);
                    }
                    else
                    {
                        string sql = "SELECT";
                        if (top != null) sql += " top " + top.Value;
                        sql += " * FROM View_SpecialLineTemp_New where 1=1";
                        if (!string.IsNullOrEmpty(item.LineType))
                        {
                            sql += " and LineType in (" + item.LineType + ")";
                        }
                        if (!string.IsNullOrEmpty(item.Destinationid))
                        {
                            sql += " and ErpID in (select lineid from linedest where destid in (0" + item.Destinationid + "0))";
                        }
                        //sql += " order by TopEnd desc,EditTime desc";
                        var query = db.Database.SqlQuery<View_SpecialLineTemp_New>(sql, new List<SqlParameter>().ToArray()).GetEnumerator();
                        List<View_SpecialLineTemp_New> result = new List<View_SpecialLineTemp_New>();
                        while (query.MoveNext())
                        {
                            result.Add(query.Current);
                        }
                        item.SpecialLineList = InitViewSpecialLineTemp(result);
                    }
                }
                return request;
            }
        }

        public static List<OL_Line> GuessYouLike(string lineclass, string linetype, string firstDestination, int top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<OL_Line> request = new List<OL_Line>();
                string sql = string.Empty;
                if (!string.IsNullOrEmpty(linetype) && firstDestination != null)
                {
                    if (linetype.Equals("visa"))
                    {
                        string destinationName = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id =" + firstDestination);
                        sql = string.Format("SELECT top {0} *, null LineTypeName " +
                                            " from OL_Line left join OL_Destination on OL_Destination.Id = OL_Line.FirstDestination " +
                                            " where (dbo.OL_Line.Destination like '%{2}%' or dbo.OL_Line.LineName like '%{2}%') and " +
                                            " Sale = '0' and Price> 0 and PlanDate>='{1}' and linetype<> '{3}'", top, DateTime.Today.ToString(), destinationName, linetype);
                        request = db.Database.SqlQuery<OL_Line>(sql, new List<SqlParameter>().ToArray()).ToList();
                        if (request.Count > 0) return request;
                    }
                }
                sql = string.Format("SELECT top {0} *,null LineTypeName " +
                                    " from OL_Line left join OL_Destination on OL_Destination.Id = firstDestination " +
                                    " where Sale = '0' and Price> 0 and PlanDate>='{1}' and LineClass='{2}'", top, DateTime.Today.ToString(), lineclass);
                request = db.Database.SqlQuery<OL_Line>(sql, new List<SqlParameter>().ToArray()).ToList();
                return request;
            }
        }

        public static List<OL_Line> Famous(int top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<OL_Line> request = new List<OL_Line>();
                string sql = string.Format("SELECT top {0} *,null LineTypeName from OL_Line left join OL_Destination on OL_Destination.Id = firstDestination where IndexRecom = 1 order by NewSortTime desc ", top);
                request = db.Database.SqlQuery<OL_Line>(sql, new List<SqlParameter>().ToArray()).ToList();
                return request;
            }
        }

        public static List<tbLine> GuessYouLikeTbLine(string lineclass, string linetype, string firstDestination, int top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<tbLine> request = new List<tbLine>();
                string sql = string.Empty;
                if (!string.IsNullOrEmpty(linetype) && firstDestination != null)
                {
                    if (linetype.Equals("visa"))
                    {
                        string destinationName = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id =" + firstDestination);
                        sql = string.Format("SELECT top {0} * " +
                                            " from tbLine left join OL_Destination on OL_Destination.Id = tbLine.Destination " +
                                            " where (dbo.tbLine.DestName like '%{2}%' or dbo.tbLine.Cname like '%{2}%') and " +
                                            " SalesTag = '0' and Price> 0 and BeginDate>='{1}' and Types<> '{3}'", top, DateTime.Today.ToString(), destinationName, GetTypes(linetype));
                        request = db.Database.SqlQuery<tbLine>(sql, new List<SqlParameter>().ToArray()).ToList();
                        if (request.Count > 0) return request;
                    }
                }
                sql = string.Format("SELECT top {0} * " +
                                    " from tbLine left join OL_Destination on OL_Destination.Id = tbLine.Destination " +
                                    " where SalesTag = '0' and Price> 0 and BeginDate>='{1}' and LineType='{2}'", top, DateTime.Today.ToString(), lineclass);
                request = db.Database.SqlQuery<tbLine>(sql, new List<SqlParameter>().ToArray()).ToList();
                return request;
            }
        }

        public static List<OL_Journal> Recom_Journals_List(string recomtype, int? top)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                List<OL_Journal> request = new List<OL_Journal>();
                string sql = string.Format("SELECT top {0} * from OL_Journal where Recom = '" + recomtype + "' and flag='1'  order by EditDate desc ", top);
                request = db.Database.SqlQuery<OL_Journal>(sql, new List<SqlParameter>().ToArray()).ToList();
                return request;
            }
        }

        private static string GetTypes(string linetype)
        {
            switch (linetype)
            {
                case "inland": return "101";
                case "outbound": return "102";
                case "freetour": return "103";
                case "cruises": return "104";
                case "visa": return "105";
                default: return "";
            }
        }

        public static int? GetIntegral(Guid uid)
        {
            using (SCYTSModel db = new SCYTSModel())
            {
                return (from p in db.OL_Integral where p.uid == uid select p).Sum(p => p.integral.HasValue ? p.integral : 0);
            }
        }

        #region 重构区

        internal static List<SpecialLine> InitViewSpecialLine(List<View_SpecialLine> parameterList)
        {
            List<SpecialLine> result = new List<SpecialLine>();
            foreach (View_SpecialLine parameter in parameterList)
            {
                SpecialLine res = new SpecialLine();
                res.AreaId = parameter.AreaId;
                res.Destination = parameter.Destination;
                res.EditTime = parameter.EditTime;
                res.Id = parameter.Id;
                res.LineClass = parameter.LineClass;
                res.LineDays = parameter.LineDays;
                res.LineFeature = parameter.LineFeature;
                res.Lineid = parameter.Lineid;
                res.LineName = parameter.LineName;
                res.LineType = parameter.LineType;
                res.MisLineId = parameter.MisLineId;
                res.Pics = "/Images/none.gif";
                if (parameter.Pics.Length == 24)
                {
                    res.Pics = string.Format("/images/views/{0}/m_{1}", parameter.Pics.Split("/".ToCharArray())[0], parameter.Pics.Split("/".ToCharArray())[1]);
                }
                if (parameter.LineType == "Visa")
                {
                    res.Pics = string.Format("/images/shadow/{0}", parameter.Pics);
                }
                res.PlanDate = parameter.PlanDate;
                res.PlanDateStr = parameter.PlanDate.HasValue ? parameter.PlanDate.Value.ToString("yyyy-MM-dd") : "";
                res.PlanType = parameter.PlanType;
                res.PlanTypeName = parameter.PlanTypeName;
                res.Price = parameter.Price;
                res.SortNum = parameter.SortNum;
                res.Standard = parameter.Standard;
                res.Stid = parameter.Stid;
                res.Topic = parameter.Topic;
                result.Add(res);
            }
            return result;
        }
        internal static List<SpecialLine> InitViewSpecialLine(List<View_SpecialLine_New> parameterList)
        {
            List<SpecialLine> result = new List<SpecialLine>();
            foreach (View_SpecialLine_New parameter in parameterList)
            {
                SpecialLine res = new SpecialLine();
                res.EditTime = parameter.EditTime;
                res.Id = parameter.Id;
                res.LineDays = (byte)parameter.Days;
                res.LineClass = parameter.LineType;
                res.Lineid = parameter.Lineid;
                res.LineName = parameter.Cname;
                res.Destination = parameter.DestName;
                res.MisLineId = parameter.ErpID;
                res.Pics = "/Images/none.gif";
                if (parameter.PhotoPath.Length == 24)
                {
                    res.Pics = string.Format("/images/views/{0}/m_{1}", parameter.PhotoPath.Split("/".ToCharArray())[0], parameter.PhotoPath.Split("/".ToCharArray())[1]);
                }
                if (parameter.Types == 110)
                {
                    res.Pics = string.Format("/images/shadow/{0}", parameter.PhotoPath);
                }
                res.PlanDate = parameter.BeginDate;
                res.PlanDateStr = parameter.BeginDate.HasValue ? parameter.BeginDate.Value.ToString("yyyy-MM-dd") : "";
                //res.PlanType = parameter.PlanType;
                //res.PlanTypeName = parameter.PlanTypeName;
                res.Price = parameter.Price;
                res.SortNum = parameter.SortNum;
                res.Stid = parameter.Stid;
                //res.Topic = parameter.Topic;
                result.Add(res);
            }
            return result;
        }
        internal static List<SpecialLine> InitViewSpecialLineTemp(List<View_SpecialLineTemp> parameterList)
        {
            List<SpecialLine> result = new List<SpecialLine>();
            foreach (View_SpecialLineTemp parameter in parameterList)
            {
                SpecialLine res = new SpecialLine();
                res.AreaId = parameter.AreaId;
                res.Destination = parameter.Destination;
                res.EditTime = parameter.EditTime;
                res.Id = parameter.Id;
                res.LineClass = parameter.LineClass;
                res.LineDays = parameter.LineDays;
                res.LineFeature = parameter.LineFeature;
                res.Lineid = parameter.Lineid;
                res.LineName = parameter.LineName;
                res.LineType = parameter.LineType;
                res.MisLineId = parameter.MisLineId;
                res.Pics = "/Images/none.gif";
                if (parameter.Pics.Length == 24)
                {
                    res.Pics = string.Format("/images/views/{0}/m_{1}", parameter.Pics.Split("/".ToCharArray())[0], parameter.Pics.Split("/".ToCharArray())[1]);
                }
                if (parameter.LineType == "Visa")
                {
                    res.Pics = string.Format("/images/shadow/{0}", parameter.Pics);
                }
                res.PlanDate = parameter.PlanDate;
                res.PlanDateStr = parameter.PlanDate.HasValue ? parameter.PlanDate.Value.ToString("yyyy-MM-dd") : "";
                res.PlanType = parameter.PlanType;
                res.PlanTypeName = parameter.PlanTypeName;
                res.Price = parameter.Price;
                res.SortNum = parameter.SortNum;
                res.Standard = parameter.Standard;
                res.Stid = parameter.Stid;
                res.Topic = parameter.Topic;
                result.Add(res);
            }
            return result;
        }
        internal static List<SpecialLine> InitViewSpecialLineTemp(List<View_SpecialLineTemp_New> parameterList)
        {
            List<SpecialLine> result = new List<SpecialLine>();
            foreach (View_SpecialLineTemp_New parameter in parameterList)
            {
                SpecialLine res = new SpecialLine();
                res.EditTime = parameter.CreateTime;
                res.Id = parameter.Id;
                res.LineClass = parameter.LineType;
                res.LineDays = (byte)parameter.Days;
                res.Lineid = parameter.lineId;
                res.LineName = parameter.LineName;
                res.Destination = parameter.DestName;
                //res.LineType = parameter.LineType;
                res.MisLineId = parameter.ErpID;
                res.Pics = "/Images/none.gif";
                if (parameter.PhotoPath.Length == 24)
                {
                    res.Pics = string.Format("/images/views/{0}/m_{1}", parameter.PhotoPath.Split("/".ToCharArray())[0], parameter.PhotoPath.Split("/".ToCharArray())[1]);
                }
                if (parameter.Types == 110)
                {
                    res.Pics = string.Format("/images/shadow/{0}", parameter.PhotoPath);
                }
                res.PlanDate = parameter.BeginDate;
                res.PlanDateStr = parameter.BeginDate.HasValue ? parameter.BeginDate.Value.ToString("yyyy-MM-dd") : "";
                //res.PlanType = parameter.PlanType;
                //res.PlanTypeName = parameter.PlanTypeName;
                res.Price = parameter.Price;
                res.SortNum = parameter.SortNum;
                //res.Standard = parameter.Standard;
                res.Stid = parameter.Stid;
                res.Topic = parameter.Topic;
                result.Add(res);
            }
            return result;
        }

        #endregion
    }
}