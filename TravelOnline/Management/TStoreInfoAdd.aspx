<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TStoreInfoAdd.aspx.cs" Inherits="TravelOnline.Management.TStoreInfoAdd" %>

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
    <div class=mt><H1></H1><strong>新增店铺信息</strong></div>
        <form id="form_data" onsubmit="return false;" method="post">
        <input id="tradingAreaId" name="tradingAreaId" type="hidden" value="<%= tradingAreaId%>" />
        <div id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
        </div>
    <dl><dt>名称：</dt>
        <dd>
            <input id="name" name="name" type="text" maxlength="500" size="50"  value="<%=name %>"/>
        </dd>
    </dl>
    <dl><dt>链接：</dt>
        <dd>
            <input id="link" name="link" type="text" maxlength="500" size="50"  value="<%=link %>"/>
        </dd>
    </dl>
    <dl><dt>内容：</dt>
        <dd>
            <textarea name="context" rows="3" cols="38"><%=context %></textarea>
        </dd>
    </dl>
    </div>
    <input id="pic" name="pic" type="hidden" value="<%=pic %>"/>
   </form>
    <span class="clr"></span>
    <div id=select class="m select">
    <div class=mt><H1></H1><STRONG>jpg、gif 格式&nbsp;&nbsp;&nbsp;&nbsp;</STRONG><STRONG id="pxtip">分辨率为 480*150</STRONG></div>
    <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=AdImage&Thumb=100">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px" valign="middle">
                    <p class="bnP1" id="bnPs1">
                        <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                    </p>         
                </td>
                <td  width="390px">
                    <img id="Img1" alt="分辨率为 480*150" <%=ThumbSrc1 %>"/>
                </td>
            </tr>
        </table>
    </form>
        <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
        <%=buttoninfo %>
    </div>
    <script type="text/javascript">
        var ImgObj1 = new Image();

        UpLoadFileCheck = function () {
            this.AllowExt = ".jpg,.gif,.png"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
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
                $("#Img1").attr('src', "/images/Please_wait.gif");
                $('#UpLoadForm1').ajaxSubmit({
                    success: function (html, status) {
                        var result = html.replace("<pre>", "");
                        result = result.replace("</pre>", "");
                        result = result.replace("<pre style=\"word-wrap: break-word; white-space: pre-wrap;\">", "");
                        var array = result.split(',');
                        //alert(result);
                        var PicUrlAdd = array[0].replace('<PRE>', '');
                        $("#pic").val(PicUrlAdd);
                        var thumbPath = array[1].replace("//", "/").replace("</PRE>", "");
                        $("#Img1").attr('src', thumbPath);
                        ImgObj1.src = PicUrlAdd;
                    }
                });
            }
        }

        function checkpic() {
            alert('图片壹\n宽度: ' + ImgObj1.width + '\n高度: ' + ImgObj1.height);
            alert('图片贰\n宽度: : ' + ImgObj2.width + '\n高度: ' + ImgObj2.height);
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

            //            if ($("#sellflag").val() == "3") {
            //                if ($("#range").val() == "5" || $("#range").val() == "9") {
            //                }
            //                else {
            //                    jNotify('<strong>公共券号，使用范围只能选邮轮产品或按指定产品两种</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            //                    return false;
            //                }
            //            }


            if ($("#name").val() == "") {
                jNotify('<strong>请输入活动名称</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }


            if ($("#title").val() == "") {
                jNotify('<strong>请输入活动简介!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#context").val() == "") {
                jNotify('<strong>请输入活动内容!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            if ($("#color").val() == "") {
                jNotify('<strong>请输入标示颜色!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SaveTStoreInfo&r=" + Math.random();
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
