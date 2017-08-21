<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineList.aspx.cs" Inherits="TravelOnline.Travel.LineList" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleName %> - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
</head>
<body id="<%=BodyId %>">
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
<div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<%=Map1 %>&nbsp;&gt;&nbsp;<%=Map2 %></div><!--crumb end-->
<DIV id=i-right class=m> <%=LineRecommendSort %> </DIV> <!--i-right end-->
<a name="herea"></a>
<DIV id=select class="m select">
<DIV class=mt><H1></H1><STRONG>旅游产品筛选</STRONG><DIV class=extra><A href="#" onclick="myrefresh()">重置筛选条件</A></DIV></DIV>
<%=LineAreaSort %>
<%=LineDetailSort %>
</DIV><!--select end -->
</DIV><!--right-extra end-->

<DIV class=extra></DIV>
<DIV id="inputs" style="DISPLAY:none">
<input id="TB_Class" type="hidden" value="<%=Category %>"/><input id="TB_LineType" type="hidden" value="<%=ProducType %>"/><input id="TB_area" type="hidden" value="<%=AreaId %>"/>
<input id="TB_price" type="hidden" value="0"/><input id="TB_day" type="hidden" value="0"/><input id="TB_Visa" type="hidden" value="<%=VisaId %>"/>
<input id="TB_topic" type="hidden" value="0"/><input id="TB_Sort" type="hidden" value="0"/>
<input id="TB_CurrPage" type="hidden" value="1"/><input id="TB_RowCount" type="hidden" value="0"/>
<input id="TB_Pdate" type="hidden" value=""/>
</DIV>
<DIV id="loading1" style="DISPLAY:none;Z-INDEX: 10;BORDER-BOTTOM: #f3e7c7 10px solid; BORDER-LEFT: #f3e7c7 10px solid; BORDER-TOP: #f3e7c7 10px solid; BORDER-RIGHT: #f3e7c7 10px solid;"><IMG src="/Images/loading2.gif"></DIV>
<div class="right-extra">
<DIV id=filter></DIV>
<DIV id=plist class=m><DIV id=Linelist class="mc"></DIV></DIV>
<div id=bottompages class="m clearfix"></div>
</DIV><!--right-xtra end-->

</div>
<SPAN class=clr></SPAN>
<SCRIPT type="text/javascript">
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
        var url = "../Travel/AjaxLineList.aspx?action=LoadLineList&KeyWord=&Topic=" + $("#TB_topic").val() + "&LineClass=" + $("#TB_Class").val() + "&Area=" + $("#TB_area").val() + "&Price=" + $("#TB_price").val() + "&Days=" + $("#TB_day").val() + "&Sort=" + $("#TB_Sort").val() + "&Pages=" + $("#TB_CurrPage").val() + "&RowCount=" + $("#TB_RowCount").val() + "&LineType=" + $("#TB_LineType").val() + "&Pdate=" + $("#TB_Pdate").val() + "&VisaId=" + $("#TB_Visa").val();
        //window.open(url);
        //$("#loading").center();
        $("#Linelist").html("<div class=iloading>正在加载中，请稍候...</div>");
        if (flag==1)  $("#loading").show();
        $.getJSON(url, function (date) {
            //$("#loading").hide();
            //$("#loading").fadeOut();
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

