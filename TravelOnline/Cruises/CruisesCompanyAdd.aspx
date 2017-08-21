<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesCompanyAdd.aspx.cs" Inherits="TravelOnline.Cruises.CruisesCompanyAdd" %>
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
        .select {WIDTH: 520px;}
        .select DL {WIDTH: 520px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 400px;}
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
    <DIV class=mt><H1></H1><STRONG>邮轮船公司</STRONG></DIV>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
        <input id="logourl" name="logourl" type="hidden" value="<%=logourl %>"/>
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="myrefresh()" class="tools <%=hide %>"><img src="../images/icon/add.png" class=img20>新增</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="check_null()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <DL><DT>邮轮公司名称：</DT>
        <DD>
            <input id="cname" name="cname" type="text" style="width: 360px;" maxlength="100" value="<%=cname %>"/>
        </DD>
    </DL>
    <DL><DT>英文名称：</DT>
        <DD>
            <input id="ename" name="ename" type="text" style="width: 360px;" maxlength="100" value="<%=ename %>"/>
        </DD>
    </DL>
    <DL><DT>邮轮公司介绍：</DT>
        <DD>
            <textarea name="intro" id="intro" cols="" rows="" style="width: 360px;height:60px"><%=intro %></textarea>
        </DD>
    </DL>
   </form>
   <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=Cruises&Thumb=55">
        <DL><DT>展示图片：</DT>
        <DD>
            <table border="0" cellpadding="0" cellspacing="0" class="upp">
                <tr>
                    <td  width="150px">
                        <img id="Img1" alt="展示图片 宽度360px以内" <%=ThumbSrc %>/>
                    </td>
                    <td width="60px" valign="middle">
                        <p class="bnP1" id="bnPs1">
                            <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                        </p>         
                    </td>                
                </tr>
            </table>
        </DD>
    </DL>
    </form>
     <%--<a id="A1" onclick="myrefresh()" class="easyui-linkbutton  <%=hide %>" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
     <a id="SaveInfo" onclick="check_null()" class="easyui-linkbutton" plain="true" iconCls="icon-save" style="margin-left: 150px;margin-top: 10px;">保存</a>
    --%></DIV>
    <script type="text/javascript">
        var ImgObj1 = new Image();

        UpLoadFileCheck = function () {
            this.AllowExt = ".jpg,.gif"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
        }

        UpLoadFileCheck.prototype.CheckExt = function (obj, flag) {
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
                        $("#logourl").val(PicUrlAdd);
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
    </script>
    <script type="text/javascript">

        function check_null() {
            if ($("#cname").val() == "" || $("#ename").val() == "") {
                jNotify('<strong>中文名称和英文名称都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=CruisesCompany&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        parent.$('#GridView_Serch_Button').click();
                    }
                    else {
                        parent.$('#GridView_Refresh_Button').click();
                    }
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
