using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Company
{
    public partial class CompanyInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM Company where 1=1 ";
            if (tb_companyname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and companyname like '%{1}%' ", sqlstr, tb_companyname.Text.Trim());
            if (tb_city.Text.Trim().Length > 0) sqlstr = string.Format("{0} and city like '%{1}%' ", sqlstr, tb_city.Text.Trim());
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
                //if (DataBinder.Eval(e.Row.DataItem, "Preferences").ToString() == "1") e.Row.Cells[6].Text = "√";
                //if (DataBinder.Eval(e.Row.DataItem, "Recommend").ToString() == "1") e.Row.Cells[7].Text = "√";
                //if (DataBinder.Eval(e.Row.DataItem, "Sale").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Edit({0})\">{1}</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "companyname").ToString());
                e.Row.Cells[6].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Dept({0})\">部门</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                e.Row.Cells[6].Text += string.Format("<A href=\"javascript:void(0)\" onclick=\"User({0})\">用户</A>", DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}