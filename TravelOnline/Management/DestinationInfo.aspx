<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DestinationInfo.aspx.cs" Inherits="TravelOnline.Management.DestinationInfo" %>
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
    <script type="text/javascript" src="/Js/DestinationClass.js"></script>
    <script type="text/javascript" src="/Js/Destination/DestinationClass.js"></script>
    <style>
        .select {WIDTH: 370px;}
        .select DL {WIDTH: 360px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 260px;}
    </style>
        <script type="text/javascript">
            $(function () {
                $('#sel1,#sel2,#sel3').select({ data: data, field_name: '#SelectClass', value: '<%=ClassPath %>' });
            });

            function myrefresh() {
                var url = "AjaxService.aspx?action=CreateDestinationSelect&r=" + Math.random();
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
        <input id="OldName" type="OldName" value="<%=OldName %>"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>目的地信息</STRONG></DIV>
    <DL class=fore><DT>目的地大类：</DT>
        <DD>
            <select name="Select1" id="Select1" style="width:206px;">
	            <option value="InLand">国内</option>
	            <option value="OutBound">境外</option>
            </select>
        </DD>
    </DL>
    <DL class=fore><DT>一级分类：</DT>
        <DD>
            <select id="sel1" class="city" style="width:206px;" <%=setddl %>></select>
        </DD>
    </DL>
    <DL><DT>二级分类：</DT>
        <DD>
            <select id="sel2" class="city" style="width:206px;" <%=setddl %>></select>
        </DD>
    </DL>
    <DL><DT>三级分类：</DT>
        <DD>
            <select id="sel3" class="city" style="width:206px;" <%=setddl %>></select>
        </DD>
    </DL>
    <DL><DT>目的地名称：</DT>
        <DD>
            <input name="DestinationName" type="text" id="DestinationName" style="width:200px;" value="<%=DestinationName %>" maxlength="100" />
        </DD>
    </DL>
    <DL><DT>英文名称：</DT>
        <DD>
            <input name="Ename" type="text" id="Ename" style="width:200px;" value="<%=Ename %>"  maxlength="100" />
        </DD>
    </DL>
    <DL><DT>拼音：</DT>
        <DD>
            <input name="PinYin" type="text" id="PinYin" style="width:200px;" value="<%=PinYin %>"  maxlength="100" />
        </DD>
    </DL>
    <DL><DT>拼音缩写：</DT>
        <DD>
            <input name="SortPinYin" type="text" id="SortPinYin" style="width:200px;" value="<%=SortPinYin %>"  maxlength="50" />
        </DD>
    </DL>
    <DL><DT>baidu地图 X：</DT>
        <DD>
            <input name="map_x" type="text" id="map_x" style="width:200px;" value="<%=map_x %>"  maxlength="50" />
        </DD>
    </DL>
    <DL><DT>baidu地图 Y：</DT>
        <DD>
            <input name="map_y" type="text" id="map_y" style="width:200px;" value="<%=map_y %>"  maxlength="50" />
        </DD>
    </DL>
    <DL><DT>地图缩放级别：</DT>
        <DD>
            <input name="map_size" id="map_size" type="text" class="easyui-numberbox" precision="0" max="18" size="4" maxlength="2" style="text-align:center;" value="<%=map_size %>"/>
        </DD>
    </DL>
    
    <DL><DT>排序：</DT>
        <DD>
            <input name="SortNum" id="SortNum" type="text" class="easyui-numberbox" precision="0" max="250" size="4" maxlength="3" style="text-align:center;" value="<%=SortNum %>"/>
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
        $(document).ready(function () {
            setTimeout(function () { $("#Select1").val("<%=Dtype%>"); }, 1);

            $("#DestinationName").blur(function () {
                $(this).GetPinYin();
            });
        });

        function resetit() {
            $("#DestinationName").val("");
            $("#Ename").val("");
            $("#SortNum").val("");
            $("#PinYin").val("");
            $("#SortPinYin").val("");
            $("#OldName").val("");
        }

        function check_null() {
            if ($("#DestinationName").val() == "") {
                jNotify('<strong>请输入类别名称!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SaveDestinationClass&sel1=" + $("#sel1").val() + "&sel2=" + $("#sel2").val() + "&sel3=" + $("#sel3").val() + "&Dtype=" + $("#Select1").val() + "&Id=" + $("#Cid").val() + "&DestinationName=" + escape($("#DestinationName").val()) + "&SortNum=" + $("#SortNum").val() + "&Ename=" + $("#Ename").val() + "&PinYin=" + $("#PinYin").val() + "&SortPinYin=" + $("#SortPinYin").val() + "&map_x=" + $("#map_x").val() + "&map_y=" + $("#map_y").val() + "&map_size=" + $("#map_size").val() + "&r=" + Math.random();
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

        jQuery.fn.GetPinYin = function () {
            var cnname = $.trim($(this).val());
            if (cnname.length < 2 || cnname == "") {
                return false;
            }
            var oldname = $("#OldName").val();
            if (oldname != cnname) {
                var url = "AjaxService.aspx?action=GetPinYin&CnName=" + escape(cnname) + "&r=" + Math.random();
                $.getJSON(url, function (date) {
                    $("#PinYin").val(date.py);
                    $("#SortPinYin").val(date.sortpy);
                })
                $("#OldName").val(cnname);
            }
        };
    </script> 
</body>
</html>
