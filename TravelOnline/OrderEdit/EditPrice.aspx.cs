using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;

namespace TravelOnline.OrderEdit
{
    public partial class EditPrice : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList;
        public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("没有操作权限！");
            Response.End();

            QueryId = Request.QueryString["OrderId"];
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@6@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            LoadTempOrder();

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                PriceList = PurchaseClass.GetEditPriceList(OrderId, Convert.ToInt32(Nums), Convert.ToInt32(Adults), Convert.ToInt32(Childs), DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["PlanId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"].ToString(), DS.Tables[0].Rows[0]["ProductType"].ToString());

                switch (DS.Tables[0].Rows[0]["PayType"].ToString())
                {
                    case "1":
                        RB1 = "checked=\"checked\"";
                        break;
                    case "2":
                        //BranchOption = PurchaseClass.GetBranch(Convert.ToInt32(DS.Tables[0].Rows[0]["BranchId"].ToString()), "PayOption");
                        RB2 = "checked=\"checked\"";
                        break;
                    default:
                        RB1 = "checked=\"checked\"";
                        break;
                }
                
                //PriceList = PurchaseClass.GetPriceList(Convert.ToInt32(Nums), Convert.ToInt32(Adults), Convert.ToInt32(Childs), DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["PlanId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"].ToString());
                Preference = PurchaseClass.GetPreferenceList(Convert.ToInt32(Nums), DS.Tables[0].Rows[0]["LineID"].ToString());
                BranchOption = PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "PayOption");

            }
            else
            {
                Response.Write("订单不存在！");
                Response.End();
            }
        }
    }
}