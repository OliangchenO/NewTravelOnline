using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class ProductClass : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@2@1") == -1)
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
            //left outer join, inner join
            //string sqlstr = "SELECT a.Id,a.ProductName,a.ProductUrl,a.MisClassId,b.ProductName as ParentName FROM OL_ProductClass a left outer join OL_ProductClass b on a.ParentId=b.id where 1=1 ";
            string Parentid = parent2.Text;
            if (Parentid == "") Parentid = parent1.Text;

            string serchid = "";
            if (Parentid.Trim().Length > 0)
            {
                serchid = GetProductClass.getParentClassListById(Parentid);
                if (serchid == null) serchid = "";
            }

            string sqlstr = "SELECT Id,ProductName,ProductUrl,MisClassId,ClassList,ClassLevel,ProductSort,ProductType FROM OL_ProductClass where 1=1 ";
            if (this.CheckBox1.Checked == true)
            {
                if (Parentid == "") Parentid = "0";
                if (Parentid.Length > 0) sqlstr = string.Format("{0} and ParentId ='{1}' ", sqlstr, Parentid);
            }
            else
            { 
                if (serchid.Length > 0) sqlstr = string.Format("{0} and ClassList like '{1}%' ", sqlstr, serchid);
            }
            sqlstr = string.Format("{0} order by ProductSort", sqlstr);
            
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
                string LitStyle = "<span style=\"PADDING-LEFT:{0}px\";>{1}{2}{3}</span>";
                string LitImg1 = "<img src=/images/openfolder.gif align=absmiddle />";
                string LitImg2 = "<img src=/images/file.gif align=absmiddle />";
                string LitImg3 = "<img src=/images/t.gif align=absmiddle />";
                int ClassTj = MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "ClassLevel").ToString());
                if (ClassTj == 1)
                {
                    //e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "ProductName").ToString();
                    e.Row.Cells[3].Text = string.Format(LitStyle, 5, LitImg1, DataBinder.Eval(e.Row.DataItem, "ProductName").ToString(), "", "");
                }
                else
                {
                    e.Row.Cells[3].Text = string.Format(LitStyle, (ClassTj-1) * 20, LitImg3, LitImg2, DataBinder.Eval(e.Row.DataItem, "ProductName").ToString());
                }

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

                e.Row.Cells[7].Text = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditInfo('{0}')\">修改</a>", DataBinder.Eval(e.Row.DataItem, "id"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}