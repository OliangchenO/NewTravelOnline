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
using System.Text.RegularExpressions;
using TravelOnline.Class.Common;

namespace TravelOnline.CruisesOrder
{
    public partial class ThirdStep : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice;
        public string VisitListInfo,dinner,dinnerstring, Dtime1, Dtime2, Dnum1, Dnum2;
        public string RB1, RB2, RB3, RB4, RC1, RC2, hide1, CompanyAddress;
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

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);

                VisitListInfo = PurchaseClass.GetCruisesVisitList(OrderId,"");

                //dinner = MyDataBaseComm.getScalar("select dinner from OL_Line where MisLineId='" + DS.Tables[0].Rows[0]["LineID"].ToString() + "'");
                
                //Dtime1 = "";
                //Dtime2 = "";
                //Dnum1 = "";
                //Dnum2 = "";
                //string chk = "";
                //string[] Dtime = dinner.Split('|');
                //if (Dtime.Length > 1)
                //{

                //    Dtime1 = Dtime[0].Split("@".ToCharArray())[0];
                //    Dnum1 = Dtime[0].Split("@".ToCharArray())[1];
                //    Dtime2 = Dtime[1].Split("@".ToCharArray())[0];
                //    Dnum2 = Dtime[1].Split("@".ToCharArray())[1];
                //    //.Split("/".ToCharArray())[0]
                //    dinnerstring = string.Format("<input tgs=\"{1}\" id=\"Radio1\" type=\"radio\" name=\"dinner\" value=\"1@{0}\" checked=\"checked\" /><label for=\"Radio1\" class=\"radiobtn\">{0}</label>", Dtime1, MyConvert.ConToInt(Dnum1));
                //    dinnerstring += string.Format("<input tgs=\"{1}\" id=\"Radio2\" type=\"radio\" name=\"dinner\" value=\"2@{0}\" /><label for=\"Radio2\" class=\"radiobtn\">{0}</label>", Dtime2, MyConvert.ConToInt(Dnum2));
                //}
                //else
                //{
                //    chk = "checked=\"checked\"";
                //}

                //dinnerstring += string.Format("<input tgs=\"0\" id=\"Radio3\" type=\"radio\" name=\"dinner\" value=\"3@All\" {1} /><label for=\"Radio3\" class=\"radiobtn\">{0}</label>", "以上时间均可", chk);
                
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        
    }
}