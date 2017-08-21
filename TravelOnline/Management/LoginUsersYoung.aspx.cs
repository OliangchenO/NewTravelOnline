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
    public partial class LoginUsersYoung : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (!IsPostBack)
            {
                TB_Bdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(-1));
                TB_Edate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("SELECT Id,UserName,UserEmail,Company,Mobile,RegTime,LastLoginTime,LoginCount,userType,case when sex = 1 then '男' else '女' end sex from OL_LoginUser where (UserType='upload' or UserType='unUpload')", Convert.ToString(Session["Online_UserId"]));
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            if (TB_Email.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserEmail like '%{1}%' ", sqlstr, TB_Email.Text.Trim());
            if (TB_UserType.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserType = '{1}' ", sqlstr, TB_UserType.Text.Trim());
            if (TB_Bdate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and RegTime >= '{1}' ", sqlstr, Convert.ToDateTime(TB_Bdate.Text));
                }
                catch
                { }
            }

            if (TB_Edate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and RegTime <= '{1}' ", sqlstr, Convert.ToDateTime(TB_Edate.Text).AddDays(1));
                }
                catch
                { }
            }

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
                e.Row.Cells[1].Text = string.Format("<div class=uemail title=\"{0}\">{0}</div>", DataBinder.Eval(e.Row.DataItem, "UserEmail"));
                switch (DataBinder.Eval(e.Row.DataItem, "UserType").ToString())
                {
                    case "upload":
                        e.Row.Cells[6].Text = string.Format("<a class=tip href=\"javascript:void(0);\" onclick=\"ShowInfo('{0}')\">查看</a>&nbsp;<a class=tip href=\"\\AD\\2017Young\\File\\{0}.xlsx\"\">下载</a>", DataBinder.Eval(e.Row.DataItem, "Id"));
                        break;
                    case "UnUpload":
                        e.Row.Cells[6].Text = string.Format("<a class=tip href=\"javascript:void(0);\" onclick=\"ShowInfo('{0}')\">查看</a>", DataBinder.Eval(e.Row.DataItem, "Id"));
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