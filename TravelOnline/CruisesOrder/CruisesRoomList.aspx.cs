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
    public partial class CruisesRoomList : BasePage
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
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Cid + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "select GuestName,listid from View_GuestRoomInfo where listid in (select id from cr_roomlist where orderflag = '0' and lineid='" + Cid + "' and roomid='" + Request.QueryString["roomid"] + "')";
            DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(sqlstr);

            //Response.Write(sqlstr);
            //Response.End();

            CruisesShip = ViewState["CruisesShip"].ToString();
            sqlstr = "SELECT *,(select companyname from company where id=View_CR_RoomList.ordercompany) as company,";
            sqlstr += "(select top 1 combineid from CR_Combine where autoid=View_CR_RoomList.autoid) as combineid ";
            sqlstr = string.Format("{0} from View_CR_RoomList where roomid='{1}' ", sqlstr, Request.QueryString["roomid"]);
            sqlstr = string.Format("{0} and LineID='{1}' ", sqlstr, Cid);
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and OrderName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (companyid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and ordercompany = '{1}' ", sqlstr, companyid.Text.Trim());
            if (MyConvert.ConToInt(AutoId.Text.Trim()) > 0)
            {
                sqlstr = string.Format("{0} and AutoId ={1} ", sqlstr, AutoId.Text.Trim());
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
                AllNums = DS.Tables[0].Compute("sum(peoples)", "true").ToString();
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
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "合计";
                e.Row.Cells[5].Text = AllNums;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                rebate = "0";
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "rebate").ToString()) > 0) rebate = "1";

                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "peoples").ToString() + " (" + DataBinder.Eval(e.Row.DataItem, "adults").ToString() + "+" + DataBinder.Eval(e.Row.DataItem, "childs").ToString() + ")";

                e.Row.Cells[7].Text = GetRoomName(DS1.Tables[0], DataBinder.Eval(e.Row.DataItem, "id").ToString());
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "combineid").ToString()) > 0)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "combineid").ToString() != DataBinder.Eval(e.Row.DataItem, "autoid").ToString())
                    {
                        e.Row.Cells[7].Text += "<br>与" + DataBinder.Eval(e.Row.DataItem, "combineid").ToString() + "合并";
                    }
                    else
                    {
                        e.Row.Cells[7].Text += "<br>关联合并订单<br>";
                    }
                }

                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[9].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
                        break;
                    case "1":
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[9].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
                        break;
                    case "2":
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[9].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
                        break;
                    case "3":
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">完成</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "8":
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    default:
                        break;
                }
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
            DataRow[] drs = dt.Select("listid='" + listid + "'");
            if (drs.Count() > 0)
            {
                foreach (DataRow dr in drs)
                {
                    GuestName += dr["GuestName"].ToString() + "，";
                }
            }
            if (GuestName.Length > 2) GuestName = GuestName.Substring(0, GuestName.Length - 1);
            return GuestName;
        }
    }
}