using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Threading;
using LitJson;
using System.Web.Security;
using TravelOnline.WeiXinPay;


namespace TravelOnline.WeChat
{
    public partial class JsApiPayPage : System.Web.UI.Page
    {
        public static string wxJsApiParam {get;set;} //H5调起JS API参数
        public static string wxEditAddrParam { get; set; }//获取地址信息
        public static string total_fee { get; set; }//参数金额

        public static string OrderID { get; set; }//guid订单号

        public static string out_trade_no { get; set; }//订单号6位数

        public static string body { get; set; }//商品信息

        public static string openid { get; set; }//商品信息

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["out_trade_no"] != null)
                {
                    Session["out_trade_no"] = Request.QueryString["out_trade_no"];
                }
                if (Request.QueryString["total_fee"] != null)
                {
                    Session["total_fee"] = Request.QueryString["total_fee"];
                }
                if (Request.QueryString["body"] != null)
                {
                    Session["body"] = Request.QueryString["body"];
                }
                if (Request.QueryString["OrderID"] != null)
                {
                    Session["OrderID"] = Request.QueryString["OrderID"];
                }
                if (Session["openid"] == null)
                {
                    GetOpenID();
                }
                if (Session["openid"] == null)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面异常请返回首页！" + "</span>");
                    return;
                }
                JsApiPay jsApiPay = new JsApiPay(this);
                string openid = Session["openid"].ToString();

                OrderID = Session["OrderID"].ToString();
                total_fee = Session["total_fee"].ToString();
                jsApiPay.out_trade_no = Session["out_trade_no"].ToString() + System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                jsApiPay.body = Session["body"].ToString();
                //检测是否给当前页面传递了相关参数

                if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");
                    Log.Error(this.GetType().ToString(), "This page have not get params, cannot be inited, exit...");
                    return;
                }
                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数

                
                jsApiPay.openid = openid;
                jsApiPay.total_fee = int.Parse(total_fee);

                //JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();//处理提交字段及加密字段
                    
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数 
                    Log.Debug(this.GetType().ToString(), "wxJsApiParam : " + wxJsApiParam);
                    //在页面上显示订单信息
                    Log.Debug("detail", WeChatOrder.OrderDetail(OrderID));
                    Response.Write("<head><title>订单详情</title><link href='/assets/plugins/font-awesome/css/font-awesome.min.css' rel='stylesheet' type='text/css'><link href='/assets/plugins/bootstrap/css/bootstrap.css' rel='stylesheet' type='text/css'><link href='/assets/css/style-metronic.css' rel='stylesheet' type='text/css'><link href='/assets/css/style.css' rel='stylesheet' type='text/css'>");
                    Response.Write("<link href='/assets/css/style-responsive.css' rel='stylesheet' type='text/css'><link href='/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css' rel='stylesheet' type='text/css'>");
                    Response.Write("<link href='/assets/plugins/iCheck/skins/square/grey.css' rel='stylesheet' type='text/css'><link href='/app_css/custom.css' rel='stylesheet'><link rel='shortcut icon' href='~/favicon.ico'></head>");

                    Response.Write("<div id='header' class='pre-header' style='position: fixed; top: 0px; left: 0px;width:101%'><div class='container'><div class='row'>");
                    Response.Write("<a class='icon_back' href='javascript:;' onclick='javascript:history.go(-1)'><i class='fa fa-reply'></i></a><div class='tit' id='titlename'></div><a class='icon_home' href='/app/main'><i class='fa fa-home'></i></a></div></div></div>");
                    
                    
                    Response.Write(WeChatOrder.OrderDetail(OrderID)); 
                    //Response.Write("<script type='text/javascript'>callpay();</script>");
                }
                catch(Exception ex)
                {
                    Log.Error("JsApiPage.aspx.cs--->JsAPI支付预处理错误：", ex.ToString());
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试哦！" + "</span>");
                }
            }
        }

        private void GetOpenID()
        {
            JsApiPay jsApiPay = new JsApiPay(this);
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                Log.Info("GetOpenidAndAccessToken", "Go");
                jsApiPay.GetOpenidAndAccessToken();
                Log.Info("GetOpenidAndAccessToken", "OK");
                //获取收货地址js函数入口参数
                Log.Info("GetEditAddressParameters", "Go");
                wxEditAddrParam = jsApiPay.GetEditAddressParameters();
                Log.Info("GetEditAddressParameters", "OK");

                Log.Info("jsApiPay.openid", "Go");
                Session["openid"] = jsApiPay.openid;
                Log.Info("jsApiPay.openid", "OK");


            }
            catch (Exception ex)
            {
                Log.Error("ProductPage.aspx.cs--->Page_Load()", "ex.msg:" + ex.Message + "ex.tostring:" + ex.ToString());
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错!，请返回页面并重试！" + "</span>");
            }
        }
    }
}