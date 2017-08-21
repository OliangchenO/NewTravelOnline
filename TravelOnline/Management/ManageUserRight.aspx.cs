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
    public partial class ManageUserRight : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@1") == -1)
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
            string sqlstr = "SELECT id,rightname,rightflag FROM OL_UserRight where 1=1 ";
            if (TextBox1.Text.Trim().Length > 0) sqlstr = string.Format("{0} and RightName like '%{1}%' ", sqlstr, TextBox1.Text.Trim());
            if (DropDownList1.Text != "All") sqlstr = string.Format("{0} and RightFlag = '{1}' ", sqlstr, DropDownList1.Text);

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
                switch (DataBinder.Eval(e.Row.DataItem, "RightFlag").ToString())
                {
                    case "Menu":
                        e.Row.Cells[2].Text = "模块操作";
                        e.Row.Cells[4].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                        break;
                    case "Operation":
                        e.Row.Cells[2].Text = "模块操作";
                        e.Row.Cells[4].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                        break;
                    case "ChangePassWord":

                        break;
                    case "EditInfo":
                        
                        break;
                    default:
                        break;
                }
            }


            //e.Row.Cells[1].Visible = false;  //把指定列隐藏，主要用于隐藏id列
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}