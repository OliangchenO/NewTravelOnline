<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesRebate.aspx.cs" Inherits="TravelOnline.Cruises.CruisesRebate" %>
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
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <style>
        .runcode{border:1px solid #ddd;background:url(/images/iconDate.gif) center right no-repeat #f7f7f7;cursor:pointer;font:12px tahoma,arial;height:21px;width:150px;}
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
        <input name="roomid" id="roomid" type="hidden" value="<%=roomid %>"/>
        <input name="allotid" id="allotid" type="hidden" value="<%=allotid %>"/>
        <input name="flag" id="flag" type="hidden" value="<%=flag %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a id=nextstep href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>优惠类型：</div>
        <select name="Rebateflag" id="Rebateflag" style="width:110px;">
	        <option value="0">每间舱房优惠</option>
	        <option value="1">每人观光优惠</option>
        </select>
    </div>
    <div class=line_input>
        <div class=firstinput>报名日期：</div>
        <input value="<%=begindate %>" class="runcode ipt" type="text" name="begindate" id="begindate" maxlength="10" style="width: 100px" readonly="readonly" />&nbsp;&nbsp; 
        至&nbsp; <input value="<%=enddate %>" class="runcode ipt" type="text" name="enddate" id="enddate" maxlength="10" style="width: 100px" readonly="readonly" />
    </div>
    <div class=line_input>
        <div class=firstinput>优惠金额：</div>
        <input id="price" name="price" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=price %>"/>
        舱房优惠每间，观光优惠每人
    </div>
    <div class=line_input>
        <div class=firstinput>优惠说明：<br>100字以内&nbsp;</div>
        <textarea name="infos" id="infos" cols="" rows="" style="width: 300px;height:60px"><%=infos%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>注意：</div>
        批量增加时，所有已分配房型都将增加此政策
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Rebateflag").val("<%=Rebateflag%>");
            $('#begindate').calendar({ maxDate: '#enddate', btnBar: false });
            $('#enddate').calendar({ minDate: '#begindate', btnBar: false });
        });

        function SaveInfo() {
            if ($("#price").val() == "" || $("#infos").val() == "") {
                jNotify('<strong>价格和优惠说明都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            $("#nextstep").hide();
            var url = "AjaxService.aspx?action=CruisesRebate&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                $("#nextstep").show();
                var obj = eval(data);
                if (obj.success) {
                    $("#price").val("")
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


