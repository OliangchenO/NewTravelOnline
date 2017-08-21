<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryInfo.aspx.cs" Inherits="TravelOnline.Management.SummaryInfo" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleInfo%> - <%=desname%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/WriteJournal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="/Scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="/Scripts/swfupload/fileprogress.js"></script>
    <script type="text/javascript" src="/Scripts/swfupload/handlers.js"></script>
    <script type="text/javascript" charset="utf-8">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script type="text/javascript" src="/ueditor/ueditor.config.js"></script>  
    <script type="text/javascript" src="/ueditor/ueditor.all.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .DropListDiv {Z-INDEX: 9999; }
    </style>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<%--<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>--%>
<DIV class="w main">
<div id="order_title">
<h2 class="headline"><SPAN class=headstep><%=TitleInfo%> - <%=desname%></span></h2>
</div>
<DIV class=clr></DIV>
<form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input id="id" name="id" type="hidden" value="<%=id%>"/>
        <input id="uid" name="uid" type="hidden" value="<%=uid%>"/>
        <input id="desid" name="desid" type="hidden" value="<%=desid%>"/>
        <input id="contents" name="contents" type="hidden" value=""/>
        <input id="typeid" name="typeid" type="hidden" value="<%=typeid%>"/>
        <input id="flag" name="flag" type="hidden" value="<%=flag %>"/>
        <input id="btnCancel" type="button" value="取消全部上传" onclick="swfu.cancelQueue();" disabled="disabled" style="margin-left: 2px; font-size: 8pt; height: 22px;" />
    </DIV>
    <table style="width: 100%;" border="0">
        <tr>
            <td valign="top">
                <div class="journals-content" name="wtitle" id="wtitle">
	                <label style="color:#000"> <i></i>资讯类别</label>
	                <input class=sel 
                        style="margin-top: 8px;height:28px;line-height:28px;font-size:14px;color:#666;width: 632px" 
                        type="text" name="typename" id="typename" readonly="readonly" value="<%=typename%>"/>
                </div>
                <div class="editor-wrap">
                    <div class="journals-content">
                    <label style="color:#000"><i></i>内容详情：</label>
                    <script id="editor" type="text/plain"><%=contents %></script>
                    </div>
                </div>
            </td>
            <td width="290px" valign="top">
                <div id="imagebar" style="background-color: #FFFFFF;">
            	    <div style="padding-top: 15px;width:280px;">
				        <span style="CURSOR: hand;" id="spanButtonPlaceHolder"></span><span style="padding-top: 15px;color: #999">(单击图片可插入正文)</span>
				    </div>
                    <p>&nbsp;</p>
                    <div id="bgbox">
                        <ul>
                            <li class="uplpad_info_pic_1">
                                <img src="/Scripts/swfupload/backimg.png" alt="">
                                <p style="color: #999">点击“添加照片”开始上传<br>单张照片不能大于5M<br>每一次最多30张<br>总共能上传100张照片</p>
                            </li>
                        </ul>
                    </div>
			        <div id="fsUploadProgress" style="clear:both;display:none;width:280px;height:auto;max-height:500px;overflow-y:auto;"></div>
                </div>
            </td>
        </tr>
    </table>
</form>

