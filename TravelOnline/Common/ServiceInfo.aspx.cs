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
    public partial class ServiceInfo : System.Web.UI.Page
    {
        public string NewsTitle, NewsTime, NewsContent, LineOnHotSale;
        StringBuilder Strings = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/service/aboutus.html");
            Response.End();
            //Flag = Request.QueryString["Flag"]; //Service7
            switch (Request.QueryString["Flag"])
            {
                case "Service7":
                    AboutUs("Service7");
                    NewsTitle = "关于我们";
                    break;
                case "Service1":
                    AboutUs("Service1");
                    NewsTitle = "联系我们";
                    break;
                case "Service2":
                    AboutUs("Service2");
                    NewsTitle = "人才招聘";
                    break;
                default:
                    Response.End();
                    break;
            }
        }

        protected void AboutUs(string types)
        {
            string SqlQueryText = string.Format("select * from OL_Affiche where AfficheType='{0}' order by EditTime desc", types);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {

                    Strings.Append(string.Format("<DIV class=mtc><STRONG>{0}</STRONG><DIV class=extra></DIV></DIV>", DS.Tables[0].Rows[i]["AfficheName"].ToString()));
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[i]["AfficheContent"].ToString()));
                }
                NewsContent = Strings.ToString();
            }
            else
            {
                NewsTitle = "没有找到任何内容！";
            }
        }

        //protected void LoadInfo()
        //{
        //    string SqlQueryText = string.Format("select top 1 * from OL_Affiche where Id='{0}'", MyConvert.ConToInt(Request.QueryString["Id"]));

        //    DataSet DS = new DataSet();
        //    DS.Clear();
        //    DS = MyDataBaseComm.getDataSet(SqlQueryText);
        //    if (DS.Tables[0].Rows.Count > 0)
        //    {
        //        NewsTitle = DS.Tables[0].Rows[0]["AfficheName"].ToString();
        //        NewsTime = DS.Tables[0].Rows[0]["EditTime"].ToString();
        //        NewsContent = DS.Tables[0].Rows[0]["AfficheContent"].ToString();
        //    }
        //    else
        //    {
        //        NewsTitle = "没有找到任何内容！";
        //    }
        //}
    }
}