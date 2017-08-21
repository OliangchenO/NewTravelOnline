using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.PayMent
{
    public partial class PayResult : System.Web.UI.Page
    {
        public string imgurl, infos, Result, hide1, hide2, hide3;
        protected void Page_Load(object sender, EventArgs e)
        {
            imgurl = "<IMG src=\"/Images/ico_hook.gif\">";
            infos = "";
            hide1 = "hide";
            hide2 = "hide";
            hide3 = "hide";
            Result = Request.QueryString["Result"];

            switch (Result)
            {
                case "Success":
                    hide1 = "";
                    infos = "";
                    break;
                case "DbError":
                    imgurl = "<IMG src=\"/Images/Icon/del.png\" width=\"48px\">";
                    infos = "支付失败，支付数据更新错误";
                    hide2 = "";
                    break;
                case "VerifyError":
                    imgurl = "<IMG src=\"/Images/Icon/del.png\" width=\"48px\">";
                    infos = "支付失败，返回数据验证错误";
                    hide2 = "";
                    break;
                case "ParaError":
                    imgurl = "<IMG src=\"/Images/Icon/del.png\" width=\"48px\">";
                    infos = "支付失败，无返回参数";
                    hide2 = "";
                    break;
                case "StatusError":
                    imgurl = "<IMG src=\"/Images/Icon/del.png\" width=\"48px\">";
                    infos = "支付失败，返回数据状态错误";
                    hide2 = "";
                    break;
                default:
                    hide1 = "";
                    break;
            }
            //VerifyError ParaError
        }
    }
}