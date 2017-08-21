<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdjustOther.aspx.cs" Inherits="TravelOnline.CruisesOrder.AdjustOther" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - <%=TitleName %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Order.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/Hotel.css"/>
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <style>
    .roomdivlist {width: 960px;}
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
        <input id="Flag" type="hidden" value="<%=Flag %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>订单号：<%=AutoId %> <%=OrderName%> <%=TitleName %></SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
    </div>
    <DIV class=clr></DIV>

 <div class="w main" id="ChangeInfos">
    <div class="m detail">
        <UL class=tab><LI class=curr><%=TitleName%><SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01" style="font-size: 14px"  >
        <%=ChangeInfos%>
        </div>
    </div>
    <div class="gotonext">
        <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="SubmitOrder()">提交变更申请</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/VisitEdit.js"></script>
    <script type="text/javascript">
        
        $("#OrderRoomList :radio").click(function () {
            //alert($(this).attr("price"));
            var berth = $(this).attr("berth");
            var price = $(this).attr("price");
            var roomid = $(this).attr("roomid");
            $("#tabdiv :radio").each(function () {
                $(this).hide();
                $(this).attr('checked', 'checked');
                this.checked = false;
                if ($(this).attr("berth") == berth) $(this).show();
                //if (Number($(this).attr("price")) < Number(price)) $(this).hide();
                if ($(this).attr("roomid") == roomid) $(this).hide();
            });
        });

        function SubmitOrder() {
            var DoString = "";
            if ($('#Flag').val() == "DinnerChange") {
                DoString = $("#ChangeInfos :radio:checked").val();
            }

            if ($('#Flag').val() == "Settlement") {
                DoString = $("#ChangeInfos :radio:checked").val();
            }
            if ($('#Flag').val() == "Combine") {
                if ($("#CombineId").val() == "") {
                    jNotify('<strong>请输入待合并关联的订单号!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                DoString = $("#CombineId").val();
            }
            $("#islogin").show();
            $("#nextstep").hide();
            var url = "AjaxService.aspx?action=" + $('#Flag').val() + "&DoString=" + DoString + "&TempOrderId=" + $('#TempOrderId').val() + "&r=" + Math.random();
            //window.open(url);
            $.get(url, function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("您的申请已成功提交，请等待后台处理！");
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
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
