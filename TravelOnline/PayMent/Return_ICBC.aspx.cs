using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using TravelOnline.Class.Purchase;
using TravelOnline.TravelMisWebService;
using System.Configuration;


namespace TravelOnline.PayMent
{
    public partial class Return_ICBC : System.Web.UI.Page
    {
        public string imgurl, infos, merVAR, notifyData, signMsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            merVAR = Request.Form["merVAR"].Trim();
            notifyData = Request.Form["notifyData"].Trim();
            signMsg = Request.Form["signMsg"].Trim();

            SaveErrorToLog("merVAR", merVAR);
            SaveErrorToLog("notifyData", notifyData);
            SaveErrorToLog("signMsg", signMsg);

            //merVAR = "85007b74-a490-423a-8f69-a1f401634105";
            //notifyData = "PD94bWwgIHZlcnNpb249IjEuMCIgZW5jb2Rpbmc9IkdCSyIgc3RhbmRhbG9uZT0ibm8iID8+PEIyQ1Jlcz48aW50ZXJmYWNlTmFtZT5JQ0JDX1BFUkJBTktfQjJDPC9pbnRlcmZhY2VOYW1lPjxpbnRlcmZhY2VWZXJzaW9uPjEuMC4wLjExPC9pbnRlcmZhY2VWZXJzaW9uPjxvcmRlckluZm8+PG9yZGVyRGF0ZT4yMDEzMDcwOTAwMzYxMzwvb3JkZXJEYXRlPjxjdXJUeXBlPjAwMTwvY3VyVHlwZT48bWVySUQ+MTAwMUVDMjM4MjA4NDQ8L21lcklEPjxzdWJPcmRlckluZm9MaXN0PjxzdWJPcmRlckluZm8+PG9yZGVyaWQ+MTE3NzMwMzY3Mjc0MTMxODwvb3JkZXJpZD48YW1vdW50PjEwPC9hbW91bnQ+PGluc3RhbGxtZW50VGltZXM+MTwvaW5zdGFsbG1lbnRUaW1lcz48bWVyQWNjdD4xMDAxMTcyOTI5MDA0NjE4ODY2PC9tZXJBY2N0Pjx0cmFuU2VyaWFsTm8+SEZHMDAwMDAzODcxNjgzNDM0PC90cmFuU2VyaWFsTm8+PC9zdWJPcmRlckluZm8+PC9zdWJPcmRlckluZm9MaXN0Pjwvb3JkZXJJbmZvPjxjdXN0b20+PHZlcmlmeUpvaW5GbGFnPjA8L3ZlcmlmeUpvaW5GbGFnPjxKb2luRmxhZz48L0pvaW5GbGFnPjxVc2VyTnVtPjwvVXNlck51bT48L2N1c3RvbT48YmFuaz48VHJhbkJhdGNoTm8+PC9UcmFuQmF0Y2hObz48bm90aWZ5RGF0ZT4yMDEzMDcwOTAwMzYxOTwvbm90aWZ5RGF0ZT48dHJhblN0YXQ+MTwvdHJhblN0YXQ+PGNvbW1lbnQ+vbvS17PJuaajoTwvY29tbWVudD48L2Jhbms+PC9CMkNSZXM+";
            //signMsg = "ZCDgwtgE56wNv+/LJrlTYPiv5XqfEuCxKzLs9rSJu09qJBO4sagADoFvXZffs8zN021KO8HUVxRYqVTiabu69Q5SKSmhiO13+z5LMUIsZ83R1yXAERs+RSpMYYjmdNUIshTvDJfDsvsmmbZ2GE1ZRVNFdhxPmM2eH8P8NwnU3AA=";

            //infosecapiLib.infosec icbc_infosec = new infosecapiLib.infosec();

            //string dec_notifyData = icbc_infosec.base64dec(notifyData);


            string UpPassWord_1 = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp_1 = new TravelOnlineService();
            rsp_1.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

            IcbcSignData SignInfo = new IcbcSignData();

            SignInfo = rsp_1.Icbc_Base64dec_Class(UpPassWord_1, notifyData);
            string dec_notifyData = SignInfo.TransData;

            //校验数据
            //int VerifyRet = icbc_infosec.verifySign(dec_notifyData, Server.MapPath("../PayMent/Icbc_Cert/ebb2cpublic.crt"), signMsg);

