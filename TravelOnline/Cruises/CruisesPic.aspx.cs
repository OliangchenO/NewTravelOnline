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
    public partial class CruisesPic : BasePage
    {
        public string Cid, uid, mname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM CR_Pic where shipid='" + Request.QueryString["Id"] + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and cname like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and pictype = '{1}' ", sqlstr, DropDownList1.SelectedValue);
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
                e.Row.Cells[5].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Edit({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                string[] sArray = DataBinder.Eval(e.Row.DataItem, "picurl").ToString().Split('/');
                e.Row.Cells[4].Text = string.Format("<img height='100px' src=\"/Upload/Cruises/{1}/Thumb_{0}\" />", sArray[4].ToString(), DataBinder.Eval(e.Row.DataItem, "shipid").ToString());
                switch (DataBinder.Eval(e.Row.DataItem, "pictype").ToString())
                {
                    case "ship":
                        e.Row.Cells[2].Text = "邮轮外观";
                        break;
                    case "others":
                        e.Row.Cells[2].Text = "各项设施";
                        break;
                    case "deck":
                        e.Row.Cells[2].Text = "甲板";
                        break;
                    case "room":
                        e.Row.Cells[2].Text = "舱房";
                        break;
                    default:
                        break;
                }

                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "cname").ToString() + " " + DataBinder.Eval(e.Row.DataItem, "roomtype").ToString();
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}