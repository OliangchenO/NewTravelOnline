using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using Com.Alipay;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using TravelOnline.TravelMisWebService;
using TravelOnline.Class.Purchase;

namespace TravelOnline.PayMent
{
    public partial class return_notifyalipay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    string order_no_old = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态

                    string order_no = Request.Form["extra_common_param"];

                    //if (Request.Form["trade_status"] == "WAIT_BUYER_PAY")
                    //{//该判断表示买家已在支付宝交易管理中产生了交易记录，但没有付款

                    //    //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    //    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //    //如果有做过处理，不执行商户的业务程序

                    //    Response.Write("success");  //请不要修改或删除
                    //}
                    //else if (Request.Form["trade_status"] == "WAIT_SELLER_SEND_GOODS")
                    //{//该判断示买家已在支付宝交易管理中产生了交易记录且付款成功，但卖家没有发货

                    //    //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    //    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //    //如果有做过处理，不执行商户的业务程序

                    //    Response.Write("success");  //请不要修改或删除
                    //}
                    //else if (Request.Form["trade_status"] == "WAIT_BUYER_CONFIRM_GOODS")
                    //{//该判断表示卖家已经发了货，但买家还没有做确认收货的操作

                    //    //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    //    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //    //如果有做过处理，不执行商户的业务程序

                    //    Response.Write("success");  //请不要修改或删除
                    //}
                    //else if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    //{//该判断表示买家已经确认收货，这笔交易完成

                    //    //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                    //    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //    //如果有做过处理，不执行商户的业务程序

                    //    Response.Write("success");  //请不要修改或删除
                    //}
                    //else
                    //{
                    //    Response.Write("success");  //其他状态判断。
                    //}

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
                        //Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
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
                            SaveErrorToLog("支付宝异步支付成功", "trade_no=" + trade_no);
                            Response.Write("success");
                            //result
                        }
                        else
                        {
                            SaveErrorToLog("支付宝异步数据库更新错误", "trade_no=" + trade_no);
                            Response.Write("fail");
                        }
                    }
                    else
                    {
                        SaveErrorToLog("支付宝支付记录已存在", "trade_no=" + trade_no);
                        Response.Write("success");
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    SaveErrorToLog("支付宝异步验证错误错误，notify_id=", "");
                    SaveErrorToLog("notify_id=", Request.Form["notify_id"] + " sign=" + Request.Form["sign"]);
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("支付宝无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
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