<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayResult.aspx.cs" Inherits="TravelOnline.PayMent.PayResult" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线支付</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
<DIV class="w main">
<DIV class=clr></DIV>
<DIV class=forget class=w>
<DIV class=mt>
<H2>在线支付结果</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
    <DIV class=success>
        <DIV class=fa><%=imgurl %></DIV>
        <DIV class="fl <%=hide1 %>">
            <div style="LINE-HEIGHT: 30px; color: #009900; font-size: 24px; font-weight: bold;">在线支付成功！</div><BR>
            <div class=infos>订单状态可在页面右上的“我的订单”中查看，谢谢您的预订！</div>
        </DIV>
        <DIV class="fl <%=hide2 %>">
            <div style="LINE-HEIGHT: 30px; color: #009900; font-size: 24px; font-weight: bold;"><%=infos %></div><BR>
            <div class=infos>订单状态可在页面右上的“我的订单”中查看，谢谢您的预订！</div>
        </DIV>
    </DIV>
</DIV>

<DIV class=clr></DIV></DIV>
</DIV>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>