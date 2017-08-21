using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Cruises
{
    public partial class CruisesCompany : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
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
            string sqlstr = "SELECT *,(select count(id) from CR_Ship where comid=CR_Company.id) as ships FROM CR_Company where 1=1 ";
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and cname like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            
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
                e.Row.Cells[5].Text = string.Format("<a class=order href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                //e.Row.Cells[5].Text += string.Format(" <a class=order href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">船队</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                e.Row.Cells[5].Text += string.Format(" <A class=order href=\"CruisesShip.aspx?id={0}\" target=\"_blank\">船队</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                //e.Row.Cells[5].Text += string.Format(" <A class=order href=\"CruisesPic.aspx?id={0}\" target=\"_blank\">图片</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                

            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

    }
}