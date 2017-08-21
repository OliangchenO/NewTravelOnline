using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Test
{
    public partial class manageuser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GridView1.Attributes.Add("SortExpression", "id");
                this.GridView1.Attributes.Add("SortDirection", "ASC");
                this.GridView_DataBind();
            }
            ClientScript.RegisterStartupScript(e.GetType(), "key", " <script>$('#GridView1_lbnFirst').linkbutton({disabled:true})</script>");
        }

        /// <summary>
        /// 绑定到GridView
        /// </summary>
        protected override void GridView_DataBind()
        {

            DataTable dtBind = this.getDB();
            //DataSet DS = new DataSet();
            //DS.Clear();
            //string sqlstr = "SELECT * FROM OL_ManageUser";
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(sqlstr);

            string sortExpression = this.GridView1.Attributes["SortExpression"];
            string sortDirection = this.GridView1.Attributes["SortDirection"];

            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                //DS.Tables[0].DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
                dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }

            this.GridView1.DataSource = dtBind;// DS.Tables[0].DefaultView;
            this.GridView1.DataBind();
            //this.GridView1.EnableViewState = false;
        }

        private DataTable getDB()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id");
            dt.Columns.Add("UserName");
            dt.Columns.Add("age");

            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            return dt;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.GridView_DataBind();
        }
    }
}