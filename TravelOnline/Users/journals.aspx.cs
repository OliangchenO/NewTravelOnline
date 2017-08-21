using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Users
{
    public partial class journals : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string PreDate;
            PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
            string sqlstr = string.Format("SELECT id,uid,title,comment,views,inputdate,flag from OL_Journal where userid='{0}' order by inputdate desc", Convert.ToString(Session["Online_UserId"]));


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
                        break;
                    default:
                        //e.Row.Cells[0].Text = "";
                        break;
                }
                e.Row.Cells[5].Text = string.Format(" <a href=\"WriteJournal.aspx?uid={0}\" target=_blank>修改</a>", DataBinder.Eval(e.Row.DataItem, "uid"));

                //Session["Online_YJDept"]
                //if (Convert.ToString(Session["Online_YJDept"]).Length > 1)
                //{
                //    e.Row.Cells[5].Text += string.Format(" <a href=\"WriteJournal.aspx?flag=audit&uid={0}\" target=_blank>审核</a>", DataBinder.Eval(e.Row.DataItem, "uid"));
                //}
                
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}