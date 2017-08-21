using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Configuration;
using TravelOnline.GetCombineKeys;
using TravelOnline.Class.Manage;
using TravelOnline.Class.Travel;
using System.Data;
using TravelOnline.Class.Purchase;

namespace TravelOnline.WebService
{
    /// <summary>
    /// LineUpLoad 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://travelmis.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class LineUpLoad : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
        public class LineInfos
        {
            public string Id { get; set; }
            public string MisId { get; set; }
            public string LineName { get; set; }
            public string LineType { get; set; }
            public string LineClass { get; set; } //LineDays Standard
            public string Topic { get; set; }
            public string LineDays { get; set; }
            public string Standard { get; set; }
            public string Price { get; set; }
            public string MsPrice { get; set; }
            public string AreaId { get; set; }
            public string LineFeature { get; set; }
            public string PlanDate { get; set; }
            public string DeptId { get; set; }
            public string Pics { get; set; }
            public string SpFlag { get; set; }
            public string yfk { get; set; }
            public string Tags { get; set; }
            public string CuisesId { get; set; }
            public string SfId { get; set; }
            public string wwwyh { get; set; }
            public string VisaId { get; set; }
            public string CruisesPlanId { get; set; }
            
            public RouteService RouteServices { get; set; }
            public RouteInfos[] RouteDetail { get; set; }
            public PlanInfos[] PlanDetail { get; set; }

            public VisaInfo VisaInfos { get; set; }
            public VisaDocument[] VisaDocuments { get; set; }
        }

        public class MisOrderInfo
        {
            public string AutoId { get; set; }
            public string PriceName { get; set; }
            public string Price { get; set; }
            public string username { get; set; }
        }

        public class VisaInfo
        {
            public string Validity { get; set; }
            public string Stay { get; set; }
            public string WorkDay { get; set; }
            public string Memo { get; set; }
        }

        public class CouponInfo
        {
            public string id { get; set; }
            public string par { get; set; }
            public string deduction { get; set; }
            public string infos { get; set; }
        }

        public class VisaDocument
        {
            public string Flag { get; set; }
            public string FlagName { get; set; }
            public string VisaType { get; set; }
            public string VisaName { get; set; }
            public string VisaContent { get; set; }
            public string VisaFile { get; set; }
            public string V1 { get; set; }
            public string V2 { get; set; }
        }

        public class RouteService
        {
            public string Feature { get; set; } //行程特色
            public string Attentions { get; set; }
            public string PriceIn { get; set; }
            public string PriceOut { get; set; }
            public string OwnExpense { get; set; }
            public string Shopping { get; set; }
            public string TravelAgency { get; set; }

            public string Scenery { get; set; }
            public string Traffic { get; set; }
            public string Hotel { get; set; }
            public string Foods { get; set; }
            public string Guide { get; set; }
            public string Insure { get; set; }
            public string Others { get; set; }
        }

        public class RouteInfos
        { 
            public string rname { get; set; }
            public string route { get; set; }
            public string dinner { get; set; }
            public string bus { get; set; }
            public string room { get; set; }
            public string daterank { get; set; }
            public string Pics { get; set; }
        }

        public class PlanInfos
        {
            public string PlanId { get; set; }
            public string BgnDate { get; set; }
            public string Price { get; set; }
            public string ChildPrice { get; set; }
        }

        public class OrderGuestList
        {
            public string OrderId { get; set; }
            public string GuestId { get; set; }
            public string GuestName { get; set; }
            public string GuestEnName { get; set; }
            public string Sex { get; set; }
            public string IdType { get; set; }
            public string IdNumber { get; set; }
            public string BirthDay { get; set; }
            public string PassType { get; set; }
            public string PassBgn { get; set; }
            public string PassEnd { get; set; }
            public string Sign { get; set; }
            public string Home { get; set; }
            public string Tel { get; set; }
            public string Mobile { get; set; }
            public string rankno { get; set; }
            public string IdentityCard { get; set; }
            public string Address { get; set; }
            public string EAddress { get; set; }
            public string ESign { get; set; }
            public string WWWPlanNo { get; set; }
            public string WWWPlanId { get; set; }
            public string Company { get; set; }
            public string Vocation { get; set; }
            public string Country { get; set; }
            public string TongXing { get; set; }
            public string firstcj { get; set; }
            public string cjdate { get; set; }
            public string cjmdd { get; set; }
            public string cjsy { get; set; }
        }

        public class OrderPriceList
        {
            public string PriceType { get; set; }
            public string PriceId { get; set; }
            public string PriceName { get; set; }
            public string PriceMemo { get; set; }
            public string SellPrice { get; set; }
            public string OrderNums { get; set; }
            public string SumPrice { get; set; }
            public string allmdjs { get; set; }
        }

        public class OrderGuestAndPrice
        {
            public string OrderId { get; set; }
            public string GFlag { get; set; }
            public string PFlag { get; set; }
            public OrderGuestList[] Guests { get; set; }
            public OrderPriceList[] Prices { get; set; }
        }

        [WebMethod]
        public string GetOnlineOrderPrice(MisOrderInfo Infos, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "-1";

            string SqlQueryText = string.Format("select * from OL_Order where AutoId='{0}'", Infos.AutoId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int rebate = Convert.ToInt32(DS.Tables[0].Rows[0]["rebate"].ToString());
                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                if (DS.Tables[0].Rows[0]["RebateFlag"].ToString() == "1")
                {
                    strings = (price - rebate).ToString();
                }
                else
                {
                    strings = DS.Tables[0].Rows[0]["Price"].ToString();
                }
                //OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                //strings = DS.Tables[0].Rows[0]["Price"].ToString();
            }
            return strings;
        }

