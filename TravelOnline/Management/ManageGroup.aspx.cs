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
    public partial class ManageGroup : BasePage
    {
        public string LineType, ClassType, Types;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "select g.id,g.MisLineId,g.GroupDate,g.pre_price,g.EditTime,g.EditUserId,g.EditUserName,g.Discount,g.Num,l.LineName from ol_line l,OL_GroupPlan g where g.MisLineId=l.MisLineId ";
            if (tb_cid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and MisLineId = '{1}' ", sqlstr, tb_cid.Text.Trim());
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
                e.Row.Cells[3].Text = string.Format("<A href=\"/line/{0}.html\" target=_blank>{1}</A>", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "LineName").ToString());
                e.Row.Cells[4].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "GroupDate"));
                e.Row.Cells[8].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());

            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}