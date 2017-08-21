using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.Management
{
    public partial class SpecialTopicAdd : System.Web.UI.Page
    {
        public string id, Cname, Types, SortNum, setddl, Url, hide, Line_Desid, DestinationInfos,LineType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }
            id = Request.QueryString["Id"];
            hide = "class=hide";
            if (!IsPostBack)
            {
                if (id != null)
                {
                    LoadUserInfo();
                }
            }
        }

        protected void LoadUserInfo()
        {
            string SqlQueryText = string.Format("select * from SpecialTopic where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //id, AdName, AdPageUrl, AdPicUrl, AdSecPicUrl, AdFlag, ThumbSrc1, ThumbSrc2;
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                Cname = DS.Tables[0].Rows[0]["Cname"].ToString();
                Types = DS.Tables[0].Rows[0]["Types"].ToString();
                SortNum = DS.Tables[0].Rows[0]["SortNum"].ToString();
                Url = DS.Tables[0].Rows[0]["Url"].ToString();
                setddl = "disabled=\"disabled\"";
                DestinationInfos = DS.Tables[0].Rows[0]["DestinationList"].ToString();
                Line_Desid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                LineType= DS.Tables[0].Rows[0]["LineType"].ToString();
            }
        }
    }
}