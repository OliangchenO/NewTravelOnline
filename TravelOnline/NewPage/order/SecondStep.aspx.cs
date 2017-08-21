using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;
using TravelOnline.NewPage.Class;

namespace TravelOnline.NewPage.order
{
    public partial class SecondStep : System.Web.UI.Page
    {
        public string LoginInfo="", OrderId, username, AutoId, pay, linename, nums, bgndate, lineid, LineDays, Adults, Childs, Price;
        public string Integral = "0";
        public string hide_inland = "hide", hide_outbound = "hide", hide_visa="hide", ProductType, Attentions, visahide = "";
        public string price01 = "", price02 = "", price03 = "", price04 = "", Guestinfo = "", LineDayString = "";
        public string ordername = "请填写真实姓名", orderemail = "用于接收确认信息", ordermobile = "用于接收确认信息";
        public string ratio = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Buffer = true;
            //Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-10);
            //Response.Expires = 0;
            //Response.CacheControl = "no-cache";
            //Response.AppendHeader("Pragma", "No-Cache");

            OrderId = Request.QueryString["orderid"];
            username = Convert.ToString(Session["Online_UserName"]);
            ratio = Convert.ToString(ConfigurationManager.AppSettings["Integral_ratio"]);
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["Online_UserId"]).Length > 0)
                {
                    ordername = Convert.ToString(Session["Online_UserName"]);
                    orderemail = Convert.ToString(Session["Online_UserEmail"]);
                    ordermobile = Convert.ToString(Session["Online_UserMobile"]);
                    LoginInfo = "<li>您好，<a class=\"colorF60\" href=\"javascript:;\">" + username + "</a></li><li><a href=\"/login/logout.aspx\">退出</a></li>";
                    //Integral = DataClass.GetIntegral(new Guid(Convert.ToString(Session["Online_UserId"])));
                    Integral = GetIntegral();
                }
                else
                {
                    LoginInfo = "<li>您好，</li><li><a href=\"/member/login.html\">请登录</a></li>";
                }
                LoadOrder();
            }
        }
        protected string GetIntegral()
        {
            string SqlQueryText = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(Session["Online_UserId"]));
            return MyDataBaseComm.getScalar(SqlQueryText);
        }

        protected void LoadOrder()
        {
            string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", OrderId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                
                  
                
                linename = DS.Tables[0].Rows[0]["linename"].ToString();
                nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
                bgndate = string.Format("{0:yyyy-MM-dd}", MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["BeginDate"].ToString()));
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Price = DS.Tables[0].Rows[0]["Price"].ToString();

                ProductType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                if (ProductType == "Visa")
                {
                    ReadRouteXML(lineid, "Visa");
                    visahide = "hide";
                    hide_visa = "";
                }
                else
                {
                    ReadRouteXML(lineid, "");
                    LineDayString = "旅游天数：<span class=\"fb\">" + DS.Tables[0].Rows[0]["LineDays"].ToString() + "</span>";
                }

                if (ProductType == "InLand") hide_inland = "";
                if (ProductType == "OutBound") hide_outbound = "";
                if (ProductType == "Cruises") hide_outbound = "";
                if (ProductType == "FreeTour")
                {
                    if (DS.Tables[0].Rows[0]["ProductClass"].ToString() == "1062")
                    {
                        hide_inland = "";
                    }
                    else {
                        hide_outbound = "";
                    }
                }
                    

                SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}' order by InputDate desc", OrderId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (DS.Tables[0].Rows[i]["PriceType"].ToString() == "SellPrice")
                        {
                            price01 += "<dd><p>" + DS.Tables[0].Rows[i]["PriceName"].ToString() + "</p><strong><span>" + DS.Tables[0].Rows[i]["OrderNums"].ToString() + " ×</span>¥" + DS.Tables[0].Rows[i]["SellPrice"].ToString() + "</strong></dd>";
                        }
                        if (DS.Tables[0].Rows[i]["PriceType"].ToString() == "ExtPrice")
                        {
                            price02 += "<dd><p>" + DS.Tables[0].Rows[i]["PriceName"].ToString() + "</p><strong><span>" + DS.Tables[0].Rows[i]["OrderNums"].ToString() + " ×</span>¥" + DS.Tables[0].Rows[i]["SellPrice"].ToString() + "</strong></dd>";
                        }
                        if (DS.Tables[0].Rows[i]["PriceType"].ToString() == "Preference")
                        {
                            price03 += "<dd><p>" + DS.Tables[0].Rows[i]["PriceName"].ToString() + "</p><strong><span>" + DS.Tables[0].Rows[i]["OrderNums"].ToString() + " ×</span>¥" + DS.Tables[0].Rows[i]["SellPrice"].ToString() + "</strong></dd>";
                        }
                    }
                    if (price01.Length > 0) price01 = "<dl><dt>基本费用</dt>" + price01 + "</dl>";
                    if (price02.Length > 0) price02 = "<dl><dt>可选费用</dt>" + price02 + "</dl>";
                    if (price03.Length > 0) price03 = "<dl><dt>优惠信息</dt>" + price03 + "</dl>";
                }

                StringBuilder String = new StringBuilder();
                for (int i = 1; i <= MyConvert.ConToInt(nums); i++)
                {
                    String.Append(string.Format(@"<div class='save-box fl'><dl><dd class='s1'>第<span>{0}</span>位游客</dd></dl></div>
                        <ul class='rl ykxx'>
                    	    <li>
                                <label class='info-label' for=''><span>*</span>中文姓名</label>
                                <input class='input-w text_default' type='text' placeholder='证件的中文姓名' value='证件的中文姓名' maxlength='30' name='guestname' class='inputxt' datatype='z2-5' nullmsg='请填写中文姓名' errormsg='中文姓名不可少于2个汉字' />
                            </li>
                            <li>
                                <label class='info-label' for=''></label>
                                <input class='rainput' id='vsem1' name='vsex{1}' type='radio' value='男' datatype='*' nullmsg='请选择性别' errormsg='请选择性别' checked='checked'/>
                        	    <label class='radio' for='vsem1'>男</label>
                                <input class='rainput' id='vsef1' name='vsex{1}' type='radio' value='女'/>
                                <label class='radio' for='vsef1'>女</label>
                            </li>
                            <li>
                        	    <label class='info-label' for=''>手机号码</label>
                                <input class='input-w text_default' type='text' value='' name='guestmobile' maxlength='11' />
                            </li>
                            <li class='sr-box'>
                        	    <label class='info-label' for=''></label>
                        	    <label class='radio rad1'>
                        		    <input class='' name='sr{0}' type='radio' value='1'/>现在输入证件信息
                                </label>
                                <label class='radio rad2'>
                        		    <input class='' name='sr{0}' type='radio' value='0' checked='checked' />稍后输入
                                    <span>如果您的证件未带在身边在您提交订单后由客服人员为您填写信息</span>
                                </label>
                            </li>
                            <div class='sr-info hide'>
                                <li>
                                    <label class='info-label' for=''><span>*</span>证件类型</label>
                                    <select class='cer' name='cer1' id=''>
                                        <option value='1'>身份证</option>
                                        <option value='2'>护照</option>
                                        <option value='5'>军官证</option>
                                        <option value='6'>回乡证</option>
                                        <option value='3'>港澳通行证</option>
                                        <option value='7'>台胞证</option>
                                    </select>
                                    <input class='input-w zj text_default' type='text' maxlength='18' placeholder='证件号码' value='证件号码' name='guestzjhm' datatype='n-e' nullmsg='请填写证件号码' errormsg='请填写正确的证件号码' />
                                </li>
                            </div>
                        </ul>",
                        i,
                        i-1
                        ));

                    if (i < MyConvert.ConToInt(nums)) String.Append("<div class='line-d'></div>");
                }
                //<div class='visa-sr hide'>
                //    <li>
                //        <label class='info-label' for=''><span>*</span>出生日期</label>
                //        <input class='text_default input-w Wdate' maxlength='10' type='text' value='示例：1977-03-26' name='guestcsrq' datatype='*' onClick='WdatePicker()' nullmsg='请填写出生日期' errormsg='请填写出生日期'/>
                //    </li>
                //</div>
                Guestinfo = String.ToString();
            }


        }

        protected void ReadRouteXML(string LineId,string flag)
        {
            string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, LineId);
            if (System.IO.File.Exists(path) == true)
            {
                StringBuilder Strings = new StringBuilder();
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlNode x = XmlDoc.SelectSingleNode("//Route");
                if (x != null)
                {
                    if (flag == "Visa")
                    {
                        Attentions = "<dd>" + x.SelectSingleNode("Memo").InnerText.Replace("\n", "</dd><dd>") + "</dd>";
                    }
                    else
                    {
                        Attentions = "<dd>" + x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</dd><dd>") + "</dd>";
                    }
                    
                    if (Attentions.Length < 9) Attentions = "";
                }
            }
        }



    }
}