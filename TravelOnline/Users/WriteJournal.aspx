<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WriteJournal.aspx.cs" Inherits="TravelOnline.Users.WriteJournal" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=pagetitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/WriteJournal.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/shoppingcart.css" rel="stylesheet" type="text/css" />
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
    <script type=text/javascript src="/ueditor/ueditor.config.js"></script>  
    <script type=text/javascript src="/ueditor/ueditor.all.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .des_select 
        {
            border: solid 2px #DDF0DD;
            MARGIN: 5px 10px 5px 0px;
	        padding: 5px 20px 5px 30px;
            background: #F1F7F1 url(../images/ico-right.png) 0 5px no-repeat;
            background-position: 5px 3px;
            CURSOR: pointer;
            overflow: hidden;
            font-size: 12px;
            FONT-WEIGHT: bold;
            FLOAT: left
        }

        .des_deselect 
        {
            border: solid 2px #DDF0DD;
            MARGIN: 5px 10px 5px 0px;
	        padding: 5px 20px 5px 30px;
            CURSOR: pointer;
            overflow: hidden;
            font-size: 12px;
            FONT-WEIGHT: normal;
            FLOAT: left
        }
    </style>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<%--<script type="text/javascript" src="/Js/Hot/hotwords.js"></script>--%>
<DIV class="w main">
<div id="order_title">
<h2 class="headline"><SPAN class=headstep><%=pagetitle%></span></h2>
</div>
<DIV class=clr></DIV>
<form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input id="upid" name="upid" type="hidden" value="<%=upid%>"/>
        <input id="Journalid" name="Journalid" type="hidden" value="<%=ucode%>"/>
        <input id="uid" name="uid" type="hidden" value="<%=Uid%>"/>
        <input id="contents" name="contents" type="hidden" value=""/>
        <input id="coverimgurl" name="coverimgurl" type="hidden" value="<%=coverpic%>"/>
        <input id="coverpicurl" name="coverpicurl" type="hidden" value="<%=coverpicurl%>"/>
        <input id="coverimgid" name="coverimgid" type="hidden" value="<%=coverid%>"/>
        <input id="albumid" name="albumid" type="hidden" value=""/>
        <input id="Destinationid" name="Destinationid" type="hidden" />
        <input id="FirstDestination" name="FirstDestination" type="hidden" value=""/>
        <input id="DestinationName" name="DestinationName" type="hidden" value=""/>
        <input id="Des_List" name="Des_List" type="hidden" value=""/>
        <input id="ImportFlag" name="ImportFlag" type="hidden" value="<%=ImportFlag%>"/>
        <input id="btnCancel" type="button" value="取消全部上传" onclick="swfu.cancelQueue();" disabled="disabled" style="margin-left: 2px; font-size: 8pt; height: 22px;" />
    </DIV>
    <table style="width: 100%;" border="0">
        <tr>
            <td valign="top">
                <div class="journals-content" name="wtitle" id="wtitle">
	                <label style="color:#000"> <i></i>标题：<span class="memo">(最多输入50个汉字)</span></label>
	                <input type="text" class="txt-input" id="title" name="title" maxlength="50" value="<%=title %>"/>
                </div>
                <div class="editor-wrap">
                    <div class="journals-content">
                    <label style="color:#000"><i></i>正文：</label>
                    <script id="editor" type="text/plain"><%=contents %></script>	
                    </div>
                </div>
                <div class="journals-content" name="wtitle" id="Div1">
	                <label style="color:#000"> <i></i>SEO关键字：<span class="memo">搜索引擎关键字</span></label>
	                <input type="text" class="txt-input" id="seo" name="seo" maxlength="50" value="<%=seo %>"/>
                    
                </div>
                <div class="journals-content" name="wtitle" id="Div2">
	                <label style="color:#000"> <i></i>目的地：<span class="memo">(拼音首字母检索)</span></label>
                    <input class=sel style="height:28px;line-height:28px;font-size:14px;color:#666;width: 200px" type="text" name="Destination" id="Destination" maxlength="10"/>
	                <div id="DestinationList" style="clear:both;width:660px;height:auto;max-height:500px;overflow-y:auto;MARGIN-top: 5px;"><%=DestinationInfos %></div>
				</div>
                <div class="journals-content" name="#wtag" id="wtag" style="color:#000">
	                <label> 标签：<span class="memo">(多个标签请用逗号,分隔，最多10个)</span><span id="tagwarn" class="warn"></span></label>
	                <div  class="tags-list" id="tags-avilable"> <em>美食</em><em>购物</em><em>摄影</em><em>行程</em><em>夜生活</em><em>家庭游</em><em>海滨</em><em>自驾</em><em>登山</em><em>赏花</em><em>一日游</em><em>三日游</em> </div>
	                <input id="tag" name="tag" type="text" class="txt-input" value="<%=tags %>"/>
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
                        <%--<div class="uppic"><img src="/Upload/journals/130630/201306300530241757520_100.jpg" /></div>--%>
                    </div>
			        <div id="fsUploadProgress" style="clear:both;display:none;width:280px;height:auto;max-height:500px;overflow-y:auto;"></div>
                    <div class="journals-cover <%=hide %>" name="wtitle" id="cover">
	                    <label style="color:#000"> <i></i>封面图片：</label>
	                    <div class='uppiccover'><%=coverpichtml %></div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</form>
