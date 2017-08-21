<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesVisitAdd.aspx.cs" Inherits="TravelOnline.Cruises.CruisesVisitAdd" %>
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
        <input name="days" id="days" type="hidden" value="<%=days %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="myrefresh()" class="tools <%=hide %>"><img src="../images/icon/add.png" class=img20>新增</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>观光类型：</div>
        <input value="<%=vtitle %>" class=sel type="text" name="vtitle" id="vtitle" maxlength="50" readonly="readonly" style="width: 300px"/>&nbsp;&nbsp;
        销售价格：<input id="price" name="price" type="text" class="ipt easyui-numberbox" precision="0" max="999999" style="width: 65px;text-align:center;" maxlength="6" value="<%=price %>"/>&nbsp;&nbsp;
        人数：<input id="nums" name="nums" type="text" class="ipt easyui-numberbox" precision="0" max="9999" style="width: 40px;text-align:center;" maxlength="4" value="<%=nums %>"/>

    </div>
    <div class=line_input>
        <div class=firstinput>观光名称：</div>
        <input class=ipt id="visitname" name="visitname" type="text" style="width: 562px;" maxlength="100" value="<%=visitname %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>游览景点：</div>
        <input class=ipt id="vmemo" name="vmemo" type="text" style="width: 562px;" maxlength="100" value="<%=vmemo %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>游览日期：</div>
        <input class=ipt id="vdate" name="vdate" type="text" style="width: 100px;" maxlength="50" value="<%=vdate %>"/>&nbsp;&nbsp;
        停留时间：<input class=ipt id="stay" name="stay" type="text" style="width: 150px;" maxlength="50" value="<%=stay %>"/>&nbsp;&nbsp;
        游览时间：<input class=ipt id="sight" name="sight" type="text" style="width: 150px;" maxlength="50" value="<%=sight %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>用餐：</div>
        <input class=ipt id="dinner" name="dinner" type="text" style="width: 562px;" maxlength="50" value="<%=dinner %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>行程介绍：</div>
        <textarea class=iptm name="intro" id="intro" style="width: 562px;height:100px"><%=intro%></textarea>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#vtitle').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=CruisesVisit&SerchName=" + $("#lineid").val();
            show(this, "days", url, "");
        });

        function SaveInfo() {
            if ($("#days").val() == "" || $("#visitname").val() == "") {
                jNotify('<strong>观光类型和名称都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#price").val() == "") {
                jNotify('<strong>销售价格不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            
            var url = "AjaxService.aspx?action=CruisesVisit&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        $("#visitname").val("");
                        $("#price").val("");
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