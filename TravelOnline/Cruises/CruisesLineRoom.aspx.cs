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
    public partial class CruisesLineRoom : BasePage
    {
        public string Cid, shipid, CruisesShip, ThirdRoomInfo;
        public string sum1, sum2, sum3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];
            shipid = Request.QueryString["shipid"];
            if (!IsPostBack)
            {
                ThirdRoomInfo = MyDataBaseComm.getScalar("select isnull(sum(nums),0) from View_ThirdRoomCheck where LineId='" + Cid + "'");
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Cid + "'");
                LoadDeptInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDeptInfo()
        {
            string sqlstr = "select id,dataname from InitData where ftype='RoomType' and id in (select typeid from CR_ShipRoom where shipid='" + shipid + "' group by typeid)";

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            //有bug cname无法查询
            string sqlstr = "SELECT *,(select count(id) from CR_RoomAllot where roomid=CR_ShipRoom.id and lineid='" + Cid + "') as allot,(select ISNULL(sum(nums),0) from CR_RoomAllot where roomid=CR_ShipRoom.id and lineid='" + Cid + "') as nums,(SELECT COUNT(id) FROM CR_RoomList WHERE orderflag = '0' AND roomid=CR_ShipRoom.id and lineid='" + Cid + "') AS sells FROM CR_ShipRoom where shipid='" + shipid + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and cname like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and typeid = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            if (DropDownList2.Text != "all")
            {
                sqlstr = string.Format("{0} and id in (select roomid from CR_RoomAllot where lineid={1} and shipid={2} group by roomid) ", sqlstr, Cid, shipid);
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
            try
            {
                sum1 = DS.Tables[0].Compute("sum(allot)", "true").ToString();
                sum2 = DS.Tables[0].Compute("sum(nums)", "true").ToString();
                sum3 = DS.Tables[0].Compute("sum(sells)", "true").ToString();
            }
            catch
            {
                sum1 = "0";
                sum2 = "0";
                sum3 = "0";
            }
            this.GridView1.DataSource = DS.Tables[0].DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "合计";
                e.Row.Cells[6].Text = sum1;
                e.Row.Cells[7].Text = sum2;
                e.Row.Cells[8].Text = sum3;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "sells").ToString() != "0")
                {
                    e.Row.Cells[7].Text = string.Format("<A class=order href=\"../CruisesOrder/CruisesLineOrder.aspx?roomid={0}&id={1}\" target=\"_blank\" >{2}</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), Cid, DataBinder.Eval(e.Row.DataItem, "nums").ToString());

                    e.Row.Cells[8].Text = string.Format("<A class=order href=\"../CruisesOrder/CruisesRoomList.aspx?roomid={0}&id={1}\" target=\"_blank\" >{2}</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), Cid, DataBinder.Eval(e.Row.DataItem, "sells").ToString());
                }
                e.Row.Cells[9].Text = string.Format("<A class=order href=\"LineRoomAllot.aspx?roomid={0}&lineid={1}&shipid={2}\" target=\"_blank\" >分配</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), Cid, shipid);
                //e.Row.Cells[9].Text += string.Format(" <A class=order href=\"CruisesRoom.aspx?id={0}\" target=\"_blank\">房型</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}