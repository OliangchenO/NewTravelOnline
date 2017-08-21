using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
//using TravelOnline.Class.Common;

namespace TravelOnline.Travel
{
    public partial class ShowRoute : System.Web.UI.Page
    {
        public string PageContent;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Request.QueryString["Id"];
            string url = Convert.ToString(ConfigurationManager.AppSettings["PlanRouteUrl"]) + "?cid=" + Request.QueryString["Id"];
            PageContent = GetPageContent(url,"");
        }

        private readonly static int TIMEOUT = 15000;
        private CookieContainer _cookieCon = new CookieContainer();
        private CredentialCache _credentials = new CredentialCache();

        /// <summary>
        /// 通过url请求数据
        /// </summary>
        /// <param >被请求页面的url</param>
        /// <param >代理服务器</param>

        /// <returns>返回页面内容</returns>
        public string GetPageContent(string url, string proxyServer)
        {
            StringBuilder ret = new StringBuilder("");
            HttpWebResponse rsp = null;

            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                if (!string.IsNullOrEmpty(proxyServer))
                {


                    req.Proxy = new WebProxy(proxyServer);
                }
                req.CookieContainer = this._cookieCon;
                req.Headers.Add("Accept-Language: zh-cn");
                req.AllowAutoRedirect = true;
                req.Timeout = TIMEOUT;

                if (this._credentials != null)
                {
                    req.PreAuthenticate = true;
                    req.Credentials = this._credentials;
                }
                rsp = (HttpWebResponse)req.GetResponse();

                Stream rspStream = rsp.GetResponseStream();
                StreamReader sr = new StreamReader(rspStream, System.Text.Encoding.Default);

                //获取数据
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    ret.Append(read, 0, count);
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {
                ret.Append(e.Message);
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return ret.ToString();
        }


        /// <summary>
        /// 通过url请求数据(Post方法)
        /// </summary>
        /// <param >被请求页面的url</param>
        /// <param >POST的内容</param>
        /// <param >代理</param>
        /// <returns>返回页面内容</returns>
        public string GetPageContent(string url, string param, string proxyServer)
        {
            StringBuilder ret = new StringBuilder("");
            HttpWebResponse rsp = null;

            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                if (!string.IsNullOrEmpty(proxyServer))
                {
                    req.Proxy = new WebProxy(proxyServer);
                }
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Add("Accept-Language: zh-cn");
                req.CookieContainer = _cookieCon;
                req.Timeout = TIMEOUT;
                req.AllowAutoRedirect = true;
                if (_credentials != null)
                {
                    req.PreAuthenticate = true;
                    req.Credentials = _credentials;
                }

                //传入POST参数的分析
                if (param != null)
                {
                    string temp = EncodeParams(param, System.Text.Encoding.Default);
                    byte[] bytes = Encoding.UTF8.GetBytes(temp);
                    req.ContentLength = bytes.Length;
                    Stream rspStream = req.GetRequestStream();
                    rspStream.Write(bytes, 0, bytes.Length);
                    rspStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }

                //取得请求后返回的的数据
                rsp = (HttpWebResponse)(req.GetResponse());
                Stream ReceiveStream = rsp.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream, System.Text.Encoding.Default);

                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    ret.Append(read, 0, count);
                    count = sr.Read(read, 0, 256);
                }
            }
            //catch (Exception e)
            //{
            //    string err = e.ToString();
            //}
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return ret.ToString();
        }

        /// <summary>
        /// 通过传入的url请求文件数据
        /// </summary>
        /// <param >图片的URL</param>
        /// <param >代理服务器</param>
        /// <returns>图片内容</returns>
        public byte[] GetFile(string url, string proxyServer)
        {
            WebResponse rsp = null;
            byte[] retBytes = null;

            try
            {
                Uri uri = new Uri(url);
                WebRequest req = WebRequest.Create(uri);

                rsp = req.GetResponse();
                Stream stream = rsp.GetResponseStream();

                if (!string.IsNullOrEmpty(proxyServer))
                {
                    req.Proxy = new WebProxy(proxyServer);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    int b;
                    while ((b = stream.ReadByte()) != -1)
                    {
                        ms.WriteByte((byte)b);
                    }
                    retBytes = ms.ToArray();
                }
            }
            catch
            {
                retBytes = null;
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return retBytes;
        }

        private string EncodeParams(string param, Encoding enc)
        {
            StringBuilder ret = new StringBuilder();
            char[] reserved = { '?', '=', '&', '%', '+' };

            if (param != null)
            {
                int i = 0, j;
                while (i < param.Length)
                {
                    j = param.IndexOfAny(reserved, i);
                    if (j == -1)
                    {
                        ret.Append(HttpUtility.UrlEncode(param.Substring(i, param.Length - i), enc));
                        break;
                    }
                    ret.Append(HttpUtility.UrlEncode(param.Substring(i, j - i), enc));
                    ret.Append(param.Substring(j, 1));
                    i = j + 1;
                }
            }
            return ret.ToString();
        }
    }
}