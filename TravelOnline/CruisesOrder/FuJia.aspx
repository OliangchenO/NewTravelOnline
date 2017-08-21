<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuJia.aspx.cs" Inherits="TravelOnline.CruisesOrder.FuJia" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>船队信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .select {WIDTH: 720px;}
        .select DL {WIDTH: 720px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 600px;}
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input id="OrderId" name="OrderId" type="hidden" value="<%=OrderId %>"/>
        <input id="autoid" name="autoid" type="hidden" value="<%=autoid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>订单联系人&nbsp;&nbsp;</div>
        <input id="Order_lxr" name="Order_lxr" type="text" maxlength="50" style="width: 400px" value="<%=Order_lxr %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>订单备注：<br>100字以内&nbsp;&nbsp;</div>
        <textarea rows="2" cols="20" id="Order_Memo" name="Order_Memo" style="width: 400px; height: 100px;" onkeydown="limitChars(this, 100)" onchange="limitChars(this, 100)" onpropertychange="limitChars(this, 100)"><%=Order_Memo%></textarea>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">

        function limitChars(obj, count) {
            if (obj.value.length > count) {
                obj.value = obj.value.substr(0, count);
            }
        };

        function SaveInfo() {
            var url = "/Management/AjaxService.aspx?action=FuJia&autoid=" + $("#autoid").val() + "&Order_lxr=" + escape($("#Order_lxr").val()) + "&Order_Memo=" + escape($("#Order_Memo").val()) + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval("(" + data + ")");
                if (obj.success) {
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>
