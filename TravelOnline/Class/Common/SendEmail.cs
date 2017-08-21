using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace TravelOnline.Class.Common
{
    public class SendEmailClass
    {

        /// <summary>
        /// 功能：发送邮件,返回字符串：“发送成功”否则返回错误代码。
        /// </summary>
        /// <param name="MailTo">MailTo为收信人地址</param>
        /// <param name="Subject">Subject为标题</param>
        /// <param name="Body">Body为信件内容</param>
        /// <param name="BodyFormat">BodyFormat为信件内容格式:0为Text,1为Html</param>
        /// <param name="Priority">Priority为优先级:0为低,1为中,2为高</param>
        /// <param name="Attachments">Attachment为附件,为null则不发送</param>
        public static string SendMail(string[] MailTo, string Subject, string Body, int BodyFormat, int Priority, string Attachments)
        {
            string result;
            if (MailTo.Length < 1)
            {
                return "没有任何要发送邮件的地址";
            }

            if (Convert.ToString(ConfigurationManager.AppSettings["MailSendSmtp"]).Length < 5)
            {
                return "请到Web.Config文件设置邮件发送参数";
            }
            SmtpClient mail = new SmtpClient();
            //发送方式
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            //mail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            //smtp服务器
            mail.Host = Convert.ToString(ConfigurationManager.AppSettings["MailSendSmtp"]); //"smtp.163.com";
            //用户名凭证               
            mail.Credentials = new System.Net.NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["MailSendAddress"]), Convert.ToString(ConfigurationManager.AppSettings["MailSendPassWord"]));
            //邮件信息
            MailMessage message = new MailMessage();
            //发件人
            message.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["MailSendAddress"]));
            //收件人
            foreach (object item in MailTo)
            {
                message.To.Add(item.ToString());
            }
            //主题
            message.Subject = Subject;
            //内容
            message.Body = Body;
            //正文编码
            message.BodyEncoding = System.Text.Encoding.UTF8;
            //设置为HTML格式
            message.IsBodyHtml = true;
            //优先级
            message.Priority = MailPriority.High;

            try
            {
                mail.Send(message);
                result = "0";
                return result;
            }
            catch (Exception e)
            {
                result = e.ToString();
                SaveErrorToLog(result, result);
            }
            return result;
        }


        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(DateTime.Now.ToString() + ":");
                writer.WriteLine(inErrorlog);
                writer.WriteLine(inSQL);
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }



    }
}