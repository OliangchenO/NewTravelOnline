using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using TravelOnline.WeChat.freetrip.model;
using ZXing;
using ZXing.Common;

namespace TravelOnline.WeChat.freetrip.interfaces
{
    public class TradingAreaService
    {
        public static List<TradingArea> getTradingArea(TradingAreaRS rs)
        {
            List<TradingArea> tradingAreaList = new List<TradingArea>();
            string sqlstr = "SELECT * FROM OL_TradingArea where 1=1 ";
            if (rs.id != 0) sqlstr = string.Format("{0} and id = '{1}' ", sqlstr, rs.id);
            if (rs.name != null) sqlstr = string.Format("{0} and name like '%{1}%' ", sqlstr, rs.name.Trim());
            if (rs.flag != null) sqlstr = string.Format("{0} and flag = '{1}' ", sqlstr, rs.flag.Trim());
            if (rs.destid != null) sqlstr = string.Format("{0} and destid = '{1}' ", sqlstr, rs.destid.Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    TradingArea tradingArea = new TradingArea();
                    tradingArea.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                    tradingArea.name = DS.Tables[0].Rows[i]["name"].ToString();
                    tradingArea.flag = DS.Tables[0].Rows[i]["flag"].ToString();
                    tradingArea.detail = DS.Tables[0].Rows[i]["detail"].ToString();
                    tradingArea.pic = DS.Tables[0].Rows[i]["pic"].ToString();
                    tradingArea.destid = DS.Tables[0].Rows[i]["destid"].ToString();
                    tradingAreaList.Add(tradingArea);
                }
            }
            return tradingAreaList;
        }

        public static Boolean insertTradingArea(TradingArea tradingArea)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("insert into dbo.OL_TradingArea (name,flag,detail,pic,destid,destname,EditTime,EditUserId,EditUserName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    tradingArea.name,
                    tradingArea.flag,
                    tradingArea.detail,
                    tradingArea.pic,
                    tradingArea.destid,
                    tradingArea.destname,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static Boolean updateTradingArea(TradingArea tradingArea)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("update dbo.OL_TradingArea set name='{1}',flag='{2}',detail='{3}',pic='{4}',destid='{5}',destname='{6}',EditTime='{7}',EditUserId='{8}',EditUserName='{9}' where id={0}",
                    tradingArea.id,
                    tradingArea.name,
                    tradingArea.flag,
                    tradingArea.detail,
                    tradingArea.pic,
                    tradingArea.destid,
                    tradingArea.destname,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static List<TActivity> getTActivity(TActivityRS rs)
        {
            List<TActivity> activityList = new List<TActivity>();
            string sqlstr = "SELECT * FROM OL_TActivity where 1=1 ";
            if (rs.id != 0) sqlstr = string.Format("{0} and id = '{1}' ", sqlstr, rs.id);
            if (rs.name != null) sqlstr = string.Format("{0} and name like '%{1}%' ", sqlstr, rs.name.Trim());
            if (rs.tradingAreaId != null) sqlstr = string.Format("{0} and tradingAreaId in ('{1}') ", sqlstr, rs.tradingAreaId.Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    TActivity activity = new TActivity();
                    activity.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                    activity.tradingAreaId = DS.Tables[0].Rows[i]["tradingAreaId"].ToString();
                    activity.name = DS.Tables[0].Rows[i]["name"].ToString();
                    activity.context = DS.Tables[0].Rows[i]["context"].ToString();
                    activity.color = DS.Tables[0].Rows[i]["color"].ToString();
                    activity.key = DS.Tables[0].Rows[i]["title"].ToString();
                    activityList.Add(activity);
                }
            }
            return activityList;
        }

        public static Boolean insertTActivity(TActivity activity)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("insert into dbo.OL_TActivity (name,context,tradingAreaId,title,color,EditTime,EditUserId,EditUserName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    activity.name,
                    activity.context,
                    activity.tradingAreaId,
                    activity.key,
                    activity.color,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static Boolean updateTActivity(TActivity activity)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("update dbo.OL_TActivity set name='{1}',context='{2}',tradingAreaId='{3}',title='{4}',color='{5}',EditTime='{6}',EditUserId='{7}',EditUserName='{8}' where id={0}",
                    activity.id,
                    activity.name,
                    activity.context,
                    activity.tradingAreaId,
                    activity.key,
                    activity.color,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static List<TCoupon> getTCoupon(TCouponRS rs)
        {
            List<TCoupon> couponList = new List<TCoupon>();
            string sqlstr = "SELECT * FROM OL_TCoupon where 1=1 ";
            if (rs.id != 0) sqlstr = string.Format("{0} and id = '{1}' ", sqlstr, rs.id);
            if (rs.tradingAreaId != null) sqlstr = string.Format("{0} and tradingAreaId in ('{1}') ", sqlstr, rs.tradingAreaId.Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    TCoupon coupon = new TCoupon();
                    coupon.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                    coupon.tradingAreaId = DS.Tables[0].Rows[i]["tradingAreaId"].ToString();
                    coupon.starDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[i]["starDate"].ToString());
                    coupon.endDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[i]["endDate"].ToString());
                    coupon.context = DS.Tables[0].Rows[i]["context"].ToString();
                    coupon.barCode = DS.Tables[0].Rows[i]["barCode"].ToString();
                    if (coupon.barCode != null && coupon.barCode != "")
                    {
                        coupon.barCodeImg = createBarCode(coupon.barCode);
                    }
                    couponList.Add(coupon);
                }
            }
            return couponList;
        }

