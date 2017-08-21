using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Cruises
{
    public partial class CruisesPicAdd : System.Web.UI.Page
    {
        public string picurl, cname, roomtype, pictype, ThumbSrc;
        public string Cid, shipid, roomid, deck, days;
        public string hide;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }

            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            Cid = Request.QueryString["Cid"];
            shipid = Request.QueryString["shipid"];
            if (!IsPostBack)
            {
                if (Cid != null)
                {
                    LoadInfo();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from CR_Pic where id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                roomid = DS.Tables[0].Rows[0]["roomid"].ToString();
                deck = DS.Tables[0].Rows[0]["deck"].ToString();
                picurl = DS.Tables[0].Rows[0]["picurl"].ToString();
                cname = DS.Tables[0].Rows[0]["cname"].ToString();
                roomtype = DS.Tables[0].Rows[0]["roomtype"].ToString();
                pictype = DS.Tables[0].Rows[0]["pictype"].ToString();

                string[] sArray = DS.Tables[0].Rows[0]["picurl"].ToString().Split('/');
                ThumbSrc = string.Format("src=\"/Upload/Cruises/{1}/Thumb_{0}\"", sArray[4].ToString(), shipid);

            }

            hide = "hide";
        }
    }
}