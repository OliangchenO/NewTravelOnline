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
using TravelOnline.TravelMisWebService;

namespace TravelOnline.NewPage.pay
{
    public partial class AliPay : System.Web.UI.Page
    {
        public string OrderId, YeE, PayType = "", planid;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            PayType = Request.QueryString["p"];
            if (PayType == "" || PayType == null) PayType = "AliPay";

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
                //if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                //YeE = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString())).ToString();
                YeE = Request.QueryString["amt"].Replace(".00","");
                //planid = "," + DS.Tables[0].Rows[0]["planid"].ToString() + ",";

                //if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["PayFlag"].ToString()) == 0)
                //{
                //    if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
                //    {
                //        string ordernums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                //        decimal lijian = 2000 * MyConvert.ConToDec(ordernums);
                //        decimal pprice = MyConvert.ConToDec(YeE);
                //        if (pprice > lijian) YeE = (pprice - lijian).ToString();

                //        //decimal pprice = MyConvert.ConToDec(YeE);
                //        //if (pprice > 2000) YeE = (pprice - 2000).ToString();
                //    }
                //}
                

                ////////////////////////////////////////////请求参数////////////////////////////////////////////
                //Response.Write(Request.Form["P_PayNow"]);
                //return;

                //必填参数//
                Guid ocode = System.Guid.NewGuid();
                //请与贵网站订单系统中的唯一订单号匹配
                string out_trade_no = ocode.ToString(); // Request.Form["P_OrderId"]; 
                //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                string subject = DS.Tables[0].Rows[0]["LineName"].ToString() + " " + DS.Tables[0].Rows[0]["AutoId"].ToString();//P_AutoId
                //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
                string body = "出发日期：" + string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]) + " 人数：" + DS.Tables[0].Rows[0]["OrderNums"].ToString();
                //订单总金额，显示在支付宝收银台里的“应付总额”里
                string total_fee = YeE;

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

                if (PayType == "AliPay")
                {
                    paymethod = "directPay";
                }
                else
                {
                    paymethod = "bankPay";
                    defaultbank = PayType;
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
                string extra_common_param = OrderId;
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
            else
            {
                Response.Write("支付参数错误，请重试！");
            }
        }


    }
}