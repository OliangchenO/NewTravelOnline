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
    public partial class BannerAd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@3@2") == -1)
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
            string sqlstr = string.Format("SELECT * FROM OL_FlashAD where AdFlag = '{0}' order by AdSort", DropDownList1.Text);

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
                e.Row.Cells[6].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                string[] sArray = DataBinder.Eval(e.Row.DataItem, "AdPicUrl").ToString().Split('/');
                string pics;
                switch (DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().Substring(0,2))
                {
                    case "IL":
                        e.Row.Cells[2].Text = string.Format("首页左{0}", DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().Substring(2, 1));
                        pics = "height='75px'";
                        break;
                    case "IC":
                        e.Row.Cells[2].Text = string.Format("首页中{0}", DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().Substring(2, 1));
                        pics = "height='70px' width='200px'";
                        break;
                    case "IR":
                        e.Row.Cells[2].Text = string.Format("首页右{0}", DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().Substring(2, 1));
                        pics = "height='75px'";
                        break;
                    default:
                        pics = "height='75px'";
                        break;
                }
                e.Row.Cells[4].Text = string.Format("<img {0} src=\"/Upload/AdImage/Thumb_{1}\" />", pics,sArray[3].ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}