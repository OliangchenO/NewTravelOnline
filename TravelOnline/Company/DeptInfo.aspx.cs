using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.Company
{
    public partial class DeptInfo : BasePage
    {
        public string Cid, deptid, companyid, deptname, companyname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            if (!IsPostBack)
            {
                LoadCompanyInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadCompanyInfo()
        {
            string SqlQueryText = string.Format("select * from Company where id='{0}'", Request.QueryString["companyid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                companyid = DS.Tables[0].Rows[0]["uid"].ToString();
                companyname = DS.Tables[0].Rows[0]["companyname"].ToString();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM DeptInfo where companyid =(select uid from Company where id='" + Request.QueryString["companyid"] + "')";
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
                e.Row.Cells[4].Text = string.Format("<A href=\"javascript:void(0)\" onclick=\"Edit('{0}','{1}','{2}')\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString(), DataBinder.Eval(e.Row.DataItem, "deptname").ToString(), DataBinder.Eval(e.Row.DataItem, "misid").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid ucode = CombineKeys.NewComb();
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into DeptInfo (uid,companyid,deptname) values ('{0}','{1}','{2}')",
                    ucode,
                    Request.Form["companyid"].Trim(),
                    Request.Form["deptname"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update DeptInfo set deptname='{1}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["deptname"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"公司信息保存失败\"})");
            }
        }


    }
}