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
    public partial class CruisesLineOrder : BasePage
    {
        public string Cid, shipid, CruisesShip, rebate, VisaInfo;
        public string AllNums, allrooms, allgather, allfanli;
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
                //CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Cid + "'");
                VisaInfo = GetVisaInfo();
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Cid + "'");
                this.GridView_DataBind();
            }
        }

        protected string GetVisaInfo()
        {
            string aa = "";
            string sqlstr = string.Format("select count(1) as nums,visaflag from View_GuestRoomInfo where LineID='{0}' group by visaflag", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    switch (DS.Tables[0].Rows[i]["visaflag"].ToString())
                    {
                        case "1":
                            aa += "团签：" + DS.Tables[0].Rows[i]["nums"].ToString() + "人&nbsp;&nbsp;";
                            break;
                        case "2":
                            aa += "个签：" + DS.Tables[0].Rows[i]["nums"].ToString() + "人&nbsp;&nbsp;";
                            break;
                        default:
                            aa += "未设置：" + DS.Tables[0].Rows[i]["nums"].ToString() + "人&nbsp;&nbsp;";
                            break;
                    }
                }
            }
            return aa;
        }
        
        protected override void GridView_DataBind()
        {
            CruisesShip = ViewState["CruisesShip"].ToString();
            string sqlstr = "SELECT *,(select companyname from company where id=OL_Order.ordercompany) as company,";
            sqlstr += "(select top 1 combineid from CR_Combine where autoid=OL_Order.autoid) as combineid,";
            sqlstr += "(select isnull(sum(peoples),0) from CR_RoomList where orderflag='0' and roomnoid>0 and OrderId=OL_Order.OrderId) as roomcount,";
            sqlstr += "(select count(1) from CR_RoomList where orderflag='0' and OrderId=OL_Order.OrderId) as rooms,";
            sqlstr += "(select count(1) from OL_GuestInfo where flag='0' and DinnerId>0 and OrderId=OL_Order.OrderId) as dinnercount,";
            sqlstr += "(select count(1) from OL_GuestInfo where flag='0' and PlanAllotid>0 and OrderId=OL_Order.OrderId) as plancount,";
            sqlstr += "(select count(1) from CR_VisitList where flag='0' and Busid>0 and OrderId=OL_Order.OrderId) as buscount";
            sqlstr += " from OL_Order where OrderFlag<>'9' and shipid>0 ";
            sqlstr = string.Format("{0} and LineID='{1}' ", sqlstr, Cid);
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and OrderName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (companyid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and ordercompany = '{1}' ", sqlstr, companyid.Text.Trim());
            if (DropDownList1.Text != "all")
            {
                sqlstr = string.Format("{0} and OrderFlag in ({1}) ", sqlstr, DropDownList1.Text);
            }
            switch (DropDownList2.Text)
            {
                case "0":
                    sqlstr = string.Format("{0} and ordercompany ={1} ", sqlstr, DropDownList2.Text);
                    break;
                case "1":
                    sqlstr = string.Format("{0} and ordercompany ={1} ", sqlstr, DropDownList2.Text);
                    break;
                case "2":
                    sqlstr = string.Format("{0} and ordercompany >={1} ", sqlstr, DropDownList2.Text);
                    break;
                default:
                    break;
            }
            if (MyConvert.ConToInt(AutoId.Text.Trim()) > 0)
            {
                sqlstr = string.Format("{0} and AutoId ={1} ", sqlstr, AutoId.Text.Trim());
            }

            //Request.QueryString["roomid"] 
            if (Request.QueryString["roomid"] != null)
            {
                sqlstr = string.Format("{0} and OrderId in (select orderid from View_RoomOrderList where roomid='{1}') ", sqlstr, Request.QueryString["roomid"]);
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
                AllNums = DS.Tables[0].Compute("sum(OrderNums)", "true").ToString();
                allrooms = DS.Tables[0].Compute("sum(rooms)", "true").ToString();
                allgather = DS.Tables[0].Compute("sum(Price)", "true").ToString();
                allfanli = DS.Tables[0].Compute("sum(rebate)", "true").ToString();
            }
            catch
            {
                AllNums = "0";
                allrooms = "0";
                allgather = "0";
                allfanli = "0";
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
                e.Row.Cells[6].Text = allrooms;
                e.Row.Cells[7].Text = allgather;
                e.Row.Cells[8].Text = allfanli;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[5].Text = string.Format("<a href=\"/{0}/{1}/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));
                rebate = "0";
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "rebate").ToString()) > 0) rebate = "1";
                e.Row.Cells[11].Text += "房" + DataBinder.Eval(e.Row.DataItem, "roomcount").ToString() + " 餐" + DataBinder.Eval(e.Row.DataItem, "dinnercount").ToString() + " 车" + DataBinder.Eval(e.Row.DataItem, "buscount").ToString() + " 团" + DataBinder.Eval(e.Row.DataItem, "plancount").ToString();
                
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "combineid").ToString()) > 0)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "combineid").ToString() != DataBinder.Eval(e.Row.DataItem, "autoid").ToString())
                    {
                        e.Row.Cells[11].Text += "<br>与" + DataBinder.Eval(e.Row.DataItem, "combineid").ToString() + "合并";
                    }
                    else
                    {
                        e.Row.Cells[11].Text += "<br>关联合并订单<br>";
                    }
                }
                //roomcount,dinnercount,buscount
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[12].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
                        break;
                    case "10":
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[12].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
                        break;
                    case "30":
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[12].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs=\"{0}\" dept=\"{1}\" reb=\"{2}\" comb=\"{3}\" aut=\"{4}\">操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate, DataBinder.Eval(e.Row.DataItem, "combineid").ToString(), DataBinder.Eval(e.Row.DataItem, "AutoId").ToString());
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
    }
}