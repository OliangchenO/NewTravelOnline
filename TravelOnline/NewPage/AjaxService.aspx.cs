using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;
using System.Net;
using System.IO;
using TravelOnline.WeChat.freetrip.interfaces;
using TravelOnline.Destination.Class;
using LitJson;
using TravelOnline.RequestHandlerFacade;
using TravelOnline.NewPage.pay;
using TravelOnline.NewPage.Class;

namespace TravelOnline.NewPage
{
    public partial class AjaxService : System.Web.UI.Page
    {
        private readonly static int TIMEOUT = 15000;
        private CookieContainer _cookieCon = new CookieContainer();
        private CredentialCache _credentials = new CredentialCache();

        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            switch (Request.QueryString["action"])
            {
                case "PuFaPay":
                    Response.Write(PuFaPay());
                    break;
                case "BOCMPay":
                    Response.Write(BOCMPay());
                    break;
                case "SendPuFaSMS":
                    Response.Write(SendPuFaSMS());
                    break;
                case "SendBOCMSMS":
                    Response.Write(SendBOCMSMS());
                    break;
                case "OrderSubmit":
                    Response.Write(OrderSubmit());
                    break;
                case "SaveOrderInfo":
                    Response.Write(SaveOrderInfo());
                    break;
                case "LoadPrice":
                    Response.Write(LoadPrice());
                    break;
                case "LoadRoomInfo":
                    Response.Write(LoadRoomInfo());
                    break;
                case "LoadRoomPic":
                    Response.Write(LoadRoomPic());
                    break;
                case "checkCoupon":
                    Response.Write(checkCoupon());
                    break;
                case "checkIntegral":
                    Response.Write(checkIntegral());
                    break;
                case "lotteryCoupon":
                    Response.Write(lotteryCoupon());
                    break;
                case "lotteryCoupon2017":
                    Response.Write(lotteryCoupon2017());
                    break;
                case "showScenic":
                    Response.Write(showScenic());
                    break;
                default:
                    Response.Write("({\"error\":1})");
                    Response.End();
                    break;
            }

        }

        /// <summary>
        /// 通过url请求数据
        /// </summary>
        /// <param >被请求页面的url</param>
        /// <param >代理服务器</param>

