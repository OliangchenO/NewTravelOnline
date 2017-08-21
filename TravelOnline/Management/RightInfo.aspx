<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RightInfo.aspx.cs" Inherits="TravelOnline.Management.RightInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style>
        .select {WIDTH: 620px;visibility:hidden;position:fixed}
        .select DL {WIDTH: 630px;}
        .select DT {WIDTH: 120px;}
        .select DD {WIDTH: 510px;}
        .activity {visibility:visible}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px; width: 600px;">
    权限类型：<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="Menu">模块操作权限</asp:ListItem>
            <asp:ListItem Value="Operation">业务类型权限</asp:ListItem>
        </asp:DropDownList>
    &nbsp;&nbsp;权限名称：<asp:TextBox ID="TextBox1" runat="server" Width="220px"></asp:TextBox>&nbsp;&nbsp;
        <a id="SaveInfo" onclick="allchks()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="RightId" type="hidden" value="<%=id %>"/>
    </div>
    <span class="clr"></span>
    <div id="select1" class="m select activity"><%=MenuCheckBoxString %></div>
    <div id="select2" class="m select"><%=OperationCheckBoxString %></div>
    </form>
    <script type="text/javascript">
        $(function () {
            $('#DropDownList1').change(function () {
                if ($('#DropDownList1').val() == 'Operation') {
                    $("#select1").removeClass("activity");
                    $("#select2").addClass("activity");
                } else {
                    $("#select2").removeClass("activity");
                    $("#select1").addClass("activity");
                }
            });
        });

       function allchks() {
           var arrChk = "";
           var items = $("input[name='CheckBox']:checked");
            if ($("#TextBox1").val() == "") {
                jNotify('<strong>请输入权限名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if (items.length == 0) {
                jNotify('<strong>请勾选权限明细!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
           
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value; });
            arrChk = "," + arrChk;
            var url = "AjaxService.aspx?action=SaveUserRight&Id=" + $("#RightId").val() + "&RightFlag=Menu" + "&RightName=" + escape($("#TextBox1").val()) + "&RightCode=" + arrChk + "&r=" + Math.random();
            
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
