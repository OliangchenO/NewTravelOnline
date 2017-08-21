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
    public partial class FriendLink : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@4") == -1)
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
            //string sqlstr = "SELECT * from OL_FriendLink";
            string sqlstr = "SELECT * FROM OL_FriendLink order by rankid";
            if (DropDownList1.Text != "0") sqlstr = string.Format("SELECT * FROM OL_FriendLink where LinkType = '{0}' order by rankid", DropDownList1.Text);

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
                e.Row.Cells[4].Text = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", DataBinder.Eval(e.Row.DataItem, "LinkUrl")); 

                e.Row.Cells[6].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));

                switch (DataBinder.Eval(e.Row.DataItem, "LinkType").ToString())
                {
                    case "1":
                        e.Row.Cells[2].Text = "首页";
                        break;
                    case "2":
                        e.Row.Cells[2].Text = "出境";
                        break;
                    case "3":
                        e.Row.Cells[2].Text = "国内";
                        break;
                    case "4":
                        e.Row.Cells[2].Text = "自由行";
                        break;
                    case "5":
                        e.Row.Cells[2].Text = "邮轮";
                        break;
                    case "6":
                        e.Row.Cells[2].Text = "签证";
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