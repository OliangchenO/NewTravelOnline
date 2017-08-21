<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdjustPrice.aspx.cs" Inherits="TravelOnline.OrderEdit.AdjustPrice" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style>
        .select {WIDTH: 310px;}
        .select DL {WIDTH: 300px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 200px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <DIV id="inputs" style="DISPLAY:none">
        <input id="OrderId" type="hidden" value="<%=OrderId %>"/>
        <input id="flag" type="hidden" value="<%=flag %>"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>费用增加或减少</STRONG></DIV>
    <DL class=fore><DT>
        </DT>
        <DD>
            <input name="fee" id="Radio1" type="radio" value="1" checked=checked />费用增加&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <input name="fee" id="Radio2" type="radio" value="2"  />费用减少</DD>
    </DL>
    <DL><DT>费用说明：</DT>
        <DD>
            <asp:TextBox ID="TB_PriceName" runat="server" Width="150" MaxLength="50"></asp:TextBox>
        </DD>
    </DL>
    <DL><DT>单价：</DT>
        <DD>
            <input name="TB_Price" id="TB_Price" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="6" style="text-align:center;"/>
        </DD>
    </DL>
    <DL><DT>人数：</DT>
        <DD>
            <input name="TB_Num" id="TB_Num" type="text" class="easyui-numberbox" precision="0" max="999" size="10" maxlength="3" style="text-align:center;"/>
        </DD>
    </DL>
    <DL><DT>调整总额：</DT>
        <DD>
            <input name="TB_All" id="TB_All" type="text" class="easyui-numberbox" precision="0" max="99999999" size="10" maxlength="8" style="text-align:center;" readonly="readonly" />
        </DD>
    </DL>
    <DL><DT></DT>
        <DD>
           
        </DD>
    </DL>
     <a id="SaveInfo" onclick="allchks()" class="easyui-linkbutton" plain="true" 
            iconCls="icon-save" style="margin-left: 180px;margin-top: 10px;">保存</a>
    </DIV>
    </form>
    <script type="text/javascript">
        function allchks() {
            if ($("#TB_PriceName").val() == "") {
                jNotify('<strong>请输入费用说明!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#TB_Price").val() == "") {
                jNotify('<strong>请输入金额!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            SaveUserInfo();
        }

        $("#TB_Price").bind("keyup", function () {
            SumAllPrice();
        })
        $("#TB_Num").bind("keyup", function () {
            SumAllPrice();
        })

        function SumAllPrice() {
            var PriceSum = 0;
            PriceSum = Number($("#TB_Price").val()) * Number($("#TB_Num").val());
            $("#TB_All").val(PriceSum);
        }

        function SaveUserInfo() {
            var items = $("input[name='fee']:checked").val();
            var url = "/Management/AjaxService.aspx?action=AdjustPrice&OrderId=" + $("#OrderId").val() + "&Cruises=" + $("#flag").val() + "&PriceName=" + escape($("#TB_PriceName").val()) + "&Price=" + $("#TB_Price").val() + "&Nums=" + $("#TB_Num").val() + "&Flag=" + items + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == "OK") {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                } else {
                    jError('<strong>' + date.success + '</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }
    </script> 
</body>
</html>

