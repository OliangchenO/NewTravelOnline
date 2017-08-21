<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialTopicAdd.aspx.cs" Inherits="TravelOnline.Management.SpecialTopicAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .select {WIDTH: 540px;}
        .inputdiv {PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 10px; PADDING-RIGHT: 0px; PADDING-TOP: 5px}
        table {border: solid 1px #e8eef4;border-collapse: collapse;}
        table td{padding: 10px 5px 10px 5px;border: solid 1px #e8eef4;}
        table th{padding: 5px 5px;text-align: left;background-color: #e8eef4;border: solid 1px #e8eef4;}
        .upp {WIDTH: 540px;}
        .width120 {WIDTH: 250px;}
        .bnP1{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad1.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        .bnP2{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        input[type='file'] {
         width:60px;
         height:26px;
         CURSOR: hand;
         line-height:26px;
         position:relative;
         opacity:0;                        /*设置它的透明度为0，即完全透明。这个语句，对付除IE以外的浏览器*/
         filter:alpha(opacity=0);    /*设置它的透明度为0，即完全透明。这个语句，对付IE浏览器。*/
        }
        .select {WIDTH: 500px;}
        .select DL {WIDTH: 720px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 600px;}
        
        .des_select 
        {
            border: solid 2px #DDF0DD;
            MARGIN: 0px 5px 5px 5px;
	        padding: 10px 20px 5px 30px;
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
	        padding: 10px 20px 5px 30px;
            CURSOR: pointer;
            overflow: hidden;
            font-size: 12px;
            FONT-WEIGHT: normal;
            FLOAT: left
        }
    </style>
</head>
<body>
<form id="form_data" onsubmit="return false;" method="post">
    <div class=inputdiv>
    专题类型：<select name="DropDownList1" id="DropDownList1" <%=setddl %> >
	<option value="Index_Sell">首页特价精选</option>
    <option value="Index_Season">首页当季推荐</option>
    <option value="Index_Outbound">首页出境游</option>
    <option value="Index_Inland">首页国内游</option>
    <option value="Index_Cruise">首页邮轮</option>
    <option value="Index_Freetour">首页自由行</option>
    <option value="Index_Visa">首页签证</option>
    <option value="Outbound_Hot">出境热销排行</option>
    <option value="Outbound_Sell">出境特价精选</option>
    <option value="Outbound_01">出境短线</option>
    <option value="Outbound_02">出境长线</option>
    <option value="Outbound_03">出境主题旅游</option>
    <option value="Inland_Hot">国内热销排行</option>
    <option value="Inland_Sell">国内特价精选</option>
    <option value="Inland_01">国内推荐目的地</option>
    <option value="Inland_02">国内主题旅游</option>
    <option value="Freetour_Hot">自由行热销排行</option>
    <option value="Freetour_Sell">自由行特价精选</option>
    <option value="Freetour_01">自由行出境短线</option>
    <option value="Freetour_02">自由行出境长线</option>
    <option value="Freetour_03">自由行国内热门</option>
    <option value="Cruise_Best">邮轮线路精选</option>
    <option value="Visa_All">所有签证</option>
    <option value="OTA">电商产品</option>
    </select>&nbsp; 排序：<input name="SortNum" id="SortNum" type="text" class="easyui-numberbox" precision="0" max="250" size="3" maxlength="3" style="text-align:center;"  value="<%=SortNum %>"/>
    </div>
    <div class="inputdiv">
    专题名称：<input name="Cname" type="text" id="Cname" style="width:300px;" value="<%=Cname %>"/>
    </div>
    <div class="inputdiv">
    链接地址：<input name="Url" type="text" id="Url" style="width:300px;" value="<%=Url %>"/>
    </div>
    <div class="inputdiv">
    线路类型：<input name="LineType" type="text" id="LineType" style="width:300px;" value="<%=LineType %>"/>
    </div><span style="padding-left:70px">畅游类型id用半角,隔开，例如：11,21,31</span>
    <div class=inputdiv>
     &nbsp;&nbsp;目的地：
        <input class=sel type="text" name="Destination" id="Destination" maxlength="10" style="width: 180px"/>
    </div>
    <div class=inputdiv>
        <div id="DestinationList" style="clear:both;width:500px;height:auto;max-height:240px;overflow-y:auto;"><%=DestinationInfos %></div>
    </div>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="stid" name="stid" type="hidden" value="<%=id %>"/>
        <input id="Line_Desid" name="Line_Desid" type="hidden" value="<%=Line_Desid %>"/>
        <input id="none" name="none" type="hidden" />
        <input id="Destinationid" name="Destinationid" type="hidden" />
        <input id="FirstDestination" name="FirstDestination" type="hidden" value=""/>
        <input id="DestinationName" name="DestinationName" type="hidden" value=""/>
        <input id="Des_List" name="Des_List" type="hidden" />
    </DIV>
</form>
<div class=inputdiv>
        <a id="SaveInfo" onclick="SaveAllInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
    </div>
    
    <SPAN class=clr></SPAN>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#DropDownList1").val("<%=Types%>");
        })

        function resetit() {
            $("#Cname").val("");
            $("#SortNum").val("");
            $("#stid").val("");
        }
    
        $('#Destination').bind('keyup', function () {
            var url = "../Common/GetAutoDropList.aspx?action=JournalDestination&SerchName=" + encodeURI(this.value);
            if (this.value.length > 1) show(this, "Destinationid", url, "yes");
        });

        function afterselet(callback) {
            $('#DestinationList').show();
            var haveit = 0;
            var des_id = $("#Destinationid").val();
            $("#DestinationList div").each(function () {
                var pid = $(this).attr("tag");
                if (pid == des_id) {
                    $(this).attr({ "class": "des_select" });
                    haveit = 1;
                }
            });
            if (haveit == 0) {
                var str = "<div class=des_select tag=" + des_id + ">";
                str += $("#Destination").val();
                str += "</div>";
                $("#DestinationList").append(str);
            }
            $("#Destination").val("");
            $("#Destination").focus();
        }

        $("#DestinationList div").live("click", function () {
            if ($(this).attr("class") == "des_select") {
                $(this).attr({ "class": "des_deselect" });
            }
            else {
                $(this).attr({ "class": "des_select" });
            }
        })

        var FirstDesId = "0";
        var DesId = "";
        var DesName = "";
        var DesList = "";

        function getDestion() {
            DesId = ",";
            DesName = ",";
            DesList = "";
            FirstDesId = "0";
            $("#DestinationList div").each(function () {
                if ($(this).attr("class") == "des_select") {
                    DesId += $(this).attr("tag") + ",";
                    DesName += $(this).html() + ",";
                    DesList += "<div class=des_select tag=" + $(this).attr("tag") + ">";
                    DesList += $(this).html();
                    DesList += "</div>";
                    if (FirstDesId == '0') {
                        FirstDesId = $(this).attr("tag");
                    }
                }
            });
        }


        function SaveAllInfo() {
            getDestion();
            $("#Destinationid").val(DesId);
            $("#DestinationName").val(DesName);
            $("#Des_List").val(DesList);
            if ($("#Cname").val() == "" ) {
                jError('<strong>请输入专题名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }
            var url = "AjaxService.aspx?action=SaveSpecialTopic&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("保存成功");
                    if ($("#stid").val() == "") resetit();
                }
                else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });

        }
    </script> 
</body>
</html>