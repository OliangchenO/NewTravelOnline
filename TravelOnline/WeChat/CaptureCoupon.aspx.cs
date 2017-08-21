using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.WeChat
{
    public partial class CaptureCoupon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("app/order#login", true);

            captureCoupon();
        }

        private void captureCoupon()
        {
            string SqlQueryText = string.Format("select * from Pre_Policy where id='{0}'", Request.QueryString["Cid"].Trim());
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                string captureCount = MyDataBaseComm.getScalar("select count(1) from Pre_Ticket where pid='" + Request.QueryString["Cid"].Trim() + "' and userid='" + Convert.ToString(Session["Online_UserId"]) + "'");
                if (MyConvert.ConToInt(captureCount) > 0)
                {
                    Response.Write("<script>alert('领取失败，你已经领用过此优惠券！');window.location.href='/WX_AD/double_11/';</script>");
                    Response.End();
                }
                String AutoId = Convert.ToString(CombineKeys.NewComb());

                SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                    DS1.Tables[0].Rows[0]["id"].ToString(),
                    AutoId,
                    MyConvert.CreateVerifyCode(12),
                    DS1.Tables[0].Rows[0]["par"].ToString(),
                    DS1.Tables[0].Rows[0]["amount"].ToString(),
                    Convert.ToString(Session["Online_UserId"]),
                    DS1.Tables[0].Rows[0]["begindate"].ToString(),
                    DS1.Tables[0].Rows[0]["enddate"].ToString(),
                    DateTime.Now.ToString(),
                    "0",
                    DS1.Tables[0].Rows[0]["deduction"].ToString(),
                    DS1.Tables[0].Rows[0]["range"].ToString(),
                    DS1.Tables[0].Rows[0]["product"].ToString(),
                    Convert.ToString(Session["Online_UserEmail"]),
                    Convert.ToString(Session["Online_UserName"]),
                    DS1.Tables[0].Rows[0]["pbdate"].ToString(),
                    DS1.Tables[0].Rows[0]["pedate"].ToString(),
                    DS1.Tables[0].Rows[0]["sellflag"].ToString()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("<script>alert('领取成功，可至会员中心-我的优惠券，查看所领取的优惠券！');window.location.href='/app/order#coupon';</script>");
                    Response.End();
                }
                else
                {
                    Response.Write("<script>alert('领取失败，请稍后再试！');window.location.href='/WX_AD/double_11/';</script>");
                    Response.End();
                }
            }
            else
            {
                Response.Write("<script>alert('领取失败，优惠券不存在！');window.location.href='/WX_AD/double_11/';</script>");
                Response.End();
            }
        }
    }
}