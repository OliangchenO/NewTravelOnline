﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FavoriteLine.aspx.cs" Inherits="TravelOnline.Management.FavoriteLine" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅游线路推荐管理</title>
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
    <style type="text/css">
        #explain {WIDTH: 100%;MARGIN-TOP: 0px; }
        #explain table {table-layout:fixed;}
        #explain td {white-space:nowrap;overflow:hidden;BORDER-right: #EFEFEF 1px solid;}
        .fixColleft {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; }
        .fix_hidden {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; overflow:hidden; white-space:nowrap;}
        .headline { font-size:16px; background:url(/images/bg_function.png) no-repeat;padding:0 0 10px 45px; margin:20px 5px 0;}

	    .diamond{-ms-filter: "progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865475, M12=-0.7071067811865477, M21=0.7071067811865477, M22=0.7071067811865475, SizingMethod='auto expand')";
            filter: progid:DXImageTransform.Microsoft.Matrix(
	            M11=0.7071067811865475,
	            M12=-0.7071067811865477,
	            M21=0.7071067811865477,
	            M22=0.7071067811865475,
	            SizingMethod='auto expand'
            );

            -moz-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform:rotate(45deg);
	    }
	    :root .diamond{filter:none\9;}
	    .tips{display:none;width:200px;position:absolute;background: #fff;border:1px solid #4BA41B;padding:10px;}
	    .tips-angle{position:absolute;display:block;width:8px;height:8px;font-size:0;background:#fff;border-left:1px solid #4BA41B;border-top:1px solid #4BA41B;top:-5px;top:-7px\9;left:165px;}
	    .tips-text {TEXT-ALIGN: left;}
	    .tips-text a {width:90px;}
	</style>
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
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>收藏线路</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>旅游线路推荐管理</STRONG>
    </DIV>
    <div class="serchbar">
        类型：<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>全部</asp:ListItem>
            <asp:ListItem Value="OutBound">出境</asp:ListItem>
            <asp:ListItem Value="InLand">国内</asp:ListItem>
            <asp:ListItem Value="FreeTour">自由行</asp:ListItem>
            <asp:ListItem Value="Cruises">邮轮</asp:ListItem>
            <asp:ListItem Value="Visa">签证</asp:ListItem>
              </asp:DropDownList> <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem>全部</asp:ListItem>
            <asp:ListItem Value="0">销售</asp:ListItem>
            <asp:ListItem Value="1">暂停</asp:ListItem>
              </asp:DropDownList> &nbsp; 线路：<asp:TextBox ID="tb_cname" runat="server" Width="150"></asp:TextBox>&nbsp;
              ID：<asp:TextBox ID="tb_mislineid" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;
              &nbsp;&nbsp;<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
    </div>
    <DIV id="inputs" style="DISPLAY:none">
        <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
        <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
    </DIV>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="40" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="Num" SortDirection="DESC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "MisLineId") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TypeName" HeaderText="类型" SortExpression="LineClass">
		                    <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="旅游线路名称" SortExpression="LineName">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="28%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Num" HeaderText="收藏数" SortExpression="Num">
		                    <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        
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
        <SCRIPT language="javascript" src="../Scripts/NowrapSet.js"></SCRIPT>
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
        });

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

        function DoIt(DoFlag) {
            var arrChk = "";

            if (DoFlag != "WeChatReset") {
                if ($("#DropDownList1").val() == "全部") {
                    alert("请选择类型后再操作");
                    return;
                }
                var items = $("input[name='CheckBox']:checked");
                if (items.length == 0) {
                    jNotify('<strong>请选择要操作的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
                $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
                arrChk = arrChk.substr(0, arrChk.length - 1);

            }

            var url = "AjaxService.aspx?action=NewRecom&LineType=" + $("#DropDownList1").val() + "&DoFlag=" + DoFlag + "&Id=" + arrChk + "&r=" + Math.random();
            if (DoFlag == "CacheReset") url = "AjaxService.aspx?action=CacheReset&LineType=" + $("#DropDownList1").val() + "&Id=" + arrChk + "&r=" + Math.random();
            if (DoFlag == "WeChatReset") url = "AjaxService.aspx?action=WeChatReset&r=" + Math.random();
            if (DoFlag == "WeChatcancel") url = "AjaxService.aspx?action=WeChatcancel&Id=" + arrChk + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                    $("#toolbars").hide();
                } else {
                    jError('<strong>操作失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }


        $('#JustDoIt').bind('click', function () {
            window.event.cancelBubble = true;
            $("#toolbars").toggle();
            $("#toolbars").css("left", $(this).offset().left - 140);
            $("#toolbars").css("top", $(this).offset().top + $(this).height() + 8);
        });

        //单击其它地方关掉
        $('.tips-text').bind('click', function () {
            window.event.cancelBubble = true;
        });

        $(document).bind("click", function () {
            $("#toolbars").hide();
        });
</script>
</body>
</html>
