using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.TravelMisWebService;
using TravelOnline.Class.Purchase;
using TravelOnline.Class.Manage;
using TravelOnline.Class.Travel;
using System.Data;
using System.Configuration;

using TravelOnline.LoginUsers;
using System.Text;
using TravelOnline.EncryptCode;
using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;
using TravelOnline.Class.Common;
using System.IO;
using TravelOnline.WeChat.freetrip.model;
using TravelOnline.WeChat.freetrip.interfaces;
using LitJson;
using TestMvc.Utility;
using System.Net;

namespace TravelOnline.WeChat
{
    public partial class AjaxService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            switch (Request.QueryString["action"])
            {
                case "LoadLineList":
                    Response.Write(WeChatClass.LineListCreate(Request.QueryString["navbar"], Request.QueryString["linetype"], Request.QueryString["lineclass"], Request.QueryString["lineclassname"], MyConvert.ConToInt(Request.QueryString["filter"]), Request.QueryString["dest"], MyConvert.ConToInt(Request.QueryString["pages"]), Request.QueryString["search"]));
                    Response.End();
                    //LoadLineList();
                    break;
                case "LoadDestinationList":
                    Response.Write(WeChatClass.DestinationListCreate(Request.QueryString["lineclass"], Request.QueryString["linetype"]));
                    Response.End();
                    break;
                case "LoadLineInfo":
                    Response.Write(WeChatClass.LineInfoStringCreate(Request.QueryString["lineid"]));
                    Response.End();
                    //LoadLineInfo();
                    break;
                case "LoadLineDateJson":
                    Response.Write(WeChatClass.CreatePlanDateJason(Request.QueryString["lineid"]));
                    Response.End();
                    break;
                case "TempOrder":
                    Response.Write(WeChatOrder.TempOrder(Request.QueryString["lineid"], Request.QueryString["planid"], Request.QueryString["begindate"], Request.QueryString["adults"], Request.QueryString["childs"]));
                    Response.End();
                    break;
                case "OrderFirstStep":
                    Response.Write(WeChatOrder.OrderFirstStep(Request.QueryString["uid"]));
                    Response.End();
                    //Response.Write(WeChatOrder.OrderFirstStep_New(Request.QueryString["uid"]));
                    //Response.End();
                    break;
                case "OrderSecondStep":
                    Response.Write(WeChatOrder.OrderSecondStep(Request.QueryString["uid"]));
                    Response.End();
                    //Response.Write(WeChatOrder.OrderSecondStep_New(Request.QueryString["uid"]));
                    //Response.End();
                    break;
                case "OrderThirdStep":
                    Response.Write(WeChatOrder.OrderThirdStep(Request.QueryString["uid"]));
                    Response.End();
                    //Response.Write(WeChatOrder.OrderThirdStep_New(Request.QueryString["uid"]));
                    //Response.End();
                    break;
                case "OrderPrice":
                    Response.Write(WeChatOrder.OrderPrice(Request.QueryString["uid"], Request.QueryString["price"], Request.Form["PriceStrings"]));
                    Response.End();
                    break;
                case "OrderSubmit":
                    Response.Write(WeChatOrder.OrderSubmit(Request.QueryString["uid"], Request.QueryString["paytype"], Request.QueryString["integral"], Request.Form["ordername"].Trim(), Request.Form["orderphone"].Trim(), Request.Form["orderemail"].Trim(), Request.Form["ordermemo"].Trim()));
                    Response.End();
                    break;
                case "OrderList":
                    //Response.Write(WeChatOrder.OrderList(MyConvert.ConToInt(Request.QueryString["pages"])));
                    //Response.End();
                    Response.Write(WeChatOrder.OrderList_New(MyConvert.ConToInt(Request.QueryString["pages"])));
                    Response.End();
                    break;
                case "FxOrderList":
                    //Response.Write(WeChatOrder.FxOrderList(MyConvert.ConToInt(Request.QueryString["pages"])));
                    //Response.End();
                    Response.Write(WeChatOrder.FxOrderList_New(MyConvert.ConToInt(Request.QueryString["pages"])));
                    Response.End();
                    break;
                case "OrderDetail":
                    //Response.Write(WeChatOrder.OrderDetail(Request.QueryString["uid"]));
                    //Response.End();
                    Response.Write(WeChatOrder.OrderDetail_New(Request.QueryString["uid"]));
                    Response.End();
                    break;
                case "Login":
                    CheckAuthcode(Request.Form["authcode"].Trim());
                    CheckEmail(Request.Form["loginname"].Trim(), Request.Form["loginpwd"].Trim());
                    break;
                case "Reg":
                    CheckRegEmail(Request.Form["regemail"].Trim(), Request.Form["regphone"].Trim());
                    RegUser(Request.Form["regname"].Trim(), Request.Form["regemail"].Trim(), Request.Form["regphone"].Trim(), Request.Form["regpwd"].Trim());
                    break;
                case "CheckOnline":
                    if (Convert.ToString(Session["Online_UserId"]).Length > 0)
                    {
                        Response.Write("{\"success\":\"ok\"}");
                    }
                    else
                    {
                        Response.Write("{\"error\":\"ok\"}");
                    }
                    break;
                case "Logout":
                    Session["Online_UserId"] = null;
                    Response.Write("{\"success\":\"ok\"}");
                    break;
                case "CheckFxOnline":
                    if (Convert.ToString(Session["Fx_UserId"]).Length > 0 && Convert.ToString(Session["Fx_Login"]).Length > 0)
                    {
                        Response.Write("{\"success\":\"ok\"}");
                    }
                    else
                    {
                        Response.Write("{\"error\":\"ok\"}");
                    }
                    break;
                case "CouponList":
                    //Response.Write(WeChatOrder.CouponList(MyConvert.ConToInt(Request.QueryString["pages"])));
                    //Response.End();
                    Response.Write(WeChatOrder.CouponList_New(MyConvert.ConToInt(Request.QueryString["pages"])));
                    Response.End();
                    break;
                case "FxLogin":
                    CheckAuthcode(Request.Form["authcode"].Trim());
                    CheckFxLoginInfo(Request.Form["mobile"].Trim(), Request.Form["userpassword"].Trim());
                    break;
                case "FxReg":
                    CheckFxRegEmail(Request.Form["email"].Trim(), Request.Form["mobile"].Trim());
                    RegFxUser();
                    break;
                case "EditInfo":
                    EditInfo();
                    break;
                case "FXSendRegSMS":
                    SendSMS(MyDataBaseComm.StripSQLInjection(Request.Form["mobile"].Trim()));
                    break;
                case "FreetripSearchLineList":
                    LineSelecter select = new LineSelecter();
                    if (Request.QueryString["dest"].Trim() != "")
                    {
                        select.searchval = Request.QueryString["dest"].Trim();
                    }
                    string result = Regex.Unescape(LineInfoService.GetFreetripLineList(select));
                    Response.Write(result.Replace(@"\n", ""));
                    Response.End();
                    break;
                case "queryCommentList":
                    string res = queryCommentList();
                    Response.Write(res.Replace(@"\n", ""));
                    Response.End();
                    break;
                case "uploadImgFile":
                    uplodaImgFile();
                    break;
                case "publishComment":
                    publishComment();
                    break;
                case "CancelOrder":
                    Response.Write(WeChatOrder.CancelOrder(Request.QueryString["OrderId"]));
                    Response.End();
                    break;
                case "IntegralDetail":
                    //Response.Write(WeChatOrder.IntegralDetail());
                    //Response.End();
                    Response.Write(WeChatOrder.IntegralDetail_New());
                    Response.End();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }
        //private object LoadLineList()
        //{
        //    return WeChatClass.LineListCreate_New(Request.QueryString["navbar"], Request.QueryString["linetype"], Request.QueryString["lineclass"], Request.QueryString["lineclassname"], MyConvert.ConToInt(Request.QueryString["filter"]), Request.QueryString["dest"], MyConvert.ConToInt(Request.QueryString["pages"]), Request.QueryString["search"]);
        //}
        //private object LoadLineInfo()
        //{
        //    return WeChatClass.LineInfoStringCreate_New(Request.QueryString["lineid"]);
        //}
        private void EditInfo()
        {
            if (Convert.ToString(Session["Fx_mobileLogin"]).Length > 0)
            {
                FxEditInfo(Request.Form["mobile"].Trim(), Request.Form["userpassword"].Trim());
            }
            else if (Convert.ToString(Session["Fx_wxLogin"]).Length > 0)
            {
                WxEditInfo();
            }
        }

