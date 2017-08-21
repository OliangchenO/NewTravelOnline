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
    public partial class TempOrder : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList;
        public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap, hide1, hide2;
        public string huifeng, huifeng1="hide";
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
            string SqlQueryText = string.Format("select *,(select count(1) from Pre_Ticket where userid='{1}' and flag='0' and begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}') as precount from OL_TempOrder where OrderId='{0}'", QueryId, Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            //Response.Write(SqlQueryText);
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
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);                
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
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
                if (Request.Cookies["OnlyOnlinePay"] != null)
                {
                    hide2 = "hide";
                    RB1 = "checked=\"checked\"";
                }

                if (Request.Cookies["OnlyIcbcPay"] != null)
                {
                    hide2 = "hide";
                    RB1 = "checked=\"checked\"";
                }
                
                //BranchOption = PurchaseClass.GetBranch(0, "Option");
                BranchOption = PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "PayOption");
                //BranchMap = PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchMap");

                PriceList = PurchaseClass.GetPriceList(Convert.ToInt32(Nums), Convert.ToInt32(Adults), Convert.ToInt32(Childs), DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["PlanId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"].ToString(), DS.Tables[0].Rows[0]["OrderId"].ToString(), DS.Tables[0].Rows[0]["ProductType"].ToString(), DS.Tables[0].Rows[0]["precount"].ToString(), Convert.ToString(Session["Online_UserId"]),0,"0");
                Preference = PurchaseClass.GetPreferenceList(Convert.ToInt32(Nums), DS.Tables[0].Rows[0]["LineID"].ToString());

                StringBuilder Strings = new StringBuilder();
                Strings.Append("<li id=Pre1 class=\"order1\"><div class=oname>支付说明：</div><div class=oinfo>请于订单生成后24小时内，通过网上支付方式支付500元预付款，过时订单自动取消；并于出发前20日付清全款，出发前14日未付款视为自动放弃。</div>");
                Strings.Append("<ul id=PrePrice class=hide>");
                Strings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>优惠合计</div><div class=tpic></div></li>");
                Strings.Append(string.Format("<li class=pre_priceli tps=PrePrice tag=PrePrice id=Preference><div class=ftype>在线支付优惠</div><div class=fname>每人立减{0}元</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice id=Pre_Price>{0}</span></div>", "0"));
                Strings.Append("<div class=fnum>");
                Strings.Append(string.Format("<select class=psel><option value=\"{0}\">{0}</option></select>", "0"));
                Strings.Append(string.Format("</div><div class=fsum><span class=PrePrice>&yen;</span> <span class=PrePrice id=SumPre_Price>{0}</span></div><div id=pic class=fpic></div></li>", "0"));
                Strings.Append("</ul></li>");

                if (Request.Cookies["HuiFeng2014"] != null)
                {
                    hide2 = "hide";
                    RB1 = "checked=\"checked\"";
                    huifeng = "hide";
                    huifeng1 = "";
                    Preference = Strings.ToString();
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }            
        }
    }
}