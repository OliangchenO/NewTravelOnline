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
    public partial class SpecialTopic : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@7") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@11") != -1)
                {
                    DropDownList1.Items.Add(new ListItem("首页特价精选","Index_Sell"));
                    DropDownList1.Items.Add(new ListItem("首页当季推荐","Index_Season"));
                    DropDownList1.Items.Add(new ListItem("首页出境游","Index_Outbound"));
                    DropDownList1.Items.Add(new ListItem("首页国内游","Index_Inland"));
                    DropDownList1.Items.Add(new ListItem("首页邮轮","Index_Cruise"));
                    DropDownList1.Items.Add(new ListItem("首页自由行","Index_Freetour"));
                    DropDownList1.Items.Add(new ListItem("首页签证","Index_Visa"));
                }
                DropDownList1.Items.Add(new ListItem("出境热销排行", "Outbound_Hot"));
                DropDownList1.Items.Add(new ListItem("出境特价精选", "Outbound_Sell"));
                DropDownList1.Items.Add(new ListItem("出境短线", "Outbound_01"));
                DropDownList1.Items.Add(new ListItem("出境长线", "Outbound_02"));
                DropDownList1.Items.Add(new ListItem("出境主题旅游", "Outbound_03"));
                DropDownList1.Items.Add(new ListItem("国内热销排行", "Inland_Hot"));
                DropDownList1.Items.Add(new ListItem("国内特价精选", "Inland_Sell"));
                DropDownList1.Items.Add(new ListItem("国内推荐目的地", "Inland_01"));
                DropDownList1.Items.Add(new ListItem("国内主题旅游", "Inland_02"));
                DropDownList1.Items.Add(new ListItem("自由行热销排行", "Freetour_Hot"));
                DropDownList1.Items.Add(new ListItem("自由行特价精选", "Freetour_Sell"));
                DropDownList1.Items.Add(new ListItem("自由行出境短线", "Freetour_01"));
                DropDownList1.Items.Add(new ListItem("自由行出境长线", "Freetour_02"));
                DropDownList1.Items.Add(new ListItem("自由行国内热门", "Freetour_03"));
                DropDownList1.Items.Add(new ListItem("邮轮线路精选", "Cruise_Best"));
                DropDownList1.Items.Add(new ListItem("所有签证", "Visa_All"));
                DropDownList1.Items.Add(new ListItem("电商产品", "OTA"));
                DropDownList1.Items.Add(new ListItem("微信限时抢购", "WeChat_FlashSale"));
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("SELECT *,(select count(1) from SpecialLine where Stid=SpecialTopic.id) as nums FROM SpecialTopic where Types = '{0}' order by SortNum,EditTime desc", DropDownList1.Text);
            //if (TextBox1.Text.Trim().Length > 0) sqlstr = string.Format("{0} and RightName like '%{1}%' ", sqlstr, TextBox1.Text.Trim());

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
                e.Row.Cells[4].Text = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", DataBinder.Eval(e.Row.DataItem, "Url"));
                e.Row.Cells[7].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                e.Row.Cells[7].Text += string.Format(" <a href=\"SpecialLine.aspx?id={0}\" target=\"_blank\">线路</a>", DataBinder.Eval(e.Row.DataItem, "id"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}