using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.Cruises
{
    public partial class CruisesDamage : BasePage
    {
        public string Cid, deptid, companyid, deptname, CruisesShip, LineId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            LineId = Request.QueryString["LineId"];
            if (!IsPostBack)
            {
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + LineId + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM CR_Damage where lineid='" + LineId + "'";
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
                if (DataBinder.Eval(e.Row.DataItem, "flag").ToString() == "1")
                {
                    e.Row.Cells[2].Text = "订金";
                }
                else
                {
                    e.Row.Cells[2].Text = "总额";
                }
                e.Row.Cells[3].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "begindate")) + " ～ " + string.Format("{0:MM-dd}", DataBinder.Eval(e.Row.DataItem, "enddate"));
                e.Row.Cells[6].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Edit('{0}','{1:yyyy-MM-dd}','{2:yyyy-MM-dd}','{3}','{4}','{5}')\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "begindate"), DataBinder.Eval(e.Row.DataItem, "enddate"), DataBinder.Eval(e.Row.DataItem, "flag").ToString(), DataBinder.Eval(e.Row.DataItem, "rate").ToString(), DataBinder.Eval(e.Row.DataItem, "infos").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}