<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineDestination.aspx.cs" Inherits="TravelOnline.Management.LineDestination" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>目的地</title>
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
        
        .des_select 
        {
            border: solid 2px #DDF0DD;
            MARGIN: 5px 5px 5px 5px;
	        padding: 5px 20px 5px 30px;
            background: #F1F7F1 url(../images/ico-right.png) 0 10px no-repeat;
            background-position: 5px 10px;
            CURSOR: pointer;
            overflow: hidden;
            font-size: 12px;
            FONT-WEIGHT: bold;
            FLOAT: left
        }

        .des_deselect 
        {
            border: solid 2px #DDF0DD;
            MARGIN: 5px 5px 5px 5px;
	        padding: 5px 20px 5px 30px;
            CURSOR: pointer;
            overflow: hidden;
            font-size: 12px;
            FONT-WEIGHT: normal;
            FLOAT: left
        }

    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
                <input name="flag" id="flag" type="hidden" value="<%=flag %>"/>
                <input id="Line_Desid" name="Line_Desid" type="hidden" value="<%=Line_Desid %>"/>
                <input id="none" name="none" type="hidden" />
                <input id="Destinationid" name="Destinationid" type="hidden" />
                <input id="FirstDestination" name="FirstDestination" type="hidden" value=""/>
                <input id="DestinationName" name="DestinationName" type="hidden" value=""/>
                <input id="Des_List" name="Des_List" type="hidden" />
                <input id="Day_List" name="Day_List" type="hidden" value="<%=viewdays %>"/>
            </DIV>
            <div class=toolbar_inputa>
                <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class=line_input>
                目的地：
                <input class=sel type="text" name="Destination" id="Destination" maxlength="10" style="width: 120px"/>
                &nbsp;天数：<input id="days" name="days" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 30px;text-align:center;" maxlength="2"  value="1"/>
                &nbsp;积分：<input id="Integral" name="Integral" type="text" class="ipt easyui-numberbox" precision="3" max="1" style="width: 30px;text-align:center;" maxlength="5" value="<%=Integral %>"/> 5%填写0.05，不积分填1
            </div>
            <div class=line_input>
                <div id="DestinationList" style="clear:both;width:540px;height:auto;max-height:240px;overflow-y:auto;"><%=DestinationInfos %></div>
            </div>
        </form>
    </DIV>
    <script type="text/javascript">
        $('#Destination').bind('keyup', function () {
            var url = "../Common/GetAutoDropList.aspx?action=JournalDestination&SerchName=" + encodeURI(this.value);
            if ($("#flag").val() == "view") {
                if ($("#Line_Desid").val() == "") {
                    jError('<strong>所选线路还没有输入目的地，不能选择景点!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return;
                }
                url = "../Common/GetAutoDropList.aspx?action=DestinationView&desid=" + $("#Line_Desid").val() + "&SerchName=" + encodeURI(this.value);
            }
            if (this.value.length > 1) show(this, "Destinationid", url, "yes");
        });

        function afterselet(callback) {
            
            $('#DestinationList').show();
            var haveit = 0;
            var des_id = $("#Destinationid").val();
            var days = $("#days").val();
            if (days == "") days = "1";
            var daystring = "D" + days + " ";
            if (days == "") daystring = ""
            $("#DestinationList div").each(function () {
                var pid = $(this).attr("tag");
                if (pid == des_id) {
                    $(this).attr({ "class": "des_select" });
                    haveit = 1;
                }
            });
            if (haveit == 0) {
                var str = "<div class=des_select tag=" + des_id + " day=" + days + ">";
                str += daystring;
                str += $("#Destination").val();
                str += "</div>";
                $("#DestinationList").append(str);
            }
            $("#Destination").val("");
            $("#Destination").focus();
        }

        $("#DestinationList div").live("click", function () {
            if ($(this).attr("class") == "des_select") {
                //$(this).attr({ "class": "des_deselect" });
                $(this).remove();
            }
            else {
                $(this).attr({ "class": "des_select" });
            }
        })

        var FirstDesId = "0";
        var DesId = "";
        var DesName = "";
        var DesList = "";
        var DayList = "";

        function getDestion() {
            DesId = ",";
            DesName = ",";
            DesList = "";
            DayList = ",";
            FirstDesId = "0";
            var days = "";
            $("#DestinationList div").each(function () {
                if ($(this).attr("class") == "des_select") {
                    DesId += $(this).attr("tag") + ",";
                    days = $(this).attr("day");
                    if (typeof(days) == "undefined") days = "";
                    DayList += days + ",";
                    DesName += $(this).html() + ",";
                    DesList += "<div class=des_select tag=" + $(this).attr("tag") + " day=" + days + ">";
                    DesList += $(this).html();
                    DesList += "</div>";
                    if (FirstDesId == '0') {
                        FirstDesId = $(this).attr("tag");
                    }
                }
            });
            if (DayList.indexOf("undefined") > -1) DayList = "";
        }

        function SaveInfo() {
            getDestion();
            $("#Destinationid").val(DesId);
            $("#Day_List").val(DayList);
            $("#DestinationName").val(DesName);
            $("#Des_List").val(DesList);
            $("#FirstDestination").val(FirstDesId);
            if ($("#Destinationid").val().length < 2) {
                jError('<strong>请输入目的地!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }
            $("#save").hide();
            $("#islogin").show();
            var url = "AjaxService.aspx?action=LineDestination&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $("#islogin").hide();
                    $("#save").show();
                }
                else {
                    $("#islogin").hide();
                    $("#save").show();
                    if (obj.xianzhi) alert("积分比例不能大于0.03");
                    alert("保存失败");
                }
            });
        }
    </script> 
</body>
</html>