<div class="gotonext">
<span style="padding-left: 30px">&nbsp;</span><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>
<A id="addnew" class="btn-link btn-personal" href="SummaryInfo.aspx?desid=<%=desid %>">新 增</A>
<A id="OrderBtn" class="btn-link btn-personal" href="javascript:void(0);" onclick="Save()">保 存</A>
</div>
<script type="text/javascript">
    var swfu;

    window.onload = function () {
        var settings = {
            flash_url: "/Scripts/swfupload/swfupload.swf",
            upload_url: "/Utility/ThumbCreate.ashx?PathSet=summary",
            file_size_limit: "100 MB",
            file_types: "*.jpg;*.jpeg;*.gif;*.png;",
            file_types_description: "JPG Images",
            file_upload_limit: 100,
            file_queue_limit: 30,
            custom_settings: {
                progressTarget: "fsUploadProgress",
                cancelButtonId: "btnCancel"
            },
            debug: false,
            // Button settings
            button_image_url: "/images/addimages.png",
            button_placeholder_id: "spanButtonPlaceHolder",
            button_width: 80,
            button_height: 22,
            button_text_style: '.button {CURSOR: pointer;} .buttonSmall { CURSOR: pointer;}',
            button_text_top_padding: 1,
            button_text_left_padding: 5,

            // The event handler functions are defined in handlers.js
            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            file_dialog_complete_handler: MyfileDialogComplete,
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: ShowData,
            upload_complete_handler: uploadComplete
            //queue_complete_handler : queueComplete //上传完成显示文件数	// Queue plugin event  uploadSuccess
        };

        swfu = new SWFUpload(settings);

    };

    function MyfileDialogComplete(numFilesSelected, numFilesQueued) {
        try {
            $("#bgbox").hide();
            $("#fsUploadProgress").show();
            this.startUpload();
        } catch (ex) {
            this.debug(ex);
        }
    }

    function ShowData(file, serverData) {
        $("#" + file.id).html(serverData);
    };

    window.onscroll = function () {
        var top = "260";
        var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
        if (scrollTop > top) {
            $("#imagebar").attr({ "class": "package_ptfix" });
        } else {
            $("#imagebar").removeAttr("class");
        }
    }

</script>
<script type="text/javascript">
    var options = {
        initialFrameWidth: 660
        , initialFrameHeight: 320
        , focus: false
        , elementPathEnabled: false
        , contextMenu: []
        , pasteplain: true
        , toolbars: [
            ['undo', 'redo', '|',
            'bold', 'italic', 'underline', 'horizontal', 'pasteplain', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
            'emotion', 'gmap', 'spechars', 'wordimage']
        ]
        , wordCount: false
    };
    var ue = UE.getEditor('editor', options);
    var domUtils = UE.dom.domUtils;
    ue.addListener("ready", function () {
        UE.getEditor('editor').execCommand('insertHtml', " ");
    });

    $(document).ready(function () {

        $('#typename').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=SummaryType&flag=" + $("#flag").val() + "&SerchName=" + encodeURI(this.value);
            //window.open(url);
            show(this, "typeid", url, "");
        });

        $('.uppic IMG').live("click", function () {
            var imgurl = $(this).attr("src").replace("_T100.", ".");
            var picid = $(this).attr("picid");
            UE.getEditor('editor').execCommand('insertHtml', "<p><img src=\"" + imgurl + "\" ></p>");
            //$('.photo-mask').hide();
            $(this).parent().parent('.progressWrapper').hide();
        })

    });

    String.prototype.trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, "");
    };
    String.prototype.isNullOrEmpty = function () {
        return null == this || "" == this;
    };
    String.prototype.contains = function (str) {
        return (new RegExp(str, "gi")).test(this);
    };
    String.prototype.endWith = function (str) {
        return new RegExp(str + "$", "i").test(this);
    };

    function Save() {
        if (UE.getEditor('editor').hasContents() == false || $("#typeid").val() == "") {
            jError('<strong>请输入概述类型和概述正文!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }

        $("#OrderBtn").hide();
        $("#addnew").hide();
        $("#islogin").show();
        var url = "/Users/JournalsService.aspx?action=Summary&r=" + Math.random();
        $.post(url, $("#form_data").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                $("#uid").val(obj.success);
                jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                $("#islogin").hide();
                $("#OrderBtn").show();
                $("#addnew").show();
            }
            else {
                $("#islogin").hide();
                $("#OrderBtn").show();
                $("#addnew").show();
                alert(obj.error);
            }
        });
    }
</script>
<DIV class=clr></DIV>
</DIV>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>
