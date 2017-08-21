<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineCarNoAllot.aspx.cs" Inherits="TravelOnline.Cruises.LineCarNoAllot" %>
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
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>

</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
        <input name="flag" id="flag" type="hidden" value="<%=flag %>"/>
        <input name="visitid" id="visitid" type="hidden" value=""/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
        <A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class="line_input <%=hide1 %>">
        <div class=firstinput>观光线路：</div>
        <input class=sel type="text" name="visitname" id="visitname" maxlength="50" readonly="readonly" style="width: 400px"/>&nbsp;&nbsp;
    </div>
    <div class="line_input <%=hide1 %>">
        <div class=firstinput>车号范围：</div>
        <input id="No1" name="No1" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 100px;text-align:center;" maxlength="3" /> &nbsp;至 &nbsp;
        <input id="No2" name="No2" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 100px;text-align:center;" maxlength="3" />&nbsp;&nbsp;
        座位数：<input id="nums" name="nums" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 100px;text-align:center;" maxlength="2" value="<%=BusNums %>"/>
    </div>
    <div class="line_input <%=hide2 %>">
        <div class=firstinput>团号范围：</div>
        <input id="Plan1" name="Plan1" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 100px;text-align:center;" maxlength="3" /> &nbsp;至 &nbsp;
        <input id="Plan2" name="Plan2" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 100px;text-align:center;" maxlength="3" />&nbsp;&nbsp;
        人数：<input id="Berth" name="Berth" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 100px;text-align:center;" maxlength="2" value="<%=BusNums %>" />
    </div>
    <div class="line_input <%=hide2 %>">
        <div class=firstinput>备注说明：</div>
        <input id="memo" name="memo" type="text" class="ipt" style="width: 396px;" maxlength="100" value="<%=memo %>" />
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#visitname').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=CruisesLineVisit&SerchName=" + $("#lineid").val();
            show(this, "visitid", url, "");
        });

        function SaveInfo() {
            var action = "CruisesCarNoAllot"
            if ($("#flag").val() == "CarNo") {
                if ($("#visitid").val() == "") {
                    jNotify('<strong>观光线路不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if ($("#No1").val() == "" || $("#No2").val() == "") {
                    jNotify('<strong>车号范围不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#No1").val()) <= 0) {
                    jNotify('<strong>车号错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#No1").val()) > Number($("#No2").val())) {
                    jNotify('<strong>车号范围错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if ($("#nums").val() == "") {
                    jNotify('<strong>座位数不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#nums").val()) <= 0) {
                    jNotify('<strong>座位数错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }

            if ($("#flag").val() == "PlanNo") {
                if ($("#Plan1").val() == "" || $("#Plan2").val() == "") {
                    jNotify('<strong>团号范围不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#Plan1").val()) <= 0) {
                    jNotify('<strong>团号错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#Plan1").val()) > Number($("#Plan2").val())) {
                    jNotify('<strong>团号范围错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if ($("#Berth").val() == "") {
                    jNotify('<strong>人数不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#Berth").val()) <= 0) {
                    jNotify('<strong>人数错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                action = "CruisesPlanNoAllot"
            }

            if ($("#flag").val() == "EditPlanNo") {
                if ($("#Berth").val() == "") {
                    jNotify('<strong>人数不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#Berth").val()) <= 0) {
                    jNotify('<strong>人数错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                action = "EditPlanNoAllot"
            }

            if ($("#flag").val() == "EditCarNo") {
                if ($("#nums").val() == "") {
                    jNotify('<strong>座位数不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                if (Number($("#nums").val()) <= 0) {
                    jNotify('<strong>座位数错误!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                action = "EditCarNoAllot"
            }
            
            $("#islogin").show();
            $("#save").hide();
            var url = "AjaxService.aspx?action=" + action + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    $("#islogin").hide();
                    $("#save").show();
                    $("#No1").val("");
                    $("#No2").val("");
                    $("#Plan1").val("");
                    $("#Plan2").val("");
                    parent.$('#GridView_Serch_Button').click();
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    $("#islogin").hide();
                    $("#save").show();
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>