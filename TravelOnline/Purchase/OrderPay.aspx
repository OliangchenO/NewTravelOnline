<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPay.aspx.cs" Inherits="TravelOnline.Purchase.OrderPay" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 在线支付</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Alipay.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>

    <script>
        function do_onload() {
            var d = new Date();
            //给出定单日期
            fm.date.value = d.getYear() * 10000 + (1 + d.getMonth()) * 100 + d.getDate();
            //给出不重复的定单号
            var s = d.getYear() * 366 + d.getMonth() * 31 + d.getDate();
            s = (s * 24 + d.getHours()) * 60 + d.getMinutes();
            s = s * 60 + d.getSeconds();
            fm.billno.value = s.toString().substring(1);
        }
    </script>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
<DIV class="w main">
<form id="form1" runat="server" onsubmit="javascript:return check_null(this);" >
<div id="order_title">
<h2 class="headline"><%=LineName %><SPAN class=headstep>在线支付</SPAN>
<p class="fontcolor02 <%=hide %>">出发日期：<%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
</h2>
</div>
<DIV class=clr></DIV>
<DIV class=forget class=w>
<DIV class=mt>
<H2>订单信息</H2><B></B></DIV>
<DIV style="PADDING-TOP: 10px" class=mc>
<ul class=payul>
<li class="<%=hide %>"><div class=tname>旅游线路：</div><div class=tinfo><A href="<%=LineUrl %>" style="color: #159ce9" target="_blank"><%=LineName%></A></div></li>
<li class="<%=hide %>"><div class=tname>出发日期：</div><div class=tinfo><%=BeginDate%></div></li>
<li class="<%=hide %>"><div class=tname>订单号：</div><div class=tinfo><A href="/OrderView/<%=OrderId %>.html" style="color: #159ce9" target="_blank"><%=AutoId %></A></div></li>
<li class="<%=hide %>"><div class=tname>预订人数：</div><div class=tinfo><%=Nums%>人 <%=NumsInfo%></div></li>
<li><div class=tname>订单金额：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount"><%=Price%></span></div></li>
<li class="<%=hidePfyh %>"><div class=tname>实付金额：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="span3"><%=pufaprice%></span> &nbsp;<%=pfyhinfo %></div></li>
<li class="<%=hide1 %>"><div class=tname>同行返佣：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="span1"><%=Rebate%></span> </div></li>
<%=FkInfo%>
<li><div class=tname>已付款：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="span2"><%=Pay%></span></div></li>
<li><div class=tname>&nbsp;</div><div class=tinfo>&nbsp;</div></li>
<li><div class=tname>本次付款：</div><div class=tinfo><span class="base_price02" style="FONT-SIZE: 20px;">&yen;</span><input id="P_PayNow" name="P_PayNow" type="text" class="easyui-numberbox" precision="0" max="999999" size="7" maxlength="9" style="WIDTH: 100px;text-align:center;FONT-WEIGHT: bold; FONT-SIZE: 20px; color:#e56700;BORDER-BOTTOM: #196297 1px solid;BORDER-LEFT: #ffffff 1px solid;BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid;" value="<%=YeE %>"/></div></li>
</ul>
</DIV>
</DIV>
<DIV class=forget class=w>
<DIV class=mt>
<H2>支付方式</H2><B></B></DIV>
<DIV style="PADDING-TOP: 10px" class=mc>
<ul class=payul>

