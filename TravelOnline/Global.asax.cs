using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using TravelOnline.Class.Purchase;
using System.Text;
using System.IO;

namespace TravelOnline
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码

            //占位订单自动变更为需确认不占位订单
            //'创建一个计时器，单位：毫秒  60000=1分钟 10000=10秒

            //int OrderAdjust_timelimit = 1200000; //20分钟
            int OrderAdjust_timelimit = 3600000; //60分钟
            System.Timers.Timer OrderAdjust_aTimer = new System.Timers.Timer(OrderAdjust_timelimit);
            OrderAdjust_aTimer.Elapsed += OrderAdjust_Fresher;
            OrderAdjust_aTimer.AutoReset = true;
            OrderAdjust_aTimer.Enabled = true;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

            //遍历Post参数，隐藏域除外 
            foreach (string i in this.Request.Form)
            {
                if (i == "__VIEWSTATE") continue;
                if (i == "__EVENTVALIDATION") continue;
                if (MyDataBaseComm.CheckSQLInjection(this.Request.Form[i].ToString()) == true)
                {
                    //Response.Write("警告！你的IP已经被记录!不要使用敏感字符！");
                    //Response.End(); 
                    SaveErrorToLog("链接：" + this.Request.Url.ToString(), "参数：" + i, "传递：" + this.Request.Form[i].ToString());
                    Response.End(); 
                }
            }
            //遍历Get参数。 
            foreach (string i in this.Request.QueryString)
            {
                if (MyDataBaseComm.CheckSQLInjection(this.Request.QueryString[i].ToString()) == true)
                {
                    //Response.Write("警告！你的IP已经被记录!不要使用敏感字符！");
                    //Response.End();
                    SaveErrorToLog("链接：" + this.Request.Url.ToString(), "参数：" + i, "传递：" + this.Request.QueryString[i].ToString());
                    Response.End(); 
                }
            }
        }

        public static void SaveErrorToLog(string s1,string s2,string s3)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\FormLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(DateTime.Now.ToString() + ":");
                writer.WriteLine(s1);
                writer.WriteLine(s2);
                writer.WriteLine(s3);
                writer.WriteLine("");
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public void OrderAdjust_Fresher(object sender, ElapsedEventArgs e)
        {
            //int ClearTime = 2;
            int NowTime = DateTime.Now.Hour;
            //1点到3点之间执行
            if (NowTime > 1 && NowTime < 3)
            {
                try
                {
                    PurchaseAutoRun.OrderAutoAdjuest();
                }
                catch
                {
                }

                try
                {
                    PurchaseAutoRun.DeleteTempOrder();
                }
                catch
                {
                }

                try
                {
                    PurchaseAutoRun.DeleteWeekTempOrder();
                }
                catch
                {
                }

                try
                {
                    //PurchaseAutoRun.CruisesLineAdjuest();
                    PurchaseAutoRun.ClearShip_Line_14120();
                }
                catch
                {
                }

                try
                {
                    PurchaseAutoRun.DeleteAllCache();
                    PurchaseAutoRun.CancelLineTop();
                }
                catch
                {
                }

                //会员积分计算
                try
                {
                    PurchaseAutoRun.AutoGetOrderIntegral();
                }
                catch
                {
                }

                //删除过期的线路
                try
                {
                    PurchaseAutoRun.DeleteTodayLine();
                }
                catch
                {
                }
            }
            
        }

    }
}
