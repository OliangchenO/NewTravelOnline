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
    public partial class ShowNews : System.Web.UI.Page
    {
        public string NewsTitle, NewsTime, NewsContent, LineOnHotSale;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Request.QueryString["Id"];
            string id = Request.QueryString["Id"];
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/showinfo/" + id + ".html");
            Response.End();
            //LineOnHotSale = LinePreferences.LineOnHotSale("Index");
            //LoadInfo();
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select top 1 * from OL_Affiche where Id='{0}'", MyConvert.ConToInt(Request.QueryString["Id"]));

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                NewsTitle = DS.Tables[0].Rows[0]["AfficheName"].ToString();
                NewsTime = DS.Tables[0].Rows[0]["EditTime"].ToString();
                NewsContent = DS.Tables[0].Rows[0]["AfficheContent"].ToString();
            }
            else
            {
                NewsTitle = "没有找到任何新闻内容！";
            }
        }
    }
}