<%--<TEXTAREA id=editor1 name=editor1></TEXTAREA>--%>
<%--<div class="details">
    <div>
        <script type="text/plain" id="editor"></script>
        <div class="con-split"></div>
    </div>
</div>--%>

<div class="gotonext">
<span style="padding-left: 30px">&nbsp;</span><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>
<%=BuyButton %>
</div>
<div class="photo-mask" style="width:76px;top:100px;left:100px;">
    <div class="mask-bg"><em>设为封面</em></div>
</div>
<script type="text/javascript">
    var swfu;

    window.onload = function () {
        var settings = {
            flash_url: "/Scripts/swfupload/swfupload.swf",
            upload_url: "/Utility/ThumbCreate.ashx?PathSet=journals&upid=" + $("#upid").val() + "&Journalid=" + $("#Journalid").val(),
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
            //button_text: '<span class="button">&nbsp; &nbsp;选择文件 &nbsp;  &nbsp;<span class="buttonSmall">(2 MB Max)</span></span>',
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
            <%=auditscript %>$("#imagebar").attr({ "class": "package_ptfix" });
        } else {
            <%=auditscript %>$("#imagebar").removeAttr("class");
        }
        <%=auditscript %>$('.photo-mask').hide();
    }

    $('#Destination').bind('keyup', function () {
        var url = "../Common/GetAutoDropList.aspx?action=JournalDestination&SerchName=" + encodeURI(this.value);
        if (this.value.length > 1) show(this, "Destinationid", url, "yes");
    });

    function afterselet(callback) {
        var des_id = $("#Destinationid").val();
        if (des_id != "" )
        {
            $('#DestinationList').show();
            var haveit = 0;
            $("#DestinationList div").each(function () {
                var pid = $(this).attr("tag");
                if (pid == des_id) {
                    $(this).attr({ "class": "des_select" });
                    haveit = 1;
                }
            });
            if (haveit == 0) {
                var str = "<div class=des_select tag=" + des_id + ">";
                str += $("#Destination").val();
                str += "</div>";
                $("#DestinationList").append(str);
            }
            $("#Destination").val("");
            $("#Destination").focus();
        }
        else
        {
            $("#Destination").val("");
            $("#Destination").focus();
        }
        
    }

    $("#DestinationList div").live("click", function () {
        if ($(this).attr("class") == "des_select") {
            $(this).attr({ "class": "des_deselect" });
        }
        else {
            $(this).attr({ "class": "des_select" });
        }
    })

