using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using System.IO;
using System.Text;

namespace TravelOnline.PayMent
{
    public partial class CCBPayResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            string orderNum = Request.QueryString["ORDERID"];
            string merOrderNum = orderNum.Substring(10);//订单号
            string PAYMENT = Request.QueryString["PAYMENT"];            //获取支付金额
            string tranResult = Request.QueryString["SUCCESS"];     //支付成功标志Y,N
            string INSTALLNUM = "0";
            try
            {
                INSTALLNUM = Request.QueryString["INSTALLNUM"];
            }catch(Exception){
            }

            DataSet DS = new DataSet();
            DS.Clear();
            string SqlQueryText = string.Format("select orderid from OL_Order where AutoID='{0}'", merOrderNum);
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count == 0)
            {
                SaveErrorToLog("建设银行数据库更新错误", "out_trade_no=" + merOrderNum);
                return;
            }
            string OrderID = DS.Tables[0].Rows[0]["OrderID"].ToString();


            if (tranResult.Equals("Y"))
            {
                SqlQueryText = string.Format("select PayPrice from OL_PayMent where PayType='CCB' and OrderId='{0}' and TradeNo='{1}'", OrderID, orderNum);

                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    List<string> Sql = new List<string>();
                    Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", OrderID));

                    Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','CCB')",
                        OrderID,
                        orderNum,
                        "",
                        PAYMENT,
                        DateTime.Now.ToString(),
                        ""
                        )
                    );
                    if ("0".Equals(INSTALLNUM) || null==INSTALLNUM)
                    {
                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，建设银行交易号：{3}')",
                            OrderID,
                            DateTime.Now.ToString(),
                            PAYMENT,
                            orderNum
                            )
                        );
                    }
                    else
                    {
                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，建设银行分期支付交易号：{3}')",
                            OrderID,
                            DateTime.Now.ToString(),
                            PAYMENT,
                            orderNum
                            )
                        );
                    }
                    

                    string[] SqlQuery = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuery) == true)
                    {
                        string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                        TravelOnlineService rsp = new TravelOnlineService();
                        rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                        PayInfo Pays = new PayInfo();
                        Pays.OrderId = OrderID;
                        Pays.TradeNo = orderNum;
                        Pays.PayPrice = PAYMENT;
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
                        SaveErrorToLog("建设银行支付成功", "trade_no=" + orderNum);
                        string url = string.Format("/fifthstep.html?orderid={0}", OrderID);
                        Response.Redirect(url, true);
                    }
                    else
                    {
                        SaveErrorToLog("建设银行支付数据库更新错误", "trade_no=" + orderNum);
                        string url = string.Format("/fourthstep.html?error=101&orderid={0}", OrderID);
                        Response.Redirect(url, true);
                    }
                }
                else
                {
                    SaveErrorToLog("建设银行支付记录已存在", "trade_no=" + orderNum);
                    string url = string.Format("/fifthstep.html?orderid={0}", OrderID);
                    Response.Redirect(url, true);
                }
            }
            else
            {
                SaveErrorToLog("建设银行支付状态态错误！", "trade_status=" + tranResult);
                SaveErrorToLog("建设银行支付失败！", "trade_no=" + orderNum);
                string url = string.Format("/fourthstep.html?error=102&orderid={0}", merOrderNum);
                Response.Redirect(url, true);
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