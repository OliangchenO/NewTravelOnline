using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.LoginUsers;
using TravelOnline.Class.Common;

namespace TravelOnline.Purchase
{
    public partial class OrderCheck : System.Web.UI.Page
    {
        public string Allintegral,QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice, CuisesList;
        public string User_Info, User_Memo, GuestList, Contract, Invoice, DinnerInfo;
        public string RB1, RB2, Flag, StepString, hide1, hide2, integralhide = "hide", PolicyHide = "hide";
        public string huifeng, huifeng1 = "hide", tebieshuoming = "特别说明";
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            Flag = Request.QueryString["Flag"];
            if (Flag == "")
            {
                StepString = "";
                StepString += "<li class=\"view\">选择线路 </li>";
                StepString += "<li class=\"selects\">选择价格</li>";
                StepString += "<li class=\"book\">填写信息</li>";
                StepString += "<li class=\"check\">核对订单</li>";
                StepString += "<li class=\"submit\">成功提交</li>";
            }
            else {
                StepString = "";
                StepString += "<li class=\"view\">选择价格 </li>";
                StepString += "<li class=\"selects\">录入名单</li>";
                StepString += "<li class=\"book\">岸上观光</li>";
                StepString += "<li class=\"check\">核对订单</li>";
                StepString += "<li class=\"submit\">成功提交</li>";
            }

            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                //Allintegral = MyDataBaseComm.getScalar("select isnull(sum(integral),0) from OL_Integral where uid='" + Convert.ToString(Session["Online_UserId"]) + "'");
                //if (MyConvert.ConToInt(Allintegral) > 0) integralhide = "";
                //integralhide = "";
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();

                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    hide1 = "hide";
                }
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2") hide1 = "hide";

                string ProType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                string ProClass = DS.Tables[0].Rows[0]["ProductClass"].ToString();

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                if (price < MyConvert.ConToInt(Allintegral)) Allintegral = price.ToString();

                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);

                //青旅呼叫中心订单
                string Ccflag = "";
                Int32 CcId = 0;
                if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1)
                {
                    if (Request.Cookies["CallCenterOrderId"] != null)
                    {
                        string CookieCcid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["CallCenterOrderId"].Value));
                        CcId = MyConvert.ConToInt(CookieCcid);
                    }

                    if (CcId > 0)
                    {
                        Ccflag = "<font style=\"color: #CC0000; font-size: 15px; font-weight: bold\">您当前预定的是呼叫中心订单</font><br>";
                    }
                }

                User_Memo = Ccflag + DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                User_Info = "" + DS.Tables[0].Rows[0]["OrderName"].ToString();
                if (DS.Tables[0].Rows[0]["OrderTel"].ToString().Length > 0) User_Info += " &nbsp; 电话：" + DS.Tables[0].Rows[0]["OrderTel"].ToString();
                if (DS.Tables[0].Rows[0]["OrderMobile"].ToString().Length > 0) User_Info += " &nbsp; 手机：" + DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                if (DS.Tables[0].Rows[0]["OrderFax"].ToString().Length > 0) User_Info += " &nbsp; 传真：" + DS.Tables[0].Rows[0]["OrderFax"].ToString();
                if (DS.Tables[0].Rows[0]["OrderEmail"].ToString().Length > 0) User_Info += " &nbsp; 邮件：" + DS.Tables[0].Rows[0]["OrderEmail"].ToString();

                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) > 0)
                {
                    CuisesList = PurchaseClass.GetOrderCuisesRoom(OrderId);
                    GuestList = PurchaseClass.GetCuisesOrderGuest(OrderId);
                }
                else
                {
                    if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "Cruises") CuisesList = PurchaseClass.GetOrderCuises(OrderId);
                    GuestList = PurchaseClass.GetOrderGuest(OrderId);
                }
                if (Convert.ToString(Session["Online_UserCompany"]).Length == 0)
                {
                    PolicyHide = "";
                }
                PriceList = PurchaseClass.GetOrderPrice(OrderId);

                PurchaseClass.OrderExtendInfo ExtendInfo = new PurchaseClass.OrderExtendInfo();
                ExtendInfo = PurchaseClass.GetOrderExtInfo(OrderId, ProType, ProClass);
                Contract = ExtendInfo.Contract;
                Invoice = ExtendInfo.Invoice;

                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) > 0)
                {

                    DinnerInfo = "<div class=\"m detail\"><UL class=tab><LI class=curr>晚餐时间<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">";
                    DinnerInfo += "<ul class=order>" + ExtendInfo.Dinner + "</ul>";
                    DinnerInfo += "</div></div>";
                }
                
                if (Invoice == null) Invoice = "<li><div class=oname>不需要发票</div><div class=oinfo></div></li>";
            
                //同行付款方式显示
                RB1 = "checked=\"checked\"";
                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    if (Convert.ToString(Session["Online_UserCompany"])=="1")
                    {
                        hide2 = "hide";
                    }
                    else 
                    {
                        if (Convert.ToString(Session["Online_RebateFlag"]) == "1")
                        {
                            RB1 = "";
                            RB2 = "checked=\"checked\"";
                        }  
                    }
                }
                else
                {
                    hide2 = "hide";
                }

            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

            if (Request.Cookies["HuiFeng2014"] != null)
            {
                huifeng = "hide";
                huifeng1 = "";
                hide1 = "hide";
                tebieshuoming = "会员工号及特别说明";
                //mendian_hide = "hide";
            }
        }
    }
}