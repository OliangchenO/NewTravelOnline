<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="TravelOnline.Management.UserInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style>
        .select {WIDTH: 310px;}
        .select DL {WIDTH: 300px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 200px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <DIV id="inputs" style="DISPLAY:none">
        <input id="UserId" type="hidden" value="<%=id %>"/>
        <input id="OldPassWord" type="hidden" value="<%=OldPassWord %>"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>后台管理用户</STRONG></DIV>
    <DL class=fore><DT>登录账号：</DT>
        <DD>
            <asp:TextBox ID="TB_LoginName" class="text" runat="server" Width="150"></asp:TextBox>  
        </DD>
    </DL>
    <DL><DT>用户姓名：</DT>
        <DD>
            <asp:TextBox ID="TB_UserName" runat="server" Width="150"></asp:TextBox>
        </DD>
    </DL>
    <DL><DT>登陆密码：</DT>
        <DD>
            <asp:TextBox ID="TB_Password" runat="server" Width="150"></asp:TextBox>
        </DD>
    </DL>
    <DL><DT>模块权限：</DT>
        <DD>
            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="RightName" DataValueField="id" Width="158"></asp:DropDownList>
        </DD>
    </DL>
    <DL><DT>隶属部门：</DT>
        <DD>
            <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="DeptName" DataValueField="id" Width="158"></asp:DropDownList>
        </DD>
    </DL>
    <DL><DT></DT>
        <DD>
           
        </DD>
    </DL>
     <a id="SaveInfo" onclick="allchks()" class="easyui-linkbutton" plain="true" 
            iconCls="icon-save" style="margin-left: 180px;margin-top: 10px;">保存</a>
    </DIV>
    </form>
    <script type="text/javascript">
        function allchks() {
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            if ($("#TB_LoginName").val() == "") {
                jNotify('<strong>请输入登录账号!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#TB_UserName").val() == "") {
                jNotify('<strong>请输入用户姓名!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#OldPassWord").val() == "" && $("#TB_Password").val() == "") {
                jNotify('<strong>请输入登陆密码!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#UserId").val().length > 10) {
                SaveUserInfo()
            }
            else {

                var url = "AjaxService.aspx?action=CheckUser&LoginName=" + escape($("#TB_LoginName").val()) + "&r=" + Math.random();
                $.getJSON(url, function (date) {
                    if (date.success != 0) {
                        jError('<strong>登录账号已存在!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                        return false;
                    }
                    else {
                        SaveUserInfo()
                    }
                })
            
            }
        }

        function SaveUserInfo() { 
            var url = "AjaxService.aspx?action=SaveUser&Id=" + $("#UserId").val() + "&UserRight=" + $("#DropDownList1").val() + "&UserDept=" + $("#DropDownList2").val() + "&LoginName=" + escape($("#TB_LoginName").val()) + "&UserName=" + escape($("#TB_UserName").val()) + "&LoginPassWord=" + escape($("#TB_Password").val()) + "&OldPassWord=" + $("#OldPassWord").val() + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    if ($("#RightId").val() == "0") $("#TextBox1").val("");
                } else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }
    </script> 
</body>
</html>
