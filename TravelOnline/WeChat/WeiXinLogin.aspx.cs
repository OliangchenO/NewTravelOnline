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
using TravelOnline.WeChat.jssdk;

namespace TravelOnline.WeChat
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
            if (Session["openid"] == null)
            {
                Log.Debug("Session[openid]=", "null");
                GetOpenID();
            }
            string state = Request.QueryString["state"];
            if ("Fx_regedit".Equals(state))
            {
                fxRegedit();
            }
            else if ("Wx_regedit".Equals(state))
            {
                regedit();
            }
            
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
                Log.Debug("Request.QueryString[code]", "!=null");
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
                //string redirect_uri = HttpUtility.UrlEncode("http://www.scyts.com/WeChat/main_fx.aspx");
                WxPayData data = new WxPayData();
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type", "code");
                data.SetValue("scope", "snsapi_login,snsapi_base,snsapi_userinfo");//
                data.SetValue("state", Request.QueryString["state"] + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                Log.Debug("url:", url);
                try
                {
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
                Session["refresh_token"] = (string)jd["refresh_token"];
                Log.Debug("refresh_token", (string)jd["refresh_token"]);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
                throw new WxPayException(ex.ToString());
            }
        }

        public void RefreshAccess_token()
        {

            WxPayData data = new WxPayData();
            
            data.SetValue("appid", WxPayConfig.APPID);
            data.SetValue("grant_type", "refresh_token");
            data.SetValue("refresh_token", Convert.ToString(Session["refresh_token"]));
            string url = "https://api.weixin.qq.com/sns/oauth2/refresh_token?" + data.ToUrl();
            Log.Debug("GetName()url:", url);
            //请求url以获取数据
            string result = HttpService.Get(url);
            Log.Debug("result", result);
            //保存access_token，用于收货地址获取
            try
            {
                JsonData jd = JsonMapper.ToObject(result);
                Session["access_token"] = (string)jd["access_token"];
                Session["openid"] = (string)jd["openid"];
            }catch(Exception){

            }
            
            //获取用户openid

        }

        public Userinfo GetWeiXinUserInfo()
        {
            Userinfo userinfo = null;
            WxPayData data = new WxPayData();
            data.SetValue("access_token", Session["access_token"]);
            data.SetValue("openid", Session["openid"]);
            data.SetValue("lang", "zh_CN");
            string url = "https://api.weixin.qq.com/sns/userinfo?" + data.ToUrl();
            Log.Debug("GetWeiXinUserInfo()url:", url);
            //请求url以获取数据
            string result = HttpService.Get(url);
            Log.Debug("result", result);
            try
            {
                userinfo = JsonConvert.DeserializeObject<Userinfo>(result);
            }
            catch (Exception)
            {

            }    
            return userinfo;
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
                    //string loginstep = Convert.ToString(Request.Cookies["loginstep"]);
                    string url = Request.Url.ToString();
                    if (Convert.ToString(HttpContext.Current.Session["OrderUid"]).Length > 0)
                    {
                        Response.Redirect("/WeChat/order.aspx#first");
                    }
                    else
                    {
                        Response.Redirect("/WeChat/order.aspx#member");
                    }
                    
                }
                else
                {
                    RefreshAccess_token();
                    Userinfo userinfo = GetWeiXinUserInfo();
                    Log.Debug("if(SqlQueryText)", "无");
                    Session["Online_UserId"] = Convert.ToString(CombineKeys.NewComb());
                    Session["Online_UserName"] = userinfo.nickname;
                    int sex = 1;
                    if(userinfo.sex=="2"){
                        sex = 0;
                    }
                    LoginUser.RegistUser RUser = new LoginUser.RegistUser
                    {
                        Id = Session["Online_UserId"].ToString(),
                        UserName = userinfo.nickname,
                        ThirdPartyType = "WeiXin",
                        ThirdPartyID = userinfo.openid,
                        Sex = sex
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
                        if (Convert.ToString(HttpContext.Current.Session["OrderUid"]).Length > 0)
                        {
                            Response.Redirect("/WeChat/order.aspx#first");
                        }
                        else
                        {
                            Response.Redirect("/WeChat/order.aspx#member");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug("EX:", ex.Message.ToString());
                throw ex;
            }
        }

        private void fxRegedit()
        {
            Log.Debug("fxRegedit()", "start!");
            try
            {
                LoginUser.RegistUser RUser = new LoginUser.RegistUser();
                string SqlQueryText = string.Format("(ThirdPartyID='{0}' and ThirdPartyType='WeiXinFx')", Session["openid"]);
                RUser = LoginUser.LoginFxUser(SqlQueryText);
                if (RUser != null)
                {
                    Session["Fx_UserId"] = RUser.Id;
                    Session["Fx_Mobile"] = RUser.Mobile;
                    Session["Fx_UserEmail"] = RUser.UserEmail;
                    Session["Fx_UserName"] = RUser.UserName;
                    Session["Fx_Wxid"] = RUser.Wxid;
                    Session["Fx_Storename"] = RUser.Storename;
                    Session["Fx_Tel"] = RUser.Tel;
                    Session["Fx_Address"] = RUser.Address;
                    Session["Fx_Login"] = "Y";
                    Session["Fx_Headimgurl"] = RUser.headimgurl;
                    Session["ThirdPartyID"] = RUser.ThirdPartyID;
                    Session["Fx_wxLogin"] = "Y";
                    SqlQueryText = string.Format("UPDATE OL_FXLoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}' where id='{0}'", RUser.Id, DateTime.Now.ToString());
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                    Response.Redirect("/WeChat/main_fx.aspx");
                }
                else
                {
                    RefreshAccess_token();
                    Userinfo userinfo = GetWeiXinUserInfo();
                    Log.Debug("if(SqlQueryText)", "无");
                    int sex = 1;
                    if (userinfo.sex == "2")
                    {
                        sex = 0;
                    }
                    RUser = new LoginUser.RegistUser
                    {
                        Id = Convert.ToString(CombineKeys.NewComb()),
                        UserName = userinfo.nickname,
                        ThirdPartyType = "WeiXinFx",
                        ThirdPartyID = userinfo.openid,
                        Address = userinfo.country + userinfo.province + userinfo.city,
                        Sex = sex,
                        Storename = userinfo.nickname+"的店铺",
                        Wxid = userinfo.nickname,
                        headimgurl = userinfo.headimgurl
                    };
                    if (LoginUser.LoginUser_Sql(RUser, "FxRegist") != true)
                    {
                        Log.Debug("注册失败", "注册失败");
                        Response.Write("({\"error\":\"<i></i>注册失败\"})");
                        Response.End();
                    }
                    else
                    {
                        Log.Debug("注册成功", "注册成功，跳转");
                        Session["Fx_UserId"] = RUser.Id;
                        Session["Fx_Mobile"] = RUser.Mobile;
                        Session["Fx_UserEmail"] = RUser.UserEmail;
                        Session["Fx_UserName"] = RUser.UserName;
                        Session["Fx_Wxid"] = RUser.Wxid;
                        Session["Fx_Storename"] = RUser.Storename;
                        Session["Fx_Tel"] = RUser.Tel;
                        Session["Fx_Address"] = RUser.Address;
                        Session["Fx_Login"] = "Y";
                        Session["Fx_Headimgurl"] = RUser.headimgurl;
                        Session["ThirdPartyID"] = RUser.ThirdPartyID;
                        Session["Fx_wxLogin"] = "Y";
                        Response.Redirect("/WeChat/Fx_editinfo.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Debug("EX:", ex.Message.ToString());
            }
        }
    }
}