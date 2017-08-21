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
    public partial class Summary : BasePage
    {
        public string Cid, CruisesCompany,TitleInfo,hide,url;
        public int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@2@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];
            flag = MyConvert.ConToInt(Request.QueryString["flag"]);
            TitleInfo = "目的地概述";
            if (flag == 1) TitleInfo = "交通资源信息";
            url = "href=\"SummaryInfo.aspx?desid=" + Cid + "&flag=" + flag + "\" target=\"_blank\"";
            if (flag == 9)
            {
                TitleInfo = "景点资源信息";
                hide = "hide";
                GridView1.Columns[3].HeaderText = "景点名称";
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[4].Visible = false;
                url = "href=\"/Destination/ViewInfo.aspx?desid=" + Cid + "\" target=\"_blank\"";
            }
            if (flag == 10)
            {
                TitleInfo = "景点图片";
                hide = "hide";
                GridView1.Columns[2].HeaderText = "景点图片";
                GridView1.Columns[3].HeaderText = "图片描述";
                GridView1.Columns[4].Visible = false;
                url = "href=\"javascript:\" onclick=\"AddView(" + Cid + "," + Request.QueryString["desid"] + ")\"";
            }
                
            if (!IsPostBack)
            {
                switch (flag)
                {
                    case 9:
                        CruisesCompany = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + Cid + "'");
                
                        break;
                    case 10:
                        CruisesCompany = MyDataBaseComm.getScalar("select viewname from OL_View where id='" + Cid + "'");

                        break;
                    default:
                        CruisesCompany = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + Cid + "'");
                        break;
                }
                if (flag != 9) LoadDeptInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDeptInfo()
        {
            string sqlstr = "select id,dataname from InitData where ftype='Summary' order by sort,id";
            if (flag == 1) sqlstr = "select id,dataname from InitData where ftype='Traffic' order by sort,id";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "";
            //if (flag == 9)
            //{
            //    sqlstr = "SELECT id,viewname as title,'景点' as typename,(select count(id) from OL_ViewPic where viewid=OL_View.id) as scount,desid FROM OL_View where desid='" + Request.QueryString["Id"] + "'";
            //}
            //else
            //{
            //    sqlstr = "SELECT * FROM OL_Summary where desid='" + Request.QueryString["Id"] + "' ";
            //    sqlstr = string.Format("{0} and flag = '{1}' ", sqlstr, flag);
            //    if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and typeid = '{1}' ", sqlstr, DropDownList1.SelectedValue);
            //}
            switch (flag)
            {
                case 9:
                    sqlstr = "SELECT id,viewname as title,'景点' as typename,(select count(id) from OL_ViewPic where viewid=OL_View.id) as scount,desid FROM OL_View where desid='" + Request.QueryString["id"] + "'";
                    break;
                case 10:
                    sqlstr = "SELECT id,picname as title,'' as typename,picurl FROM OL_ViewPic where viewid='" + Request.QueryString["id"] + "'";
                    break;
                default:
                    sqlstr = "SELECT * FROM OL_Summary where desid='" + Request.QueryString["Id"] + "' ";
                    sqlstr = string.Format("{0} and flag = '{1}' ", sqlstr, flag);
                    if (DropDownList1.SelectedValue != "全部") sqlstr = string.Format("{0} and typeid = '{1}' ", sqlstr, DropDownList1.SelectedValue);
                    break;
            }
            //if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and cname like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
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
                //if (flag == 9)
                //{
                //    e.Row.Cells[5].Text = string.Format("<A class=order href=\"/Destination/ViewInfo.aspx?id={0}\" target=\"_blank\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                //    e.Row.Cells[5].Text += string.Format("<A class=order href=\"Summary.aspx?flag=10&id={0}&desid={2}\" target=\"_blank\">图片({1})</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "scount").ToString(), DataBinder.Eval(e.Row.DataItem, "desid").ToString());
                
                //}
                //else
                //{
                //    e.Row.Cells[2].Text = "";
                //    e.Row.Cells[5].Text = string.Format("<A class=order href=\"SummaryInfo.aspx?id={0}\" target=\"_blank\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                //}

                switch (flag)
                {
                    case 9:
                        e.Row.Cells[5].Text = string.Format("<A class=order href=\"/Destination/ViewInfo.aspx?id={0}\" target=\"_blank\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                        e.Row.Cells[5].Text += string.Format("<A class=order href=\"Summary.aspx?flag=10&id={0}&desid={2}\" target=\"_blank\">图片({1})</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "scount").ToString(), DataBinder.Eval(e.Row.DataItem, "desid").ToString());
                        break;
                    case 10:
                        string[] sArray = DataBinder.Eval(e.Row.DataItem, "picurl").ToString().Split('/');
                        string ThumbSrc1 = string.Format("src=\"/Upload/View/{0}/{1}/S_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                        e.Row.Cells[2].Text = string.Format("<img height='50px' width='80px' {0} />", ThumbSrc1);
                        e.Row.Cells[5].Text = string.Format("<A class=order href=\"javascript:\" onclick=\"EditView({0})\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                        break;
                    default:
                        e.Row.Cells[5].Text = string.Format("<A class=order href=\"SummaryInfo.aspx?id={0}\" target=\"_blank\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
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