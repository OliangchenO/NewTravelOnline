using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.Class.Common;
using System.Data;


namespace TravelOnline.Activity
{
    public partial class ActivityManage : BasePage
    {
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
                TB_Bdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(-1));
                TB_Edate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(+1));
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM dbo.Act_ActInfoMain";
            sqlstr += string.Format(" where Start='{0}'", ddlAct_MainStart.Text);
            if (this.txbAct_Name.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and ActivityName='{0}'", txbAct_Name.Text);
            }
            if (this.txbAct_Name.Text.Trim().Length > 0)
            {
                sqlstr += string.Format(" and ActivityName='{0}'", txbAct_Name.Text);
            }
            if (TB_Bdate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and ActivityStartTime >= '{1}' ", sqlstr, Convert.ToDateTime(TB_Bdate.Text));
                }
                catch
                { }
            }

            if (TB_Edate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and ActivityStartTime <= '{1}' ", sqlstr, Convert.ToDateTime(TB_Edate.Text).AddDays(1));
                }
                catch
                { }
            }
            sqlstr += " Order by ActInfoMain_ID";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);

            this.GridView1.DataSource = DS.Tables[0].DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[10].Text = string.Format("<a href=\"/Activity/ActivityOrder.aspx?ActInfoMain_ID={0}\" target=_blank>查看</a> &nbsp;<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>&nbsp", DataBinder.Eval(e.Row.DataItem, "ActInfoMain_ID"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        protected void ddlAct_MainStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlAct_MainStart.SelectedValue == "正常")
            {
                this.lblDisable.Text = "禁用";
            }
            else
            {
                this.lblDisable.Text = "启动";
            }
        }
    }
}