        public static string createBarCode(string code)
        {
            EncodingOptions options = null;
            BarcodeWriter writer = null;
            options = new EncodingOptions
            {
                Width = 250,
                Height = 80,
                PureBarcode=false,
                Margin=3
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.ITF;
            writer.Options = options;
            Bitmap bitmap = writer.Write(code);
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            var strbaser64 = "data:image/png;base64," + Convert.ToBase64String(arr);
            return strbaser64;
        }

        public static Boolean insertTCoupon(TCoupon coupon)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("insert into dbo.OL_TCoupon (barCode,context,tradingAreaId,starDate,endDate,EditTime,EditUserId,EditUserName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    coupon.barCode,
                    coupon.context,
                    coupon.tradingAreaId,
                    coupon.starDate,
                    coupon.endDate,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static Boolean updateTCoupon(TCoupon coupon)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("update dbo.OL_TCoupon set barCode='{1}',context='{2}',tradingAreaId='{3}',starDate='{4}',endDate='{5}',EditTime='{6}',EditUserId='{7}',EditUserName='{8}' where id={0}",
                    coupon.id,
                    coupon.barCode,
                    coupon.context,
                    coupon.tradingAreaId,
                    coupon.starDate,
                    coupon.endDate,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }
        public static List<TStore> getTStore(TStoreRS rs)
        {
            List<TStore> storeList = new List<TStore>();
            string sqlstr = "SELECT * FROM OL_TStore where 1=1 ";
            if (rs.id != 0) sqlstr = string.Format("{0} and id = '{1}' ", sqlstr, rs.id);
            if (rs.name != null) sqlstr = string.Format("{0} and name like '%{1}%' ", sqlstr, rs.name.Trim());
            if (rs.tradingAreaId != null) sqlstr = string.Format("{0} and tradingAreaId in ('{1}') ", sqlstr, rs.tradingAreaId.Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    TStore store = new TStore();
                    store.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                    store.tradingAreaId = DS.Tables[0].Rows[i]["tradingAreaId"].ToString();
                    store.name = DS.Tables[0].Rows[i]["name"].ToString();
                    store.context = DS.Tables[0].Rows[i]["context"].ToString();
                    store.link = DS.Tables[0].Rows[i]["link"].ToString();
                    store.pic = DS.Tables[0].Rows[i]["pic"].ToString();
                    storeList.Add(store);
                }
            }
            return storeList;
        }
        public static Boolean insertTStore(TStore store)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("insert into dbo.OL_TStore (name,context,tradingAreaId,pic,link,EditTime,EditUserId,EditUserName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    store.name,
                    store.context,
                    store.tradingAreaId,
                    store.pic,
                    store.link,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static Boolean updateTStore(TStore store)
        {
            Boolean result = false;
            string SqlQueryText = string.Format("update dbo.OL_TStore set name='{1}',context='{2}',tradingAreaId='{3}',pic='{4}',link='{5}',EditTime='{6}',EditUserId='{7}',EditUserName='{8}' where id={0}",
                    store.id,
                    store.name,
                    store.context,
                    store.tradingAreaId,
                    store.pic,
                    store.link,
                    DateTime.Now,
                    HttpContext.Current.Session["Manager_UserId"],
                    HttpContext.Current.Session["Manager_UserName"]
                );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                result = true;
            }
            return result;
        }

        public static string getShopDetail(string tradingAreaId)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_ShopDetail_" + tradingAreaId]);
            if (infos=="")
            {
                ShopDetail shopDetail = new ShopDetail();
                //获取商圈信息
                TradingAreaRS tradingAreaRS = new TradingAreaRS();
                tradingAreaRS.id = MyConvert.ConToInt(tradingAreaId);
                shopDetail.tradingArea = TradingAreaService.getTradingArea(tradingAreaRS);
                //获取活动信息
                TActivityRS activityRS = new TActivityRS();
                activityRS.tradingAreaId = tradingAreaId;
                shopDetail.activity = TradingAreaService.getTActivity(activityRS);
                //获取优惠券信息
                TCouponRS couponRS = new TCouponRS();
                couponRS.tradingAreaId = tradingAreaId;
                shopDetail.coupon = TradingAreaService.getTCoupon(couponRS);
                //获取店铺信息
                TStoreRS storeRS = new TStoreRS();
                activityRS.tradingAreaId = tradingAreaId;
                shopDetail.store = TradingAreaService.getTStore(storeRS);
                infos = JsonMapper.ToJson(shopDetail);
                HttpContext.Current.Cache.Insert("WeChat_ShopDetail_" + tradingAreaId, infos);
            }
            return infos;
        }
    }
}