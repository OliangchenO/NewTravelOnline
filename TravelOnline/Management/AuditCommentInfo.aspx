<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditCommentInfo.aspx.cs" Inherits="TravelOnline.Management.AuditCommentInfo" %>

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
    <div class=mt><H1></H1><strong>审核点评信息</strong></div>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
        </DIV>
    <dl><dt>线路名称：</dt>
        <dd>
            <input id="lineName" name="lineName" type="text" size="50" maxlength="500" value="<%=lineName %>"/>
        </dd>
    </dl>
    <dl><dt>订单编号：</dt>
        <dd>
            <input id="orderId" name="orderId" type="text" maxlength="500" size="50" style="text-align:center;" value="<%=orderId %>"/>
    </dl>
    <dl><dt>评级：</dt>
        <dd>
            <input class=sel type="text" name="rank" id="rank" maxlength="10" style="width: 120px" value="<%=rank %>"/>
        </dd>
    </dl>
    <dl><dt>点评内容：</dt>
        <dd>
            <textarea name="context" rows="3" cols="38"><%=context %></textarea>
        </dd>
    </dl>
    <dl><dt>点评图片：</dt>
        <%if (pics != null)
          {
              foreach (string img in pics)
              {%>
        <dd>
            <img id="Img1" alt="分辨率为 480*150" src="<%=img%>" />
        </dd>
        <%}
          } %>
    </dl>
    </div>
    </DIV>
   </form>
     <%=buttoninfo %>
    </div>

    <script type="text/javascript">

        function afterselet(callback) {
            var des_id = $("#destid").val();
            if (des_id != "") {
                $("#destname").focus();
            }
        }

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

        function check_null() {

            var url = "AjaxService.aspx?action=AuditCommentInfo&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>审核成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
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
