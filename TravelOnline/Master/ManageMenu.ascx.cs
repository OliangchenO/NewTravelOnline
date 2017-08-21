using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TravelOnline.Master
{
    public partial class ManageMenu : System.Web.UI.UserControl
    {
        public string MenuString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateMenu();
        }
        protected void CreateMenu()
        {
            StringBuilder Strings = new StringBuilder();
            if (Convert.ToString(Session["Manager_UserRight"]).Length > 0)
            {
                string Rights = Convert.ToString(Session["Manager_UserRight"]);
                if (Rights.IndexOf("@1@") > 0) Strings.Append("<DL><DT>登录用户管理<B></B></DT><DD>");
                if (Rights.IndexOf("@1@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageUserRight.aspx\">用户权限设置</A></DIV>");
                if (Rights.IndexOf("@1@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageDept.aspx\">部门设置</A></DIV>");
                if (Rights.IndexOf("@1@3") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageUser.aspx\">登录用户设置</A></DIV>");
                if (Rights.IndexOf("@1@4") > 0) Strings.Append("<DIV class=item><A href=\"/Company/CompanyInfo.aspx\">同行旅行社</A></DIV>");
                if (Rights.IndexOf("@1@5") > 0) Strings.Append("<DIV class=item><A href=\"/Company/UserInfo.aspx\">同行用户</A></DIV>");
                //if (Rights.IndexOf("@1@4") > 0) Strings.Append("<DIV class=item><A href=\"EditPassWord.aspx\">修改登录密码</A></DIV>");
                if (Rights.IndexOf("@1@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@2@") > 0) Strings.Append("<DL><DT>基础信息设置<B></B></DT><DD>");
                if (Rights.IndexOf("@2@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ProductClass.aspx\">旅游产品分类导航</A></DIV>");
                if (Rights.IndexOf("@2@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ProductType.aspx\">旅游产品大类设置</A></DIV>");
                if (Rights.IndexOf("@2@3") > 0) Strings.Append("<DIV class=item><A href=\"/Management/InitData.aspx\">基础资料信息</A></DIV>");
                if (Rights.IndexOf("@2@4") > 0) Strings.Append("<DIV class=item><A href=\"/Management/Destination.aspx\">目的地</A></DIV>");
                if (Rights.IndexOf("@2@5") > 0) Strings.Append("<DIV class=item><A href=\"/Management/SeoLink.aspx\">SEO内链关键字</A></DIV>");
                if (Rights.IndexOf("@2@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@3@") > 0) Strings.Append("<DL><DT>网站广告管理<B></B></DT><DD>");
                if (Rights.IndexOf("@3@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/FlashAD.aspx\">Flash轮换广告</A></DIV>");
                //if (Rights.IndexOf("@3@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/BannerAD.aspx\">Banner随机广告</A></DIV>");
                if (Rights.IndexOf("@3@3") > 0) Strings.Append("<DIV class=item><A href=\"/Management/TopicTravel.aspx\">首页主题旅游</A></DIV>");
                if (Rights.IndexOf("@3@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@4@") > 0) Strings.Append("<DL><DT>公共信息管理<B></B></DT><DD>");
                if (Rights.IndexOf("@4@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/Affiche.aspx\">公告信息</A></DIV>");
                if (Rights.IndexOf("@4@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/OurService.aspx\">服务信息</A></DIV>");
                //if (Rights.IndexOf("@4@3") > 0) Strings.Append("<DIV class=item><A href=\"AboutUs.aspx\">关于我们</A></DIV>");
                if (Rights.IndexOf("@4@4") > 0) Strings.Append("<DIV class=item><A href=\"/Management/FriendLink.aspx\">友情链接</A></DIV>");
                if (Rights.IndexOf("@4@5") > 0) Strings.Append("<DIV class=item><A href=\"/Management/HelpInfo.aspx\">帮助信息</A></DIV>");
                if (Rights.IndexOf("@4@6") > 0) Strings.Append("<DIV class=item><A href=\"/Management/OutBoundInfo.aspx\">出境须知</A></DIV>");
                if (Rights.IndexOf("@4@7") > 0) Strings.Append("<DIV class=item><A href=\"/Management/journals.aspx\">游记攻略</A></DIV>");
                if (Rights.IndexOf("@4@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@5@") > 0) Strings.Append("<DL><DT>旅游产品管理<B></B></DT><DD>");
                if (Rights.IndexOf("@5@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageLine.aspx?LineType=OutBound\">出境旅游</A></DIV>");
                if (Rights.IndexOf("@5@5") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageLine.aspx?LineType=InLand\">国内旅游</A></DIV>");
                if (Rights.IndexOf("@5@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageLine.aspx?LineType=FreeTour\">自由行</A></DIV>");
                if (Rights.IndexOf("@5@3") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageLine.aspx?LineType=Cruises\">邮轮旅游</A></DIV>");
                if (Rights.IndexOf("@5@4") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageLine.aspx?LineType=Visa\">单项服务</A></DIV>");
                if (Rights.IndexOf("@5@5") > 0) Strings.Append("<DIV class=item><A href=\"/Management/PreferPolicy.aspx\">优惠券</A></DIV>");
                if (Rights.IndexOf("@5@6") > 0) Strings.Append("<DIV class=item><A href=\"/Management/Recommend.aspx\">线路推荐</A></DIV>");
                if (Rights.IndexOf("@5@7") > 0) Strings.Append("<DIV class=item><A href=\"/Management/SpecialTopic.aspx\">线路专题</A></DIV>");
                if (Rights.IndexOf("@5@8") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManagePrefer.aspx\">线路优惠</A></DIV>");
                if (Rights.IndexOf("@5@13") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageGroup.aspx\">线路拼团</A></DIV>");
                if (Rights.IndexOf("@5@11") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageTradingArea.aspx\">商圈管理</A></DIV>");
                if (Rights.IndexOf("@5@12") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageComment.aspx\">点评管理</A></DIV>");
                Strings.Append("<DIV class=item><A href=\"/Management/FavoriteLine.aspx\">线路收藏</A></DIV>");
                if (Rights.IndexOf("@5@") > 0) Strings.Append("</DD></DL>");


                if (Rights.IndexOf("@7@") > 0) Strings.Append("<DL><DT>邮轮包船<B></B></DT><DD>");
                if (Rights.IndexOf("@7@1") > 0) Strings.Append("<DIV class=item><A href=\"/Cruises/CruisesCompany.aspx\">邮轮信息</A></DIV>");
                if (Rights.IndexOf("@7@2") > 0) Strings.Append("<DIV class=item><A href=\"/Cruises/CruisesLine.aspx\">包船航线</A></DIV>");
                if (Rights.IndexOf("@7@3") > 0) Strings.Append("<DIV class=item><A href=\"/CruisesOrder/ManageCruisesOrder.aspx\">包船订单</A></DIV>");
                if (Rights.IndexOf("@7@4") > 0) Strings.Append("<DIV class=item><A href=\"/CruisesOrder/CruisesApply.aspx\">变更申请</A></DIV>");
                if (Rights.IndexOf("@7@5") > 0) Strings.Append("<DIV class=item><A href=\"/CruisesOrder/Synchro.aspx\">同步记录</A></DIV>");
                if (Rights.IndexOf("@7@6") > 0) Strings.Append("<DIV class=item><A href=\"/Activity/ActivityManage.aspx\">船上活动</A></DIV>");
                if (Rights.IndexOf("@7@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@6@") > 0) Strings.Append("<DL><DT>会员管理<B></B></DT><DD>");
                if (Rights.IndexOf("@6@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/LoginUsers.aspx\">会员信息管理</A></DIV>");
                if (Rights.IndexOf("@6@2") > 0) Strings.Append("<DIV class=item><A href=\"/Management/OrderList.aspx\">订单管理</A></DIV>");
                if (Rights.IndexOf("@6@5") > 0) Strings.Append("<DIV class=item><A href=\"/Management/TempOrderList.aspx\">临时订单</A></DIV>");
                if (Rights.IndexOf("@6@6") > 0) Strings.Append("<DIV class=item><A href=\"/Management/XiRongList.aspx\">禧荣订单</A></DIV>");
                if (Rights.IndexOf("@6@7") > 0) Strings.Append("<DIV class=item><A href=\"/Management/Integral.aspx\">会员积分管理</A></DIV>");
                if (Rights.IndexOf("@6@3") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageHome.aspx\">会员评价</A></DIV>");
                if (Rights.IndexOf("@6@4") > 0) Strings.Append("<DIV class=item><A href=\"/Management/ManageHome.aspx\">有问必答</A></DIV>");
                if (Rights.IndexOf("@6@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@8@") > 0) Strings.Append("<DL><DT>报表中心<B></B></DT><DD>");
                if (Rights.IndexOf("@8@1") > 0) Strings.Append("<DIV class=item><A href=\"MemberReport.aspx\">会员报表</A></DIV>");
                if (Rights.IndexOf("@8@2") > 0) Strings.Append("<DIV class=item><A href=\"SalesReport.aspx\">销售报表</A></DIV>");
                if (Rights.IndexOf("@8@3") > 0) Strings.Append("<DIV class=item><A href=\"ProductReport.aspx\">产品报表</A></DIV>");
                if (Rights.IndexOf("@8@4") > 0) Strings.Append("<DIV class=item><A href=\"FinancialReport.aspx\">财务报表</A></DIV>");
                if (Rights.IndexOf("@8@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@9@") > 0) Strings.Append("<DL><DT>驰誉线路管理<B></B></DT><DD>");
                if (Rights.IndexOf("@9@1") > 0) Strings.Append("<DIV class=item><A href=\"LineAudit.aspx\">线路审核</A></DIV>");
                if (Rights.IndexOf("@9@") > 0) Strings.Append("</DD></DL>");

                if (Rights.IndexOf("@10@") > 0) Strings.Append("<DL><DT>分销管理<B></B></DT><DD>");
                if (Rights.IndexOf("@10@1") > 0) Strings.Append("<DIV class=item><A href=\"/Management/FxOrderList.aspx\">分销订单</A></DIV>");
                if (Rights.IndexOf("@10@") > 0) Strings.Append("</DD></DL>");

                MenuString = Strings.ToString();
            }
        }

        //public static bool IsChecked(string Codes, string RightCode)
        //{
        //    if (RightCode.IndexOf(Codes) > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}