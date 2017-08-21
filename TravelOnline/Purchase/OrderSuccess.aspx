<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSuccess.aspx.cs" Inherits="TravelOnline.Purchase.OrderSuccess" %>
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
        <h2 class="headline"><%=LineName %><SPAN class=headstep>预订成功</SPAN>
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
<H2>恭喜您，您的预订单已成功提交！</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
    <DIV class=success>
        <DIV class=fa><IMG src="/Images/ico_hook.gif"></DIV>
        <DIV class=fl>
            <div style="LINE-HEIGHT: 20px; color: #009900; font-size: 16px; font-weight: bold;"><%=LineName %> <SPAN class=headstep1>预订成功!</SPAN></div><BR>
            <div class=infos>您的订单号：<A href="/OrderView/<%=OrderId %>.html" style="color: #159ce9"><%=AutoId %></A> （订单状态可在页面右上的“我的订单”内容查看）</div>
<div class="infos <%=hide5 %>">我们的工作人员将会尽快通过电话或者EMAIL和您联系。如果您所填写的电话、E-mail有误，此订单将被取消。<br>
<span style="color: #FF0000">即时占位订单，请您在预订后<%=AdjustTime %>小时以内付款；非占位订单，请您在订单确认以后<%=AdjustTime %>小时内付款（紧急情况或特殊情况除外）。</span><br>
如果因特殊原因无法成行，我们也会及时通知您。
</div>
            <%--<div class=jf>您本次您本次预订的积分是：<%=Price %> &nbsp;&nbsp;&nbsp;&nbsp;<A href="javascript:void(0);" style="color: #159ce9">积分规则</A><br>
如果本订单经确认后最终成交，这些积分将累积到您的个人积分上
</div>--%>
        </DIV>
    </DIV>
        <DIV class="success">
        <DIV class=fa><IMG src="/Images/ico_money.gif" width=48px></DIV>
        <DIV class=fl>
            <div style="LINE-HEIGHT: 25px; color: #0066CC; font-size: 24px; font-weight: bold;">支付方式</div><BR>
            <div class="infos <%=hide1 %>">您的订单已即时占位成功，请在<%=AdjustTime %>小时以内通过网上银行或支付宝付款，付款后您的占位信息将保留；<br>如果超过付款时间您还没有付款，订单将自动转为非占位状态，需要我们的工作人员给您进行确认是否有余位。</div>
            <div class="infos <%=hide2 %>">您的订单目前暂时不能在线支付，请您在订单确认以后的24小时之内付款。<br><br></div>
            <div class="infos <%=hide3 %>">您选择到门店付款。<br></div>
            <div class="infos <%=hide4 %>">请于24小时内支付500元预付款，过时订单自动取消；并于出发前20日付清全款，出发前14日未付视为自动放弃。<br></div>
            <%=PayUrl %>
            <%=BranchMap %>
            <%--<A class="btn-link btn-personal" href="/Pay/PayNow.aspx?OrderId=<%=OrderId %>" target="_blank">立刻支付</A>--%>
        </DIV>
<SPAN class=clr>
</SPAN></DIV>
<ul class="sul <%=hide7 %>">
<li>电话：4006-777-666 </li>
<li>传真：021-64742928(出境)</li>
<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;021-64670982(国内)</li>
<li>上海中国青年旅行社</li>
<li>联系地址：上海市徐汇区衡山路2号（200031）</li>  
<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>    
</ul>

<ul class="sul <%=hide6 %>">
<li>电话：021-64747516 </li>
<li>上海中国青年旅行社</li>
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
        (function ($) {
            var origContent = "";
            function loadContent(hash) {
                if (hash != "") {
                    if (origContent == "") {
                        origContent = $('#content').html();
                    }
                    $('#content').load(hash + ".html",
            function () { prettyPrint(); });
                } else if (origContent != "") {
                    $('#content').html(origContent);
                }
            }
            $(document).ready(function () {
                $.history.init(loadContent);
                $('#navigation a').click(function (e) {
                    var url = $(this).attr('href');
                    url = url.replace(/^.*#/, '');
                    $.history.load(url);
                    return false;
                });
            });
        })(jQuery); 
    </script>
    <script type="text/javascript">
        var lineid = "<%=LineId %>";
        //14788
        if (lineid == "14788") {
            var _mvq = _mvq || [];
            _mvq.push(['$setAccount', 'm-29176-0']);
            _mvq.push(['$setGeneral', 'ordercreate', '', '<%=Convert.ToString(Session["Online_UserName"]) %>', '<%=Convert.ToString(Session["Online_UserEmail"]) %>']);
            _mvq.push(['$logConversion']);
            _mvq.push(['$addOrder', <%=AutoId %>, <%=Price %>]);
            _mvq.push(['$addItem', /*订单号*/'<%=AutoId %>', /*商品id*/'', /*商品名称*/'<%=LineName %>', /*商品价格*/'<%=Price %>', /*商品数量*/'<%=Nums %>', /*商品页url*/'', /*商品页图片url*/'']);
            _mvq.push(['$logData']);

            (function () {
                var mvl = document.createElement('script');
                mvl.type = 'text/javascript'; mvl.async = true;
                mvl.src = ('https:' == document.location.protocol ? 'https://static-ssl.mediav.com/mvl.js' : 'http://static.mediav.com/mvl.js');
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(mvl, s);
            })();
        }
        
    </script>
</body>
</html>




