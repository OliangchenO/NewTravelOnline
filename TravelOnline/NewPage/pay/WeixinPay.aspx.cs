using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.NewPage.Class;
using TravelOnline.WeiXinPay;

namespace TravelOnline.NewPage.pay
{
    public partial class WeixinPay : System.Web.UI.Page
    {
        public string LoginInfo = "", OrderId, AutoId, LineName, YeE, PayType = "", planid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                LoginInfo = "<a rel=\"nofollow\" href=\"/member/login.html\">请登陆</a><span>";
            }
            else
            {
                LoginInfo = "您好，" + Convert.ToString(Session["Online_UserName"]) + "<span>|</span><a rel=\"nofollow\" href=\"/login/logout.aspx\">退出</a>";
            }
            OrderId = Request.QueryString["orderid"];
            PayType = Request.QueryString["p"];
            YeE = Request.QueryString["amt"].Replace(".00", "");
            LoadTempOrder();
        }

        protected void LoadTempOrder()
        {
            //点击支付按钮时加判断订单状态by毛寅珅
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "2")
                {
                    Response.Write("订单状态异常，无法支付！");
                    Response.Redirect(string.Format("http://www.scyts.com/OrderView/{0}.html", OrderId), true);
                }
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                if (LineName.Length > 30)
                {
                    LineName = LineName.Substring(0, 30);
                }
                string LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
                WxPayData data = new WxPayData();
                data.SetValue("body", LineName);//商品描述
                data.SetValue("attach", "weixinnav");//附加数据
                data.SetValue("out_trade_no", AutoId + System.DateTime.Now.ToString("yyyyMMddHHmmssffff"));//商户订单号
                YeE = (Convert.ToDecimal(YeE) * 100).ToString().Replace(".00", "");
                data.SetValue("total_fee", YeE);//总金额
                data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
                //data.SetValue("goods_tag", "jjj");//商品标记
                data.SetValue("trade_type", "NATIVE");//交易类型
                data.SetValue("product_id", LineId);//商品ID

                WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
                string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
                ImgWeiXinPay.ImageUrl = "MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url);
            }
            else
            {
                Response.Write("支付参数错误，请重试！");
            }
        }
    }
}