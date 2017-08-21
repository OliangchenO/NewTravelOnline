<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferSend.aspx.cs" Inherits="TravelOnline.Management.PreferSend" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <style>
        .select {WIDTH: 400px;}
        .select DL {WIDTH: 400px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 300px;}
    </style>
        <script type="text/javascript">

	    </script>
</head>
<body>
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>优惠券赠送发放</STRONG></DIV>
    <DL class=fore><DT>发放类型：</DT>
        <DD><%=sellflag%> &nbsp;<b>抵扣方式：</b><%=deduction%> &nbsp;<b>使用范围：</b><%=range%></DD>
    </DL>
    <DL><DT>有效期：</DT>
        <DD><%=begindate %> &nbsp;至&nbsp; <%=enddate %></DD>
    </DL>
    <DL><DT>面值：</DT>
        <DD><%=par %> &nbsp;<b>销售价：</b><%=sellprice %> &nbsp;<b>消费额度：</b><%=amount %></DD>
    </DL>
    <DL><DT>使用说明：</DT>
        <DD><%=memo%></DD>
    </DL>
    <DIV class=mt><H1></H1><STRONG>优惠券发送选项</STRONG></DIV>
    <DL class=fore><DT><input type="checkbox" name="CheckBox1" id="CheckBox1" value="11" />登陆日期：</DT>
        <DD>
            <input class="iconDate" type="text" name="begindate" id="begindate" maxlength="10" style="width: 100px;" readonly="readonly"/>&nbsp;至&nbsp;
            <input class="iconDate" type="text" name="enddate" id="enddate" maxlength="10" style="width: 100px;" readonly="readonly"/>
        </DD>
    </DL>
    <DL><DT><input type="checkbox" name="CheckBox2" id="CheckBox2" value="22" />订单总额：</DT>
        <DD>
            <input id="price1" name="price1" type="text" class="easyui-numberbox" precision="0" max="999999" maxlength="10" style="width: 99px;text-align:center;"/>&nbsp;至&nbsp;
            <input id="price2" name="price2" type="text" class="easyui-numberbox" precision="0" max="999999" maxlength="10" style="width: 98px;text-align:center;"/>
        </DD>
    </DL>
    <DL><DT><input type="checkbox" name="CheckBox3" id="CheckBox3" value="33" />年龄范围：</DT>
        <DD>
            <input id="age1" name="age1" type="text" class="easyui-numberbox" precision="0" max="99" maxlength="2" style="width: 99px;text-align:center;"/>&nbsp;至&nbsp;
            <input id="age2" name="age2" type="text" class="easyui-numberbox" precision="0" max="99" maxlength="2" style="width: 98px;text-align:center;"/>
        </DD>
    </DL>
    <DL><DT><input type="checkbox" name="CheckBox4" id="CheckBox4" value="44" />登陆账号：</DT>
        <DD>
            <input id="email" name="email" type="text" style="width: 226px;"/>
        </DD>
    </DL>
    <DL><DT>查询结果：</DT>
        <DD><span name="infos" id="infos">发放之前请先查询记录数量</span></DD>
    </DL>
     <a id="A1" onclick="LoadInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">查询</a>
     <a id="A2" onclick="check_null()" class="easyui-linkbutton" plain="true" iconCls="icon-save" style="margin-left: 60px;margin-top: 10px;">发放</a>
    </DIV>
    </form>
    <script type="text/javascript">
        $(function () {
            $('#begindate').calendar({ maxDate: '#begindate', btnBar: false });
            $('#enddate').calendar({ minDate: '#enddate', btnBar: false });

        });

        function check_null() {
            //alert($("#form_data").serialize());
            //return false;
            if ($("#CheckBox1").prop("checked") == true) {
                if ($("#begindate").val() == "" || $("#enddate").val() == "") {
                    jNotify('<strong>登陆日期均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            if ($("#CheckBox2").prop("checked") == true) {
                if ($("#price1").val() == "" || $("#price1").val() == "") {
                    jNotify('<strong>订单总额均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            if ($("#CheckBox3").prop("checked") == true) {
                if ($("#age1").val() == "" || $("#age1").val() == "") {
                    jNotify('<strong>年龄范围均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }

            if ($("#CheckBox4").prop("checked") == true) {
                if ($("#email").val() == "") {
                    jNotify('<strong>登陆账号不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            var url = "AjaxService.aspx?action=PreferSend&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>' + obj.success + '!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }

        function LoadInfo() {
            //alert($("#form_data").serialize());
            //return false;
            if ($("#CheckBox1").prop("checked") == true) {
                if ($("#begindate").val() == "" || $("#enddate").val() == "") {
                    jNotify('<strong>登陆日期均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            if ($("#CheckBox2").prop("checked") == true) {
                if ($("#price1").val() == "" || $("#price1").val() == "") {
                    jNotify('<strong>订单总额均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            if ($("#CheckBox3").prop("checked") == true) {
                if ($("#age1").val() == "" || $("#age1").val() == "") {
                    jNotify('<strong>年龄范围均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            var url = "AjaxService.aspx?action=PreferSerch&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    $("#infos").html(obj.success);
                }
                if (obj.error) {
                    $("#infos").html("没有查询到任何数据");
                }
            });
        }
    </script> 
</body>
</html>

