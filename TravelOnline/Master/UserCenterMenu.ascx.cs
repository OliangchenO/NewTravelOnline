using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Master
{
    public partial class UserCenterMenu : System.Web.UI.UserControl
    {
        public string MenuInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
            {
                MenuInfo = "<DL><DT>邮轮包船相关<B></B></DT><DD>";
                MenuInfo += "<DIV id=TH_M01 class=item><A href=\"/CruisesOrder/CruisesOrder.aspx\">邮轮包船订单</A></DIV>";
                MenuInfo += "<DIV id=TH_M01 class=item><A href=\"/Users/CruisesApply.aspx\">舱房变更申请</A></DIV>";
                MenuInfo += "</DD></DL>";
            }
        }
    }
}