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
    public partial class PreferSend : System.Web.UI.Page
    {
        public string id, sellflag, deduction, range, product, begindate, enddate, par, sellprice, amount, memo;
        public string buttoninfo;
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
                if (id != null)
                {
                    LoadInfo();
                }
                else
                {
                    Response.Write("传递参数不正确，不能操作");
                    Response.End();
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
                

                id = DS.Tables[0].Rows[0]["Id"].ToString();
                //sellflag = DS.Tables[0].Rows[0]["sellflag"].ToString();
                //deduction = DS.Tables[0].Rows[0]["deduction"].ToString();
                //range = DS.Tables[0].Rows[0]["range"].ToString();
                switch (DS.Tables[0].Rows[0]["sellflag"].ToString())
                {
                    case "1":
                        sellflag = "赠送";
                        break;
                    case "2":
                        sellflag = "销售";
                        break;
                    default:
                        break;
                }
                switch (DS.Tables[0].Rows[0]["deduction"].ToString())
                {
                    case "1":
                        deduction = "整单";
                        break;
                    case "2":
                        deduction = "每人";
                        break;
                    default:
                        break;
                }
                switch (DS.Tables[0].Rows[0]["range"].ToString())
                {
                    case "1":
                        range = "全部";
                        break;
                    case "2":
                        range = "出境";
                        break;
                    case "3":
                        range = "国内";
                        break;
                    case "4":
                        range = "单项";
                        break;
                    case "5":
                        range = "邮轮";
                        break;
                    case "8":
                        range = "线路";
                        break;
                    case "9":
                        range = "产品";
                        break;
                    default:
                        break;
                }
                begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]);
                enddate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                par = DS.Tables[0].Rows[0]["par"].ToString();
                sellprice = DS.Tables[0].Rows[0]["sellprice"].ToString();
                amount = DS.Tables[0].Rows[0]["amount"].ToString();
                memo = DS.Tables[0].Rows[0]["memo"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}