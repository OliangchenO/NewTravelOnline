<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TravelOnline.Test.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <asp:TextBox ID="TextBox1" runat="server" Width="246px"></asp:TextBox>
        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" 
            GroupName="sex" Text="男" />
        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="sex" Text="女" />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    

     <asp:GridView ID="GridView1" BorderStyle="Solid" BorderWidth="1px" BorderColor="#73B7EA"
    Width="100%" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
    BackColor="White" CellPadding="4" GridLines="None"   OnPageIndexChanging="GridView_PageIndexChanging" PageSize="10"
    AllowPaging="true" OnDataBound="GridView_DataBound">

<Columns>
    <asp:TemplateField HeaderText="选择" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <asp:CheckBox ID="CheckBox3" runat="server"  />
        </ItemTemplate>
    </asp:TemplateField> 
	<asp:BoundField DataField="id" HeaderText="ID" SortExpression="id"  />
	<asp:BoundField DataField="name" HeaderText="NAME" SortExpression="name" />
	<asp:BoundField DataField="age" HeaderText="AGE" SortExpression="age" />
    <asp:TemplateField HeaderText="操作">
        <ItemTemplate>
        </ItemTemplate>
    </asp:TemplateField> 
</Columns>

<PagerTemplate>
        <div style="text-align: center;"><span style="color: Blue">
            共有<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;
            第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;&nbsp;
            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">首页</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">上一页</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"  Enabled="<%#     ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">下一页</asp:LinkButton>&nbsp;
            <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">尾页</asp:LinkButton>&nbsp;&nbsp;
            到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
            <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
    </PagerTemplate>

</asp:GridView>
    </div>
    <input type="checkbox" name="chk_list" id="chk_list_1" value="1" />1<br /> 
<input type="checkbox" name="chk_list" id="chk_list_2" value="2" />2<br /> 
<input type="checkbox" name="chk_list" id="chk_list_3" value="3" />3<br /> 
<input type="checkbox" name="chk_list" id="chk_list_4" value="4" />4<br /> 
<input type="checkbox" name="chk_all" id="chk_all" />全选/取消全选 
<script type="text/javascript">
    $("#chk_all").click(function () {
        $("input[name='chk_list']").attr("checked", $(this).attr("checked"));
    }); 
</script> 
    </form>
</body>
</html>
