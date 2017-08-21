using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using TravelOnline.WeiXinPay;
using System.Data;
using System.Runtime.Serialization.Json;
using LitJson;

namespace TravelOnline.WeChat.jssdk
{
    public class JsSDKTools
    {
        private string appId;
        private string appSecret;

        public JsSDKTools()
        {
            appId = WxPayConfig.APPID;
            appSecret = WxPayConfig.APPSECRET;
        }

        public JsSDKTools(string appId, string appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        //得到数据包，返回使用页面  
        public String getSignPackage()
        {
            string jsapiTicket = getJsApiTicket();
            string url = HttpContext.Current.Request.Url.ToString();
            string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
            string nonceStr = createNonceStr();


            // 这里参数的顺序要按照 key 值 ASCII 码升序排序  
            string rawstring = "jsapi_ticket=" + jsapiTicket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url + "";

            string signature = SHA1_Hash(rawstring);

            string config = "wx.config({debug: false,appId: '" + appId + "',timestamp:" + timestamp + " ,nonceStr: '" + nonceStr + "',signature: '" + signature + "',jsApiList: ['onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareQQ', 'onMenuShareWeibo', 'onMenuShareQZone']});";

            return config;
        }

        //创建随机字符串  
        private string createNonceStr()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        //得到ticket 如果文件里时间 超时则重新获取  
        private string getJsApiTicket()
        {
            //这里我从数据库读取
            int expire_time=0;
            string ticket = "";
            string SqlQueryText = string.Format("select top 1 * from JsApi_Ticket where gettime>='{0}' order by ticket_expires desc", DateTime.Now.Date);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                expire_time = int.Parse(DS.Tables[0].Rows[0]["ticket_expires"].ToString());
                ticket = DS.Tables[0].Rows[0]["jsapi_ticket"].ToString();
            }
            
            string accessToken = getAccessToken();//获取系统的全局token 
            if (expire_time < ConvertDateTimeInt(DateTime.Now))
            {
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=" + accessToken + "";
                Jsapi api = JsonConvert.DeserializeObject<Jsapi>(httpGet(url));
                ticket = api.ticket;
                if (ticket != "")
                {
                    expire_time = ConvertDateTimeInt(DateTime.Now) + 7000;
                    SqlQueryText = string.Format("insert into JsApi_Ticket (id, jsapi_ticket,ticket_expires,gettime) values ('{0}','{1}','{2}','{3}')", System.Guid.NewGuid(), ticket, expire_time, DateTime.Now.ToString());
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                }
            }
            return ticket;
        }

        //得到accesstoken 如果文件里时间 超时则重新获取  
        private string getAccessToken()
        {
            // access_token 应该全局存储与更新，以下代码以写入到文件中做示例
            string access_token = "";
            string SqlQueryText = string.Format("select top 1 * from JsApi_AccessToken where gettime>='{0}' order by token_expires desc", DateTime.Now.Date);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            AccToken readJSTicket = new AccToken();
            if (DS.Tables[0].Rows.Count > 0)
            {
                readJSTicket.access_token = DS.Tables[0].Rows[0]["access_token"].ToString();
                readJSTicket.expires_in = int.Parse(DS.Tables[0].Rows[0]["token_expires"].ToString());
            }
            
            if (readJSTicket.expires_in < ConvertDateTimeInt(DateTime.Now))
            {
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appId + "&secret=" + appSecret + "";

                AccToken iden = JsonMapper.ToObject<AccToken>(httpGet(url));

                if (iden != null)
                {
                    iden.expires_in = ConvertDateTimeInt(DateTime.Now) + 7000;
                    access_token = iden.access_token;
                    SqlQueryText = string.Format("insert into JsApi_AccessToken (id,access_token,token_expires,gettime) values ('{0}','{1}','{2}','{3}')", System.Guid.NewGuid(), iden.access_token, iden.expires_in, DateTime.Now);
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                }
            }
            else
            {
                access_token = readJSTicket.access_token;
            }
            return access_token;
        }

        //发起一个http请球，返回值  
        private string httpGet(string url)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据  
                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据  
                string pageHtml = System.Text.Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句              

                return pageHtml;
            }


            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
                return null;
            }
        }

        //SHA1哈希加密算法  
        public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }


        /// <summary>  
        /// StreamWriter写入文件方法  
        /// </summary>  
        private void StreamWriterMetod(string str, string patch)
        {
            try
            {
                FileStream fsFile = new FileStream(patch, FileMode.OpenOrCreate);
                StreamWriter swWriter = new StreamWriter(fsFile);
                swWriter.WriteLine(str);
                swWriter.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>double</returns>  
        public int ConvertDateTimeInt(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = Convert.ToInt32((time - startTime).TotalSeconds);
            return intResult;
        }
    }

}