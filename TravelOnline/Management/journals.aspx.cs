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
    public partial class journals : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@7") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                LoadDropListInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDropListInfo()
        {
            string sqlstr = "select deptname from OL_LoginUser where deptname is not null or deptname<>'' group by deptname";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string PreDate;
            PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string sqlstr = string.Format("SELECT id,uid,title,comment,views,inputdate,flag,(select UserName from OL_LoginUser where id=OL_Journal.userid) as UserName,(select deptname from OL_LoginUser where id=OL_Journal.userid) as deptname,EditDate from OL_Journal where 1=1 ", Convert.ToString(Session["Online_UserId"]));
            if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and title like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            if (DropDownList1.Text != "全部") sqlstr = string.Format("{0} and userid in (select id from OL_LoginUser where deptname like '%{1}%') ", sqlstr, DropDownList1.Text);
            if (DropDownList2.Text != "all") sqlstr = string.Format("{0} and Recom = '{1}' ", sqlstr, DropDownList2.Text);
            sqlstr = string.Format("{0} order by inputdate desc", sqlstr);

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
                switch (DataBinder.Eval(e.Row.DataItem, "flag").ToString())
                {
                    case "0":
                        e.Row.Cells[0].Text = "待审核";
                        break;
                    case "1":
                        e.Row.Cells[0].Text = "已发布";
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        break;
                    default:
                        //e.Row.Cells[0].Text = "";
                        break;
                }
                e.Row.Cells[2].Text = string.Format("<A href=\"/Common/ShowJournal.aspx?id={1}&flag=audit\" target=_blank>{0}</A>", DataBinder.Eval(e.Row.DataItem, "title"), DataBinder.Eval(e.Row.DataItem, "id"));
                e.Row.Cells[7].Text = string.Format("<a href=\"/Users/WriteJournal.aspx?flag=audit&uid={0}\" target=_blank>审核</a> <a href=\"javascript: void(0); \" class=\"JustDoIt\" plain=\"true\" name=\"{0}\" iconCls=\"icon - lamp\">推荐</a>", DataBinder.Eval(e.Row.DataItem, "uid"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}