<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesLineSet.aspx.cs" Inherits="TravelOnline.Cruises.CruisesLineSet" %>
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
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="shipid" id="shipid" type="hidden" value="<%=shipid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>包船船队：</div>
        <input value="<%=typename %>" class=sel type="text" name="typename" id="typename" maxlength="50" readonly="readonly" style="width: 400px"/>
    </div>
    <div class=line_input>
        <div class=firstinput>出发日期：</div>
        <input class="ipt iconDate" id="PlanDate" name="PlanDate" type="PlanDate" value="<%=PlanDate %>" style="width: 120px" readonly="readonly" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        计划id：<input id="planid" name="planid" type="text" class="ipt easyui-numberbox" precision="0" max="9999999999" style="width: 120px;text-align:center;" maxlength="10" value="<%=planid %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>儿童年龄：</div>
        <select name="AgeLimit" id="AgeLimit" style="width:130px;">
	        <option selected="selected" value="18">18岁</option>
	        <option value="21">21岁</option>
        </select>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input id="VisitSell" name="VisitSell" type="checkbox" value="1" <%=VisitSell %> />销售岸上观光
    </div>
    <div class=line_input>
        <div class=firstinput>报表格式：</div>
        <select name="CruisesReport" id="CruisesReport" style="width:130px;">
	        <option selected="selected" value="1">皇家加勒比</option>
	        <option value="2">歌诗达</option>
        </select>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#typename').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=CruisesShip&SerchName=All";
            show(this, "shipid", url, "");
        });

        $(document).ready(function () {
            $("#AgeLimit").val("<%=AgeLimit%>");
            $("#CruisesReport").val("<%=CruisesReport%>");
        });

        $(function () {
            $('#PlanDate').calendar({ minDate: '%y-%M-%d', btnBar: false });
        });

        function SaveInfo() {
            if ($("#planid").val() == "" || $("#shipid").val() == "" || $("#PlanDate").val() == "") {
                jNotify('<strong>包船船队、出发日期和计划id都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=CruisesLineSet&r=" + Math.random();
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
