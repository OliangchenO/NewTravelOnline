<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInfoAdd.aspx.cs" Inherits="TravelOnline.Management.GroupInfoAdd" %>
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
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <style>
        .select {WIDTH: 400px;}
        .select DL {WIDTH: 400px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 300px;}
    </style>
    <style>
        .upp {WIDTH: 220px;}
        .width120 {WIDTH: 100px;}
        .bnP1{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad1.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        .bnP2{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        input[type='file'] {
         width:60px;
         height:26px;
         CURSOR: hand;
         line-height:26px;
         position:relative;
         opacity:0;                        /*设置它的透明度为0，即完全透明。这个语句，对付除IE以外的浏览器*/
         filter:alpha(opacity=0);    /*设置它的透明度为0，即完全透明。这个语句，对付IE浏览器。*/
        }
    </style>
        <script type="text/javascript">
            function myrefresh() {
                window.location.reload();
            }
	</script>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>新增拼团信息</STRONG></DIV>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
    </DIV>
    <DL><DT>旅游线路编号：</DT>
        <DD>
            <input id="MisLineId" name="MisLineId" type="text" style="width: 240px;" maxlength="500" value="<%=MisLineId %>"/>
        </DD>
    </DL>
    <DL><DT>出团日期：</DT>
        <DD>
            <input value="<%=GroupDate %>" class="iconDate" type="text" name="GroupDate" id="GroupDate" maxlength="10" style="width: 110px;" readonly="readonly"/>&nbsp;
        </DD>
    </DL>
    <DL><DT>订金金额：</DT>
        <DD>
            <input id="pre_price" name="pre_price" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=pre_price %>"/>&nbsp;&nbsp;
        </DD>
    </DL>
    <DL><DT>优惠金额：</DT>
        <DD>
            <input id="Discount" name="Discount" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=Discount %>"/>&nbsp;&nbsp;
        </DD>
    </DL>
    <DL><DT>成团人数：</DT>
        <DD>
            <input id="Num" name="Num" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=Num %>"/>&nbsp;&nbsp;
        </DD>
    </DL>
   </form>
     <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
     <%=buttoninfo %>
    </DIV>

    <script type="text/javascript">
        $(function () {
            $('#GroupDate').calendar({btnBar: false });
        });

        function checkit(obj, flag) {
            var d = new UpLoadFileCheck();
            d.CheckExt(obj, flag)
        }

        function sleep(num) {
            var tempDate = new Date();
            var tempStr = "";
            var theXmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            while ((new Date() - tempDate) < num) {
                tempStr += "\n" + (new Date() - tempDate);
                try {
                    theXmlHttp.open("get", "about:blank?JK=" + Math.random(), false);
                    theXmlHttp.send();
                }
                catch (e) { ; }
            }
            return;
        }
    </script>
    <script type="text/javascript">

        function check_null() {
            if ($("#MisLineId").val() == "") {
                 jNotify('<strong>请输入线路编号</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                 return false;
            }
            

            if ($("#Discount").val() == "") {
                jNotify('<strong>优惠金额不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SaveGroupInfo&r=" + Math.random();
            $.post(url, $("#form_data").serialize(),function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }

        function savenums() {
            var url = "AjaxService.aspx?action=SaveGroupNums&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>
