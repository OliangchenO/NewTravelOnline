using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline
{
    public partial class ActivityOrder : BasePage
    {
        public string ActInfoMain_ID;
        public string ActOrderID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            //if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@3@1") == -1)
            //{
            //    Response.Write("没有操作权限！");
            //    Response.End();
            //}
            if (!IsPostBack)
            {
                ActInfoMain_ID = Request.QueryString["ActInfoMain_ID"];
                this.txbActInfoMain_ID.Text = ActInfoMain_ID;
                this.GridView_DataBind();
            }
            else
            {
                this.txbActInfoMain_ID.Text = "";
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM [Act_Order] Where [Status]='1'";
            if (this.txbActInfoMain_ID.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and ActInfoMain_ID ='{0}'", this.txbActInfoMain_ID.Text);
            }
            if (this.txbMobile.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and OrderMobile like '%{0}%'", this.txbMobile.Text);
            }

            if (this.txbGuestName.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and GuestName like '%{0}%'", this.txbGuestName.Text);
            }

            if (this.txbOL_OrderID.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and OL_OrderID like '%{0}%'", this.txbOL_OrderID.Text);
            }
            //if (TextBox1.Text.Trim().Length > 0) sqlstr = string.Format("{0} and RightName like '%{1}%' ", sqlstr, TextBox1.Text.Trim());
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

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[7].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"DisableActOrder({0},{1})\">取消</a> &nbsp;<a href=\"/Activity/Print.aspx?ActOrderID={1}\" target=_blank>打印</a>&nbsp",DataBinder.Eval(e.Row.DataItem, "ActInfoMain_ID"),DataBinder.Eval(e.Row.DataItem, "ActOrderID"));
            }
        }
    }
}