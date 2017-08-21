<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityManage.aspx.cs" Inherits="TravelOnline.Activity.ActivityManage" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>活动管理</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="js/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="js/EasyUI/themes/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="../Scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="js/header.js"></script>
    <script type="text/javascript" src="js/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="js/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="js/jNotify.jquery.js"></script>
</head>
<body>
    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <div class="w main">

        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

<DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>活动中心</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>活动信息设置</STRONG>
    </DIV>
    <div class="serchbar">
    活动主题：<asp:textbox runat="server" ID="txbAct_Name"></asp:textbox>
    活动报名时间：<asp:TextBox ID="TB_Bdate" class="iconDate" runat="server" Width="85"></asp:TextBox>-
        <asp:TextBox ID="TB_Edate" class="iconDate" runat="server" Width="85"></asp:TextBox>
    活动状态：<asp:DropDownList ID="ddlAct_MainStart" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlAct_MainStart_SelectedIndexChanged">
            <asp:ListItem Selected="True">正常</asp:ListItem>
            <asp:ListItem>禁用</asp:ListItem></asp:DropDownList>
            <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查&nbsp;询</a>
    </div>
    <div class="toolbar">
        <a id="AddNew" href="#" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DeleteSelect()">
            <asp:Label ID="lblDisable" runat="server" Text="Label">禁用</asp:Label></a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>
        <DIV id="inputs" style="DISPLAY:none">
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="20" 
                    onpageindexchanging="GridView_PageIndexChanging" 
                    OnRowDataBound="GridView1_RowDataBound" GridLines="Vertical" 
                    SortExpression="ActInfoMain_ID" SortDirection="ASC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "ActInfoMain_ID") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号" Visible="False">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ActInfoMain_ID" HeaderText="活动ID" 
                            SortExpression="ActInfoMain_ID">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActivityName" HeaderText="名称" SortExpression="ActivityName">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActivityStartTime" HeaderText="开始时间" SortExpression="ActivityStartTime" DataFormatString="{0:yy-MM-dd}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActivityEndTime" HeaderText="结束时间" SortExpression="ActivityEndTime" DataFormatString="{0:yy-MM-dd}">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MaxNum" HeaderText="最大报名数" SortExpression="MaxNum">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MinNum" HeaderText="最少报名数" SortExpression="MinNum">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JoinNum" HeaderText="现报名数" SortExpression="JoinNum">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Start" HeaderText="状态" SortExpression="Start">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle />
                        </asp:TemplateField>
	                </Columns>
                    <PagerTemplate>
                        <div class="pagination"><span class="pagecounts">
                            第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;&nbsp;
                            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="首页"><img src="/images/icons/pagination_first.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="上一页"><img src="/images/icons/pagination_prev.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="下一页"><img src="/images/icons/pagination_next.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="尾页"><img src="/images/icons/pagination_last.gif" alt="" /></asp:LinkButton>&nbsp;&nbsp;
                                到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
                            <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
                    </PagerTemplate>
	                <PagerStyle BackColor="#F7F7F7" ForeColor="#333333" HorizontalAlign="right" Font-Size="12px" />
	                <HeaderStyle BackColor="#4BA41B" Font-Bold="True" ForeColor="White" />
	                <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </DIV>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView_Refresh_Button" />
            <asp:AsyncPostBackTrigger ControlID="GridView_Serch_Button" />
        </Triggers>
        </asp:UpdatePanel>
        
    </form>
</DIV>
<DIV class=clr></DIV></DIV>
    <span class="clr"></span>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#AddNew').dialog({ id: 'No1', page: 'Act_Info.aspx', title: '增加活动信息', width: 590, height: 580, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
        });

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

            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            alert(arrChk);
        }
        $(function () {
            $('#TB_Bdate').calendar({ maxDate: '#TB_Edate', btnBar: false });
            $('#TB_Edate').calendar({ minDate: '#TB_Bdate', btnBar: false });
        });

        function DeleteSelect() {
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            if (items.length == 0) {
                jNotify('<strong>请选择要取消的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if (confirm('确认要取消吗？')) {
            }
            else
            { return false; }
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            arrChk = arrChk.substr(0, arrChk.length - 1);
            var url = "AjaxService.aspx?action=DisableActInfoMain&ActInfoMain_ID=" + arrChk + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息取消成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>信息取消失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }
</script>
</body>
</html>
