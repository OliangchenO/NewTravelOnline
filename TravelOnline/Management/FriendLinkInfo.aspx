<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLinkInfo.aspx.cs" Inherits="TravelOnline.Management.FriendLinkInfo" %>
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
    <%--<style>
        .select {WIDTH: 720px;}
        .select DL {WIDTH: 720px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 600px;}
    </style>--%>
    <script type="text/javascript">
        function myrefresh() {
            window.location.reload();
        }

        $(document).ready(function () {
            $("#Select1").val("<%=LinkType%>");
        })
	</script>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="myrefresh()" class="tools <%=hide %>"><img src="../images/icon/add.png" class=img20>新增</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>分类：</div>
            <select name="Select1" id="Select1" style="width:150px;">
                <option value="1">首页</option>
	            <option value="2">出境</option>
	            <option value="3">国内</option>
	            <option value="4">自由行</option>
	            <option value="5">邮轮</option>
	            <option value="6">签证</option>
            </select>
    </div>
    <div class=line_input>
        <div class=firstinput>链接名称：</div>
        <input class=ipt id="LinkName" name="LinkName" type="text" style="width: 300px;" maxlength="30" value="<%=LinkName %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>链接地址：</div>
        <input class=ipt id="LinkUrl" name="LinkUrl" type="text" style="width: 300px;" maxlength="150" value="<%=LinkUrl %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>排序号：</div>
        <input class=ipt id="rankid" name="rankid" type="text" style="width: 60px;" maxlength="6" value="<%=rankid %>"/>&nbsp;&nbsp;数字越大，则排序越靠前
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#typename').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=InitData&SerchName=RoomType";
            show(this, "typeid", url, "");
        });

        function SaveInfo() {
            if ($("#LinkName").val() == "" || $("#LinkUrl").val() == "") {
                jNotify('<strong>链接名称和链接地址都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            //data
            if ($("#LinkUrl").val().indexOf("http://")<0) {
                jNotify('<strong>链接地址必须为：http:// 格式!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=FriendLink&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success == 0) {
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