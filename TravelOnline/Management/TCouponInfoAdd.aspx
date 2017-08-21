<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TCouponInfoAdd.aspx.cs" Inherits="TravelOnline.Management.TCouponInfoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/WriteJournal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" charset="utf-8">
         window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script type="text/javascript" src="/ueditor/ueditor.config.js"></script>  
    <script type="text/javascript" src="/ueditor/ueditor.all.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
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
    <span class=clr></span>
   
    <div id=select class="m select">
    <div class=mt><H1></H1><strong>新增活动信息</strong></div>
        <form id="form_data" onsubmit="return false;" method="post">
        <input id="tradingAreaId" name="tradingAreaId" type="hidden" value="<%= tradingAreaId%>" />
        <div id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
        </div>
    <dl><dt>开始日期：</dt>
        <dd>
            <input value="<%=starDate %>" class="iconDate" type="text" name="starDate" id="starDate" maxlength="10" style="width: 110px;" />
        </dd>
    </dl>
    <dl><dt>结束日期：</dt>
        <dd>
            <input value="<%=endDate %>" class="iconDate" type="text" name="endDate" id="endDate" maxlength="10" style="width: 110px;" />
        </dd>
    </dl>
    <dl><dt>优惠券内容：</dt>
        <dd>
            <textarea name="context" rows="3" cols="38"><%=context %></textarea>
        </dd>
    </dl>
    <dl><dt>barcode：</dt>
        <dd>
            <input id="barcode" name="barcode" type="text" maxlength="500" size="50" value="<%=barcode %>"/>
        </dd>
    </dl>
    </div>
   </form>
    <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
        <%=buttoninfo %>
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
                catch (e) {; }
            }
            return;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#starDate').calendar({ maxDate: '#endDate', btnBar: false });
            $('#endDate').calendar({ minDate: '#starDate', btnBar: false });

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


            if ($("#starDate").val() == "") {
                jNotify('<strong>请输入开始时间</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }


            if ($("#endDate").val() == "") {
                jNotify('<strong>请输入结束时间!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#context").val() == "") {
                jNotify('<strong>请输入优惠券内容!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#barcode").val() == "") {
                jNotify('<strong>请输入barcode!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SaveTCouponInfo&r=" + Math.random();
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
