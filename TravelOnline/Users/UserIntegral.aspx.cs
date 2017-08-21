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
    public partial class UserIntegral : BasePage
    {
        public string Allintegral;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (!IsPostBack)
            {
                if (MyDataBaseComm.getScalar("select id from OL_Member where uid='" + Convert.ToString(Session["Online_UserId"]) + "'") == null)
                {
                    Response.Redirect("/login/joinmember.aspx", true);
                    return;
                }
                ViewState["Allintegral"] = MyDataBaseComm.getScalar("select isnull(sum(integral),0) from OL_Integral where uid='" + Convert.ToString(Session["Online_UserId"]) + "'");
                this.GridView_DataBind();
            }
           
        }

        protected override void GridView_DataBind()
        {
            Allintegral = ViewState["Allintegral"].ToString();
            string sqlstr = string.Format("SELECT OL_Integral.*,OL_Order.AutoId,OL_Order.OrderFlag,isnull(OL_Order.LineName,'活动获取') LineName,OL_Order.BeginDate,OL_Order.OrderNums,OL_Order.Price from OL_Integral left join OL_Order on OL_Integral.orderid=OL_Order.orderid where OL_Integral.uid='{0}'", Convert.ToString(Session["Online_UserId"]));
            DateTime date = DateTime.Today;
            date = date.AddMonths(-1);
            if (DropDownList1.Text == "1")
            {
                sqlstr = string.Format("{0} and OL_Integral.getdate >= '{1}' ", sqlstr, date.ToString());
            }
            else
            {
                sqlstr = string.Format("{0} and OL_Integral.getdate < '{1}' ", sqlstr, date.ToString());
            }

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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                e.Row.Cells[2].Text = string.Format("<a href=\"/line/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "lineid"), DataBinder.Eval(e.Row.DataItem, "LineName"));

                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                //switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                //{
                //    case "0":
                //        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //        break;
                //    case "1":
                //        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //        break;
                //    case "2":
                //        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //        break;
                //    case "3":
                //        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">完成</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //        break;
                //    case "8":
                //        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //        break;
                //    default:
                //        break;
                //}
                switch (DataBinder.Eval(e.Row.DataItem, "flag").ToString())
                {
                    case "1":
                        e.Row.Cells[1].Text = "使用";
                        //e.Row.Cells[8].Text = "";
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        break;
                    default:
                        e.Row.Cells[1].Text = "获得";
                        break;
                }

            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}