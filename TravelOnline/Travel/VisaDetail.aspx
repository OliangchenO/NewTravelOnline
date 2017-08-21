<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisaDetail.aspx.cs" Inherits="TravelOnline.Travel.VisaDetail" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.VisaKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/shoppingcart.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
</head>
<body id="category">
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
        <div class="m rank"><div class="mt"><h2>热卖旅游线路排行</h2></div><div class="mc"><ul class="tabcon"><%=LineOnHotSale %></ul></div></div><!--rank end-->
        <div id=recent class="m rank"><div class="mt"><h2>最近浏览的线路</h2></div><div class="mc"><ul class="tabcon"><LI><DIV class=iloading>正在加载中，请稍候...</DIV></LI></ul></div></div><!--rank end-->
        <div class="m da211x90" id="IL3"><script type="text/javascript" src="/Js/AD/IL3.js"></script></div>
        <div class="m da211x90" id="IL4"><script type="text/javascript" src="/Js/AD/IL4.js"></script></div>
        <div class="m da211x90" id="IL5"><script type="text/javascript" src="/Js/AD/IL5.js"></script></div>
    </div><!--left end-->  
      
    <div class="right-extra">
        <div class="crumb">
        <a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<a href = "/Visa.html">单办签证</a>&nbsp;&gt;&nbsp;<%=Map1 %>&nbsp;&gt;&nbsp;<%=LineName %>
    </div>
    <!--crumb end-->

<DIV id=tops class="m select"><DIV class=mt></DIV></DIV>
 <DIV id=preview>
<DIV id=spec-n1 class=piczoom ><%=FirstImg %></DIV>
<DIV id=spec-n5><DIV id=spec-left class=control></DIV><DIV id=spec-right class=control></DIV><DIV id=spec-list><UL class=list-h><%=ImgList %></UL></DIV></DIV>
</DIV>
<UL id=summary>
<DIV id=name>
<H1><%=LineName %><FONT style="COLOR: #ff0000; font-size: 13px;" id=advertiseWord> <%=LineFeature%></FONT></H1></DIV><!--pname end-->
  <LI><SPAN>线路编号：<%=id %></SPAN></LI>
  <LI><DIV class=fl>预&nbsp;订&nbsp;价：<STRONG class=price>￥<%=LinePrice %></STRONG></DIV><DIV class=clr></DIV></LI>
  <LI class=clearfix><SPAN class=fl>商品评分：</SPAN> <DIV id=star418674 class=fl><DIV class="star sa5"></DIV></DIV></LI>
  <LI id=cx class=hide></LI>
  <LI id=tz class=hide></LI>
  <LI class=partake><SPAN>推荐分享：</SPAN> 
  <DIV>
  <A id=site-sina title=推荐到新浪微博 onclick="tj('http://v.t.sina.com.cn/share/share.php?appkey=2445336821','sina')" href="javascript:void(0)"></A>
  <A id=site-qzone title=推荐到腾讯微博 onclick="tj('http://v.t.qq.com/share/share.php?source=1000002&amp;site=http://www.360buy.com','qzone')" href="javascript:void(0)"></A>
  <A id=site-renren title=推荐到人人 onclick="tj('http://share.renren.com/share/buttonshare/post/1004?','renren')" href="javascript:void(0)"></A>
  <A id=site-kaixing title=推荐到开心网 onclick="tj('http://www.kaixin001.com/repaste/share.php?','kaixing')" href="javascript:void(0)"></A>
  <A id=site-douban title=推荐到豆瓣 onclick="tj('http://www.douban.com/recommend/?','douban')" href="javascript:void(0)"></A>
  <A id=site-msn title=推荐到MSN onclick="tj('http://profile.live.com/badge/?','MSN')" href="javascript:void(0)"></A>
  <A id=site-qq title=通过QQ发送链接给好友 href="javascript:void(0)"></A>
  <A id=site-email title=邮件 href="#"></A>
  </DIV></LI>
</UL><!--infos end-->

<div id=choose class=m>
<DL class=amount>
  <DT>　预订人数：</DT>
  <DD><div style="width: 60px"><A class=reduce onclick="setAmount.reduce('#pamount')" href="javascript:void(0)">-</A>
  <INPUT id=pamount onkeyup="setAmount.modify('#pamount')" value=1 type=text>
  <A class=add onclick="setAmount.add('#pamount')" href="javascript:void(0)">+</A></div></DD>
</DL>
<div class=btns>
<A id=InitCartUrl class=btn-0yuan onclick="BuyUrl(<%=id %>)" href="javascript:void(0)">在线购买</A>
<div id="fqPanel" class="fl"></div>
<INPUT id="btn_fav" class=btn-coll onclick="mark(<%=id %>)" value="收 藏" type=button>
<INPUT id="btn_print" class=btn-coll onclick="RoutePrint(<%=id %>)" value="打 印" type=button>
<span class=clr></span></div></div>
<DIV id="inputs" style="DISPLAY:none">
<input id="TB_LineId" type="hidden" value="<%=id %>"/>
</DIV>
</div><!--right-extra end-->

<div class="right-extra">
<div id=manager class="m select"><div class=mt><H1></H1><STRONG>特别说明</STRONG></div><div class="tj" id="specialmemo"><%=pdates %><%=RouteFeature %></div></div>
<span class=clr></span>
</div><!--right-extra end-->

<div class="right-extra">
<div id="detail" class="m detail">
<UL class=tab>
<LI class=curr data="d-all">所需材料<span></span></LI>
<%=VisaLi %>
</UL><!--知识库标签-->
<div id="d-all" class="mc fore tabcon">
<%=VisaCt %>
<div id="Div1"><%=RegularOrderProcess%><%=RegularContractInfos %><%=RegularPayInfos %></div><!--预订须知等 end-->
</div><!--tabcon end-->
<%=VisaDiv %>
</div><!--detail end-->
</div>
<div class="right-extra"></div><!--right-extra end-->
</div><!--w main end-->
<span class=clr></span>
<SCRIPT type="text/javascript">
    function SerchIt() {
        if ($("#Text1").val() == "" && $("#Text2").val() == "") {
            alert("请输入目的地、线路名称（或编号）、出发日期");
            $("#SerchNow").attr("href", "javascript:void(0);");
            return false;
        }
        $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
    }

    function GoToOrder() {
        var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                OrderNow();
            }
            else {
                LoginNow();
            }
        })
    }

    function OrderNow() {
        if ($(".thickbox").length != 0) {
            jdThickBoxclose()
        }
        var planid = "0";
        var begindate = "";
        var url = "/Purchase/OrderNow.aspx?lineid=" + $("#TB_LineId").val() + "&planid=" + planid + "&begindate=" + begindate + "&nums=" + $("#pamount").val() + "&r=" + Math.random();
        $.jdThickBox({
            type: "iframe",
            title: "在线预订",
            source: url,
            width: 580,
            height: 400,
            _title: "thicktitler",
            _close: "thickcloser",
            _con: "thickconr"
        })
    }
   
</SCRIPT>
<script type="text/javascript" src="/Scripts/base.lib.js"></script>
<script type="text/javascript" src="/Scripts/base.product.js"></script>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


