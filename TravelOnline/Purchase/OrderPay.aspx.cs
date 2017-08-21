using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Com.Alipay;
using System.Configuration;

using System.IO;
using System.Xml;
//using System.Runtime.InteropServices;
using TravelOnline.TravelMisWebService;
//using [DllImport("MW_SignalClient.dll")];

namespace TravelOnline.Purchase
{
    public partial class OrderPay : System.Web.UI.Page
    {
        //[DllImport("ICBCEBankUtil.dll")]
        
        public string QueryId, OrderId, CouponId,LineName, FkInfo, yfk, Pay, YeE, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice;
        public string LineUrl, Price, AutoId, Rebate, hide, hide1, hide_test, planid, pfyhinfo,pufaprice;
        public string hideIcbc, hidePingAn, hideAliPay, hideAliAll, hidePfyh, IcbcCheck, PingAnCheck, AliPayCheck, PufaCheck; //checked="checked"
        decimal N_PayNow, N_Pay, N_Yfk, N_Yue;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(string.Format("<orderDate>{0:yyyyMMddHHmmss}</orderDate>", DateTime.Now.AddDays(5)));
            QueryId = Request.QueryString["OrderId"];

            //if (TravelOnline.Class.Common.PublicPageKeyWords.NewPage == "1")
            //{
            //    Response.Redirect("/pay.html?orderid=" + QueryId);
            //    Response.End();
            //}
            Response.Redirect("/pay.html?orderid=" + QueryId);
            Response.End();

            //string SqlQueryText = string.Format("select PlanId from OL_Order where OrderId='{0}'", QueryId);

            //DataSet DS = new DataSet();
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    string planid = "," + DS.Tables[0].Rows[0]["planid"].ToString() + ",";
            //    if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
            //    {
            //        Response.Redirect("/pay.html?orderid=" + QueryId);
            //        Response.End();
            //    }
            //}
            

            CouponId = Request.QueryString["CouponId"];
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                //Response.Redirect("~/index.html", true);
            }

            hide_test = "hide";
            hideIcbc = "hide";
            hidePingAn = "hide";
            hidePfyh = "hide";

            AliPayCheck = "checked=\"checked\"";
            PufaCheck = "";
            IcbcCheck = "";
            PingAnCheck = "";

            if (Request.Cookies["OnlyIcbcPay"] != null)
            {
                hide_test = "";
            }

            if (Request.Cookies["OnlyIcbcPay"] != null)
            {
                hideIcbc = "";
                hideAliPay = "hide";
                hideAliAll = "hide";
                hidePingAn = "hide";
                
                AliPayCheck = "";
                PingAnCheck = "";
                IcbcCheck = "checked=\"checked\"";
            }
            //else
            //{
            //    hideIcbc = "hide";
            //    AliPayCheck = "checked=\"checked\"";
            //}

            if (Request.Cookies["OnlyPingAnPay"] != null)
            {
                hidePingAn = "";
                hideAliPay = "hide";
                hideAliAll = "hide";
                hideIcbc = "hide";

                IcbcCheck = "";
                AliPayCheck = "";
                PingAnCheck = "checked=\"checked\"";
            }
            

