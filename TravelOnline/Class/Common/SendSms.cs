using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TravelOnline.com._106dcx.www;
using TravelOnline.cn.ztsms.www;
using System.Configuration;
using System.Text;
using TravelOnline.GetCombineKeys;
using System.Xml;

namespace TravelOnline.Class.Common
{
    public class SendSms
    {

        public static string SendSMS_106dcx(string cellPhone, string smsContent)
        {
            Guid ucode = CombineKeys.NewComb();

            string username = Convert.ToString(ConfigurationManager.AppSettings["106dcx_username"]);
            string password = Convert.ToString(ConfigurationManager.AppSettings["106dcx_password"]);
            ExtendedCodeServiceService rsp = new ExtendedCodeServiceService();

            //OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
            string result = rsp.massSend(username, password, smsContent, cellPhone,"","",ucode.ToString(),"");
            XmlDocument cnpXmlDoc = new XmlDocument();
            cnpXmlDoc.LoadXml(result);

            XmlNode x = cnpXmlDoc.SelectSingleNode("//xmlroot");

            if (x != null)
            {
                XmlNode x1 = cnpXmlDoc.SelectSingleNode("//item");
                if (x1.SelectSingleNode("replyCode").InnerText == "001")
                {
                    return "ok";
                }
                else

                {
                    return x1.SelectSingleNode("replyDescription").InnerText;
                }
            }
            else
            {
                return "发送失败，系统内部错误！";
            }
            //<xmlroot><msg><item><replyDescription>密码错误：密码错误</replyDescription><replyCode>006</replyCode></item></msg></xmlroot>
            
        }

        public static string SendSMS_ztsms(string cellPhone, string smsContent)
        {
            Guid ucode = CombineKeys.NewComb();

            string username = Convert.ToString(ConfigurationManager.AppSettings["ztsms_username"]);
            string password = Convert.ToString(ConfigurationManager.AppSettings["ztsms_password"]);
            string productid = Convert.ToString(ConfigurationManager.AppSettings["ztsms_id"]);
            SendsmsWebServiceImplService rsp = new SendsmsWebServiceImplService();

            //OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
            //sendSms(String username,String password,String oldMobile,String content,String product_number,String dstime,String xh,String repeat)
            string result = rsp.sendSms(username, password, cellPhone, smsContent, productid, "", "", "1");

            if (result.IndexOf("1,") > -1)
            {
                return "ok";
            }
            else
            {
                return "发送失败，错误信息：" + result;
            }
            //XmlDocument cnpXmlDoc = new XmlDocument();
            //cnpXmlDoc.LoadXml(result);
            //XmlNode x = cnpXmlDoc.SelectSingleNode("//xmlroot");
            //if (x != null)
            //{
            //    XmlNode x1 = cnpXmlDoc.SelectSingleNode("//item");
            //    if (x1.SelectSingleNode("replyCode").InnerText == "001")
            //    {
            //        return "ok";
            //    }
            //    else
            //    {
            //        return x1.SelectSingleNode("replyDescription").InnerText;
            //    }
            //}
            //else
            //{
            //    return "发送失败，系统内部错误！";
            //}
        }
    }
}