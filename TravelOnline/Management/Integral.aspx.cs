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
    public partial class Integral : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@6@7") == -1)
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
            string sqlstr = string.Format("select Id UID,UserName,Mobile,(select isnull(sum(integral),0) from OL_Integral where uid = OL_LoginUser.Id and flag = 0) AllIntegral,(select isnull(sum(integral),0) from OL_Integral where uid = OL_LoginUser.Id and flag = 1) UseIntegral from OL_LoginUser where 1=1 ");
            if (User_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserName like '%{1}%' ", sqlstr, User_Name.Text.Trim());
            if (Mobile.Text.Trim().Length > 0) sqlstr = string.Format("{0} and Mobile like '%{1}%' ", sqlstr, Mobile.Text.Trim());

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
                //e.Row.Cells[4].Text += string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">新增积分</a>", DataBinder.Eval(e.Row.DataItem, "UID"));
                e.Row.Cells[4].Text += string.Format(" <a class=order href=\"IntegralList.aspx?id={0}\" target=_blank>查看</a>", DataBinder.Eval(e.Row.DataItem, "UID"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}