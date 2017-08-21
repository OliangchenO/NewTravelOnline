<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineRoomEdit.aspx.cs" Inherits="TravelOnline.Cruises.LineRoomEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>船队信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body>
<form id="form_data" onsubmit="return false;" method="post">
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
        <DIV id="inputs" style="DISPLAY:none">
            <input name="roomid" id="roomid" type="hidden" value="<%=roomid %>"/>
        </DIV>
        <div class=line_input>
            <div class=firstinput>房间名称：</div>
            <input class=ipt id="roomname" name="roomname" type="text" style="width: 400px;" maxlength="50" value="<%=roomname %>"/>
        </div>
        <div class=line_input>
            <div class=firstinput>可住人数：</div>
            <input id="berth" name="berth" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 56px;text-align:center;" maxlength="2" value="<%=berth %>"/>
        </div>
        <div class=toolbar_inputa>
            <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </DIV>
</form>
    <script type="text/javascript">
        function SaveInfo() {
            if ($("#roomname").val() == "" || $("#berth").val() == "") {
                jNotify('<strong>房间名称和可住人数都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=EditRoomNo&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
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