        private void FxEditInfo(string loginname, string loginpwd)
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("(UserEmail='{0}' or Mobile='{0}') and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(loginname), SecurityCode.Md5_Encrypt(loginpwd, 32));
            RUser = LoginUser.LoginFxUser(SqlQueryText);
            if (RUser != null)
            {
                string email = Convert.ToString(Request.Form["email"]);
                string username = Convert.ToString(Request.Form["username"]);
                string wxid = Convert.ToString(Request.Form["wxid"]);
                string storename = Convert.ToString(Request.Form["storename"]);
                string tel = Convert.ToString(Request.Form["tel"]);
                string address = Convert.ToString(Request.Form["address"]);
                RUser.UserEmail = email;
                RUser.UserName = username;
                RUser.Wxid = wxid;
                RUser.Storename = storename;
                RUser.Tel = tel;
                RUser.Address = address;
                SqlQueryText = string.Format("UPDATE OL_FXLoginUser set UserEmail='{0}',UserName='{1}',Wxid='{2}',Storename='{3}',Tel='{4}',Address='{5}' where id='{6}'", RUser.UserEmail, RUser.UserName, RUser.Wxid, RUser.Storename, RUser.Tel, RUser.Address, RUser.Id);
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                CheckFxLoginInfo(loginname, loginpwd);
            }
            else
            {
                Response.Write("({\"error\":\"密码错误\"})");
            }
        }

