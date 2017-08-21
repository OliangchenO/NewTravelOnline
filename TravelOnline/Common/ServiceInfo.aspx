<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceInfo.aspx.cs" Inherits="TravelOnline.Common.ServiceInfo" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=NewsTitle %></title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/news.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript">
        (function () { var _sn = ["Styles/slide_OutBound"]; var _su = "/"; var _sw = screen.width; var _se, _st; for (i in _sn) { _se = document.createElement("link"); _se.type = "text/css"; _se.rel = "stylesheet"; if (_sw >= 1280) { _st = _su + _sn[i] + ".w.css" } else { _st = _su + _sn[i] + ".css" } _se.href = _st; document.getElementsByTagName("head")[0].appendChild(_se) } })()
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortList1" runat="server" />
<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
<div class="w main">
<div class=left>
<div class="m right-report"><div class=mt><H2>服务信息</H2></div>
<div class=mc>
    <UL class=serviceinfo>
        <LI><A href="/Service/AboutUs">关于我们</A></LI>
        <LI><A href="/Service/ContactUs">联系我们</A></LI>
        <LI><A href="/Service/JoinUs">人才招聘</A></LI>
    </UL>
</div>
</div>
<%--<div class="m right-report"><div class=mt><H2>常见问题</H2></div>
<div class=mc>
    <UL>
        <li>· <a href="/News/21.html">注册、登录及常见问题</a></li>
        <li>· <a href="/News/21.html">订购流程及常见问题</a></li>

    </UL>
</div>
</div>--%>
<div class="m right-report"><div class=mt><H2>办公电话</H2></div>
<div class=mc>
    <UL class=serviceinfo>
        <LI>服务时间：8:00－18：00</LI>
        <LI>&nbsp;&nbsp;&nbsp;工作日：周一至周日</LI>
        <LI>呼叫中心：<span>4006 777 666</span></LI>
        <LI>办公电话：<span>021-64330000</span></LI>
        <LI>办公传真：<span>021-64330507</span></LI>
        <LI>客服邮箱：<a href="mailto:service@scyts.com">service@scyts.com</a></LI>
    </UL>
</div>
</div>

<div class="m right-report"><div class=mt><H2>顾客投诉</H2></div>
<div class=mc>
    <UL class=serviceinfo>
        <LI>投诉信箱：<a href="mailto:tousu@scyts.com">tousu@scyts.com</a></LI>
    </UL>
</div>
</div>
   
</div><!--left end-->
<DIV class=extra></DIV>
<div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<%=NewsTitle %></div><!--crumb end-->
<div class="right-extra">
    <DIV id=detail class=m>
    <DIV class=mta><H2><%=NewsTitle %></H2></DIV>
    <%=NewsContent %>
    </DIV>
</DIV><!--right-xtra end-->
</div>
<SPAN class=clr></SPAN>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>
