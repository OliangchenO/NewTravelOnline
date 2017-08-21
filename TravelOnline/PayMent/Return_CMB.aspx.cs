using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.IO;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using TravelOnline.Class.Common;

namespace TravelOnline.PayMent
{
    public partial class Return_CMB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            string Succeed = Request.QueryString["Succeed"];
            string CoNo = Request.QueryString["CoNo"];
            string BillNo = Request.QueryString["BillNo"];              //交易号
            string Amount = Request.QueryString["Amount"];            //获取总金额
            string Date = Request.QueryString["Date"];
            string MerchantPara = Request.QueryString["MerchantPara"]; 
            string Msg = Request.QueryString["Msg"];
            string Signature = Request.QueryString["Signature"];

            string order_no = Request.QueryString["MerchantPara"]; //订单号

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\public.key";
            string CheckString = string.Format("Succeed={0}&CoNo={1}&BillNo={2}&Amount={3}&Date={4}&MerchantPara={5}&Msg={6}&Signature={7}", Succeed, CoNo, BillNo, Amount, Date, MerchantPara, Msg, Signature);
            short CheckResult = CMBC_DLL.CheckInfoFromBank(FilePath, CheckString);

            if (CheckResult == 0)
            {
                string SqlQueryText = string.Format("select PayPrice from OL_PayMent where PayType='CMB' and OrderId='{0}' and TradeNo='{1}'", order_no, BillNo);

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    List<string> Sql = new List<string>();
                    Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", order_no));

                    Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','CMB')",
                        order_no,
                        BillNo,
                        "",
                        Amount,
                        DateTime.Now.ToString(),
                        ""
                        )
                    );

                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，招商银行交易号：{3}')",
                        order_no,
                        DateTime.Now.ToString(),
                        Amount,
                        BillNo
                        )
                    );

                    string[] SqlQuery = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuery) == true)
                    {
                        string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                        TravelOnlineService rsp = new TravelOnlineService();
                        rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                        PayInfo Pays = new PayInfo();
                        Pays.OrderId = order_no;
                        Pays.TradeNo = BillNo;
                        Pays.PayPrice = Amount;
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
                        SaveErrorToLog("招商银行支付成功", "trade_no=" + BillNo);
                        string url = string.Format("/Pay/Success/{0}.html", order_no);
                        Response.Redirect(url, true);
                        //Response.Write("success");
                        //result
                    }
                    else
                    {
                        SaveErrorToLog("招商银行支付数据库更新错误", "trade_no=" + BillNo);
                        Response.Write("招商银行支付数据库更新错误，支付失败！");
                    }
                }
                else
                {
                    SaveErrorToLog("招商银行支付记录已存在", "trade_no=" + BillNo);
                    Response.Write("招商银行支付记录已存在，支付失败！");
                }
            }
            else
            {
                SaveErrorToLog("招商银行支付返回数据校验错误", "trade_no=" + BillNo);
                Response.Write("招商银行支付返回数据校验错误，支付失败！");
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