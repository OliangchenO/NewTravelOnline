using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class IntegralList : BasePage
    {
        public string uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);

            uid = Request.QueryString["id"];
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("select * from OL_Integral left join OL_LoginUser on uid = OL_LoginUser.Id left join OL_Order on OL_Integral.orderid = OL_Order.OrderId where uid = '{0}'", uid);
            if (DropDownList1.SelectedValue.Equals("0")) sqlstr = string.Format("{0} and flag = 0 and getdate < enddate ", sqlstr);
            if (DropDownList1.SelectedValue.Equals("1")) sqlstr = string.Format("{0} and flag = 0 and getdate = enddate ", sqlstr);
            if (DropDownList1.SelectedValue.Equals("2")) sqlstr = string.Format("{0} and flag = 0 and getdate > enddate ", sqlstr);
            if (DropDownList1.SelectedValue.Equals("3")) sqlstr = string.Format("{0} and flag = 1 ", sqlstr);

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
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); 
                e.Row.Cells[2].Text = string.Format("<a href=\"/line/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "lineid"), DataBinder.Eval(e.Row.DataItem, "LineName"));
                
                switch (DataBinder.Eval(e.Row.DataItem, "flag").ToString())
                {
                    case "1":
                        e.Row.Cells[1].Text = "使用";
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        break;
                    default:
                        if (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "getdate")) < Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "enddate")))
                            e.Row.Cells[1].Text = "获得";
                        if (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "getdate")) == Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "enddate")))
                            e.Row.Cells[1].Text = "活动";
                        if (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "getdate")) > Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "enddate")))
                            e.Row.Cells[1].Text = "获赠";
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