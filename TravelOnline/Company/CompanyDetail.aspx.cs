using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace TravelOnline.Company
{
    public partial class CompanyDetail : System.Web.UI.Page
    {
        public string Cid, companyname, address, zipcode, area, city, tel, fax, misid, RebateFlag;
        public string hide;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Request.QueryString["Id"] != null)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from Company where id='{0}'", Request.QueryString["Id"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                companyname = DS.Tables[0].Rows[0]["companyname"].ToString();
                address = DS.Tables[0].Rows[0]["address"].ToString();
                zipcode = DS.Tables[0].Rows[0]["zipcode"].ToString();
                area = DS.Tables[0].Rows[0]["area"].ToString();
                city = DS.Tables[0].Rows[0]["city"].ToString();
                tel = DS.Tables[0].Rows[0]["tel"].ToString();
                fax = DS.Tables[0].Rows[0]["fax"].ToString();
                misid = DS.Tables[0].Rows[0]["misid"].ToString();
                RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
            }

            hide = "hide";
        }
    }
}