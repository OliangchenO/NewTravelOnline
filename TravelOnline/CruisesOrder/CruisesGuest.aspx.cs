using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.CruisesOrder
{
    public partial class CruisesGuest : BasePage
    {
        public string Cid, shipid, CruisesShip, rebate;
        public string AllNums;
        public DataSet DS1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@3") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];

            if (!IsPostBack)
            {
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Request.QueryString["lineid"] + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "select guestid,visitname,BusNo from View_CR_VisitList where flag='0' ";
            if (Request.QueryString["id"] != null)
            {
                sqlstr = string.Format("{0} and guestid in (select id from View_GuestRoomInfo where PlanAllotid='{1}') ", sqlstr, Request.QueryString["id"]);
            }

            //按观光线路
            if (Request.QueryString["visitid"] != null)
            {
                sqlstr = string.Format("{0} and visitid='{1}'", sqlstr, Request.QueryString["visitid"]);
            }

            //按车号
            if (Request.QueryString["carid"] != null)
            {
                sqlstr = string.Format("{0} and Busid='{1}'", sqlstr, Request.QueryString["carid"]);
            }

            //按餐桌
            if (Request.QueryString["dinnerid"] != null)
            {
                sqlstr = string.Format("{0} and guestid in (select id from View_GuestRoomInfo where DinnerId='{1}') ", sqlstr, Request.QueryString["dinnerid"]);
            }

            //按房间
            if (Request.QueryString["roomid"] != null)
            {
                sqlstr = string.Format("{0} and guestid in (select id from View_GuestRoomInfo where RoomNoid='{1}') ", sqlstr, Request.QueryString["roomid"]);
            }

            DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(sqlstr);

            //Response.Write(sqlstr);
            //Response.End();

            CruisesShip = ViewState["CruisesShip"].ToString();
            sqlstr = "SELECT *,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";

            //按观光线路
            if (Request.QueryString["visitid"] != null)
            {
                sqlstr = string.Format("{0} from View_GuestRoomInfo where id in (select guestid from CR_VisitList where visitid='{1}' and flag='0') ", sqlstr, Request.QueryString["visitid"]);
            }
            
            //按分团
            if (Request.QueryString["id"] != null)
            {
                sqlstr = string.Format("{0} from View_GuestRoomInfo where PlanAllotid='{1}' ", sqlstr, Request.QueryString["id"]);
            }
            //按车号
            if (Request.QueryString["carid"] != null)
            {
                sqlstr = string.Format("{0} from View_GuestRoomInfo where id in (select guestid from CR_VisitList where Busid='{1}' and flag='0') ", sqlstr, Request.QueryString["carid"]);
            }
            //按餐桌
            if (Request.QueryString["dinnerid"] != null)
            {
                sqlstr = string.Format("{0} from View_GuestRoomInfo where DinnerId='{1}' ", sqlstr, Request.QueryString["dinnerid"]);
            }

            //按房间
            if (Request.QueryString["roomid"] != null)
            {
                sqlstr = string.Format("{0} from View_GuestRoomInfo where RoomNoid='{1}' ", sqlstr, Request.QueryString["roomid"]);
            }

            sqlstr = string.Format("{0} and LineID='{1}' ", sqlstr, Request.QueryString["lineid"]);
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and OrderName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (MyConvert.ConToInt(AutoId.Text.Trim()) > 0)
            {
                sqlstr = string.Format("{0} and AutoId ={1} ", sqlstr, AutoId.Text.Trim());
            }
            sqlstr = string.Format("{0} order by IsLeader desc ", sqlstr);
            
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
                AllNums = DS.Tables[0].Rows.Count.ToString();
            }
            catch
            {
                AllNums = "0";
            }
            this.GridView1.DataSource = DS.Tables[0].DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[4].Text = "合计";
            //    e.Row.Cells[5].Text = AllNums;
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[9].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
            //If (DataBinder.Eval(e.Item.DataItem, "ciceroni").ToString = "-1") Then
            //    e.Item.Cells(1).Text += "<font color=red><b>[领队]</b></font>"
            //End If
                if (DataBinder.Eval(e.Row.DataItem, "IsLeader").ToString() == "1")
                {
                    e.Row.Cells[3].Text += " <font color=red><b>[领队]</b></font>";
                }

                e.Row.Cells[8].Text = GetRoomName(DS1.Tables[0], DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        protected string GetRoomName(DataTable dt, string listid)
        {
            string GuestName = "";
            DataRow[] drs = dt.Select("guestid='" + listid + "'");
            if (drs.Count() > 0)
            {
                foreach (DataRow dr in drs)
                {
                    if (dr["BusNo"].ToString().Length > 0) GuestName += dr["visitname"].ToString() + dr["BusNo"].ToString() + "，";
                }
            }
            if (GuestName.Length > 2) GuestName = GuestName.Substring(0, GuestName.Length - 1);
            return GuestName;
        }
    }
}