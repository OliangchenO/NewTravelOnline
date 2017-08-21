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
    public partial class FlashAD : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@3@1") == -1)
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
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                if (DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().IndexOf("LeftHot_") > -1 || DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString().IndexOf("LeftArea_") > -1)
                {

                }
                else
                {
                    string[] sArray = DataBinder.Eval(e.Row.DataItem, "AdPicUrl").ToString().Split('/');
                    //<img id="Img1" alt="宽屏版 分辨率为 766*270" <%=ThumbSrc1 %>/>
                    e.Row.Cells[4].Text = string.Format("<img width='250px' height='75px' src=\"/Upload/AdImage/Thumb_{0}\" />", sArray[3].ToString());
                }
                
                switch (DataBinder.Eval(e.Row.DataItem, "AdFlag").ToString())
                {
                    case "Index":
                        e.Row.Cells[2].Text = "首页";
                        break;
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
                    case "WeChat":
                        e.Row.Cells[2].Text = "微信";
                        break;
                    default:
                        e.Row.Cells[2].Text = "新版";
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