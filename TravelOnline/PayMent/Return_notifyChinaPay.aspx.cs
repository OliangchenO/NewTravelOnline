using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.TravelMisWebService;

namespace TravelOnline.PayMent
{
    public partial class Return_notifyChinaPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            string orderNum = Request.Form["MerOrderNo"];
            string merOrderNum = orderNum.Substring(10);//订单号
            string tranSerialNo = Request.Form["AcqSeqId"];              //流水号
            string merOrderAmt = (MyConvert.ConToDec(Request.Form["OrderAmt"]) / 100).ToString();            //获取总金额以分为单位
            string tranResult = Request.Form["OrderStatus"];
            DataSet DS = new DataSet();
            DS.Clear();
            string SqlQueryText = string.Format("select orderid from OL_Order where AutoID='{0}'", merOrderNum);
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count == 0)
            {
                SaveErrorToLog("中国银联数据库更新错误", "out_trade_no=" + merOrderNum);
                return;
            }
            string OrderID = DS.Tables[0].Rows[0]["OrderID"].ToString();

            if (tranResult.Equals("0000"))
            {
                SqlQueryText = string.Format("select PayPrice from OL_PayMent where PayType='ChinaPay' and OrderId='{0}' and TradeNo='{1}'", OrderID, tranSerialNo);

                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    List<string> Sql = new List<string>();
                    Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", OrderID));

                    Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','ChinaPay')",
                        OrderID,
                        tranSerialNo,
                        "",
                        merOrderAmt,
                        DateTime.Now.ToString(),
                        ""
                        )
                    );

                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，中国银联交易号：{3}')",
                        OrderID,
                        DateTime.Now.ToString(),
                        merOrderAmt,
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
                        Pays.OrderId = OrderID;
                        Pays.TradeNo = tranSerialNo;
                        Pays.PayPrice = merOrderAmt;
                        Pays.PayTime = DateTime.Now.ToString();
                        Pays.PayContent = "";

                        string Result;
                        try
                        {
                            Result = rsp.PayInfoSave(UpPassWord, Pays);
                        }
                        catch
                        {
                        }
                        SaveErrorToLog("中国银联支付成功", "trade_no=" + tranSerialNo);
                    }
                    else
                    {
                        SaveErrorToLog("中国银联支付数据库更新错误", "trade_no=" + tranSerialNo);
                    }
                }
                else
                {
                    SaveErrorToLog("中国银联支付记录已存在", "trade_no=" + tranSerialNo);
                }
            }
            else
            {
                SaveErrorToLog("中国银联支付失败！", "trade_no=" + tranSerialNo);
            }


        }

        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\PayErrorLog.txt";

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