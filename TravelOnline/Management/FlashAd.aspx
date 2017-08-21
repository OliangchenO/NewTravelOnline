<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlashAd.aspx.cs" Inherits="TravelOnline.Management.FlashAD" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flash轮换广告</title>
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
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>Flash轮换广告</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>Flash轮换广告设置</STRONG>
    </DIV>
    <div class="serchbar">
        板块：<asp:DropDownList ID="DropDownList1" runat="server">
            <%--<asp:ListItem Value="Index">首页</asp:ListItem>
            <asp:ListItem Value="OutBound">出境旅游</asp:ListItem>
            <asp:ListItem Value="InLand">国内旅游</asp:ListItem>
            <asp:ListItem Value="FreeTour">自由行</asp:ListItem>
            <asp:ListItem Value="Cruises">邮轮</asp:ListItem>
            <asp:ListItem Value="Visa">签证</asp:ListItem>--%>
            <asp:ListItem Value="Index_New">新首页</asp:ListItem>
            <asp:ListItem Value="OutBound_New">新出境</asp:ListItem>
            <asp:ListItem Value="InLand_New">新国内</asp:ListItem>
            <asp:ListItem Value="FreeTour_New">新自由行</asp:ListItem>
            <asp:ListItem Value="Cruises_New">新邮轮</asp:ListItem>
            <asp:ListItem Value="Visa_New">新签证</asp:ListItem>
            <asp:ListItem Value="OutBound_Small">出境小轮换</asp:ListItem>
            <asp:ListItem Value="InLand_Small">国内小轮换</asp:ListItem>
            <asp:ListItem Value="Citie">磁贴</asp:ListItem>
            <asp:ListItem Value="Index_Up_3">首页三栏上</asp:ListItem>
            <asp:ListItem Value="Index_Down_3">首页三栏下</asp:ListItem>
            <asp:ListItem Value="OutBound_3">出境三栏</asp:ListItem>
            <asp:ListItem Value="InLand_3">国内三栏</asp:ListItem>
            <asp:ListItem Value="FreeTour_3">自由行三栏</asp:ListItem>
            <asp:ListItem Value="Cruises_3">邮轮三栏</asp:ListItem>
            <asp:ListItem Value="Visa_3">签证三栏</asp:ListItem>
            <asp:ListItem Value="Index_T">首页通栏</asp:ListItem>
            <asp:ListItem Value="OutBound_T">出境通栏</asp:ListItem>
            <asp:ListItem Value="InLand_T">国内通栏</asp:ListItem>
            <asp:ListItem Value="FreeTour_T">自由行通栏</asp:ListItem>
            <asp:ListItem Value="Cruises_T">邮轮通栏</asp:ListItem>
            <asp:ListItem Value="Visa_T">签证通栏</asp:ListItem>
            <asp:ListItem Value="LeftHot_OutBound">出境热点</asp:ListItem>
            <asp:ListItem Value="LeftHot_InLand">国内热点</asp:ListItem>
            <asp:ListItem Value="Hot_FreeTour">自由行热点</asp:ListItem>
            <asp:ListItem Value="LeftArea_OutBound">出境目的地热点</asp:ListItem>
            <asp:ListItem Value="LeftArea_InLand">国内目的地热点</asp:ListItem>
            <asp:ListItem Value="Partner">合作伙伴</asp:ListItem>
            <asp:ListItem Value="CruisesShip">邮轮船队推荐</asp:ListItem>
            <asp:ListItem Value="WeChat">微信</asp:ListItem>
            <asp:ListItem Value="---">----新版----</asp:ListItem>
            <asp:ListItem Value="N_Index_Slide">首页轮换</asp:ListItem>
            <asp:ListItem Value="N_Index_Banner">轮换右侧广告位</asp:ListItem>
            <asp:ListItem Value="N_Index_Season">首页当季推荐</asp:ListItem>
            <asp:ListItem Value="N_S_OutBound_Slide">出境轮换</asp:ListItem>
            <asp:ListItem Value="N_S_InLand_Slide">国内轮换</asp:ListItem>
            <asp:ListItem Value="N_S_FreeTour_Slide">自由行轮换</asp:ListItem>
            <asp:ListItem Value="N_S_Cruise_Slide">邮轮轮换</asp:ListItem>
            <asp:ListItem Value="N_S_Visa_Slide">签证轮换</asp:ListItem>
            <asp:ListItem Value="N_S_List_Season">线路列表当季推荐</asp:ListItem>
            <asp:ListItem Value="freetrip">青青旅行</asp:ListItem>
            <asp:ListItem Value="N_S_Journal_SL">游记左侧图片</asp:ListItem>
            <asp:ListItem Value="N_S_Journal_Slide">游记轮换</asp:ListItem>
            <%--<asp:ListItem Value="WeChat">微信</asp:ListItem>
            <asp:ListItem Value="WeChat">微信</asp:ListItem>
            <asp:ListItem Value="WeChat">微信</asp:ListItem>--%>
        </asp:DropDownList>
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        
    </div>
    <div class="toolbar">
        <a id="AddNew" href="###" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DeleteSelect()">删除</a>
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="20" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="AdSort" SortDirection="ASC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="板块" SortExpression="RightFlag">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="AdName" HeaderText="文字说明" SortExpression="AdName">
		                    <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
		                <asp:BoundField DataField="AdPicUrl" HeaderText="图片" SortExpression="AdPicUrl">
		                    <HeaderStyle Width="35%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AdPageUrl" HeaderText="链接" SortExpression="AdPageUrl">
		                    <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
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
            $('#DropDownList1').change(function () {
                $('#GridView_Serch_Button').click() 
            });
            $('#AddNew').dialog({ id: 'No1', page: 'FlashAdInfo.aspx', title: '增加Flash轮换广告', width: 590, height: 580, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
        });

        function EditInfo(id) {
            var url = "FlashAdInfo.aspx?Id=" + id;
            //alert("aa");
            var dg = new $.dialog({ id: 'No1', page: url, title: '修改Flash轮换广告', width: 590, height: 580, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
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
            var url = "AjaxService.aspx?action=DeleteAdInfos&AdFlag=" + $("#DropDownList1").val() + "&Id=" + arrChk + "&r=" + Math.random();
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
