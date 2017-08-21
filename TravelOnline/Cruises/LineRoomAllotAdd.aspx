<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineRoomAllotAdd.aspx.cs" Inherits="TravelOnline.Cruises.LineRoomAllotAdd" %>
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
        .firstinput {text-align: right;width:100px;FLOAT: left;}
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="shipid" id="shipid" type="hidden" value="<%=shipid %>"/>
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
        <input name="roomid" id="roomid" type="hidden" value="<%=roomid %>"/>
        <input name="companyid" id="companyid" type="hidden" value="<%=companyid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>分销商：</div>
        <input value="<%=company %>" class=sel type="text" name="company" id="company" maxlength="50" style="width: 300px"/>
    </div>
    <div class=line_input>
        <div class=firstinput>销售类型：</div>
        <select id="allotflag" name="allotflag"  style="width: 108px">
            <option selected="selected" value="0">公开销售</option>
            <option value="1">分销商销售</option>
        </select>&nbsp;&nbsp;&nbsp;&nbsp;
        销售状态：
        <select id="sellflag" name="sellflag"  style="width: 108px">
            <option selected="selected" value="0">正常销售</option>
            <option value="1">暂停销售</option>
        </select>
    </div>
    <div class=line_input>
        <div class=firstinput>销售间数：</div>
        <input id="nums" name="nums" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 100px;text-align:center;" maxlength="3" value="<%=nums %>"/>&nbsp;&nbsp;&nbsp;&nbsp;
        推荐状态：
        <select id="recommend" name="recommend"  style="width: 108px">
            <option selected="selected" value="0">无推荐</option>
            <option value="1">买一送一</option>
            <option value="2">特价销售</option>
            <option value="3">限时促销</option>
	        <option value="4">第三人免船票</option>
            <option value="5">已满员</option>
            <option value="6">超值换购</option>
        </select>
    </div>
    <div class=line_input>
        <div class=firstinput>第1、2人价格：</div>
        <input id="price" name="price" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=price %>"/>&nbsp;&nbsp;&nbsp;&nbsp;
        同行返利：
        <input id="rebate" name="rebate" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=rebate %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>第3人成人价格：</div>
        <input id="thirdprice" name="thirdprice" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=thirdprice %>"/>&nbsp;&nbsp;&nbsp;&nbsp;
         同行返利：
        <input id="thirdrebate" name="thirdrebate" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=thirdrebate %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>第3人儿童价格：</div>
        <input id="childprice" name="childprice" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=childprice %>"/>&nbsp;&nbsp;&nbsp;&nbsp;
         同行返利：
        <input id="childrebate" name="childrebate" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 100px;text-align:center;" maxlength="6" value="<%=childrebate %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>注意：</div>
        修改时，仅可修改价格和销售间数、同行返利
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#allotflag").val("<%=allotflag%>");
            $("#sellflag").val("<%=sellflag%>");
            $("#recommend").val("<%=recommend%>");
        });

        $('#company').bind('keyup', function () {
            $("#companyid").val("");
            var url = "../Common/GetAutoDropList.aspx?action=CompanyInfo&SerchName=" + encodeURI(this.value);
            if (this.value.length > 2) show(this, "companyid", url, "");
            $("#allotflag").val("1");
        });

        function SaveInfo() {
            if ($("#allotflag").val() == "1") {
                if ($("#companyid").val() == "") {
                    jError('<strong>您选择了分销商销售，分销商必须选择!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return;
                }
            }
            else {
                if ($("#companyid").val() != "") {
                    jError('<strong>您选择了公开销售，分销商不能选择!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return;
                }
            }

            if ($("#price").val() == "" || $("#nums").val() == "") {
                jNotify('<strong>价格和销售数量都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=LineRoomAllot&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        $("#price").val("");
                        $("#nums").val("");
                        $("#thirdprice").val("");
                        $("#rebate").val("");
                        parent.$('#GridView_Serch_Button').click();
                    }
                    else {
                        parent.$('#GridView_Refresh_Button').click();
                    }
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

