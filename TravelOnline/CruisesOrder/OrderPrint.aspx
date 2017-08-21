<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPrint.aspx.cs" Inherits="TravelOnline.CruisesOrder.OrderPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>邮轮包船订单详情</title>
    <style>
    .tb {width:100%;table-layout:fixed;font-size: 14px;BORDER-LEFT: #666666 1px solid;}
    .tb td {PADDING: 2px 5px 0px 5px;BORDER-BOTTOM: #666666 1px solid;BORDER-RIGHT: #666666 1px solid;LINE-HEIGHT: 20px;}
    .linename{BORDER-BOTTOM: #666666 1px solid;FONT-WEIGHT: bold;font-size: 18px;TEXT-ALIGN: center}
    .trt{FONT-WEIGHT: bold;TEXT-ALIGN: center;}
    .title{FONT-WEIGHT: bold;color:#000;background:#CCCCCC}
    .fl {FLOAT: left}
    .fr {FLOAT: right}
    .fn {FLOAT: none}
    .al {TEXT-ALIGN: left}
    .ac {TEXT-ALIGN: center}
    .ar {TEXT-ALIGN: right}
    .hide {DISPLAY: none}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; margin-left: auto;margin-right: auto;">
        <table height="80" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
			<tr><td vAlign="middle" align="center" height="60"><IMG height=60 src="/images/06180025515.jpg" width=650><HR width="100%" SIZE="1"></td></tr>
		</table>
        <table border="0" cellpadding="0" cellspacing="0" align="center" width="100%">
            <tr><td class=linename height="40px"><%=LineName %></td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <%=OrderInfo %>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>联系信息</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <%=LinkInfo%>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>房间和人数</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center" >
            <%=RoomInfo %>
        </table>
        <table class="tb <%=hide %>" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>同行返利明细</td></tr>
        </table>
        <table class="tb <%=hide %>" border="0" cellpadding="0" cellspacing="0" align="center" >
            <%=RebateInfo%>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>费用明细</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center" >
            <%=PriceInfo %>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>游客信息</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center" >
            <%=GuestInfo %>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>要求用餐批次</td></tr>
            <tr><td><%=DinnerInfo %>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>岸上观光线路确认</td></tr>
            <tr><td><%=visit%>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>付款要求</td></tr>
            <tr><td><%=pay%>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>订单或舱房取消说明</td></tr>
            <tr><td><%=cancel%>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>签证材料递交</td></tr>
            <tr><td><%=visa%>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>姓名更换须知</td></tr>
            <tr><td><%=change%>&nbsp;</td></tr>
        </table>
        <table class="tb" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr><td class=title>其他须知</td></tr>
            <tr><td><%=other%>&nbsp;</td></tr>
        </table>
        <table style="width: 680px; font-size: 14px;LINE-HEIGHT: 22px;PADDING-top: 5px" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr>
		        <td align="center"><%--<HR width="100%" SIZE="1">--%>
			        上海中国青年旅行社&nbsp;&nbsp;地址：衡山路2号&nbsp;&nbsp;邮编：200031
		        </td>
	        </tr>
	        <tr>
		        <td align="center">邮轮部&nbsp;&nbsp;联系电话：021-64330000-2129&nbsp;&nbsp;传真：021-64747571
		        </td>
	        </tr>
        </table>
        <div style="width:1px; height:1px;LEFT: 480px; POSITION: relative; TOP: -150px"><IMG  alt="" src="/Images/dianzizhang.png" width="250px"></div>
    </div>
    </form>
</body>
</html>
