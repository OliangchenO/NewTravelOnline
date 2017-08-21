<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDetail.aspx.cs" Inherits="TravelOnline.Destination.ViewDetail" %>
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
        img {height:80px;max-width:300px;}
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
                        result = result.replace("<pre style=\"word-wrap: break-word; white-space: pre-wrap;\">", "");
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
            $("#PicUrl1").val("");
            $("#PicUrl2").val("");
            $("#Img1").removeAttr("src");
            ImgObj1 = new Image();
        }
    </script>
</head>
<body>
<form id="form_data" onsubmit="return false;" method="post">
    <div class=inputdiv>
    文字说明：<input name="picname" type="text" id="picname" style="width:220px;" value="<%=picname %>"/>
    &nbsp;&nbsp;<a id="SaveInfo" onclick="SaveAllInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
    </div>
    <div class=inputdiv>
    详细描述：<textarea name="picmemo" id="picmemo" style="width:350px;height:60px;vertical-align:top;color:#666;"><%=picmemo%></textarea>
    </div>
    <div class=inputdiv>
        
    </div>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="id" name="id" type="hidden" value="<%=id %>"/>
        <input id="viewid" name="viewid" type="hidden" value="<%=viewid %>"/>
        <input id="desid" name="desid" type="hidden" value="<%=desid %>"/>
        <input id="PicUrl1" name="PicUrl1" type="hidden" value="<%=picurl %>"/>
        <input id="PicUrl2" name="PicUrl2" type="hidden" value=""/>
    </DIV>
</form>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>景点图片（jpg、gif 格式）</STRONG></DIV>
    <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?flag=viewpic&PathSet=View/<%=desid %>/<%=viewid %>&Thumb=240">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px" valign="middle">
                    <p class="bnP1" id="bnPs1">
                        <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                    </p>         
                </td>
                <td  width="390px">
                    <img id="Img1" alt="分辨率为 600*400以上最佳" <%=ThumbSrc1 %>/>
                </td>
            </tr>
        </table>
    </form>
    <%--<form <%=IsShow %> id="UpLoadForm2" name="UpLoadForm2" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=Topic">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px">
                   <p class="bnP1" id="bnPs2">
                        <input onmouseover="javascript:$('#bnPs2').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs2').attr('class', 'bnP1');" type="file" id="Upload2" name="Upload2" onchange="checkit(this,'2')"/>
                    </p>
                </td>
                <td width="390px">
                    <img id="Img2" alt="分辨率为 88*31" <%=ThumbSrc2 %>/>
                </td>
            </tr>
        </table>
    </form>--%>
    </DIV>
    <script type="text/javascript">
        function SaveAllInfo() {
            if ($("#PicUrl1").val() == "") {
                jNotify('<strong>请上传旅游图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "/Management/AjaxService.aspx?action=SaveViewPic&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    if ($("#id").val() == "") {
                        resetit();
                    }
                }
                else {
                    jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>

