<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempOrder.aspx.cs" Inherits="TravelOnline.Purchase.TempOrder" %>
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
    <link href="/Styles/Order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style>
        .order1 {
        font-size: 13px;
        LINE-HEIGHT: 20px;
        }
        .order1 .oname {
        WIDTH: 100px;
        TEXT-ALIGN: right;
        }
        .order1 div {
        HEIGHT: 40px;
        OVERFLOW: hidden;
        FONT-WEIGHT: normal;
        FLOAT: left;
        TEXT-ALIGN: left;
        }
        .order1 .oinfo {
        WIDTH: 700px;
        LINE-HEIGHT: 20px;
        }
    </style>
</head>
<body id="none">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="Nums" type="hidden" value="<%=Nums %>"/>
        <input id="Adults" type="hidden" value="<%=Adults %>"/>
        <input id="Childs" type="hidden" value="<%=Childs %>"/>
        <input id="TempOrderId" type="hidden" value="<%=OrderId %>"/>
        <input id="LineId" type="hidden" value=""/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>选择价格</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step2" style="display:block;">
            <li class="view">选择线路 </li>
	        <li class="selects">选择价格</li>
	        <li class="book">填写信息</li>
	        <li class="check">核对订单</li>
	        <li class="submit">成功提交</li>
        </ul>
    </div>
    <DIV class=clr></DIV>
    <DIV class="left ">
    <div id="pricebar"  style="width:150px;">
    <DIV id=mymenu class=m>
    <DIV class=mc style="BACKGROUND: #ffffff;">
    <DL tag="1" class="<%=huifeng %>">
    <DT tag="1">人数与价格<B></B></DT>
    <DD>
        <div class="package_pepleprise">
        <p>总价:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount">0</span></p>
        <div id="divshow"><p>人均:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAve">0</span></p>
        <p>人数:&nbsp;<span class="base_price02" id="spanNums"><%=Nums %></span></p></div>
        </div>  
    </DD>
    </DL>
    <DL class="<%=huifeng %>">
    <DT>热线电话<B></B></DT>
    <DD><div class="package_pepleprise"><span class="base_price03">4006-777-666</span></DIV></DD>
    </DL>
    </DIV></DIV></div></DIV>


 <div class="right-extra"><%=PriceList %>

<%--     
    <div class="m detail">
        <UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=price>
                <li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>
                <li id=1><div class=ftype>成人价</div><div class=fname>海上观金门游船船票兑换券</div><div class=fprice>&yen;<span class=sellprice>300235</span></div>
                    <div class=fnum>
                        <select class=psel>
	                        <option value="0">0</option>
	                        <option value="1">1</option>
	                        <option value="2">2</option>
                        </select>
                    </div>
                    <div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div>
                </li>
            </ul>
        </div>
    </div>
    <div class="m detail"><UL class=tab><LI class=curr>保险费用<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            
        </div>
    </div>
    <DIV class="select"><DIV class=mt><H1></H1><STRONG>特别注意事项</STRONG>
        </DIV>
    </DIV>

--%>
    <div class="m detail">
        <UL class=tab><LI class=curr>支付方式<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">请选择您的支付方式：
                <span class="<%=hide1%>"><input id="Radio1" type="radio" name="ht" value="1" <%=RB1%>/><label for="Radio1" class=radiobtn>在线支付</label></span>
                <span class="<%=hide2%>"><input id="Radio2" type="radio" name="ht" value="2" <%=RB2%>/><label for="Radio2" class=radiobtn>门店支付</label></span>
            </div>
            <ul id="Ht_List">
                <%=Preference %>
                <li id=Pre2 class="hide order">
                    <div class=oname>门店地址：</div><div class=oinfo>
                        <select id=Ht_Branch style="width: 500px">
                            <%=BranchOption %>
                        </select>&nbsp;
                    </div>
                    <span id=BranchInfo>
                        <%=BranchMap %>
                    </span>
                </li>
            </ul>
        </div>
    </div>
    <form id="form_data" onsubmit="return false;" method="post">
        <input id="PriceStrings" name="PriceStrings" type="hidden" value=""/>
        <input id="DinnerStrings" name="DinnerStrings" type="hidden" value=""/>
    </form>
    <div class="gotonext">
        <A id=upstep class="btn-link btn-personal" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">下一步</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
 <%----%>

