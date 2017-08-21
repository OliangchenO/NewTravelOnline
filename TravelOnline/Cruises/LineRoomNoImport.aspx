<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineRoomNoImport.aspx.cs" Inherits="TravelOnline.Cruises.LineRoomNoImport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>船队信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="lineid" id="lineid" type="hidden" value="<%=lineid %>"/>
    </DIV>
    <div class=toolbar_inputa>
        <asp:LinkButton ID="LinkButton1" class="tools" runat="server" 
            onclick="LinkButton1_Click"><img src="../images/icon/Spell.png" class=img20>导入</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput>邮轮公司：</div>
        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
            <asp:ListItem Value="1">皇家加勒比</asp:ListItem>
            <asp:ListItem Value="2">歌诗达</asp:ListItem>
            <asp:ListItem Value="3">诺唯真</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class=line_input>
        <div class=firstinput>选择文件：</div>
        <asp:FileUpload ID="filePath" Width="450px" runat="server" />
    </div>
    <div class="line_input <%=hide1 %>">
        <div class=firstinput>房号模版：</div>
        <A class=tools href="/OfficeFiles/CruisesModel/RoyalCaribbean/CaribbeanRoomNo.xls" target="_blank">皇家加勒比</A>&nbsp;&nbsp;&nbsp;&nbsp;<A class=tools href="/OfficeFiles/CruisesModel/Costa/CostaRoomNo.xls" target="_blank">歌诗达</A>&nbsp;&nbsp;&nbsp;&nbsp;<A class=tools href="/OfficeFiles/CruisesModel/Norvegian/NorvegianCruiseRoomNo.xls" target="_blank">诺唯真</A>
    </div>
    <div class="line_input <%=hide2 %>">
        <div class=firstinput>餐桌模版：</div>
        <A class=tools href="/OfficeFiles/CruisesModel/RoyalCaribbean/CaribbeanDinner.xls" target="_blank">皇家加勒比</A>&nbsp;&nbsp;&nbsp;&nbsp;<A class=tools href="/OfficeFiles/CruisesModel/Costa/CostaRoomNo.xls" target="_blank">歌诗达</A>
    </div>
    <div class="line_input <%=hide3 %>">
        <div class=firstinput>Booking：</div>
        <A class=tools href="/OfficeFiles/CruisesModel/RoyalCaribbean/BookingModel.xls" target="_blank">皇家加勒比</A>&nbsp;&nbsp;&nbsp;&nbsp;<A class=tools href="/OfficeFiles/CruisesModel/Costa/BookingModel.xls" target="_blank">歌诗达</A>
    </div>
    <div class=line_input>
        <div class=firstinput>导入结果：</div>
    </div>
    <div class=line_input>
        <div style="padding-left: 20px; font-size: 12px; line-height: 25px;"><%=InputResult %></div>
    </div>
    </DIV>
</form>
    <script type="text/javascript"></script> 
</body>
</html>



