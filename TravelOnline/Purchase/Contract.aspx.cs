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
    public partial class Contract : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, EndDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice;
        public string OrderName, Address, TourstName, Tel, Sex, Age, User_Info, User_Memo, GuestList, Invoice, AutoId, Contracts, JSONData, LineDays;
        public string step1, step2, step3, ProType, ProClass, PlanNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            step1 = "style=\"DISPLAY:none\"";
            step2 = "style=\"DISPLAY:none\"";
            step3 = "style=\"DISPLAY:none\"";
            if (Request.QueryString["DeptId"] == null)
            {
                if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                {
                    LoadTempOrder();
                }
                else
                {
                    Response.Redirect("~/index.html", true);
                }
            }
            else
            {
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
                SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", QueryId);
            }

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) == DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                if (Request.QueryString["DeptId"] == null)
                {
                    if (Convert.ToString(Session["Online_UserId"]) == DS.Tables[0].Rows[0]["OrderUser"].ToString() || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                    {
                    }
                    else
                    {
                        Response.Redirect("~/index.html", true);
                    }
                }
                else
                {
                    if (Request.QueryString["DeptId"] == DS.Tables[0].Rows[0]["DeptId"].ToString() && Request.QueryString["OrderId"] == DS.Tables[0].Rows[0]["OrderId"].ToString() && Request.QueryString["OrderNo"] == DS.Tables[0].Rows[0]["AutoId"].ToString())
                    {
                    }
                    else
                    {
                        Response.Write("无任何数据！");
                        Response.End();
                    }
                }
                ProType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                ProClass = DS.Tables[0].Rows[0]["ProductClass"].ToString();
                PlanNo = DS.Tables[0].Rows[0]["PlanNo"].ToString();

                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();

                OrderName = DS.Tables[0].Rows[0]["OrderName"].ToString();
                //Address = DS.Tables[0].Rows[0]["OrderName"].ToString();
                Tel = DS.Tables[0].Rows[0]["OrderTel"].ToString() + " " + DS.Tables[0].Rows[0]["OrderMobile"].ToString();

                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();

                AutoId = "订单号：" + DS.Tables[0].Rows[0]["AutoId"].ToString();

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                EndDate = string.Format("{0:yyyy年MM月dd日}", Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"]).AddDays(Convert.ToInt32(DS.Tables[0].Rows[0]["LineDays"]) - 1));
                if (BeginDate == "1900年01月01日")
                {
                    BeginDate = "";
                    EndDate = "";
                }


                User_Memo = DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                User_Info = "" + DS.Tables[0].Rows[0]["OrderName"].ToString();
                if (DS.Tables[0].Rows[0]["OrderTel"].ToString().Length > 0) User_Info += " &nbsp; 电话：" + DS.Tables[0].Rows[0]["OrderTel"].ToString();
                if (DS.Tables[0].Rows[0]["OrderMobile"].ToString().Length > 0) User_Info += " &nbsp; 手机：" + DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                if (DS.Tables[0].Rows[0]["OrderFax"].ToString().Length > 0) User_Info += " &nbsp; 传真：" + DS.Tables[0].Rows[0]["OrderFax"].ToString();
                if (DS.Tables[0].Rows[0]["OrderEmail"].ToString().Length > 0) User_Info += " &nbsp; 邮件：" + DS.Tables[0].Rows[0]["OrderEmail"].ToString();

                //PriceList = PurchaseClass.GetOrderPrice(OrderId);
                //GuestList = PurchaseClass.GetOrderGuest(OrderId);

                PurchaseClass.OrderExtendInfo ExtendInfo = new PurchaseClass.OrderExtendInfo();
                ExtendInfo = PurchaseClass.GetOrderExtInfo(OrderId, ProType, ProClass);
                Contracts = ExtendInfo.Contract;
                //Invoice = ExtendInfo.Invoice;
                //JSONData = PurchaseClass.GetTimePoint(OrderId);
                
                switch (DS.Tables[0].Rows[0]["ProductType"].ToString())
                {
                    case "OutBound":
                        step2 = "";
                        break;
                    case "InLand":
                        step1 = "";
                        break;
                    case "Cruises":
                        step3 = "";
                        break;
                    default:
                        break;
                }

                SqlQueryText = string.Format("select top 1 *,DATEDIFF(yy, BirthDay, GETDATE()) AS age from OL_GuestInfo where OrderId='{0}'", QueryId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    TourstName = DS.Tables[0].Rows[0]["GuestName"].ToString();
                    if (DS.Tables[0].Rows[0]["GuestName"].ToString() == "F")
                    {
                        Sex = "女";
                    }
                    else
                    {
                        Sex = "男";
                    }
                    Age = DS.Tables[0].Rows[0]["age"].ToString();
                }
                
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}