        /// <returns>返回页面内容</returns>
        public string GetPageContent(string url, string proxyServer)
        {
            StringBuilder ret = new StringBuilder("");
            HttpWebResponse rsp = null;

            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                if (!string.IsNullOrEmpty(proxyServer))
                {


                    req.Proxy = new WebProxy(proxyServer);
                }
                req.CookieContainer = this._cookieCon;
                req.Headers.Add("Accept-Language: zh-cn");
                req.AllowAutoRedirect = true;
                req.Timeout = TIMEOUT;

                if (this._credentials != null)
                {
                    req.PreAuthenticate = true;
                    req.Credentials = this._credentials;
                }
                rsp = (HttpWebResponse)req.GetResponse();

                Stream rspStream = rsp.GetResponseStream();
                StreamReader sr = new StreamReader(rspStream, System.Text.Encoding.Default);

                //获取数据
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    ret.Append(read, 0, count);
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {
                ret.Append(e.Message);
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return ret.ToString();
        }

        protected string PuFaPay()
        {
            string orderID = Request.Form["pf_orderid"];
            string OrderAmt = Request.Form["OrderAmt"];
            string CardID = Request.Form["xykh"];
            string CVV2 = Request.Form["cvv2"];
            string checkcode = Request.Form["pfcode"];
            string AutoID = "";//web页面订单号

            //点击支付按钮时加判断订单状态by毛寅珅
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", orderID);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                AutoID = DS.Tables[0].Rows[0]["AutoID"].ToString();
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "2")
                {
                    return "({\"error\":\"订单状态异常！请确认订单状态！\"})";
                }
            }

            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='PuFa' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(CardID), MyDataBaseComm.StripSQLInjection(checkcode));

            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    return "({\"error\":\"动态密码已过有效期，请重新发送\"})";
                }
            }
            else
            {
                return "({\"error\":\"信用卡号或动态密码错误\"})";
            }

            string verifyCode = "";
            string url = Convert.ToString(ConfigurationManager.AppSettings["PostUrl"]) + "/default.aspx?FunctionName=BFpayOrder";
            url += "&CardID=" + Request.Form["xykh"].Trim();
            url += "&OrderNum=" + AutoID;
            url += "&OrderAmt=" + OrderAmt;
            url += "&Cvv2=" + CVV2;
            url += "&stagesNum=" + "";
            try
            {
                verifyCode = GetPageContent(url, "");
                //操作超时
                if (verifyCode == "操作超时") verifyCode = "error";
            }
            catch
            {
                verifyCode = "error";
            }

            if (verifyCode == "error")
            {
                return "({\"error\":\"支付失败，请稍后再试\"})";
            }
            else
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", orderID));

                Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','spdb')",
                    orderID,
                    verifyCode,
                    "",
                    OrderAmt,
                    DateTime.Now.ToString(),
                    "浦发银行信用卡在线支付"
                    )
                );
                //显示订单号by毛寅珅
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','浦发银行信用卡在线支付成功，流水号：{3}，订单号：{4}，付款金额：{2}')",
                    orderID,
                    DateTime.Now.ToString(),
                    OrderAmt,
                    verifyCode,
                    AutoID
                    )
                );
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                    TravelOnlineService rsp = new TravelOnlineService();
                    rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                    PayInfo Pays = new PayInfo();
                    Pays.OrderId = orderID;
                    Pays.TradeNo = CardID;
                    Pays.PayPrice = OrderAmt;
                    Pays.PayTime = DateTime.Now.ToString();
                    Pays.PayContent = "浦银行信用卡在线支付";

                    string Result;
                    try
                    {
                        Result = rsp.PayInfoSave(UpPassWord, Pays);
                    }
                    catch
                    {
                    }
                }
                return "({\"success\":\"" + orderID + "\"})";

            }
        }

        protected string BOCMPay()
        {
            string orderID = Request.Form["pf_orderid"];
            string OrderAmt = Request.Form["OrderAmt"];
            string amt = Request.QueryString["amt"];
            Decimal amtDecimal = Decimal.Parse(amt);
            string checkcode = Request.Form["jtcode"];
            string AutoID = "";//web页面订单号

            string yfk = Request.Form["P_Yfk"];

            string autoID = Request.Form["autoID"];
            string cardNo = Request.Form["cardNo"];
            string expireDate = Request.Form["expireDate"];
            string customerName = Request.Form["customerName"];

            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", orderID);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                AutoID = DS.Tables[0].Rows[0]["AutoID"].ToString();
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "2")
                {
                    return "({\"error\":\"订单状态异常！请确认订单状态！\"})";
                }
            }

            RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            string amount = (Decimal.Round(amtDecimal) * 100).ToString().TrimEnd('.');
            Rq.amount = amount;
            Rq.orderId = (MyConvert.GetTimeStamp() + AutoID).PadLeft(20, '0');
            Rq.channel = "BOCM";
            Rq.gateway = "Purchase";
            BocmParam param = new BocmParam();
            param.cardNo = cardNo;
            param.expireDate = expireDate;
            param.customerName = customerName;
            string no = string.Format("{0:HHmmss}", DateTime.Now);
            param.batchNo = no;
            param.voucherNo = no;
            param.traceNo = autoID;
            BocmParam oldParam = JsonMapper.ToObject<BocmParam>(Session["SendBOCMSMS" + autoID].ToString());
            param.oldBatchNo = oldParam.batchNo;
            param.oldVoucherNo = oldParam.voucherNo;
            param.codeSeq = Session["codeSeq" + autoID].ToString();
            param.code = checkcode;
            string bocmPay = Regex.Unescape(JsonMapper.ToJson(param));
            Rq.productInfo = bocmPay;
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
            BocmResult result = JsonMapper.ToObject<BocmResult>(Rs.requestUrl);

            if (result.returnCode != "00")
            {
                return "({\"error\":\"" + result.returnMessage + "\"})";
            }
            else
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", orderID));

                Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','bocm')",
                    orderID,
                    result.authNo,
                    "",
                    amt,
                    DateTime.Now.ToString(),
                    "交通银行信用卡在线支付"
                    )
                );

                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','交通银行信用卡在线支付成功，授权号：{3}，订单号：{4}，付款金额：{2}')",
                    orderID,
                    DateTime.Now.ToString(),
                    amt,
                    result.authNo,
                    AutoID
                    )
                );
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                    TravelOnlineService rsp = new TravelOnlineService();
                    rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                    PayInfo Pays = new PayInfo();
                    Pays.OrderId = orderID;
                    Pays.TradeNo = cardNo;
                    Pays.PayPrice = amt;
                    Pays.PayTime = DateTime.Now.ToString();
                    Pays.PayContent = "交通银行信用卡在线支付";

                    string Result;
                    try
                    {
                        Result = rsp.PayInfoSave(UpPassWord, Pays);
                    }
                    catch
                    {
                    }
                }
                return "({\"success\":\"" + orderID + "\"})";

            }
        }

        protected string SendPuFaSMS()
        {
            string verifyCode = "";
            string url = Convert.ToString(ConfigurationManager.AppSettings["PostUrl"]) + "/default.aspx?FunctionName=BFgetDynamicCode&CardID=" + Request.Form["xykh"].Trim();
            try
            {
                verifyCode = GetPageContent(url, "");
                if (verifyCode.Length != 6) verifyCode = "error";
                if (verifyCode == "操作超时") verifyCode = "error";
            }
            catch
            {
                verifyCode = "error";
            }

            if (verifyCode == "error")
            {
                return "({\"error\":\"动态密码发送失败，请稍后再试\"})";
            }
            else
            {
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    System.Guid.NewGuid(),
                    Request.Form["xykh"].Trim(),
                    "浦发银行动态验证码",
                    "0",
                    "PuFa",
                    verifyCode,
                    DateTime.Now.ToString()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    return "({\"success\":\"发送成功，如未收到，请60秒后重发\"})";
                }
                else
                {
                    return "({\"error\":\"动态密码发送失败，请稍后再试\"})";
                }
            }
        }

        private string SendBOCMSMS()
        {
            string OrderAmt = Request.Form["OrderAmt"];
            string yfk = Request.Form["P_Yfk"];
            string amt = Request.QueryString["amt"];
            string autoID = Request.Form["autoID"];
            string cardNo = Request.Form["cardNo"];
            string expireDate = Request.Form["expireDate"];
            string customerName = Request.Form["customerName"];
            RequestWithoutCardInfoRq Rq = new RequestWithoutCardInfoRq();
            string amount = (Convert.ToInt32(Request.QueryString["amt"]) * 100).ToString();
            Rq.amount = amount;
            Rq.channel = "BOCM";//分期：CCBINS,非分期：CCB
            Rq.gateway = "GetSmsCode";
            BocmParam param = new BocmParam();
            param.cardNo = cardNo;
            param.expireDate = expireDate;
            param.customerName = customerName;
            string no = string.Format("{0:HHmmss}", DateTime.Now);
            param.batchNo = no;
            param.voucherNo = no;
            param.traceNo = autoID;
            string bocmSms = Regex.Unescape(JsonMapper.ToJson(param));
            Session.Add("SendBOCMSMS" + autoID, bocmSms);
            Rq.productInfo = bocmSms;
            RequestHandlerFacadeImplService ToService = new RequestHandlerFacadeImplService();
            BocmResult result = new BocmResult();
            try
            {
                RequestWithoutCardInfoRs Rs = ToService.requestWithoutCardInfo(Rq);
                result = JsonMapper.ToObject<BocmResult>(Rs.requestUrl);
            }
            catch (Exception e)
            {
                return "({\"error\":\"" + e.Message + "\"})";
            }


            if (result.returnCode == "00")
            {
                Session.Add("codeSeq" + autoID, result.codeSeq);
                return "({\"success\":\"发送成功，如未收到，请60秒后重发\"})";
            }
            else
            {
                return "({\"error\":\"" + result.returnMessage + "\"})";
            }
        }

        protected string OrderSubmit()
        {
            try
            {

                string OrderId = Request.Form["OrderId"];
                int ratio = Convert.ToInt32(ConfigurationManager.AppSettings["Integral_ratio"]);
                string couponAmount = string.IsNullOrEmpty(Request.Form["couponAmount"]) ? "0" : Request.Form["couponAmount"];
                string IntegralAmount = string.IsNullOrEmpty(Request.Form["IntegralAmount"]) ? "0" : Request.Form["IntegralAmount"];
                int AutoId;
                //return "({\"success\":\"" + OrderId + "\"})";

                //string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", OrderId);
                string SqlQueryText = "select *,(select PriceId from OL_OrderPrice where OrderId=OL_TempOrder.OrderId and PriceType='Coupon') as CouponId,";
                SqlQueryText += "(select OrderNums from OL_OrderPrice where OrderId=OL_TempOrder.OrderId and PriceType='Coupon') as CouponNums,";
                SqlQueryText += "(select misid from OL_LoginUser where Id=OL_TempOrder.OrderUser) as auser,";
                SqlQueryText += "(select misid from DeptInfo where id=OL_TempOrder.orderdept) as adept,";
                SqlQueryText += "(select misid from Company where id=OL_TempOrder.ordercompany) as acom";
                SqlQueryText += string.Format(" from OL_TempOrder where OrderId='{0}'", OrderId);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "7")
                    {
                        return "({\"error\":\"订单提交失败，请稍后再试！\"})";
                    }
                    AutoId = MyConvert.ConToInt(DS.Tables[0].Rows[0]["AutoId"].ToString());
                    OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();

                    OrderInfos Sorder = new OrderInfos();
                    Sorder.adult = DS.Tables[0].Rows[0]["Adults"].ToString();
                    Sorder.begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]);
                    Sorder.childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                    Sorder.days = DS.Tables[0].Rows[0]["LineDays"].ToString();
                    Sorder.deptid = DS.Tables[0].Rows[0]["DeptId"].ToString();

                    decimal OrderGathering = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString());
                    Sorder.gathering = OrderGathering.ToString();
                    Sorder.infoid = DS.Tables[0].Rows[0]["ProductClass"].ToString();

                    Sorder.lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                    Sorder.linename = DS.Tables[0].Rows[0]["LineName"].ToString();
                    Sorder.orderdate = DateTime.Now.ToString(); //DS.Tables[0].Rows[0]["OrderTime"].ToString();
                    Sorder.orderflag = DS.Tables[0].Rows[0]["OrderFlag"].ToString();
                    Sorder.orderid = DS.Tables[0].Rows[0]["OrderId"].ToString();

                    Sorder.ordername = Request.Form["ordername"].Trim();
                    Sorder.mobile = Request.Form["ordermobile"].Trim();
                    Sorder.email = Request.Form["orderemail"].Trim();
                    Sorder.ordermemo = Request.Form["ordermemo"].Trim();

                    Sorder.ordernumber = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                    Sorder.planid = DS.Tables[0].Rows[0]["PlanId"].ToString();
                    Sorder.tel = DS.Tables[0].Rows[0]["OrderTel"].ToString();
                    Sorder.orderno = DS.Tables[0].Rows[0]["AutoId"].ToString();
                    Sorder.contract = DS.Tables[0].Rows[0]["Contract"].ToString();
                    Sorder.invoice = DS.Tables[0].Rows[0]["Invoice"].ToString();
                    Sorder.SellDept = DS.Tables[0].Rows[0]["BranchId"].ToString();

                    Sorder.CruisesFlag = "0";
                    Sorder.ccid = DS.Tables[0].Rows[0]["ccid"].ToString();
                    Sorder.acom = DS.Tables[0].Rows[0]["acom"].ToString();
                    Sorder.adept = DS.Tables[0].Rows[0]["adept"].ToString();
                    Sorder.SellUser = DS.Tables[0].Rows[0]["auser"].ToString();
                    Sorder.ordertypes = DS.Tables[0].Rows[0]["ProductType"].ToString();

                    //公司id为1，表示内部部门预定
                    if (DS.Tables[0].Rows[0]["ordercompany"].ToString() == "1")
                    {
                        Sorder.SellDept = DS.Tables[0].Rows[0]["adept"].ToString();
                        Sorder.acom = "0";
                        Sorder.adept = "0";
                    }

                    List<string> Sql = new List<string>();

                    //return "({\"error\":\"1\"})";

                    if (Convert.ToString(Session["Online_UserId"]).Length > 0)
                    {
                        Sql.Add(string.Format("update OL_TempOrder set UserName='{1}',OrderUser='{2}' where OrderId='{0}'", OrderId, Convert.ToString(Session["Online_UserName"]), Convert.ToString(Session["Online_UserId"])));
                    }

                    SaveErrorToLog("步骤开始：订单号 " + AutoId);
                    SaveErrorToLog("步骤1：更新订单预订人ok");

                    string OrderName = Request.Form["ordername"].Trim();
                    string OrderEmail = Request.Form["orderemail"].Trim();
                    string OrderMobile = Request.Form["ordermobile"].Trim();
                    string OrderMemo = Request.Form["ordermemo"].Trim();

                    Sql.Add(string.Format("UPDATE OL_TempOrder set OrderFlag='7',OrderName='{1}',OrderMobile='{2}',OrderEmail='{3}',OrderMemo='{4}' where OrderId='{0}'",
                        OrderId,
                        OrderName,
                        OrderMobile,
                        OrderEmail,
                        OrderMemo
                    ));
                    SaveErrorToLog("步骤2：更新订单联系人及邮件电话ok");

                    if (Request.Form["fptf"] == "是")
                    {
                        Sql.Add(string.Format("insert into OL_Invoice (OrderId,InvoiceName,InvoiceContent,GuestName,GuestMobile,GuestAddress,InvoiceFlag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            OrderId,
                            Request.Form["InvoiceName"],
                            Request.Form["choose"],
                            Request.Form["In_GuestName"],
                            Request.Form["In_GuestMobile"],
                            Request.Form["In_GuestAddress"],
                            Request.Form["express"]
                        ));
                        SaveErrorToLog("步骤3：发票ok");
                    }

                    string TableName = "OL_GuestInfo";
                    string[] Fil = new string[9];
                    string[] Val = new string[9];

                    string[] GuestName = Request.Form["guestname"].Split(',');
                    //string[] Sex = Request.Form["vsex1"].Split(',');
                    string[] Mobile = Request.Form["guestmobile"].Split(',');
                    string[] IdType = Request.Form["cer1"].Split(',');
                    string[] IdNumber = Request.Form["guestzjhm"].Split(',');
                    //return "({\"error\":\"" + GuestName.Length + "\"})";
                    for (int i = 0; i < GuestName.Length; i++)
                    {
                        Fil[0] = "OrderId"; Val[0] = OrderId;
                        Fil[1] = "GuestName"; Val[1] = GuestName[i];
                        Fil[2] = "Sex"; Val[2] = Request.Form["vsex" + i];
                        Fil[3] = "Mobile"; Val[3] = Mobile[i];
                        Fil[4] = "IdType"; Val[4] = IdType[i];
                        Fil[5] = "IdNumber"; Val[5] = IdNumber[i];
                        Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                    }

                    SaveErrorToLog("步骤4：游客ok");
                    //if (OrderFlag == "9")
                    //{

                    //    Sql.Add(string.Format("update OL_TempOrder set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), "0", "0"));
                    //    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您暂存了预订单"));
                    //    SaveErrorToLog("步骤6：暂存订单ok");
                    //}
                    //else
                    //{
                    //    Sql.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", OrderId));
                    //    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您提交了预订单"));
                    //    Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", OrderId));
                    //    Sql.Add(string.Format("update OL_Order set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4}-{5} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), "0", "0", "0"));
                    //    SaveErrorToLog("步骤6：提交订单ok");
                    //}
                    string[] SqlQuery = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuery) == true)
                    {
                        List<string> Sqlnew = new List<string>();
                        string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                        TravelOnlineService rsp = new TravelOnlineService();
                        rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                        string OrderFlag = "0";//预订状态，不占位订单和无位置订单为0，畅游占位成功为1，提交错误返回9
                        try
                        {
                            OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
                            //OrderFlag = "1";
                        }
                        catch
                        {
                            OrderFlag = "9";
                        }
                        SaveErrorToLog("步骤5 畅游提交：订单号=" + AutoId + " OrderFlag=" + OrderFlag);

                        if (OrderFlag == "9")
                        {
                            Sqlnew.Add(string.Format("update OL_TempOrder set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), "0", "0"));
                            Sqlnew.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您暂存了预订单"));
                            SaveErrorToLog("步骤6：暂存订单ok");
                        }
                        else
                        {
                            Sqlnew.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota FROM OL_TempOrder WHERE OrderId='{0}'", OrderId));

                            Sqlnew.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您在官网提交了预订单"));
                            Sqlnew.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", OrderId));
                            Sqlnew.Add(string.Format("update OL_Order set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4}-{5} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), "0", "0", "0"));
                            SaveErrorToLog("步骤6：提交订单ok");

                            if (!couponAmount.Equals("0"))
                            {
                                Sqlnew.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您使用了" + couponAmount + "元优惠券"));
                                Sqlnew.Add(string.Format("update OL_Order set couponAmount=isnull(couponAmount,0)+{0} where OrderId='{1}'", couponAmount, OrderId));

                                string couponNum = Request.Form["couponNum"];
                                if (Convert.ToString(ConfigurationManager.AppSettings["couponNum"]).IndexOf(couponNum) > -1)
                                {
                                    SaveErrorToLog("订单：" + OrderId + "使用了" + couponAmount + "元优惠券");
                                }
                                else
                                {
                                    Sqlnew.Add(string.Format("update Pre_Ticket set flag='1',usedate='{2}',AutoId='{3}',OrderId='{4}' where uno='{0}' and userid='{1}' and flag='0'", couponNum, Convert.ToString(Session["Online_UserId"]), DateTime.Now.ToString(), AutoId, OrderId));
                                    SaveErrorToLog("订单：" + OrderId + "使用了" + couponAmount + "元优惠券");
                                }
                            }
                            if (!IntegralAmount.Equals("0"))
                            {
                                Sqlnew.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您使用了" + Convert.ToInt32(IntegralAmount) * ratio + "点积分"));
                                Sqlnew.Add(string.Format("update OL_Order set couponAmount=isnull(couponAmount,0)+{0} where OrderId='{1}'", IntegralAmount, OrderId));

                                Sqlnew.Add(string.Format("INSERT INTO [dbo].[OL_Integral] ([uid],[orderid],[lineid],[integral],[getdate],[flag],[dept],[enddate]) " +
                                                                            "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                                    , Convert.ToString(Session["Online_UserId"])
                                    , OrderId
                                    , AutoId
                                    , Convert.ToInt32(IntegralAmount) * -ratio
                                    , DateTime.Now.ToString()
                                    , '1'
                                    , '0'
                                    , DateTime.Now.AddYears(2).ToString()));
                                SaveErrorToLog("订单：" + OrderId + "使用了" + IntegralAmount + "点积分");
                            }
                            try
                            {
                                CommentInfoService.insertOrderComment(AutoId.ToString());
                            }
                            catch (Exception ex)
                            {
                                SaveErrorToLog("订单：" + AutoId + "初始化点评失败！失败原因：" + ex.Message);
                            }
                        }

                        //string[] SqlQuery1 = Sqlnew.ToArray();
                        if (MyDataBaseComm.Transaction(Sqlnew.ToArray()) == true)
                        {
                            if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1)
                            {
                                //清空呼叫中心订单id
                                HttpCookie cookie = default(HttpCookie);
                                cookie = new HttpCookie("CallCenterOrderId", "");
                                cookie.Expires = DateTime.Now.AddDays(-10);
                                Response.Cookies.Add(cookie);
                            }
                            return "({\"success\":\"" + OrderId + "\"})";
                        }
                        else
                        {
                            SaveErrorToLog("步骤7：订单提交失败，临时订单转正式订单错误");
                            return "({\"error\":\"订单提交失败，请稍后再试！\"})";
                        }
                    }
                    else
                    {
                        SaveErrorToLog("步骤7：订单提交失败，临时订单提交错误");
                        return "({\"error\":\"订单提交失败，请稍后再试！\"})";
                    }

                }
                else
                {
                    return "({\"error\":\"您的订单已经预订成功，不能重复提交！\"})";
                }

            }
            catch (Exception exception)
            {
                string message = exception.Message;
                SaveErrorToLog("步骤7：订单提交失败 " + message);
                return "({\"error\":\"订单提交失败，请稍后再试！\"})";
            }


        }

        protected string SaveOrderInfo()
        {
            //return "({\"error\":\"订单提交失败，" + Request.Form["p1"] + "\"})";//Request.Form["p1"].Length
            //青旅呼叫中心订单
            string CcId = "0";
            if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1)
            {
                if (Request.Cookies["CallCenterOrderId"] != null)
                {
                    string CookieCcid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["CallCenterOrderId"].Value));
                    CcId = MyConvert.ConToInt(CookieCcid).ToString();
                }
            }

            string TableName = "OL_TempOrder";
            string Id = "0";
            string[] Fil = new string[23];
            string[] Val = new string[23];

            string lineid = Request.Form["TB_LineId"].Trim();
            string planid = Request.Form["planid"].Trim();
            string linename = Request.Form["TB_LineName"].Trim();
            string begindate = Request.Form["begindate"].Trim();
            string adults = Request.QueryString["adults"];
            string childs = Request.QueryString["childs"];
            string price = Request.QueryString["AllPrice"];
            string sale = Request.Form["TB_Sale"];

            string ProductFlag = Request.Form["TB_ProductFlag"];

            string ordernums = (MyConvert.ConToInt(adults) + MyConvert.ConToInt(childs)).ToString();

            if (sale == "1")
            {
                return "({\"error\":\"产品非销售状态，无法预订\"})";
            }

            if (Convert.ToString(ConfigurationManager.AppSettings["seckill"]).IndexOf("," + lineid + ",") > -1)
            {
                string SqlQueryText1 = string.Format("select * from OL_SecKillLine where MisLineId='{0}'", lineid);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    DateTime beginDate = MyConvert.ConToDateTime(DS1.Tables[0].Rows[0]["beginDate"].ToString());
                    DateTime endDate = MyConvert.ConToDateTime(DS1.Tables[0].Rows[0]["endDate"].ToString());
                    DateTime now = DateTime.Now;
                    if (now.CompareTo(beginDate) >= 0 && now.CompareTo(endDate) <= 0)
                    {
                    }
                    else
                    {
                        return "({\"error\":\"产品未到售卖时间，无法预订\"})";
                    }
                }
            }

            //浦发人数限制
            if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf("," + planid + ",") > -1)
            {
                if (MyConvert.ConToInt(ordernums) > 2)
                {
                    return "({\"error\":\"浦发信用卡立减活动，最多只能报名2人！\"})";
                }
            }

            if (Convert.ToString(ConfigurationManager.AppSettings["pdyhQianZheng"]).IndexOf("," + lineid + ",") > -1)
            {

                if (MyConvert.ConToInt(ordernums) > 1)
                {
                    return "({\"error\":\"浦发信用卡签证活动，最多只能报名1人！\"})";
                }
            }
            //浦发人数限制

            //上海银行人数限制
            if (isboshActivity(lineid))
            {
                if (MyConvert.ConToInt(ordernums) > 1)
                {
                    return "({\"error\":\"上海银行活动，最多只能报名1人！\"})";
                }
            }
            //上海银行人数限制

            string ordername = "临时用户";
            string orderemail = "";
            string ordermobile = "";
            string userid = "cc68b789-0d20-4ab5-a07b-9f4c002b7b96";

            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                ordername = Convert.ToString(Session["Online_UserName"]);
                orderemail = Convert.ToString(Session["Online_UserEmail"]);
                ordermobile = Convert.ToString(Session["Online_UserMobile"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string ProductType = "0", ProductClass = "0", LineDays = "0";
            string SqlQueryText = string.Format("select * from OL_Line where MisLineId='{0}'", Request.Form["TB_LineId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ProductType = DS.Tables[0].Rows[0]["LineType"].ToString();
                ProductClass = DS.Tables[0].Rows[0]["LineClass"].ToString();
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
            }
            else
            {
                return "({\"error\":\"订单提交失败，旅游线路不存在，请稍后再试\"})";
            }

            PlanSeats GetPlan = new PlanSeats();
            if (planid == "0")
            {
                GetPlan.Seats = "99";
                GetPlan.Route = "99";
                GetPlan.StopDate = "";
            }
            else
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, begindate);
                }
                catch
                {
                    GetPlan.Seats = "0";
                    GetPlan.Route = "99";
                    GetPlan.StopDate = "";
                }
            }
            string Seats = GetPlan.Seats;

            if (MyConvert.ConToInt(ordernums) > MyConvert.ConToInt(Seats))
            {
                return "({\"error\":\"剩余人数不足，请选择其他出发日期\"})";
            }

            string orderid = CombineKeys.NewComb().ToString();
            Fil[0] = "OrderId"; Val[0] = orderid;
            Fil[1] = "ProductType"; Val[1] = ProductType;
            Fil[2] = "ProductClass"; Val[2] = ProductClass;
            Fil[3] = "LineID"; Val[3] = lineid;
            Fil[4] = "PlanId"; Val[4] = planid;
            Fil[5] = "LineName"; Val[5] = linename;
            Fil[6] = "BeginDate"; Val[6] = begindate;
            Fil[7] = "OrderNums"; Val[7] = ordernums;
            Fil[8] = "Adults"; Val[8] = adults;
            Fil[9] = "Childs"; Val[9] = childs;
            Fil[10] = "Price"; Val[10] = price;
            Fil[11] = "OrderName"; Val[11] = ordername;
            Fil[12] = "OrderTime"; Val[12] = DateTime.Now.ToString();
            Fil[13] = "OrderUser"; Val[13] = userid; // "453a3c3e-e479-408d-ae97-9f4c002b7bce";
            Fil[14] = "OrderFlag"; Val[14] = "1";
            Fil[15] = "DeptId"; Val[15] = "0";
            Fil[16] = "UserName"; Val[16] = ordername;
            Fil[17] = "LineDays"; Val[17] = LineDays;
            Fil[18] = "ccid"; Val[18] = CcId;

            Id = MyDataBaseComm.InsertDataStrGetReturn(TableName, Fil, Val);


            if (MyConvert.ConToInt(Id) == 0)
            {
                return "({\"error\":\"订单提交失败，请稍后再试\"})";
            }
            else
            {
                List<string> Sql = new List<string>();
                TableName = "OL_OrderPrice";
                Fil = new string[9];
                Val = new string[9];

                if (ProductFlag == "visa")
                {
                    Fil[0] = "OrderId"; Val[0] = orderid;
                    Fil[1] = "PriceType"; Val[1] = "SellPrice";
                    Fil[2] = "PriceName"; Val[2] = "旅游签证";
                    Fil[3] = "OrderNums"; Val[3] = ordernums;
                    Fil[4] = "SellPrice"; Val[4] = Request.Form["TB_Price"].Trim();
                    Fil[5] = "SumPrice"; Val[5] = price;
                    Fil[6] = "InputDate"; Val[6] = DateTime.Now.ToString();
                    Fil[7] = "PriceId"; Val[7] = "0";
                    Fil[8] = "PriceMemo"; Val[8] = "单办签证";

                    Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                }
                else
                {
                    string[] PriceType = Request.Form["p1"].Split(',');
                    string[] PriceId = Request.Form["p2"].Split(',');
                    string[] PriceName = Request.Form["p3"].Split(',');
                    string[] PriceMemo = Request.Form["p4"].Split(',');
                    string[] OrderNums = Request.Form["p9"].Split(',');
                    string[] SellPrice = Request.Form["p5"].Split(',');


                    for (int i = 0; i < PriceType.Length; i++)
                    {
                        if (OrderNums[i] != "0")
                        {
                            Fil[0] = "OrderId"; Val[0] = orderid;
                            Fil[1] = "PriceType"; Val[1] = PriceType[i];
                            Fil[2] = "PriceName"; Val[2] = PriceName[i];
                            Fil[3] = "OrderNums"; Val[3] = OrderNums[i];
                            Fil[4] = "SellPrice"; Val[4] = SellPrice[i];
                            Fil[5] = "SumPrice"; Val[5] = (MyConvert.ConToDec(OrderNums[i]) * MyConvert.ConToDec(SellPrice[i])).ToString();
                            Fil[6] = "InputDate"; Val[6] = DateTime.Now.ToString();
                            Fil[7] = "PriceId"; Val[7] = PriceId[i];
                            Fil[8] = "PriceMemo"; Val[8] = PriceMemo[i];

                            Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                        }
                    }
                }

                if (Convert.ToString(ConfigurationManager.AppSettings["prebook"]).IndexOf("," + lineid + ",") > -1)
                {
                    TableName = "OL_ActivityOrder";
                    Fil = new string[5];
                    Val = new string[5];
                    Fil[0] = "OrderId"; Val[0] = orderid;
                    Fil[1] = "AType"; Val[1] = "1";//预付活动
                    Fil[2] = "APrice"; Val[2] = "99";
                    Fil[3] = "ABeginDate"; Val[3] = Convert.ToDateTime("2015-11-11 00:00:00").ToString();
                    Fil[4] = "AEndDate"; Val[4] = Convert.ToDateTime("2015-11-11 23:59:59").ToString();
                    Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                }

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    return "({\"success\":\"" + orderid + "\"})";
                }
                else
                {
                    return "({\"error\":\"订单提交失败，请稍后再试\"})";
                }
            }

        }

        protected string LoadRoomPic()
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select *,(SELECT top 1 picurl from CR_Pic where roomid=CR_ShipRoom.id and pictype='room') as picurl from CR_ShipRoom where id='{0}'",
                Request.QueryString["Id"]
            );
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append(string.Format(@"<img src='{5}' width='250' height='160'>
                    <div class='info fl'>
                        <h4>{0}</h4>
                        <p>设施简介：{1}</p>
                        <dl>
                            <dd>可住人数：</dd>
                            <dd>{2}人</dd>
                        </dl>
                        <dl>
                            <dd>舱房面积：</dd>
                            <dd>{3}㎡</dd>
                        </dl>
                        <dl>
                            <dd>所在层数：</dd>
                            <dd>{4}层</dd>
                        </dl>
                    </div>
                    <div class='clear'></div>",
                    DS.Tables[0].Rows[0]["roomname"].ToString(),
                    DS.Tables[0].Rows[0]["intro"].ToString().Replace("\r\n", "<br>"),
                    DS.Tables[0].Rows[0]["berth"].ToString(),
                    DS.Tables[0].Rows[0]["area"].ToString(),
                    DS.Tables[0].Rows[0]["deck"].ToString(),
                    DS.Tables[0].Rows[0]["picurl"].ToString()
                ));
                return Strings.ToString();
            }
            else
            {
                return "没有查询到舱房信息";
            }
        }

        protected string LoadRoomInfo()
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select (select AgeLimit from OL_Line where MisLineId=CR_RoomAllot.lineid)as AgeLimit,(select count(id) from CR_RoomList where orderflag='0' and allotid=CR_RoomAllot.id) as sellroom,CR_RoomAllot.* from CR_RoomAllot where CR_RoomAllot.id='{0}'",
                Request.QueryString["Id"]
            );
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string AgeLimit = "";
                if (DS.Tables[0].Rows[0]["AgeLimit"].ToString().Length > 0) AgeLimit = "(" + DS.Tables[0].Rows[0]["AgeLimit"].ToString() + "周岁以下)";

                int HaveRoom = MyConvert.ConToInt(DS.Tables[0].Rows[0]["nums"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellroom"].ToString());
                int DefaultOrderNums = MyConvert.ConToInt(ConfigurationManager.AppSettings["DefaultOrderNums"]);
                int OrderNums = 0;
                int berth = MyConvert.ConToInt(DS.Tables[0].Rows[0]["berth"].ToString());
                if (HaveRoom < 0) HaveRoom = 0;

                OrderNums = berth * HaveRoom;
                if (OrderNums > DefaultOrderNums) OrderNums = DefaultOrderNums;
                string allotid = DS.Tables[0].Rows[0]["id"].ToString();
                //<div class=roomsell tps='{1}' haveroom='{2}' mnum='{3}' p1='{4}' p2='{5}' p3='{9}' tag={0} id=RoomSell{0}><span>成人：{6}</span><span>儿童{10}：{7} </span><span>房间数：{8} </span>价格小计：<span class=pcount>&yen;0</span><span class=RoomSelectButton><strong id=RB_{0} class=radioBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>选择此舱</span><span class=hide>0</span></div>
                Strings.Append(string.Format(@"<dl class='hdl' id='RoomSell{0}' tps='{1}' haveroom='{2}' mnum='{3}' p1='{4}' p2='{5}' p3='{9}' tag={0}>
                        <dd class='d1'>成人</dd>
                        <dd class='select'><select id='cr{0}' class='select2me'>{6}</select></dd>
                        <dd class='d2'>儿童{10}</dd>
                        <dd class='select'><select id='et{0}' class='select2me'>{7}</select></dd>
                        <dd class='d3'>房间数</dd>
                        <dd class='select'><select id='fjs{0}' class='select2co'>{8}</select></dd>
                        <dd class='d4'>总计：<span><em>￥</em>0</span><input class='countprice' type='hidden' value='' /></dd>
                        <dd class='d5' id='RB_{0}'><b></b>确定</dd>
                    </dl>",
                    allotid,
                    DS.Tables[0].Rows[0]["roomname"].ToString(),
                    HaveRoom,
                    berth,
                    DS.Tables[0].Rows[0]["price"].ToString(),
                    DS.Tables[0].Rows[0]["thirdprice"].ToString(),
                    GetOrderDropList(OrderNums, 0, "cr", allotid),
                    GetOrderDropList(OrderNums, 0, "et", allotid),
                    "<option value='0'>0</option>",
                    DS.Tables[0].Rows[0]["childprice"].ToString(),
                    AgeLimit
                ));
                return Strings.ToString();
            }
            else
            {
                return "没有查询到舱房信息";
            }
        }

        protected string GetOrderDropList(int OrderNums, int EditNum, string dropname, string allotid)
        {
            string OptionHtml = "";
            for (int i = 0; i <= OrderNums; i++)
            {
                if (i == 0)
                {
                    OptionHtml += string.Format("<option value='{0}' selected='selected'>{0}</option>", i);
                }
                else
                {
                    OptionHtml += string.Format("<option value='{0}'>{0}</option>", i);
                }
            }
            if (OrderNums == 0)
            {
                OptionHtml += "<option value='{0}' selected='selected'>满</option>";
            }
            return OptionHtml;
        }

        protected string LoadPrice()
        {
            string Planid = Request.QueryString["planid"];
            string Lineid = Request.QueryString["lineid"];
            string BeginDate = Request.QueryString["begindate"];
            int Adults = MyConvert.ConToInt(Request.QueryString["adults"]);
            int Childs = MyConvert.ConToInt(Request.QueryString["childs"]);
            int OrderNums = Adults + Childs;
            string PriceId = "0";
            string NoPrice = "没有任何可选择的费用！";

            string OptionHtml = "";
            string AdultOption = "";
            string ChildOption = "";
            string AdultSelOption = "";
            string ChildSelOption = "";
            string OrderNumsOption = "";
            string PreferOption = "";
            string Drops = "";

            AdultOption = string.Format("<option value=\"0\">0</option><option value=\"{0}\" selected=\"selected\">{0}</option>", Adults);
            ChildOption = string.Format("<option value=\"0\">0</option><option value=\"{0}\" selected=\"selected\">{0}</option>", Childs);
            AdultSelOption = string.Format("<option value=\"0\">0</option><option value=\"{0}\">{0}</option>", Adults);
            ChildSelOption = string.Format("<option value=\"0\">0</option><option value=\"{0}\">{0}</option>", Childs);
            OrderNumsOption = string.Format("<option value=\"0\">0</option><option value=\"{0}\" selected=\"selected\">{0}</option>", OrderNums);

            for (int i = 0; i <= OrderNums; i++)
            {
                OptionHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
            }

            for (int i = 0; i <= OrderNums; i++)
            {
                if (i == OrderNums)
                {
                    PreferOption += string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                }
                else
                {
                    PreferOption += string.Format("<option value=\"{0}\">{0}</option>", i);
                }
            }

            StringBuilder Strings = new StringBuilder();
            StringBuilder ExtStrings = new StringBuilder();

            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            PlanPrices GetPlan = new PlanPrices();

            try
            {
                if (Planid == "0")
                {
                    GetPlan = rsp.GetLinePrices(UpPassWord, Lineid, BeginDate);
                }
                else
                {
                    GetPlan = rsp.GetPlanPrices(UpPassWord, Lineid, Planid, PriceId);

                }
            }
            catch
            {
                return NoPrice;
            }
            if (GetPlan.PlanStaPrice != null)
            {
                Strings.Append("<h4>基本团费</h4><div class=\"select-content fl clearfix\">");
                if (Childs > 0)
                {
                    int havechilds = 0;
                    for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                    {
                        if (GetPlan.PlanStaPrice[i].PriceType == "儿童价") havechilds++;
                    }
                    if (havechilds == 0) AdultOption = OrderNumsOption;
                }

                for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                {
                    if (GetPlan.PlanStaPrice[i].PriceType == "儿童价" && Childs == 0)
                    { }
                    else
                    {
                        Drops = "";
                        switch (GetPlan.PlanStaPrice[i].PriceType)
                        {
                            case "成人价":
                                Drops = AdultOption;
                                AdultOption = AdultSelOption;
                                break;
                            case "儿童价":
                                Drops = ChildOption;
                                ChildOption = ChildSelOption;
                                break;
                            default:
                                Drops = OptionHtml;
                                break;
                        }
                        Strings.Append(string.Format(@"
                            <dl class='clearfix SellPrice' tag='{0}'>
                                <input name='p1' id='p1_{0}' type='hidden' value='SellPrice' />
                                <input name='p2' id='p2_{0}' type='hidden' value='{0}' />
                                <input name='p3' id='p3_{0}' type='hidden' value='{1}' />
                                <input name='p4' id='p4_{0}' type='hidden' value='{2}' />
                                <input name='p5' id='p5_{0}' type='hidden' value='{3}' />
							    <dt>{1}</dt>
							    <dd class='clearfix'>
								    <div class='con1'>{2}<span>￥{3}</span></div>
                                    <div class='select-num clearfix'>
									    <select id='p9_{0}' name='p9' class='select2me'>{4}</select>
									    <span>人</span>
									    <div class='other-price' id='p10_{0}'>- -</div>
									    <div class='sure'></div>
								    </div>
							    </dd>
						    </dl>",
                            GetPlan.PlanStaPrice[i].PriceId,
                            GetPlan.PlanStaPrice[i].PriceType,
                            GetPlan.PlanStaPrice[i].PriceName,
                            GetPlan.PlanStaPrice[i].Price,
                            Drops
                        ));

                    }

                }
                Strings.Append("</div><div class=\"wire\"></div>");
            }

            string AdultNeedOption = string.Format("<option value=\"{0}\">{0}</option>", Adults);
            string ChildNeedOption = string.Format("<option value=\"{0}\">{0}</option>", Childs);
            string AllNeedOption = string.Format("<option value=\"{0}\">{0}</option>", OrderNums);

            //OnlineNeeds

            if (GetPlan.PlanExtPrice != null)
            {
                string ExtType = "";
                Strings.Append("<h4>可选服务</h4><div class=\"select-content fl clearfix\">");
                for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                {
                    switch (GetPlan.PlanExtPrice[i].InfoId)
                    {
                        case "1":
                            ExtType = "单房差";
                            break;
                        case "2":
                            ExtType = "自费项目";
                            break;
                        case "3":
                            ExtType = "小费";
                            break;
                        case "4":
                            ExtType = "其他费用";
                            break;
                        case "5":
                            ExtType = "保险费用";
                            break;
                        case "6":
                            ExtType = "机票税金";
                            break;
                        case "7":
                            ExtType = "费用升级";
                            break;
                        default:
                            break;
                    }


                    switch (GetPlan.PlanExtPrice[i].OnlineNeeds)
                    {
                        case "1":
                            Drops = AdultNeedOption;
                            break;
                        case "2":
                            Drops = ChildNeedOption;
                            break;
                        case "3":
                            Drops = AllNeedOption;
                            break;
                        default:
                            Drops = OptionHtml;
                            break;
                    }

                    Strings.Append(string.Format(@"
                        <dl class='clearfix ExtPrice' tag='{0}'>
                            <input name='p1' id='p1_{0}' type='hidden' value='ExtPrice' />
                            <input name='p2' id='p2_{0}' type='hidden' value='{0}' />
                            <input name='p3' id='p3_{0}' type='hidden' value='{5}' />
                            <input name='p4' id='p4_{0}' type='hidden' value='{1}{2}' />
                            <input name='p5' id='p5_{0}' type='hidden' value='{3}' />
							<dt>{5}</dt>
							<dd class='clearfix'>
								<div class='con1'>{1}{2}<span>￥{3}</span></div>
                                <div class='select-num clearfix'>
									<select id='p9_{0}' name='p9' class='select2me'>{4}</select>
									<span>人</span>
									<div class='other-price' id='p10_{0}'>- -</div>
									<div class='sure'></div>
								</div>
							</dd>
						</dl>",
                        GetPlan.PlanExtPrice[i].PriceId,
                        GetPlan.PlanExtPrice[i].PriceType,
                        GetPlan.PlanExtPrice[i].PriceName,
                        GetPlan.PlanExtPrice[i].Price,
                        Drops,
                        ExtType
                    ));


                }
                Strings.Append("</div><div class=\"wire\"></div>");
            }

            string SqlQueryText = string.Format("select wwwyh from OL_Line where MisLineId='{0}'", Lineid);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0)
                {
                    Strings.Append("<h4>优惠信息</h4><div class=\"select-content fl clearfix\">");
                    Strings.Append(string.Format(@"
                        <dl class='clearfix ExtPrice' tag='{0}'>
                            <input name='p1' id='p1_{0}' type='hidden' value='Preference' />
                            <input name='p2' id='p2_{0}' type='hidden' value='{0}' />
                            <input name='p3' id='p3_{0}' type='hidden' value='{1}' />
                            <input name='p4' id='p4_{0}' type='hidden' value='每人立减{3}元' />
                            <input name='p5' id='p5_{0}' type='hidden' value='-{3}' />
							<dt>{5}</dt>
							<dd class='clearfix'>
								<div class='con1'>{1} 每人立减{3}元<span>￥-{3}</span></div>
                                <div class='select-num clearfix'>
									<select id='p9_{0}' name='p9' class='select2me'>{4}</select>
									<span>人</span>
									<div class='other-price' id='p10_{0}'>- -</div>
									<div class='sure'></div>
								</div>
							</dd>
						</dl>",
                        "0",
                        "在线支付优惠",
                        "",
                        DS.Tables[0].Rows[0]["wwwyh"].ToString(),
                        PreferOption,
                        "预订立减"
                        ));
                    Strings.Append("</div><div class=\"wire\"></div>");
                }
            }

            SqlQueryText = string.Format("select preferAmount from OL_Preferential where Lineid='{0}' and startDate<='{1}' and endDate>='{1}' and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate())", Lineid, Convert.ToDateTime(BeginDate));

            DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["preferAmount"].ToString()) > 0)
                {
                    Strings.Append("<h4>促销信息</h4><div class=\"select-content fl clearfix\">");
                    Strings.Append(string.Format(@"
                        <dl class='clearfix ExtPrice' tag='{0}'>
                            <input name='p1' id='p1_{0}' type='hidden' value='Preference' />
                            <input name='p2' id='p2_{0}' type='hidden' value='{0}' />
                            <input name='p3' id='p3_{0}' type='hidden' value='{1}' />
                            <input name='p4' id='p4_{0}' type='hidden' value='每人立减{3}元' />
                            <input name='p5' id='p5_{0}' type='hidden' value='-{3}' />
							<dt>{5}</dt>
							<dd class='clearfix'>
								<div class='con1'>{1} 每人立减{3}元<span>￥-{3}</span></div>
                                <div class='select-num clearfix'>
									<select id='p9_{0}' name='p9' class='select2me'>{4}</select>
									<span>人</span>
									<div class='other-price' id='p10_{0}'>- -</div>
									<div class='sure'></div>
								</div>
							</dd>
						</dl>",
                        "0",
                        "促销活动",
                        "",
                        DS.Tables[0].Rows[0]["preferAmount"].ToString(),
                        PreferOption,
                        "预订立减"
                        ));
                    Strings.Append("</div><div class=\"wire\"></div>");
                }
            }

            //return NoPrice;
            return Strings.ToString();
        }

        protected string checkCoupon()
        {
            string ProductType = Request.Form["ProductType"];
            if (ProductType == "Visa")
            {
                return "({\"error\":\"签证产品无法使用优惠券！\"})";
            }
            string couponNum = Request.Form["couponNum"];
            if(couponNum.Length==0) return "({\"error\":\"优惠券信息输入错误，请检查后再输入！\"})";
            if (Convert.ToString(ConfigurationManager.AppSettings["couponNum"]).IndexOf(couponNum) > -1)
            {
                string value = Convert.ToString(ConfigurationManager.AppSettings["value"]);
                string begindate = Convert.ToString(ConfigurationManager.AppSettings["beginDate"]);
                string enddate = Convert.ToString(ConfigurationManager.AppSettings["endDate"]);
                string amount = Convert.ToString(ConfigurationManager.AppSettings["amount"]);
                if (Decimal.Parse(amount).CompareTo(Decimal.Parse(Request.QueryString["price"].ToString())) > 0)
                {
                    SaveErrorToLog("订单金额小于" + amount + "无法使用此优惠券");
                    return "({\"error\":\"订单金额不足" + amount + "无法使用此优惠券！\"})";
                }
                DateTime beginTime = DateTime.ParseExact(begindate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                DateTime endTime = DateTime.ParseExact(enddate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                if (DateTime.Now.CompareTo(beginTime) < 0)
                {
                    return "({\"error\":\"未到优惠券使用日期！\"})";
                }
                if (DateTime.Now.CompareTo(endTime) > 0)
                {
                    return "({\"error\":\"优惠券已过期！\"})";
                }
                return "({\"success\":\"" + value + "\"})";
            }
            string userId = Convert.ToString(Session["Online_UserId"]);
            string sqlstr = string.Format("SELECT *,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where userid='{0}' and uno='{1}'", userId, couponNum);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            string par = "0";
            if (DS.Tables[0].Rows.Count > 0)
            {
                //id = DS.Tables[0].Rows[0]["Id"].ToString();
                //sellflag = DS.Tables[0].Rows[0]["sellflag"].ToString();//
                //deduction = DS.Tables[0].Rows[0]["deduction"].ToString();//优惠方式
                //range = DS.Tables[0].Rows[0]["range"].ToString();
                //product = DS.Tables[0].Rows[0]["product"].ToString();
                string begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]);
                string enddate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                //pbdate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["pbdate"]);
                //pedate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["pedate"]);
                par = DS.Tables[0].Rows[0]["par"].ToString();//优惠券金额
                //sellprice = DS.Tables[0].Rows[0]["sellprice"].ToString();
                string amount = DS.Tables[0].Rows[0]["amount"].ToString();//最低使用金额
                string flag = DS.Tables[0].Rows[0]["flag"].ToString();//是否使用 0未使用，1已使用
                //memo = DS.Tables[0].Rows[0]["memo"].ToString();
                //sellnums = DS.Tables[0].Rows[0]["sellnums"].ToString();
                //logourl = DS.Tables[0].Rows[0]["picurl"].ToString();
                //pre_no = DS.Tables[0].Rows[0]["pre_no"].ToString();
                if (Decimal.Parse(amount).CompareTo(Decimal.Parse(Request.QueryString["price"].ToString())) > 0)
                {
                    SaveErrorToLog("订单金额小于" + amount + "无法使用此优惠券");
                    return "({\"error\":\"订单金额不足" + amount + "无法使用此优惠券！\"})";
                }
                DateTime beginTime = DateTime.ParseExact(begindate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                DateTime endTime = DateTime.ParseExact(enddate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                if (DateTime.Now.CompareTo(beginTime) < 0)
                {
                    return "({\"error\":\"未到优惠券使用日期！\"})";
                }
                if (DateTime.Now.CompareTo(endTime) > 0)
                {
                    return "({\"error\":\"优惠券已过期！\"})";
                }
                if ("1".Equals(flag))
                {
                    return "({\"error\":\"优惠券已使用！\"})";
                }
            }
            else
            {
                return "({\"error\":\"优惠券信息输入错误，请检查后再输入！\"})";
            }
            return "({\"success\":\"" + par + "\"})";
        }

        protected string checkIntegral()
        {
            string ProductType = Request.Form["ProductType"];
            int ratio = Convert.ToInt32(ConfigurationManager.AppSettings["Integral_ratio"]);
            if (ProductType == "Visa")
            {
                return "({\"error\":\"签证产品无法使用积分！\"})";
            }
            string integralNum = Request.Form["IntegralNum"];
            //判断是否为整百数
            try
            {
                string SqlQueryText = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(Session["Online_UserId"]));
                string integralMaxNum = MyDataBaseComm.getScalar(SqlQueryText);
                //int? integralMaxNum = DataClass.GetIntegral(new Guid(Convert.ToString(Session["Online_UserId"])));
                if (Convert.ToInt32(integralNum) % ratio != 0) return "({\"error\":\"使用积分只能是"+ ratio + "的倍数！\"})";
                else if (Convert.ToInt32(integralNum) < 0) return "({\"error\":\"请输入正确的积分！\"})";
                else if (Convert.ToInt32(integralNum) > Convert.ToInt32(integralMaxNum)) return "({\"error\":\"积分不足，无法使用积分！\"})";
                else return "({\"success\":\"" + Convert.ToInt32(integralNum) / ratio + "\"})";
            }
            catch
            {
                return "({\"error\":\"请输入正确的积分！\"})";
            }
        }

        protected string lotteryCoupon()
        {
            int v = 0;
            string userId = Convert.ToString(Session["Online_UserId"]);
            string sqlstr = string.Format("select count(1) from pre_ticket where convert(varchar(10),inputdate,120)='{0}' and userid='{1}' and type='lottery'", string.Format("{0:yyyy-MM-dd}", DateTime.Today), userId);
            string count = MyDataBaseComm.getScalar(sqlstr);
            if (MyConvert.ConToInt(count) >= 3)
            {
                return "({\"error\":\"今日已转三次，请明日再试！\"})";
            }
            sqlstr = string.Format("select count(1) from pre_ticket where userid='{0}' and type='lottery'", userId);
            string num = MyDataBaseComm.getScalar(sqlstr);
            Int64 numVal = MyConvert.ConToInt(num);
            string cid = "0";
            if (numVal < 10)
            {
                v = random(0, 2);
            }
            else if (numVal > 10 && numVal < 50)
            {
                v = random(0, 4);
            }
            else if (numVal > 50 && numVal < 100)
            {
                v = random(0, 6);
            }
            else if (numVal > 100 && numVal < 200)
            {
                v = random(0, 11);
            }
            else if (numVal > 200)
            {
                v = random(0, 16);
            }
            switch (v)
            {
                case 0:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar5"]);
                    break;
                case 1:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar5"]);
                    break;
                case 3:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar58"]);
                    break;
                case 5:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar188"]);
                    break;
                case 10:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar288"]);
                    break;
                case 15:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar888"]);
                    break;
                default:
                    cid = Convert.ToString(ConfigurationManager.AppSettings["couponPar5"]);
                    break;

            }

            string SqlQueryText = string.Format("select * from Pre_Policy where id='{0}'", cid.Trim());
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                String AutoId = Convert.ToString(CombineKeys.NewComb());

                SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag,type) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','lottery')",
                    DS1.Tables[0].Rows[0]["id"].ToString(),
                    AutoId,
                    MyConvert.CreateVerifyCode(12),
                    DS1.Tables[0].Rows[0]["par"].ToString(),
                    DS1.Tables[0].Rows[0]["amount"].ToString(),
                    userId,
                    DS1.Tables[0].Rows[0]["begindate"].ToString(),
                    DS1.Tables[0].Rows[0]["enddate"].ToString(),
                    DateTime.Now.ToString(),
                    "0",
                    DS1.Tables[0].Rows[0]["deduction"].ToString(),
                    DS1.Tables[0].Rows[0]["range"].ToString(),
                    DS1.Tables[0].Rows[0]["product"].ToString(),
                    Convert.ToString(Session["Online_UserEmail"]),
                    Convert.ToString(Session["Online_UserName"]),
                    DS1.Tables[0].Rows[0]["pbdate"].ToString(),
                    DS1.Tables[0].Rows[0]["pedate"].ToString(),
                    DS1.Tables[0].Rows[0]["sellflag"].ToString()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    return "({\"success\":1,\"item\":" + v + "})";
                }
                else
                {
                    return "({\"error\":\"抽取失败，请稍后重试！\"})";
                }
            }
            else
            {
                return "({\"error\":\"抽取失败，请稍后重试！\"})";
            }
        }
        protected string lotteryCoupon2017()
        {
            //判断活动期间
            if (DateTime.Now < new DateTime(2017, 1, 12)) return "({\"error\":\"活动尚未开始，敬请期待！\"})";
            if (DateTime.Now >= new DateTime(2017, 1, 31)) return "({\"error\":\"活动已结束！\"})";

            int v = 0;
            string userId = Convert.ToString(Session["Online_UserId"]);
            string sqlstr = string.Format("select count(1) from pre_ticket where convert(varchar(10),inputdate,120)='{0}' and userid='{1}' and type='lottery'", string.Format("{0:yyyy-MM-dd}", DateTime.Today), userId);
            string count_ticket = MyDataBaseComm.getScalar(sqlstr);
            sqlstr = string.Format("select count(1) from OL_Integral where convert(varchar(10),getdate,120)='{0}' and uid='{1}' and getdate = enddate", string.Format("{0:yyyy-MM-dd}", DateTime.Today), userId);
            string count_integral = MyDataBaseComm.getScalar(sqlstr);
            if (MyConvert.ConToInt(count_ticket) + MyConvert.ConToInt(count_integral) >= 3)
            {
                return "({\"error\":\"今日已抽取三次，请明日再试！\"})";
            }
            //随机
            v = random(1, 101);
            string cid = "0";//优惠卷
            string req = "";
            bool ticket_integral = false; //ticket-true  integral-false
            if (v == 1 || v == 100)//188元现金券    2%
            {
                ticket_integral = true;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得188元现金券,可至“首页-我的青旅-我的优惠券”查看！\"})";
                cid = Convert.ToString(ConfigurationManager.AppSettings["2017coupon188"]);
            }
            else if (v >= 2 && v <= 4)//88元现金券  3%
            {
                ticket_integral = true;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得88元现金券,可至“首页-我的青旅-我的优惠券”查看！\"})";
                cid = Convert.ToString(ConfigurationManager.AppSettings["2017coupon88"]);
            }
            else if (v <= 99 && v >= 95)//58元现金券    5%
            {
                ticket_integral = true;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得58元现金券,可至“首页-我的青旅-我的优惠券”查看！\"})";
                cid = Convert.ToString(ConfigurationManager.AppSettings["2017coupon58"]);
            }
            else if (v >= 5 && v <= 14)//28元现金券 10%
            {
                ticket_integral = true;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得28元现金券,可至“首页-我的青旅-我的优惠券”查看！\"})";
                cid = Convert.ToString(ConfigurationManager.AppSettings["2017coupon28"]);
            }
            else if (v <= 94 && v >= 75) //8元现金券    20%
            {
                ticket_integral = true;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得8元现金券,可至“首页-我的青旅-我的优惠券”查看！\"})";
                cid = Convert.ToString(ConfigurationManager.AppSettings["2017coupon8"]);
            }
            else if (v % 2 == 1) //10积分 30%
            {
                ticket_integral = false;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得10积分,可至“首页-我的青旅-我的积分”查看！\"})";
            }
            else if (v % 2 == 0)//20积分  30%
            {
                ticket_integral = false;
                req = "({\"success\":" + v + ",\"item\":\"恭喜你获得20积分,可至“首页-我的青旅-我的积分”查看！\"})";
            }
            else
            {
                req = "({\"error\":\"抽取失败，请稍后重试！\"})";
            }
            if (ticket_integral)
            {
                string SqlQueryText = string.Format("select * from Pre_Policy where id='{0}'", cid.Trim());
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    String AutoId = Convert.ToString(CombineKeys.NewComb());

                    SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag,type) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','lottery')",
                        DS1.Tables[0].Rows[0]["id"].ToString(),
                        AutoId,
                        MyConvert.CreateVerifyCode(12),
                        DS1.Tables[0].Rows[0]["par"].ToString(),
                        DS1.Tables[0].Rows[0]["amount"].ToString(),
                        userId,
                        DS1.Tables[0].Rows[0]["begindate"].ToString(),
                        DS1.Tables[0].Rows[0]["enddate"].ToString(),
                        DateTime.Now.ToString(),
                        "0",
                        DS1.Tables[0].Rows[0]["deduction"].ToString(),
                        DS1.Tables[0].Rows[0]["range"].ToString(),
                        DS1.Tables[0].Rows[0]["product"].ToString(),
                        Convert.ToString(Session["Online_UserEmail"]),
                        Convert.ToString(Session["Online_UserName"]),
                        DS1.Tables[0].Rows[0]["pbdate"].ToString(),
                        DS1.Tables[0].Rows[0]["pedate"].ToString(),
                        DS1.Tables[0].Rows[0]["sellflag"].ToString()
                    );

                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        return req;
                    }
                    else
                    {
                        return "({\"error\":\"抽取失败，请稍后重试！\"})";
                    }
                }
                else
                {
                    return "({\"error\":\"抽取失败，请稍后重试！\"})";
                }
            }
            else
            {
                string SqlQueryText = string.Format("INSERT INTO [dbo].[OL_Integral] ([uid],[integral],[getdate],[flag],[dept],[enddate]) " +
                                                                               "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')"
                                       , Convert.ToString(Session["Online_UserId"])
                                       , v % 2 == 0 ? 20 : 10
                                       , DateTime.Now.ToString()
                                       , '0'
                                       , '0'
                                       , DateTime.Now.ToString());

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    return req;
                }
                else
                {
                    return "({\"error\":\"抽取失败，请稍后重试！\"})";
                }
            }
        }

        public string showScenic()
        {
            string scenicName = Request.QueryString["scenicName"];
            string showScenic = "";
            if (scenicName != "" && scenicName != "")
            {
                string SqlQueryText = string.Format("select * from OL_View where viewname = '{0}'", scenicName.Trim());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ViewInfo viewInfo = new ViewInfo();
                    viewInfo.viewname = DS.Tables[0].Rows[0]["viewname"].ToString();
                    viewInfo.intro = DS.Tables[0].Rows[0]["intro"].ToString();
                    string SqlQueryPic = string.Format("SELECT picurl FROM OL_ViewPic where viewid= {0}", DS.Tables[0].Rows[0]["id"].ToString());
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryPic);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        string pics = "";
                        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                        {
                            pics = pics + DS1.Tables[0].Rows[i]["picurl"].ToString() + ",";
                        }
                        viewInfo.pics = pics.Substring(0, pics.Length - 1);
                    }
                    showScenic = Regex.Unescape(JsonMapper.ToJson(viewInfo)).Replace("\r\n", "<br>");
                }
            }
            return showScenic;
        }

        private int random(int min, int max)
        {
            Random ra = new Random();
            return ra.Next(min, max);
        }

        private Boolean isboshActivity(string lineid)
        {
            return Session["userType"] != null && Session["userType"].ToString().Equals("ShYinHangRegUser") && Convert.ToString(ConfigurationManager.AppSettings["boshActivity"]).IndexOf(lineid) > -1;
        }

        private static void SaveErrorToLog(string infos)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\NewOrderLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(infos);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

    }
}