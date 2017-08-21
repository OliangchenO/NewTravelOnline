<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlashAdInfo.aspx.cs" Inherits="TravelOnline.Management.FlashAdInfo" %>
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
        .select {WIDTH: 540px;}
        .inputdiv {PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 10px; PADDING-RIGHT: 0px; PADDING-TOP: 5px}
        table {border: solid 1px #e8eef4;border-collapse: collapse;}
        table td{padding: 10px 5px 10px 5px;border: solid 1px #e8eef4;}
        table th{padding: 5px 5px;text-align: left;background-color: #e8eef4;border: solid 1px #e8eef4;}
        .upp {WIDTH: 540px;}
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
        $(document).ready(function () {
            $("#DropDownList1").val("<%=AdFlag%>");
            var flag = $('#DropDownList1').val();
            settext(flag);

            $('#DropDownList1').change(function () {
                $("#UpLoadForm1").show();
                $("#UpLoadForm2").hide();
                $("#citieshow").hide();
                settext($(this).val());
            });
        })

        function settext(flag) {
            if (flag == "Index") {
                $("#Img1").attr('alt', '宽屏版 分辨率为 766*270');
                $("#Img2").attr('alt', '窄屏版 分辨率为 546*270');
                $("#UpLoadForm2").show();
                $("#pxtip").html("宽屏766*270 窄屏546*270");
            }
            if (flag == "OutBound" || flag == "InLand" || flag == "FreeTour" || flag == "Cruises" || flag == "Visa") {
                $("#Img1").attr('alt', '宽屏版 分辨率为 766*200');
                $("#Img2").attr('alt', '窄屏版 分辨率为 546*200');
                $("#UpLoadForm2").show();
                $("#pxtip").html("宽屏766*200 窄屏546*200");
            }

            if (flag.indexOf("_New") > 0) {
                $("#Img1").attr('alt', '分辨率为 1170*300');
                $("#pxtip").html("分辨率为 1170*300");
            }
            if (flag == "Index_New") {
                $("#Img1").attr('alt', '分辨率为 900*300');
                $("#pxtip").html("分辨率为 900*300");
            }

            if (flag.indexOf("_Small") > 0) {
                $("#Img1").attr('alt', '分辨率为 230*390');
                $("#pxtip").html("分辨率为 230*390");
            }
            if (flag.indexOf("_3") > 0) {
                $("#Img1").attr('alt', '分辨率为 390*175');
                $("#pxtip").html("分辨率为 390*175");
            }
            if (flag.indexOf("_T") > 0) {
                $("#Img1").attr('alt', '分辨率为 1170*120');
                $("#pxtip").html("分辨率为 1170*120");
            }
            if (flag == "Citie") {
                $("#Img1").attr('alt', '分辨率见文字说明');
                $("#Img2").attr('alt', '分辨率见文字说明');
                $("#UpLoadForm2").show();
                $("#citieshow").show();
                $("#pxtip").html("分辨率请参考图片");
            }
            if (flag=="Hot_FreeTour") {
                $("#Img1").attr('alt', '分辨率为 218*193');
                $("#pxtip").html("分辨率为 218*193");
            }
            if (flag == "Partner") {
                $("#Img1").attr('alt', '分辨率为 110*45');
                $("#pxtip").html("分辨率为 110*45");
            }
            if (flag.indexOf("LeftHot") > -1) {
                $("#UpLoadForm1").hide();
                $("#UpLoadForm2").hide();
                $("#pxtip").html("首页左侧本月热点线路");
            }
            if (flag.indexOf("LeftArea") > -1) {
                $("#UpLoadForm1").hide();
                $("#UpLoadForm2").hide();
                $("#pxtip").html("首页左侧本月热点目的地");
            }
            if (flag == "CruisesShip") {
                $("#Img1").attr('alt', '分辨率为 585*200');
                $("#pxtip").html("分辨率为 585*200");
            }
            if (flag == "WeChat") {
                $("#Img1").attr('alt', '分辨率为 480*150');
                $("#pxtip").html("分辨率为 480*150");
            }

            //2014新页面
            if (flag == "N_Index_Slide") {
                $("#Img1").attr('alt', '分辨率为 604*435');
                $("#pxtip").html("分辨率为 604*435");
            }

            if (flag == "N_Index_Banner") {
                $("#Img1").attr('alt', '分辨率为 177*181');
                $("#pxtip").html("分辨率为 177*181");
            }

            if (flag == "N_Index_Season") {
                $("#Img1").attr('alt', '分辨率为 927*200');
                $("#pxtip").html("分辨率为 927*200");
            }

            if (flag.indexOf("_S_") > 0) {
                $("#Img1").attr('alt', '分辨率为 876*360');
                $("#pxtip").html("分辨率为 876*360");
            }

            if (flag == "N_S_Cruise_Slide") {
                $("#Img1").attr('alt', '分辨率为 1200*350');
                $("#pxtip").html("分辨率为 1200*350");
            }

            if (flag == "N_S_List_Season") {
                $("#Img1").attr('alt', '分辨率为 273*298');
                $("#pxtip").html("分辨率为 273*298");
            }

            if (flag == "N_S_Journal_Slide") {
                $("#Img1").attr('alt', '分辨率为 938*360');
                $("#pxtip").html("分辨率为 938*360");
            }

            if (flag == "N_S_Journal_SL") {
                $("#Img1").attr('alt', '分辨率为 240*360');
                $("#pxtip").html("分辨率为 240*360");
            }
        }

        var ImgObj1 = new Image();
        var ImgObj2 = new Image();

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
                $("#Img" + flag).attr('src', "/images/Please_wait.gif");
                $('#UpLoadForm' + flag).ajaxSubmit({
                    success: function (html, status) {
                        var result = html.replace("<pre>", "");
                        result = result.replace("</pre>", "");
                        result = result.replace("<pre style=\"word-wrap: break-word; white-space: pre-wrap;\">", "");
                        var array = result.split(',');
                        //alert(result);
                        var PicUrlAdd = array[0].replace('<PRE>', '');
                        $("#PicUrl" + flag).val(PicUrlAdd);
                        var thumbPath = array[1].replace("//", "/").replace("</PRE>", "");
                        $("#Img" + flag).attr('src', thumbPath);
                        if (flag == "1") ImgObj1.src = PicUrlAdd;
                        if (flag == "2") ImgObj2.src = PicUrlAdd;
                    }
                });
            }
        }

        function checkpic() {
            alert('图片壹\n宽度: ' + ImgObj1.width + '\n高度: ' + ImgObj1.height);
            alert('图片贰\n宽度: : ' + ImgObj2.width + '\n高度: ' + ImgObj2.height);
        }

        function checkit(obj,flag) {
            var d = new UpLoadFileCheck();
            d.CheckExt(obj,flag)
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
            ImgObj2 = new Image();
        }

//        $(function () {
//            $('#DropDownList1').change(function () {
//                $("#UpLoadForm2").hide();
//                $("#citieshow").hide();
//                
//                var flag = $('#DropDownList1').val();
//                if (flag == "Index") {
//                    $("#Img1").attr('alt', '宽屏版 分辨率为 766*270');
//                    $("#Img2").attr('alt', '窄屏版 分辨率为 546*270');
//                    $("#UpLoadForm2").show();
//                    $("#pxtip").html("宽屏766*270 窄屏546*270");
//                }
//                if (flag == "OutBound" || flag == "InLand" || flag == "FreeTour" || flag == "Cruises" || flag == "Visa") {
//                    $("#Img1").attr('alt', '宽屏版 分辨率为 766*200');
//                    $("#Img2").attr('alt', '窄屏版 分辨率为 546*200');
//                    $("#UpLoadForm2").show();
//                    $("#pxtip").html("宽屏766*200 窄屏546*200");
//                }

//                if (flag.indexOf("_New") > 0) {
//                    $("#Img1").attr('alt', '分辨率为 1170*300');
//                    $("#pxtip").html("分辨率为 1170*300");
//                }
//                if (flag == "Index_New" > 0) {
//                    $("#Img1").attr('alt', '分辨率为 900*300');
//                    $("#pxtip").html("分辨率为 900*300");
//                }
//                
//                if (flag.indexOf("_Small") > 0) {
//                    $("#Img1").attr('alt', '分辨率为 230*390');
//                    $("#pxtip").html("分辨率为 230*390");
//                }
//                if (flag.indexOf("_3") > 0) {
//                    $("#Img1").attr('alt', '分辨率为 390*175');
//                    $("#pxtip").html("分辨率为 390*175");
//                }
//                if (flag.indexOf("_T") > 0) {
//                    $("#Img1").attr('alt', '分辨率为 1170*120');
//                    $("#pxtip").html("分辨率为 1170*120");
//                }
//                if (flag == "Citie") {
//                    $("#Img1").attr('alt', '分辨率见文字说明');
//                    $("#Img2").attr('alt', '分辨率见文字说明');
//                    $("#UpLoadForm2").show();
//                    $("#citieshow").show();
//                    $("#pxtip").html("分辨率请参考图片");
//                }
//            });
//        });
    </script>
</head>
<body>
    <div class=inputdiv>
    广告版块：<select name="DropDownList1" id="DropDownList1" <%=setddl %> >
	<%--<option value="Index">首页</option>
	<option value="OutBound">出境旅游</option>
	<option value="InLand">国内旅游</option>
	<option value="FreeTour">自由行</option>
	<option value="Cruises">邮轮</option>
	<option value="Visa">签证</option>--%>
    <option value="Index_New">新首页</option>
    <option value="OutBound_New">新出境</option>
    <option value="InLand_New">新国内</option>
    <option value="Cruises_New">新邮轮</option>
    <option value="FreeTour_New">新自由行</option>
    <option value="Visa_New">新签证</option>
    <option value="OutBound_Small">出境小轮换</option>
    <option value="InLand_Small">国内小轮换</option>
    <option value="Citie">磁贴</option>
    <option value="Index_Up_3">首页三栏上</option>
    <option value="Index_Down_3">首页三栏下</option>
    <option value="OutBound_3">出境三栏</option>
    <option value="InLand_3">国内三栏</option>
    <option value="FreeTour_3">自由行三栏</option>
    <option value="Cruises_3">邮轮三栏</option>
    <option value="Visa_3">签证三栏</option>
    <option value="Index_T">首页通栏</option>
    <option value="OutBound_T">出境通栏</option>
    <option value="InLand_T">国内通栏</option>
    <option value="FreeTour_T">自由行通栏</option>
    <option value="Cruises_T">邮轮通栏</option>
    <option value="Visa_T">签证通栏</option>
    <option value="LeftHot_OutBound">出境热点</option>
    <option value="LeftHot_InLand">国内热点</option>
    <option value="Hot_FreeTour">自由行热点</option>
    <option value="LeftArea_OutBound">出境目的地热点</option>
    <option value="LeftArea_InLand">国内目的地热点</option>
    <option value="Partner">合作伙伴</option>
    <option value="CruisesShip">邮轮船队推荐</option>
    <option value="WeChat">微信</option>
    <option value="---">----新版----</option>
    <option value="N_Index_Slide">首页轮换</option>
    <option value="N_Index_Banner">轮换右侧广告位</option>
    <option value="N_Index_Season">首页当季推荐</option>
    <option value="N_S_OutBound_Slide">出境轮换</option>
    <option value="N_S_InLand_Slide">国内轮换</option>
    <option value="N_S_FreeTour_Slide">自由行轮换</option>
    <option value="N_S_Cruise_Slide">邮轮轮换</option>
    <option value="N_S_Visa_Slide">签证轮换</option>
    <option value="N_S_List_Season">线路列表当季推荐</option>
    <option value="freetrip">青青旅行</option>
    <option value="N_S_Journal_SL">游记左侧图片</option>
    <option value="N_S_Journal_Slide">游记轮换</option>
    </select>&nbsp; 排序：<input name="AdSort" id="AdSort" type="text" class="easyui-numberbox" precision="0" max="250" size="3" maxlength="3" style="text-align:center;"  value="<%=AdSort %>"/>
    </div>
    <div class="inputdiv">
    文字说明：<input name="AdName" type="text" id="AdName" style="width:220px;" value="<%=AdName %>"/>
    &nbsp;背景色：#<input name="BackGround" type="text" id="BackGround" style="width:100px;" value="<%=BackGround %>"/>(示例:FFFFFF)
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
    <DIV class=mt><H1></H1><STRONG>jpg、gif 格式&nbsp;&nbsp;&nbsp;&nbsp;</STRONG><STRONG id="pxtip">图片分辨率请参考不同板块设置</STRONG></DIV>
    <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=AdImage&Thumb=100">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px" valign="middle">
                    <p class="bnP1" id="bnPs1">
                        <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                    </p>         
                </td>
                <td  width="390px">
                    <img id="Img1" alt="宽屏版 分辨率为 766*270" <%=ThumbSrc1 %>/>
                </td>
            </tr>
        </table>
    </form>
    <form <%=hide %> id="UpLoadForm2" name="UpLoadForm2" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=AdImage&Thumb=100">
        <table border="0" cellpadding="0" cellspacing="0" class="upp">
            <tr>
                <td width="60px">
                   <p class="bnP1" id="bnPs2">
                        <input onmouseover="javascript:$('#bnPs2').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs2').attr('class', 'bnP1');" type="file" id="Upload2" name="Upload2" onchange="checkit(this,'2')"/>
                    </p>
                </td>
                <td width="390px">
                    <img id="Img2" alt="窄屏版 分辨率为 546*270" <%=ThumbSrc2 %>/>
                </td>
            </tr>
        </table>
    </form>
    <table id="citieshow" border="0" cellpadding="0" cellspacing="0" class="hide">
            <tr>
                <td  width="540px">
                    <img id="Img3" alt="磁贴图片示例" src="../img/citieshow.jpg" width="540px" />
                </td>
            </tr>
        </table>
    </DIV>
    <script type="text/javascript">
        setTimeout(function () { $("#DropDownList1").val("<%=AdFlag%>"); }, 1);

        function GoToUrl() {
            if ($("#AdUrl").val().length < 5) {
                jNotify('<strong>请输入链接地址!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            window.open($("#AdUrl").val())
        }

        function SaveAllInfo() {
            //checkpic();
            var flag = $('#DropDownList1').val();

            if (flag.indexOf("LeftHot") > -1 || flag.indexOf("LeftArea") > -1) {
            }
            else{
                if ($("#PicUrl1").val() == "") {
                    jNotify('<strong>请上传图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            
            if (flag == "Index" || flag == "OutBound" || flag == "InLand" || flag == "FreeTour" || flag == "Cruises" || flag == "Visa") {
                if ($("#PicUrl1").val() == "" || $("#PicUrl2").val() == "") {
                    jNotify('<strong>请上传两个不同分辨率的图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            if (flag == "Citie") {
                if ($("#PicUrl1").val() == "" || $("#PicUrl2").val() == "") {
                    jNotify('<strong>请上传两张不同的磁贴图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }
            
            if ($("#AdName").val() == "" || $("#AdUrl").val() == "") {
                jNotify('<strong>请输入文字说明和链接地址!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=SaveFlashAd&HideFlag=0&Id=" + $("#PicId").val() + "&AdSort=" + $("#AdSort").val() + "&AdFlag=" + $("#DropDownList1").val() + "&AdName=" + escape($("#AdName").val()) + "&AdUrl=" + escape($("#AdUrl").val().replace(/\//g, "@")) + "&PicUrl1=" + $("#PicUrl1").val() + "&PicUrl2=" + $("#PicUrl2").val() + "&MisClassId=" + $("#MisClassId").val() + "&BackGround=" + $("#BackGround").val() + "&r=" + Math.random();
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
