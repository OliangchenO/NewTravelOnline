using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class PreferInfo : System.Web.UI.Page
    {
        public string id, sellflag, deduction, range, product, begindate, enddate, par, sellprice, amount, memo, ThumbSrc, logourl;
        public string buttoninfo, pbdate, pedate, sellnums, pre_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }

            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                begindate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                enddate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                pbdate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                pedate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                buttoninfo="<a id=\"SaveInfo\" onclick=\"check_null()\" class=\"easyui-linkbutton\" plain=\"true\" iconCls=\"icon-save\" style=\"margin-left: 50px;margin-top: 10px;\">保存</a>";
                if (id != null)
                {
                    LoadInfo();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff from Pre_Policy where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["ff"].ToString() != "0")
                {
                    buttoninfo = "<a id=\"SaveInfo\" onclick=\"savenums()\" class=\"easyui-linkbutton\" plain=\"true\" iconCls=\"icon-save\" style=\"margin-left: 50px;margin-top: 10px;\">只保存数量、有效期</a>";

                }

                id = DS.Tables[0].Rows[0]["Id"].ToString();
                sellflag = DS.Tables[0].Rows[0]["sellflag"].ToString();
                deduction = DS.Tables[0].Rows[0]["deduction"].ToString();
                range = DS.Tables[0].Rows[0]["range"].ToString();
                product = DS.Tables[0].Rows[0]["product"].ToString();
                begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]);
                enddate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                pbdate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["pbdate"]);
                pedate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["pedate"]);
                par = DS.Tables[0].Rows[0]["par"].ToString();
                sellprice = DS.Tables[0].Rows[0]["sellprice"].ToString();
                amount = DS.Tables[0].Rows[0]["amount"].ToString();
                memo = DS.Tables[0].Rows[0]["memo"].ToString();
                sellnums = DS.Tables[0].Rows[0]["sellnums"].ToString();
                logourl = DS.Tables[0].Rows[0]["picurl"].ToString();
                pre_no = DS.Tables[0].Rows[0]["pre_no"].ToString();
                if (logourl.Length > 10)
                {
                    string[] sArray = DS.Tables[0].Rows[0]["picurl"].ToString().Split('/');
                    ThumbSrc = string.Format("src=\"/Upload/Coupon/Thumb_{0}\"", sArray[3].ToString());
                }
                
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}