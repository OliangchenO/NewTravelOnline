using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using TravelOnline.WeiXinPay;
using LitJson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using TravelOnline.LoginUsers;

using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using TravelOnline.EncryptCode;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.Member
{
    public partial class WeiXinLogin : System.Web.UI.Page
    {        
        private Page page { get; set; }

        /// <summary>
        /// access_token用于获取收货地址js函数入口参数
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        public static string wxEditAddrParam { get; set; }//获取地址信息

        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Debug("Page_load:请求地址：", Request.QueryString.ToString());
            //Session["openid"] = "o8C-Njq7Sj29gQ2Xi9hSsoIlsXtk";
            if (Session["openid"] == null)
            {
                Log.Debug("Session[openid]=", "null");
                GetOpenID();
            }
            regedit();
        }

        private void GetOpenID()
        {
            JsApiPay jsApiPay = new JsApiPay(this);
            try
            {
                //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                GetOpenidAndAccessToken();
            }
            catch (Exception ex)
            {
                Log.Error("ProductPage.aspx.cs--->Page_Load()", "ex.msg:" + ex.Message + "ex.tostring:" + ex.ToString());
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错!，请返回页面并重试！" + "</span>");
            }
        }

        public void GetOpenidAndAccessToken()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                Log.Debug("Request.QueryString[code]","!=null");
                //获取code码，以获取openid和access_token
                string code = Request.QueryString["code"];
                Log.Debug("GetOpenidAndAccessTokenFromCode(code)", "start!");
                GetOpenidAndAccessTokenFromCode(code);
            }
            else
            {
                Log.Debug("Request.QueryString[code]", "=null");
                string host = Request.Url.Host;
                string path = Request.Path;
                string redirect_uri = HttpUtility.UrlEncode("http://" + host + path);
                //string redirect_uri = HttpUtility.UrlEncode("http://" + "test.scyts.com" + path);
                WxPayData data = new WxPayData();
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type", "code");
                data.SetValue("scope", "snsapi_login,snsapi_base,snsapi_userinfo");//
                data.SetValue("state","STATE" + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/qrconnect?" + data.ToUrl();
                Log.Debug("url:", url);
                try
                {
                    //ImgWeiXinPay.ImageUrl = "../NewPage/pay/MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url);
                    //触发微信返回code码
                    Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                }
                catch
                {

                }
            }
        }

        public void GetOpenidAndAccessTokenFromCode(string code)
        {
            try
            {
                //构造获取openid及access_token的url
                WxPayData data = new WxPayData();
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("secret", WxPayConfig.APPSECRET);
                data.SetValue("code", code);
                data.SetValue("grant_type", "authorization_code");
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();

                //请求url以获取数据
                string result = HttpService.Get(url);
                Log.Debug("result", result);
                //保存access_token，用于收货地址获取
                JsonData jd = JsonMapper.ToObject(result);
                access_token = (string)jd["access_token"];
                //获取用户openid
                openid = (string)jd["openid"];
                Session["openid"] = openid;
                Log.Debug("openid", openid);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
                throw new WxPayException(ex.ToString());
            }
        }

        public void Getaccess_token()
        {
            
                WxPayData data = new WxPayData();
                data.SetValue("grant_type", "client_credential");
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("secret", WxPayConfig.APPSECRET);
                string url = "https://api.weixin.qq.com/cgi-bin/token?" + data.ToUrl();
                Log.Debug("GetName()url:", url);
                //请求url以获取数据
                string result = HttpService.Get(url);
                Log.Debug("result", result);
                //保存access_token，用于收货地址获取
                JsonData jd = JsonMapper.ToObject(result);
                Session["access_token"] = (string)jd["access_token"];

                //获取用户openid
            
        }

        public void GetWeiXinUserInfo()
        {
            if (Session["UserName"] == null)
            {

                WxPayData data = new WxPayData();
                data.SetValue("access_token", Session["access_token"]);
                data.SetValue("openid", Session["openid"]);
                string url = "https://api.weixin.qq.com/cgi-bin/user/info?" + data.ToUrl();
                Log.Debug("GetWeiXinUserInfo()url:", url);
                //请求url以获取数据
                string result = HttpService.Get(url);
                Log.Debug("result", result);
                //保存access_token，用于收货地址获取
                JsonData jd = JsonMapper.ToObject(result);
                Session["UserName"] = (string)jd["nickname"];

                //获取用户openid
            }
            
        }

        private void regedit()
        {
            Log.Debug("regedit()", "start!");
            try
            {
                string SqlQueryText = string.Format("select top 1 ID,ThirdPartyID,UserName  from OL_LoginUser where ThirdPartyID='{0}'", Session["openid"]);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Log.Debug("SqlQueryText:", SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Log.Debug("有", SqlQueryText);
                    Session["Online_UserId"] = DS.Tables[0].Rows[0]["Id"].ToString();
                    Session["Online_UserName"] = DS.Tables[0].Rows[0]["UserName"].ToString();
                    Log.Debug("regedit(): if(SqlQueryText)", "有" + DS.Tables[0].Rows[0]["ThirdPartyID"].ToString());
                    Response.Redirect("http://www.scyts.com");
                }
                else
                {
                    Getaccess_token();
                    GetWeiXinUserInfo();
                    Log.Debug("if(SqlQueryText)", "无");
                    Session["Online_UserId"] = Convert.ToString(CombineKeys.NewComb());
                    LoginUser.RegistUser RUser = new LoginUser.RegistUser
                    {
                        Id = Session["Online_UserId"].ToString(),
                        UserName = Session["UserName"].ToString(),
                        ThirdPartyType = "WeiXin",
                        ThirdPartyID = Session["openid"].ToString()
                    };
                    if (LoginUser.LoginUser_Sql(RUser, "Regist") != true)
                    {
                        Log.Debug("注册失败", "注册失败");
                        Response.Write("({\"error\":\"<i></i>注册失败\"})");
                        Response.End();
                    }
                    else
                    {
                        Log.Debug("注册成功", "注册成功，跳转");
                        Response.Redirect("/users/userinfo.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                Log.Debug("EX:",ex.Message);
            }
        }
    }
}