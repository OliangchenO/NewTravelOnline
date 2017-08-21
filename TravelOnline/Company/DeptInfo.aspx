<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptInfo.aspx.cs" Inherits="TravelOnline.Company.DeptInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body>

<div class="page_input">
    <div class="main_input">
        <div class="MeetingTitle">
            <div><img src="../images/icon/New Doc.png" class=img30><%=companyname %> 部门资料</div>
        </div>
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input id="Cid" name="Cid" type="hidden" />
                <input id="companyid" name="companyid" type="hidden" value="<%=companyid %>"/>
            </DIV>
            <div class=toolbar_input><div class=firstinput>部门名称：</div><div class=fl><input class=ipt type="text" name="deptname" id="deptname" maxlength="50"  size="38" />&nbsp;&nbsp;
            畅游代码：<input id="misid" name="misid" type="text" class="ipt easyui-numberbox" precision="0" max="99999999" style="width: 100px;text-align:center;" maxlength="8" /></div>
                <a href="javascript:void(0)" class="tools" id="add" onclick="addnew()"><img src="../images/icon/add.png" class=img20>新增</a>
                <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
                <a href="javascript:void(0)" class=tools id="delete"><img src="../images/icon/del.png" class=img20>删除</a>
            </div>
        </form>
        <div class="clear"></div>
        <form id="form1" runat="server">        
            <DIV style="DISPLAY:none">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
                <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
                <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
            </DIV>
            <asp:ScriptManager ID="sm1" runat="server" />
            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <DIV id=explain class="datagrid">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="8" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                            Width="100%" AllowPaging="True" PageSize="6" BackColor="White" 
                            onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                            OnDataBound="GridView_DataBound" GridLines="None" SortExpression="id" 
                            SortDirection="DESC" ShowHeaderWhenEmpty="True" CellSpacing="1">
	                        <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                        <Columns>
                                <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                                    <ItemTemplate>
                                        <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号" ItemStyle-BorderStyle="None">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="deptname" HeaderText="部门名称" SortExpression="deptname">
		                            <HeaderStyle Width="50%" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="misid" HeaderText="畅游代码" SortExpression="misid">
		                            <HeaderStyle Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
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
        if ($("#deptname").val() == "" || $("#companyid").val() == "" || $("#misid").val() == "") {
            jError('<strong>部门名称及畅游代码都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            $("#deptname").focus();
            return;
        }

        $(this).hide();
        $.post("AjaxService.aspx?action=DeptInfo&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        addnew();
                        $('#GridView_Serch_Button').click();
                    }
                    else {
                        $('#GridView_Refresh_Button').click();
                    }
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });

                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        $(this).show();
    });

    function addnew() {
        $("#deptname").val("");
        $("#Cid").val("");
    }

    function Edit(id, name,misid) {
        $("#deptname").val(name);
        $("#Cid").val(id);
        $("#misid").val(misid);
    }
</script>
</body>
</html>
