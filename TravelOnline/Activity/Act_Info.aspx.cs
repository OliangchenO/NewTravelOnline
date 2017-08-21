using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.Class.Common;
using System.Data;
using System.Data.SqlClient;

namespace TravelOnline.Activity
{
    public partial class Act_Info : BasePage
    {
        public string ActInfoMain_ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ActInfoMain_ID"] != null)
                {
                    ActInfoMain_ID = Request.QueryString["ActInfoMain_ID"].ToString();
                    LoadActInfo();
                }
            }
        }

        protected void LoadActInfo()
        {
            string SqlQueryText = string.Format("select * from dbo.Act_ActInfoMain where ActInfoMain_ID='{0}'", Request.QueryString["ActInfoMain_ID"].ToString());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                this.txbAct_Name.Text = DS.Tables[0].Rows[0]["ActivityName"].ToString();
                this.more_search_date.Value = DS.Tables[0].Rows[0]["ActivityStartTime"].ToString();
                this.more_search_dateend.Value = DS.Tables[0].Rows[0]["ActivityEndTime"].ToString();
                this.ddlStart.SelectedValue = DS.Tables[0].Rows[0]["Start"].ToString();
                this.txbMinNum.Text = DS.Tables[0].Rows[0]["MinNum"].ToString();
                this.txbMaxNum.Text = DS.Tables[0].Rows[0]["MaxNum"].ToString();
                this.txbMinAge.Text = DS.Tables[0].Rows[0]["MinAge"].ToString();
                this.txbMaxAge.Text = DS.Tables[0].Rows[0]["MaxAge"].ToString();
                this.txbPlace.Text = DS.Tables[0].Rows[0]["Place"].ToString();
                this.txbActSTime.Value = DS.Tables[0].Rows[0]["ActivityRunSTime"].ToString();
                this.txbActETime.Value = DS.Tables[0].Rows[0]["ActivityRunETime"].ToString();
            }
        }
    }
}