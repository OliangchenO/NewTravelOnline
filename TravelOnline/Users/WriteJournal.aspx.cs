using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.LoginUsers;
using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;

namespace TravelOnline.Users
{
    public partial class WriteJournal : System.Web.UI.Page
    {
        public string ImportFlag,auditscript, upid, pagetitle, flag, BuyButton, ucode, Uid, title, contents, tags, coverpic, coverid, albumid, hide, hide1, hide2, coverpichtml, DestinationInfos, seo, coverpicurl;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
            
            }
            else
            {
                Response.Redirect("/login/login.aspx", true);
            }
                
            Uid = Request.QueryString["uid"];
            flag = Request.QueryString["flag"];
            hide1 = "hide";
            if (flag == "audit")
            {
                auditscript = "//";
                hide1 = "";
                if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@7") > -1)
                { }
                else
                {
                    Response.Write("没有操作权限！");
                    Response.End();
                }
                BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save('A')\">审核通过</A>";
            }
            else
            {
                if (Uid != null)
                {
                    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save('S')\">修 改</A>";
                }
                else
                {
                    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"Save('S')\">发 表</A>";
                }
            }
            hide = "hide";

            if (!IsPostBack)
            {
                if (Uid != null)
                {
                    pagetitle = "修改游记";
                    LoadInfo();
                }
                else
                {
                    upid = Convert.ToString(Session["Online_UserId"]);
                    ucode = CombineKeys.NewComb().ToString();
                    pagetitle = "发表新的游记";
                }
                if (flag == "audit")
                {
                    pagetitle = "审核游记";
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_Journal where uid='{0}'", Uid);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (flag != "audit")
                {
                    if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@7") == -1)
                    {
                        if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["userid"].ToString())
                        {
                            Response.Write("没有操作权限！");
                            Response.End();
                        }
                    }
                }

                DestinationInfos = DS.Tables[0].Rows[0]["DestinationList"].ToString();
                upid = DS.Tables[0].Rows[0]["userid"].ToString();
                ucode = DS.Tables[0].Rows[0]["uid"].ToString();
                Uid = DS.Tables[0].Rows[0]["uid"].ToString();
                title = DS.Tables[0].Rows[0]["title"].ToString();
                tags = DS.Tables[0].Rows[0]["tags"].ToString();
                contents = DS.Tables[0].Rows[0]["contents"].ToString();
                coverpic = DS.Tables[0].Rows[0]["coverpic"].ToString();
                coverpicurl = DS.Tables[0].Rows[0]["coverpicurl"].ToString();
                coverid = DS.Tables[0].Rows[0]["coverid"].ToString();
                seo = DS.Tables[0].Rows[0]["seo"].ToString();
                if (MyConvert.ConToInt(coverid) > 0)
                {
                    hide = "";
                    coverpichtml = coverpic;
                }
                
                ImportFlag = DS.Tables[0].Rows[0]["ImportFlag"].ToString();
                
                //contents = DS.Tables[0].Rows[0]["contents"].ToString();
                //contents = DS.Tables[0].Rows[0]["contents"].ToString();
                //BeginDate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]) + " 至 " + string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                //int nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellnums"].ToString());
                //int ff = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ff"].ToString());
                //if (ff < nums)
                //{
                //    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\" onclick=\"GoToOrder()\">立即购买</A>";
                //}
                //else
                //{
                //    BuyButton = "<A id=\"OrderBtn\" class=\"btn-link btn-personal\" href=\"javascript:void(0);\">已售完</A>";
                //}
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}