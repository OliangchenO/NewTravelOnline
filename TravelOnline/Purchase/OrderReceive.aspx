<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReceive.aspx.cs" Inherits="TravelOnline.Purchase.OrderReceive" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 在线预订</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
</head>
<body id="none">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="TempOrderId" type="hidden" value="<%=OrderId %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>订单暂存</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step5" style="display:block;">
            <li class="view">选择线路 </li>
	        <li class="selects">选择价格</li>
	        <li class="book">填写信息</li>
	        <li class="check">核对订单</li>
	        <li class="submit">成功提交</li>
        </ul>
    </div>
    <DIV class=clr></DIV>
<DIV class=forget class=w>
<DIV class=mt>
<H2>您的预订单已成功暂存！</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
    <DIV class=success>
        <DIV class=fa><IMG src="/Images/ico_hook.gif"></DIV>
        <DIV class=fl>
            <div style="LINE-HEIGHT: 20px; color: #009900; font-size: 16px; font-weight: bold;"><%=LineName %> <SPAN class=headstep1> 订单暂存成功!</SPAN></div><BR>
            <div class=infos>您的订单号：<A href="javascript:void(0);" style="color: #159ce9"><%=AutoId %></A> （订单状态可在页面右上的“我的订单”内容查看）</div>
<div class=infos><span style="color: #FF0000">由于系统通讯原因，您的订单未能成功提交，系统将您的订单自动暂存，您随时可以对此订单进行修改，待系统通讯恢复正常或我们的工作人员和您联系后，您可以再次提交此订单，对此给您造成的不便，敬请谅解。</span><br>
</div>
        </DIV>
    </DIV>
<ul class=sul>
<li>电话：4006-777-666</li>
<li>传真：021-64742928(出境)</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;021-64670982(国内)</li>
<li>上海中国青年旅行社有限公司</li>
<li>联系地址：上海市徐汇区衡山路2号（200031）</li>  
<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>    
</ul>

</DIV>
    <div class="gotonext">
         <A class="btn-link btn-personal" href="/index.html">返回首页</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<A class="btn-link btn-personal" href="/Users/UserHome.aspx">订单中心</A>
    </div>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">


    </script>
</body>
</html>





