using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using TravelOnline.Class.Travel;
using System.Collections;

namespace TravelOnline.Class.Purchase
{
    public class PurchaseAutoRun
    {
        public static void OrderAutoAdjuest()
        {
            //List<string> Sql = new List<string>();
            //Sql.Add(string.Format("update Pre_Ticket set flag='2' where flag='0' and enddate <'{0:yyyy-MM-dd}'", DateTime.Today));
            //string[] SqlQuery = Sql.ToArray();
            //MyDataBaseComm.Transaction(SqlQuery);

            DateTime date = DateTime.Today;
            date = date.AddDays(-3);
            string SqlQueryText = string.Format("select * from OL_Order where OrderFlag='1' and PayFlag='0' and shipid='0' and OrderTime >= '{0}'", date);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            DateTime times = DateTime.Now;
            times = times.AddHours(-24);

            DateTime gn_times = DateTime.Now;
            gn_times = gn_times.AddHours(-12);

            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                //ProductType
                if (DS.Tables[0].Rows[i]["ProductType"].ToString() == "InLand")
                {
                    if (Convert.ToDateTime(DS.Tables[0].Rows[i]["OrderTime"]) <= gn_times)
                    {
                        string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                        TravelOnlineService rsp = new TravelOnlineService();
                        rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                        try
                        {
                            string Result = rsp.OnlineOrderAdjust(UpPassWord, DS.Tables[0].Rows[i]["OrderId"].ToString());
                            if (Result == "OK")
                            {
                                List<string> Sql = new List<string>();
                                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS.Tables[0].Rows[i]["OrderId"].ToString(), DateTime.Now.ToString(), "您的订单超过了在线支付时限，已自动变更为需确认状态！"));
                                Sql.Add(string.Format("update OL_Order set OrderFlag='0' where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                                string[] SqlQuery = Sql.ToArray();
                                MyDataBaseComm.Transaction(SqlQuery);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    if (Convert.ToDateTime(DS.Tables[0].Rows[i]["OrderTime"]) <= times)
                    {
                        string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                        TravelOnlineService rsp = new TravelOnlineService();
                        rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                        try
                        {
                            string Result = rsp.OnlineOrderAdjust(UpPassWord, DS.Tables[0].Rows[i]["OrderId"].ToString());
                            if (Result == "OK")
                            {
                                List<string> Sql = new List<string>();
                                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS.Tables[0].Rows[i]["OrderId"].ToString(), DateTime.Now.ToString(), "您的订单超过了在线支付时限，已自动变更为需确认状态！"));
                                Sql.Add(string.Format("update OL_Order set OrderFlag='0' where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                                string[] SqlQuery = Sql.ToArray();
                                MyDataBaseComm.Transaction(SqlQuery);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                
            }

            //string path = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";
            //try
            //{
            //    StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
            //    writer.WriteLine(DateTime.Now.ToString() + "///");
            //    writer.Close();
            //}
            //catch (Exception exception)
            //{
            //    string message = exception.Message;
            //}
        }

        public static void DeleteTempOrder()
        {
            DateTime times = DateTime.Now;
            times = times.AddHours(-12);

            string SqlQueryText = string.Format("select * from OL_TempOrder where ProductType<>'Coupon' and OrderFlag='0' and OrderTime <= '{0}'", times);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_RoomList where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_RoomOrder where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderExtend where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderLog where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                }
                string[] SqlQueryList = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQueryList);
            }
        }

        public static void DeleteAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            string flag = "0";
            foreach (string key in al)
            {
                flag = "0";
                if (key.IndexOf("LineClassTitleString") > -1) flag = "1";
                if (key.IndexOf("LineProvinceTitleString") > -1) flag = "1";
                if (key.IndexOf("LineListProvinceSort") > -1) flag = "1";
                if (key.IndexOf("LineListProvinceSortId") > -1) flag = "1";
                if (key.IndexOf("LineListCitySort") > -1) flag = "1";
                if (key.IndexOf("LineListCitySortId") > -1) flag = "1";
                
                if (flag == "0") _cache.Remove(key);
            } 
        }

        public static void DeleteSelectCache(string sname)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            string flag = "0";
            foreach (string key in al)
            {
                if (key.IndexOf(sname) > -1) _cache.Remove(key);
            }
        }

        public static void DeleteWeekTempOrder()
        {
            DateTime times = DateTime.Now;
            times = times.AddDays(-7);

            string SqlQueryText = string.Format("select * from OL_TempOrder where ProductType<>'Coupon' and OrderFlag='9' and OrderTime <= '{0}'", times);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_RoomList where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_RoomOrder where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderExtend where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderLog where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                }
                string[] SqlQueryList = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQueryList);
            }

        }


        public static void AutoGetOrderIntegral()
        {
            DateTime times = DateTime.Now;
            times = times.AddDays(-14);

            int Integral = 0;
            decimal rate = 0;
            string SqlQueryText = string.Format("select *,DATEADD(MONTH,DATEDIFF(MONTH,-1,GETDATE()),-1) as daterange from View_MemberOrder where Integral<>'1' and enddate <= '{0}'", times);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (MyConvert.ConToDec(DS.Tables[0].Rows[i]["Price"].ToString()) > 0)
                    {
                        Integral = 0;
                        rate = 0;
                        times = Convert.ToDateTime(DS.Tables[0].Rows[i]["daterange"].ToString());
                        times = times.AddYears(2);
                        if (MyConvert.ConToDec(DS.Tables[0].Rows[i]["Integral"].ToString()) > 0)
                        {
                            rate = MyConvert.ConToDec(DS.Tables[0].Rows[i]["Integral"].ToString());
                        }
                        else
                        {
                            switch (DS.Tables[0].Rows[i]["ProductType"].ToString())
                            {
                                case "OutBound":
                                    rate = MyConvert.ConToDec(Convert.ToString(ConfigurationManager.AppSettings["OutBoundIntegral"]));
                                    break;
                                case "InLand":
                                    rate = MyConvert.ConToDec(Convert.ToString(ConfigurationManager.AppSettings["InLandIntegral"]));
                                    break;
                                case "FreeTour":
                                    rate = MyConvert.ConToDec(Convert.ToString(ConfigurationManager.AppSettings["FreeTourIntegral"]));
                                    break;
                                case "Cruises":
                                    rate = MyConvert.ConToDec(Convert.ToString(ConfigurationManager.AppSettings["CruisesIntegral"]));
                                    break;
                                case "Visa":
                                    rate = MyConvert.ConToDec(Convert.ToString(ConfigurationManager.AppSettings["VisaIntegral"]));
                                    break;
                                default:
                                    rate = 0;
                                    break;
                            }
                        }

                        Integral = (int)((MyConvert.ConToDec(DS.Tables[0].Rows[i]["Price"].ToString()) * rate) - MyConvert.ConToDec("0.5"));
                        
                        Sql.Add(string.Format("insert into OL_Integral (uid,orderid,lineid,integral,getdate,flag,dept,enddate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            DS.Tables[0].Rows[i]["OrderUser"].ToString(),
                            DS.Tables[0].Rows[i]["OrderId"].ToString(),
                            DS.Tables[0].Rows[i]["lineid"].ToString(),
                            Integral,
                            DateTime.Now,
                            "0",
                            DS.Tables[0].Rows[i]["DeptId"].ToString(),
                            times
                        ));
                        Sql.Add(string.Format("update OL_Order set OrderFlag='3' where OrderId='{0}'",
                            DS.Tables[0].Rows[i]["OrderId"].ToString()
                        ));
                    }
                    
                }
                string[] SqlQueryList = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQueryList);
            }

        }

        public static void CruisesLineAdjuest()
        {
            DateTime date = DateTime.Today;
            string SqlQueryText = string.Format("select MisLineId from OL_Line where shipid>0 and PlanDate>'{0}'", date);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                HttpContext.Current.Cache.Insert(string.Format("CruisesRoomString{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString()), "");
            }
        }

        public static void CancelLineTop()
        {
            string SqlQueryText = "";
            SqlQueryText = string.Format("update OL_Line set TopBegin = null ,TopEnd= null where TopEnd<'{0}'", DateTime.Today);

            MyDataBaseComm.ExcuteSql(SqlQueryText);
        }

        public static void ClearShip_Line_14120()//台湾航线清位
        {
            List<string> Sql;

            DateTime date = DateTime.Today;
            date = date.AddDays(-3);
            string SqlQueryText = string.Format("select * from OL_Order where OrderFlag='1' and PayFlag='0' and lineid='14120' and OrderTime < '{0}'", date);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                Sql = new List<string>();

                //舱房取消
                Sql.Add(string.Format("update CR_RoomList set orderflag='1',roomnoid='0',roomno='' where OrderId='{0}'",
                    DS.Tables[0].Rows[i]["OrderId"].ToString()
                ));

                //取消游客
                Sql.Add(string.Format("update OL_GuestInfo set flag='1' where OrderId='{0}'",
                    DS.Tables[0].Rows[i]["OrderId"].ToString()
                ));

                //变更观光明细为取消状态
                Sql.Add(string.Format("update CR_VisitList set flag='1' where OrderId='{0}'",
                    DS.Tables[0].Rows[i]["OrderId"].ToString()
                ));

                //订单金额全退
                Sql.Add(string.Format("update OL_Order set OrderFlag='8',Price=0,rebate=0 where OrderId='{0}'",
                    DS.Tables[0].Rows[i]["OrderId"].ToString()
                ));

                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", 
                    DS.Tables[0].Rows[i]["OrderId"].ToString(),
                    DateTime.Now.ToString(),
                    "您的订单已被自动取消！"
                ));
                                

                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    string result = PurchaseClass.CruisesOrderAdjust(DS.Tables[0].Rows[i]["OrderId"].ToString(), "OrderRetreat", "Yes");
                }
                
            }
        }

        public static void DeleteTodayLine()
        {
            DateTime times = DateTime.Now;
            string SqlQueryText = "select a.id,a.lineid,b.Cname,b.types from SpecialLine as a left join SpecialTopic as b on a.Stid=b.id left join ol_line as c on a.lineid =c.MisLineId where 1=1 and c.Sale='1'";
            SqlQueryText += " and b.types in ('Index_Sell','Index_Season','Index_Outbound','Index_Inland','Index_Freetour','Outbound_Hot','Outbound_Sell','Outbound_01','Outbound_02','Outbound_03',";
            SqlQueryText += "'Inland_Hot','Inland_Sell','Inland_01','Inland_02','Freetour_Hot','Freetour_Sell','Freetour_01','Freetour_02','Freetour_03')";
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SaveErrorToLog(string.Format("删除过期的线路，线路id：{0}，专题类型：{1}{2}", dt.Rows[i]["lineid"].ToString(), UIHelper.DictionaryKeyToValue(dt.Rows[i]["types"].ToString(), BindDicSpecialType()), dt.Rows[i]["Cname"].ToString()), string.Empty);
                    Sql.Add(string.Format("delete from SpecialLine where id='{0}'", dt.Rows[i]["id"].ToString()));
                }
                string[] SqlQueryList = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQueryList);
            }
        }

        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\AutoRunLog.txt";

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

        public static IDictionary<string, string> dicSpecialType;
        private static IDictionary<string, string> BindDicSpecialType()
        {
            dicSpecialType = new Dictionary<string, string>();
            dicSpecialType.Add("Index_Sell", "首页特价精选");
            dicSpecialType.Add("Index_Season", "首页当季推荐");
            dicSpecialType.Add("Index_Outbound", "首页出境游");
            dicSpecialType.Add("Index_Inland", "首页国内游");
            dicSpecialType.Add("Index_Cruise", "首页邮轮");
            dicSpecialType.Add("Index_Freetour", "首页自由行");
            dicSpecialType.Add("Index_Visa", "首页签证");
            dicSpecialType.Add("Outbound_Hot", "出境热销排行");
            dicSpecialType.Add("Outbound_Sell", "出境特价精选");
            dicSpecialType.Add("Outbound_01", "出境短线");
            dicSpecialType.Add("Outbound_02", "出境长线");
            dicSpecialType.Add("Outbound_03", "出境主题旅游");
            dicSpecialType.Add("Inland_Hot", "国内热销排行");
            dicSpecialType.Add("Inland_Sell", "国内特价精选");
            dicSpecialType.Add("Inland_01", "国内推荐目的地");
            dicSpecialType.Add("Inland_02", "国内主题旅游");
            dicSpecialType.Add("Freetour_Hot", "自由行热销排行");
            dicSpecialType.Add("Freetour_Sell", "自由行特价精选");
            dicSpecialType.Add("Freetour_01", "自由行出境短线");
            dicSpecialType.Add("Freetour_02", "自由行出境长线");
            dicSpecialType.Add("Freetour_03", "自由行国内热门");
            dicSpecialType.Add("Cruise_Best", "邮轮线路精选");
            dicSpecialType.Add("Visa_All", "所有签证");
            dicSpecialType.Add("OTA", "电商产品");
            dicSpecialType.Add("WeChat_FlashSale", "微信限时抢购");
            return dicSpecialType;
        }
    }
}