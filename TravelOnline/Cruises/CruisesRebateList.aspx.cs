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
    public partial class CruisesRebateList : BasePage
    {
        public string Cid, roomid, lineid, shipid, CruisesShip;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            roomid = Request.QueryString["roomid"];
            lineid = Request.QueryString["lineid"];
            if (!IsPostBack)
            {
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + lineid + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT CR_RoomAllot.companyid,CR_RoomAllot.typeid,CR_RoomAllot.roomid,CR_RoomAllot.allotflag,CR_RoomAllot.sellflag,CR_RoomAllot.roomname,CR_RoomAllot.company,CR_RoomAllot.roomcode,CR_RoomAllot.price,CR_Rebate.id,CR_Rebate.begindate,CR_Rebate.enddate,CR_Rebate.infos,CR_Rebate.price as rprice,CR_Rebate.flag FROM CR_RoomAllot,CR_Rebate where CR_RoomAllot.id=CR_Rebate.allotid and CR_RoomAllot.lineid='" + lineid + "'";
            if (DropDownList1.Text != "all")
            {
                sqlstr = string.Format("{0} and flag ='{1}' ", sqlstr, DropDownList1.Text);
            }
            if (roomid != null) sqlstr = string.Format("{0} and CR_Rebate.roomid ='{1}'", sqlstr, roomid);
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
                if (DataBinder.Eval(e.Row.DataItem, "allotflag").ToString() == "0")
                {
                    e.Row.Cells[3].Text = "公开销售";
                }
                //e.Row.ForeColor=
                if (DataBinder.Eval(e.Row.DataItem, "sellflag").ToString() == "1")
                {
                    e.Row.Cells[3].Text += " 暂停";
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
                if (DataBinder.Eval(e.Row.DataItem, "flag").ToString() == "1")
                {
                    e.Row.Cells[6].Text = " 观光";
                }
                else
                {
                    e.Row.Cells[6].Text = " 舱房";
                }
                    
                e.Row.Cells[10].Text = string.Format("<A class=order href=\"javascript:void(0)\" onclick=\"Edit({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                e.Row.Cells[7].Text = string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "begindate")) + " ～ " + string.Format("{0:MM-dd}", DataBinder.Eval(e.Row.DataItem, "enddate"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}