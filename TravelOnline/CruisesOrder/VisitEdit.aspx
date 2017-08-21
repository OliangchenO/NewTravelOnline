<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitEdit.aspx.cs" Inherits="TravelOnline.CruisesOrder.VisitEdit" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 岸上观光调整</title>
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
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>岸上观光调整</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
    </div>
    <DIV class=clr></DIV>

 <div class="w main">
    <%=PriceList %>
    <form id="form_data" onsubmit="return false;" method="post">
        <input id="PriceStrings" name="PriceStrings" type="hidden" value=""/>
    </form>
    <div class="gotonext">
        <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="SubmitOrder()">下一步</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/VisitEdit.js"></script>
    <script type="text/javascript">
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

            var Parms = "";
           $(".priceli").each(function () {
                var pid = "#" + $(this).attr("id");
                if (Number($(pid + " .psel").val()) != 0) {
                    //Parms += $(pid + " .psel").val();
                    Parms += $(this).attr("tps") + "@@";
                    Parms += $(this).attr("tag") + "@@";
                    Parms += $(pid + " .ftype").html() + "@@";
//                    if ($(this).attr("Cuises") == "1") {
//                        Parms += $(pid + " .froomname").html() + "@@";
//                    }
//                    else {
//                        Parms += $(pid + " .fname").html() + "@@";
//                    }
                    Parms += $(pid + " .fnamelong").html() + "@@";
                    Parms += $(pid + " .sellprice").html() + "@@";
                    Parms += $(pid + " .psel").val() + "@@";
                    Parms += $(pid + " .sumprice").html();
                    Parms += "||";
                }
            });

            Parms = Parms.substr(0, Parms.length - 2);
            $("#PriceStrings").val(Parms);
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            //alert(Parms);
            var url = "/Purchase/AjaxService.aspx?action=VisitEdit&TempOrderId=" + $('#TempOrderId').val() + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "VisitSelect.aspx?OrderId=" + $('#TempOrderId').val();
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(obj.error);
                }
            });
        }
    </script>
</body>
</html>


