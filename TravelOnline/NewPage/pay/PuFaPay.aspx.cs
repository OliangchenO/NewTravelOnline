using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Text;
using System.Data;

namespace TravelOnline.NewPage.pay
{
    public partial class PuFaPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("({\"error\":\"支付失败，请稍后再试\"})");
            //Response.End();

            string orderID = Request.Form["pf_orderid"];
            string ReqNo = System.DateTime.Now.ToString("yyyyMMdd00HHmmss");
            string OrderAmt = Request.Form["OrderAmt"];
            string StagesNum = "0";
            string CardID = Request.Form["xykh"];
            string CVV2 = Request.Form["cvv2"];
            string checkcode = Request.Form["pfcode"];
            string mobile = Request.Form["pfmobile"];

            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='PuFa' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(CardID), MyDataBaseComm.StripSQLInjection(checkcode));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    Response.Write("({\"error\":\"动态密码已过有效期，请重新发送\"})");
                    Response.End();
                }
            }
            else
            {
                Response.Write("({\"error\":\"信用卡号或动态密码错误\"})");
                Response.End();
            }

            try
            {
                //string Paymsg = Web2.Class.Common.BLL.Pay(orderID, ReqNo, OrderAmt, StagesNum, CardID, CVV2);
                //if (Web2.Class.Common.BLL.GetCardSignMsg(Paymsg) == "0000")
                //{
                //    //付款成功
                //    List<string> Sql = new List<string>();
                //    Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", orderID));

                //    Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','Alipay')",
                //        orderID,
                //        "",
                //        "",
                //        OrderAmt,
                //        DateTime.Now.ToString(),
                //        "浦发银行信用卡在线支付"
                //        )
                //    );
                //    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','浦发银行信用卡在线支付成功，付款金额：{2}')",
                //        orderID,
                //        DateTime.Now.ToString(),
                //        OrderAmt,
                //        ""
                //        )
                //    );
                //    string[] SqlQuery = Sql.ToArray();
                //    MyDataBaseComm.Transaction(SqlQuery);
                //    Response.Write("({\"success\":\"" + orderID + "\"})");
                    
                //}
                //else
                //{
                //    Response.Write("({\"error\":\"" + Web2.Class.Common.BLL.GetCardSignMsg(Paymsg) + "\"})");
                //}
            }
            catch
            {
                Response.Write("({\"error\":\"支付失败，请稍后再试\"})");
            }
            
        }
    }
}