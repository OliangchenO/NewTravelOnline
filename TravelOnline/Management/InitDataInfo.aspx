<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitDataInfo.aspx.cs" Inherits="TravelOnline.Management.InitDataInfo" %>
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
        .select {WIDTH: 300px;}
        .select DL {WIDTH: 300px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 200px;}
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
    <DIV class=mt><H1></H1><STRONG>修改信息</STRONG></DIV>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="ID" id="ID" type="hidden" value="<%=id %>"/>
    </DIV>
    <DL><DT>参数类型：</DT>
        <DD>
            <select name="ftype" id="ftype" style="width:100px;">
	            <option value="SeriesType|船队系列">船队系列</option>
	            <option value="RoomType|舱房类型">舱房类型</option>
                <option value="Summary|目的地概况">目的地概况</option>
                <option value="Traffic|交通概况">交通概况</option>
            </select>
        </DD>
    </DL>
    <DL><DT>基础数据名称：</DT>
        <DD>
            <input id="dataname" name="dataname" type="text" style="width: 100px;" maxlength="20" value="<%=dataname %>"/>
        </DD>
    </DL>
    <DL><DT>排序：</DT>
        <DD>
            <input id="sort" name="sort" type="text" style="width: 100px;" maxlength="20" value="<%=sort %>"/>
        </DD>
    </DL>
    </form>
     <%=buttoninfo %>
    </DIV>
    <script type="text/javascript">
        function check_null() {
            var url = "AjaxService.aspx?action=SaveInitDataInfo&r=" + Math.random();
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
    </script> 
</body>
</html>
