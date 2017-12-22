using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using TravelOnline.EncryptCode;
using TravelOnline.TravelMisWebService;

namespace TravelOnline.NewPage.erp
{
    public class ErpUtil
    {
        public static string getToken()
        {
            string requestTime = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            string infos = Convert.ToString(HttpContext.Current.Cache["ErpToken_" + requestTime]);
            //infos = "";
            if (infos == "")
            {
                RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
                string LoginName = ConfigurationManager.AppSettings["JINLoginName"].ToString();
                string PassWord = ConfigurationManager.AppSettings["JINPassWord"].ToString();

                string dateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                string Sign = SecurityCode.Md5_Encrypt(LoginName + PassWord + requestTime, 32);
                string requestxml = string.Format("<AuthorizationRQ><LoginName>{0}</LoginName ><Sign>{1}</Sign><DateTime>{2}</DateTime ></AuthorizationRQ>", LoginName, Sign, dateTime);
                IRestRequest request = new RestRequest("jjapi-ws/auth/userAuth", Method.POST);
                request.RequestFormat = DataFormat.Xml;
                request.AddHeader("Accept", "application/xml");
                request.AddParameter("application/xml", requestxml, ParameterType.RequestBody);
                XmlSerializer ser = new XmlSerializer();

                IRestResponse response = client.Execute(request);
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.LoadXml(response.Content);
                XmlNode Header = XmlDoc.SelectSingleNode("//Header");
                if (Header != null)
                {
                    string status = Header.SelectSingleNode("Status").InnerText;
                    if (status == "0")
                    {
                        XmlNode Body = XmlDoc.SelectSingleNode("//Body");
                        XmlNode Authorization = Body.SelectSingleNode("//Authorization");
                        string erpToken = Authorization.SelectSingleNode("Token").InnerText;
                        HttpContext.Current.Cache.Insert("ErpToken_" + requestTime, erpToken);
                        return erpToken;
                    }
                    else
                    {
                        string Error = Header.SelectSingleNode("Error").InnerText;
                        return Error;
                    }
                }
                return "调用失败";
            } else
            {
                return infos;
            }
            
        }

        /**
         * rpMode:  *收款方式：在线支付/优惠券/积分抵扣
         * orderNo: *订单号
         * amount:  *实收金额
         * summary: *摘要
         * checkNo: 单据号
         * bankaccount: 优惠券类型/积分类型/支付渠道
         * point: 积分值
         * proportion: 积分兑换比例
         * dsenddate: 支付时间
         * */
        public static string savePayInfo(string rpMode, string orderNo, string amount, string summary, string checkNo, string bankaccount, string dsenddate)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsOrderPayRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><OrderQuery><orderPays><orderPay>");
            Stings.Append(string.Format("<rpMode>{0}</rpMode>", rpMode));
            Stings.Append(string.Format("<orderNo>{0}</orderNo>", orderNo.Trim()));
            Stings.Append(string.Format("<amount>{0}</amount>", amount));
            Stings.Append(string.Format("<summary>{0}</summary>", summary));
            Stings.Append(string.Format("<checkNo>{0}</checkNo>", checkNo));
            Stings.Append(string.Format("<bankAccount>{0}</bankAccount>", bankaccount));
            Stings.Append("</orderPay></orderPays></OrderQuery></Body></JJTourcrsOrderPayRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsOrderPay", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNode o = XmlDoc.SelectSingleNode("//o");
            string status = o.SelectSingleNode("status").InnerText;
            if(status.Equals("0"))
            {
                throw new Exception();
            }
            return status;
        }

        public static string getTeamInfo(string startDate, string endDate, string MislineId)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsListTeamRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><TeamListQuery>");
            Stings.Append(string.Format("<startDate>{0}</startDate>", string.Format("{0:yyyy-MM-dd}", startDate)));
            Stings.Append(string.Format("<endDate>{0}</endDate>", string.Format("{0:yyyy-MM-dd}", endDate)));
            Stings.Append(string.Format("<iweboutid>{0}</iweboutid>", MislineId));
            Stings.Append("</TeamListQuery></Body></JJTourcrsListTeamRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsListTeam", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNodeList e = XmlDoc.SelectNodes("//e");
            StringBuilder Teams = new StringBuilder();
            Teams.Append("var json = [");
            string defaultStartDate = startDate;
            string defaultEndDate = endDate;
            int i = 0;
            foreach (XmlNode teamInfo in e)
            {
                defaultEndDate = teamInfo.SelectSingleNode("date").InnerText;
                if (i == 0)
                {
                    defaultStartDate = teamInfo.SelectSingleNode("date").InnerText;
                    Teams.Append("{");
                }
                else
                {
                    Teams.Append(",{");
                    
                }
                string content = "余位充足";
                int stock = MyConvert.ConToInt(teamInfo.SelectSingleNode("stock").InnerText);
                if (stock <= 9&&stock>0)
                {
                    content = "仅剩"+teamInfo.SelectSingleNode("stock").InnerText+"人";
                }else if(stock==0)
                {
                    content = "已满";
                }
                Teams.Append(string.Format("'planid': '{0}', 'date': '{1}', 'price': '{2}', 'content': '{3}'", teamInfo.SelectSingleNode("planid").InnerText, teamInfo.SelectSingleNode("date").InnerText, teamInfo.SelectSingleNode("price").InnerText, content));
                Teams.Append("}");
                i++;
            }
            Teams.Append(string.Format("] ; var defaultStartDate = '{0}';", defaultStartDate));
            Teams.Append(string.Format(" var defaultEndDate = '{0}';", defaultEndDate));
            return Teams.ToString();
        }

