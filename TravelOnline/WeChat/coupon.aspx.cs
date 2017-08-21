using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Purchase;

namespace TravelOnline.WeChat
{
    public partial class coupon : System.Web.UI.Page
    {
        public Guid ucode;
        public string CouponId;
        protected void Page_Load(object sender, EventArgs e)
        {
            CouponId = Request.QueryString["uid"];
            ucode = System.Guid.NewGuid();
        }
    }
}