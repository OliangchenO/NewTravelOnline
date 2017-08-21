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
    public partial class ManageUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@3") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                LoadDeptInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDeptInfo()
        {
            string sqlstr = "select id,DeptName from OL_Dept";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "请选择");
        }
        
        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT *,(select RightName from OL_UserRight where id=OL_ManageUser.UserRight) as RightName,(select DeptName from OL_Dept where id=OL_ManageUser.UserDept) as DeptName FROM OL_ManageUser where 1=1 ";
            if (TB_LoginName.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LoginName like '%{1}%' ", sqlstr, TB_LoginName.Text.Trim());
            if (TB_UserName.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserName like '%{1}%' ", sqlstr, TB_UserName.Text.Trim());
            if (DropDownList1.Text != "请选择") sqlstr = string.Format("{0} and UserDept = '{1}' ", sqlstr, DropDownList1.Text);

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
                e.Row.Cells[6].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

    }
}