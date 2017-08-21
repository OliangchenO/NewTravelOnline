using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.LoginUsers;
using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.Management
{
    public partial class SummaryInfo : System.Web.UI.Page
    {
        public string desname, desid, id, uid, typeid, typename, BuyButton, contents, TitleInfo;
        public int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@2@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            id = Request.QueryString["id"];
            desid = Request.QueryString["desid"];
            flag = MyConvert.ConToInt(Request.QueryString["flag"]);
            TitleInfo = "目的地概述";
            if (flag == 1) TitleInfo = "交通资源信息";
            BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save()\">保 存</A>";

            if (!IsPostBack)
            {
                if (id != null)
                {
                    LoadInfo();

                }
                else
                {
                    desname = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + desid + "'");
                
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_Summary where id='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                uid = DS.Tables[0].Rows[0]["uid"].ToString();
                id = DS.Tables[0].Rows[0]["id"].ToString();
                desid = DS.Tables[0].Rows[0]["desid"].ToString();
                typeid = DS.Tables[0].Rows[0]["typeid"].ToString();
                typename = DS.Tables[0].Rows[0]["typename"].ToString();
                contents = DS.Tables[0].Rows[0]["contents"].ToString();
                desname = MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + desid + "'");
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }

}