        //调整畅游订单金额
        [WebMethod]
        public string OnlineOrderPriceAdjust(string ids, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string result = "";

            string SqlQueryText = string.Format("select * from OL_Order where AutoId in ({0})", ids);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    result = PurchaseClass.CruisesOrderAdjust(DS.Tables[0].Rows[i]["OrderId"].ToString(), "AdjustPrice", "Yes");
                    
                }
            }
            return result;
        }

        [WebMethod]
        public string UpdateCruisesGuestMobile(OrderGuestList Infos, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string SqlQueryText = string.Format("update OL_GuestInfo set Mobile='{1}' where id='{0}'",
                Infos.GuestId,
                Infos.Mobile
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                strings = "OK";
            }
            
            return strings;
        }

        [WebMethod]
        public string SetCruisesLeader(string GuestId, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string SqlQueryText = string.Format("update OL_GuestInfo set IsLeader='1' where id='{0}'",
                GuestId
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                strings = "OK";
            }

            return strings;
        }

        [WebMethod]
        public string CancelCruisesLeader(string GuestId, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string SqlQueryText = string.Format("update OL_GuestInfo set IsLeader='0' where id='{0}'",
                GuestId
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                strings = "OK";
            }

            return strings;
        }

        [WebMethod]
        public string UpdateCruisesGuestInfo(OrderGuestList Infos, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string SqlQueryText = string.Format("select id from OL_GuestInfo where OrderId='{0}' and id='{1}'", Infos.OrderId, Infos.GuestId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //Home='{10}',Tel='{11}',IdentityCard='{12}',Address='{13}',EAddress='{14}',ESign=
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("update OL_GuestInfo set GuestName='{2}',GuestEnName='{3}',Sex='{4}',IdNumber='{5}',BirthDay={6},PassBgn={7},PassEnd={8},Sign='{9}',Home='{10}',Tel='{11}',IdentityCard='{12}',Address='{13}',EAddress='{14}',ESign='{15}',Mobile='{16}',Company='{17}',Vocation='{18}',Country='{19}',TongXing='{20}',firstcj='{21}',cjdate='{22}',cjmdd='{23}',cjsy='{24}' where OrderId='{0}' and id='{1}'",
                    Infos.OrderId,
                    Infos.GuestId,
                    Infos.GuestName,
                    Infos.GuestEnName,
                    Infos.Sex,
                    Infos.IdNumber,
                    MyConvert.ConToDate(Infos.BirthDay),
                    MyConvert.ConToDate(Infos.PassBgn),
                    MyConvert.ConToDate(Infos.PassEnd),
                    Infos.Sign,
                    Infos.Home,
                    Infos.Tel,
                    Infos.IdentityCard,
                    Infos.Address,
                    Infos.EAddress,
                    Infos.ESign,
                    Infos.Mobile,
                    Infos.Company,
                    Infos.Vocation,
                    Infos.Country,
                    Infos.TongXing,
                    Infos.firstcj,
                    Infos.cjdate,
                    Infos.cjmdd,
                    Infos.cjsy
                ));
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    strings = "OK";
                }
            }
            return strings;
        }

        [WebMethod]
        public string ChangeCruisesOrderPrice(MisOrderInfo Infos, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string OrderId="";
            string SqlQueryText = string.Format("select * from OL_Order where AutoId='{0}'", Infos.AutoId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
            
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                Sql.Add(string.Format("update OL_Order set Price=Price + {0} where OrderId='{1}'", Infos.Price, OrderId));
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), Infos.username + "调整了费用，金额：" + Infos.Price));
                Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    OrderId,
                    "ExtPrice",
                    "0",
                    "费用调整",
                    Infos.PriceName,
                    Infos.Price,
                    "1",
                    Infos.Price,
                    DateTime.Now.ToString()
                ));
                
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    strings = "OK";
                }
            }
            return strings;
        }

        [WebMethod]
        public OrderGuestAndPrice GetOrderGuestAndPrice(string OrderId, string UpPassWord)
        {
            OrderGuestAndPrice OrderInfos = new OrderGuestAndPrice();
            OrderInfos.OrderId = "0";
            OrderInfos.GFlag = "0";
            OrderInfos.PFlag = "0";
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]))
            {
                return OrderInfos;
            }

            OrderInfos.OrderId = OrderId;


            string SqlQueryText = string.Format("select *,(select PlanNo from CR_PlanNo where id=OL_GuestInfo.PlanAllotid) as PlanNo from OL_GuestInfo where (flag='0' or flag is null) and OrderId='{0}'", OrderId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                OrderInfos.GFlag = DS.Tables[0].Rows.Count.ToString();
                OrderInfos.Guests = new OrderGuestList[DS.Tables[0].Rows.Count];
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                { 
                    OrderInfos.Guests[i] = new OrderGuestList();
                    OrderInfos.Guests[i].GuestId = DS.Tables[0].Rows[i]["id"].ToString();
                    OrderInfos.Guests[i].BirthDay = DS.Tables[0].Rows[i]["BirthDay"].ToString();
                    OrderInfos.Guests[i].GuestEnName = DS.Tables[0].Rows[i]["GuestEnName"].ToString();
                    OrderInfos.Guests[i].GuestName = DS.Tables[0].Rows[i]["GuestName"].ToString();
                    OrderInfos.Guests[i].Home = DS.Tables[0].Rows[i]["Home"].ToString();
                    OrderInfos.Guests[i].IdNumber = DS.Tables[0].Rows[i]["IdNumber"].ToString();
                    OrderInfos.Guests[i].IdType = DS.Tables[0].Rows[i]["IdType"].ToString();
                    OrderInfos.Guests[i].PassBgn = DS.Tables[0].Rows[i]["PassBgn"].ToString();
                    OrderInfos.Guests[i].PassEnd = DS.Tables[0].Rows[i]["PassEnd"].ToString();
                    OrderInfos.Guests[i].PassType = DS.Tables[0].Rows[i]["PassType"].ToString();
                    OrderInfos.Guests[i].Sex = DS.Tables[0].Rows[i]["Sex"].ToString();
                    OrderInfos.Guests[i].Sign = DS.Tables[0].Rows[i]["Sign"].ToString();
                    OrderInfos.Guests[i].Tel = DS.Tables[0].Rows[i]["Tel"].ToString();
                    OrderInfos.Guests[i].rankno = DS.Tables[0].Rows[i]["rankno"].ToString();
                    OrderInfos.Guests[i].WWWPlanId = DS.Tables[0].Rows[i]["PlanAllotid"].ToString();
                    OrderInfos.Guests[i].WWWPlanNo = DS.Tables[0].Rows[i]["PlanNo"].ToString();
                    OrderInfos.Guests[i].Mobile = DS.Tables[0].Rows[i]["Mobile"].ToString();
                }
            }

            string price = "0", shipid = "0", OrderNums = "0", rebate = "0", allmdjs = "0";
            string RebateFlag = "0";
            //if (RebateFlag == "1")
            //{
            //    Sorder.gathering = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["rebate"].ToString())).ToString();
            //}
            SqlQueryText = string.Format("select OrderNums,Price,shipid,rebate,allmdjs,RebateFlag from OL_Order where OrderId='{0}'", OrderId);
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                price = DS.Tables[0].Rows[0]["Price"].ToString();
                OrderNums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
                rebate = DS.Tables[0].Rows[0]["rebate"].ToString();
                allmdjs = DS.Tables[0].Rows[0]["allmdjs"].ToString();
            }

            if (MyConvert.ConToInt(shipid) > 0)
            {
                OrderInfos.PFlag = DS.Tables[0].Rows.Count.ToString();

                if (RebateFlag == "1" && MyConvert.ConToInt(rebate) > 0)
                {
                    OrderInfos.Prices = new OrderPriceList[2];
                    OrderInfos.Prices[0] = new OrderPriceList();
                    OrderInfos.Prices[0].OrderNums = OrderNums;
                    OrderInfos.Prices[0].PriceId = "0";
                    OrderInfos.Prices[0].PriceMemo = "邮轮包船团费";
                    OrderInfos.Prices[0].PriceName = "团费";
                    OrderInfos.Prices[0].PriceType = "SellPrice";
                    OrderInfos.Prices[0].SellPrice = price;
                    OrderInfos.Prices[0].SumPrice = price;
                    OrderInfos.Prices[0].allmdjs = allmdjs;

                    OrderInfos.Prices[1] = new OrderPriceList();
                    OrderInfos.Prices[1].OrderNums = OrderNums;
                    OrderInfos.Prices[1].PriceId = "0";
                    OrderInfos.Prices[1].PriceMemo = "邮轮包船费用减免";
                    OrderInfos.Prices[1].PriceName = "团费";
                    OrderInfos.Prices[1].PriceType = "SellPrice";
                    OrderInfos.Prices[1].SellPrice = "-" + rebate;
                    OrderInfos.Prices[1].SumPrice = "-" + rebate;
                    OrderInfos.Prices[1].allmdjs = "0";
                }
                else
                {
                    OrderInfos.Prices = new OrderPriceList[DS.Tables[0].Rows.Count];
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        OrderInfos.Prices[i] = new OrderPriceList();
                        OrderInfos.Prices[i].OrderNums = OrderNums;
                        OrderInfos.Prices[i].PriceId = "0";
                        OrderInfos.Prices[i].PriceMemo = "邮轮包船团费";
                        OrderInfos.Prices[i].PriceName = "团费";
                        OrderInfos.Prices[i].PriceType = "SellPrice";
                        OrderInfos.Prices[i].SellPrice = price;
                        OrderInfos.Prices[i].SumPrice = price;
                        OrderInfos.Prices[i].allmdjs = allmdjs;
                    }
                }
            }
            else
            {
                SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}'", OrderId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    OrderInfos.PFlag = DS.Tables[0].Rows.Count.ToString();
                    OrderInfos.Prices = new OrderPriceList[DS.Tables[0].Rows.Count];
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        OrderInfos.Prices[i] = new OrderPriceList();
                        OrderInfos.Prices[i].OrderNums = DS.Tables[0].Rows[i]["OrderNums"].ToString();
                        OrderInfos.Prices[i].PriceId = DS.Tables[0].Rows[i]["PriceId"].ToString();
                        OrderInfos.Prices[i].PriceMemo = DS.Tables[0].Rows[i]["PriceMemo"].ToString();
                        OrderInfos.Prices[i].PriceName = DS.Tables[0].Rows[i]["PriceName"].ToString();
                        OrderInfos.Prices[i].PriceType = DS.Tables[0].Rows[i]["PriceType"].ToString();
                        OrderInfos.Prices[i].SellPrice = DS.Tables[0].Rows[i]["SellPrice"].ToString();
                        OrderInfos.Prices[i].SumPrice = DS.Tables[0].Rows[i]["SumPrice"].ToString();
                        OrderInfos.Prices[i].allmdjs = "0";
                    }
                }
            }
            
            return OrderInfos;
        }

        [WebMethod]
        public string ChangeOrderFlag(string OrderId, string Flag,string LogInfo,string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            string SqlQueryText = string.Format("select shipid from OL_Order where OrderId='{0}'", OrderId);
            DataSet DS = new DataSet(); 
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Flag == "8")
                {
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) > 0)
                    {
                        strings = "邮轮包船订单，不允许从畅游取消";
                        return strings;
                    }
                }

                List<string> Sql = new List<string>();
                Sql.Add(string.Format("update OL_Order set OrderFlag='{0}' where OrderId='{1}'", Flag, OrderId));
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), LogInfo));
                if (Flag == "8")
                {
                    //取消订单时，把对应使用优惠券和积分一起取消
                    Sql.Add(string.Format("update Pre_Ticket set flag=0 where OrderId='{0}'", OrderId));
                    Sql.Add(string.Format("delete OL_Integral where OrderId='{0}'", OrderId));
                }
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    strings = "OK";
                }
                
            }
            return strings;
        }

        [WebMethod]
        public string ChangeOrderTime(string OrderId, string Flag, string LogInfo, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "Error";
            string strings = "Error";

            List<string> Sql = new List<string>();
            Sql.Add(string.Format("update OL_Order set OrderFlag='{0}',OrderTime='{2}' where OrderId='{1}'", Flag, OrderId, DateTime.Now.ToString()));
            Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), LogInfo));
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                strings = "OK";
            }
            return strings;
        }
        
        
        [WebMethod]
        public string LineInfoUpLoad(LineInfos UpLoadInfo,string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "上传密码错误！";
            if (UpLoadInfo == null) return "没有任何数据，不能上传！";

            StringBuilder Stings = new StringBuilder();
            string PlanDates;
            if (UpLoadInfo.PlanDetail != null)
            {
                for (int i = 0; i < UpLoadInfo.PlanDetail.Length; i++)
                {
                    DateTime begindate = Convert.ToDateTime(UpLoadInfo.PlanDetail[i].BgnDate);
                    Stings.Append(string.Format("{0}/{1},", begindate.ToString("MM"), begindate.ToString("dd")));
                }
            }
            PlanDates = Stings.ToString();
            if (PlanDates.Length > 2) PlanDates = PlanDates.Substring(0, PlanDates.Length - 1);
            Stings.Clear();

            string SqlQueryText = string.Format("select top 1 Id from OL_Line where MisLineId='{0}'", UpLoadInfo.MisId);
            string CombId = MyDataBaseComm.getScalar(SqlQueryText);
            if (CombId == null)
            {
                
                UpLoadInfo.Id = Convert.ToString(CombineKeys.NewComb());
                Stings.Append("insert into OL_Line ");
                Stings.Append("(Id,MisLineId,LineType,LineName,LineClass,LineDays,Standard,Topic,Price,AreaId,LineFeature,PlanDate,DeptId,Pics,EditTime,Pdates,SpFlag,MsPrice,yfk,Tags,CuisesId,PlanType,wwwyh,VisaId,Planid)");
                Stings.Append(" values ");
                Stings.Append("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',',{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')");
                SqlQueryText = string.Format(Stings.ToString(),
                    UpLoadInfo.Id,
                    UpLoadInfo.MisId,
                    UpLoadInfo.LineType,
                    UpLoadInfo.LineName,
                    UpLoadInfo.LineClass,
                    UpLoadInfo.LineDays,
                    UpLoadInfo.Standard,
                    UpLoadInfo.Topic,
                    UpLoadInfo.Price,
                    UpLoadInfo.AreaId,
                    UpLoadInfo.LineFeature,
                    MyConvert.ConToDate(UpLoadInfo.PlanDate),
                    UpLoadInfo.DeptId,
                    UpLoadInfo.Pics,
                    DateTime.Now.ToString(),
                    PlanDates,
                    UpLoadInfo.SpFlag,
                    UpLoadInfo.MsPrice,
                    MyConvert.ConToInt(UpLoadInfo.yfk),
                    UpLoadInfo.Tags,
                    UpLoadInfo.CuisesId,
                    UpLoadInfo.SfId,
                    UpLoadInfo.wwwyh,
                    UpLoadInfo.VisaId,
                    UpLoadInfo.CruisesPlanId
                ); 
            }
            else
            {
                UpLoadInfo.Id = CombId;
                Stings.Append("update OL_Line set ");
                Stings.Append("Sale='0',LineType='{1}',LineName='{2}',LineClass='{3}',LineDays='{4}',");
                Stings.Append("Standard='{5}',Topic='{6}',Price='{7}',AreaId=',{8}',");
                Stings.Append("LineFeature='{9}',PlanDate={10},Pics='{11}',EditTime='{12}',Pdates='{13}',SpFlag='{14}',MsPrice='{15}',yfk='{16}',Tags='{17}',CuisesId='{18}',PlanType='{19}',wwwyh='{20}',VisaId='{21}',Planid='{22}'");
                Stings.Append(" where id='{0}'");
                SqlQueryText = string.Format(Stings.ToString(),
                    UpLoadInfo.Id,
                    UpLoadInfo.LineType,
                    UpLoadInfo.LineName,
                    UpLoadInfo.LineClass,
                    UpLoadInfo.LineDays,
                    UpLoadInfo.Standard,
                    UpLoadInfo.Topic,
                    UpLoadInfo.Price,
                    UpLoadInfo.AreaId,
                    UpLoadInfo.LineFeature,
                    MyConvert.ConToDate(UpLoadInfo.PlanDate),
                    UpLoadInfo.Pics,
                    DateTime.Now.ToString(),
                    PlanDates,
                    UpLoadInfo.SpFlag,
                    UpLoadInfo.MsPrice,
                    MyConvert.ConToInt(UpLoadInfo.yfk),
                    UpLoadInfo.Tags,
                    UpLoadInfo.CuisesId,
                    UpLoadInfo.SfId,
                    UpLoadInfo.wwwyh,
                    UpLoadInfo.VisaId,
                    UpLoadInfo.CruisesPlanId
                ); 
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) != true)
            {
                return "上传失败！";
            }

            LineClass.CreateLineList(UpLoadInfo.LineType, UpLoadInfo.LineClass);

            if (UpLoadInfo.LineType == "FreeTour") HttpContext.Current.Cache.Insert("IndexFreeTour", "");
            if (UpLoadInfo.LineType == "Cruises") HttpContext.Current.Cache.Insert("IndexCruises", "");
            
            Stings.Clear();
            //行程Xml生成
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<Route>");
            Stings.Append(string.Format("<Feature>{0}</Feature>", UpLoadInfo.RouteServices.Feature));
            Stings.Append(string.Format("<Attentions>{0}</Attentions>", UpLoadInfo.RouteServices.Attentions));
            Stings.Append(string.Format("<PriceIn>{0}</PriceIn>", UpLoadInfo.RouteServices.PriceIn));
            Stings.Append(string.Format("<PriceOut>{0}</PriceOut>", UpLoadInfo.RouteServices.PriceOut));
            Stings.Append(string.Format("<OwnExpense>{0}</OwnExpense>", UpLoadInfo.RouteServices.OwnExpense));
            Stings.Append(string.Format("<Shopping>{0}</Shopping>", UpLoadInfo.RouteServices.Shopping));
            Stings.Append(string.Format("<TravelAgency>{0}</TravelAgency>", UpLoadInfo.RouteServices.TravelAgency));
            Stings.Append(string.Format("<Scenery>{0}</Scenery>", UpLoadInfo.RouteServices.Scenery));
            Stings.Append(string.Format("<Traffic>{0}</Traffic>", UpLoadInfo.RouteServices.Traffic));
            Stings.Append(string.Format("<Hotel>{0}</Hotel>", UpLoadInfo.RouteServices.Hotel));
            Stings.Append(string.Format("<Foods>{0}</Foods>", UpLoadInfo.RouteServices.Foods));
            Stings.Append(string.Format("<Guide>{0}</Guide>", UpLoadInfo.RouteServices.Guide));
            Stings.Append(string.Format("<Insure>{0}</Insure>", UpLoadInfo.RouteServices.Insure));
            Stings.Append(string.Format("<Others>{0}</Others>", UpLoadInfo.RouteServices.Others));
            Stings.Append("<RouteDetail>");

            if (UpLoadInfo.RouteDetail != null)
            {
                for (int i = 0; i < UpLoadInfo.RouteDetail.Length; i++)
                {
                    Stings.Append("<RouteInfos>");
                    Stings.Append(string.Format("<daterank>{0}</daterank>", UpLoadInfo.RouteDetail[i].daterank));
                    Stings.Append(string.Format("<rname>{0}</rname>", UpLoadInfo.RouteDetail[i].rname));
                    Stings.Append(string.Format("<route>{0}</route>", UpLoadInfo.RouteDetail[i].route));
                    Stings.Append(string.Format("<dinner>{0}</dinner>", UpLoadInfo.RouteDetail[i].dinner));
                    Stings.Append(string.Format("<bus>{0}</bus>", UpLoadInfo.RouteDetail[i].bus));
                    Stings.Append(string.Format("<room>{0}</room>", UpLoadInfo.RouteDetail[i].room));
                    Stings.Append(string.Format("<Pics>{0}</Pics>", UpLoadInfo.RouteDetail[i].Pics));
                    Stings.Append("</RouteInfos>");
                }
                Stings.Append("</RouteDetail>");
                Stings.Append("</Route>");
                Stings.Append("");
                SaveScriptToFile.SaveXml(Stings.ToString(), "Route", UpLoadInfo.MisId);
            }

            Stings.Clear();

            if (UpLoadInfo.PlanDetail != null)
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("delete from OL_Plan where lineid='{0}'", UpLoadInfo.MisId));
                for (int i = 0; i < UpLoadInfo.PlanDetail.Length; i++)
                {
                    Sql.Add(string.Format("insert into OL_Plan (lineid,planid,begindate,price) values ('{0}','{1}','{2}','{3}')",
                        UpLoadInfo.MisId,
                        UpLoadInfo.PlanDetail[i].PlanId,
                        UpLoadInfo.PlanDetail[i].BgnDate,
                        UpLoadInfo.PlanDetail[i].Price
                    ));
                }
                string[] SqlQuerys = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQuerys);
                //Stings.Append(string.Format("var defaultStartDate = '{0}';", UpLoadInfo.PlanDetail[0].BgnDate));
                //Stings.Append(string.Format("var defaultEndDate = '{0}';", UpLoadInfo.PlanDetail[UpLoadInfo.PlanDetail.Length - 1].BgnDate));
                //Stings.Append("var json = [");
                //string CreatIt = "0";
                //for (int i = 0; i < UpLoadInfo.PlanDetail.Length; i++)
                //{
                //    CreatIt = "0";
                //    if (i > 0)
                //    {
                //        if (UpLoadInfo.PlanDetail[i].BgnDate == UpLoadInfo.PlanDetail[i - 1].BgnDate)
                //        {
                //            CreatIt = "1";
                //        }
                //    }
                //    if (CreatIt == "0")
                //    {
                //        Stings.Append("{");
                //        Stings.Append(string.Format("'planid': '{0}',", UpLoadInfo.PlanDetail[i].PlanId));
                //        Stings.Append(string.Format("'date': '{0}',", UpLoadInfo.PlanDetail[i].BgnDate));
                //        Stings.Append(string.Format("'price': '{0}',", UpLoadInfo.PlanDetail[i].Price));
                //        Stings.Append(string.Format("'content': '{0}'", ""));
                //        Stings.Append("},");
                //    }
                //}
                //string infos = Stings.ToString();
                //infos = infos.Substring(0, infos.Length - 1);
                //infos += "];";
                //SaveScriptToFile.SaveScript(infos, "PlanDate", UpLoadInfo.MisId);
            }
            else
            {
                //SaveScriptToFile.SaveScript("var defaultStartDate = '2010-01-01';var defaultEndDate = '2010-01-01';", "PlanDate", UpLoadInfo.MisId);
            }
            return "Success";
        }

        [WebMethod]
        public string VisaInfoUpLoad(LineInfos UpLoadInfo, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "上传密码错误！";
            if (UpLoadInfo == null) return "没有任何数据，不能上传！";

            StringBuilder Stings = new StringBuilder();
            string SqlQueryText = string.Format("select top 1 Id from OL_Line where MisLineId='{0}'", UpLoadInfo.MisId);
            string CombId = MyDataBaseComm.getScalar(SqlQueryText);
            if (CombId == null)
            {

                UpLoadInfo.Id = Convert.ToString(CombineKeys.NewComb());
                Stings.Append("insert into OL_Line ");
                Stings.Append("(Id,MisLineId,LineType,LineName,LineClass,LineDays,Standard,Topic,Price,PlanType,LineFeature,DeptId,Pics,EditTime,Pdates,PlanDate,MsPrice,SpFlag,yfk)");
                Stings.Append(" values ");
                Stings.Append("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}','1')");
                SqlQueryText = string.Format(Stings.ToString(),
                    UpLoadInfo.Id,
                    UpLoadInfo.MisId,
                    UpLoadInfo.LineType,
                    UpLoadInfo.LineName,
                    UpLoadInfo.LineClass,
                    UpLoadInfo.LineDays,
                    UpLoadInfo.Standard,
                    UpLoadInfo.Topic,
                    UpLoadInfo.Price,
                    "4",
                    UpLoadInfo.LineFeature,
                    UpLoadInfo.DeptId,
                    UpLoadInfo.Pics,
                    DateTime.Now.ToString(),
                    UpLoadInfo.PlanDate,
                    "2049-12-31",
                    UpLoadInfo.MsPrice,
                    MyConvert.ConToInt(UpLoadInfo.yfk)
                );
            }
            else
            {
                UpLoadInfo.Id = CombId;
                Stings.Append("update OL_Line set ");
                Stings.Append("Sale='0',LineType='{1}',LineName='{2}',LineClass='{3}',LineDays='{4}',");
                Stings.Append("Standard='{5}',Topic='{6}',Price='{7}',PlanType='{8}',");
                Stings.Append("LineFeature='{9}',Pics='{10}',EditTime='{11}',Pdates='{12}',MsPrice='{13}',yfk='{14}',PlanDate='2049-12-31'");
                Stings.Append(" where id='{0}'");
                SqlQueryText = string.Format(Stings.ToString(),
                    UpLoadInfo.Id,
                    UpLoadInfo.LineType,
                    UpLoadInfo.LineName,
                    UpLoadInfo.LineClass,
                    UpLoadInfo.LineDays,
                    UpLoadInfo.Standard,
                    UpLoadInfo.Topic,
                    UpLoadInfo.Price,
                    "4",
                    UpLoadInfo.LineFeature,
                    UpLoadInfo.Pics,
                    DateTime.Now.ToString(),
                    UpLoadInfo.PlanDate,
                    UpLoadInfo.MsPrice,
                    MyConvert.ConToInt(UpLoadInfo.yfk)
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) != true)
            {
                return "上传失败！";
            }

            LineClass.CreateVisaList(UpLoadInfo.LineClass);

            Stings.Clear();
            //行程Xml生成
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<Route>");
            Stings.Append(string.Format("<Validity>{0}</Validity>", UpLoadInfo.VisaInfos.Validity));
            Stings.Append(string.Format("<Stay>{0}</Stay>", UpLoadInfo.VisaInfos.Stay));
            Stings.Append(string.Format("<WorkDay>{0}</WorkDay>", UpLoadInfo.VisaInfos.WorkDay));
            Stings.Append(string.Format("<Memo>{0}</Memo>", UpLoadInfo.VisaInfos.Memo));
            Stings.Append("<RouteDetail>");

            if (UpLoadInfo.VisaDocuments != null)
            {
                for (int i = 0; i < UpLoadInfo.VisaDocuments.Length; i++)
                {
                    Stings.Append("<RouteInfos>");
                    Stings.Append(string.Format("<Flag>{0}</Flag>", UpLoadInfo.VisaDocuments[i].Flag));
                    Stings.Append(string.Format("<FlagName>{0}</FlagName>", UpLoadInfo.VisaDocuments[i].FlagName));
                    Stings.Append(string.Format("<VisaType>{0}</VisaType>", UpLoadInfo.VisaDocuments[i].VisaType));
                    Stings.Append(string.Format("<VisaName>{0}</VisaName>", UpLoadInfo.VisaDocuments[i].VisaName));
                    Stings.Append(string.Format("<VisaContent>{0}</VisaContent>", UpLoadInfo.VisaDocuments[i].VisaContent));
                    Stings.Append(string.Format("<VisaFile>{0}</VisaFile>", UpLoadInfo.VisaDocuments[i].VisaFile));
                    Stings.Append(string.Format("<V1>{0}</V1>", UpLoadInfo.VisaDocuments[i].V1));
                    Stings.Append(string.Format("<V2>{0}</V2>", UpLoadInfo.VisaDocuments[i].V2));
                    Stings.Append("</RouteInfos>");
                }
                Stings.Append("</RouteDetail>");
                Stings.Append("</Route>");
                Stings.Append("");
                SaveScriptToFile.SaveXml(Stings.ToString(), "Route", UpLoadInfo.MisId);
            }
            return "Success";
        }

        [WebMethod]
        public string UnUpLoad(string MisLineId, string UpPassWord)
        {

            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return "上传密码错误！";
            string SqlQueryText;
            

            SqlQueryText = string.Format("update OL_Line set Sale='1',EditTime='{0}' where MisLineId='{1}'", DateTime.Now.ToString(),MisLineId);
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) != true)
            {
                return "取消上传失败！";
            }

            SqlQueryText = string.Format("select top 1 LineType,LineClass from OL_Line where MisLineId='{0}'", MisLineId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["LineType"].ToString() == "Visa")
                {
                    LineClass.CreateVisaList(DS.Tables[0].Rows[0]["LineClass"].ToString());
                }
                else                 
                {
                    LineClass.CreateLineList(DS.Tables[0].Rows[0]["LineType"].ToString(), DS.Tables[0].Rows[0]["LineClass"].ToString());
                }
                //LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                //LinePrice = DS.Tables[0].Rows[0]["Price"].ToString();
            }

            return "Success";
        }

        [WebMethod]
        public void AutoUpLoad(LineInfos UpLoadInfo, string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return;
            if (UpLoadInfo == null) return;
            if (UpLoadInfo.PlanDetail != null)
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("delete from OL_Plan where lineid='{0}'", UpLoadInfo.MisId));
                for (int i = 0; i < UpLoadInfo.PlanDetail.Length; i++)
                {
                    Sql.Add(string.Format("insert into OL_Plan (lineid,planid,begindate,price) values ('{0}','{1}','{2}','{3}')",
                        UpLoadInfo.MisId,
                        UpLoadInfo.PlanDetail[i].PlanId,
                        UpLoadInfo.PlanDetail[i].BgnDate,
                        UpLoadInfo.PlanDetail[i].Price
                    ));
                }
                string[] SqlQuerys = Sql.ToArray();
                MyDataBaseComm.Transaction(SqlQuerys);

                StringBuilder Stings = new StringBuilder();
                StringBuilder Plans = new StringBuilder();
                Stings.Append(string.Format("var defaultStartDate = '{0}';", UpLoadInfo.PlanDetail[0].BgnDate));
                Stings.Append(string.Format("var defaultEndDate = '{0}';", UpLoadInfo.PlanDetail[UpLoadInfo.PlanDetail.Length - 1].BgnDate));
                Stings.Append("var json = [");
                //string CreatIt = "0";
                for (int i = 0; i < UpLoadInfo.PlanDetail.Length; i++)
                {
                    //CreatIt = "0";
                    //if (i > 0)
                    //{
                    //    if (UpLoadInfo.PlanDetail[i].BgnDate == UpLoadInfo.PlanDetail[i - 1].BgnDate)
                    //    {
                    //        CreatIt = "1";
                    //    }
                    //}
                    //if (CreatIt == "0")
                    //{
                    //    Stings.Append("{");
                    //    Stings.Append(string.Format("'planid': '{0}',", UpLoadInfo.PlanDetail[i].PlanId));
                    //    Stings.Append(string.Format("'date': '{0}',", UpLoadInfo.PlanDetail[i].BgnDate));
                    //    Stings.Append(string.Format("'price': '{0}',", UpLoadInfo.PlanDetail[i].Price));
                    //    Stings.Append(string.Format("'content': '{0}'", ""));
                    //    Stings.Append("},");
                    //}

                    DateTime begindate = Convert.ToDateTime(UpLoadInfo.PlanDetail[i].BgnDate);
                    Plans.Append(string.Format("{0}/{1},", begindate.ToString("MM"), begindate.ToString("dd")));
                }
                //string infos = Stings.ToString();
                //infos = infos.Substring(0, infos.Length - 1);
                //infos += "];";
                //SaveScriptToFile.SaveScript(infos, "PlanDate", UpLoadInfo.MisId);

                string PlanDates = Plans.ToString();
                if (PlanDates.Length > 2) PlanDates = PlanDates.Substring(0, PlanDates.Length - 1);

                string SqlQueryText = string.Format("update OL_Line set Price='{1}', MsPrice='{2}',PlanDate={3},Pdates='{4}',yfk='{5}',wwwyh='{6}',EditTime='{7}' where MisLineId='{0}'", 
                    UpLoadInfo.MisId, 
                    UpLoadInfo.Price, 
                    UpLoadInfo.MsPrice, 
                    MyConvert.ConToDate(UpLoadInfo.PlanDate), 
                    PlanDates, 
                    MyConvert.ConToInt(UpLoadInfo.yfk),
                    UpLoadInfo.wwwyh,
                    DateTime.Now.ToString());
                MyDataBaseComm.ExcuteSql(SqlQueryText);
            }
            else
            {
                //SaveScriptToFile.SaveScript("var defaultStartDate = '2010-01-01';var defaultEndDate = '2010-01-01';", "PlanDate", UpLoadInfo.MisId);
            }
        }

        [WebMethod]
        public void AutoCreateListJs(string UpPassWord)
        {
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"])) return;
            //SELECT LineType, LineClass FROM OL_Line GROUP BY LineType, LineClass ORDER BY LineType
            string SqlQueryText = "SELECT LineType, LineClass FROM OL_Line where linetype<>'Visa' GROUP BY LineType, LineClass ORDER BY LineType";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    LineClass.CreateLineList(DS.Tables[0].Rows[i]["LineType"].ToString(), DS.Tables[0].Rows[i]["LineClass"].ToString());
                }
                HttpContext.Current.Cache.Insert("IndexFreeTour", "");
                HttpContext.Current.Cache.Insert("IndexCruises", "");
            }
        }

        [WebMethod]
        public CouponInfo CouponBranchUse(string CouponNo, string UpPassWord, string ProductType, string ProductClass, string LineID, string PlanDate, string amount, string price)
        {
            CouponInfo CouponInfos = new CouponInfo();
            CouponInfos.id = "0";
            CouponInfos.par = "0";
            CouponInfos.deduction = "0";
            CouponInfos.infos = "";
            string CanUse = "0";

            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]))
            {
                CouponInfos.infos = "配置不正确，不能使用";
            }
            else
            {

                if (CouponNo.Length < 9)
                {
                    CouponInfos.infos = "券号不正确，不能使用";
                }
                else
                {
                    string SqlQueryText = string.Format("select *,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where uno='{0}' and flag='0' and pbdate<='{1}' and pedate>='{1}' and begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}'", CouponNo, PlanDate, DateTime.Today);
                    //if (useflag == "1") SqlQueryText += " and deduction='1'";
                    //if (useflag == "2") SqlQueryText += " and deduction='2'";
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string CouponAmount = DS.Tables[0].Rows[0]["amount"].ToString();
                        string product = DS.Tables[0].Rows[0]["product"].ToString();
                        if (DS.Tables[0].Rows[0]["deduction"].ToString() == "1")
                        {
                            if (MyConvert.ConToInt(CouponAmount) > 0)
                            {
                                if (MyConvert.ConToInt(CouponAmount) > MyConvert.ConToInt(amount))
                                {
                                    CouponInfos.infos = "您选择的优惠券不满足整单需消费" + CouponAmount + "元的使用要求";//+ " / " + amount + " / " + price
                                    CanUse = "1";
                                }
                            }
                        }
                        else
                        {
                            if (MyConvert.ConToInt(CouponAmount) > 0)
                            {
                                if (MyConvert.ConToInt(CouponAmount) > MyConvert.ConToInt(price))
                                {
                                    CouponInfos.infos = "您选择的优惠券不满足每人需消费" + CouponAmount + "元的使用要求";
                                    CanUse = "1";
                                }
                            }
                        }

                        switch (DS.Tables[0].Rows[0]["range"].ToString())
                        {
                            case "1":

                                break;
                            case "2":
                                if (ProductType != "OutBound")
                                {
                                    CouponInfos.infos = "您选择的优惠券只能出境旅游产品使用";
                                    CanUse = "1";
                                }
                                break;
                            case "3":
                                if (ProductType != "InLand")
                                {
                                    CouponInfos.infos = "您选择的优惠券只能国内旅游产品使用";
                                    CanUse = "1";
                                }
                                break;
                            case "4":
                                if (ProductType != "Visa")
                                {
                                    CouponInfos.infos = "您选择的优惠券只能单项服务旅游产品使用";
                                    CanUse = "1";
                                }
                                break;
                            case "5":
                                if (ProductType != "Cruises")
                                {
                                    CouponInfos.infos = "您选择的优惠券只能邮轮旅游产品使用";
                                    CanUse = "1";
                                }
                                break;
                            case "8":
                                if (product.IndexOf("," + ProductClass + ",") < 0)
                                {
                                    CouponInfos.infos = "您选择的优惠券不满足可使用范围";
                                    CanUse = "1";
                                }
                                break;
                            case "9":
                                if (product.IndexOf("," + LineID + ",") < 0)
                                {
                                    CouponInfos.infos = "您选择的优惠券不能在当前旅游线路使用";
                                    CanUse = "1";
                                }
                                break;
                            default:
                                CouponInfos.infos = "您选择的优惠券不能在当前旅游线路使用";
                                CanUse = "1";
                                break;
                        }

                        if (CanUse == "0")
                        {
                            //string.Format("update Pre_Ticket set flag='1',usedate='{2}',AutoId='{4}',OrderId='{5}' where pid='{0}' and userid='{1}' and flag='0' and id in ({3})", CouponId, Convert.ToString(Session["Online_UserId"]), DateTime.Now.ToString(), CouponTicketId, AutoId, OrderId));
                            SqlQueryText = string.Format("update Pre_Ticket set flag='1',usedate='{1}',AutoId='-1' where id='{0}'",
                                DS.Tables[0].Rows[0]["id"].ToString(),
                                DateTime.Now.ToString());

                            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                            {
                                CouponInfos.id = DS.Tables[0].Rows[0]["id"].ToString();
                                CouponInfos.par = DS.Tables[0].Rows[0]["par"].ToString();
                                CouponInfos.deduction = DS.Tables[0].Rows[0]["deduction"].ToString();
                                CouponInfos.infos = DS.Tables[0].Rows[0]["memo"].ToString();
                            }
                            else
                            {
                                CouponInfos.infos = "优惠券使用失败，请稍后再试";
                            }
                        
                        }
                    }
                    else
                    {
                        CouponInfos.infos = "您选择的优惠券不存在，或已使用，或已过期，或不符合使用条件";
                    }
                }
            }
            return CouponInfos;
        }


        [WebMethod]
        public string CouponBranchSerch(string CouponNo, string UpPassWord)
        {
            string infos = "";
            if (UpPassWord != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]))
            {
                infos = "配置不正确，不能查询";
            }
            else
            {

                if (CouponNo.Length < 9)
                {
                    infos = "券号不正确，不能查询";
                }
                else
                {
                    string SqlQueryText = string.Format("select *,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where uno='{0}'", CouponNo);
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        infos += DS.Tables[0].Rows[0]["UserName"].ToString();
                        if (DS.Tables[0].Rows[0]["flag"].ToString() == "0")
                        {
                            infos += " 可使用 ";
                        }
                        else
                        {
                            infos += " 已使用 ";
                        }
                        
                        if (DS.Tables[0].Rows[0]["deduction"].ToString() == "1")
                        {
                            infos += " 整单消费满" + DS.Tables[0].Rows[0]["amount"].ToString() + "元 ";
                        }
                        else
                        {
                            infos += " 人均消费满" + DS.Tables[0].Rows[0]["amount"].ToString() + "元 ";
                            infos += "每人";
                        }
                        infos += "抵用：" + DS.Tables[0].Rows[0]["par"].ToString();
                        infos += "<br>使用：" + string.Format("{0:yy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]) + " 至" + string.Format("{0:yy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                        infos += " / 出发：" + string.Format("{0:yy-MM-dd}", DS.Tables[0].Rows[0]["pbdate"]) + " 至" + string.Format("{0:yy-MM-dd}", DS.Tables[0].Rows[0]["pedate"]);
                        infos += "<br>使用说明：" + DS.Tables[0].Rows[0]["memo"].ToString();
                        infos += "";

                        
                    }
                    else
                    {
                        infos = "您选择的优惠券不存在";
                    }
                }
            }
            return infos;
        }


    }
}
