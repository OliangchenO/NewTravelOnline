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
    public partial class ManageCruisesOrder : BasePage
    {
        public string rebate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@3") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT *,(select companyname from company where id=OL_Order.ordercompany) as company from OL_Order where shipid>0 ";
            //if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            DateTime date = DateTime.Today;
            date = date.AddMonths(-6);
            //if (DropDownList1.Text == "1")
            //{
            //    sqlstr = string.Format("{0} and OrderTime >= '{1}' ", sqlstr, date.ToString());
            //}
            //else
            //{
            //    sqlstr = string.Format("{0} and OrderTime < '{1}' ", sqlstr, date.ToString());
            //}
            if (MyConvert.ConToInt(AutoId.Text.Trim()) > 0)
            {
                sqlstr = string.Format("{0} and AutoId ={1} ", sqlstr, AutoId.Text.Trim());
            }
            switch (DropDownList1.SelectedValue)
            {
                case "1":
                    sqlstr = string.Format("{0} and OrderFlag<>'9' and OrderTime >= '{1}' ", sqlstr, date.ToString());
                    break;
                case "2":
                    sqlstr = string.Format("{0} and OrderFlag<>'9' and OrderTime < '{1}' ", sqlstr, date.ToString());
                    break;
                case "3":
                    sqlstr += " and OrderFlag='9' ";
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
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[2].Text = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["ProductType"], DS.Tables[0].Rows[0]["ProductClass"], DS.Tables[0].Rows[0]["LineID"]);
                e.Row.Cells[5].Text = string.Format("<a href=\"/line/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));
                rebate = "0";
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "rebate").ToString()) > 0) rebate = "1";
                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[10].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} dept={1} reb={2}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate);
                        break;
                    case "10":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[10].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} dept={1} reb={2}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate);
                        break;
                    case "30":
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[10].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} dept={1} reb={2}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "orderdept").ToString(), rebate);
                        break;
                    case "3":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">完成</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "8":
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        e.Row.Cells[10].Text = string.Format("<A class=\"order\" href=\"javascript:DeleteOrder('{0}');void(0)\">删除</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "9":
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">已删除</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    default:
                        break;
                }

                //e.Row.Cells[8].Text = string.Format("<a href=\"VisitEdit.aspx?OrderId={0}\" target=_blank>修改观光</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
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