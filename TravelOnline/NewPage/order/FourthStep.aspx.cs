using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.NewPage.order
{
    public partial class FourthStep : System.Web.UI.Page
    {
        public string OrderId, username, ErrorCode, ErrorInfo, hide;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Request.QueryString["orderid"];
            if (OrderId.Length<30) hide = "hide";

            ErrorCode = Request.QueryString["error"];
            username = Convert.ToString(Session["Online_UserName"]);
            if (ErrorCode == null) ErrorCode = "100";
            ErrorInfo = "网络系统错误";
            if (ErrorCode == "101") ErrorInfo = "数据更新错误";
            if (ErrorCode == "102") ErrorInfo = "支付状态错误";
            if (ErrorCode == "103") ErrorInfo = "支付验证失败";
            if (ErrorCode == "104") ErrorInfo = "支付无返回参数";

        }
    }
}