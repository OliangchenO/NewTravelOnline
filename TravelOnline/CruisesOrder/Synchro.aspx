<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Synchro.aspx.cs" Inherits="TravelOnline.CruisesOrder.Synchro" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心 - 畅游同步错误记录</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/base.js"></script>
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
	.tips{display:none;width:85px;position:absolute;background: #fff;border:1px solid #4BA41B;padding:10px;}
	.tips-angle{position:absolute;display:block;width:8px;height:8px;font-size:0;background:#fff;border-left:1px solid #4BA41B;border-top:1px solid #4BA41B;top:-5px;top:-7px\9;left:75px;}
	.tips-text {TEXT-ALIGN: center;}
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
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>畅游同步错误记录</SPAN></DIV>


 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>畅游同步错误记录</STRONG>
    </DIV>
    <div class="toolbar">&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Width="120">
            <asp:ListItem Value="1">最近一月记录</asp:ListItem>
            <asp:ListItem Value="2">历史记录</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="DropDownList2" runat="server" Width="120">
            <asp:ListItem Value="all">全部</asp:ListItem>
            <asp:ListItem Value="AdjustCruisesRoom">舱房变更</asp:ListItem>
            <asp:ListItem Value="CancelCruisesRoom">舱房取消</asp:ListItem>
            <asp:ListItem Value="AdjustVisit">观光调整</asp:ListItem>
            <asp:ListItem Value="OrderRetreat">订单取消</asp:ListItem>
            <asp:ListItem Value="AdjustPrice">费用调整</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        &nbsp;&nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		&nbsp;&nbsp;
        <%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>--%>
        <DIV id="inputs" style="DISPLAY:none">
            <input id="TB_DeptId" type="hidden" value="0"/>
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
    <DIV id=explain class="datagrid">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="5" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
            Width="822px" AllowPaging="True" 
            onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
            OnDataBound="GridView_DataBound" GridLines="Horizontal" SortExpression="id" 
            SortDirection="DESC" PageSize="30">
	        <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" 
                Height="30px" />
	        <Columns>
                <asp:BoundField DataField="AutoId" HeaderText="订单号" SortExpression="AutoId">
		            <HeaderStyle Width="7%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="同步类型">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="14%" />
                </asp:TemplateField>
                <asp:BoundField DataField="infos" HeaderText="同步错误信息" SortExpression="infos">
		            <HeaderStyle Width="38%" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="inputtime" HeaderText="记录日期" SortExpression="inputtime" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="dotime" HeaderText="同步时间" SortExpression="dotime" DataFormatString="{0:MM-dd HH:ss:mm}">
		            <HeaderStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="douser" HeaderText="操作人" SortExpression="douser">
		            <HeaderStyle Width="10%" />
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
    <div id="tip"><div class="t_box"><div><s><i></i></s><span id=olog></span></div></div></div>
    <div id="toolbars" class="tips">
	    <div class="tips-text">
		   <a id=A1 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-edit">同步确认</a>
           <a id=A2 href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-edit">取消同步</a>
	    </div>
	    <div class="tips-angle diamond"></div>
    </div>
    </form>
</DIV>    

</DIV>

<DIV class=clr></DIV></DIV>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('.tip').mouseover(function () {
                $('#tip').show('fast');
                $('#olog').html("<br><span class=iloading>正在加载中，请稍候...</span><br>");
                var orderid = $(this).attr("tag");
                var url = "/Users/AjaxService.aspx?action=OrderLogInfo&OrderId=" + orderid;
                var infos = "";
                //window.open(url);
                $.getJSON(url, function (date) {
                    infos = date.success;
                    $('#olog').html(infos);
                })
            }).mouseout(function () {
                $('#tip').hide();
            }).mousemove(function (e) {
                $('#tip').css({ "top": (e.pageY - 30) + "px", "left": (e.pageX + 30) + "px" })
            })
        });

        var orderid
        $('.Doit').bind('click', function () {
            if (orderid == $(this).attr("tgs")) {
                $("#toolbars").toggle();
            }
            else {
                $("#toolbars").show('fast');
            }
            applyid = $(this).attr("tgs");
            $("#toolbars").css("left", $(this).offset().left - 70);
            $("#toolbars").css("top", $(this).offset().top + $(this).height() + 8);
            $("#A1").unbind('click').click(function () { DoAdjust(applyid); });
            $("#A2").unbind('click').click(function () { DoCancel(applyid); });
        });

        //img.unbind('click').click(function () { doa(applyid); });

        //单击其它地方关掉
        var flag = false;
        if (window.addEventListener) {//判断浏览器，flag=true为firfox，false为IE
            flag = true;
        }

        if (flag) {
            //document.addEventListener("click", hide, false);
        } else {
            document.attachEvent("onclick", hide);
        }

        function hide(e) {
            if (document.activeElement.tagName == "A" || document.activeElement.tagName == "SPAN")
            { }
            else {
                $("#toolbars").hide();
            }
        }

        function DoAdjust(applyid) {
            if (confirm('确认操作您选择的记录吗？')) {
            }
            else
            { return false; }
            var url = "AjaxService.aspx?action=DoMisError&ApplyId=" + applyid + "&r=" + Math.random();

            $.get(url, function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if (obj.success != "OK") {
                        alert(obj.success);
                    }
                    else {
                        alert("同步操作成功!");
                    }
                    $('#GridView_Refresh_Button').click();
                }
                else {
                    jError('<strong>' + obj.error + '!</strong>', { clickDiv: true, autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }

        function DoCancel(applyid) {
            if (confirm('确认要取消吗？')) {
            }
            else
            { return false; }
            var url = "AjaxService.aspx?action=CancelMisError&ApplyId=" + applyid + "&r=" + Math.random();
            $.get(url, function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("取消成功!");
                    $('#GridView_Refresh_Button').click();
                }
                else {
                    jError('<strong>' + obj.error + '!</strong>', { clickDiv: true, autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
            //jClose();
        }
    </script>
</body>
</html>
