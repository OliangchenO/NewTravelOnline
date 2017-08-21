<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferInfoAdd.aspx.cs" Inherits="TravelOnline.Management.PreferInfoAdd" %>
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
    <DIV class=mt><H1></H1><STRONG>新增优惠信息</STRONG></DIV>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
    </DIV>
    <DL><DT>旅游线路编号：</DT>
        <DD>
            <input id="LineId" name="LineId" type="text" style="width: 240px;" maxlength="500" value="<%=LineId %>"/>
        </DD>
    </DL>
    <DL><DT>出团日期：</DT>
        <DD>
            <input value="<%=startDate %>" class="iconDate" type="text" name="startDate" id="startDate" maxlength="10" style="width: 110px;" readonly="readonly"/>&nbsp;至&nbsp;
            <input value="<%=endDate %>" class="iconDate" type="text" name="endDate" id="endDate" maxlength="10" style="width: 110px;" readonly="readonly"/>
        </DD>
    </DL>
    <DL><DT>报名日期：</DT>
        <DD>
            <input value="<%=pStartDate %>" class="iconDate" type="text" name="pStartDate" id="pStartDate" maxlength="10" style="width: 110px;" readonly="readonly"/>&nbsp;至&nbsp;
            <input value="<%=pEndDate %>" class="iconDate" type="text" name="pEndDate" id="pEndDate" maxlength="10" style="width: 110px;" readonly="readonly"/>
        </DD>
    </DL>
    <DL><DT>优惠金额：</DT>
        <DD>
            <input id="preferAmount" name="preferAmount" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=preferAmount %>"/>&nbsp;&nbsp;
        </DD>
    </DL>
   </form>
     <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
     <%=buttoninfo %>
    </DIV>

    <script type="text/javascript">

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
        $(function () {
            $('#startDate').calendar({ maxDate: '#endDate', btnBar: false });
            $('#endDate').calendar({ minDate: '#startDate', btnBar: false });
            $('#pStartDate').calendar({ maxDate: '#pStartDate', btnBar: false });
            $('#pEndDate').calendar({ minDate: '#pEndDate', btnBar: false });
            
        });

        function check_null() {

//            if ($("#sellflag").val() == "3") {
//                if ($("#range").val() == "5" || $("#range").val() == "9") {
//                }
//                else {
//                    jNotify('<strong>公共券号，使用范围只能选邮轮产品或按指定产品两种</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                    return false;
//                }
//            }

            
            if ($("#LineId").val() == "") {
                 jNotify('<strong>请输入线路编号</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                 return false;
            }
            

            if ($("#preferAmount").val() == "") {
                jNotify('<strong>优惠金额不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SavePreferInfo&r=" + Math.random();
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
            var url = "AjaxService.aspx?action=SavePreferNums&r=" + Math.random();
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
