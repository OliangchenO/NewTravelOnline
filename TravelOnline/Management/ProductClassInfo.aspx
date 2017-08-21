<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductClassInfo.aspx.cs" Inherits="TravelOnline.Management.ProductClassInfo" %>
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
    <script type="text/javascript" src="/Scripts/lhgselect/lhgselect.min.js"></script>
    <script type="text/javascript" src="/Js/ProductClass/ProductClass.js"></script>
    <style>
        .select {WIDTH: 370px;}
        .select DL {WIDTH: 360px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 260px;}
    </style>
        <script type="text/javascript">
            $(function () {
                $('#sel1,#sel2').select({ data: data, field_name: '#SelectClass', value: '<%=ClassPath %>' });
            });

            function myrefresh() {
                var url = "AjaxService.aspx?action=CreateProductClassSelect&r=" + Math.random();
                $.getJSON(url, function (date) {
                });
                window.location.reload();
            }
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <DIV id="inputs" style="DISPLAY:none">
        <input id="Cid" type="hidden" value="<%=id %>"/>
        <input id="SelectClass" type="hidden" value=""/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>旅游产品分类导航管理</STRONG></DIV>
    <DL class=fore><DT>业务板块：</DT>
        <DD>
            <select name="Select1" id="Select1" style="width:200px;">
	            <option value="OutBound">出境旅游</option>
	            <option value="InLand">国内旅游</option>
	            <option value="FreeTour">自由行</option>
	            <option value="Cruises">邮轮</option>
	            <option value="Visa">签证</option>
            </select>
        </DD>
    </DL>
    <DL class=fore><DT>一级分类：</DT>
        <DD>
            <select id="sel1" class="city" style="width:200px;" <%=setddl %>></select>
        </DD>
    </DL>
    <DL><DT>二级分类：</DT>
        <DD>
            <select id="sel2" class="city" style="width:200px;" <%=setddl %>></select>
        </DD>
    </DL>
    <DL><DT>类别名称：</DT>
        <DD>
            <input name="ProductName" type="text" id="ProductName" style="width:200px;" value="<%=ProductName %>" maxlength="20" />
        </DD>
    </DL>
    <DL><DT>链接地址：</DT>
        <DD>
            <input name="ProductUrl" type="text" id="ProductUrl" style="width:200px;" value="<%=ProductUrl %>"  maxlength="100" />
        </DD>
    </DL>
    <DL><DT>畅游Id：</DT>
        <DD>
            <input id="MisClassId" type="text" class="easyui-numberbox" precision="0" max="999999" size="4" maxlength="6" style="text-align:center;" value="<%=MisClassId %>"/>
        </DD>
    </DL>
    <DL><DT>排序：</DT>
        <DD>
            <input id="ProductSort" type="text" class="easyui-numberbox" precision="0" max="250" size="4" maxlength="3" style="text-align:center;" value="<%=ProductSort %>"/>
        </DD>
    </DL>
    <DL><DT></DT>
        <DD>
        </DD>
    </DL>
    <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" 
            iconCls="icon-reload" style="margin-left: 150px;margin-top: 10px;">刷新分类</a>
     <a id="SaveInfo" onclick="check_null()" class="easyui-linkbutton" plain="true" 
            iconCls="icon-save" style="margin-left: 50px;margin-top: 10px;">保存</a>
    </DIV>
    </form>
    <script type="text/javascript">
        setTimeout(function () { $("#Select1").val("<%=ProductType%>"); }, 1);

        function resetit() {
            $("#ProductName").val("");
            $("#ProductUrl").val("");
            $("#MisClassId").val("");
            $("#ProductSort").val("");
        }

        function check_null() {
            if ($("#ProductName").val() == "") {
                jNotify('<strong>请输入类别名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

//            if ($("#ProductUrl").val() == "") {
//                jNotify('<strong>请输入链接地址!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                return false;
//            }

            var url = "AjaxService.aspx?action=SaveProductClass&sel1=" + $("#sel1").val() + "&sel2=" + $("#sel2").val() + "&ProductType=" + $("#Select1").val() + "&Id=" + $("#Cid").val() + "&ProductName=" + escape($("#ProductName").val()) + "&ProductUrl=" + $("#ProductUrl").val().replace(/\//g, "@") + "&MisClassId=" + $("#MisClassId").val() + "&ProductSort=" + $("#ProductSort").val() + "&r=" + Math.random();
            //alert(url);
            //window.open(url);
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    if ($("#Cid").val() == "") {
                        resetit();
                    }
                }
                else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }
    </script> 
</body>
</html>
