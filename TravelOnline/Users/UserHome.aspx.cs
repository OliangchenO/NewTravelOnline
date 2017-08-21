using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Users
{
    public partial class UserHome : BasePage
    {
        public string hyhide = "", hyname="普通会员";
        public string Allintegral;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (!IsPostBack)
            {
                if (MyDataBaseComm.getScalar("select id from OL_Member where uid='" + Convert.ToString(Session["Online_UserId"]) + "'") != null)
                {
                    hyname = "积分会员";
                    hyhide = "hide";
                }
                if (Convert.ToString(Session["Online_UserCompany"]).Length > 0)
                {
                    hyname = "门市或同业账号";
                    hyhide = "hide";
                }
                Allintegral = MyDataBaseComm.getScalar("select isnull(sum(integral),0) from OL_Integral where uid='" + Convert.ToString(Session["Online_UserId"]) + "'");
                this.GridView_DataBind();
                this.GridView2_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("SELECT top 5 *,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderFlag<>'9' and OrderUser='{0}'", Convert.ToString(Session["Online_UserId"]));
            DateTime date = DateTime.Today;
            date = date.AddMonths(-3);
            sqlstr = string.Format("{0} and OrderTime >= '{1}' ", sqlstr, date.ToString());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);

            string sortExpression = this.GridView1.Attributes["SortExpression"];
            string sortDirection = this.GridView1.Attributes["SortDirection"];
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                DS.Tables[0].DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }

            this.GridView1.DataSource = DS.Tables[0].DefaultView;
            this.GridView1.DataBind();

            
        }

        protected void GridView2_DataBind()
        {
            DateTime date = DateTime.Today;
            date = date.AddMonths(-3);
            string sqlstr = string.Format("SELECT top 5 id,lineid,inputdate,(select top 1 linename from OL_Line where mislineid=OL_Favorite.lineid) as linename,(select top 1 price from OL_Line where mislineid=OL_Favorite.lineid) as price from OL_Favorite where uid='{0}' and inputdate >= '{1}' order by inputdate desc ", Convert.ToString(Session["Online_UserId"]), date.ToString());
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(sqlstr);
            this.GridView2.DataSource = DS1.Tables[0].DefaultView;
            this.GridView2.DataBind();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = string.Format("<a href=\"/line/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "lineid"), DataBinder.Eval(e.Row.DataItem, "linename"));
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[2].Text = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["ProductType"], DS.Tables[0].Rows[0]["ProductClass"], DS.Tables[0].Rows[0]["LineID"]);
                e.Row.Cells[3].Text = string.Format("<a href=\"/line/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));

                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = "待确认";
                        break;
                    case "1":
                        e.Row.Cells[1].Text = "占位";
                        break;
                    case "2":
                        e.Row.Cells[1].Text = "确认";
                        break;
                    case "3":
                        e.Row.Cells[1].Text = "完成";
                        break;
                    case "8":
                        e.Row.Cells[1].Text = "取消";
                        break;
                    default:
                        break;
                }
            }
        }

    }
}