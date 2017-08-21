using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.Management
{
    public partial class FlashAdInfo : System.Web.UI.Page
    {
        public string id, AdName, AdPageUrl, AdPicUrl, AdSecPicUrl, AdFlag, ThumbSrc1, ThumbSrc2, AdSort, setddl, BackGround, hide;
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
            string SqlQueryText = string.Format("select * from OL_FlashAD where id='{0}'",id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //id, AdName, AdPageUrl, AdPicUrl, AdSecPicUrl, AdFlag, ThumbSrc1, ThumbSrc2;
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                AdName = DS.Tables[0].Rows[0]["AdName"].ToString();
                AdPageUrl = DS.Tables[0].Rows[0]["AdPageUrl"].ToString();
                AdPicUrl = DS.Tables[0].Rows[0]["AdPicUrl"].ToString();
                AdSecPicUrl = DS.Tables[0].Rows[0]["AdSecPicUrl"].ToString();
                AdFlag = DS.Tables[0].Rows[0]["AdFlag"].ToString();
                AdSort = DS.Tables[0].Rows[0]["AdSort"].ToString();
                BackGround = DS.Tables[0].Rows[0]["BackGround"].ToString();
                if (DS.Tables[0].Rows[0]["AdFlag"].ToString().IndexOf("LeftHot_") > -1 || DS.Tables[0].Rows[0]["AdFlag"].ToString().IndexOf("LeftArea_") > -1)
                {

                }
                else
                { 
                    string[] sArray = DS.Tables[0].Rows[0]["AdPicUrl"].ToString().Split('/');
                    ThumbSrc1 = string.Format("src=\"/Upload/AdImage/Thumb_{0}\"", sArray[3].ToString());
                    if (DS.Tables[0].Rows[0]["AdSecPicUrl"].ToString().Length > 2)
                    {
                        sArray = DS.Tables[0].Rows[0]["AdSecPicUrl"].ToString().Split('/');
                        ThumbSrc2 = string.Format("src=\"/Upload/AdImage/Thumb_{0}\"", sArray[3].ToString());
                        hide = "";
                    }
                    else
                    {
                        hide = "class=hide";
                    }
                }
             }

            setddl = "disabled=\"disabled\"";
        }
    }
}