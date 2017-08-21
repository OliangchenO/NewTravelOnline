using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Travel
{
    public partial class ProductsInfo : System.Web.UI.Page
    {
        public string BodyId, Category, id, ProducType;
        protected void Page_Load(object sender, EventArgs e)
        {
            Category = Request.QueryString["Category"];
            id = Request.QueryString["Id"];
            ProducType = Request.QueryString["ProducType"];
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