        public static PlanPrices getPriceInfo(string MislineId, string startDate)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsListTeamPriceRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><TeamPriceListQuery>");
            Stings.Append(string.Format("<startDate>{0}</startDate>", string.Format("{0:yyyy-MM-dd}", startDate)));
            Stings.Append(string.Format("<endDate>{0}</endDate>", string.Format("{0:yyyy-MM-dd}", startDate)));
            Stings.Append(string.Format("<iweboutid>{0}</iweboutid>", MislineId));
            Stings.Append("</TeamPriceListQuery></Body></JJTourcrsListTeamPriceRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsListTeamPrice", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            PlanPrices prices = new PlanPrices();
            //prices.Planid = TeamId;
            XmlNodeList e = XmlDoc.SelectNodes("//e");
            List<StaPrice> staPrices = new List<StaPrice>();
            List<ExtPrice> extPrices = new List<ExtPrice>();
            foreach (XmlNode priceInfo in e)
            {
                string cprcitem = priceInfo.SelectSingleNode("cprcitem").InnerText;

                switch (cprcitem)
                {
                    case "团费":
                        StaPrice staPrice = new StaPrice();
                        staPrice.PriceId = priceInfo.SelectSingleNode("upriceid").InnerText;
                        staPrice.PriceType = priceInfo.SelectSingleNode("cprctype").InnerText;
                        staPrice.Price = priceInfo.SelectSingleNode("nprice").InnerText;
                        staPrice.PriceName = priceInfo.SelectSingleNode("cmemo").InnerText;
                        staPrices.Add(staPrice);
                        break;
                    case "其他附加费用":
                        ExtPrice extPrice = new ExtPrice();
                        extPrice.PriceId = priceInfo.SelectSingleNode("upriceid").InnerText;
                        extPrice.PriceType = priceInfo.SelectSingleNode("cprctype").InnerText;
                        extPrice.Price = priceInfo.SelectSingleNode("nprice").InnerText;
                        extPrice.PriceName = priceInfo.SelectSingleNode("cmemo").InnerText;
                        extPrices.Add(extPrice);
                        break;
                    default:
                        break;
                }
            }
            prices.PlanStaPrice = staPrices.ToArray();
            prices.PlanExtPrice = extPrices.ToArray();
            return prices;
        }

        public static PlanPrices getPriceInfo(string TeamId)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsListTeamPriceRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><TeamPriceListQuery>");
            Stings.Append(string.Format("<uteamid>{0}</uteamid>", TeamId));
            Stings.Append("</TeamPriceListQuery></Body></JJTourcrsListTeamPriceRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsListTeamPrice", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            PlanPrices prices = new PlanPrices();
            prices.Planid = TeamId;
            XmlNodeList e = XmlDoc.SelectNodes("//e");
            List<StaPrice> staPrices = new List<StaPrice>();
            List<ExtPrice> extPrices = new List<ExtPrice>();
            foreach (XmlNode priceInfo in e)
            {
                string cprcitem = priceInfo.SelectSingleNode("cprcitem").InnerText;
                
                switch (cprcitem)
                {
                    case "团费":
                        StaPrice staPrice = new StaPrice();
                        staPrice.PriceId = priceInfo.SelectSingleNode("upriceid").InnerText;
                        staPrice.PriceType = priceInfo.SelectSingleNode("cprctype").InnerText;
                        staPrice.Price = priceInfo.SelectSingleNode("nprice").InnerText;
                        staPrice.PriceName = priceInfo.SelectSingleNode("cmemo").InnerText;
                        staPrices.Add(staPrice);
                        break;
                    case "其他附加费用":
                        ExtPrice extPrice = new ExtPrice();
                        extPrice.PriceId = priceInfo.SelectSingleNode("upriceid").InnerText;
                        extPrice.PriceType = priceInfo.SelectSingleNode("cprctype").InnerText;
                        extPrice.Price = priceInfo.SelectSingleNode("nprice").InnerText;
                        extPrice.PriceName = priceInfo.SelectSingleNode("cmemo").InnerText;
                        extPrice.OnlineNeeds = "0";
                        extPrices.Add(extPrice);
                        break;
                    default:
                        break;
                }
            }
            prices.PlanStaPrice = staPrices.ToArray();
            prices.PlanExtPrice = extPrices.ToArray();
            return prices;
        }

