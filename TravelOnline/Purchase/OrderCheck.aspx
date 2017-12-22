<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCheck.aspx.cs" Inherits="TravelOnline.Purchase.OrderCheck" %>
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
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
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
        <h2 class="headline"><%=LineName %><SPAN class=headstep>核对订单</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step4" style="display:block;">
            <%=StepString %>
        </ul>
    </div>
    <DIV class=clr></DIV>
    <DIV class=left>
    <div id="pricebar"  style="width:150px;">
    <DIV id=mymenu class=m>
    <DIV class=mc style="BACKGROUND: #ffffff;">
    <DL tag="1" class="<%=huifeng %>">
    <DT tag="1">人数与价格<B></B></DT>
    <DD>
        <div class="package_pepleprise">
        <p>总价:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount"><%=AllPrice %></span></p>
        <div id="divshow"><p>人均:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAve"><%=AvePrice %></span></p>
        <p>人数:&nbsp;<span class="base_price02" id="spanNums"><%=Nums %></span></p></div>
        </div>  
    </DD>
    </DL>
        <DL class="<%=huifeng %>">
    <DT>热线电话<B></B></DT>
    <DD><div class="package_pepleprise"><span class="base_price03">4006-777-666</span></DIV></DD>
    </DL>
    </DIV></DIV></div></DIV>

 <div class="right-extra">
    <div class="m detail">
        <UL class=tab><LI class=curr>联系信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="checkinfo" class="mc tabcon"><%=User_Info %></div>
            <ul class=checks>
                <li class=memo><div class=oname><%=tebieshuoming %>：</div><div class="oinfo"><%=User_Memo %></div></li>
            </ul>
        </div>
    </div>

    <%=CuisesList%>

    <div class="m detail">
        <UL class=tab><LI class=curr>费用明细<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <%=PriceList %>
        </div>
    </div>
    <%=DinnerInfo %>
    <div class="m detail">
        <UL class=tab><LI class=curr>游客信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01"><div class="VisitList">
            <%=GuestList %>
        </div></div>
    </div>
    
    <div class="m detail <%=hide1%>">
        <UL class=tab><LI class=curr>旅游合同<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order><%=Contract %></ul>  
        </div>
    </div>

    <div class="m detail <%=hide1%>">
        <UL class=tab><LI class=curr>发票信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order><%=Invoice %></ul>
        </div>
    </div>
    <div class="m detail <%=huifeng1%>">
        <UL class=tab><LI class=curr>发票信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
        如需个人支付部分的费用发票，请联系客服64747516
        </div>
    </div>
    <div class="m detail <%=PolicyHide%>">
        <UL class=tab><LI class=curr>优惠券使用<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
             <ul class=order>券号:&nbsp;<input id="PolicyNo" name="PolicyNo" type="text" class="ipt" style="width: 150px;font-size: 14px;font-weight:bold;" maxlength="12" value=""/> &nbsp; &nbsp;
                <a href="javascript:" onclick="showPolicy()" class="btn btn-small btn-success">查看可用优惠券</a>
             </ul>
             <div style="margin-top:10px;display:none" id="Policy">
                <span id="Span1" class="iloading1">查询中，请稍候...</span>
             </div>
        </div>
        
    </div>
    <div class="m detail <%=integralhide%>">
        <UL class=tab><LI class=curr>积分使用<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order>本次订单最多可用 <em style="color:red;font-size: 14px;font-weight:bold;"><%=Allintegral%></em> 积分，本次使用 <input id="Integral" name="Integral" type="text" class="ipt easyui-numberbox" precision="0" max="<%=Allintegral%>" style="width: 60px;text-align:center;font-size: 14px;font-weight:bold;" maxlength="5" value=""/> 积分</ul>
        </div>
    </div>
    <div class="m detail <%=hide2%>">
        <UL class=tab><LI class=curr>付款方式<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">请选择您的付款方式：
                <input id="Radio1" type="radio" name="RebateFlag" value="0" <%=RB1%>/><label for="Radio1" class=radiobtn>按订单全额付款</label>
                <input id="Radio2" type="radio" name="RebateFlag" value="1" <%=RB2%>/><label for="Radio2" class=radiobtn>按订单结算价付款</label>
            </div>
        </div>
    </div>

    <div class="gotonext">
         <A id=upstep class="btn-link btn-personal" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="SubmitOrder()">提交订单</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        function showPolicy() {
            $("#Policy").show();
            var url = "/Purchase/AjaxService.aspx?action=ShowMyCoupon&TempOrderId=" + $('#TempOrderId').val() + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                $("#Policy").html(date.success);
            })
        }

        function showid(uno) {
            $("#PolicyNo").val(uno);
        }
        function SubmitOrder() {
            var info = "";
            if ($("#Integral").val() != "") {
                info = "本次订单使用" + $("#Integral").val() + "积分，";
            }
            if (confirm(info + "确认要提交订单吗？")) {
            }
            else
            { return false; }
            var PayType = "0";
            if ($("#ht_title :radio:checked").val() == "1") {
                PayType = "1";
            }

            var url = "/Purchase/AjaxService.aspx?action=ThirdStep&Policy=" + $('#PolicyNo').val() + "&Integral=" + $('#Integral').val() + "&TempOrderId=" + $('#TempOrderId').val() + "&PayType=" + PayType + "&r=" + Math.random();
            //window.open(url);
            //return false;

            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    window.history.forward(100);
                    var ul = "/Order/FinalStep/" + $('#TempOrderId').val() + ".html"
                    location.replace(ul);
                    //top.location = "/Order/FinalStep/" + $('#TempOrderId').val() + ".html";
                }
                else if (date.success == 9) {
                    window.history.forward(10);
                    var ul = "/Order/OrderSave/" + $('#TempOrderId').val() + ".html"
                    location.replace(ul);
                }
                else if (date.success == 10 || date.success == 30) {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(date.info);
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert("提交 失败，请稍后重试！");
                }
            })
        }
    </script>
</body>
</html>



