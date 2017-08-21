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
    public partial class FxOrderList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@10@1") == -1)
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
            try
            {
                string sqlstr = string.Format("SELECT *,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_FxOrder.OrderId) as pay,(select count(1) from OL_OrderPrice where OrderId=OL_FxOrder.OrderId and PriceType='Coupon') as Coupon from OL_FxOrder where  OrderFlag<>'9' ", Convert.ToString(Session["Online_UserId"]));
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

                DataTable dt = MyDataBaseComm.getDataSet(sqlstr).Tables[0];

                string sortExpression = this.GridView1.Attributes["SortExpression"];
                string sortDirection = this.GridView1.Attributes["SortDirection"];
                if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
                {
                    dt.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
                }

                this.GridView1.DataSource = dt.DefaultView;
                this.GridView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); 
                e.Row.Cells[4].Text = string.Format("<a href=\"/line/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "1":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "2":
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
                if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() != "Coupon")
                {
                    if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "0" || DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "1")
                    {
                        if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "shipid").ToString()) == 0)
                        {
                            //e.Row.Cells[9].Text += string.Format(" <a class=order href=\"javascript:void(0);\" onclick=\"EditPrice('{0}')\">调费</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        }
                    }
                    decimal Pay = MyConvert.ConToDec(DataBinder.Eval(e.Row.DataItem, "Pay").ToString());
                    if (Pay > 0)
                    {
                        e.Row.Cells[9].Text += " " + string.Format("<a class=pay href=\"/Order/Contract/{0}.html\" target=_blank>合同</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                    }
                }
                if (DataBinder.Eval(e.Row.DataItem, "ota").ToString() == "spdb") e.Row.Cells[9].Text += " 浦发";
            }
        }
        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}