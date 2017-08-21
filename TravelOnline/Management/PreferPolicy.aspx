<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferPolicy.aspx.cs" Inherits="TravelOnline.Management.PreferPolicy" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>优惠券管理</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript">

	</script>
</head>
<body id="none">

    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <DIV class="w main">
        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

<DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>优惠券管理</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>优惠券设置</STRONG>
    </DIV>
    <div class="serchbar">
        类型：<asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
            <asp:ListItem Value="全部">全部</asp:ListItem>
            <asp:ListItem Value="1">赠送</asp:ListItem>
            <asp:ListItem Value="2">销售</asp:ListItem>
            <asp:ListItem Value="3">公共券号</asp:ListItem>
            <asp:ListItem Value="4">邮轮领券</asp:ListItem>
        </asp:DropDownList>
        &nbsp;抵扣：<asp:DropDownList ID="DropDownList2" runat="server" Width="60px">
            <asp:ListItem Value="全部">全部</asp:ListItem>
            <asp:ListItem Value="1">整单</asp:ListItem>
            <asp:ListItem Value="2">每人</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        范围：<asp:DropDownList ID="DropDownList3" runat="server" Width="80px">
            <asp:ListItem Value="1">全部</asp:ListItem>
            <asp:ListItem Value="2">出境</asp:ListItem>
            <asp:ListItem Value="3">国内</asp:ListItem>
            <asp:ListItem Value="4">单项</asp:ListItem>
            <asp:ListItem Value="5">邮轮</asp:ListItem>
            <asp:ListItem Value="8">线路类型</asp:ListItem>
            <asp:ListItem Value="9">指定产品</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        
    </div>
    <div class="toolbar">
        <a id="AddNew" href="###" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DeleteSelect()">删除</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>
        <DIV id="inputs" style="DISPLAY:none">
            <input id="TB_DeptId" type="hidden" value="0"/>
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="20" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="id" SortDirection="DESC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="类型">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="6%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="抵扣">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="范围">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="sellprice" HeaderText="售价" SortExpression="sellprice">
		                    <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="par" HeaderText="面值" SortExpression="par">
		                    <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="begindate" HeaderText="开始日期" SortExpression="begindate" DataFormatString="{0:yy-MM-dd}">
		                    <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="enddate" HeaderText="结束日期" SortExpression="enddate" DataFormatString="{0:yy-MM-dd}">
		                    <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ff" HeaderText="发放" SortExpression="ff">
		                    <HeaderStyle Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sy" HeaderText="使用" SortExpression="sy">
		                    <HeaderStyle Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="操作人" SortExpression="UserName">
		                    <HeaderStyle Width="7%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="8%" />
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
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#AddNew').dialog({ id: 'No1', page: 'PreferInfo.aspx', title: '增加优惠券', width: 430, height: 650, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
        });

        function EditInfo(id) {
            var url = "PreferInfo.aspx?id=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '修改优惠券', width: 430, height: 650, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
            dg.ShowDialog();
        }

        function ExtendTicket(id) {
            var url = "PreferSend.aspx?id=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '优惠券赠送发放', width: 430, height: 520, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
            dg.ShowDialog();
        }

        function SaveInfo() {
            if ($("#TB_DeptName").val() == "") {
                jNotify('<strong>请输入部门名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=SaveDept&Id=" + $("#TB_DeptId").val() + "&DeptName=" + escape($("#TB_DeptName").val()) + "&ErpId=" + $("#TB_ErpId").val() + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    AddNew();
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

        function chkall(obj) {
            if (obj.checked) {
                $("input[name$='CheckBox']").each(function () { this.checked = true; });
            } else {
                $("input[name$='CheckBox']").each(function () { this.checked = false; });
            }
        }

        function allchks() {

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
            $("input[name$='CheckBox']:checked").each(function () { arrChk += "'" + this.value + "',"; });
            arrChk = arrChk.substr(0, arrChk.length - 1);
            var url = "AjaxService.aspx?action=DeletePreferInfos&Id=" + arrChk + "&DbTableName=OL_Dept&r=" + Math.random();
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
</body>
</html>
