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
    public partial class MyCoupon : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string PreDate;
            PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string sqlstr = string.Format("SELECT *,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where userid='{0}'", Convert.ToString(Session["Online_UserId"]));
            
            switch (DropDownList1.Text)
            {
                case "1":
                    sqlstr += " and flag = '0' and enddate>='" + PreDate + "'";
                    break;
                case "2":
                    sqlstr += " and flag = '1' ";
                    break;
                case "3":
                    sqlstr += " and flag = '0' and enddate<'" + PreDate + "'";
                    break;
                default:
                    break;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (DataBinder.Eval(e.Row.DataItem, "flag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = "可用";
                        //e.Row.Cells[0].Text = "";
                        e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "begindate")) + " 至 " + string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "enddate"));
                        break;
                    case "1":
                        //e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); 
                        e.Row.Cells[1].Text = "已用";
                        e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd hh:ss}", DataBinder.Eval(e.Row.DataItem, "usedate")) + " 已使用<br>订单号：" + string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId"));
                        break;
                    default:
                        //e.Row.Cells[0].Text = "";
                        break;
                }

                if (DropDownList1.Text == "3")
                {
                    e.Row.Cells[1].Text = "已过期";
                    e.Row.Cells[5].Text = string.Format("过期日期：{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "enddate"));
                        
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