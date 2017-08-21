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

namespace TravelOnline.Destination
{
    public partial class ViewInfo : System.Web.UI.Page
    {
        public string pagetitle, BuyButton, OldName, PinYin, SortPinYin, SeoName;
        public string id, desid, uid, viewname, tags, tel, address, ticket, ticketmemo, opentime, map_x, map_y, map_size,visitseason, visittime, intro, viewpoint, traffic, memo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            id = Request.QueryString["id"];
            desid = Request.QueryString["desid"];
            if (!IsPostBack)
            {
                if (id != null)
                {
                    pagetitle = "修改景点信息";
                    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save()\">保 存</A>";
                    LoadInfo();
                }
                else
                {
                    pagetitle = "新增景点";
                    pagetitle += " - " + MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + desid + "'");
                    BuyButton = "<A id=\"AddBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"AddNew()\">新 增</A> <A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save()\">保 存</A>";
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_View where id='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["id"].ToString();
                uid = DS.Tables[0].Rows[0]["uid"].ToString();
                desid = DS.Tables[0].Rows[0]["desid"].ToString();
                viewname = DS.Tables[0].Rows[0]["viewname"].ToString();
                OldName = DS.Tables[0].Rows[0]["viewname"].ToString();
                PinYin = DS.Tables[0].Rows[0]["PinYin"].ToString();
                SortPinYin = DS.Tables[0].Rows[0]["SortPinYin"].ToString();

                tags = DS.Tables[0].Rows[0]["tags"].ToString();
                tel = DS.Tables[0].Rows[0]["tel"].ToString();
                address = DS.Tables[0].Rows[0]["address"].ToString();
                ticket = DS.Tables[0].Rows[0]["ticket"].ToString();
                ticketmemo = DS.Tables[0].Rows[0]["ticketmemo"].ToString();
                opentime = DS.Tables[0].Rows[0]["opentime"].ToString();
                map_x = DS.Tables[0].Rows[0]["map_x"].ToString();
                map_y = DS.Tables[0].Rows[0]["map_y"].ToString();
                map_size = DS.Tables[0].Rows[0]["map_size"].ToString();
                visitseason = DS.Tables[0].Rows[0]["visitseason"].ToString();
                visittime = DS.Tables[0].Rows[0]["visittime"].ToString();
                intro = DS.Tables[0].Rows[0]["intro"].ToString();
                viewpoint = DS.Tables[0].Rows[0]["viewpoint"].ToString();
                traffic = DS.Tables[0].Rows[0]["traffic"].ToString();
                memo = DS.Tables[0].Rows[0]["memo"].ToString();
                SeoName = DS.Tables[0].Rows[0]["SeoName"].ToString();
                pagetitle += " - " + MyDataBaseComm.getScalar("select DestinationName from OL_Destination where id='" + desid + "'");
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}