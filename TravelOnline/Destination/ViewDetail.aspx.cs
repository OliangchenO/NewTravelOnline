using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace TravelOnline.Destination
{
    public partial class ViewDetail : System.Web.UI.Page
    {
        public string id, desid, viewid, picname, picmemo, picurl, ThumbSrc1, ThumbSrc2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            id = Request.QueryString["Id"];
            viewid = Request.QueryString["viewid"];
            desid = Request.QueryString["desid"];
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
            string SqlQueryText = string.Format("select * from OL_ViewPic where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["id"].ToString();
                desid = DS.Tables[0].Rows[0]["desid"].ToString();
                viewid = DS.Tables[0].Rows[0]["viewid"].ToString();
                picname = DS.Tables[0].Rows[0]["picname"].ToString();
                picmemo = DS.Tables[0].Rows[0]["picmemo"].ToString();
                picurl = DS.Tables[0].Rows[0]["picurl"].ToString();
                string[] sArray = DS.Tables[0].Rows[0]["picurl"].ToString().Split('/');
                ThumbSrc1 = string.Format("src=\"/Upload/View/{0}/{1}/S_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
            }
        }
    }
}