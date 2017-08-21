using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.Class.Common;
using Aspose.Cells;


namespace TravelOnline.Management
{
    public partial class SalesReport : BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@8@2") == -1)
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
            string strSqlCmd = string.Format("select * from View_OL_OrderList where 1=1");
            if (TB_Name.Text.Trim().Length > 0) strSqlCmd = string.Format("{0} and OrderName like '%{1}%' ", strSqlCmd, TB_Name.Text.Trim());
            if (TB_Line.Text.Trim().Length > 0) strSqlCmd = string.Format("{0} and LineName like '%{1}%' ", strSqlCmd, TB_Line.Text.Trim());
            if (TB_Bdate.Text.Length > 8)
            {
                try
                {
                    strSqlCmd = string.Format("{0} and OrderTime >= '{1}' ", strSqlCmd, Convert.ToDateTime(TB_Bdate.Text));
                }
                catch
                { }
            }

            if (TB_Edate.Text.Length > 8)
            {
                try
                {
                    strSqlCmd = string.Format("{0} and OrderTime <= '{1}' ", strSqlCmd, Convert.ToDateTime(TB_Edate.Text).AddDays(1));
                }
                catch
                { }
            }

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(strSqlCmd);

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
                e.Row.Cells[4].Text = string.Format("<a href=\"/line/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));

                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
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
                        //e.Row.Cells[9].Text += " " + string.Format("<a class=pay href=\"/Order/Contract/{0}.html\" target=_blank>合同</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                    }

                }

                if (DataBinder.Eval(e.Row.DataItem, "ota").ToString() == "spdb") e.Row.Cells[9].Text += " 浦发";
                //<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>

            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        protected void btnTableToExcel_Click(object sender, EventArgs e)
        {
            string strSqlCmd = string.Format("select * from View_OL_OrderList where 1=1");
            if (TB_Name.Text.Trim().Length > 0) strSqlCmd = string.Format("{0} and OrderName like '%{1}%' ", strSqlCmd, TB_Name.Text.Trim());
            if (TB_Line.Text.Trim().Length > 0) strSqlCmd = string.Format("{0} and LineName like '%{1}%' ", strSqlCmd, TB_Line.Text.Trim());
            if (TB_Bdate.Text.Length > 8)
            {
                try
                {
                    strSqlCmd = string.Format("{0} and OrderTime >= '{1}' ", strSqlCmd, Convert.ToDateTime(TB_Bdate.Text));
                }
                catch
                { }
            }

            if (TB_Edate.Text.Length > 8)
            {
                try
                {
                    strSqlCmd = string.Format("{0} and OrderTime <= '{1}' ", strSqlCmd, Convert.ToDateTime(TB_Edate.Text).AddDays(1));
                }
                catch
                { }
            }

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(strSqlCmd);
            int num = 0;
            if (DS.Tables[0].Rows.Count > 0)
            {
                System.Data.DataTable dataTable = DS.Tables[0];
                Workbook workbook = new Workbook(string.Format("{0}OfficeFiles\\CruisesOut\\SalesReport.xls", System.AppDomain.CurrentDomain.BaseDirectory));
                Worksheet sheet = workbook.Worksheets[0];
                Cells cells = sheet.Cells;
                //cells["A1"].PutValue("报表");
                //var colIndex = "A";
                for (int i = 0; i < DS.Tables[0].Rows.Count;i++)
                {
                    for (int j = 0; j < DS.Tables[0].Columns.Count; j++)
                    {
                        sheet.Cells[i+2, j].PutValue(DS.Tables[0].Rows[i][j].ToString());
                    }
                }

                workbook.Save(System.Web.HttpContext.Current.Response, "SalesReport.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                System.Web.HttpContext.Current.Response.End();
                return;
            }
            Response.Write("没有任何数据可导出");
            Response.End();
        }

    }
}