        private void WxEditInfo()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("ThirdPartyID='{0}'", MyDataBaseComm.StripSQLInjection(Convert.ToString(Session["ThirdPartyID"])));
            RUser = LoginUser.LoginFxUser(SqlQueryText);
            if (RUser != null)
            {
                string mobile = Convert.ToString(Request.Form["mobile"]);
                string email = Convert.ToString(Request.Form["email"]);
                string username = Convert.ToString(Request.Form["username"]);
                string wxid = Convert.ToString(Request.Form["wxid"]);
                string storename = Convert.ToString(Request.Form["storename"]);
                string tel = Convert.ToString(Request.Form["tel"]);
                string address = Convert.ToString(Request.Form["address"]);
                RUser.Mobile = mobile;
                RUser.UserEmail = email;
                RUser.UserName = username;
                RUser.Wxid = wxid;
                RUser.Storename = storename;
                RUser.Tel = tel;
                RUser.Address = address;
                SqlQueryText = string.Format("UPDATE OL_FXLoginUser set UserEmail='{0}',UserName='{1}',Wxid='{2}',Storename='{3}',Tel='{4}',Address='{5}',mobile='{7}' where id='{6}'", RUser.UserEmail, RUser.UserName, RUser.Wxid, RUser.Storename, RUser.Tel, RUser.Address, RUser.Id, RUser.Mobile);
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                CheckWxLoginInfo(Convert.ToString(Session["ThirdPartyID"]));
            }
            else
            {
                Response.Write("({\"error\":\"密码错误\"})");
            }
        }

        private void RegFxUser()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select top 1 * from OL_SmsSend where flag='FxMobileLogin' and mobile='{0}' and extinfo='{1}' order by sendtime desc", MyDataBaseComm.StripSQLInjection(Request.Form["mobile"].Trim()), MyDataBaseComm.StripSQLInjection(Request.Form["Phoneyzm"].Trim()));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DateTime rp_times = DateTime.Now;
                rp_times = rp_times.AddMinutes(-30);
                if (Convert.ToDateTime(DS.Tables[0].Rows[0]["sendtime"]) < rp_times)
                {
                    Response.Write("({\"error\":\"<i></i>短信验证码已过有效期，请重新发送\"})");
                    Response.End();
                }
            }
            else
            {
                Response.Write("({\"error\":\"<i></i>手机号或短信验证码错误\"})");
                Response.End();
            }

            String AutoId = Convert.ToString(CombineKeys.NewComb());
            string mobile = Convert.ToString(Request.Form["mobile"]);
            string email = Convert.ToString(Request.Form["email"]);
            string userpassword = Convert.ToString(Request.Form["userpassword"]);
            string userpassword2 = Convert.ToString(Request.Form["userpassword2"]);
            if (userpassword == null || !userpassword.Equals(userpassword2))
            {
                Response.Write("({\"error\":\"<i></i>密码与确认密码不一致\"})");
                Response.End();
            }
            string username = Convert.ToString(Request.Form["username"]);
            string wxid = Convert.ToString(Request.Form["wxid"]);
            string storename = Convert.ToString(Request.Form["storename"]);
            string tel = Convert.ToString(Request.Form["tel"]);
            string address = Convert.ToString(Request.Form["address"]);
            LoginUser.RegistUser RUser = new LoginUser.RegistUser { Id = AutoId, Mobile = mobile, LoginPassWord = SecurityCode.Md5_Encrypt(userpassword, 32), UserEmail = email, UserName = username, Wxid = wxid, Storename = storename, Tel = tel, Address = address };

            if (LoginUser.LoginUser_Sql(RUser, "FxRegist") != true)
            {
                Response.Write("({\"error\":\"注册失败\"})");
                Response.End();
            }

            CheckFxLoginInfo(mobile, userpassword);
        }

        private void CheckFxRegEmail(string regemail, string regphone)
        {
            string SqlQueryText = string.Format("select top 1 id from OL_FXLoginUser where (UserEmail='{0}' or Mobile='{1}')", regemail, regphone);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"error\":\"手机或邮件已被注册\"})");
                Response.End();
            }
        }

        private void CheckFxLoginInfo(string loginname, string loginpwd)
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("(UserEmail='{0}' or Mobile='{0}') and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(loginname), SecurityCode.Md5_Encrypt(loginpwd, 32));

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
                Session["Fx_mobileLogin"] = "Y";
                SqlQueryText = string.Format("UPDATE OL_FXLoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}' where id='{0}'", RUser.Id, DateTime.Now.ToString());
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                Response.Write("({\"success\":\"登录成功\"})");
            }
            else
            {
                Response.Write("({\"error\":\"用户不存在或密码不正确\"})");
            }
        }

        private void CheckWxLoginInfo(string thirdPartyID)
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("ThirdPartyID='{0}'", MyDataBaseComm.StripSQLInjection(thirdPartyID));

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
                Response.Write("({\"success\":\"登录成功\"})");
            }
            else
            {
                Response.Write("({\"error\":\"用户登陆失败请稍后再试\"})");
            }
        }

        protected void CheckAuthcode(string aucode)
        {
            if (String.Compare(Session["CheckCode"].ToString(), aucode, true) != 0)
            {
                Response.Write("{\"error\":\"验证码错误\"}");//" + Session["CheckCode"].ToString() + " / " + aucode + "
                Response.End();
            }
        }

        //检验用户身份
        protected void CheckEmail(string loginname, string loginpwd)
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("(UserEmail='{0}' or Mobile='{0}') and LoginPassWord='{1}'", MyDataBaseComm.StripSQLInjection(loginname), SecurityCode.Md5_Encrypt(loginpwd, 32));

            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser != null)
            {
                Session["Online_UserId"] = RUser.Id;
                Session["Online_UserName"] = RUser.UserName;
                Session["Online_UserDept"] = RUser.Deptid;
                Session["Online_UserCompany"] = RUser.Companyid;
                Session["Online_RebateFlag"] = RUser.RebateFlag;
                Session["Online_YJDept"] = RUser.YJDeptName;
                Session["Online_WeChatEmail"] = RUser.UserEmail;
                Session["Online_WeChatPhone"] = RUser.Mobile;
                SqlQueryText = string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{1}' where id='{0}'", RUser.Id, DateTime.Now.ToString());
                MyDataBaseComm.ExcuteSql(SqlQueryText);
                Response.Write("{\"success\":\"登录成功\"}");
            }
            else
            {
                Response.Write("{\"error\":\"用户不存在或密码不正确\"}");
            }

            RUser = null;
        }

        protected void CheckRegEmail(string regemail, string regphone)
        {
            string SqlQueryText = "";
            if (regemail != null && regemail != "")
            {
                SqlQueryText = string.Format("select top 1 id from OL_LoginUser where (UserEmail='{0}' or Mobile='{1}')", regemail, regphone);
            }
            else
            {
                SqlQueryText = string.Format("select top 1 id from OL_LoginUser where Mobile='{0}'", regphone);
            }

            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"error\":\"手机或邮件已被注册\"}");
                Response.End();
            }
        }

        protected void RegUser(string regname, string regemail, string regphone, string regpwd)
        {
            String AutoId = Convert.ToString(CombineKeys.NewComb());
            LoginUser.RegistUser RUser = new LoginUser.RegistUser { Id = AutoId, UserName = regname, UserEmail = regemail, Mobile = regphone, LoginPassWord = SecurityCode.Md5_Encrypt(regpwd, 32) };

            if (LoginUser.LoginUser_Sql(RUser, "WeChatRegist") != true)
            {
                Response.Write("{\"error\":\"注册失败\"}");
                Response.End();
            }

            CheckEmail(regemail, regpwd);
        }

        public static string GetClientIP()
        {
            //获得IP地址 
            string hostname;
            System.Net.IPHostEntry localhost;
            hostname = System.Net.Dns.GetHostName();
            localhost = System.Net.Dns.GetHostEntry(hostname);
            string ip = localhost.AddressList[0].ToString();
            int i = 1;
            while (ip.Contains(":"))
            {
                if (i == localhost.AddressList.Length)
                {
                    ip = "";
                    break;
                }
                ip = localhost.AddressList[i].ToString();
                if (ip.Contains(":"))
                {
                    i++;
                }
                else
                    break;
            }
            return ip;

        }

        private static void SaveErrorToLog(string infos)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\NewOrderLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(infos);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        protected void SendSMS(string checkmobile)
        {
            if (checkmobile != "")
            {
                string SqlQueryText = string.Format("select top 1 id from OL_FxLoginUser where Mobile='{0}'", checkmobile);
                if (MyDataBaseComm.getScalar(SqlQueryText) != null)
                {
                    Response.Write("({\"error\":\"<i></i>手机号码已经存在！\"})");
                    Response.End();
                }
            }

            if (Request.QueryString["code"].ToLower() != Session["CheckCode"].ToString().ToLower())
            {

                Response.Write("({\"error\":\"<i></i>验证码错误！\"})");
                Response.End();
            }

            string formRandom = Request.Form["random"].ToString();
            string postRandom = Request.QueryString["r"].ToString();
            if (!formRandom.Equals(postRandom))
            {
                Response.Write("({\"error\":\"<i></i>恶意调用发短信接口！\"})");
                Response.End();
            }

            string SendCount = MyDataBaseComm.getScalar("select count(id) from OL_SmsSend where sendtime>GETDATE()-1 and flag='FxMobileLogin' and mobile='" + MyDataBaseComm.StripSQLInjection(checkmobile.Trim()) + "'");
            if (MyConvert.ConToInt(SendCount) > 5)
            {
                Response.Write("({\"error\":\"<i></i>您的手机号码发送次数过多\"})");
                Response.End();
            }

            string ip = GetClientIP();

            if (ip != null)
            {
                string SendNum = MyDataBaseComm.getScalar("select count(id) from OL_SmsSend where sendtime>GETDATE()-1 and flag='FxMobileLogin' and ip='" + ip + "'");
                if (MyConvert.ConToInt(SendNum) > 30)
                {
                    SaveErrorToLog("发送多次发送验证码ip：" + ip);
                    Response.Write("({\"error\":\"<i></i>此IP发送次数过多\"})");
                    Response.End();
                }
            }

            string Verify = MyConvert.CreateNumberVerifyCode(4);
            string smscontent = "您的动态密码是：" + Verify + "，请在30分钟内完成验证，谢谢您的支持！【上海青旅】";
            string sendresult = SendSms.SendSMS_ztsms(MyDataBaseComm.StripSQLInjection(checkmobile.Trim()), smscontent);
            if (sendresult == "ok")
            {
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_SmsSend (uid,mobile,smscontent,sendid,flag,extinfo,sendtime,ip) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    System.Guid.NewGuid(),
                    MyDataBaseComm.StripSQLInjection(checkmobile.Trim()),
                    smscontent,
                    "0",
                    "FxMobileLogin",
                    Verify,
                    DateTime.Now.ToString(),
                    ip
                );



                //短信监控触发邮箱警告！！！Start
                string strSqlQuery = "select count(id) from dbo.OL_SmsSend where sendtime >CONVERT(varchar(100), GETDATE(), 23)";
                string SendCountNum = MyDataBaseComm.getScalar(strSqlQuery);
                if (MyConvert.ConToInt(SendCountNum) != 0 && MyConvert.ConToInt(SendCountNum) % 500 == 0)
                {
                    //发送邮箱
                    StringBuilder Strings = new StringBuilder();

                    Strings.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                    Strings.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><title>忘记密码提示</title></head>");
                    Strings.Append("<body>");
                    Strings.Append("<table width=\"650\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: 1px solid #999999;\">");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"font-size: 12px; line-height: 20px; text-align: left;\">");
                    Strings.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style='width: 465.0pt; height: 72px; margin-left: 7.5pt; border: none; border-bottom: solid #333333 1.0pt'>");
                    Strings.Append("<tr>");
                    Strings.Append("<td width=\"192\" style='width: 144.0pt; height: 40px; border: none; padding: 20px 0px 0px 0px;'>");
                    Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\"><img border=\"0\" id=\"_x0000_i1025\" src=\"http://www.scyts.com/Images/logo.gif\" alt=\"上海中国青年旅行社\" /></a>");
                    Strings.Append("</td>");
                    Strings.Append("<td width=\"428\" height=\"40\" valign=\"bottom\" align=\"right\" style='width: 321.0pt; border: none; padding: 0cm 0cm 2.5pt 0cm; font-size: 12px;'>");
                    Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\">上海中国青年旅行社首页</a>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("<table width=\"620\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-left: 10px;\">");
                    Strings.Append("<tr>");
                    Strings.Append("<td style=\"font-size: 12px; line-height: 25px; padding-top: 10px;\">");
                    Strings.Append(string.Format("<strong>您好:今天{0}短信平台已经发送{1}条短信！</strong>", DateTime.Now.ToString("yyyy-MM-dd"), MyConvert.ConToInt(SendCountNum).ToString()));
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("<tr>");
                    Strings.Append("<td align=\"left\" style='border: none; padding: 1.5pt 0cm 7.5pt 0cm; border-top: dashed #999999 1.0pt'>");
                    Strings.Append("<p style='line-height: 15.0pt'>");
                    Strings.Append("<span style=\"font-size: 9.0pt; color: #999999\">您之所以收到这封邮件，是因为您曾经注册成为上海中国青年旅行社网站的用户。<br />");
                    Strings.Append("本邮件由系统自动发出，请勿直接回复！</p>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("</td>");
                    Strings.Append("</tr>");
                    Strings.Append("</table>");
                    Strings.Append("</body>");
                    Strings.Append("</html>");

                    List<string> MailTo = new List<string>();
                    MailTo.Add(ConfigurationManager.AppSettings["SmsSendMail"]);
                    string result = SendEmailClass.SendMail(MailTo.ToArray(), "短信平台监控通知", Strings.ToString(), 1, 2, "null");
                    if (result != "0")
                    {
                        SaveErrorToLog("短信监控：邮箱发送失败！！");
                    }


                    //发送短信
                    string smsMessage = "今天短信以发送：" + MyConvert.ConToInt(SendCountNum).ToString() + "，请注意！【上海青旅】";
                    string smsMessagesendresult = SendSms.SendSMS_ztsms(MyDataBaseComm.StripSQLInjection(Request.Form["mobilePhone"].Trim()), smsMessage);
                    if (smsMessagesendresult != "ok")
                    {
                        SaveErrorToLog("短信监控：短信发送失败！！");
                    }
                }
                //短信监控触发邮箱警告！！！End


                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"<i></i>发送成功，如未收到，请60秒后重发\"})");
                    Response.End();
                }
                else
                {
                    Response.Write("({\"error\":\"<i></i>动态密码发送失败，请稍后再试\"})");
                    Response.End();
                }
            }
            else
            {
                SaveErrorToLog("注册短信发送失败：" + sendresult);
                Response.Write("({\"error\":\"<i></i>发送失败，请稍后再试\"})");
                Response.End();
            }

        }

        public string queryCommentList()
        {
            CommentRS rs = new CommentRS();
            if (Request.QueryString["pagesize"] != null && Request.QueryString["pagesize"] != "")
            {
                rs.pagesize = MyConvert.ConToInt(Request.QueryString["pagesize"]);
            }
            if (Request.QueryString["currpage"] != null && Request.QueryString["currpage"] != "")
            {
                rs.currpage = MyConvert.ConToInt(Request.QueryString["currpage"]);
            }
            if (Request.QueryString["commentStatus"] != null && Request.QueryString["commentStatus"] != "")
            {
                rs.commentStatus = Request.QueryString["commentStatus"].ToString();
            }
            if (Request.QueryString["lineId"] != null && Request.QueryString["lineId"] != "")
            {
                rs.lineId = Request.QueryString["lineId"].ToString();
            }
            if (Request.QueryString["auditStatus"] != null && Request.QueryString["auditStatus"] != "")
            {
                rs.auditStatus = Request.QueryString["auditStatus"].ToString();
            }
            if (Session["Online_UserId"] != null)
            {
                rs.userId = Session["Online_UserId"].ToString();
            }
            string res = Regex.Unescape(CommentInfoService.getComments(rs));
            return res;
        }

        public void uplodaImgFile()
        {
            string picUrl = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                try
                {
                    string _fileNamePath = Request.Files[i].FileName;
                    //开始上传
                    string _savedFileResult = UpLoadImage(_fileNamePath, i);
                    if (i == Request.Files.Count - 1)
                    {
                        picUrl = picUrl + _savedFileResult;
                    }
                    else
                    {
                        picUrl = picUrl + _savedFileResult + ",";
                    }

                }
                catch
                {
                    Response.Write("上传提交出错");
                }
            }
            Response.Write(picUrl);
            Response.End();
        }

        private string UpLoadImage(string fileNamePath, int i)
        {
            string ChildPath = Session["Online_UserId"].ToString();

            string PathSet = string.Format("{0:yyyy-MM-dd}", DateTime.Now); //保存路径
            //string Thumb = context.Request.QueryString["Thumb"]; //是否生成缩略图，为0 不生成，为数字，表示缩略图高度
            //string flag = context.Request.QueryString["flag"];
            try
            {
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                string toFilePath = "";
                toFilePath = Path.Combine(serverPath, string.Format(@"Upload\CommentImage\{0}\{1}", PathSet, ChildPath));

                if (System.IO.Directory.Exists(toFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(toFilePath);
                }

                //获取要保存的文件信息
                FileInfo file = new FileInfo(fileNamePath);
                //获得文件扩展名
                string fileNameExt = file.Extension;

                //验证合法的文件
                if (PicUploadHander.CheckImageExt(fileNameExt))
                {
                    //生成将要保存的随机文件名
                    string fileName = PicUploadHander.GetImageName() + fileNameExt;

                    //获得要保存的文件路径
                    string serverFileName = toFilePath + "/" + fileName;
                    //物理完整路径                    
                    string toFileFullPath = serverFileName; //HttpContext.Current.Server.MapPath(toFilePath);

                    //将要保存的完整文件名                
                    string toFile = toFileFullPath;//+ fileName;

                    ///创建WebClient实例       
                    WebClient myWebClient = new WebClient();
                    //设定windows网络安全认证   方法1
                    myWebClient.Credentials = CredentialCache.DefaultCredentials;
                    ////设定windows网络安全认证   方法2
                    Request.Files[i].SaveAs(toFile);

                    //上传成功后网站内源图片相对路径
                    string relativePath = System.Web.HttpContext.Current.Request.ApplicationPath
                                          + string.Format(@"Upload/CommentImage/{0}/{1}/{2}", PathSet, ChildPath, fileName);
                    return relativePath;
                }
                else
                {
                    return "文件格式非法，请上传gif或jpg格式的文件。";
                    //throw new Exception("文件格式非法，请上传gif或jpg格式的文件。");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void publishComment()
        {
            int id = MyConvert.ConToInt(Request.Form["id"]);
            string context = Convert.ToString(Request.Form["context"]);
            string pic = Convert.ToString(Request.Form["pic"]);
            string rank = Convert.ToString(Request.Form["rank"]);
            string title = Convert.ToString(Request.Form["title"]);
            Comment comment = new Comment();
            comment.id = id;
            comment.context = context;
            comment.pic = pic;
            comment.rank = rank;
            comment.title = title;
            comment.commentStatus = "COMMENTED";
            if (CommentInfoService.updateComment(comment))
            {
                Response.Write("{\"isError\":0,\"msg\":\"点评发布成功\"}");
                Response.End();
            }
            else
            {
                Response.Write("{\"isError\":0,\"msg\":\"点评发布失败，请稍后再试\"}");
                Response.End();
            }

        }

    }
}