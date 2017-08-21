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
    public partial class CruisesSetDinner : BasePage
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
                LoadInfo();
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + LineId + "'");
                this.GridView_DataBind();
            }
        }

        protected void LoadInfo()
        {
            string sqlstr = "select (CASE DinnerClaim WHEN 'All' THEN '未指定' ELSE DinnerClaim END) AS DinnerClaim from View_GuestRoomInfo where lineid='" + LineId + "' and AutoId in (" + AutoId + ") group by DinnerClaim";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
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
            switch (DropDownList1.SelectedValue)
            {
                case "全部":
                    break;
                case "未指定":
                    sqlstr = string.Format("{0} and DinnerClaim = '{1}' ", sqlstr, "All");
                    break;
                default:
                    sqlstr = string.Format("{0} and DinnerClaim = '{1}' ", sqlstr, DropDownList1.SelectedValue);
                    break;
            }
            switch (DropDownList2.SelectedValue)
            {
                case "all":
                    break;
                case "0":
                    sqlstr += " and (DinnerId=0 or DinnerId is null)";
                    break;
                case "1":
                    sqlstr += " and DinnerId>0";
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
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[7].Text = GetRoomName(DS1.Tables[0], DataBinder.Eval(e.Row.DataItem, "id").ToString());
                if (DataBinder.Eval(e.Row.DataItem, "DinnerClaim").ToString() == "All")
                {
                    e.Row.Cells[5].Text = "未指定";
                }
                //else
                //{
                //    e.Row.Cells[2].Text = "总额";
                //}
                //e.Row.Cells[3].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "begindate")) + " ～ " + string.Format("{0:MM-dd}", DataBinder.Eval(e.Row.DataItem, "enddate"));
                //e.Row.Cells[6].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Edit('{0}','{1:yyyy-MM-dd}','{2:yyyy-MM-dd}','{3}','{4}','{5}')\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "begindate"), DataBinder.Eval(e.Row.DataItem, "enddate"), DataBinder.Eval(e.Row.DataItem, "flag").ToString(), DataBinder.Eval(e.Row.DataItem, "rate").ToString(), DataBinder.Eval(e.Row.DataItem, "infos").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        //protected string GetRoomName(DataTable dt, string listid)
        //{
        //    string GuestName = "";
        //    DataRow[] drs = dt.Select("listid='" + listid + "'");
        //    if (drs.Count() > 0)
        //    {
        //        foreach (DataRow dr in drs)
        //        {
        //            GuestName += dr["GuestName"].ToString() + "，";
        //        }
        //    }
        //    if (GuestName.Length > 2) GuestName = GuestName.Substring(0, GuestName.Length - 1);
        //    return GuestName;
        //}
    }
}