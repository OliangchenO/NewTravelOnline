<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPrice.aspx.cs" Inherits="TravelOnline.OrderEdit.EditPrice" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 在线预订</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
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
    </DL></DIV></DIV></div></DIV>

 <div class="right-extra"><%=PriceList %>
    <div class="m detail">
        <UL class=tab><LI class=curr>支付方式<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">请选择您的支付方式：
                <input id="Radio1" type="radio" name="ht" value="1" <%=RB1%>/><label for="Radio1" class=radiobtn>在线支付</label>
                <input id="Radio2" type="radio" name="ht" value="2" <%=RB2%>/><label for="Radio2" class=radiobtn>门店支付</label>
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
    <div class="gotonext">
         <A class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">下一步</A>
    </div>
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
//            var Parms = "";
//            $(".priceli").each(function () {
//                var pid = "#" + $(this).attr("id");
//                if (Number($(pid + " .psel").val()) != 0) {
//                    //Parms += $(pid + " .psel").val();
//                    Parms += $(this).attr("tps") + "@@";
//                    Parms += $(this).attr("tag") + "@@";
//                    Parms += $(pid + " .ftype").html() + "@@";
//                    Parms += $(pid + " .fname").html() + "@@";
//                    Parms += $(pid + " .sellprice").html() + "@@";
//                    Parms += $(pid + " .psel").val() + "@@";
//                    Parms += $(pid + " .sumprice").html();
//                    Parms += "||";

//                }
//            });
            //            Parms = Parms.substr(0, Parms.length - 2);
            var Parms = "";
            var PayType = "";
            var rebates = 0;
            $(".priceli").each(function () {
                var pid = "#" + $(this).attr("id");
                if ($(this).attr("tps") == "Rebate") {
                    if ($(pid + " .psel").val() != "0") rebates += 1;
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
                alert("促销优惠最多只能选择一项，请检查！");
                return false;
            }
            if ($("#ht_title :radio:checked").val() == "1") {
                PayType = "1@" + $("#Nums").val() + "@" + $("#Pre_Price").html() + "@" + $("#SumPre_Price").html();
            }
            else {
                PayType = "2@" + $("#Ht_Branch").val();
            }
            
            Parms = Parms.substr(0, Parms.length - 2);
            var url = "/Purchase/AjaxService.aspx?action=FirdtStep&EditFlag=Manage&TempOrderId=" + $('#TempOrderId').val() + "&Price=" + $("#spanAmount").html() + "&PriceStrings=" + escape(Parms) + "&PayType=" + PayType + "&r=" + Math.random();
            
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    top.location = "EditInfo.aspx?OrderId=" + $('#TempOrderId').val();
                }
                else {
                    alert("提交 失败，请稍后重试！");
                }
            })
        }
    </script>
</body>
</html>


