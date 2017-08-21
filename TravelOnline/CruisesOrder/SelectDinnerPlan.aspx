<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDinnerPlan.aspx.cs" Inherits="TravelOnline.CruisesOrder.SelectDinnerPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body>
<div class="page_input">
    <div class="main_input">
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input id="LineId" name="LineId" type="hidden" value="<%=LineId %>"/>
                <input id="GuestId" name="GuestId" type="hidden" value="<%=GuestId %>"/>
                <input id="SelectId" name="SelectId" type="hidden" value=""/>
                <input id="flag" name="flag" type="hidden" value="<%=flag %>"/>
            </DIV>
        </form>
        <form id="form1" runat="server">   
            <div class=toolbar_inputa><div class="fl <%=hide1 %>">用餐时段：<asp:DropDownList ID="DropDownList1" 
                    runat="server" DataTextField="DinnerTime" DataValueField="DinnerTime" 
                    Width="80px" ></asp:DropDownList></div>
                    <div class=fl>&nbsp;桌/团号: <asp:TextBox class=text ID="AutoId" runat="server" Width="30px" MaxLength="10"></asp:TextBox></div>
                    <div class=fl>&nbsp;&nbsp;待分配人数：<%=SelectNums %>人</div>
                    <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>&nbsp;&nbsp;
                <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
                <A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>&nbsp;&nbsp;
            </div>
            <div class="clear"></div>
            <DIV style="DISPLAY:none">
                <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
                <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
            </DIV>
            <asp:ScriptManager ID="sm1" runat="server" />
            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <DIV id=explain class="datagrid">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="5" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                            Width="100%" AllowPaging="True" PageSize="10" BackColor="White" 
                            onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                            OnDataBound="GridView_DataBound" GridLines="None" SortExpression="Berth" 
                            SortDirection="DESC" ShowHeaderWhenEmpty="True" CellSpacing="0">
	                        <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                        <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-BorderStyle="None">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="NoName" HeaderText="名称" SortExpression="NoName">
		                            <HeaderStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="berth" HeaderText="容纳人数" SortExpression="berth">
		                            <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nums" HeaderText="已分配" SortExpression="Nums">
		                            <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="surplus" HeaderText="剩余" SortExpression="surplus">
		                            <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <input id="Radio<%#Container.DataItemIndex+1 %>" type="radio" name="CarNos" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
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
    </div>
</div>
<script type="text/javascript">
    $("#save").click(function () {
        var sold = $("#explain :radio:checked").val();
        var action = "CruisesGroupAllotDinnerNo"
        if (sold == null) {
            alert("请选择您要分配的数据!");
            return false;
        }
        $("#SelectId").val(sold);
        if ($("#flag").val() == "Plan") action = "CruisesGroupAllotPlan"
        $(this).hide();
        $("#islogin").show();
        $.post("AjaxService.aspx?action=" + action + "&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("分配成功！");
                    parent.$('#GridView_Refresh_Button').click();
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $("#save").show();
                    $("#islogin").hide();
                }
            });
    });
</script>
</body>
</html>
