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
    public partial class LineRoomAllot : BasePage
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
            shipid = Request.QueryString["shipid"];
            if (!IsPostBack)
            {
                CruisesShip = MyDataBaseComm.getScalar("select typename + ' - ' +  roomname from CR_ShipRoom where id='" + roomid + "'");
                this.GridView_DataBind();
            }
        }
        
        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT *,(SELECT COUNT(id) FROM CR_RoomList WHERE orderflag = '0' AND allotid = dbo.CR_RoomAllot.id) AS sellroom,(select count(1) from CR_Rebate where allotid=CR_RoomAllot.id) as rebates FROM CR_RoomAllot where lineid='" + lineid + "' and roomid='" + roomid + "'";
            //if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and cname like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            //if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and typeid = '{1}' ", sqlstr, DropDownList1.SelectedValue);
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
                //e.Row.Cells[2].Text = e.Row.Cells[2].Text + " " + DataBinder.Eval(e.Row.DataItem, "id").ToString();
                //e.Row.ForeColor=
                if (DataBinder.Eval(e.Row.DataItem, "sellflag").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[12].Text = string.Format("<A class=order href=\"javascript:void(0)\" onclick=\"Edit({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}