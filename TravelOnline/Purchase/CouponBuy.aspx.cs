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

namespace TravelOnline.Purchase
{
    public partial class CouponBuy : System.Web.UI.Page
    {
        public string Uid, BeginDate, sellprice, par, memo, logourl, BuyButton;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Uid = Request.QueryString["uid"];
            if (!IsPostBack)
            {
                LoadCoupon();
            }
        }

        protected void LoadCoupon()
        {
            string SqlQueryText = string.Format("select *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff from Pre_Policy where uid='{0}'", Uid);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                logourl = DS.Tables[0].Rows[0]["picurl"].ToString();
                Uid = DS.Tables[0].Rows[0]["Uid"].ToString();
                sellprice = DS.Tables[0].Rows[0]["sellprice"].ToString();
                par = DS.Tables[0].Rows[0]["par"].ToString();
                memo = DS.Tables[0].Rows[0]["memo"].ToString();
                BeginDate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]) + " 至 " + string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                int nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellnums"].ToString()); 
                int ff = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ff"].ToString());
                if (ff < nums)
                {
                    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"GoToOrder()\">立即购买</A>";
                }
                else
                {
                    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\">已售完</A>";
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}