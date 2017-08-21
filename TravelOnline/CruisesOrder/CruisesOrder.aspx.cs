using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.CruisesOrder
{
    public partial class CruisesOrder : BasePage
    {
        public string rebate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserDept"]).Length == 0)
            {
                Response.Redirect("/login/login.aspx", true);
            }
                
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * from OL_Order where OrderFlag<>'9' and shipid>0 ";
            //if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            DateTime date = DateTime.Today;
            date = date.AddMonths(-6);
            if (DropDownList1.Text == "1")
            {
                sqlstr = string.Format("{0} and OrderTime >= '{1}' ", sqlstr, date.ToString());
            }
            else
            {
                sqlstr = string.Format("{0} and OrderTime < '{1}' ", sqlstr, date.ToString());
            }

            if (DropDownList2.Text == "1")
            {
                sqlstr = string.Format("{0} and OrderUser = '{1}'", sqlstr, Convert.ToString(Session["Online_UserId"]));
            }
            else
            {
                sqlstr = string.Format("{0} and orderdept = '{1}' ", sqlstr, Convert.ToString(Session["Online_UserDept"]));
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
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[2].Text = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["ProductType"], DS.Tables[0].Rows[0]["ProductClass"], DS.Tables[0].Rows[0]["LineID"]);
                e.Row.Cells[3].Text = string.Format("<a href=\"/line/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));

                rebate = "0";
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "rebate").ToString()) > 0) rebate = "1";
                
                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[8].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} reb={1}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), rebate);
                        break;
                    case "1":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[8].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} reb={1}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), rebate);
                        break;
                    case "2":
                        e.Row.Cells[8].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} reb={1}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), rebate);
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "3":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">完成</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "8":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    default:
                        break;
                }

                //e.Row.Cells[8].Text = string.Format("<a class=order href=\"VisitEdit.aspx?OrderId={0}\" target=_blank>修改观光</a> ", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //e.Row.Cells[8].Text += string.Format("<a href=\"VisitEdit.aspx?OrderId={0}\" target=_blank>调整舱房</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //e.Row.Cells[8].Text += string.Format("<A class=order href=\"javascript:void(0)\" onclick=\"AdjustRoom('{0}')\">调整舱房</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                //e.Row.Cells[8].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"));
            

                if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "1" || DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "2")
                {
                    //decimal Fee = MyConvert.ConToDec(DataBinder.Eval(e.Row.DataItem, "Price").ToString());
                    //decimal Pay = MyConvert.ConToDec(DataBinder.Eval(e.Row.DataItem, "Pay").ToString());
                    //decimal Yue = Fee - Pay;
                    //if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "1")
                    //{
                    //    e.Row.Cells[8].Text += " " + string.Format("<a class=pay href=\"javascript:CancelOrder('{0}');void(0);\" >取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                    //}
                    //if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() == "Cruises" && DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() != "2")
                    //{
                    //    e.Row.Cells[8].Text = "";
                    //}

                }
                else
                {

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