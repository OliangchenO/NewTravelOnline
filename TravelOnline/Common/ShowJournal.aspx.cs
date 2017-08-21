using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Travel;

namespace TravelOnline.Common
{
    public partial class ShowJournal : System.Web.UI.Page
    {
        public string NewsTitle, NewsTime, NewsContent, LineOnHotSale, NewsUser, Keywords, Title1, Journal;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["Id"];
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/showjournal/" + id + ".html");
            Response.End();
            //LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            //LoadInfo();
        }

        protected void LoadInfo()
        {
            string SqlQueryText = "";
            if (Request.QueryString["flag"] == "audit")
            {
                SqlQueryText = string.Format("select top 1 *,(select UserName from OL_LoginUser where id=OL_Journal.userid) as NewsUser from OL_Journal where Id='{0}'", MyConvert.ConToInt(Request.QueryString["Id"]));
            }
            else
            {
                SqlQueryText = string.Format("select top 1 *,(select UserName from OL_LoginUser where id=OL_Journal.userid) as NewsUser from OL_Journal where flag='1' and Id='{0}'", MyConvert.ConToInt(Request.QueryString["Id"]));
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Journal = LinePreferences.JournalLine(DS.Tables[0].Rows[0]["FirstDestination"].ToString());

                if (DS.Tables[0].Rows[0]["seo"].ToString().Length > 2)
                {
                    Keywords = DS.Tables[0].Rows[0]["seo"].ToString();
                    Title1 = DS.Tables[0].Rows[0]["seo"].ToString();
                }
                else
                {
                    Keywords = DS.Tables[0].Rows[0]["title"].ToString();
                    Title1 = DS.Tables[0].Rows[0]["title"].ToString();
                }
                NewsTitle = DS.Tables[0].Rows[0]["title"].ToString();
                NewsTime = DS.Tables[0].Rows[0]["inputdate"].ToString();
                NewsContent = DS.Tables[0].Rows[0]["contents"].ToString();
                NewsUser = DS.Tables[0].Rows[0]["NewsUser"].ToString();
            }
            else
            {
                NewsTitle = "没有找到任何内容！";
            }
        }
    }
}