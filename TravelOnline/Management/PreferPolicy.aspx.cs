using System;
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
    public partial class PreferPolicy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@5") == -1)
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
            string sqlstr = "SELECT *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff,(select count(id) from Pre_Ticket where pid=Pre_Policy.id and flag='1') as sy FROM Pre_Policy where 1=1 ";
            //if (TB_LoginName.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LoginName like '%{1}%' ", sqlstr, TB_LoginName.Text.Trim());
            //if (TB_UserName.Text.Trim().Length > 0) sqlstr = string.Format("{0} and UserName like '%{1}%' ", sqlstr, TB_UserName.Text.Trim());
            if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and sellflag = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            if (DropDownList2.SelectedValue != "全部") sqlstr = string.Format("{0} and deduction = '{1}' ", sqlstr, DropDownList2.SelectedValue);
            if (DropDownList3.SelectedValue != "1") sqlstr = string.Format("{0} and range = '{1}' ", sqlstr, DropDownList3.SelectedValue);

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
                e.Row.Cells[12].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                switch (DataBinder.Eval(e.Row.DataItem, "sellflag").ToString())
                {
                    case "1":
                        e.Row.Cells[2].Text = "赠送";
                        e.Row.Cells[12].Text += string.Format(" <a href=\"javascript:void(0);\" onclick=\"ExtendTicket('{0}')\">发放</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                        break;
                    case "2":
                        e.Row.Cells[2].Text = "销售";
                        e.Row.Cells[12].Text += string.Format(" <a class=order href=\"/Order/Coupon/{0}.html\" target=_blank>购买</a>", DataBinder.Eval(e.Row.DataItem, "uid")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                        //e.Row.Cells[12].Text += string.Format(" <a href=\"javascript:void(0);\" onclick=\"SellTicket('{0}')\">购买</a>", DataBinder.Eval(e.Row.DataItem, "uid"));
                        break;
                    case "3":
                        e.Row.Cells[2].Text = "公共券号";
                        break;
                    case "4":
                        e.Row.Cells[2].Text = "邮轮领券";
                        e.Row.Cells[12].Text += string.Format(" <a class=order href=\"/Order/CouponReceive/{0}.html\" target=_blank>领取</a>", DataBinder.Eval(e.Row.DataItem, "uid")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                        
                        break;
                    default:
                        break;
                }
                e.Row.Cells[12].Text += string.Format(" <a class=order href=\"PreferList.aspx?id={0}\" target=_blank>查看</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                        
                switch (DataBinder.Eval(e.Row.DataItem, "deduction").ToString())
                {
                    case "1":
                        e.Row.Cells[3].Text = "整单";
                        break;
                    case "2":
                        e.Row.Cells[3].Text = "每人";
                        break;
                    default:
                        break;
                }
                switch (DataBinder.Eval(e.Row.DataItem, "range").ToString())
                {
                    case "1":
                        e.Row.Cells[4].Text = "全部";
                        break;
                    case "2":
                        e.Row.Cells[4].Text = "出境";
                        break;
                    case "3":
                        e.Row.Cells[4].Text = "国内";
                        break;
                    case "4":
                        e.Row.Cells[4].Text = "单项";
                        break;
                    case "5":
                        e.Row.Cells[4].Text = "邮轮";
                        break;
                    case "8":
                        e.Row.Cells[4].Text = "线路";
                        break;
                    case "9":
                        e.Row.Cells[4].Text = "产品";
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