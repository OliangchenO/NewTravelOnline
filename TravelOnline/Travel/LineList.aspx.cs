using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Travel;

namespace TravelOnline.Travel
{
    public partial class LineList : System.Web.UI.Page
    {
        public string BodyId, Category, ProducType, AreaId, VisaId, TitleName, Map1, Map2;
        public string LineOnHotSale, LineSendAll, LineDetailSort, LineAreaSort, LineRecommendSort;
        protected void Page_Load(object sender, EventArgs e)
        {
            Category = Request.QueryString["Category"];
            ProducType = Request.QueryString["ProducType"];

            Response.Status = "301 Moved Permanently";
            Response.AddHeader("Location", "/" + ProducType.ToLower() + "/" + Category + "-0-0-0-0-0-0-0-0-1.html");
            Response.End();


            //Category = Request.QueryString["Category"];
            //AreaId = Request.QueryString["AreaId"];
            //ProducType = Request.QueryString["ProducType"];
            //VisaId = Request.QueryString["VisaId"];
            //TitleName = "旅游线路列表";
            switch (ProducType)
            {
                case "OutBound":
                    BodyId = "pop";
                    Map1 = "<a href = \"/OutBound.html\">出境旅游</a>";
                    TitleName = "出境旅游线路列表";
                    break;
                case "InLand":
                    BodyId = "tuan";
                    Map1 = "<a href = \"/InLand.html\">国内旅游</a>";
                    TitleName = "国内旅游线路列表";
                    break;
                case "FreeTour":
                    BodyId = "auction";
                    Map1 = "<a href = \"/FreeTour.html\">自由行</a>";
                    TitleName = "自由行游线路列表";
                    break;
                case "Cruises":
                    BodyId = "read";
                    AreaId = "0";
                    Map1 = "<a href = \"/Cruises.html\">邮轮旅游</a>";
                    TitleName = "邮轮旅游线路列表";
                    break;
                case "Visa":
                    BodyId = "category";
                    AreaId = "0";
                    Map1 = "<a href = \"/Visa.html\">签证办理</a>";
                    TitleName = "签证办理列表";
                    break;
                default:
                    BodyId = "pop";
                    break;
            }

            Map2 = "旅游线路列表";
            //string classname = Convert.ToString(MyDataBaseComm.getScalar(string.Format("select top 1 ProductName from OL_ProductType where MisClassId='{0}'", LineType)));

            //LineOnHotSale = LinePreferences.LineOnHotSale(ProducType);
            //LineSendAll = LinePreferences.LineSendAll(ProducType);

            if (ProducType == "Visa")
            {
                GetVisaInfo();
            }
            else
            {
                GetTravelInfo();
            }
            
            //ClientScript.RegisterStartupScript(e.GetType(), "key", " <script>LoadLineList();</script>");
        }

        protected void GetTravelInfo()
        {
            LineOnHotSale = LinePreferences.LineOnHotSale(ProducType);
            LineSendAll = LinePreferences.LineSendAll(ProducType);

            if (MyConvert.ConToInt(Request.QueryString["Category"]) != 0)
            {
                LineRecommendSort = LineDetailRegular.LineRecommendSortCreate(Category);
            }
            else
            {
                LineRecommendSort = LineDetailRegular.LineRecommendBigCreate(ProducType);
            }

            if (MyConvert.ConToInt(Request.QueryString["Category"]) != 0) LineAreaSort = LineDetailRegular.LineAreaSortCreate(Category);
            LineDetailSort = LineDetailRegular.LineDetailSortCreate();
        }

        protected void GetVisaInfo()
        {
            LineOnHotSale = LinePreferences.LineOnHotSale("OutBound");
            LineSendAll = LinePreferences.LineSendAll("OutBound");

            LineRecommendSort = LineDetailRegular.LineRecommendBigCreate("OutBound");
            if (MyConvert.ConToInt(Request.QueryString["Category"]) != 0) LineAreaSort = LineDetailRegular.VisaAreaSortCreate(Category);
            LineDetailSort = LineDetailRegular.VisaDetailSortCreate();
        }

    }
}