            if (!IsPostBack)
            {
                hide1 = "hide";
                if (QueryId != null)
                {
                    LoadTempOrder();
                }
                else
                {
                    hide = "hide";
                    LoadCouponOrder();
                }
                
            }
            //E:\VS2010\TravelOnline\TravelOnline\Purchase\PayMent\Icbc_Cert\scyts.com.crt 
            //Response.Write(Server.MapPath("../PayMent/Icbc_Cert/scyts.com.crt").ToString());
            //string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, id)
        }

        protected void LoadCouponOrder()
        {
            string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", CouponId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString();
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                
                YeE = DS.Tables[0].Rows[0]["Price"].ToString();
                Pay = "0";
                yfk = YeE;

                decimal N_Nums = MyConvert.ConToDec(DS.Tables[0].Rows[0]["OrderNums"].ToString());//订单人数
                decimal N_Yfk = MyConvert.ConToDec(yfk);//应付余额
                decimal All_Yfk = N_Yfk * N_Nums;//最低预付总额
                All_Yfk = MyConvert.ConToDec(YeE);
                FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">" + YeE + "</span> （本次最低在线支付款项）</div></li>";
                        
                yfk = All_Yfk.ToString();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString();
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                Rebate = DS.Tables[0].Rows[0]["rebate"].ToString();

                planid = "," + DS.Tables[0].Rows[0]["planid"].ToString() + ",";

                LineUrl = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["ProductType"], DS.Tables[0].Rows[0]["ProductClass"], DS.Tables[0].Rows[0]["LineID"]);

                //<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span1\">" +  DS.Tables[0].Rows[0]["yfk"].ToString() + "</span> （每位客人）</div></li>

                YeE = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString())).ToString();

                //if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["PayFlag"].ToString()) == 0)
                //{
                    
                //}
                if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
                {
                    string ordernums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                    decimal lijian = 2000 * MyConvert.ConToDec(ordernums);
                    pufaprice = (MyConvert.ConToDec(Price) - lijian).ToString();
                    decimal pprice = MyConvert.ConToDec(YeE);
                    decimal sjfk = MyConvert.ConToDec(pufaprice);
                    if (pprice > lijian)
                    {
                        YeE = (pprice - lijian).ToString();
                    }
                    else
                    {
                        YeE = "0";
                    }
                        

                    hide_test = "hide";
                    hideIcbc = "hide";
                    hidePingAn = "hide";
                    hideAliPay = "hide";
                    hideAliAll = "hide";
                    hideIcbc = "hide";
                    hidePfyh = "";

                    AliPayCheck = "";
                    PufaCheck = "checked=\"checked\"";
                    IcbcCheck = "";
                    PingAnCheck = "";
                    pfyhinfo = "浦发信用卡活动每人立减2000元，请选择信用卡支付";
                }

                if (MyConvert.ConToDec(Rebate) > 0)
                {
                    if (DS.Tables[0].Rows[0]["RebateFlag"].ToString() == "1")
                    {
                        hide1 = "";
                        YeE = (MyConvert.ConToDec(YeE) - MyConvert.ConToDec(Rebate)).ToString();
                    }
                }
                
                Pay = DS.Tables[0].Rows[0]["pay"].ToString();
                yfk = MyConvert.ConToDec(DS.Tables[0].Rows[0]["yfk"].ToString()).ToString();

                decimal N_Nums = MyConvert.ConToDec(DS.Tables[0].Rows[0]["OrderNums"].ToString());//订单人数
                decimal N_Yfk = MyConvert.ConToDec(DS.Tables[0].Rows[0]["yfk"].ToString());//应付余额
                decimal All_Yfk = N_Yfk * N_Nums;//最低预付总额

                if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString()) == 0)
                {

                    if (All_Yfk == 0)
                    {
                        if (MyConvert.ConToDec(YeE) > 500)
                        {
                            All_Yfk = 500;
                            FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">500</span> （本次最低在线支付款项）</div></li>";
                        }
                        else
                        {
                            All_Yfk = MyConvert.ConToDec(YeE);
                            FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">" + YeE + "</span> （本次最低在线支付款项）</div></li>";
                        }
                    }
                    else
                    {
                        FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">" + DS.Tables[0].Rows[0]["yfk"].ToString() + " * " + Nums + "人 = &yen;" + All_Yfk + "</span> （本次最低在线支付款项）</div></li>";
                    }
                    if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
                    {
                        All_Yfk = 3000;
                        FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">3000</span> </div></li>";
                        
                    }
                    //if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["yfk"].ToString()) > 0) FkInfo = "<li><div class=tname>最低预付：</div><div class=tinfo><span class=\"base_price02\">&yen;</span><span class=\"base_price02\" id=\"span_yfk\">" + DS.Tables[0].Rows[0]["yfk"].ToString() + " * " + Nums + "人 = &yen;" + All_Yfk + "</span> （本次最低在线支付款项）</div></li>";
                }
                yfk = All_Yfk.ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format("({0}成人 {1}儿童)", Adults, Childs);
                BeginDate = string.Format("{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);

                if (DS.Tables[0].Rows[0]["PayFlag"].ToString() == "0")
                {
                    if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "2")
                    {
                    }
                    else
                    {
                        LinkButton1.Text = "暂不能支付";
                    }
                }
                else
                {
                    //LinkButton1.Text = "暂不能支付";
                    //LinkButton1.Enabled = false;
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (QueryId != null)
            {
                if (LinkButton1.Text == "暂不能支付")
                {
                    LoadTempOrder();
                    return;
                }
            }
            else
            {
                if (LinkButton1.Text == "暂不能支付")
                {
                    hide = "hide";
                    LoadCouponOrder();
                    return;
                }

                string SqlQueryText = string.Format("select OrderId,ProductClass,OrderNums,OrderUser from OL_Order where ProductType='Coupon' and OrderId='{0}'", CouponId);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    //自动券号生成，付款成功后操作 
                    //PurchaseClass.CouponGetAfterBuy(DS.Tables[0].Rows[0]["OrderUser"].ToString(), DS.Tables[0].Rows[0]["ProductClass"].ToString(), MyConvert.ConToInt(DS.Tables[0].Rows[0]["OrderNums"].ToString()));
                }
                else
                {
                    List<string> Sql = new List<string>();
                    Sql.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", CouponId));
                    Sql.Add(string.Format("update OL_Order set PayFlag='0',OrderFlag='2',OrderTime='{1}' where OrderId='{0}'", CouponId, DateTime.Now.ToString()));
                    string[] SqlQuery = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuery) == true)
                    {

                    }
                    else
                    {
                        LinkButton1.Text = "优惠券购买失败，暂不能支付";
                        return;
                    }
                }
                
            }

            //费用判断 P_Pay  Request.Form["P_PayNow"] decimal N_PayNow，N_Pay，N_Yfk，N_Yue
            N_PayNow = MyConvert.ConToDec(Request.Form["P_PayNow"]);//本次支付
            N_Pay = MyConvert.ConToDec(Request.Form["P_Pay"]);//已付总额
            N_Yfk = MyConvert.ConToDec(Request.Form["P_Yfk"]);//最低预付
            N_Yue = MyConvert.ConToDec(Request.Form["P_Yue"]);//应付余额

            if (N_PayNow > N_Yue)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"本次付款金额大于应付余额！\");</script>");
                LoadTempOrder();
                return;
            }

            if (N_Pay == 0)
            {
                if (N_PayNow < N_Yfk)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"本次付款金额少于最低预付款！\");</script>");
                    LoadTempOrder();
                    return;
                }
            }
            //ICBCB2C
            //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"" + Request.Form["paytype"] + "\");</script>");
            switch (Request.Form["paytype"])
            {
                case "AliPay":
                    DoAliPay();
                    break;
                case "CMB_Scyts":
                    CMBPay();
                    break;
                case "IcbcPay":
                    ICBC_Pay();
                    break;
                //case "ICBCB2C":
                //    ICBC_Pay();
                //    break;
                default:
                    DoAliPay();
                    break;
            }

        }

        private void SaveErrorToLog(string inErrorlog, string inSQL)
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

        private void ICBC_Pay()
        {
            //if (MyConvert.ConToDec(Request.Form["P_PayNow"]) > 2)
            //{
            //    hide1 = "hide";
            //    if (QueryId != null)
            //    {
            //        LoadTempOrder();
            //    }
            //    else
            //    {
            //        hide = "hide";
            //        LoadCouponOrder();
            //    }
            //    ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('工商银行快捷支付暂时不能操作，请选择其他支付方式或支付宝下方的工商银行支付');", true);
            //    return;
            //}

            //ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('支付错误，请重试');", true);
            //return;
            string gather = (MyConvert.ConToDec(Request.Form["P_PayNow"]) * 100).ToString().Replace(".00", "");
            //Response.Write(Request.getRemoteAddr());
            //Response.End();

            StringBuilder tranData = new StringBuilder();
            tranData.Append("<?xml version=\"1.0\" encoding=\"GBK\" standalone=\"no\"?>");
            tranData.Append("<B2CReq>");
            tranData.Append("<interfaceName>ICBC_PERBANK_B2C</interfaceName>");
            tranData.Append("<interfaceVersion>1.0.0.11</interfaceVersion>");

            tranData.Append("<orderInfo>");
            tranData.Append(string.Format("<orderDate>{0:yyyyMMddHHmmss}</orderDate>", DateTime.Now.AddMinutes(27)));
            tranData.Append("<curType>001</curType>"); //目前工行只支持使用人民币（001）支付
            tranData.Append("<merID>1001EC23820844</merID>"); //唯一确定一个商户的代码，由商户在工行开户时，由工行告知商户
            tranData.Append("<subOrderInfoList>");
            tranData.Append("<subOrderInfo>");
            tranData.Append(string.Format("<orderid>{0}</orderid>", Request.Form["P_AutoId"] + Request.Form["CMB_PayNo"]));
            //tranData.Append("<amount>123456</amount>"); //客户支付订单的总金额，一笔订单一个，以分为单位。不可以为零
            
            tranData.Append(string.Format("<amount>{0}</amount>", gather)); //客户支付订单的总金额，一笔订单一个，以分为单位。不可以为零
            tranData.Append("<installmentTimes>1</installmentTimes>"); //1、3、6、9、12、18、24；1代表全额付款
            tranData.Append("<merAcct>1001172929004618866</merAcct>");
            tranData.Append("<goodsID></goodsID>");

            string lingname = Request.Form["P_LineName"].ToString();
            int slengh = lingname.Length;
            if (slengh > 30) slengh = 30;

            tranData.Append(string.Format("<goodsName>{0}</goodsName>", lingname.Substring(0, slengh) + " " + Request.Form["P_AutoId"]));
            tranData.Append(string.Format("<goodsNum>{0}</goodsNum>", Request.Form["P_Nums"]));
            tranData.Append("<carriageAmt></carriageAmt>");
            tranData.Append("</subOrderInfo>");
            tranData.Append("</subOrderInfoList>");
            tranData.Append("</orderInfo>");

            tranData.Append("<custom>");
            tranData.Append("<verifyJoinFlag>0</verifyJoinFlag>");
            tranData.Append("<Language>ZH_CN</Language>");
            tranData.Append("</custom>");

            tranData.Append("<message>");
            tranData.Append("<creditType>2</creditType>");
            tranData.Append("<notifyType>HS</notifyType>"); //在交易转账处理完成后把交易结果通知商户的处理模式。取值“HS”：在交易完成后实时将通知信息以HTTP协议POST方式，主动发送给商户，发送地址为商户端随订单数据提交的接收工行支付结果的URL即表单中的merURL字段；取值“AG”：在交易完成后不通知商户。商户需使用浏览器登录工行的B2C商户服务网站，或者使用工行提供的客户端程序API主动获取通知信息
            tranData.Append("<resultType>1</resultType>"); //取值“0”：无论支付成功或者失败，银行都向商户发送交易通知信息；取值“1”，银行只向商户发送交易成功的通知信息
            
            //tranData.Append("<merReference>*.scyts.com</merReference>"); //上送商户网站域名（支持通配符，例如“*.某B2C商城.com”），如果上送，工行会在客户支付订单时，校验商户上送域名与客户跳转工行支付页面之前网站域名的一致性
            tranData.Append("<merReference>*.scyts.com</merReference>");//*.scyts.com

            //tranData.Append("<merCustomIp>116.247.72.214</merCustomIp>");
            //tranData.Append("<merCustomIp>180.168.167.214</merCustomIp>");
            //tranData.Append("<merCustomIp>101.80.146.196</merCustomIp>");
            tranData.Append("<merCustomIp></merCustomIp>");
            tranData.Append("<goodsType>1</goodsType>");
            tranData.Append("<merCustomID></merCustomID>");
            tranData.Append("<merCustomPhone></merCustomPhone>");
            tranData.Append("<goodsAddress></goodsAddress>");
            tranData.Append("<merOrderRemark></merOrderRemark>");
            tranData.Append(string.Format("<merHint>{0}</merHint>", "出发日期：" + Request.Form["P_Date"]));
            tranData.Append("<remark1></remark1>");
            tranData.Append("<remark2></remark2>");
            tranData.Append(string.Format("<merURL>{0}</merURL>", Convert.ToString(ConfigurationManager.AppSettings["IcbcPayShowUrl"])));
            tranData.Append(string.Format("<merVAR>{0}</merVAR>", Request.Form["P_OrderId"]));
            tranData.Append("</message>");
            tranData.Append("</B2CReq>");



            tranData.Append("");
            tranData.Append("");
            tranData.Append("");

            SaveErrorToLog("xml数据：", tranData.ToString());
            //Response.Write(tranData.ToString());
            //Response.End();
            string TransData="", SignData="", CertData="";
            //string keyPass = "12345678"; //商城证书私钥密码
            StringBuilder PayString = new StringBuilder();


            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

            IcbcSignData SignInfo = new IcbcSignData();

            SignInfo = rsp.Icbc_Sign_Class(UpPassWord, tranData.ToString());

            TransData = SignInfo.TransData;
            SignData = SignInfo.SignData;
            CertData = SignInfo.CertData;

            //曲线救国
            //infosecapiLib.infosec icbc_infosec = new infosecapiLib.infosec();
            //TransData = icbc_infosec.base64enc(tranData.ToString());
            ////签名
            //SignData = icbc_infosec.sign(tranData.ToString(), Server.MapPath("../PayMent/Icbc_Cert/SCYTS001.key"), keyPass); //商城私钥文件(.key)
            //CertData = icbc_infosec.base64encfile(Server.MapPath("../PayMent/Icbc_Cert/SCYTS001.crt")); //商城公钥文件(.crt)

            SaveErrorToLog("SignData：", SignData);
            SaveErrorToLog("CertData：", CertData);
            
            //https://83.41.2.81/servlet/NewB2cMerPayReqServlet
            //PayString.Append("<form id=\"fm\" action=\"https://83.41.2.81/servlet/NewB2cMerPayReqServlet\" method=\"post\">");
            PayString.Append("<form id=\"fm\" action=\"https://B2C.icbc.com.cn/servlet/ICBCINBSEBusinessServlet\" method=\"post\">");
            PayString.Append("<input id=\"interfaceName\" name=\"interfaceName\" type=\"hidden\" value=\"ICBC_PERBANK_B2C\" >");
            PayString.Append("<input id=\"interfaceVersion\" name=\"interfaceVersion\" type=\"hidden\" value=\"1.0.0.11\" >");
            PayString.Append("");
            PayString.Append("");

            PayString.Append(string.Format("<input id=\"tranData\" name=\"tranData\" type=\"hidden\" value=\"{0}\">", TransData));
            PayString.Append(string.Format("<input id=\"merSignMsg\" name=\"merSignMsg\" type=\"hidden\" value=\"{0}\">", SignData));
            PayString.Append(string.Format("<input id=\"merCert\" name=\"merCert\" type=\"hidden\" value=\"{0}\">", CertData));
            
            PayString.Append("<input type=\"submit\" value=\"确认\" style=\"display:none;\">");
            PayString.Append("</form>");
            PayString.Append("");
            PayString.Append("<script>document.forms['fm'].submit();</script>");

            string SqlQueryText;
            SqlQueryText = string.Format("insert into ICBC_Pay (orderid,payid,amount,date,flag) values ('{0}','{1}','{2}','{3}','0')",
                    Request.Form["P_OrderId"],
                    Request.Form["P_AutoId"] + Request.Form["CMB_PayNo"],
                    MyConvert.ConToDec(Request.Form["P_PayNow"]),
                    DateTime.Now.ToString()
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write(PayString.ToString());
                Response.End();
            }
            else
            {
                hide1 = "hide";
                if (QueryId != null)
                {
                    LoadTempOrder();
                }
                else
                {
                    hide = "hide";
                    LoadCouponOrder();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('支付信息提交错误，请重试！');", true);
                return;
            }

            
        }

        //private static void SaveErrorToLog(string inErrorlog, string inSQL)
        //{
        //    //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
        //    string path = AppDomain.CurrentDomain.BaseDirectory + @"\icbc.txt";

        //    try
        //    {
        //        StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
        //        writer.WriteLine(DateTime.Now.ToString() + ":");
        //        writer.WriteLine(inErrorlog);
        //        writer.WriteLine(inSQL);
        //        writer.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        string message = exception.Message;
        //    }
        //}

        private void CMBPay()
        {
            StringBuilder PayString = new StringBuilder();

            //RoomString.Append(string.Format("<div id=\"{0}\" class=\"hide\">", Cruises.CruisesPlans[i].PlanId));
            //PayString.Append("<form id=\"fm\" action=\"https://netpay.cmbchina.com/netpayment/basehttp.dll?prepayc2\" method=\"post\">");
            PayString.Append("<form id=\"fm\" action=\"https://netpay.cmbchina.com/netpayment/BaseHttp.dll?testPrePayC2\" method=\"post\">");
            PayString.Append("<input name=\"branchid\" value=\"0021\" type=\"hidden\">");
            PayString.Append("<input name=\"cono\" value=\"000244\" type=\"hidden\">");
            PayString.Append(string.Format("<input name=\"date\" type=\"hidden\" value=\"{0:yyyyMMdd}\">", DateTime.Today));
            PayString.Append(string.Format("<input name=\"billno\" type=\"hidden\" value=\"{0}\">", Request.Form["CMB_PayNo"]));
            PayString.Append(string.Format("<input name=\"amount\" type=\"hidden\" value=\"{0}\">", Request.Form["P_PayNow"]));
            PayString.Append(string.Format("<input name=\"merchanturl\" type=\"hidden\" value=\"{0}\">", Convert.ToString(ConfigurationManager.AppSettings["CMBReturnUrl"])));
            PayString.Append(string.Format("<input name=\"MerchantPara\" type=\"hidden\" value=\"{0}\">", Request.Form["P_OrderId"]));
            PayString.Append("<input type=\"submit\" value=\"确认\" style=\"display:none;\">");
            PayString.Append("</form>");
            PayString.Append("");
            PayString.Append("<script>document.forms['fm'].submit();</script>");

            Response.Write(PayString.ToString());
            Response.End();
        }

        private void DoAliPay()
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //Response.Write(Request.Form["P_PayNow"]);
            //return;

            //必填参数//
            Guid ocode = System.Guid.NewGuid();
            //请与贵网站订单系统中的唯一订单号匹配
            string out_trade_no = ocode.ToString(); // Request.Form["P_OrderId"]; 
            //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string subject = Request.Form["P_LineName"] + " " + Request.Form["P_AutoId"];//P_AutoId
            //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string body = "出发日期：" + Request.Form["P_Date"] + " 人数：" + Request.Form["P_Nums"];
            //订单总金额，显示在支付宝收银台里的“应付总额”里
            string total_fee = Request.Form["P_PayNow"];

            //扩展功能参数——授权令牌//

            //授权令牌，该参数的值由快捷登录接口(alipay.auth.authorize)的页面跳转同步通知参数中获取
            string token = "";
            //注意：
            //token的有效时间为30分钟，过期后需重新执行快捷登录接口(alipay.auth.authorize)获得新的token

            //扩展功能参数——默认支付方式//

            //默认支付方式，代码见“即时到帐接口”技术文档
            string paymethod = "";
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”
            string defaultbank = "";

            if (Request.Form["paytype"] != null)
            {
                if (Request.Form["paytype"].Trim() != "")
                {
                    if (Request.Form["paytype"] == "AliPay")
                    {
                        paymethod = "directPay";
                    }
                    else
                    {
                        paymethod = "bankPay";
                        defaultbank = Request.Form["paytype"];
                    }
                }
            }

            //扩展功能参数——防钓鱼//

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
            string exter_invoke_ip = "";
            //注意：
            //请慎重选择是否开启防钓鱼功能
            //exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
            //建议使用POST方式请求数据
            //示例：
            //exter_invoke_ip = "";
            //Service aliQuery_timestamp = new Service();
            //anti_phishing_key = aliQuery_timestamp.Query_timestamp();               //获取防钓鱼时间戳函数

            //扩展功能参数——其他//

            //商品展示地址，要用http:// 格式的完整路径，不允许加?id=123这类自定义参数
            string show_url = Convert.ToString(ConfigurationManager.AppSettings["AliPayShowUrl"]); //"http://223.167.74.81:810";
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
            string extra_common_param = Request.Form["P_OrderId"];
            //默认买家支付宝账号
            string buyer_email = "";

            //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)//

            //提成类型，该值为固定值：10，不需要修改
            string royalty_type = "";
            //提成信息集
            string royalty_parameters = "";
            //注意：
            //与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
            //各分润金额的总和须小于等于total_fee
            //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
            //示例：
            //royalty_type = "10";
            //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("token", token);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            //构造即时到帐接口表单提交HTML数据，无需修改
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);

            //SaveErrorToLog(sHtmlText,"");
            Response.Write(sHtmlText);
        }
    }
}