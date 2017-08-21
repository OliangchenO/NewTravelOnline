<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowNews.aspx.cs" Inherits="TravelOnline.Common.ShowNews" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
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
<body id="index">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortList1" runat="server" />
<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
<div class="w main">
<div class=left>
    <div id=right-report class="m right-report r-report"></div><!--left No.1-->
    <div class="m rank"><div class="mt"><h2>一周热卖排行榜</h2></div><div class="mc"><ul class="tabcon"><%=LineOnHotSale %></ul></div></div>
    <div class="m da211x90" id="IL1"><script type="text/javascript" src="/Js/AD/IL1.js"></script></div>
    <div class="m da211x90" id="IL2"><script type="text/javascript" src="/Js/AD/IL2.js"></script></div>      
</div><!--left end-->
<DIV class=extra></DIV>
<div class="right-extra">
    <div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<a href = "/News.html">新闻中心</a>&nbsp;&gt;&nbsp;<%=NewsTitle %></div><!--crumb end-->
    <DIV id=detail class=m>
    <DIV class=mt>
    <H1><%=NewsTitle %></H1>
    <DIV class=summary>更新时间：<%=NewsTime %>&nbsp;&nbsp;&nbsp;&nbsp;</DIV></DIV>
    <DIV class=mc><%=NewsContent %></DIV>
    </DIV>
</DIV><!--right-xtra end-->
</div>
<SPAN class=clr></SPAN>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


