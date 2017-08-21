<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerAdInfo.aspx.cs" Inherits="TravelOnline.Management.BannerAdInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <style>
        .select {WIDTH: 450px;}
        .inputdiv {PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 10px; PADDING-RIGHT: 0px; PADDING-TOP: 5px}
        table {border: solid 1px #e8eef4;border-collapse: collapse;}
        table td{padding: 10px 5px 10px 5px;border: solid 1px #e8eef4;}
        table th{padding: 5px 5px;text-align: left;background-color: #e8eef4;border: solid 1px #e8eef4;}
        .upp {WIDTH: 450px;}
        .width120 {WIDTH: 250px;}
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
        var ImgObj1 = new Image();
        
        UpLoadFileCheck = function () {
            this.AllowExt = ".jpg,.gif"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
        }

        UpLoadFileCheck.prototype.CheckExt = function (obj, flag) {
            //this.ImgObj.src = obj.value;
            this.FileExt = obj.value.substr(obj.value.lastIndexOf(".")).toLowerCase();
            if (this.AllowExt != 0 && this.AllowExt.indexOf(this.FileExt) == -1)//判断文件类型是否允许上传 
            {
                jNotify('<strong>该文件类型不允许上传，请上传jpg或gif图片</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            else {
                $("#Img" + flag).attr('src', "/images/Please_wait.gif");
                $('#UpLoadForm' + flag).ajaxSubmit({
                    success: function (html, status) {
                        var result = html.replace("<pre>", "");
                        result = result.replace("</pre>", "");
                        var array = result.split(',');
                        var PicUrlAdd = array[0].replace('<PRE>', '');
                        $("#PicUrl" + flag).val(PicUrlAdd);
                        var thumbPath = array[1].replace("//", "/").replace("</PRE>", "");
                        $("#Img" + flag).attr('src', thumbPath);
                        ImgObj1.src = PicUrlAdd;
                    }
                });
            }
        }

        function checkpic() {
            alert('图片壹\n宽度: ' + ImgObj1.width + '\n高度: ' + ImgObj1.height);
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
                catch (e) { ; }
            }
            return;
        }

        function resetit() {
            $("#AdName").val("");
            $("#AdSort").val("");
            $("#AdUrl").val("");
            $("#PicUrl1").val("");
            $("#PicUrl2").val("");
            $("#Img1").removeAttr("src");
            $("#Img2").removeAttr("src");
            ImgObj1 = new Image();
        }

        $(function () {
            $('#DropDownList1').change(function () {
                if ($('#DropDownList1').val().substring(0, 2) == "IC") {
                    $("#Img1").attr('alt', '宽屏版 分辨率为 766*120');
                    $("#Img2").attr('alt', '窄屏版 分辨率为 546*120');
                    $("#UpLoadForm2").show();
                }
                else {
                    $("#Img1").attr('alt', '分辨率为 211*90');
                    $("#UpLoadForm2").hide();
                }
            });
        });
    </script>
</head>
<body>
    <div class=inputdiv>
    广告版块：<select name="DropDownList1" id="DropDownList1" <%=setddl %>>
	<option value="IL1">首页左1</option>
	<option value="IL2">首页左2</option>
	<option value="IL3">首页左3</option>
	<option value="IL4">首页左4</option>
	<option value="IL5">首页左5</option>
	<option value="IL6">首页左6</option>
	<option value="IC1">首页中1</option>
	<option value="IC2">首页中2</option>
	<option value="IC3">首页中3</option>
	<option value="IC4">首页中4</option>
	<option value="IC5">首页中5</option>
	<option value="IC6">首页中6</option>
	<option value="IR1">首页右1</option>
	<option value="IR2">首页右2</option>
	<option value="IR3">首页右3</option>
	<option value="IR4">首页右4</option>
	<option value="IR5">首页右5</option>
	<option value="IR6">首页右6</option>
    </select>&nbsp; 排序：<input name="AdSort" id="AdSort" type="text" class="easyui-numberbox" precision="0" max="250" size="3" maxlength="3" style="text-align:center;"  value="<%=AdSort %>"/>
    </div>
    <div class=inputdiv>
    文字说明：<input name="AdName" type="text" id="AdName" style="width:220px;" value="<%=AdName %>"/>
    </div>
    <div class=inputdiv>
    链接地址：<input name="AdUrl" type="text" id="AdUrl" style="width:220px;" value="<%=AdPageUrl %>"/>

        <a id="SaveInfo" onclick="SaveAllInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
        <a id="A1" onclick="GoToUrl()" class="easyui-linkbutton" plain="true" iconCls="icon-redo">查看链接</a>
    </div>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="PicId" type="hidden" value="<%=id %>"/>
        <input id="PicUrl1" type="hidden" value="<%=AdPicUrl %>"/>
        <input id="PicUrl2" type="hidden" value="<%=AdSecPicUrl %>"/>
        <input id="MisClassId" type="hidden" value="0"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>Banner随机变换广告（jpg、gif 格式）</STRONG></DIV>
    <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=AdImage&Thumb=100">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px" valign="middle">
                    <p class="bnP1" id="bnPs1">
                        <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                    </p>         
                </td>
                <td  width="390px">
                    <img id="Img1" alt="分辨率为 211*90" <%=ThumbSrc1 %>/>
                </td>
            </tr>
        </table>
    </form>
    <form <%=IsShow %> id="UpLoadForm2" name="UpLoadForm2" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=AdImage&Thumb=100">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px">
                   <p class="bnP1" id="bnPs2">
                        <input onmouseover="javascript:$('#bnPs2').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs2').attr('class', 'bnP1');" type="file" id="Upload2" name="Upload2" onchange="checkit(this,'2')"/>
                    </p>
                </td>
                <td width="390px">
                    <img id="Img2" alt="窄屏版 分辨率为 546*90" <%=ThumbSrc2 %>/>
                </td>
            </tr>
        </table>
    </form>
    </DIV>
    <script type="text/javascript">
        setTimeout(function () { $("#DropDownList1").val("<%=AdFlag%>"); }, 1);

        function SaveAllInfo() {
            //checkpic();
            if ($("#PicUrl1").val() == "") {
                jNotify('<strong>请上传Banner图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            if ($("#AdUrl").val() == "") {
                jNotify('<strong>请输入链接地址!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=SaveFlashAd&HideFlag=0&Id=" + $("#PicId").val() + "&AdSort=" + $("#AdSort").val() + "&AdFlag=" + $("#DropDownList1").val() + "&AdName=" + escape($("#AdName").val()) + "&AdUrl=" + escape($("#AdUrl").val().replace(/\//g, "@")) + "&PicUrl1=" + $("#PicUrl1").val() + "&PicUrl2=" + $("#PicUrl2").val() + "&MisClassId=" + $("#MisClassId").val() + "&r=" + Math.random();
            //alert(url);
            //window.location = url;
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    if ($("#PicId").val() == "") {
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

