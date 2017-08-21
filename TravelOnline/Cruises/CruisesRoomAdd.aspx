<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesRoomAdd.aspx.cs" Inherits="TravelOnline.Cruises.CruisesRoomAdd" %>
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
        <input name="comid" id="comid" type="hidden" value="<%=comid %>"/>
        <input name="shipid" id="shipid" type="hidden" value="<%=shipid %>"/>
        <input name="typeid" id="typeid" type="hidden" value="<%=typeid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="myrefresh()" class="tools <%=hide %>"><img src="../images/icon/add.png" class=img20>新增</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
        
    <div class=line_input>
        <div class=firstinput>舱房名称：</div>
        <input class=ipt id="roomname" name="roomname" type="text" style="width: 562px;" maxlength="100" value="<%=roomname %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>床位配置：</div>
        <input class=ipt id="configure" name="configure" type="text" style="width: 562px;" maxlength="100" value="<%=configure %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>舱房类型：</div>
        <input value="<%=typename %>" class=sel type="text" name="typename" id="typename" maxlength="50" readonly="readonly" style="width: 130px"/>&nbsp;&nbsp;
        舱房编码：<input class=ipt id="roomcode" name="roomcode" type="text" style="width: 60px;" maxlength="10" value="<%=roomcode %>"/>&nbsp;&nbsp;
        舱房间数：<input id="rooms" name="rooms" type="text" class="ipt easyui-numberbox" precision="0" max="999" style="width: 56px;text-align:center;" maxlength="3" value="<%=rooms %>"/>&nbsp;&nbsp;
        最大可住：<input id="berth" name="berth" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 56px;text-align:center;" maxlength="2" value="<%=berth %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>房间面积：</div>
        <input class=ipt id="area" name="area" type="text" style="width: 150px;" maxlength="50" value="<%=area %>"/>&nbsp;&nbsp;
        所在甲板：<input class=ipt id="deck" name="deck" type="text" style="width: 140px;" maxlength="50" value="<%=deck %>"/> 填入数字用半角 , 隔开，例如2,3,5
    </div>
    <div class=line_input>
        <div class=firstinput>设施简介：</div>
        <textarea class=iptm name="intro" id="intro" style="width: 562px;height:100px"><%=intro%></textarea>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#typename').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=InitData&SerchName=RoomType";
            show(this, "typeid", url, "");
        });

        function SaveInfo() {
            if ($("#comid").val() == "" || $("#shipid").val() == "") {
                jNotify('<strong>邮轮公司和船队都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#typeid").val() == "" || $("#roomname").val() == "") {
                jNotify('<strong>舱房类型和名称都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#roomcode").val() == "" || $("#berth").val() == "") {
                jNotify('<strong>编码和最大可住人数都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=CruisesRoom&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
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