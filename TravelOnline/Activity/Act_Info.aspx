<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Act_Info.aspx.cs" Inherits="TravelOnline.Activity.Act_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <style>
        .select {WIDTH: 540px;}
        .inputdiv {PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 10px; PADDING-RIGHT: 0px; PADDING-TOP: 5px}
        table {border: solid 1px #e8eef4;border-collapse: collapse;}
        table td{border: solid 1px #e8eef4;}
        table th{text-align: left;background-color: #e8eef4;border: solid 1px #e8eef4;}
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
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="inputdiv">
        <div style="display:none">
        <input id="ActInfoMain_ID" type="hidden" value="<%=ActInfoMain_ID %>"/>
        </div>
        <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>活动信息</STRONG></DIV>
    </DIV>
        <table width="100%">
        <tr>
            <td>
            <strong>活动主题名称：</strong>
            </td>
            <td>
            <asp:TextBox ID="txbAct_Name" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            <strong>活动有效期：</strong>
            </td>
            <td>
            <input class="Wdate" 
                    onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd HH:mm:ss'})" 
                    style="width:150px" type="text" id="more_search_date" runat="server"/>&nbsp;～&nbsp;<input class="Wdate" 
                    onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd HH:mm:ss'})" 
                    style="width:180px" type="text" id="more_search_dateend" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>活动状态：</strong></td>
            <td>
                <asp:DropDownList ID="ddlStart" runat="server">
                    <asp:ListItem>正常</asp:ListItem>
                    <asp:ListItem>禁用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><strong>报名数限制：</strong></td>
            <td>
                <asp:TextBox ID="txbMinNum" runat="server" MaxLength="3" Width="30px">0</asp:TextBox>
&nbsp;~
                <asp:TextBox ID="txbMaxNum" runat="server" MaxLength="3" Width="30px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><strong>年龄限制：</strong></td>
            <td>
                <asp:TextBox ID="txbMinAge" runat="server" Width="30px">0</asp:TextBox>
&nbsp;~
                <asp:TextBox ID="txbMaxAge" runat="server" MaxLength="3" Width="30px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            <strong>活动时间：</strong>
            </td>
            <td>
            <input class="Wdate" 
                    onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd HH:mm:ss'})" 
                    style="width:150px" type="text" id="txbActSTime" runat="server"/>&nbsp;~
                <input class="Wdate" 
                    onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy/MM/dd HH:mm:ss'})" 
                    style="width:180px" type="text" id="txbActETime" runat="server" /></td>
        </tr>
        <tr>
            <td>
            <strong>活动地点：</strong>
            </td>
            <td>
            <asp:TextBox ID="txbPlace" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr id="ActList_Info">
            <td colspan="2">
                <div id="Div_Act_List">
                </div>
            </td>
        </tr>
        <tr>
        <td colspan="2" align="right">
        <a id="SaveInfo" onclick="SaveAllInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
        <a id="A1" onclick="GoToUrl()" class="easyui-linkbutton" plain="true" iconCls="icon-redo">查看链接</a>
        </td>
        </tr>
    </table>
    </div>

    <script type="text/javascript">
        function resetit() {
            $("#txbAct_Name").val("");
            $("#more_search_date").val("");
            $("#more_search_dateend").val("");
        }
        function SaveAllInfo() {
            if ($("#txbAct_Name").val() == "") {
                jNotify('<strong>请输入活动名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#more_search_date").val() == "") {
                jNotify('<strong>请输入活动开始日期!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#more_search_dateend").val() == "") {
                jNotify('<strong>请输入活动结束日期!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#txbMinNum").val() == "") {
                jNotify('<strong>请输入最少报名数!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#txbMaxNum").val() == "") {
                jNotify('<strong>请输入最大报名数!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#txbMinAge").val() == "") {
                jNotify('<strong>请输入最小年龄!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#txbMaxAge").val() == "") {
                jNotify('<strong>请输入最大年龄!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=SaveAct_Add&ActInfoMain_ID=" + $("#ActInfoMain_ID").val()
            + "&ActivityName=" + escape($("#txbAct_Name").val())
            + "&ActivityStartTime="+ $("#more_search_date").val() 
            + "&ActivityEndTime=" + $("#more_search_dateend").val()
            + "&Start="  + escape($("#ddlStart").val())
            + "&MinNum=" + $("#txbMinNum").val()
            + "&MaxNum=" + $("#txbMaxNum").val()
            + "&MinAge=" + $("#txbMinAge").val()
            + "&MaxAge=" + $("#txbMaxAge").val()
            + "&Place="  + escape($("#txbPlace").val())
            + "&ActivityRunSTime=" + $("#txbActSTime").val()
            + "&ActivityRunETime=" + $("#txbActETime").val() 
            + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    resetit();
                }
                else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

        function EditInfo(ActInfoMain_ID) {
            var url = "Act_Info.aspx?ActInfoMain_ID=" + ActInfoMain_ID;
            //alert("aa");
            var dg = new $.dialog({ id: 'No1', page: url, title: '修改活动信息', width: 590, height: 580, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
            dg.ShowDialog();
        }

        function chkall(obj) {
            if (obj.checked) {
                $("input[name$='CheckBox']").each(function () { this.checked = true; });
            } else {
                $("input[name$='CheckBox']").each(function () { this.checked = false; });
            }
        }

        function allchks() {
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            //获取name为check的一组元素,然后选取它们中选中(checked)的。
            if (items.length == 0) {
                return false;
            }
            //alert("选中的个数为：" + items.length)
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            alert(arrChk);
        }
        

        function DeleteSelect() {
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            if (items.length == 0) {
                jNotify('<strong>请选择要删除的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if (confirm('确认要删除吗？')) {
            }
            else
            { return false; }
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            arrChk = arrChk.substr(0, arrChk.length - 1);
            var url = "AjaxService.aspx?action=DeleteAdInfos&ActInfoMain_ID=" + arrChk + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息删除成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>信息删除失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }
    </script> 
    </form>
</body>
</html>
