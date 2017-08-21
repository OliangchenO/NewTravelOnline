﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class ProductType : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@2@2") == -1)
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
            string sqlstr = "SELECT * FROM OL_ProductType where 1=1 ";
            if (DropDownList1.Text != "All") sqlstr = string.Format("{0} and ProductType = '{1}' ", sqlstr, DropDownList1.Text);

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
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "FirstDestination").ToString()) > 0)
                {
                    e.Row.Cells[6].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">{1}</A> ", DataBinder.Eval(e.Row.DataItem, "Id").ToString(), DataBinder.Eval(e.Row.DataItem, "Destination").ToString());
                }
                else
                {
                    e.Row.Cells[6].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">增加</A> ", DataBinder.Eval(e.Row.DataItem, "Id").ToString());
                }
                e.Row.Cells[7].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));

                switch (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString())
                {
                    case "OutBound":
                        e.Row.Cells[2].Text = "出境旅游";
                        break;
                    case "InLand":
                        e.Row.Cells[2].Text = "国内旅游";
                        break;
                    case "FreeTour":
                        e.Row.Cells[2].Text = "自由行";
                        break;
                    case "Cruises":
                        e.Row.Cells[2].Text = "邮轮";
                        break;
                    case "Visa":
                        e.Row.Cells[2].Text = "签证";
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