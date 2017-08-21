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
    public partial class LineRoomAllotAdd : System.Web.UI.Page
    {
        public string Cid, shipid, lineid, roomid, companyid, company, nums, price, thirdprice, childprice, rebate, thirdrebate, childrebate, allotflag, sellflag, recommend;
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
            lineid = Request.QueryString["lineid"];
            roomid = Request.QueryString["roomid"];

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
            string SqlQueryText = string.Format("select * from CR_RoomAllot where Id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["id"].ToString();
                allotflag = DS.Tables[0].Rows[0]["allotflag"].ToString();
                lineid = DS.Tables[0].Rows[0]["lineid"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                roomid = DS.Tables[0].Rows[0]["roomid"].ToString();
                companyid = DS.Tables[0].Rows[0]["companyid"].ToString();
                company = DS.Tables[0].Rows[0]["company"].ToString();
                nums = DS.Tables[0].Rows[0]["nums"].ToString();
                price = DS.Tables[0].Rows[0]["price"].ToString();
                thirdprice = DS.Tables[0].Rows[0]["thirdprice"].ToString();
                rebate = DS.Tables[0].Rows[0]["rebate"].ToString();
                sellflag = DS.Tables[0].Rows[0]["sellflag"].ToString();
                recommend = DS.Tables[0].Rows[0]["recommend"].ToString();

                childprice = DS.Tables[0].Rows[0]["childprice"].ToString();
                thirdrebate = DS.Tables[0].Rows[0]["thirdrebate"].ToString();
                childrebate = DS.Tables[0].Rows[0]["childrebate"].ToString();

                if (companyid == "0") companyid = "";
                //childprice, thirdrebate, childrebate
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}