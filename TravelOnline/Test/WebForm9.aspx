<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="TravelOnline.Test.WebForm9" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    &nbsp;111111<font style="color: #800000; font-size: 14px; font-weight: bold">您当前预定的是呼叫中心订单</font>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:CheckBox ID="CheckBox2" 
                        runat="server" Text="aabccc" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>


        <div tgs=1 tns="111" id=show1 class="CheckBoxList">
        <DIV><input type='checkbox' onclick='chkall(this,1)' name='chk_all' id='chk_all'>全选</DIV>
        <DIV><input class="ChkIt" type=checkbox id="CB1-1" name=CheckBox1 tgs="哎哎1/,,1,2,/1" onclick="SelectIts(this,1)" value="1" checked=checked/>哎哎1/,,1,2,/1</DIV>
        <DIV><input class="ChkIt" type=checkbox id="CB1-2" name=CheckBox1 tgs="哎哎2/,,1,2,/2" onclick="SelectIts(this,1)" value="2" checked=checked/>哎哎2/,,1,2,/2</DIV>
        <DIV><input class="ChkIt" type=checkbox id="CB1-3" name=CheckBox1 tgs="哎哎3/,,1,2,/3" onclick="SelectIts(this,1)" value="3" />哎哎3/,,1,2,/3</DIV>
        <DIV><input class="ChkIt" type=checkbox id="CB1-4" name=CheckBox1 tgs="哎哎4/,,1,2,/4" onclick="SelectIts(this,1)" value="4" />哎哎4/,,1,2,/4</DIV>
        </div>

        <table style="width: 100%;">
            <tr>
                <td rowspan="3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr><td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr><td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <A class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">下一步</A>
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="清除缓存" />
    &nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="显示缓存" />
    &nbsp;<asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="东京海上测试" />
    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="游记上级id" />
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
        Text="美亚保险服务器测试" />
    <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
        Text="景点拼音" />
    <asp:Button ID="Button8" runat="server" onclick="Button8_Click" 
        Text="seolink" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
    <script type="text/javascript">
        
        function GoToNext() {
            alert($("#CheckBox2").val());
            alert($("#CheckBox2").html());
            
        }
    </script>
</body>
</html>
