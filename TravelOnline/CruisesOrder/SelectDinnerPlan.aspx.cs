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

namespace TravelOnline.CruisesOrder
{
    public partial class SelectDinnerPlan : BasePage
    {
        public string Cid, LineId, GuestId, flag, SelectNums, hide1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            LineId = Request.QueryString["LineId"];
            flag = Request.QueryString["flag"];
            GuestId = Request.QueryString["Id"];
            SelectNums = GuestId.Split(',').Length.ToString();
            if (flag == "Plan") hide1 = "hide";
            if (!IsPostBack)
            {
                if (flag == "Dinner") LoadInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadInfo()
        {
            string sqlstr = "select DinnerTime from CR_DinnerNo where lineid='" + LineId + "' group by DinnerTime";

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr="";
            if (flag == "Dinner")
            {
                sqlstr = string.Format("select *,TabelNo as NoName,(Berth-Nums) as surplus from View_CR_DinnerNo where Berth>Nums and (Berth-Nums)>={1} and Lineid='{0}'", LineId, SelectNums);
                if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and DinnerTime = '{1}' ", sqlstr, DropDownList1.SelectedValue);
                if (AutoId.Text.Trim().Length > 0)
                {
                    sqlstr = string.Format("{0} and TabelNo like '%{1}%' ", sqlstr, AutoId.Text.Trim());
                }

            }
            else
            {
                sqlstr = string.Format("select *,PlanNo as NoName,(Berth-Nums) as surplus from View_CR_PlanNo where Berth>Nums and (Berth-Nums)>{1} and Lineid='{0}'", LineId, SelectNums);
                if (AutoId.Text.Trim().Length > 0)
                {
                    sqlstr = string.Format("{0} and PlanNo like '%{1}%' ", sqlstr, AutoId.Text.Trim());
                }
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
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (flag == "Dinner") e.Row.Cells[1].Text = "桌号：" + DataBinder.Eval(e.Row.DataItem, "NoName").ToString() + "";
                if (flag == "Plan") e.Row.Cells[1].Text = "第 " + DataBinder.Eval(e.Row.DataItem, "NoName").ToString() + " 团";
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GridView1.PageIndex = 0;
        //    this.GridView_DataBind();
        //}

    }
}