            //if (VerifyRet == 0)
            //{ 
            
            //}

            string payid = "", amount = "0", tranSerialNo = "", tranStat = "", total_fee="";

            XmlDocument cnpXmlDoc = new XmlDocument();
            cnpXmlDoc.LoadXml(dec_notifyData);
            XmlNode x = cnpXmlDoc.SelectSingleNode("//B2CRes");
            if (x != null)
            {
                XmlNode x1 = cnpXmlDoc.SelectSingleNode("//subOrderInfo");
                payid = x1.SelectSingleNode("orderid").InnerText;
                amount = x1.SelectSingleNode("amount").InnerText;
                tranSerialNo = x1.SelectSingleNode("tranSerialNo").InnerText;

                XmlNode x2 = cnpXmlDoc.SelectSingleNode("//bank");
                tranStat = x2.SelectSingleNode("tranStat").InnerText;
            }

            total_fee = (MyConvert.ConToDec(amount)/100).ToString();

            if (tranStat == "1")
            {
                string SqlQueryText = string.Format("select * from ICBC_Pay where OrderId='{0}' and payid='{1}' and flag='0'", merVAR, payid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    //如果有做过处理，不执行商户的业务程序
                    SqlQueryText = string.Format("select PayPrice from OL_PayMent where PayType='Icbcpay' and OrderId='{0}' and TradeNo='{1}'", merVAR, tranSerialNo);

                    DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        List<string> Sql = new List<string>();
                        Sql.Add(string.Format("UPDATE ICBC_Pay set flag='1' where OrderId='{0}' and payid='{1}'", merVAR, payid));

                        SqlQueryText = string.Format("select OrderId,ProductClass,OrderNums,OrderUser from OL_Order where ProductType='Coupon' and OrderId='{0}'", merVAR);
                        DS = new DataSet();
                        DS.Clear();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            PurchaseClass.CouponGetAfterBuy(DS.Tables[0].Rows[0]["OrderUser"].ToString(), DS.Tables[0].Rows[0]["ProductClass"].ToString(), MyConvert.ConToInt(DS.Tables[0].Rows[0]["OrderNums"].ToString()));
                        }

                        Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", merVAR));

                        string Contents = string.Format("{0} {1}", "", "");
                        Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','Icbcpay')",
                            merVAR,
                            tranSerialNo,
                            "",
                            total_fee,
                            DateTime.Now.ToString(),
                            Contents
                            )
                        );

                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，工行交易号：{3}')",
                            merVAR,
                            DateTime.Now.ToString(),
                            total_fee,
                            tranSerialNo
                            )
                        );

                        string[] SqlQuery = Sql.ToArray();
                        if (MyDataBaseComm.Transaction(SqlQuery) == true)
                        {
                            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                            TravelOnlineService rsp = new TravelOnlineService();
                            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                            PayInfo Pays = new PayInfo();
                            Pays.OrderId = merVAR;
                            Pays.TradeNo = tranSerialNo;
                            Pays.PayPrice = total_fee;
                            Pays.PayTime = DateTime.Now.ToString();
                            Pays.PayContent = Contents;

                            string Result;
                            try
                            {
                                Result = rsp.PayInfoSave(UpPassWord, Pays);
                            }
                            catch
                            {
                            }
                            SaveErrorToLog("工商银行异步支付成功", "trade_no=" + tranSerialNo);
                        }
                        else
                        {
                            SaveErrorToLog("工商银行异步数据库更新错误", "trade_no=" + tranSerialNo);
                        }
                    }
                    else
                    {
                        //已经存在支付记录
                        SaveErrorToLog("工商银行异步已支付", "trade_no=" + tranSerialNo);
                    }


                }
                else
                {
                    SaveErrorToLog("工商银行未查询到提交的支付数据", "trade_no=" + tranSerialNo);
                }
            }
            else
            {
                SaveErrorToLog("工商银行异步支付失败，校验不通过", "trade_no=" + tranSerialNo);
            }
            //Response.Write(orderid + "/" + amount + "/" + tranSerialNo + "/" + tranStat);
            //Response.Write(dec_notifyData);
            //Response.Write(VerifyRet);
            
            Response.Write("http://www.scyts.com/PayMent/icbc_payresult.aspx");
            Response.End();
        }

        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\IcbcPayLog.txt";

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

    }
}