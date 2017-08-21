using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class OutBoundInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@6") == -1)
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
            string sqlstr = "SELECT id,AfficheName,AfficheType from OL_Affiche where AfficheFlag='5' ";
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            if (DropDownList1.Text != "全部板块") sqlstr = string.Format("{0} and AfficheType = '{1}' ", sqlstr, DropDownList1.Text);

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
                e.Row.Cells[4].Text = string.Format("<a href=\"ShowNews.aspx?id={0}\" target=_blank>查看</a> &nbsp;<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";

                switch (DataBinder.Eval(e.Row.DataItem, "AfficheType").ToString())
                {
                    case "OutBound1":
                        e.Row.Cells[2].Text = "出境须知1";
                        break;
                    case "OutBound2":
                        e.Row.Cells[2].Text = "出境须知2";
                        break;
                    case "OutBound3":
                        e.Row.Cells[2].Text = "出境须知3";
                        break;
                    case "OutBound4":
                        e.Row.Cells[2].Text = "出境须知4";
                        break;
                    case "OutBound5":
                        e.Row.Cells[2].Text = "出境须知5";
                        break;
                    case "OutBound6":
                        e.Row.Cells[2].Text = "出境须知6";
                        break;
                    default:
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