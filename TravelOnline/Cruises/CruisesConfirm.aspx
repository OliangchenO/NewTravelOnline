<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesConfirm.aspx.cs" Inherits="TravelOnline.Cruises.CruisesConfirm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>船队信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
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
    <script type="text/javascript">
        function myrefresh() {
            window.location.reload();
        }
	</script>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div><div class=line_input>
        <div class=firstinput>观光说明：<BR>
		<IMG title="扩展输入框高度" onclick="AddIt('visit')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('visit')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="visit" id="visit" style="width: 86%;height:60px"><%=visit%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>付款要求：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('pay')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('pay')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="pay" id="pay" style="width: 86%;height:60px"><%=pay%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>取消说明：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('cancel')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('cancel')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="cancel" id="cancel" style="width: 86%;height:60px"><%=cancel%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>签证递交：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('visa')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('visa')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="visa" id="visa" cols="" rows="" style="width: 86%;height:60px"><%=visa%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>变更须知：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('change')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('change')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="change" id="change" cols="" rows="" style="width: 86%;height:60px"><%=change%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>其他说明：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('other')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('other')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="other" id="other" cols="" rows="" style="width: 86%;height:60px"><%=other%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>观光确认单使用说明：<BR>
        <IMG title="扩展输入框高度" onclick="AddIt('views')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('views')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="views" id="views" cols="" rows="" style="width: 86%;height:100px"><%=views%></textarea>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        function AddIt(tname) {
            var this_obj = eval('document.all.' + tname);
            var h = this_obj.offsetHeight
            this_obj.style.height = h + 200;
        }

        function DecIt(tname) {
            var this_obj = eval('document.all.' + tname);
            this_obj.style.height = "60";
        }

        function SaveInfo() {
            var url = "AjaxService.aspx?action=CruisesConfirm&r=" + Math.random();
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
