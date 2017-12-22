using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using System.Configuration;

namespace TravelOnline.NewPage
{
    public partial class Pay : System.Web.UI.Page
    {
        public string LoginInfo = "", OrderId, linename, autoid, ordernums, orderdate, price, username, planid, pufaprice,lineId,boshprice,couponAmount;
        public string hide1 = "hide", hide2 = "hide", hide3 = "hide", hide4 = "hide", hide5 = "", hide1_1 = "hide";
        public string payhide11 = "", payhide12 = "hide", payhide21 = "", payhide22 = "hide", payhide31 = "", payhide32 = "hide", payhide41 = "", payhide42 = "hide",payhide50="", payhide51 = "", payhide52 = "hide";
        decimal N_PayNow, N_Pay, N_Yfk, N_Yue, All_Yfk;
        public string yfk, Pays, YeE;
        public string aType, aPrice, hide6="hide";
        public DateTime aBeginDate, aEndDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            //OrderId = "34D40590-6C32-4C7C-BDDA-A49000F1454E";
            username = Convert.ToString(Session["Online_UserName"]);
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["Online_UserId"]).Length > 0)
                {
                    LoginInfo = "<li>您好，<a class=\"colorF60\" href=\"javascript:;\">" + username + "</a></li><li><a href=\"/login/logout.aspx\">退出</a></li>";
                }
                else
                {
                    LoginInfo = "<li>您好，</li><li><a href=\"/member/login.html\">请登录</a></li>";
                }
                LoadOrder();
            }
        }

        protected void LoadOrder()
        {
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                linename = DS.Tables[0].Rows[0]["LineName"].ToString();
                ordernums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                price = DS.Tables[0].Rows[0]["Price"].ToString();
                if (DS.Tables[0].Rows[0]["couponAmount"] != null && !DS.Tables[0].Rows[0]["couponAmount"].ToString().Equals(""))
                {
                    couponAmount = DS.Tables[0].Rows[0]["couponAmount"].ToString();
                }
                else
                {
                    couponAmount = "0";
                }
                
                autoid = DS.Tables[0].Rows[0]["AutoId"].ToString();
                planid = "," + DS.Tables[0].Rows[0]["planid"].ToString() + ",";
                lineId = "," + DS.Tables[0].Rows[0]["lineid"].ToString() + ",";


                //预付活动
                string SqlText = string.Format("select * from OL_ActivityOrder where orderid = '{0}'", OrderId);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    aType = DS1.Tables[0].Rows[0]["AType"].ToString();
                    aPrice = DS1.Tables[0].Rows[0]["APrice"].ToString();
                    if (DS1.Tables[0].Rows[0]["ABeginDate"] != null && !DS1.Tables[0].Rows[0]["ABeginDate"].ToString().Equals(""))
                    {
                        aBeginDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:mm:ss}", DS1.Tables[0].Rows[0]["ABeginDate"]));
                    }
                    if (DS1.Tables[0].Rows[0]["AEndDate"] != null && !DS1.Tables[0].Rows[0]["AEndDate"].ToString().Equals(""))
                    {
                        aEndDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:mm:ss}", DS1.Tables[0].Rows[0]["AEndDate"]));
                    }
                    if (DateTime.Now.CompareTo(aBeginDate) < 0)
                    {
                        YeE = (MyConvert.ConToDec(aPrice) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString())).ToString();
                    }
                    if (DateTime.Now.CompareTo(aBeginDate) >= 0 && DateTime.Now.CompareTo(aEndDate) <= 0)
                    {
                        YeE = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString()) - MyConvert.ConToDec(couponAmount)).ToString();
                    }
                    if (DateTime.Now.CompareTo(aEndDate) > 0)
                    {
                        hide3 = "hide";
                    }
                    hide6 = "";
                }
                else
                {
                    YeE = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString()) - MyConvert.ConToDec(couponAmount)).ToString();
                }
                pufaprice = YeE;
                boshprice = YeE;
                Pays = DS.Tables[0].Rows[0]["pay"].ToString();
                yfk = MyConvert.ConToDec(DS.Tables[0].Rows[0]["yfk"].ToString()).ToString();
                All_Yfk = MyConvert.ConToDec(DS.Tables[0].Rows[0]["OrderNums"].ToString()) * MyConvert.ConToDec(yfk);//最低预付总额

                if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["pay"].ToString()) == 0)
                {
                    if (All_Yfk == 0)
                    {
                        if (MyConvert.ConToDec(YeE) > 500)
                        {
                            All_Yfk = 500;
                        }
                        else
                        {
                            All_Yfk = MyConvert.ConToDec(YeE);
                        }
                    }
                }
                yfk = All_Yfk.ToString();

                payhide11 = "<li class='cur'>支付宝支付</li>";
                payhide12 = "";
                payhide21 = "<li id='pfpay'>信用卡快捷支付</li>";
                payhide22 = "hide";
                payhide31 = "<li id='xypay'>网银支付</li>";
                payhide32 = "hide";
                payhide41 = "<li id='wxpay'>微信支付</li>";
                payhide42 = "hide";


                int pufa_price = MyConvert.ConToInt(Convert.ToString(ConfigurationManager.AppSettings["pdyh_price"]));

                if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
                {
                    payhide21 = "<li id='pfpay'>浦发信用卡支付立减活动</li>";
                    payhide11 = "";
                    payhide12 = "hide";
                    payhide22 = "";
                    payhide31 = "";
                    payhide41 = "";
                    if (pufa_price > 0)
                    {
                        payhide21 = "<li id='pfpay'>浦发信用卡立减" + pufa_price + "元/人</li>";
                    }
                }

                if (isBosh300())
                {
                    payhide21 = "";
                    payhide11 = "";
                    payhide12 = "hide";
                    payhide31 = "";
                    payhide41 = "";
                    payhide51 = "<li id='pfpay'>上海银行借记卡立减300元/人</li>";
                    payhide52 = "";
                }

                if (isBosh250())
                {
                    payhide21 = "";
                    payhide11 = "";
                    payhide12 = "hide";
                    payhide31 = "";
                    payhide41 = "";
                    payhide51 = "<li id='pfpay'>上海银行借记卡立减250元/人</li>";
                    payhide52 = "";
                }

                if (isBosh200())
                {
                    payhide21 = "";
                    payhide11 = "";
                    payhide12 = "hide";
                    payhide31 = "";
                    payhide41 = "";
                    payhide51 = "<li id='pfpay'>上海银行借记卡立减200元/人</li>";
                    payhide52 = "";
                }

                if (isSHRCB())
                {
                    payhide21 = "";
                    payhide11 = "<li>上海农商银行</li>";
                    payhide12 = "";
                    payhide31 = "";
                    payhide41 = "";
                    payhide51 = "";
                    payhide52 = "hide";
                }
                
                orderdate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]);

                if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(planid) > -1)
                {
                    if (pufa_price > 0)
                    {
                        decimal lijian = pufa_price * MyConvert.ConToDec(ordernums);
                        decimal pprice = MyConvert.ConToDec(price);
                        if (pprice > lijian)
                        {
                            pufaprice = (pprice - lijian).ToString();
                            YeE = pufaprice;
                        }
                    }
                }

                if (isBosh300())
                {
                    decimal lijian = 300 * MyConvert.ConToDec(ordernums);
                    decimal pprice = MyConvert.ConToDec(price);
                    if (pprice > lijian)
                    {
                        boshprice = (pprice - lijian).ToString();
                        YeE = boshprice;
                    }
                }

                if (isBosh250())
                {
                    decimal lijian = 250 * MyConvert.ConToDec(ordernums);
                    decimal pprice = MyConvert.ConToDec(price);
                    if (pprice > lijian)
                    {
                        boshprice = (pprice - lijian).ToString();
                        YeE = boshprice;
                    }
                }

                if (isBosh200())
                {
                    decimal lijian = 200 * MyConvert.ConToDec(ordernums);
                    decimal pprice = MyConvert.ConToDec(price);
                    if (pprice > lijian)
                    {
                        boshprice = (pprice - lijian).ToString();
                        YeE = boshprice;
                    }
                }

                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "30" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "2")
                {

                    if (DS.Tables[0].Rows[0]["LineID"].ToString() == ConfigurationManager.AppSettings["RuTaiZhengLineID"])
                    {
                        hide1_1 = "";
                        hide3 = "";
                    }
                    else
                    {
                        hide1 = "";
                        hide3 = "";
                    }
                }
                else if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "0" && DS.Tables[0].Rows[0]["ProductType"].ToString() != "Visa")
                {
                    hide4 = "";
                    hide5 = "hide";
                }
                else
                {
                    hide2 = "";
                }
                //if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["PayFlag"].ToString()) == 0)
                //{
                    
                //}
                //else
                //{
                //    hide2 = "";
                //}
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        private Boolean isBosh300()
        {
            return Session["userType"] != null && Session["userType"].ToString().Equals("ShYinHangRegUser") && Convert.ToString(ConfigurationManager.AppSettings["bosh300"]).IndexOf(lineId) > -1;
        }

        private Boolean isBosh250()
        {
            return Session["userType"] != null && Session["userType"].ToString().Equals("ShYinHangRegUser") && Convert.ToString(ConfigurationManager.AppSettings["bosh250"]).IndexOf(lineId) > -1;
        }

        private Boolean isBosh200()
        {
            return Session["userType"] != null && Session["userType"].ToString().Equals("ShYinHangRegUser") && Convert.ToString(ConfigurationManager.AppSettings["bosh200"]).IndexOf(lineId) > -1;
        }

        private Boolean isSHRCB()
        {
            return Convert.ToString(ConfigurationManager.AppSettings["SHRCBLine"]).IndexOf(lineId) > -1;
        }


    }
}