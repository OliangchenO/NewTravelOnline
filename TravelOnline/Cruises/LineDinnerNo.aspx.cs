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
    public partial class LineDinnerNo : BasePage
    {
        public string Cid, lineid, CruisesShip;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            lineid = Request.QueryString["lineid"];
            if (!IsPostBack)
            {
                LoadInfo();
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + lineid + "'");
                this.GridView_DataBind();
            }
        }

        protected void LoadInfo()
        {
            string sqlstr = "select DinnerTime from CR_DinnerNo where lineid='" + lineid + "' group by DinnerTime";

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM View_CR_DinnerNo where Lineid='" + lineid + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and TabelNo like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and DinnerTime = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            switch (DropDownList2.SelectedValue)
            {
                case "all":
                    break;
                case "0":
                    sqlstr += " and Nums=0";
                    break;
                case "1":
                    sqlstr += " and Nums>0";
                    break;
                case "2":
                    sqlstr += " and Nums>0 and Nums<>berth";
                    break;
                case "3":
                    sqlstr += " and Nums>0 and Nums=berth";
                    break;
                default:
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //switch (DataBinder.Eval(e.Row.DataItem, "Flag").ToString())
                //{
                //    case "0":
                //        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">未入住</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                //        break;
                //    case "1":
                //        e.Row.ForeColor = System.Drawing.Color.Blue;
                //        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">已入住</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                //        break;
                //    case "2":
                //        e.Row.ForeColor = System.Drawing.Color.DarkGreen;
                //        e.Row.Cells[2].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">有拼房</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                //        break;
                //    default:
                //        break;
                //}
                e.Row.Cells[6].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/CruisesGuest.aspx?dinnerid={0}&lineid={1}\" target=_blank>名单</a>", DataBinder.Eval(e.Row.DataItem, "id"), DataBinder.Eval(e.Row.DataItem, "Lineid"));
                
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}