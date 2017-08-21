using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeiXinPay;

namespace TravelOnline.WeChat
{
    public partial class WeiXinGetOpenIdaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Log.Debug("WeiXinGetOpenId_____PageLoad:","");
                Response.Write(GetOpenId());
                Response.End();
            }
        }

        public string GetOpenId()
        {
            JsApiPay jsApiPay = new JsApiPay(this);
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                jsApiPay.GetOpenidAndAccessToken();
                //获取收货地址js函数入口参数
                Log.Debug("jsApiPay.GetOpenidAndAccessToken();", "OK");
                ////wxEditAddrParam = jsApiPay.GetEditAddressParameters();
                //Log.Debug("jsApiPay.GetEditAddressParameters();;", "OK" + wxEditAddrParam);

                if (jsApiPay.openid.ToString() !=null)
                {
                    Session["openid"] = jsApiPay.openid.ToString();
                    Log.Debug("Openid:", Session["openid"].ToString());
                    return "{\"success\":0}";
                }
                else
                {
                    return "{\"error\":\"获取OpenID失败\"}";
                }

            }
            catch (Exception ex)
            {
                Log.Error("ProductPage.aspx.cs--->GetOpenId()", "ex.msg:" + ex.Message + "ex.tostring:" + ex.ToString());
                return "<span style='color:#FF0000;font-size:20px'>" + "页面加载出错!，请重试" + "</span>";
            }
            
                      
        }
    }
}