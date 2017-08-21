using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;

namespace TravelOnline.CruisesOrder
{
    public partial class FirstStep : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, shipid, PriceList, dinnerstring;
        public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap, hide1, hide2, hide3, hide4, hide5;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            //LoadTempOrder();
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
            string SqlQueryText = string.Format("select *,(select count(1) from Pre_Ticket where flag='0' and begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}') as precount,(select top 1 VisitSell from OL_Line where MisLineId=OL_TempOrder.lineid) as VisitSell from OL_TempOrder where OrderId='{0}'", QueryId, Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            //Response.Write(SqlQueryText);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                switch (DS.Tables[0].Rows[0]["PayType"].ToString())
                {
                    case "1":
                        RB1 = "checked=\"checked\"";
                        break;
                    case "2":
                        RB2 = "checked=\"checked\"";
                        break;
                    case "3":
                        RB3 = "checked=\"checked\"";
                        break;
                    default:
                        RB1 = "checked=\"checked\"";
                        break;
                }

                PriceList = PurchaseClass.GetPriceList(Convert.ToInt32(Nums), Convert.ToInt32(Adults), Convert.ToInt32(Childs), DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["PlanId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"].ToString(), DS.Tables[0].Rows[0]["OrderId"].ToString(), DS.Tables[0].Rows[0]["ProductType"].ToString(), DS.Tables[0].Rows[0]["precount"].ToString(), Convert.ToString(Session["Online_UserId"]), Convert.ToInt32(shipid), DS.Tables[0].Rows[0]["VisitSell"].ToString());
                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    hide1 = "hide";
                    hide3 = "hide";
                    hide5 = "hide";
                    RB3 = "checked=\"checked\"";
                    RB1 = "";
                    RB2 = "";
                }
                else
                {
                    if (Request.Cookies["OnlyOnlinePay"] != null || Request.Cookies["OnlyIcbcPay"] != null)
                    {
                        hide2 = "hide";
                        hide5 = "hide";
                        RB1 = "checked=\"checked\"";
                        Preference = PurchaseClass.GetPreferenceList(Convert.ToInt32(Nums), DS.Tables[0].Rows[0]["LineID"].ToString());
                        BranchOption = PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "PayOption");

                    }
                    else
                    {
                        hide2 = "hide";
                        Preference = PurchaseClass.GetPreferenceList(Convert.ToInt32(Nums), DS.Tables[0].Rows[0]["LineID"].ToString());
                        BranchOption = PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "PayOption");

                    }
                    
                }

                string dinner = MyDataBaseComm.getScalar("select dinner from OL_Line where MisLineId='" + DS.Tables[0].Rows[0]["LineID"].ToString() + "'");

                string Dtime1 = "";
                string Dtime2 = "";
                string Dnum1 = "";
                string Dnum2 = "";
                string chk = "";
                string[] Dtime = dinner.Split('|');
                if (Dtime.Length > 1)
                {

                    Dtime1 = Dtime[0].Split("@".ToCharArray())[0];
                    Dnum1 = Dtime[0].Split("@".ToCharArray())[1];
                    Dtime2 = Dtime[1].Split("@".ToCharArray())[0];
                    Dnum2 = Dtime[1].Split("@".ToCharArray())[1];
                    //.Split("/".ToCharArray())[0]
                    dinnerstring = string.Format("<input tgs=\"{1}\" id=\"Radio1\" type=\"radio\" name=\"dinner\" value=\"1@{0}\" checked=\"checked\" /><label for=\"Radio1\" class=\"radiobtn\">{0}</label>", Dtime1, MyConvert.ConToInt(Dnum1));
                    dinnerstring += string.Format("<input tgs=\"{1}\" id=\"Radio2\" type=\"radio\" name=\"dinner\" value=\"2@{0}\" /><label for=\"Radio2\" class=\"radiobtn\">{0}</label>", Dtime2, MyConvert.ConToInt(Dnum2));
                }
                else
                {
                    chk = "checked=\"checked\"";
                    dinnerstring += string.Format("<input tgs=\"0\" id=\"Radio3\" type=\"radio\" name=\"dinner\" value=\"3@All\" {1} /><label for=\"Radio3\" class=\"radiobtn\">{0}</label>", "皆可安排", chk);
                }

                //dinnerstring += string.Format("<input tgs=\"0\" id=\"Radio3\" type=\"radio\" name=\"dinner\" value=\"3@All\" {1} /><label for=\"Radio3\" class=\"radiobtn\">{0}</label>", "以上时间均可", chk);
                //if (DS.Tables[0].Rows[0]["VisitSell"].ToString() == "1") hide4 = "hide";
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}