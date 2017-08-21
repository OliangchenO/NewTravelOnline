<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesRoute.aspx.cs" Inherits="TravelOnline.Cruises.CruisesRoute" %>
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
    <style type="text/css">
        .tit {FONT-WEIGHT: bold;TEXT-ALIGN: center; }
        .fi {TEXT-ALIGN: center; }
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
        <input name="shipid" id="shipid" type="hidden" value="<%=shipid %>"/>
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo('reset')" class="tools hide" id="A1"><img src="../images/icon/chinaz76.png" class=img20>重置</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>用餐时间1：</div>
        <input value="<%=Dtime1 %>"" class=ipt type="text" name="Dtime1" id="Dtime1" maxlength="20" style="width: 80px"/>&nbsp;&nbsp;
        用餐人数：
        <input id="Dnum1" name="Dnum1" type="text" class="ipt easyui-numberbox" precision="0" max="9999" style="width: 60px;text-align:center;" maxlength="4" value="<%=Dnum1 %>"/>&nbsp;&nbsp;&nbsp;&nbsp;
        用餐时间2：
        <input value="<%=Dtime2 %>" class=ipt type="text" name="Dtime2" id="Dtime2" maxlength="20" style="width: 80px"/>&nbsp;&nbsp;
        用餐人数：
        <input id="Dnum2" name="Dnum2" type="text" class="ipt easyui-numberbox" precision="0" max="9999" style="width: 60px;text-align:center;" maxlength="4" value="<%=Dnum2 %>"/>
    </div>
    
    <div class=line_input>
        <table style="width:100%;">
            <tr>
                <td class=tit width="10%">天数</td>
                <td class=tit width="30%">停靠港口</td>
                <td class=tit width="15%">抵达时间</td>
                <td class=tit width="15%">起航时间</td>
                <td class=tit width="30%">岸上观光</td>
            </tr>
            <%=Routes%>
        </table>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        function SaveInfo() {
            var url = "AjaxService.aspx?action=CruisesRoute&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>


