<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TravelOnline.Common.Search" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅游线路搜索</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="/Scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript">
        (function () { var _sn = ["Styles/slide_OutBound"]; var _su = "/"; var _sw = screen.width; var _se, _st; for (i in _sn) { _se = document.createElement("link"); _se.type = "text/css"; _se.rel = "stylesheet"; if (_sw >= 1280) { _st = _su + _sn[i] + ".w.css" } else { _st = _su + _sn[i] + ".css" } _se.href = _st; document.getElementsByTagName("head")[0].appendChild(_se) } })()
    </script>
</head>
<body id="index">
    <form id="form1" runat="server">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <div class="w main">
    <div class=left>        
        <div id=right-report class="m right-report r-report">
            <div class=mt><H2>旅游线路搜索</H2><div class=extra></div></div>
            <div class=mc>
                <UL>
                    <LI>目的地、线路名称或编号</LI>
                    <LI><input id="Text1" type="text" style="width: 190px" /></LI>
                    <LI>出发日期</LI>
                    <LI style="PADDING-BOTTOM: 2px;"><input class="runcode" id="Text2" type="text" style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;<a id="SerchNow" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="SerchIt()">搜 索</a></LI>
                </UL>
            </div>
        </div><!--left No.1-->
        <div class="m da211x90" id="IL1"><script type="text/javascript" src="/Js/AD/IL1.js"></script></div>
        <div class="m da211x90" id="IL2"><script type="text/javascript" src="/Js/AD/IL2.js"></script></div>      
        <div class="m rank"><div class="mt"><h2>一周热卖排行榜</h2></div><div class="mc"><ul class="tabcon"><%=LineOnHotSale %></ul></div></div>
        <div class="m rank"><div class="mt"><h2>最新推出线路</h2></div><div class="mc"><ul class="tabcon"><%=LineSendAll%></ul></div></div>
    </div><!--left end-->
<div class="right-extra">
<div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;旅游线路搜索</div><!--crumb end-->
<a name="herea"></a>
<DIV id=select class="m select">
<DIV class=mt><H1></H1><STRONG>旅游产品筛选</STRONG><DIV class=extra><A href="#" onclick="myrefresh()">重置筛选条件</A></DIV></DIV>
<%=LineDetailSort %>
</DIV><!--select end -->
</DIV><!--right-extra end-->

<DIV class=extra></DIV>
<DIV id="inputs" style="DISPLAY:none">
<input id="TB_KeyWord" type="hidden" value="<%=keyword %>"/><input id="TB_LineType" type="hidden" value=""/>
<input id="TB_area" type="hidden" value=""/><input id="TB_price" type="hidden" value="0"/>
<input id="TB_day" type="hidden" value="0"/><input id="TB_Pdate" type="hidden" value="<%=pdate %>"/>
<input id="TB_topic" type="hidden" value="<%=theme %>"/><input id="TB_Sort" type="hidden" value="0"/>
<input id="TB_CurrPage" type="hidden" value="1"/><input id="TB_RowCount" type="hidden" value="0"/>
<input id="ThirdType" type="hidden" value="<%=ThirdType %>"/>
</DIV>
<DIV id="loading" style="DISPLAY:none;Z-INDEX: 10;BORDER-BOTTOM: #f3e7c7 10px solid; BORDER-LEFT: #f3e7c7 10px solid; BORDER-TOP: #f3e7c7 10px solid; BORDER-RIGHT: #f3e7c7 10px solid;"><IMG src="/Images/loading2.gif"></DIV>
<div class="right-extra">
<DIV id=filter></DIV>
<DIV id=plist class=m><DIV id=Linelist class="mc"></DIV></DIV>
<div id=bottompages class="m clearfix"></div>
</DIV><!--right-xtra end-->

</div>
<SPAN class=clr></SPAN>
<SCRIPT type=text/javascript>
    initScrollY = 0;
    proIDs = new Array();

    $(document).ready(function () {
        LoadLineList(0);
        compare();
    })

    function myrefresh() {
        window.location.reload();
    }

    $(function () {
        $("#select DL A").click(function () {
            var parentid = $(this).parents("DL").attr("id");
            $("#" + parentid + " A").removeAttr("class");
            $(this).attr({ "class": "curr" });
            $("#TB" + parentid).val($(this).attr("tag"));
            $("#TB_RowCount").val("0");
            $("#TB_CurrPage").val("1");
            LoadLineList(1);
        });
    });

    (function ($) {
        jQuery.fn.center = function () {
            this.css('position', 'absolute');
            this.css('top', ($(window).height() - this.height()) / 2 + $(window).scrollTop() + 'px');
            this.css('left', ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + 'px');
            return this;
        }
    })(jQuery);

    function SortNow(sort) {
        $("#TB_Sort").val(sort);
        LoadLineList(1);
    }

    function GoToPage(page) {
        $("#TB_CurrPage").val(page);
        LoadLineList(1);
    }

    function LoadLineList(flag) {
        var url = "../Travel/AjaxLineList.aspx?action=LoadLineList&LineClass=0&VisaId=0&KeyWord=" + $("#TB_KeyWord").val() + "&Topic=" + $("#TB_topic").val() + "&Area=" + $("#TB_area").val() + "&Price=" + $("#TB_price").val() + "&Days=" + $("#TB_day").val() + "&Sort=" + $("#TB_Sort").val() + "&Pages=" + $("#TB_CurrPage").val() + "&RowCount=" + $("#TB_RowCount").val() + "&LineType=" + $("#TB_LineType").val() + "&Pdate=" + $("#TB_Pdate").val() + "&ThirdType=" + $("#ThirdType").val();
        //window.open(url);
        $("#loading").center();
        if (flag == 1) $("#loading").show();
        $.getJSON(url, function (date) {
            //$("#loading").hide();
            $("#loading").fadeOut();
            $("#TB_RowCount").val(date.rows);
            $("#filter").html(date.filter);
            $("#bottompages").html(date.bottompages);
            $("#Linelist").html(date.content);
        })
    }

    $(function () {
        $('#Text2').calendar({ minDate: '%y-%M-%d', btnBar: false });
    });

    function SerchIt() {
        if ($("#Text1").val() == "" && $("#Text2").val() == "") {
            alert("请输入目的地、线路名称（或编号）、出发日期");
            $("#SerchNow").attr("href", "javascript:void(0);");
            return false;
        }
        $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
    }
</SCRIPT>
<uc3:Footer ID="Footer1" runat="server" />
    </form>
</body>
</html>


