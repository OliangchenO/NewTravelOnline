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
    public partial class ManageComment : BasePage
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
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM OL_Comment where commentstatus='COMMENTED' ";
            if (tb_linename.Text.Trim().Length > 0) sqlstr = string.Format("{0} and lineName like '%{1}%' ", sqlstr, tb_linename.Text.Trim());
            if (tb_lineid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and lineId = '{1}' ", sqlstr, tb_lineid.Text.Trim());
            if (tb_orderid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and orderId = '{1}' ", sqlstr, tb_orderid.Text.Trim());
            if (tb_startDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and commentTime >= '{1}' ", sqlstr, Convert.ToDateTime(tb_startDate.Text));
            if (tb_endDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and commentTime <= '{1}' ", sqlstr, Convert.ToDateTime(tb_endDate.Text));
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
                if ("UNAUDIT".Equals(DataBinder.Eval(e.Row.DataItem, "auditStatus")))
                {
                    e.Row.Cells[7].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"auditComment({0})\">审核</A>", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                }
                else
                {
                    e.Row.Cells[7].Text = "已审核";
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