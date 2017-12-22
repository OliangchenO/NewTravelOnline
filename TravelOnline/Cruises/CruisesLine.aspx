<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesLine.aspx.cs" Inherits="TravelOnline.Cruises.CruisesLine" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>邮轮包船线路管理</title>
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
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>邮轮包船线路管理</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
        <DIV class=mt>
            <H1></H1><STRONG>邮轮包船线路管理</STRONG>
        </DIV>
        <div class="serchbar">
            类型：<asp:DropDownList ID="DropDownList1" runat="server" DataTextField="ProductName" DataValueField="MisClassId">
                  </asp:DropDownList> <asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem Value="0">销售</asp:ListItem>
                <asp:ListItem Value="1">暂停</asp:ListItem>
                <asp:ListItem Value="all">全部</asp:ListItem>
                  </asp:DropDownList> &nbsp; 线路名称：<asp:TextBox ID="tb_cname" runat="server" Width="150"></asp:TextBox>&nbsp;&nbsp;
            <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        
        </div>
        <div class="toolbar">
            <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		    <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-ok" onclick="ClearCache()">缓存清空</a>
            <DIV id="inputs" style="DISPLAY:none">
                <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
                <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
            </DIV>
	    </div>
        <DIV id=explain class="datagrid">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                CellPadding="5" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                Width="822px" AllowPaging="True" 
                onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                OnDataBound="GridView_DataBound" GridLines="Horizontal" SortExpression="EditTime" 
                SortDirection="DESC" PageSize="30">
	            <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" Height="30px" />
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
                    <asp:BoundField DataField="LineClass" HeaderText="类型" SortExpression="LineClass">
		                <HeaderStyle Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="旅游线路名称" SortExpression="LineName">
                        <ItemTemplate></ItemTemplate>
                        <HeaderStyle Width="25%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="shipname" HeaderText="船队" SortExpression="shipname">
		                <HeaderStyle Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PlanDate" HeaderText="开班" SortExpression="PlanDate" DataFormatString="{0:yy-MM-dd}">
		                <HeaderStyle Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Price" HeaderText="价格" SortExpression="Price">
		                <HeaderStyle Width="8%" />
                        <ItemStyle HorizontalAlign="Right" />
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
        <div id="toolbars" class="tips">
	        <div class="tips-text">
		       <a id=A1 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-add">房型设置</a>
               <a id=A2 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-edit">行程简介</a>
               <a id=A3 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-add">观光线路</a>
               <a id=A4 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-edit">退团损失</a>
               <a id=A5 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-tree_file">确认单说明</a>
               <HR width="96%" SIZE="1">
               <a id=A6 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-reload">房号分配</a>
               <a id=A7 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-reload">车位分配</a>
               <a id=A8 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-reload">餐桌分配</a>
               <a id=A9 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-redo">分团操作</a>
               <HR width="96%" SIZE="1">
               <a id=A10 target="_blank" href="" class="easyui-linkbutton" plain="true" iconCls="icon-down">舱房总表</a>
               <a id=A11 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-down">餐桌总表</a>
               <a id=A12 target="_blank" href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-down">量子号表</a>
               <a id=A13 target="_blank" href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-down">量子号套房</a>
            </div>
	        <div class="tips-angle diamond"></div>
        </div>
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

        function Route(id) {
            var url = "CruisesRoute.aspx?lineid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '包船行程简介', width: 750, height: 500, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function Damage(id) {
            var url = "CruisesDamage.aspx?LineId=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '退团损失设置', width: 880, height: 500, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function Confirm(id) {
            var url = "CruisesConfirm.aspx?lineid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '确认单须知信息', width: 750, height: 500, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function ClearCache() {
            var url = "AjaxService.aspx?action=ClearCache&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                } else {
                    jError('<strong>' + date.success + '</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

        var lineid
        $('.Doit').bind('click', function () {
            window.event.cancelBubble = true;
            if (lineid == $(this).attr("tgs")) {
                $("#toolbars").toggle();
            }
            else {
                $("#toolbars").show('fast');
            }
            lineid = $(this).attr("tgs");
            shipid = $(this).attr("ship");
            repflag = $(this).attr("rep"); //导出报表格式
            $("#toolbars").css("left", $(this).offset().left - 160);
            $("#toolbars").css("top", $(this).offset().top + $(this).height() + 8);
            $("#A1").attr("href", "CruisesLineRoom.aspx?id=" + lineid + "&shipid=" + shipid);
            $("#A2").unbind('click').click(function () { Route(lineid); });
            $("#A3").attr("href", "CruisesVisit.aspx?id=" + lineid);
            $("#A4").unbind('click').click(function () { Damage(lineid); });
            $("#A5").unbind('click').click(function () { Confirm(lineid); });
            $("#A6").attr("href", "LineRoomNo.aspx?lineid=" + lineid);
            $("#A7").attr("href", "LineCarNo.aspx?lineid=" + lineid);
            $("#A8").attr("href", "LineDinnerNo.aspx?lineid=" + lineid);
            $("#A9").attr("href", "LinePlanNo.aspx?lineid=" + lineid);
            //LineRoomNo.aspx
            //var url = "/CruisesOrder/ExcelOutPut.aspx?action=" + action + "&lineid=" + $("#Cid").val() + "&cid=" + arrChk;
            $("#A10").attr("href", "/CruisesOrder/ExcelOutPut.aspx?action=AllCharterManifest&lineid=" + lineid + "&report=" + repflag);
            $("#A12").attr("href", "/CruisesOrder/LiangZiHao.aspx?flag=1&action=AllCharterManifest&lineid=" + lineid + "&report=" + repflag);
            $("#A13").attr("href", "/CruisesOrder/LiangZiHao.aspx?flag=2&action=AllCharterManifest&lineid=" + lineid + "&report=" + repflag);
            if (repflag == "2") {
                //$("#A11").attr("href", "javascript:void(0);");
                $("#A11").attr("onclick", "javascript:alert('歌诗达邮轮不提供用餐表导出！');");
            }
            else {
                $("#A11").attr("target", "_blank"); 
                $("#A11").attr("href", "/CruisesOrder/ExcelOutPut.aspx?action=AllDinning&lineid=" + lineid + "&report=" + repflag);
            }
        });

        $('.tips-text').bind('click', function () {
            window.event.cancelBubble = true;
        });

        $(document).bind("click", function () {
            $("#toolbars").hide();
        });

        //单击其它地方关掉
//        var flag = false;
//        if (window.addEventListener) {//判断浏览器，flag=true为firfox，false为IE
//            flag = true;
//        }

//        if (flag) {
//            //document.addEventListener("click", hide, false);
//        } else {
//            document.attachEvent("onclick", hide);
//        }

//        function hide(e) {
//            if (document.activeElement.tagName == "A" || document.activeElement.tagName == "SPAN")
//            { }
//            else {
//                $("#toolbars").hide();
//            }
//        }
</script>
</body>
</html>