</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/FirstStep.js"></script>
    <script type="text/javascript">
        function RadioSet(vals) {
            SumAllPrice();
            if (vals == "1") {
                $("#Pre2").hide();
                $("#Pre1").show(); 
             }
            else {
                $("#Pre1").hide();
                $("#Pre2").show();
            }
        };

        function SubmitOrder() {
            $("#CheckCouponFlag").val("0");
            var Parms = "";
            var PayType = "";
            var rebates = 0;
            var CouponId = "";
            var CouponNums = "";
            $(".priceli").each(function () {
                var pid = "#" + $(this).attr("id");
                if ($(this).attr("tps") == "Rebate" || $(this).attr("tps") == "Coupon") {
                    if ($(pid + " .psel").val() != "0") {
                        rebates += 1;
                        if ($(this).attr("tps") == "Coupon") {
                            CouponId = $(this).attr("tag");
                            CouponNums = $(pid + " .psel").val();
                        }
                    }
                }

                if (Number($(pid + " .psel").val()) != 0) {
                    if (Number($(pid + " .sumprice").html())==0) {
                        alert("费用计算错误，请检查！");
                        return false;
                    }
                    //Parms += $(pid + " .psel").val();
                    Parms += $(this).attr("tps") + "@@";
                    Parms += $(this).attr("tag") + "@@";
                    Parms += $(pid + " .ftype").html() + "@@";
                    if ($(this).attr("Cuises") == "1") {
                        Parms += $(pid + " .froomname").html() + "@@";
                    }
                    else {
                        Parms += $(pid + " .fname").html() + "@@";
                    }
                    Parms += $(pid + " .sellprice").html() + "@@";
                    Parms += $(pid + " .psel").val() + "@@";
                    Parms += $(pid + " .sumprice").html();
                    Parms += "||";
                }
            });
            if (rebates > 1) {
                alert("促销优惠或优惠券最多只能选择一项，请检查！");
                return false;
            }
            var CheckCoupon = "0";
            $.ajaxSettings.async = false;
            if (CouponId != "") {
                var url1 = "/Purchase/AjaxService.aspx?action=CouponUse&TempOrderId=" + $('#TempOrderId').val() + "&CouponId=" + CouponId + "&CouponNums=" + CouponNums + "&Ave=" + $("#spanAve").html() + "&Amount=" + $("#spanAmount").html() + "&r=" + Math.random();
                $.getJSON(url1, function (date) {
                    if (date.error) {
                        alert(date.error);
                        CheckCoupon = "1";
                    }
                })
            }
            $.ajaxSettings.async = true;
            if (CheckCoupon == "1") {
                return false;
            }

            if ($("#ht_title :radio:checked").val() == "1") {
                PayType = "1@" + $("#Nums").val() + "@" + $("#Pre_Price").html() + "@" + $("#SumPre_Price").html();
            }
            else {
                PayType = "2@" + $("#Ht_Branch").val();
            }

            Parms = Parms.substr(0, Parms.length - 2);
            $("#PriceStrings").val(Parms);
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            var url = "/Purchase/AjaxService.aspx?action=FirdtStep&EditFlag=0&TempOrderId=" + $('#TempOrderId').val() + "&Price=" + $("#spanAmount").html() + "&PayType=" + PayType + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/Order/SecondStep/" + $('#TempOrderId').val() + ".html";
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(obj.error);
                }
            });
//            var url = "/Purchase/AjaxService.aspx?action=FirdtStep&EditFlag=0&TempOrderId=" + $('#TempOrderId').val() + "&Price=" + $("#spanAmount").html() + "&PriceStrings=" + escape(Parms) + "&PayType=" + PayType + "&r=" + Math.random();
//            
//            $.getJSON(url, function (date) {
//                if (date.success == 0) {
//                    top.location = "/Order/SecondStep/" + $('#TempOrderId').val() + ".html";
//                }
//                else {
//                    alert("提交 失败，请稍后重试！");
//                }
//            })
        }
    </script>
</body>
</html>

