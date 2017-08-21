using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using System.Data;
using System.IO;
using System.Text;

namespace TravelOnline.WeiXinPay
{
    public class ResultNotify:Notify
    {
        public ResultNotify(Page page)
            : base(page)
        {
        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();

            //检查支付结果中transaction_id是否存在
            Log.Debug("",notifyData.IsSet("transaction_id").ToString());
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            //更新订单库存支付金额
            Log.Debug("start UpdateOrder()", "go");
            UpdateOrder(notifyData);
            Log.Debug("start UpdateOrder()", "OK");
            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {

                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            Log.Debug("QueryOrder--->Req:", res.ToString());
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateOrder(WxPayData info)
        {
            string total_fee = (Convert.ToDecimal(info.GetValue("total_fee").ToString())/100).ToString();
            Log.Debug("total_fee:", total_fee);
            string out_trade_no = info.GetValue("out_trade_no").ToString();
            out_trade_no = out_trade_no.Substring(0,out_trade_no.Length - 18);
            Log.Debug("out_trade_no:", out_trade_no);
            string SqlQueryText = string.Format("select orderid from OL_Order where AutoID='{0}'", out_trade_no);
            string attach = info.GetValue("attach").ToString();
            string payType = "微信支付";
            if (attach != null)
            {
                if (attach.Equals("weixinwap"))
                {
                    payType = "微信公众号支付";
                }
                else if (attach.Equals("weixinnav"))
                {
                    payType = "微信扫码支付";
                }
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count == 0)
            {
                SaveErrorToLog("微信异步数据库更新错误", "out_trade_no=" + out_trade_no);
                return;
            }
            string OrderID = DS.Tables[0].Rows[0]["OrderID"].ToString();


            List<string> Sql = new List<string>();
            Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where AutoID='{0}'", out_trade_no));

            Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','WeiXinPay')",
                OrderID,
                info.GetValue("transaction_id").ToString(),
                "",
                total_fee,
                DateTime.Now.ToString(),
                payType
                )
            );
            Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{4}，流水号：{3}，付款金额：{2}')",
                OrderID,
                DateTime.Now.ToString(),
                total_fee,
                info.GetValue("transaction_id").ToString(),
                payType
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
                Pays.TradeNo = info.GetValue("transaction_id").ToString();
                Pays.PayPrice = total_fee;
                Pays.PayTime = DateTime.Now.ToString();
                Pays.PayContent = payType;

                string Result;
                try
                {
                    Result = rsp.PayInfoSave(UpPassWord, Pays);
                }
                catch
                {
                }
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