using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Travel
{
    public partial class TravelList : System.Web.UI.Page
    {
        public string BodyId, Category, Pages, id, ProducType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string categorys, pages, pid,ProducType;
            Category = Request.QueryString["Category"];
            Pages = Request.QueryString["Pages"];
            id = Request.QueryString["Id"];
            ProducType = Request.QueryString["ProducType"];
            //Response.Write("TravelList.aspx categorys=" + categorys + " pages=" + pages);
            switch (ProducType)
            {
                case "OutBound":
                    BodyId = "pop";
                    break;
                case "InLand":
                    BodyId = "tuan";
                    break;
                default:
                    BodyId = "pop";
                    break;
            }
        }
    }
}