</script>
<script type="text/javascript">
    var options = {
        initialFrameWidth: 660
        ,initialFrameHeight: 320
        , focus: false
        ,elementPathEnabled: false
        , contextMenu: []
        , pasteplain: true
        ,toolbars: [
            ['source', '|', 'undo', 'redo', '|',
            'bold', 'italic', 'underline', 'horizontal', 'pasteplain', 'link', 'removeformat', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
            'emotion', 'gmap', 'spechars', 'wordimage']
        ]
        , wordCount: false
    };
    var ue = UE.getEditor('editor', options);
    var domUtils = UE.dom.domUtils;
    ue.addListener("ready", function () {
        //ue.focus(true);
        UE.getEditor('editor').execCommand('insertHtml', " ");
    });

    var DefaultAlbumid = "0";
    var DefaultAlbumUrl = "";
    var FirstDesId = "0";
    var DesId = "";
    var DesName = "";
    var DesList = "";

    $(document).ready(function () {
        
        $("#tags-avilable em").click(function () {
            $(this).SetTag();
        });

        $(".mask-bg em").click(function () {
            $(this).SetCover();
        });

        $('.uppic IMG').live("click", function () {
            var imgurl = $(this).attr("src").replace("_T100.", "_T600.");
            var picid = $(this).attr("picid");
            UE.getEditor('editor').execCommand('insertHtml', "<p><img src=\"" + imgurl + "\" picid=\"" + picid + "\"></p>");
            $('.photo-mask').hide();
            $(this).parent().parent('.progressWrapper').hide();
        })

        $('.uppic').live("mouseover", function () {
            $('.photo-mask').css({ "top": ($(this).offset().top) + "px", "left": ($(this).offset().left) + "px" });
            
            $(".photo-mask em").attr({ "url": $(this).html() });
            if ($('#cover .uppiccover').html() == $(this).html()) {
                $(".photo-mask em").attr({ "class": "ok" });
                $(".photo-mask em").html("已设封面");
            }
            else {
                $(".photo-mask em").removeAttr("class");
                $(".photo-mask em").html("设为封面");
            }
            $('.photo-mask').show();

        })

        $("#fsUploadProgress").mouseout(function () {
            $('.photo-mask').hide();
        });

        $(".photo-mask").mouseover(function () {
            $(this).show();
        });
    });

    jQuery.fn.SetTag = function () {
        var tagv = $("#tag").val();
        if ($.inArray($(this).html(), tagv.split(/\s*[,|，]\s*/)) == -1) {
            tagv += (tagv != '' && tagv.split('').pop() != ',') ? ',' : '';
            $("#tag").val(tagv + $(this).html());
        }
    };

    jQuery.fn.SetCover = function () {
        $('#cover').show();
        $('#cover .uppiccover').html($(this).attr("url"));
        $("#coverpicurl").val($('.uppic IMG').attr("src").replace("_T100.", "_T300."));
        $(".photo-mask em").attr({ "class": "ok" });
        $(".photo-mask em").html("已设封面");
    };

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

    function getDestion() {
        DesId = ",";
        DesName = ",";
        DesList = "";
        FirstDesId = "0";
        $("#DestinationList div").each(function () {
            if ($(this).attr("class") == "des_select") {
                DesId += $(this).attr("tag") +  ",";
                DesName += $(this).html() + ",";
                DesList += "<div class=des_select tag=" + $(this).attr("tag") + ">";
                DesList += $(this).html();
                DesList += "</div>";
                if (FirstDesId == '0') {
                    FirstDesId = $(this).attr("tag");
                }
            }
        });
    }

    function Audit() {
        getDestion();
        $("#Destinationid").val(DesId);
        $("#DestinationName").val(DesName);
        $("#Des_List").val(DesList);
        $("#FirstDestination").val(FirstDesId);
        if ($("#Destinationid").val().length < 2) {
            jError('<strong>请输入目的地!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        $("#OrderBtn").hide();
        $("#islogin").show();
        var url = "JournalsService.aspx?action=Audit&r=" + Math.random();
        $.post(url, $("#form_data").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                jSuccess('<strong>审核成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                $("#islogin").hide();
                $("#OrderBtn").show();
            }
            else {
                $("#islogin").hide();
                $("#OrderBtn").show();
                alert(obj.error);
            }
        });
    }

    function getImages() {
        var imgs = $("#ueditor_0").contents().find("img");
        var uploadedList = "";
        if (imgs.length > 0) {
            for (var i = 0; i < imgs.length; i++) {
                var picid = imgs[i].getAttribute("picid");
                if (picid.length > 0) {
                    uploadedList += picid + ",";
                    if (DefaultAlbumid == '0') {
                        DefaultAlbumid = picid;
                        DefaultAlbumUrl = "<IMG src=\"" + imgs[i].getAttribute("src").replace("_T600.", "_T100.") + "\" >";
                    }
                    
                }
            }
        }
        if (uploadedList.length > 1) uploadedList = uploadedList.substr(0, uploadedList.length - 1);
        return uploadedList;
    }

    function Save(flag) {
        if ($("#uid").val() == "ok") {
            jError('<strong>已经审核，不能再次操作!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        if ($("#ImportFlag").val() != "1") {
            DefaultAlbumid = '0';
            var album_all = getImages();
            $("#albumid").val(album_all);
            //封面图片id和url
            if ($('#cover .uppiccover').html().length > 10) {
                $("#coverimgurl").val($('#cover .uppiccover').html());
                $("#coverimgid").val($('#cover .uppiccover img').attr("picid"));
            }
            else {
                $("#coverimgid").val(DefaultAlbumid);
                $("#coverimgurl").val(DefaultAlbumUrl);
            }
        }

        if (UE.getEditor('editor').hasContents() == false || $("#title").val() == "") {
            jError('<strong>请输入游记标题和正文!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        var tags = $("#tag").val().trim().split(/\s*[,|，]\s*/);
        if (tags && !tags.join("").trim().isNullOrEmpty()) {
            if (10 < tags.length) {
                jError('<strong>最多可以添加10个分类标签</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }
            else {
                var flag1 = false;
                for (var i = 0; tags[i]; i++) {
                    if (6 < tags[i].length) {
                        flag1 = true;
                        break;
                    }
                }
                if (flag1) {
                    jError('<strong>每个分类标签请控制在6个字以内</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return;
                }
            }
        }
        else {
        }

        getDestion();
        $("#Destinationid").val(DesId);
        $("#DestinationName").val(DesName);
        $("#Des_List").val(DesList);
        $("#FirstDestination").val(FirstDesId);
        if ($("#Destinationid").val().length < 2) {
            jError('<strong>请输入目的地!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        //$("#contents").val(UE.getEditor('editor').getContent());
        $("#OrderBtn").hide();
        $("#islogin").show();
        var url = "";
        if (flag == "S") url = "JournalsService.aspx?action=Save&r=" + Math.random();
        if (flag == "A") url = "JournalsService.aspx?action=Audit&r=" + Math.random();

        $.post(url, $("#form_data").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                $("#uid").val(obj.success);
                jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                $("#islogin").hide();
                $("#OrderBtn").show();
            }
            else {
                $("#islogin").hide();
                $("#OrderBtn").show();
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
