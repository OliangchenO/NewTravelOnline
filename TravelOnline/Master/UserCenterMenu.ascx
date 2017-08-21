<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserCenterMenu.ascx.cs" Inherits="TravelOnline.Master.UserCenterMenu" %>
<DIV id=mymenu class=m>
<DIV class=mt>
<H2><A href="#">会员管理中心</A></H2>
<DIV id=jdmenus-setting class=extra></DIV></DIV>
<DIV class=mc>
<DL>
  <DT>我的订单相关<B></B></DT>
  <DD>
  <DIV id=DIV1 class=item><A href="/users/userhome.aspx">会员中心首页</A></DIV>
  <DIV id=_MYJD_ordercenter class=item><A href="/users/userorder.aspx">我的订单</A></DIV>
  <DIV id=DIV2 class=item><A href="/users/receiveorder.aspx">暂存订单</A></DIV>
  <DIV id=DIV3 class=item><A href="/users/mycoupon.aspx">我的优惠券</A></DIV>
  <DIV id=DIV6 class=item><A href="/users/userintegral.aspx">我的积分</A></DIV>
  <DIV id=DIV5 class=item><A href="/users/favorite.aspx">我的收藏</A></DIV>
</DD></DL>
  <%=MenuInfo %>
<DL>
  <DT>我的账户相关<B></B></DT>
  <DD>
  <DIV id=_MYJD_personal class=item><A href="/users/userinfo.aspx">帐户详细信息</A></DIV>
  <DIV id=DIV4 class=item><A href="/users/journals.aspx">我的游记</A></DIV>
<%--  <DIV id=_MYJD_balance class=item><A href="###">我的帐户余额</A></DIV>
  <DIV id=_MYJD_score class=item><A href="###">我的积分</A></DIV>
  <DIV id=_MYJD_ticket class=item><A href="###">优惠券</A></DIV>--%>
  <DIV id=_MYJD_password class=item><A href="/users/changepassword.aspx">修改密码</A></DIV></DD></DL>
</DIV></DIV>