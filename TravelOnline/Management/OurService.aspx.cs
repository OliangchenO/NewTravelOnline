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
    public partial class OurService : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@2") == -1)
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
            string sqlstr = "SELECT id,AfficheName,AfficheType,EditTime from OL_Affiche where AfficheFlag='2' ";
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            if (DropDownList1.Text != "全部板块") sqlstr = string.Format("{0} and AfficheType = '{1}' ", sqlstr, DropDownList1.Text);
            sqlstr = string.Format("{0} order by AfficheType", sqlstr);

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
                e.Row.Cells[4].Text = string.Format("<a href=\"/Common/ShowNews.aspx?id={0}\" target=_blank>查看</a> &nbsp;<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";

                switch (DataBinder.Eval(e.Row.DataItem, "AfficheType").ToString())
                {
                    case "Service1":
                        e.Row.Cells[2].Text = "联系我们";
                        break;
                    case "Service2":
                        e.Row.Cells[2].Text = "人才招聘";
                        break;
                    case "Service3":
                        e.Row.Cells[2].Text = "同行分销";
                        break;
                    case "Service4":
                        e.Row.Cells[2].Text = "广告服务";
                        break;
                    case "Service5":
                        e.Row.Cells[2].Text = "服务终端";
                        break;
                    case "Service6":
                        e.Row.Cells[2].Text = "销售联盟";
                        break;
                    case "Service7":
                        e.Row.Cells[2].Text = "关于我们";
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