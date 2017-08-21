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
    public partial class LineCarNo : BasePage
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
            string sqlstr = "select days as id,visit as dataname from CR_Route where visit <> '' and lineid='" + lineid + "'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT *,(select count(1) from CR_VisitList where flag='0' and busid=CR_BusNo.id) as nums FROM CR_BusNo where Lineid='" + lineid + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and BusNo ='{1}' ", sqlstr, MyConvert.ConToInt(tb_cname.Text.Trim()));
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and Days = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            //if (DropDownList2.SelectedValue != "all") sqlstr = string.Format("{0} and berth={1}", sqlstr, DropDownList2.SelectedValue);
            //switch (DropDownList3.SelectedValue)
            //{
            //    case "all":
            //        break;
            //    case "0":
            //        sqlstr += " and Nums=0";
            //        break;
            //    case "1":
            //        sqlstr += " and Nums>0";
            //        break;
            //    case "2":
            //        sqlstr += " and Mergeid>0";
            //        break;
            //    case "3":
            //        sqlstr += " and Nums>0 and Nums<>berth";
            //        break;
            //    case "4":
            //        sqlstr += " and Nums>0 and Nums=berth";
            //        break;
            //    default:
            //        break;
            //}

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
                e.Row.Cells[7].Text = string.Format("<A class=order href=\"javascript:edit('{0}');void(0)\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                e.Row.Cells[7].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/CruisesGuest.aspx?carid={0}&lineid={1}\" target=_blank>名单</a>", DataBinder.Eval(e.Row.DataItem, "id"), DataBinder.Eval(e.Row.DataItem, "Lineid"));
                e.Row.Cells[7].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/ExcelOutPut.aspx?cid={0}&action=CruisesCarGuest\" target=_blank>名单导出</a>", DataBinder.Eval(e.Row.DataItem, "id"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}