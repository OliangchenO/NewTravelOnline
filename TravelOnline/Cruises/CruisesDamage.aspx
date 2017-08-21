<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesDamage.aspx.cs" Inherits="TravelOnline.Cruises.CruisesDamage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退团损失</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <style>
        .runcode{border:1px solid #ddd;background:url(/images/iconDate.gif) center right no-repeat #f7f7f7;cursor:pointer;font:12px tahoma,arial;height:21px;width:150px;}
    </style>
</head>
<body>

<div class="page_input">
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" class="tools" id="add" onclick="addnew()"><img src="../images/icon/add.png" class=img20>新增</a>
        <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
        <a href="javascript:void(0)" class=tools id="delete" onclick="DeleteSelect()"><img src="../images/icon/del.png" class=img20>删除</a>
    </div>
    <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
            <input id="Cid" name="Cid" type="hidden" />
            <input id="LineId" name="LineId" type="hidden" value="<%=LineId %>"/>
        </DIV>
        <div class="line_input hide" id="inputthis" style="line-height: 40px;"><div class=firstinput>退团日期：</div>
            <input class="runcode ipt" type="text" name="begindate" id="begindate" maxlength="10" style="width: 100px" readonly="readonly" />&nbsp;&nbsp; 
            至&nbsp; <input class="runcode ipt" type="text" name="enddate" id="enddate" maxlength="10" style="width: 100px" readonly="readonly" />&nbsp;&nbsp;
            损失费扣除方式：<select name="flag" id="flag" style="width:150px;">
	            <option selected="selected" value="1">按最低预付款(订金)</option>
	            <option value="2">按订单金额</option>
            </select>&nbsp;&nbsp;
            扣款比例：<input id="rate" name="rate" type="text" class="ipt easyui-numberbox" precision="0" max="100" style="width: 30px;text-align:center;" maxlength="3" />% 百分比<br>
            <div class=firstinput>扣款说明：</div><input id="infos" name="infos" type="text" class="ipt" style="width: 650px;" maxlength="500" />
        </div>
    </form>
    <div class="clear"></div>
    <form id="form1" runat="server">
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
                        Width="100%" AllowPaging="True" 
                        onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                        OnDataBound="GridView_DataBound" GridLines="Horizontal" SortExpression="Id" 
                        SortDirection="DESC" PageSize="30">
	                    <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" 
                            Height="30px" />
	                    <Columns>
                            <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                                <ItemTemplate>
                                    <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" ItemStyle-BorderStyle="None">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="退团日期起止" SortExpression="begindate">
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="infos" HeaderText="扣款说明" SortExpression="infos">
		                        <HeaderStyle Width="50%" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="rate" HeaderText="扣款比例%" SortExpression="rate">
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView_Refresh_Button" />
                <asp:AsyncPostBackTrigger ControlID="GridView_Serch_Button" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('#begindate').calendar({ maxDate: '#enddate', btnBar: false });
        $('#enddate').calendar({ minDate: '#begindate', btnBar: false });
    });

    $("#save").click(function () {
        if ($("#begindate").val() == "" || $("#enddate").val() == "") {
            jError('<strong>退团日期都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }

        if ($("#rate").val() == "" || $("#infos").val() == "") {
            jError('<strong>扣款比例和说明都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            $("#deptname").focus();
            return;
        }

        $(this).hide();
        $.post("AjaxService.aspx?action=CruisesDamage&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        addnew();
                        $('#GridView_Serch_Button').click();
                    }
                    else {
                        $("#inputthis").hide();
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
        $("#Cid").val("");
        $("#begindate").val("");
        $("#enddate").val("");
        $("#rate").val("");
        $("#infos").val("");
        $("#inputthis").show();
    }

    function Edit(id, a1, a2, a3, a4, a5) {
        $("#inputthis").show();
        $("#Cid").val(id);
        $("#begindate").val(a1);
        $("#enddate").val(a2);
        $("#flag").val(a3);
        $("#rate").val(a4);
        $("#infos").val(a5);
    }

    function chkall(obj) {
        if (obj.checked) {
            $("input[name$='CheckBox']").each(function () { this.checked = true; });
        } else {
            $("input[name$='CheckBox']").each(function () { this.checked = false; });
        }
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
        addnew();
        $("#inputthis").hide();
        $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ","; });
        arrChk = arrChk.substr(0, arrChk.length - 1);
        var url = "AjaxService.aspx?action=DeleteCruisesDamage&Id=" + arrChk + "&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                jSuccess('<strong>信息删除成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                $('#GridView_Refresh_Button').click();
            } else {
                jError('<strong>' + date.success + '</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
        })
    }
</script>
</body>
</html>
