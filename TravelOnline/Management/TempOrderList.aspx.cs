using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class TempOrderList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@6@5") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                TB_Bdate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(-1));
                TB_Edate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("SELECT *,(select companyname from company where id=OL_TempOrder.ordercompany) as company from OL_TempOrder where 1=1 ", Convert.ToString(Session["Online_UserId"]));
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and OrderName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            if (TB_Line.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LineName like '%{1}%' ", sqlstr, TB_Line.Text.Trim());
            if (TB_Bdate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and OrderTime >= '{1}' ", sqlstr, Convert.ToDateTime(TB_Bdate.Text));
                }
                catch
                { }
            }

            if (TB_Edate.Text.Length > 8)
            {
                try
                {
                    sqlstr = string.Format("{0} and OrderTime <= '{1}' ", sqlstr, Convert.ToDateTime(TB_Edate.Text).AddDays(1));
                }
                catch
                { }
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
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/Purchase/OrderView.aspx?Flag=Temp&OrderId={0}\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //company UserName
                e.Row.Cells[1].Text = string.Format("{0}<br/>{1}", DataBinder.Eval(e.Row.DataItem, "UserName").ToString(), DataBinder.Eval(e.Row.DataItem, "company").ToString());
                e.Row.Cells[4].Text = string.Format("<a href=\"/line/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));

                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "9":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">暂存</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    default:
                        break;
                }

                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "shipid").ToString()) > 0)
                {
                    e.Row.Cells[8].Text = string.Format("<A class=\"order\" href=\"javascript:DeleteOrder('{0}');void(0)\">删除</A> ", DataBinder.Eval(e.Row.DataItem, "OrderId"));
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