<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyDetail.aspx.cs" Inherits="TravelOnline.Company.CompanyDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司信息</title>
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
        <div class=toolbar_input>
            <a href="javascript:void(0)" class="tools <%=hide %>" id="addnew" onclick="addcompany()"><img src="../images/icon/add.png" class=img20>新增</a>
            <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
        </div>
        <div class="clear"></div>
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input id="Cid" name="Cid" type="hidden" value="<%=Cid %>"/>
            </DIV>
            <div class=line_input>
                所属地区：<select id="s_province" name="s_province" style="width: 80px"></select>&nbsp;&nbsp;
                <select id="s_city" name="s_city" style="width: 150px"></select>
                <select id="s_county" name="s_county" class=hide></select>&nbsp;&nbsp;
                默认收款方式：<select id="RebateFlag" name="RebateFlag"  style="width: 148px">
                <option selected="selected" value="0">按订单全额收款</option>
                <option value="1">按订单结算价收款</option>
            </select>
            </div> 
            <div class=line_input>
                公司名称：<input value="<%=companyname %>" class=ipt type="text" name="companyname" id="companyname" maxlength="100" style="width: 300px" />&nbsp;&nbsp;
                畅游代码：<input id="misid" name="misid" type="text" class="ipt easyui-numberbox" precision="0" max="99999999" style="width: 100px;text-align:center;" maxlength="8" value="<%=misid %>"/>
            </div>
            <div class=line_input>
                联系地址：<input value="<%=address %>" class=ipt type="text" name="address" id="address" maxlength="100"  style="width: 300px" />&nbsp;&nbsp;
                邮政编码：<input value="<%=zipcode %>" class=ipt type="text" name="zipcode" id="zipcode" maxlength="6"  style="width: 100px" />
            </div>
            <div class=line_input>
                联系电话：<input value="<%=tel %>" class=ipt type="text" name="tel" id="tel" maxlength="50"  style="width: 212px" />&nbsp;&nbsp;
                传真：<input value="<%=fax %>" class=ipt type="text" name="fax" id="fax" maxlength="50"  style="width: 212px" />&nbsp;&nbsp;
            </div>
            <div class=line_input><br></div>
        </form>
    </div>
</div>
<script type="text/javascript" src="/Scripts/area.js"></script>
<script type="text/javascript">
    _init_area();

    $(document).ready(function () {
        $("#s_province").val("<%=area%>");
        change(1);
        $("#s_city").val("<%=city%>");
        $("#RebateFlag").val("<%=RebateFlag%>");
    });

    $("#save").click(function () {
        if ($("#s_city").val() == "") {
            jError('<strong>请选择所属地区!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }

        if ($("#companyname").val() == "" || $("#misid").val() == "") {
            jError('<strong>公司名称和畅游代码都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            $("#companyname").focus();
            return;
        }
        $(this).hide();
        $.post("AjaxService.aspx?action=CompanyInfo&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") addcompany();
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        $(this).show();
    });

    function addcompany() {
        $("#companyname").val("");
        $("#address").val("");
        $("#zipcode").val("");
        $("#tel").val("");
        $("#fax").val("");
        $("#Cid").val("");
        $("#misid").val("");
        $("#RebateFlag").val("0");
    }
</script>
</body>
</html>