<li class="<%=hidePfyh %>"><div class=tname></div><div class=banks><input type="radio" name="paytype" value="SPDB" id="Radio8" <%=PufaCheck %>/><label class=banklogo><IMG src="/Images/Alipay/bank_24.png"></label>&nbsp;</div></li>
<%--<li class="<%=hideIcbc %>"><div class=tname></div><div class=banks><input id="Radio7" type="radio" name="paytype" value="ICBCB2C" <%=IcbcCheck %> /><label for="Radio1" class=banklogo><IMG for="Radio1" src="/Images/Alipay/bank_03.png"></label>&nbsp;</div></li>--%>
<li class="<%=hideAliPay %>"><div class=tname></div><div class=banks><input id="Radio1" type="radio" name="paytype" value="AliPay" <%=AliPayCheck %> /><label for="Radio1" class=banklogo><IMG for="Radio1" src="/Images/AliPay.jpg"></label>&nbsp;</div></li>
<li class="<%=hidePingAn %>"><div class=tname></div><div class=banks><input type="radio" name="paytype" value="SPABANK" id="Radio7" <%=PingAnCheck %> /><label class=banklogo><IMG src="/Images/Alipay/bank_40.png"></label><input type="radio" name="paytype" value="SDB" id="Radio2" ><label class=banklogo><IMG src="/Images/Alipay/bank_26.png"></label>&nbsp;</div></li>
<%--<li class="" style="padding-top:70px"><div class=banks><input type="radio" name="paytype" value="HSBC" id="Radio8" /><label class=banklogo><IMG src="/Images/Alipay/bank_hsbc.png"></label>&nbsp;</div></li>--%>
<li><div class=tname></div><div class=banks>&nbsp;</div></li>
<li class=hide><div class=pname>信用卡：</div><div class=pinfo>&nbsp;</div></li>
<li class="<%=hideAliAll %>">
    <div class=tname></div>
    <div class=banks>
        <%--<input type="radio" name="paytype" value="CMB_Scyts" id="Radio2"/><label class=banklogo><IMG src="/Images/Alipay/bank_07.png"></label>--%>
        <%--<input type="radio" name="paytype" value="IcbcPay" id="Radio3"/><label class=banklogo><IMG src="/Images/Alipay/bank_03.png"></label>--%>
        <input type="radio" name="paytype" value="ICBCB2C" id="Radio3"/><label class=banklogo><IMG src="/Images/Alipay/bank_03.png"></label>
        <input type="radio" name="paytype" value="CMB" id="Radio9"/><label class=banklogo><IMG src="/Images/Alipay/bank_07.png"></label>
        <input type="radio" name="paytype" value="CCB" id="Radio21"/><label class=banklogo><IMG src="/Images/Alipay/bank_05.png"></label>
        <input type="radio" name="paytype" value="BOCB2C" id="Radio22"><label class=banklogo><IMG src="/Images/Alipay/bank_25.png"></label>
        <br><br>
        <input type="radio" name="paytype" value="ABC" id="Radio4"/><label class=banklogo><IMG src="/Images/Alipay/bank_15.png"></label>
        <input type="radio" name="paytype" value="COMM" id="Radio10"/><label class=banklogo><IMG src="/Images/Alipay/bank_09.png"></label>
        <input type="radio" name="paytype" value="PSBC-DEBIT" id="Radio20"/><label class=banklogo><IMG src="/Images/Alipay/bank_34.png"></label>
        <input type="radio" name="paytype" value="CEBBANK" id="Radio23"/><label class=banklogo><IMG src="/Images/Alipay/bank_18.png"></label>
        <br><br>
        <input type="radio" name="paytype" value="SPDB" id="Radio5"/><label class=banklogo><IMG src="/Images/Alipay/bank_24.png"></label>
        <input type="radio" name="paytype" value="GDB" id="Radio11"><label class=banklogo><IMG src="/Images/Alipay/bank_16.png"></label>
        <input type="radio" name="paytype" value="CITIC" id="Radio19"/><label class=banklogo><IMG src="/Images/Alipay/bank_23.png"></label>
        <input type="radio" name="paytype" value="CIB" id="Radio24"/><label class=banklogo><IMG src="/Images/Alipay/bank_17.png"></label>
        <br><br>
        <input type="radio" name="paytype" value="SDB" id="Radio6"><label class=banklogo><IMG src="/Images/Alipay/bank_26.png"></label>
        <input type="radio" name="paytype" value="CMBC" id="Radio12"/><label class=banklogo><IMG src="/Images/Alipay/bank_31.png"></label>
        <input type="radio" name="paytype" value="BJBANK" id="Radio18"/><label class=banklogo><IMG src="/Images/Alipay/bank_32.png"></label>
        <input type="radio" name="paytype" value="HZCBB2C" id="Radio25"/><label class=banklogo><IMG src="/Images/Alipay/bank_42.png"></label>

