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
    public partial class CruisesShipAdd : System.Web.UI.Page
    {
        public string Cid, comid, series, seriesname, cname, ename, tonnage, native, capacity, length, width, waterline, deck, speed;
        public string firstseaway, rooms, voltage, feature, restaurant, collection, meeting, bar, amusement, others, free, charge;
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
            comid = Request.QueryString["comid"];
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
            string SqlQueryText = string.Format("select * from CR_Ship where id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                cname = DS.Tables[0].Rows[0]["cname"].ToString();
                ename = DS.Tables[0].Rows[0]["ename"].ToString();
                series = DS.Tables[0].Rows[0]["series"].ToString();
                seriesname = DS.Tables[0].Rows[0]["seriesname"].ToString();
                tonnage = DS.Tables[0].Rows[0]["tonnage"].ToString();
                native = DS.Tables[0].Rows[0]["native"].ToString();
                capacity = DS.Tables[0].Rows[0]["capacity"].ToString();
                length = DS.Tables[0].Rows[0]["length"].ToString();
                width = DS.Tables[0].Rows[0]["width"].ToString();
                waterline = DS.Tables[0].Rows[0]["waterline"].ToString();
                deck = DS.Tables[0].Rows[0]["deck"].ToString();
                speed = DS.Tables[0].Rows[0]["speed"].ToString();
                firstseaway = DS.Tables[0].Rows[0]["firstseaway"].ToString();
                rooms = DS.Tables[0].Rows[0]["rooms"].ToString();
                voltage = DS.Tables[0].Rows[0]["voltage"].ToString();
                feature = DS.Tables[0].Rows[0]["feature"].ToString();
                restaurant = DS.Tables[0].Rows[0]["restaurant"].ToString();
                collection = DS.Tables[0].Rows[0]["collection"].ToString();
                meeting = DS.Tables[0].Rows[0]["meeting"].ToString();
                bar = DS.Tables[0].Rows[0]["bar"].ToString();
                amusement = DS.Tables[0].Rows[0]["amusement"].ToString();
                others = DS.Tables[0].Rows[0]["others"].ToString();
                free = DS.Tables[0].Rows[0]["free"].ToString();
                charge = DS.Tables[0].Rows[0]["charge"].ToString(); 
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}