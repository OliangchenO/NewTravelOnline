using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using TravelOnline.EncryptCode;
using TravelOnline.GetCombineKeys;
using TravelOnline.LoginUsers;
using System.Configuration;
using TravelOnline.WeiXinPay;
using System.Data;
using LitJson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TravelOnline.Member
{
    public partial class QQLogin : System.Web.UI.Page
    {
        //string access_token = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Debug("Page_Load","start!");
            //获取Code
            getCode();
            //获取AccessToken

            getToken(Request.QueryString["code"]);

            //获取OpenID
            getOpenID();
            Log.Debug("Page_Load()", "start 注册到新会员");

            GetUserInfo();
            //注册到新会员
            regedit();
            
        }

        //Step1：获取Authorization Code
        private void getCode()
        {
            //判断code是否为空，为空：首次请求
            Log.Debug("getCode()", "start!");
            Log.Debug("Request.QueryString[code]:", Request.QueryString["code"]);
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                Random RandNum = new Random();
                Session["state"] = RandNum.Next(1000, 9999).ToString() + RandNum.Next(1000, 9999).ToString();
                string GetAuthCodeurl = "";
                GetAuthCodeurl += "https://graph.qq.com/oauth2.0/authorize?";
                GetAuthCodeurl += "response_type=code" + "&";
                GetAuthCodeurl += string.Format("client_id={0}", ConfigurationManager.AppSettings["client_id"]) + "&";
                GetAuthCodeurl += string.Format("redirect_uri={0}", ConfigurationManager.AppSettings["QQCallBackUrl"]) + "&";
                GetAuthCodeurl += string.Format("state={0}", SecurityCode.Md5_Encrypt(Session["state"].ToString(), 32));
                Log.Debug("getCode()_url", GetAuthCodeurl);
                Response.Redirect(GetAuthCodeurl);
            }
            Log.Debug("getCode()", "end!");
        }

        //Step2：通过Authorization Code获取Access Token
        private void getToken(string code)
        {
            Log.Debug("getToken()", "start!");
            Log.Debug("string code",code);
            try
            {
                if (Session["access_token"] == null)
                {
                    if (Request["state"].ToString().Equals(SecurityCode.Md5_Encrypt(Session["state"].ToString(), 32)))
                    {
                        Log.Debug("Session[state]:", "true");
                        string GetAccessTokenurl = "";
                        GetAccessTokenurl += "https://graph.qq.com/oauth2.0/token?";
                        GetAccessTokenurl += "grant_type=authorization_code" + "&";
                        GetAccessTokenurl += string.Format("client_id={0}", ConfigurationManager.AppSettings["client_id"]) + "&";
                        GetAccessTokenurl += string.Format("client_secret={0}", ConfigurationManager.AppSettings["client_secret"]) + "&";
                        GetAccessTokenurl += string.Format("code={0}", code) + "&";
                        GetAccessTokenurl += string.Format("redirect_uri={0}", ConfigurationManager.AppSettings["QQCallBackUrl"]);
                        Log.Debug("getToken()_Url:", GetAccessTokenurl);
                        string response = file_get_contents(GetAccessTokenurl, Encoding.UTF8);
                        Log.Debug("getToken:response:", response);
                        NameValueCollection msg;
                        if (response.IndexOf("callback") != -1)
                        {
                            int lpos = response.IndexOf("(");
                            int rpos = response.IndexOf(")");
                            response = response.Substring(lpos + 1, rpos - lpos - 1);
                            msg = ParseJson(response);
                            Log.Debug("msg:", msg.ToString());
                            if (!string.IsNullOrEmpty(msg["error"]))
                            {
                                Response.Write("<h3>getToken,error:</h3>" + msg["error"].ToString());
                                Response.Write("<h3>msg  :</h3>" + msg["error_description"]);
                                Response.End(); return;
                            }
                        }
                        Log.Debug("getToken_response:", response);
                        NameValueCollection ps = ParseUrlParameters(response);
                        Session["access_token"] = ps["access_token"].ToString();
                        Session["state"] = null;
                        //Log.Debug("access_token:", access_token);
                        Log.Debug("getToken()", "end!");
                    }
                    else
                    {
                        Response.Write("The state does not match. You may be a victim of CSRF.request=" + Request["state"] + ",session=" + Session["state"]);
                    }
                }
                else
                {
                    Log.Debug("Session[access_token].tostring", Session["access_token"].ToString());
                    //access_token = Session["access_token"].ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        //Step3：使用Access Token来获取用户的OpenID
        private void getOpenID()
        {
            try
            {
                Log.Debug("getOpenID()", "start!");
                Log.Debug("getOpenID(string access_token)", Session["access_token"].ToString());
                if (Session["openid"] == null)
                {
                    string graph_url = "https://graph.qq.com/oauth2.0/me?access_token=" + Session["access_token"].ToString();
                    //Session["access_token"] = access_token;
                    string str = file_get_contents(graph_url, Encoding.Default);
                    if (str.IndexOf("callback") != -1)
                    {
                        int lpos = str.IndexOf("(");
                        int rpos = str.IndexOf(")");
                        str = str.Substring(lpos + 1, rpos - lpos - 1);
                    }
                    NameValueCollection user = ParseJson(str);
                    if (!string.IsNullOrEmpty(user["error"]))
                    {
                        Response.Write("<h3>OpenId,error:</h3>" + user["error"]);
                        Response.Write("<h3>msg  :</h3>" + user["error_description"]);
                        Response.End();
                    }
                    Response.Write("Hello " + user["openid"]);
                    Session["openid"] = user["openid"];
                    //Session["access_token"] = null;
                    Log.Debug("Session[openid]", Session["openid"].ToString());
                    Log.Debug("getOpenID()", "end!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void GetUserInfo()
        {
            string getuserinfo = "https://graph.qq.com/user/get_user_info?";
            getuserinfo += string.Format("oauth_consumer_key={0}&", ConfigurationManager.AppSettings["client_id"]);
            getuserinfo += string.Format("access_token={0}&", Session["access_token"].ToString());
            getuserinfo += string.Format("openid={0}", Session["openid"].ToString());
            Log.Debug("GetUserInfo()", getuserinfo);
            //Response.Redirect(getuserinfo);
            try
            {
                string response = file_get_contents(getuserinfo, Encoding.UTF8);
                //Log.Debug("GetUserInfo():", response);
                //string a =JsonMapper.ToObject(response).ToString();
                //Log.Debug("a:",a);
                
                //////NameValueCollection msg;
                //////if (response.IndexOf("ret") != -1)
                //////{
                //////    int lpos = response.IndexOf("{");
                //////    int rpos = response.IndexOf("}");
                //////    response = response.Substring(lpos + 1, rpos - lpos - 1);
                //////    Log.Debug("response:", response);
                //////    msg = ParseJson(response);
                //////    Log.Debug("msg:", msg.ToString());
                //////    if (!string.IsNullOrEmpty(msg["msg"]))
                //////    {
                //////        Response.Write("<h3>GetUserInfo(),error:</h3>" + msg["msg"].ToString());
                //////        Response.Write("<h3>msg  :</h3>" + msg["msg"]);
                //////        Response.End(); return;
                //////    }
                //////}
                JObject lineListRes = (JObject)JsonConvert.DeserializeObject(response);
                Log.Debug("nickname", lineListRes["nickname"].ToString());
                Session["nickname"] = lineListRes["nickname"].ToString();
                //NameValueCollection ps = ParseUrlParametersGetUserInfo(response);
                
                //Session["nickname"] = ps["nickname"].ToString();
            }
            catch(WebException ex)
            {
                throw ex;
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

                if (DS.Tables[0].Rows.Count > 0)
                {
                    Session["Online_UserId"] = DS.Tables[0].Rows[0]["Id"].ToString();
                    Session["Online_UserName"] = DS.Tables[0].Rows[0]["UserName"].ToString();
                    Log.Debug("regedit(): if(SqlQueryText)", "有");
                    Response.Redirect("http://www.scyts.com");
                }
                else
                {
                    Log.Debug("if(SqlQueryText)", "无");
                    Session["Online_UserId"] = Convert.ToString(CombineKeys.NewComb());
                    LoginUser.RegistUser RUser = new LoginUser.RegistUser
                    {
                        Id = Session["Online_UserId"].ToString(),
                        UserName = Session["nickname"].ToString(),
                        ThirdPartyType = "QQ",
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        

        public string file_get_contents(string url, Encoding encode)
        {
            Log.Debug("file_get_contents_url:",url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    int readc;
                    byte[] buffer = new byte[1024];
                    while ((readc = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, readc);
                    }
                }
                Log.Debug("file_get_contents","End!");
                return encode.GetString(ms.ToArray());
            }
        }

        NameValueCollection ParseJson(string json_code)
        {
            
            NameValueCollection mc = new NameValueCollection();
            Regex regex = new Regex(@"(\s*\""?([^""]*)\""?\s*\:\s*\""?([^""]*)\""?\,?)");
            json_code = json_code.Trim();
            if (json_code.StartsWith("{"))
            {
                json_code = json_code.Substring(1, json_code.Length - 2);
            }
            foreach (Match m in regex.Matches(json_code))
            {
                mc.Add(m.Groups[2].Value, m.Groups[3].Value);
                //Response.Write(m.Groups[2].Value + "=" + m.Groups[3].Value + "<br/>");
            }
            return mc;
        }

        NameValueCollection ParseUrlParameters(string str_params)
        {
            NameValueCollection nc = new NameValueCollection();
            foreach (string p in str_params.Split('&'))
            {
                string[] p_s = p.Split('=');
                nc.Add(p_s[0], p_s[1]);
            }
            return nc;
        }

        NameValueCollection ParseUrlParametersGetUserInfo(string str_params)
        {
            NameValueCollection nc = new NameValueCollection();
            foreach (string p in str_params.Split(','))
            {
                string[] p_s = p.Split(':');
                nc.Add(p_s[0], p_s[1]);
                Log.Debug("nc:",p_s[0].ToString());
                Log.Debug("nc:", p_s[1].ToString());
            }
            return nc;
        }

    }
}