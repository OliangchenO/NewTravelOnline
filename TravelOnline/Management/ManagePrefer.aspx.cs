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
    public partial class ManagePrefer : BasePage
    {
        public string LineType,ClassType,Types;
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
                tb_startDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(-1));
                tb_endDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(+1));
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM OL_Preferential where 1=1 ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LineName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (tb_cid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LineId = '{1}' ", sqlstr, tb_cid.Text.Trim());
            if (tb_startDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and startDate >= '{1}' ", sqlstr, Convert.ToDateTime(tb_startDate.Text));
            if (tb_endDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and endDate <= '{1}' ", sqlstr, Convert.ToDateTime(tb_endDate.Text));
            if (tb_pStartDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and pStartDate >= '{1}' ", sqlstr, Convert.ToDateTime(tb_pStartDate.Text));
            if (tb_pEndDate.Text.Trim().Length > 0) sqlstr = string.Format("{0} and pEndDate <= '{1}' ", sqlstr, Convert.ToDateTime(tb_pEndDate.Text));
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
                e.Row.Cells[2].Text += "<br>" + DataBinder.Eval(e.Row.DataItem, "LineId").ToString();
                e.Row.Cells[3].Text = string.Format("<A href=\"/line/{1}.html\" target=_blank>{2}</A>", DataBinder.Eval(e.Row.DataItem, "LineType").ToString(), DataBinder.Eval(e.Row.DataItem, "LineId").ToString(), DataBinder.Eval(e.Row.DataItem, "LineName").ToString());
                e.Row.Cells[4].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "startDate")) + "至" + string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "endDate"));
                if (DataBinder.Eval(e.Row.DataItem, "pStartDate").ToString().Trim() != "" || DataBinder.Eval(e.Row.DataItem, "pEndDate").ToString().Trim() != "")
                {
                    e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "pStartDate")) + "至" + string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "pEndDate"));
                }
                else
                {
                    e.Row.Cells[5].Text = "不限报名时间";
                }
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