        public static string cancelOrder(string orderNo, string reason)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsCancelRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><OrderQuery><cancelOrder>");
            Stings.Append(string.Format("<orderNo>{0}</orderNo>", orderNo.Trim()));
            Stings.Append(string.Format("<reason>{0}</reason>", reason));
            Stings.Append("</cancelOrder></OrderQuery></Body></JJTourcrsCancelRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsCancel", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNode o = XmlDoc.SelectSingleNode("//o");
            string status = o.SelectSingleNode("status").InnerText;
            if (status.Equals("1"))
            {
                throw new Exception();
            }
            return status;
        }

        public static string getWechatTeamInfo(string startDate, string endDate, string MislineId)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsListTeamRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><TeamListQuery>");
            Stings.Append(string.Format("<startDate>{0}</startDate>", string.Format("{0:yyyy-MM-dd}", startDate)));
            Stings.Append(string.Format("<endDate>{0}</endDate>", string.Format("{0:yyyy-MM-dd}", endDate)));
            Stings.Append(string.Format("<iweboutid>{0}</iweboutid>", MislineId));
            Stings.Append("</TeamListQuery></Body></JJTourcrsListTeamRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsListTeam", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNodeList e = XmlDoc.SelectNodes("//e");
            StringBuilder Teams = new StringBuilder();
            Teams.Append("var json = [");
            string defaultStartDate = startDate;
            string defaultEndDate = endDate;
            int i = 0;
            foreach (XmlNode teamInfo in e)
            {
                defaultEndDate = teamInfo.SelectSingleNode("date").InnerText;
                if (i == 0)
                {
                    defaultStartDate = teamInfo.SelectSingleNode("date").InnerText;
                    Teams.Append("{");
                }
                else
                {
                    Teams.Append(",{");

                }
                string content = "余位充足";
                int stock = MyConvert.ConToInt(teamInfo.SelectSingleNode("stock").InnerText);
                if (stock <= 9)
                {
                    content = teamInfo.SelectSingleNode("price").InnerText;
                }
                Teams.Append(string.Format("'planid': '{0}', 'date': '{1}', 'price': '{2}', 'content': '{3}'", teamInfo.SelectSingleNode("planid").InnerText, teamInfo.SelectSingleNode("date").InnerText, teamInfo.SelectSingleNode("price").InnerText, content));
                Teams.Append("}");
                i++;
            }
            Teams.Append(string.Format("] ; var defaultStartDate = '{0}';", defaultStartDate));
            Teams.Append(string.Format(" var defaultEndDate = '{0}';", defaultEndDate));
            DateTime start = DateTime.Parse(defaultStartDate);
            DateTime end = DateTime.Parse(defaultEndDate);
            int Month = (end.Year - start.Year) * 12 + (end.Month - start.Month);
            Teams.Append(string.Format(" var calendarNums = '{0}';", Month==0?1:Month));
            return Teams.ToString();
        }

        public static string UpdateOrderAccount(string iweboutid, string nprice)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsUpdateOrderAccount>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><OrderAccountUpdate><orderAccount>");
            Stings.Append(string.Format("<iweboutid>{0}</iweboutid>", iweboutid.Trim()));
            Stings.Append(string.Format("<nprice>{0}</nprice>", nprice.Trim()));
            Stings.Append("</orderAccount></OrderAccountUpdate></Body></JJTourcrsUpdateOrderAccount>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsUpdateOrderAccount", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNode o = XmlDoc.SelectSingleNode("//o");
            string status = o.SelectSingleNode("status").InnerText;
            if (status.Equals("1"))
            {
                throw new Exception(o.SelectSingleNode("message").InnerText);
            }
            return status;
        }

        public static string SubGroup(string iweboutid, string groupGuestid, string groupTeamNo)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["JINWebServiceUrl"].ToString());
            StringBuilder Stings = new StringBuilder();
            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Stings.Append("<JJTourcrsSubGroupRQ>");
            Stings.Append("<Header>");
            Stings.Append(string.Format("<Token>{0}</Token>", ErpUtil.getToken()));
            Stings.Append(string.Format("<DateTime>{0}</DateTime>", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)));
            Stings.Append("</Header>");
            Stings.Append("<Body><SubGroupQuery><subGroups><subGroup>");
            Stings.Append(string.Format("<iweboutid>{0}</iweboutid>", iweboutid.Trim()));
            Stings.Append(string.Format("<groupGuestid>{0}</groupGuestid>", groupGuestid.Trim()));
            Stings.Append(string.Format("<groupTeamNo>{0}</groupTeamNo>", groupTeamNo.Trim()));
            Stings.Append("</subGroup></subGroups></SubGroupQuery></Body></JJTourcrsSubGroupRQ>");
            IRestRequest request = new RestRequest("jjapi-ws/api/JJTourcrsSubGroup", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");
            request.AddParameter("application/xml", Stings, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(response.Content);
            XmlNode o = XmlDoc.SelectSingleNode("//o");
            string status = o.SelectSingleNode("status").InnerText;
            if (status.Equals("1"))
            {
                throw new Exception(o.SelectSingleNode("message").InnerText);
            }
            return status;
        }
    }
}
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 