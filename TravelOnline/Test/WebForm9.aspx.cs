using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Common;
using TravelOnline.Class.Purchase;
using System.Collections;
using TravelOnline.tokiomarine;
using Sunrise.Spell;

using System.Text;
using System.Data;

namespace TravelOnline.Test
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        //public CMBCHINALib.FirmClient m_fc;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string FileName = "public.key";
            //string FilePath = System.Web.HttpContext.Current.Request.ApplicationPath
            //                                  + string.Format(@"bin/{0}", FileName);

            //string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\public.key";
            ////short aa = m_fc.exCheckInfoFromBank(FilePath, "Succeed=Y&CoNo=000244&BillNo=000000&Amount=0.01&Date=20120918&MerchantPara=eb1d5e46-62a9-49e7-8090-a0210003a4c8&Msg=00210002442012091800000000000000000000&Signature=100|10|224|235|155|240|43|83|51|211|140|180|18|149|169|32|246|46|22|121|27|201|233|211|152|101|22|50|126|106|5|175|190|62|24|237|163|135|245|165|39|202|3|23|241|191|161|171|140|132|185|24|231|160|148|68|117|50|147|81|240|144|3|52|");
            ////short aa = m_fc.exCheckInfoFromBank(FilePath, "Succeed=Y&CoNo=000244&BillNo=000000&Amount=0.01&Date=20120918&MerchantPara=eb1d5e46-62a9-49e7-8090-a0210003a4c8&Msg=00210002442012091800000000000000000000&Signature=100|10|224|235|155|240|43|83|51|211|140|180|18|149|169|32|246|46|22|121|27|201|233|211|152|101|22|50|126|106|5|175|190|62|24|237|163|135|245|165|39|202|3|23|241|191|161|171|140|132|185|24|231|160|148|68|117|50|147|81|240|144|3|52|");
            //short aa = CMBC_DLL.CheckInfoFromBank(FilePath, "Succeed=Y&CoNo=000244aa&BillNo=000000&Amount=0.01&Date=20120918&MerchantPara=eb1d5e46-62a9-49e7-8090-a0210003a4c8&Msg=00210002442012091800000000000000000000&Signature=100|10|224|235|155|240|43|83|51|211|140|180|18|149|169|32|246|46|22|121|27|201|233|211|152|101|22|50|126|106|5|175|190|62|24|237|163|135|245|165|39|202|3|23|241|191|161|171|140|132|185|24|231|160|148|68|117|50|147|81|240|144|3|52|");
            ////Response.Write(FilePath);
            //Response.Write(CMBC_DLL.GetLastErr(aa));
            //Response.Write(MyConvert.CreateVerifyCode(10));
            //PurchaseAutoRun.ClearShip_Line_10235();
            PurchaseAutoRun.AutoGetOrderIntegral();
            DateTime times = DateTime.Now;
            times = times.AddDays(-14);
            Response.Write(times);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            } 
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string str = "";
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();

            while (CacheEnum.MoveNext())
            {
                str += "缓存名<b>[" + CacheEnum.Key + "]</b><br />";
            }
            //"当前网站总缓存数:" + HttpRuntime.Cache.Count + "<br />"+str;   " + CacheEnum.Value.ToString() + "
            Label1.Text = "当前网站总缓存数:" + HttpRuntime.Cache.Count + "<br />" + str;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            sendTravelBizRequestType sendTBI = new tokiomarine.sendTravelBizRequestType();
            sendTBI.RequestRelated = new tokiomarine.RequestRelatedType();
            sendTBI.Reference = new tokiomarine.ReferenceType();
            sendTBI.PolicyBasicInfo = new tokiomarine.PolicyBasicInfoType();
            sendTBI.PolicyBasicInfo.premiumSpecified = true;
            sendTBI.PolicyBasicInfo.suminsuredSpecified = true;
            sendTBI.PolicyBasicInfo.expireDateSpecified = true;
            sendTBI.PolicyBasicInfo.effectiveDateSpecified = true;
            

            sendTBI.RequestRelated.RequestType = "B01";
            sendTBI.RequestRelated.User = "OTATRASH001";
            sendTBI.RequestRelated.Password = "123123";

            sendTBI.Reference.referID = "TKM123123123";
            sendTBI.Reference.referNO = "182835593";
            sendTBI.Reference.referMessage = "";

            sendTBI.PolicyBasicInfo.productCode = "00070004";
            sendTBI.PolicyBasicInfo.currency = "01";
            sendTBI.PolicyBasicInfo.premium = 35d;
            sendTBI.PolicyBasicInfo.suminsured = 100000d;

            
            sendTBI.PolicyBasicInfo.effectiveDate = Convert.ToDateTime("2013-12-01 00:00:01");
            sendTBI.PolicyBasicInfo.expireDate = Convert.ToDateTime("2013-12-05 23:59:59");

            
            sendTBI.InsuredList = new tokiomarine.InsuredType[1];
            sendTBI.InsuredList[0] = new tokiomarine.InsuredType();
            sendTBI.InsuredList[0].policyHolderFlag = "2";
            sendTBI.InsuredList[0].name = "张三";
            sendTBI.InsuredList[0].idType = "1";
            sendTBI.InsuredList[0].idNumber = "310110196910205039";
            sendTBI.InsuredList[0].birthday = Convert.ToDateTime("1969-10-20");
            sendTBI.PolicyBasicInfo.expireDateSpecified = true;

            sendTBI.InsuredList[0].groupNo = "SCYTS-2013-11-30";
            sendTBI.InsuredList[0].planCode = "A";
            sendTBI.InsuredList[0].PremiumPerPeople = 35;
            sendTBI.InsuredList[0].TravelDurationStart = Convert.ToDateTime("2013-12-01 00:00:00");
            sendTBI.InsuredList[0].TravelDurationEnd = Convert.ToDateTime("2013-12-05 23:59:59");

            ThirdPartyManagement rsp = new tokiomarine.ThirdPartyManagement();
            rsp.Url = "http://221.133.245.13:7070/CIOD_ESB/webservices/ThirdPartyManagement";

            //sendTravelBizResponseType resoult = new tokiomarine.sendTravelBizResponseType();
            //resoult = rsp.sendTravelBiz(sendTBI);

            Label1.Text = rsp.sendTravelBiz(sendTBI).ErrorMessage;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string SqlQueryText = "select id,Destinationid from OL_Journal where parentid is null";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    SqlQueryText = string.Format("update OL_Journal set parentid='{1}' where id='{0}'",
                        DS.Tables[0].Rows[i]["id"].ToString(),
                        TravelOnline.Destination.Class.PlaceClass.GetJournalDesParentId(DS.Tables[0].Rows[i]["Destinationid"].ToString())
                    );
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string SqlQueryText = "select id,viewname from OL_View where (PinYin is null or PinYin='')";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    SqlQueryText = string.Format("update OL_View set PinYin='{1}',SortPinYin='{2}' where id='{0}'",
                        DS.Tables[0].Rows[i]["id"].ToString(),
                        Spell.MakeSpellCode(DS.Tables[0].Rows[i]["viewname"].ToString(), SpellOptions.EnableUnicodeLetter).ToLower(),
                        AllPinYin.FirstIndexCode(DS.Tables[0].Rows[i]["viewname"].ToString()).ToLower()
                    );
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                }
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            string SqlQueryText = "select id,contents from OL_Journal where seocontent is not null";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    SqlQueryText = string.Format("update OL_Journal set seocontent='{1}' where id='{0}'",
                        DS.Tables[0].Rows[i]["id"].ToString(),
                        TravelOnline.Destination.Class.PlaceClass.GetSeoLinkKeyWord(DS.Tables[0].Rows[i]["contents"].ToString(), "8")
                    );
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                }
            }
        }
        
    }
}