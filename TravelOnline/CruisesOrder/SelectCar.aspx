<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCar.aspx.cs" Inherits="TravelOnline.CruisesOrder.SelectCar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body>

<div class="page_input">
    <div class="main_input">
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input id="LineId" name="LineId" type="hidden" value="<%=LineId %>"/>
                <input id="VisitListId" name="VisitListId" type="hidden" value="<%=VisitListId %>"/>
                <input id="CarId" name="CarId" type="hidden" value=""/>
            </DIV>
            <div class=toolbar_inputa><div class=fl>&nbsp;&nbsp;待分配人数：<%=SelectNums %>人</div>
                <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
                <A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>&nbsp;&nbsp;
            </div>
            <div class="clear"></div>
            <div id="hoteldetail" class=roomdivlist style="font-size: 12px; width: 99%;">
                <%=CarListInfo %>
            </DIV>
        </form>
    </div>
</div>
<script type="text/javascript">
    $("#save").click(function () {
        var sold = $("#hoteldetail :radio:checked").val();
        if (sold == null) {
            alert("请选择您要分配的车号!");
            return false;
        }
        $("#CarId").val(sold);
        $(this).hide();
        $("#islogin").show();
        $.post("AjaxService.aspx?action=SetCruisesCarNo&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("车号分配成功！");
                    parent.$('#GridView_Refresh_Button').click();
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $("#save").show();
                    $("#islogin").hide();
                }
            });
    });
</script>
</body>
</html>
