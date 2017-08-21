<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageuser.aspx.cs" Inherits="TravelOnline.Test.manageuser" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台管理用户信息</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css">
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css">
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js"></script>
    <script>
        $(function () {
            $('#AddNew').dialog({ id: 'test10', page: 'http://www.qq.com', link: true, width: 800, height: 600, title: 'QQ首页' }); //dialog({ id: 'test14', page: 'ManageHome.aspx', title: '增加后台管理新用户', autoSize: true, skin: 'aero', iconTitle: false, rang: true, maxBtn: false, cover: true});
        });

        function enable() {
            $('a.easyui-linkbutton').linkbutton('enable');
        }
        function disable() {
            $('a.easyui-linkbutton').linkbutton('disable');
        }
        function changetext() {
            //$("input[name='CheckBox'][checked]");
            $("input[name='CheckBox']").attr("checked");
        }
	</script>
    
</head>
<body id="none">

    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Scripts/hotwords.js"></script>
    <DIV class="w main">

        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

    <DIV class=crumb>
        <A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>登录用户设置</SPAN>
    </DIV>

 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>登录用户设置</STRONG>
    </DIV>
    <form id="formlogin" runat="server">
    <div class="toolbar">
        <a id="AddNew" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
		<a class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="enable()">删除</a>
		<a class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#Button1').click();">刷新</a>
		<a class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a><input id="Button2" type="button" value="button" />
        <DIV id="inputs" style="DISPLAY:none">
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        </DIV>
	</div>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                        Width="822px" AllowPaging="True" PageSize="20"  GridLines="Vertical" 
                    onpageindexchanging="GridView_PageIndexChanging" 
                    OnDataBound="GridView_DataBound" BorderColor="White">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id=CheckBox<%#Container.DataItemIndex+1 %> type=checkbox name=CheckBox value=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField> 
		                <asp:BoundField DataField="UserName" HeaderText="登录用户名" SortExpression="UserName">
		                    <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="所属部门" SortExpression="UserName">
		                    <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="操作权限" SortExpression="UserName">
		                    <HeaderStyle Width="10%" />
                        </asp:BoundField>
		                <asp:BoundField DataField="UserName" HeaderText="AGE" SortExpression="UserName"  
                            HeaderStyle-Width="15%" >
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                            
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
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
            <asp:AsyncPostBackTrigger ControlID="Button1" />
        </Triggers>
        </asp:UpdatePanel>
        
    </form>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        //        checkbox 全选
        //        $(document).ready(function () {
        //            $("#chk_all").click(function () {
        //                if (this.checked) {
        //                    $("input[name$='CheckBox']").each(function () { this.checked = true; });
        //                } else {
        //                    $("input[name$='CheckBox']").each(function () { this.checked = false; });
        //                }
        //            });
        //        });

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
                alert("bbb");
                return false;
            }
            alert("选中的个数为：" + items.length)
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            alert(arrChk);
        }
        
</script> 
</body>
</html>
 <%--<PagerTemplate>
    <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label> &nbsp;  &nbsp; 
        <asp:LinkButton ID="lbnFirst" class="easyui-linkbutton" plain="true" runat="Server" Text="首页"  CommandName="Page" CommandArgument="First" ><img src="/images/icons/pagination_first.gif" alt="" /></asp:LinkButton>
    <asp:LinkButton ID="lbnPrev" class="easyui-linkbutton" plain="true" runat="server"  CommandName="Page" CommandArgument="Prev"  ><img src="/images/icons/pagination_prev.gif" alt="" /></asp:LinkButton>
    <asp:LinkButton ID="lbnNext" class="easyui-linkbutton" plain="true" runat="Server" Text="下一页" CommandName="Page" CommandArgument="Next" ><img src="/images/icons/pagination_next.gif" alt="" /></asp:LinkButton>
        <asp:LinkButton ID="lbnLast" class="easyui-linkbutton" plain="true" runat="Server" Text="尾页"  CommandName="Page" CommandArgument="Last" ><img src="/images/icons/pagination_last.gif" alt="" /></asp:LinkButton>
        到第<asp:TextBox runat="server" ID="inPageNum" Width="20"></asp:TextBox>页 <asp:Button ID="Button1" CommandName="go" runat="server" Width="30" Height="30" Text="go"/>
        <br />
    </PagerTemplate>--%>
<%-- <PagerTemplate>
    <div class="pagination"><span class="pagecounts">
        第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;&nbsp;
        <asp:LinkButton ID="FirstPage" class="easyui-linkbutton" plain="true" iconCls="pagination-first" runat="server" CommandName="Page" CommandArgument="First" ToolTip="首页"></asp:LinkButton>&nbsp;
        <asp:LinkButton ID="PrevPage" class="easyui-linkbutton" plain="true" iconCls="pagination-prev" runat="server" CommandName="Page" CommandArgument="Prev" ToolTip="上一页"></asp:LinkButton>&nbsp;
        <asp:LinkButton ID="NextPage" class="easyui-linkbutton" plain="true" iconCls="pagination-next" runat="server" CommandName="Page" CommandArgument="Next" ToolTip="下一页"></asp:LinkButton>&nbsp;
        <asp:LinkButton ID="LastPage" class="easyui-linkbutton" plain="true" iconCls="pagination-last" runat="server" CommandName="Page" CommandArgument="Last" ToolTip="尾页"></asp:LinkButton>&nbsp;&nbsp;
        到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
        <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
</PagerTemplate>--%>
<%--<PagerTemplate>
    <div class="pagination"><span class="pagecounts">
        第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;&nbsp;
        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="首页"><img src="/images/icons/pagination_first.gif" alt="" /></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="上一页"><img src="/images/icons/pagination_prev.gif" alt="" /></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="下一页"><img src="/images/icons/pagination_next.gif" alt="" /></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="尾页"><img src="/images/icons/pagination_last.gif" alt="" /></asp:LinkButton>&nbsp;&nbsp;
            到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
        <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
</PagerTemplate>--%>
                


