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

namespace TravelOnline.Management
{
    public partial class SpecialLine : BasePage
    {
        public string Cid, AutoId, CruisesShip, CombineId,flag="0";
        public DataSet DS1;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@7") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Cid = Request.QueryString["id"];
            if (!IsPostBack)
            {
                ViewState["CruisesShip"] = MyDataBaseComm.getScalar("select Cname from SpecialTopic where Id='" + Cid + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            CruisesShip = ViewState["CruisesShip"].ToString();
            string sqlstr = "select * from View_SpecialLine where Stid='" + Cid + "' order by SortNum,EditTime desc";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count == 0)
            {
                string typeid, destid;
                sqlstr = "SELECT * FROM SpecialTopic where id='" + Cid + "'";
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(sqlstr);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    typeid = DS1.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS1.Tables[0].Rows[0]["Destinationid"].ToString();
                    if (typeid.Length > 2 || destid.Length > 3)
                    {
                        sqlstr = "SELECT * FROM View_SpecialLineTemp where 1=1";
                        if (typeid.Length > 2)
                        {

                            sqlstr += " and lineclass in (" + typeid + ")";
                        }
                        if (destid.Length > 3)
                        {
                            sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                        }
                        sqlstr += " order by TopEnd desc,EditTime desc";
                        DS.Clear();
                        DS = MyDataBaseComm.getDataSet(sqlstr);
                        flag = "1";
                    }
                    

                }
            }


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
                if (flag == "1") e.Row.Cells[0].Text = "";

            }
            
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }

    }
}