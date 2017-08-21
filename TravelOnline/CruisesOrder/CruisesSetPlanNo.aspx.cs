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
    public partial class CruisesSetPlanNo : BasePage
    {
        public string Cid, AutoId, LineId, CruisesShip, CombineId;
        public DataSet DS1;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            LineId = Request.QueryString["LineId"];
            AutoId = Request.QueryString["AutoId"];
            CombineId = Request.QueryString["CombineId"];
            if (!IsPostBack)
            {
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + LineId + "'");
                this.GridView_DataBind();
            }
        }


        protected override void GridView_DataBind()
        {
            CruisesShip = ViewState["CruisesShip"].ToString();
            string sqlstr = "SELECT * FROM View_GuestRoomInfo where lineid='" + LineId + "'";
            if (MyConvert.ConToInt(CombineId) > 0)
            {
                sqlstr += " and AutoId in (select autoid from CR_Combine where combineid = '" + CombineId + "')";
            }
            else
            {
                sqlstr += " and AutoId in (" + AutoId + ")";
            }
            //if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and DinnerClaim = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            
            switch (DropDownList2.SelectedValue)
            {
                case "all":
                    break;
                case "0":
                    sqlstr += " and (PlanAllotid=0 or PlanAllotid is null)";
                    break;
                case "1":
                    sqlstr += " and PlanAllotid>0";
                    break;
            }

            switch (DropDownList1.SelectedValue)
            {
                case "all":
                    break;
                case "1":
                    sqlstr += " and visaflag='1'";
                    break;
                case "2":
                    sqlstr += " and visaflag='2'";
                    break;
                case "3":
                    sqlstr += " and visaflag is null";
                    break;
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
                //e.Row.Cells[7].Text = GetRoomName(DS1.Tables[0], DataBinder.Eval(e.Row.DataItem, "id").ToString());
                if (DataBinder.Eval(e.Row.DataItem, "visaflag").ToString() == "1") e.Row.Cells[5].Text = "团签";
                if (DataBinder.Eval(e.Row.DataItem, "visaflag").ToString() == "2") e.Row.Cells[5].Text = "个签";
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}