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
    public partial class CruisesRoomAdd : System.Web.UI.Page
    {
        public string hide, Cid, comid, shipid, typeid, typename, roomname, roomcode, configure, deck, area, berth, intro, rooms;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

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

            Cid = Request.QueryString["Cid"];
            shipid = Request.QueryString["shipid"];
            if (!IsPostBack)
            {
                if (Cid != null)
                {
                    LoadInfo();
                }
                else 
                {
                    comid = MyDataBaseComm.getScalar("select comid from CR_Ship where id='" + shipid + "'");
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from CR_ShipRoom where id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                hide = "hide";
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                comid = DS.Tables[0].Rows[0]["comid"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                typeid = DS.Tables[0].Rows[0]["typeid"].ToString();
                typename = DS.Tables[0].Rows[0]["typename"].ToString();
                roomname = DS.Tables[0].Rows[0]["roomname"].ToString();
                roomcode = DS.Tables[0].Rows[0]["roomcode"].ToString();
                configure = DS.Tables[0].Rows[0]["configure"].ToString();
                deck = DS.Tables[0].Rows[0]["deck"].ToString();
                area = DS.Tables[0].Rows[0]["area"].ToString();
                berth = DS.Tables[0].Rows[0]["berth"].ToString();
                intro = DS.Tables[0].Rows[0]["intro"].ToString();
                rooms = DS.Tables[0].Rows[0]["rooms"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}