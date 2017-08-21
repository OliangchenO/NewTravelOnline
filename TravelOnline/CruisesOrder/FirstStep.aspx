<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstStep.aspx.cs" Inherits="TravelOnline.CruisesOrder.FirstStep" %>
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
    <link href="/Styles/Order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
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
        <input id="LineId" type="hidden" value="<%=LineId %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>选择价格</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step1" style="display:block;">
            <li class="view">选择价格 </li>
	        <li class="selects">录入名单</li>
	        <li class="book">岸上观光</li>
	        <li class="check">核对订单</li>
	        <li class="submit">成功提交</li>
        </ul>
    </div>
    <DIV class=clr></DIV>
    <DIV class=left>
    <div id="pricebar"  style="width:150px;">
    <DIV id=mymenu class=m>
    <DIV class=mc style="BACKGROUND: #ffffff;">
    <DL tag="1">
    <DT tag="1">人数与价格<B></B></DT>
    <DD>
        <div class="package_pepleprise">
        <p>总价:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount">0</span></p>
        <div id="divshow"><p>人均:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAve">0</span></p>
        <p>人数:&nbsp;<span class="base_price02" id="spanNums"><%=Nums %></span></p></div>
        </div>  
    </DD>
    </DL>
    <DL>
    <DT>热线电话<B></B></DT>
    <DD><div class="package_pepleprise"><span class="base_price03">4006-777-666</span></DIV></DD>
    </DL>
    </DIV></DIV></div></DIV>

 <div class="right-extra">
 
<%-- <div id=Div1 class="m detail">
    <UL class=tab><LI class=curr>房间和人数<SPAN></SPAN></LI></UL>
    <div class="mc tabcon borders">
        <div style="width: 780px;PADDING:5px 10px 5px 10px;" class=roomdiv>
            <DIV class=roomHead>双人间最少入住2人，不满2人需要补房差；三人间或四人间同舱的第3、第4位可享受价格优惠；</DIV>
            <form id="form_data" onsubmit="return false;" method="post">
                <DIV id="DIV1" style="DISPLAY:none">
                    <input id="AllRoom" name="AllRoom" type="hidden" value="0"/>
                    <input id="AllPrice" name="AllPrice" type="hidden" value="0"/>
                </DIV>
                <table id="RoomSelectList" style="width: 100%;">
                    <tr class=tit>
                        <td width="30%">房间类型</td>
                        <td width="5%">成人</td>
                        <td width="5%">儿童</td>
                        <td width="5%">房间数</td>
                        <td width="10%">第1、2人价格</td>
                        <td width="10%">第3成人价</td>
                        <td width="10%">第3儿童价</td>
                        <td width="10%">价格小计</td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</div>--%>

    <%=PriceList %>

    <div class="m detail <%=hide4 %>">
        <UL class=tab><LI class=curr>用餐时间<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="Dinner_Time" class="mc tabcon">请选择晚餐时间：
                <%=dinnerstring %>
            </div>
        </div>
    </div>

    <div class="m detail <%=hide3%>">
        <UL class=tab><LI class=curr>支付方式<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">请选择您的支付方式：
                <span class="<%=hide1%>"><input id="Radio1" type="radio" name="ht" value="1" <%=RB1%>/><label for="Radio1" class=radiobtn>在线支付</label></span>
                <span class="<%=hide5%>"><input id="Radio2" type="radio" name="ht" value="2" <%=RB2%>/><label for="Radio2" class=radiobtn>门店支付</label></span>
                <span class="<%=hide2%>"><input id="Radio3" type="radio" name="ht" value="3" <%=RB3%>/><label for="Radio3" class=radiobtn>公司挂账</label></span>
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
                <li id=Pre3 class="hide order">预定成功后，通过公司转账方式支付</li>
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
                $("#Pre3").hide();
                $("#Pre2").hide();
                $("#Pre1").show();
            }
            if (vals == "2") {
                $("#Pre3").hide();
                $("#Pre1").hide();
                $("#Pre2").show();
                if ($("#LineId").val() == "12509") $("#Pre1").show();
            }
            if (vals == "3") {
                $("#Pre2").hide();
                $("#Pre1").hide();
                $("#Pre3").show();
            }
        };

        function SubmitOrder() {
            var OrderNums = Number($("#Nums").val());
            var goto = 0;
            $(".Visit").each(function () {
                var vname = $(this).attr("tag");
                var visitnums = 0;
                var pid = "#" + $(this).attr("id");
                $(pid + " .psel").each(function () {
                    visitnums += Number($(this).val());
                });
                if (OrderNums != visitnums) {
                    alert(vname + "合计" + visitnums + "人，订单人数" + OrderNums + "人，两者必须相等！");
                    goto = "1";
                    return false;
                }
            });
            if (goto == "1") return false;

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

            var HT = $("#ht_title :radio:checked").val();
            switch (HT) {
                case "1":
                    PayType = "1@" + $("#Nums").val() + "@" + $("#Pre_Price").html() + "@" + $("#SumPre_Price").html();
                    break;
                case "2":
                    PayType = "2@" + $("#Ht_Branch").val();
                    if ($("#LineId").val() == "12509") PayType += "@" + $("#Nums").val() + "@" + $("#Pre_Price").html() + "@" + $("#SumPre_Price").html();
                    break;
                case "3":
                    PayType = "3@0@0@0";
                    break;
                default:
                    PayType = "3@0@0@0";
            }

            Parms = Parms.substr(0, Parms.length - 2);

            var dtimes = $("#Dinner_Time :radio:checked").val();
            var dnums = $("#Dinner_Time :radio:checked").attr("tgs");

            //检查用餐时间段的人数
            if (dnums != "0") {

            }

            $("#DinnerStrings").val(dtimes);
            $("#PriceStrings").val(Parms);
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            var url = "/Purchase/AjaxService.aspx?action=FirdtStep&EditFlag=0&TempOrderId=" + $('#TempOrderId').val() + "&Price=" + $("#spanAmount").html() + "&PayType=" + PayType + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/CruisesOrder/SecondStep/" + $('#TempOrderId').val() + ".html";
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(obj.error);
                }
            });
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


