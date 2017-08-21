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

namespace TravelOnline.Purchase
{
    public partial class OrderView : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice, CuisesList;
        public string hide, hide1, User_Info, User_Memo, GuestList, Contract, Invoice, AutoId, JSONData, DinnerInfo,PayHide = "hide";
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            if (Request.QueryString["DeptId"] == null)
            {
                //if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                //{
                //    LoadTempOrder();
                //}
                //else
                //{
                //    Response.Redirect("~/index.html", true);
                //}
                LoadTempOrder();
            }
            else { 
                LoadTempOrder(); 
            }
        }

        protected void LoadTempOrder()
        {
            string SqlQueryText;
            if (Request.QueryString["Flag"] == "Temp")
            {
                SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", QueryId);
            }
            else
            {
                SqlQueryText = string.Format("select *,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", QueryId);
            }
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) == DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                //if (Request.QueryString["DeptId"] == null)
                //{
                //    if (Convert.ToString(Session["Online_UserId"]) == DS.Tables[0].Rows[0]["OrderUser"].ToString() || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                //    {
                //    }
                //    else
                //    {
                //        Response.Redirect("~/index.html", true);
                //    }
                //}
                //else
                //{
                //    if (Request.QueryString["DeptId"] == DS.Tables[0].Rows[0]["DeptId"].ToString() && Request.QueryString["OrderId"] == DS.Tables[0].Rows[0]["OrderId"].ToString() && Request.QueryString["OrderNo"] == DS.Tables[0].Rows[0]["AutoId"].ToString())
                //    {
                //    }
                //    else
                //    {
                //        Response.Write("无任何数据！");
                //        Response.End();
                //    }
                //}
                if (Request.QueryString["Flag"] != "Temp")
                {
                    decimal Fee = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString());
                    decimal Pay = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Pay"].ToString());
                    decimal Yue = Fee - Pay;
                    string orderflag = DS.Tables[0].Rows[0]["OrderFlag"].ToString();
                    if (orderflag == "1" || orderflag == "2")
                    {
                        if (Yue > 0)
                        {
                            if (DS.Tables[0].Rows[0]["ProductType"].ToString()!= "Coupon")
                            {
                                PayHide = "";
                            }
                        }
                    }
                }
                string ProType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                if (ProType == "Coupon")
                {
                    hide = "hide";
                }

                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    hide1 = "hide";
                }
                string ProClass = DS.Tables[0].Rows[0]["ProductClass"].ToString();

                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();

                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2") hide1 = "hide";

                AutoId = "订单号：" + DS.Tables[0].Rows[0]["AutoId"].ToString();

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);

                User_Memo = DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                User_Info = "" + DS.Tables[0].Rows[0]["OrderName"].ToString();
                if (DS.Tables[0].Rows[0]["OrderTel"].ToString().Length > 0) User_Info += " &nbsp; 电话：" + DS.Tables[0].Rows[0]["OrderTel"].ToString();
                if (DS.Tables[0].Rows[0]["OrderMobile"].ToString().Length > 0) User_Info += " &nbsp; 手机：" + DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                if (DS.Tables[0].Rows[0]["OrderFax"].ToString().Length > 0) User_Info += " &nbsp; 传真：" + DS.Tables[0].Rows[0]["OrderFax"].ToString();
                if (DS.Tables[0].Rows[0]["OrderEmail"].ToString().Length > 0) User_Info += " &nbsp; 邮件：" + DS.Tables[0].Rows[0]["OrderEmail"].ToString();

                //if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "Cruises") CuisesList = PurchaseClass.GetOrderCuises(OrderId);

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
                PriceList = PurchaseClass.GetOrderPrice(OrderId);
                //GuestList = PurchaseClass.GetOrderGuest(OrderId);


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
                if (Invoice == null)
                {
                    SqlQueryText = string.Format("select * from OL_Invoice where OrderId='{0}'", QueryId);
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Invoice += string.Format("<li><div class=oname>发票抬头：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["InvoiceName"].ToString());
                        Invoice += string.Format("<li><div class=oname>发票明细：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["InvoiceContent"].ToString());
                        Invoice += string.Format("<li><div class=oname>收件人：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["GuestName"].ToString());
                        Invoice += string.Format("<li><div class=oname>手机号码：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["GuestMobile"].ToString());
                        Invoice += string.Format("<li><div class=oname>配送方式：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["InvoiceFlag"].ToString());
                        Invoice += string.Format("<li><div class=oname>收件地址：</div><div class=oinfo>{0}</div></li>", DS.Tables[0].Rows[0]["GuestAddress"].ToString());
                    }
                    else
                    {
                        Invoice = "<li><div class=oname>不需要发票</div><div class=oinfo></div></li>";
                    }
                }

                JSONData = PurchaseClass.GetTimePoint(OrderId);
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}