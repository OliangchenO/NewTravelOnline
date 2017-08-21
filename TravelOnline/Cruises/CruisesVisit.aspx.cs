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
    public partial class CruisesVisit : BasePage
    {
        public string Cid, shipid, CruisesShip;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];
            if (!IsPostBack)
            {
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Cid + "'");
                LoadDeptInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDeptInfo()
        {
            string sqlstr = "select days as id,visit as dataname from CR_Route where visit <> '' and lineid='" + Cid + "'";

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM View_CR_Visit where lineid='" + Cid + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and visitname like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and days = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            

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
                if (DataBinder.Eval(e.Row.DataItem, "orders").ToString() != "0")
                {
                    e.Row.Cells[9].Text = string.Format("<A class=order href=\"../CruisesOrder/CruisesGuest.aspx?visitid={0}&lineid={1}\" target=\"_blank\" >{2}</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), Cid, DataBinder.Eval(e.Row.DataItem, "orders").ToString());
                }
                if (DataBinder.Eval(e.Row.DataItem, "sellflag").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[10].Text = string.Format("<A class=order href=\"javascript:void(0)\" onclick=\"Edit({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}