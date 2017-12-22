using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;

namespace TravelOnline.Purchase
{
    public partial class OrderSuccess : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice;
        public string User_Info, User_Memo, GuestList, Contract, Invoice, Price, AutoId, AdjustTime;
        public string hide1, hide2, hide3, PayUrl, BranchMap, hide4 = "hide", hide5 = "", hide6 = "hide", hide7 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();   

            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]).ToString();
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();

                AdjustTime = "24";
                //InLand  ProductType
                if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "InLand")
                {
                    AdjustTime = "12";
                }

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);

                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "30" && DS.Tables[0].Rows[0]["PayFlag"].ToString() == "0")
                {
                    hide2 = "hide";
                    PayUrl = string.Format("<A class=\"btn-link btn-personal\" href=\"/Pay/PayNow.aspx?OrderId={0}\" target=\"_blank\">立刻支付</A>",OrderId);
                }
                else
                {
                    hide1 = "hide";
                }
                //DS.Tables[0].Rows[0]["ProductType"].ToString() == "FreeTour" || 
                if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "Cruises" && MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) == 0)
                {
                    PayUrl = "";
                    hide1 = "hide";
                    hide2 = "";
                }
                hide3 = "hide";
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2")
                {
                    PayUrl = "<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchName") + "</DIV>";
                    hide1 = "hide";
                    hide2 = "hide";
                    hide3 = "";
                    BranchMap = "<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchMap") + "</DIV>";
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

            if (Request.Cookies["HuiFeng2014"] != null)
            {
                hide1 = "hide";
                hide2 = "hide";
                hide3 = "hide";
                hide4 = "";
                hide5 = "hide";
                hide6 = "";
                hide7 = "hide";
                //mendian_hide = "hide";
            }
        }
    }
}