using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Saving;
using System.Xml;

namespace TravelOnline.Purchase
{
    public partial class WordOutPut : System.Web.UI.Page
    {
        public string Action, Cid, AutoId, AutoDate;
        public string path;
        public string LineName, BeginDate, EndDate, Nums, AllPrice;
        public string OrderName, TourstName, Tel, Sex, Age, LineDays, PlanNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            Action = Request.QueryString["Action"];
            Cid = Request.QueryString["Cid"];

            if (Action != "LineRoute")
            {
                if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                {
                }
                else{
                    Response.Redirect("/login/login.aspx", true);
                    Response.End();
                }
            }            

            switch (Action)
            {
                case "OutBound":
                    ContractOutPut();
                    break;
                case "InLand":
                    ContractOutPut();
                    break;
                case "Cruises":
                    ContractOutPut();
                    break;
                case "OrderRoute":
                    OrderRoute();
                    break;
                case "LineRoute":
                    OrderRoute();
                    break;
                default:
                    Response.Redirect("/login/login.aspx", true);
                    Response.End();
                    break;
            }
        }

        protected void ContractOutPut()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select *,(select top 1 PayTime from OL_PayMent where OrderId=OL_Order.OrderId order by PayTime desc) as PayTime from OL_Order where OrderId='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                AutoDate = string.Format("{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["PayTime"]);
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                OrderName = DS.Tables[0].Rows[0]["OrderName"].ToString();
                Tel = DS.Tables[0].Rows[0]["OrderTel"].ToString() + " " + DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
                AllPrice = DS.Tables[0].Rows[0]["Price"].ToString();
                BeginDate = string.Format("{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                EndDate = string.Format("{0:yyyy年MM月dd日}", Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"]).AddDays(Convert.ToInt32(DS.Tables[0].Rows[0]["LineDays"]) - 1));
                PlanNo = DS.Tables[0].Rows[0]["PlanNo"].ToString();
            }
            if (BeginDate == "1900年01月01日")
            {
                BeginDate = "";
                EndDate = "";
            }
            if (AutoDate == "1900年01月01日")
            {
                AutoDate = "";
            }

            SqlQueryText = string.Format("select top 1 *,DATEDIFF(yy, BirthDay, GETDATE()) AS age from OL_GuestInfo where OrderId='{0}'", Cid);
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                TourstName = DS.Tables[0].Rows[0]["GuestName"].ToString();
                if (DS.Tables[0].Rows[0]["GuestName"].ToString() == "F")
                {
                    Sex = "女";
                }
                else
                {
                    Sex = "男";
                }
                Age = DS.Tables[0].Rows[0]["age"].ToString();
            }

            Document doc = new Document(string.Format(@"{0}OfficeFiles\{1}Contract.doc", AppDomain.CurrentDomain.BaseDirectory, Action));
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.PageSetup.PaperSize = PaperSize.A4;

            doc.MailMerge.Execute(new String[] { "合同编号", "合同日期", "游客姓名", "联系电话", "性别", "年龄" }, new Object[] { AutoId, AutoDate, TourstName, Tel, Sex, Age });
            doc.MailMerge.Execute(new String[] { "线路名称", "天数", "出发日期", "返回日期" }, new Object[] { LineName, LineDays, BeginDate, EndDate });
            doc.MailMerge.Execute(new String[] { "费用合计", "订单人数", "团号" }, new Object[] { AllPrice, Nums, PlanNo });
            doc.MailMerge.DeleteFields();
            doc.Save(Response, LineName + " 旅游合同.doc", ContentDisposition.Attachment, SaveOptions.CreateSaveOptions(SaveFormat.Doc));

        }

        protected void OrderRoute()
        {
            string SqlQueryText;
            if (Action == "LineRoute")
            {
                SqlQueryText = string.Format("select MisLineId,LineName from OL_Line where MisLineId='{0}'", Cid);
            }
            else
            {
                SqlQueryText = string.Format("select OrderId,LineID,OrderTime,LineName,BeginDate,PlanNo,LineDays from OL_Order where OrderId='{0}'", Cid);
            }
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                string OutPutFlag;
                string yyyyMM;
                string OrderXml;
                OutPutFlag = "0";
                if (Action == "LineRoute")
                {
                    OrderXml = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, DS.Tables[0].Rows[0]["MisLineId"].ToString());
                    if (System.IO.File.Exists(OrderXml) == true)
                    {
                        OutPutFlag = "1";
                    }
                }
                else
                {
                    BeginDate = string.Format("{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                    EndDate = string.Format("{0:yyyy年MM月dd日}", Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"]).AddDays(Convert.ToInt32(DS.Tables[0].Rows[0]["LineDays"]) - 1));
                    PlanNo = DS.Tables[0].Rows[0]["PlanNo"].ToString();

                    yyyyMM = string.Format("{0:yyyy-MM}", Convert.ToDateTime(DS.Tables[0].Rows[0]["OrderTime"]));
                    OrderXml = string.Format(@"{0}XML\OrderRoute\{1}\{2}.xml", AppDomain.CurrentDomain.BaseDirectory, yyyyMM, DS.Tables[0].Rows[0]["OrderId"].ToString());
                    if (System.IO.File.Exists(OrderXml) == true)
                    {
                        OutPutFlag = "1";
                    }
                    else
                    {
                        OrderXml = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, DS.Tables[0].Rows[0]["LineID"].ToString());
                        if (System.IO.File.Exists(OrderXml) == true)
                        {
                            OutPutFlag = "1";
                        }
                    }
                }
                
                if (OutPutFlag != "1")
                {
                    Response.Write("没有找到行程数据，不能下载！");
                    Response.End();
                }
                else
                {
                    StringBuilder Strings = new StringBuilder();
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.Load(OrderXml);
                    XmlNode x = XmlDoc.SelectSingleNode("//Route");
                    if (x != null)
                    {
                        string RouteFeature, Traffic, Hotel, Scenery, Foods, Guide, Insure, Others, PriceIn, PriceOut, OwnExpense, Attentions, Shopping;
                        RouteFeature = x.SelectSingleNode("Feature").InnerText;
                        Traffic = x.SelectSingleNode("Traffic").InnerText;
                        Hotel = x.SelectSingleNode("Hotel").InnerText;
                        Scenery = x.SelectSingleNode("Scenery").InnerText;
                        Foods = x.SelectSingleNode("Foods").InnerText;
                        Guide = x.SelectSingleNode("Guide").InnerText;
                        Insure = x.SelectSingleNode("Insure").InnerText;
                        Others = x.SelectSingleNode("Others").InnerText;

                        PriceIn = x.SelectSingleNode("PriceIn").InnerText;
                        PriceOut = x.SelectSingleNode("PriceOut").InnerText;
                        OwnExpense = x.SelectSingleNode("OwnExpense").InnerText;
                        Attentions = x.SelectSingleNode("Attentions").InnerText;
                        Shopping = x.SelectSingleNode("Shopping").InnerText;

                        StringBuilder PicString = new StringBuilder();
                        XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");

                        DataSet XmlDS = new DataSet();
                        DataRow Drow;
                        XmlDS.Clear();
                        XmlDS.Tables.Add();
                        XmlDS.Tables[0].Columns.Add("daterank");
                        XmlDS.Tables[0].Columns.Add("rname");
                        XmlDS.Tables[0].Columns.Add("bus");
                        XmlDS.Tables[0].Columns.Add("route");
                        XmlDS.Tables[0].Columns.Add("dinner");
                        XmlDS.Tables[0].Columns.Add("room");
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            Drow = XmlDS.Tables[0].NewRow();
                            Drow["daterank"] = elemList[i].SelectSingleNode("daterank").InnerText;
                            Drow["rname"] = elemList[i].SelectSingleNode("rname").InnerText;
                            Drow["bus"] = elemList[i].SelectSingleNode("bus").InnerText;
                            Drow["route"] = elemList[i].SelectSingleNode("route").InnerText;
                            Drow["dinner"] = elemList[i].SelectSingleNode("dinner").InnerText;
                            Drow["room"] = elemList[i].SelectSingleNode("room").InnerText;
                            XmlDS.Tables[0].Rows.Add(Drow);                            
                        }

                        Document doc = new Document(string.Format(@"{0}OfficeFiles\RouteTemplate.doc", AppDomain.CurrentDomain.BaseDirectory));
                        DocumentBuilder builder = new DocumentBuilder(doc);
                        builder.PageSetup.PaperSize = PaperSize.A4;
                        //string RouteFeature, Traffic, Hotel, Scenery, Foods, Guide, Insure, Others, PriceIn, PriceOut, OwnExpense, Attentions, Shopping;

                        doc.MailMerge.Execute(new String[] { "线路名称", "线路特色", "旅游交通", "用餐标准" }, new Object[] { LineName, RouteFeature, Traffic, Foods });
                        doc.MailMerge.Execute(new String[] { "住宿标准", "景点门票", "导游服务", "保险服务" }, new Object[] { Hotel, Scenery, Guide, Insure });
                        doc.MailMerge.Execute(new String[] { "其他服务", "报价包含", "报价不含" }, new Object[] { Others, PriceIn, PriceOut });
                        doc.MailMerge.Execute(new String[] { "自费项目", "购物商店", "注意事项" }, new Object[] { OwnExpense, Shopping, Attentions });

                        if (Action != "LineRoute")
                        {
                            doc.MailMerge.Execute(new String[] { "团号" }, new Object[] { "团号：" + PlanNo + "  出发日期" + BeginDate + " - " + EndDate });
                        }

                        DataTable RouteDs = XmlDS.Tables[0];
                        RouteDs.TableName = "RouteList";
                        doc.MailMerge.ExecuteWithRegions(RouteDs);

                        doc.MailMerge.DeleteFields();
                        doc.Save(Response, LineName + " 行程.doc", ContentDisposition.Attachment, SaveOptions.CreateSaveOptions(SaveFormat.Doc));
                    }
                }
            }
        }

        protected void OutPut()
        { 

        }
    }
}