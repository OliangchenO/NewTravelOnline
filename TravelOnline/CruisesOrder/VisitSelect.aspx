<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitSelect.aspx.cs" Inherits="TravelOnline.CruisesOrder.VisitSelect" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 岸上观光人员选择</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>    
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <style>
    .VisitList {width: 960px;}
    .CheckBoxList {width: 960px;}
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
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>岸上观光人员选择</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
    </div>
    <DIV class=clr></DIV>
    <div class="w main">
<form id="form_data" onsubmit="return false;" method="post">
    <%=VisitListInfo %>
    <div class="gotonext">
         <A id=upstep class="btn-link btn-personal" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">确认修改</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</form>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/CruisesThirdStep.js"></script>
    <script type="text/javascript">
        function SubmitOrder() {
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            var url = "/CruisesOrder/AjaxService.aspx?action=VisitSelect&TempOrderId=" + $('#TempOrderId').val() + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if (obj.success != "OK") alert(obj.success);
                    top.location = "/OrderView/" + $('#TempOrderId').val() + ".html";
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
