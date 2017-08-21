<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="TravelOnline.Common.NewsList" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新闻中心</title>
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
<DIV id="inputs" style="DISPLAY:none">
<input id="TB_CurrPage" type="hidden" value="1"/><input id="TB_RowCount" type="hidden" value="0"/>
</DIV>
<div class="right-extra ">
    <div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<a href = "/News.html">新闻及资讯中心</a></div><!--crumb end-->
    <DIV id=detail class="m">
        <DIV class=mta>
        <H2>新闻、旅游资讯及促销信息</H2>
        <div class=news><DIV id=NewsList class=newlist></DIV></div>
        <div id=bottompages class="m clearfix"></div>
        </DIV>
    </DIV><!--right-xtra end-->
</div>
<SPAN class=clr></SPAN>
<SCRIPT type="text/javascript">
    $(document).ready(function () {
        LoadList();
    })

    function GoToPage(page) {
        $("#TB_CurrPage").val(page);
        LoadList(1);
    }

    function LoadList() {
        var url = "../Travel/AjaxLineList.aspx?action=LoadNewsList&Pages=" + $("#TB_CurrPage").val() + "&RowCount=" + $("#TB_RowCount").val();
        $("#NewsList").html("<div class=iloading>正在加载中，请稍候...</div>");
        $.getJSON(url, function (date) {
            $("#TB_RowCount").val(date.rows);
            $("#bottompages").html(date.bottompages);
            $("#NewsList").html(date.content);
        })
    }

</SCRIPT>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>



