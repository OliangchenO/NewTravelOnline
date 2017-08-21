<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineRoomNo.aspx.cs" Inherits="TravelOnline.Cruises.LineRoomNo" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=CruisesShip %> - 舱位房号分配</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <style type="text/css">
        #explain {WIDTH: 100%;MARGIN-TOP: 0px; }
        #explain table {table-layout:fixed;}
        #explain td {white-space:nowrap;overflow:hidden;BORDER-right: #EFEFEF 1px solid;}
        .fixColleft {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; }
        .fix_hidden {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; overflow:hidden; white-space:nowrap;}
        .headline { font-size:16px; background:url(/images/bg_function.png) no-repeat;padding:0 0 10px 45px; margin:20px 5px 0;}
    </style>
    <style>
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
</head>
<body id="none">
<uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<DIV class="w main">
    <form id="form1" runat="server">
        <div class="main">
            <div id="order_title">
                <h2 class="headline"><SPAN class=headstep><%=CruisesShip%> - 舱位房号分配</SPAN></h2>
                <%--<br>
                <a href="javascript:void(0);" class="easyui-linkbutton" plain="false" iconCls="icon-no" onclick="GroupDo('Cancel')">取消入住</a>--%>
            </div>
            <DIV id="DIV1" style="DISPLAY:none">
                <input id="Cid" name="Cid" type="hidden" value="<%=lineid %>"/>
            </DIV>
            <div class="serch">
                房号: <asp:TextBox class=text ID="tb_cname" runat="server" Width="50px" MaxLength="10"></asp:TextBox> 
                &nbsp;&nbsp;代码: <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="roomcode" DataValueField="roomcode" Width="60px"></asp:DropDownList>
                &nbsp;&nbsp;房型: <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem Value="all">全部</asp:ListItem>
                    <asp:ListItem Value="1">单人间</asp:ListItem>
                    <asp:ListItem Value="2">双人间</asp:ListItem>
                    <asp:ListItem Value="3">三人间</asp:ListItem>
                    <asp:ListItem Value="4">四人间</asp:ListItem>
                    <asp:ListItem Value="5">五人间</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;&nbsp;分配: <asp:DropDownList ID="DropDownList3" runat="server">
                    <asp:ListItem Value="all">全部</asp:ListItem>
                    <asp:ListItem Value="1">已入住</asp:ListItem>
                    <asp:ListItem Value="0">未入住</asp:ListItem>
                    <asp:ListItem Value="2">有拼房</asp:ListItem>
                    <asp:ListItem Value="3">未住满</asp:ListItem>
                    <asp:ListItem Value="4">已住满</asp:ListItem>
                    <asp:ListItem Value="5">大床房</asp:ListItem>
                 </asp:DropDownList> &nbsp; &nbsp;&nbsp;
                <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
            </div>
            <div style="float:right;text-align:right;margin-bottom:10px">
            <a href="javascript:void(0);" class="easyui-linkbutton" plain="false" iconCls="icon-add" onclick="Import('RoomNo')">房号导入</a>&nbsp;&nbsp;
                <a href="javascript:void(0);" class="easyui-linkbutton" plain="false" iconCls="icon-add" onclick="Import('BookingNo')">Booking导入</a>&nbsp;&nbsp;
                <a id="A1" href="javascript:void(0);" class="easyui-linkbutton" plain="false" iconCls="icon-down" onclick="OutPut('KingSize')">大床导出</a>&nbsp;&nbsp;
                <a href="javascript:void(0);" class="easyui-linkbutton" plain="false" iconCls="icon-no" onclick="GroupDo('Delete')">删除房号</a>
            </div>
            <DIV id="inputs" class=hide>
                <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
                <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
            </DIV>
            <div class="long">
                <asp:ScriptManager ID="sm1" runat="server" />
                <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <DIV id=explain class="datagrid2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                CellPadding="5" CellSpacing="0" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                                Width="100%" AllowPaging="True" PageSize="20"
                                onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                                OnDataBound="GridView_DataBound" GridLines="None" SortExpression="RoomNo" 
                                SortDirection="ASC" ShowHeaderWhenEmpty="True">
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
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate></ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RoomNo" HeaderText="舱房号" SortExpression="RoomNo">
		                                <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="roomcode" HeaderText="代码" SortExpression="roomcode">
		                                <HeaderStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="roomname" HeaderText="客房名称" SortExpression="roomname">
		                                <HeaderStyle Width="20%" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="berth" HeaderText="可住" SortExpression="berth">
		                                <HeaderStyle Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nums" HeaderText="已入住" SortExpression="Nums">
		                                <HeaderStyle Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BookingNo" HeaderText="Booking" SortExpression="BookingNo">
		                                <HeaderStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <HeaderStyle Width="12%" />
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
            </div>          
        </div>
        <SCRIPT language="javascript" src="/Scripts/NowrapSet.js"></SCRIPT>
    </form>
    <DIV class=clr></DIV>
</DIV>
<uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        function chkall(obj) {
            if (obj.checked) {
                $("input[name$='CheckBox']").each(function () { this.checked = true; });
            } else {
                $("input[name$='CheckBox']").each(function () { this.checked = false; });
            }
        }

        function EditRoom(id) {
            var url = "LineRoomEdit.aspx?roomid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: "修改舱房", width: 600, height: 200, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function Import(flag) {
            var doname = "导入舱房房号"
            if (flag == "BookingNo") {
                doname = "导入BookingId";
            }
            var url = "LineRoomNoImport.aspx?flag=" + flag + "&lineid=" + $("#Cid").val();
            var dg = new $.dialog({ id: 'No1', page: url, title: doname, width: 650, height: 500, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function OutPut(flag) {
            if (confirm("确认要导出吗？")) {
            }
            else
            { return false; }
            var url = "/CruisesOrder/ExcelOutPut.aspx?action=" + flag + "&lineid=" + $("#Cid").val();
            $("#A1").attr("href", url);
            $("#A1").attr("target", "_blank");
        }

        function CancelRoom(id) {
            if (confirm("确认要取消拼房吗？")) {
            }
            else
            { return false; }
            var url = "AjaxService.aspx?action=CancelRoomNo&roomnoid=" + id;
            //window.open(url);
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>' + date.success + '</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

        function GroupDo(flag) {
            //alert("暂未开通");
            //return;
            var arrChk = "";
            var doname = "取消客人入住"
            var action = "CruisesCheckInCancel"
            if (flag == "Delete") {
                doname = "删除房号";
                action = "CruisesRoomNoDelete"
            } 
            var items = $("input[name='CheckBox']:checked");
            if (items.length == 0) {
                jNotify('<strong>请选择要批量操作的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if (confirm("确认要批量 " + doname + " 吗？")) {
            }
            else
            { return false; }
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ","; });
            arrChk = arrChk.substr(0, arrChk.length - 1);
            var url = "AjaxService.aspx?action=" + action + "&Id=" + arrChk + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>' + date.success + '</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

    </script>
</body>
</html>