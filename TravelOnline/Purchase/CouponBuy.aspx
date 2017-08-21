<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponBuy.aspx.cs" Inherits="TravelOnline.Purchase.CouponBuy" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>优惠券购买</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/shoppingcart.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/base.js"></script>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
<DIV class="w main">
<form id="form1" runat="server" onsubmit="javascript:return check_null(this);" >
<div id="order_title">
<h2 class="headline"><SPAN class=headstep>优惠券购买</SPAN></h2>
</div>
<DIV class=clr></DIV>
<DIV class=forget class=w>
<DIV class=mt>
<H2>优惠券信息</H2><B></B></DIV>
<DIV style="PADDING-TOP: 10px" class=mc>
<ul class=payul>
<li><div class=tname>&nbsp;</div><div class=tinfo><IMG src="<%=logourl %>"></div></li>
<li><div class=tname>&nbsp;</div><div class=tinfo>&nbsp;</div></li>
<li><div class=tname>使用说明：</div><div class=tinfo>&nbsp;<%=memo%></div></li>
<li><div class=tname>有效期：</div><div class=tinfo>&nbsp;<%=BeginDate%></div></li>
<li><div class=tname>抵用金额：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" name="par" id="par"><%=par%></span> </div></li>
<li><div class=tname>销售价：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" name="sellprice" id="sellprice"><%=sellprice%></span></div></li>
<li><div class=tname>购买份数：</div><div class=tinfo>&nbsp;&nbsp;
<select id="crnum" name="crnum" style="width: 50px">
<option value="1" selected="selected">1</option>
<option value="2">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
</select>
</div></li>
<li><div class=tname>&nbsp;</div><div class=tinfo>&nbsp;</div></li>
<li><div class=tname>金额合计：</div><div class=tinfo><span class="base_price02" style="FONT-SIZE: 20px;">&yen;</span><input id="P_PayNow" name="P_PayNow" type="text" class="easyui-numberbox" precision="0" max="999999" size="7" maxlength="9" style="WIDTH: 100px;text-align:center;FONT-WEIGHT: bold; FONT-SIZE: 20px; color:#e56700;BORDER-BOTTOM: #196297 1px solid;BORDER-LEFT: #ffffff 1px solid;BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid;" value="<%=sellprice%>" readonly="readonly" /></div></li>
</ul>
</DIV>
</DIV>

<div class="gotonext">
<%--<asp:LinkButton ID="LinkButton1" runat="server" class="btn-link btn-personal">现在购买</asp:LinkButton>--%>
<span style="padding-left: 30px">&nbsp;</span><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>
<%=BuyButton %>

<DIV id="inputs" style="DISPLAY:none">
<input id="Uid" name="Uid" type="hidden" value="<%=Uid %>"/>
</DIV>
<script type="text/javascript">
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
        $("#islogin").show();
        $("#OrderBtn").hide();
        var url = "/Purchase/AjaxService.aspx?action=CouponBuy&Uid=" + $('#Uid').val() + "&Nums=" + $('#crnum').val() + "&r=" + Math.random();
        //window.open(url);
        $.getJSON(url, function (date) {
            if (date.success) {
                top.location = "/Pay/PayNow.aspx?CouponId=" + date.success;
            }
            else {
                $("#islogin").hide();
                $("#OrderBtn").show();
                alert(date.error);
            }
        })
    }

    $(document).ready(function () {
        $('#crnum').change(function () {
            var sums = 0;
            sums = Number($(this).val()) * Number($("#sellprice").html());
            $("#P_PayNow").val(sums);
        });
    });
</script>
</form>
<DIV class=clr></DIV></DIV>
<script type="text/javascript" src="/Scripts/base.lib.js"></script>
<script type="text/javascript" src="/Scripts/base.product.js"></script>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>