<%--        <input type="radio" name="paytype" value="SHBANK" id="Radio7"/><label class=banklogo><IMG src="/Images/Alipay/"></label>
        <input type="radio" name="paytype" value="BJRCB" id="Radio13"/><label class=banklogo><IMG src="/Images/Alipay/"></label>
        <input type="radio" name="paytype" value="SPABANK" id="Radio17"/><label class=banklogo><IMG src="/Images/Alipay/"></label>
        <input type="radio" name="paytype" value="FDB" id="Radio26"/><label class=banklogo><IMG src="/Images/Alipay/"></label>
        <input type="radio" name="paytype" value="WZCBB2C-DEBIT" id="Radio8"><label class=banklogo><IMG src="/Images/Alipay/"></label>
        <br>
        <input type="radio" name="paytype" value="NBBANK" id="Radio14"><label class=banklogo><IMG src="/Images/Alipay/"></label>--%>
    </div>
</li>
</ul>
</DIV>
<div class="gotonext">
<%--<A class="btn-link btn-personal" href="/index.html">返回首页</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;--%><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" class="btn-link btn-personal">现在支付</asp:LinkButton>

<DIV id="inputs" style="DISPLAY:none">
<input id="P_OrderId" name="P_OrderId" type="hidden" value="<%=OrderId %>"/>
<input id="P_LineName" name="P_LineName" type="hidden" value="<%=LineName %>"/>
<input id="P_Price" name="P_Price" type="hidden" value="<%=Price %>"/>
<input id="P_Date" name="P_Date" type="hidden" value="<%=BeginDate %>"/>
<input id="P_Nums" name="P_Nums" type="hidden" value="<%=Nums %>"/>
<input id="P_Pay" name="P_Pay" type="hidden" value="<%=Pay %>"/>
<input id="P_Yfk" name="P_Yfk" type="hidden" value="<%=yfk %>"/>
<input id="P_Yue" name="P_Yue" type="hidden" value="<%=YeE %>"/>
<input id="CMB_PayNo" name="CMB_PayNo" type="hidden" value=""/>
<input id="P_AutoId" name="P_AutoId" type="hidden" value="<%=AutoId %>"/>
</DIV>
<script type="text/javascript">
    function check_null(theForm) {
        var S_Yfk = 0;
        var S_Nums = 0;
        var S_Yue = 0;
        var S_Pay = 0;
        var S_Now = 0;

        S_Yfk = Number($("#P_Yfk").val());
        S_Yue = Number($("#P_Yue").val());
        S_Now = Number($("#P_PayNow").val());
        S_Pay = Number($("#P_Pay").val());

        if (S_Now ==0 ) {
            alert("请输入本次付款金额！");
            return false;
        }

        if (S_Now > S_Yue) {
            alert("本次付款金额大于应付余额！");
            return false;
        }

        if (S_Pay == 0) {
            if (S_Now < S_Yfk) {
                alert("本次付款金额少于最低预付款！");
                return false;
            }            
        }
    }

    $(document).ready(function () {
        var d = new Date();
        //给出不重复的定单号
        var s = d.getYear() * 366 + d.getMonth() * 31 + d.getDate();
        s = (s * 24 + d.getHours()) * 60 + d.getMinutes();
        s = s * 60 + d.getSeconds();
        $("#CMB_PayNo").val(s.toString().substring(1));
    });
</script>
</form>
<%--<form id="fm" action="https://netpay.cmbchina.com/netpayment/basehttp.dll?prepayc" method="post">
<input name="branchid" value="0021" type="hidden">
<input name="cono" value="000244" type="hidden">
<input name="date" type="hidden">
定单号：<input name="billno" type="text"><br>
金额：<input name="amount" type="text" value="1.00">
<input type="submit"  value="现在支付">
<input type='submit' value='确认' style='display:none;'>
</form>--%>
<DIV class=clr></DIV></DIV>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>





