using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.Management
{
    public partial class TopicTravelInfo : System.Web.UI.Page
    {
        public string id, AdName, AdPageUrl, AdPicUrl, AdSecPicUrl, AdFlag, ThumbSrc1, ThumbSrc2, IsShow, AdSort, MisClassId, setddl,Checks;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            id = Request.QueryString["Id"];
            IsShow = "style=\"DISPLAY:none\"";
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
            string SqlQueryText = string.Format("select * from OL_FlashAD where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                AdName = DS.Tables[0].Rows[0]["AdName"].ToString();
                AdSort = DS.Tables[0].Rows[0]["AdSort"].ToString();
                AdPageUrl = DS.Tables[0].Rows[0]["AdPageUrl"].ToString();
                AdPicUrl = DS.Tables[0].Rows[0]["AdPicUrl"].ToString();
                AdSecPicUrl = DS.Tables[0].Rows[0]["AdSecPicUrl"].ToString();
                AdFlag = DS.Tables[0].Rows[0]["AdFlag"].ToString();
                MisClassId = DS.Tables[0].Rows[0]["MisClassId"].ToString();
                string[] sArray = DS.Tables[0].Rows[0]["AdPicUrl"].ToString().Split('/');
                ThumbSrc1 = string.Format("src=\"{0}\"", DS.Tables[0].Rows[0]["AdPicUrl"].ToString());
                if (DS.Tables[0].Rows[0]["HideFlag"].ToString() == "1") Checks = "checked=checked";
            }
        }
    }
}