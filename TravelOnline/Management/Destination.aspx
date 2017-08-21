<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Destination.aspx.cs" Inherits="TravelOnline.Management.Destination" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>目的地</title>
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
    <script type="text/javascript" src="/Scripts/lhgselect/lhgselect.min.js"></script>
    <script type="text/javascript" src="/Js/Destination/DestinationClass.js"></script>
    <style type="text/css">
        .order {margin-right:8px;}
        .toolbar{WIDTH: 100%;MARGIN-TOP: 0px; }
        #explain {WIDTH: 100%;MARGIN-TOP: 0px; }
        #explain table {table-layout:fixed;}
        #explain td {white-space:nowrap;overflow:hidden;BORDER-right: #EFEFEF 1px solid;}
        .fixColleft {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; }
        .fix_hidden {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; overflow:hidden; white-space:nowrap;}
        .headline { font-size:16px; background:url(/images/bg_function.png) no-repeat;padding:0 0 10px 45px; margin:20px 5px 0;}
        #tip {position:absolute;display:none;}
        #tip s {position:absolute;top:20px;left:-21px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #4BA41B transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
        #tip s i{position:absolute;top:-10px;left:-8px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #fff transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
        #tip .t_box {position:relative;background-color:#CCC;filter:alpha(opacity=50);-moz-opacity:0.5;bottom:-3px;right:-3px;}
        #tip .t_box div{width:400px;position:relative;background-color:#FFF;border:1px solid #4BA41B;padding:5px;top:-3px;left:-3px;}
        a.tip  {
	    text-decoration:none;
	    outline:none;
	    color:#159ce9;
        }
        a.tip :visited {
	        text-decoration:none;
	        outline:none;
	        color:#159ce9;
        }
        a.tip:hover {
	        COLOR: #159ce9; TEXT-DECORATION: underline
        }
        #logul {LINE-HEIGHT: 25px;}
        #logul li {PADDING-LEFT: 5px;PADDING-RIGHT: 5px;BORDER-BOTTOM: #f3e7c7 1px solid;}
        #logul .logtit{LINE-HEIGHT: 20px;background-color:#F7F7F7;}
    </style>
    <style>
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
	.tips{display:none;width:210px;position:absolute;background: #fff;border:1px solid #4BA41B;padding:10px;}
	.tips-angle{position:absolute;display:block;width:8px;height:8px;font-size:0;background:#fff;border-left:1px solid #4BA41B;border-top:1px solid #4BA41B;top:-5px;top:-7px\9;left:200px;}
	.tips-text {TEXT-ALIGN: left;}
	.tips-text a {width:90px;}
    
	</style>
    <script type="text/javascript">
        $(function () {
            $('#AddNew').dialog({ id: 'No1', page: 'DestinationInfo.aspx', title: '新增目的地', width: 400, height: 540, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });

            $('#DropDownList1').change(function () {
                $('#GridView_Serch_Button').click()
            });
        });

        $(function () {
            $('#demo1 select').select({ data: data, field_name: '#SelectClass' });
        });

        function myrefresh() {
            var url = "AjaxService.aspx?action=CreateDestinationSelect&r=" + Math.random();
            $.getJSON(url, function (date) {
            });
            window.location.reload();
        }
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
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>目的地</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>目的地</STRONG>
    </DIV>
    <div class="serchbar" id="demo1"><select id="sel1" class="city" style="width:100px;"></select>&nbsp;<select id="sel2" class="city" style="width:100px;"></select>&nbsp;<select id="sel3" class="city" style="width:100px;"></select>  
        <asp:CheckBox ID="CheckBox1" runat="server" Text="只显示同级" /> &nbsp;<asp:CheckBox ID="CheckBox2" runat="server" Text="热门" /> &nbsp;<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="Serch()">查询</a><a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 10px;margin-top: 10px;">刷新分类</a>
    </div>
    <div class="toolbar">
        <a id="AddNew" href="###" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-ok" onclick="Hot()">设为热门</a>
		<%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DeleteSelect()">删除</a>--%>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>
        <a target="_blank" href="/CruisesOrder/ExcelOutPut.aspx?action=DestinationOutPut" class="easyui-linkbutton" plain="true" iconCls="icon-down">数据导出</a>
        <DIV id="inputs" style="DISPLAY:none">
            <asp:TextBox ID="parent1" runat="server" Width="10"></asp:TextBox>
            <asp:TextBox ID="parent2" runat="server" Width="10"></asp:TextBox>
            <asp:TextBox ID="parent3" runat="server" Width="10"></asp:TextBox>
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="30" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="ClassList" SortDirection="ASC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <input id="RB<%#Container.DataItemIndex+1 %>" type="radio" name="rb" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                                <%--<input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />--%>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="id" HeaderText="id">
		                    <HeaderStyle Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="目的名称">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="18%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Ename" HeaderText="英文名称">
		                    <HeaderStyle Width="18%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PinYin" HeaderText="拼音">
		                    <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SortPinYin" HeaderText="检索">
		                    <HeaderStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SortNum" HeaderText="排序">
		                    <HeaderStyle Width="5%" />
                        </asp:BoundField>
		                <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="25%" />
                            <ItemStyle HorizontalAlign="Left" />
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
<div id="tip"><div class="t_box"><div><s><i></i></s><span id=olog></span></div></div></div>
<div id="toolbars" class="tips">
	<div class="tips-text">
		<a id=A2 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-edit">景点信息</a>
        <a id=A3 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-edit">交通信息</a>
        <%--<HR width="96%" SIZE="1">--%>
               
    </div>
	<div class="tips-angle diamond"></div>
</div>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        function Serch() {
            $("#parent1").val($("#sel1").val());
            $("#parent2").val($("#sel2").val());
            $("#parent3").val($("#sel3").val());
            $('#GridView_Serch_Button').click()
        }

        function EditInfo(id) {
            var url = "DestinationInfo.aspx?Id=" + id;
            //alert("aa");
            var dg = new $.dialog({ id: 'No1', page: url, title: '修改目的地', width: 400, height: 540, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
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

        function Hot() {
            var items = $("input[name='rb']:checked").val();
            if (items == null) {
                jNotify('<strong>请选择要设置的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=SetHotDestination&Id=" + items + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>设置成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>设置失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

//        function DeleteSelect() {
//            var items = $("input[name='rb']:checked").val();
//            if (items == null) {
//                jNotify('<strong>请选择要删除的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                return false;
//            }
//            if (confirm('确认要删除吗？\n\n如果删除的数据有下级类别，将同时被删除！')) {
//            }
//            else
//            { return false; }
//            var url = "AjaxService.aspx?action=DeleteDestinationClass&Id=" + items + "&r=" + Math.random();
//            $.getJSON(url, function (date) {
//                if (date.success == 0) {
//                    jSuccess('<strong>信息删除成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                    $('#GridView_Refresh_Button').click();
//                } else {
//                    jError('<strong>信息删除失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                }
//            })
//        }


</script>
</body>
</html>

