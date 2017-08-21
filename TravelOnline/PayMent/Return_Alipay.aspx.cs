using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using Com.Alipay;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.IO;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using TravelOnline.Class.Purchase;

namespace TravelOnline.PayMent
{
    public partial class Return_Alipay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    string order_no_old = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];      //交易状态

                    string order_no = Request.QueryString["extra_common_param"];
                    
                    //SaveErrorToLog("同步参数传递", "order_no=" + order_no + " out_trade_no=" + order_no_old);
                    //string url1 = string.Format("/Pay/Success/{0}.html", order_no);
                    //Response.Redirect(url1, true);

                    //string extra_common_param
                    
                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        string SqlQueryText = string.Format("select PayPrice from OL_PayMent where PayType='Alipay' and OrderId='{0}' and TradeNo='{1}'", order_no, trade_no);

                        DataSet DS = new DataSet();
                        DS.Clear();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count == 0)
                        {

                            SqlQueryText = string.Format("select OrderId,ProductClass,OrderNums,OrderUser from OL_Order where ProductType='Coupon' and OrderId='{0}'", order_no);
                            DS = new DataSet();
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(SqlQueryText);
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                PurchaseClass.CouponGetAfterBuy(DS.Tables[0].Rows[0]["OrderUser"].ToString(), DS.Tables[0].Rows[0]["ProductClass"].ToString(), MyConvert.ConToInt(DS.Tables[0].Rows[0]["OrderNums"].ToString()));
                            }

                            List<string> Sql = new List<string>();
                            Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", order_no));

                            string Contents = string.Format("{0} {1}", subject, body);
                            Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','Alipay')",
                                order_no,
                                trade_no,
                                buyer_email,
                                total_fee,
                                DateTime.Now.ToString(),
                                Contents
                                )
                            );

                            Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，支付宝交易号：{3}')",
                                order_no,
                                DateTime.Now.ToString(),
                                total_fee,
                                trade_no
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
                                Pays.TradeNo = trade_no;
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
                                //string url = string.Format("/Pay/Success/{0}.html", order_no);
                                //string url = string.Format("/fourthstep.html?orderid={0}", order_no);
                                string url = string.Format("/fifthstep.html?orderid={0}", order_no);
                                Response.Redirect(url, true);
                                //result
                            }
                            else
                            {
                                SaveErrorToLog("支付宝数据库更新错误", "trade_no=" + trade_no);
                                //string url = string.Format("/Pay/DbError/{0}.html", order_no);
                                string url = string.Format("/fourthstep.html?error=101&orderid={0}", order_no);
                                Response.Redirect(url, true);
                            }

                        }
                        else
                        {
                            //string url = string.Format("/Pay/Success/{0}.html", order_no);
                            string url = string.Format("/fifthstep.html?orderid={0}", order_no);
                            Response.Redirect(url, true);
                        }
                    }
                    else
                    {
                        //Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                        SaveErrorToLog("支付宝状态错误", "trade_status=" + Request.QueryString["trade_status"]);
                        //string url = string.Format("/Pay/StatusError/{0}.html", order_no);
                        //string url = string.Format("/fourthstep.html?orderid={0}", order_no);
                        string url = string.Format("/fourthstep.html?error=102&orderid={0}", order_no);
                        Response.Redirect(url, true);
                    }

                    //Response.Write("验证成功<br />");
                    //Response.Write("trade_no=" + trade_no);
                    SaveErrorToLog("支付宝验证成功", "trade_no=" + trade_no);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    SaveErrorToLog("支付宝验证失败", "");
                    SaveErrorToLog("notify_id=", Request.QueryString["notify_id"] + " sign=" + Request.QueryString["sign"]);
                    //
                    //string url = "/Pay/VerifyError/00000.html"; //"/Pay/PayResult.aspx?Result=VerifyError";
                    //string url = string.Format("/fourthstep.html", "");
                    string url = string.Format("/fourthstep.html?error={0}", "103");
                    Response.Redirect(url, true);
                    //Response.Write("验证失败");
                }
            }
            else
            {
                //Response.Write("无返回参数");
                SaveErrorToLog("支付宝无返回参数", "");
                //string url = "/Pay/ParaError/00000.html"; //string url = "/Pay/PayResult.aspx?Result=ParaError";
                string url = string.Format("/fourthstep.html?error={0}", "104");
                Response.Redirect(url, true);
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
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