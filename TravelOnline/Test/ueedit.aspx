<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ueedit.aspx.cs" Inherits="TravelOnline.Test.ueedit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="public.css" type="text/css">
    <link rel="stylesheet" href="onlinedemo.css" type="text/css">

    <script type="text/javascript" charset="utf-8">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>

    <script type=text/javascript src="/ueditor/ueditor.config.js"></script>  
    <script type=text/javascript src="/ueditor/ueditor.all.js"></script>
</head>
<body>
<div id="wrapper">
    
    <div id="content" class="w900 border-style1 bg">
        <div class="section">
            <h3>UEditor - 完整示例</h3>
 
            <p class="note">注：线上演示版上传图片功能一次只能上传一张，涂鸦功能不能将背景和图片合成，而下载版没有限制</p>
 
            <div class="details">
                <div>
                    <script type="text/plain" id="editor"></script>
                    <div class="con-split"></div>
                </div>
            </div>
        </div>

    </div>
</div>
<script type="text/javascript">
    //实例化编辑器
    var options = {
        imageUrl: UEDITOR_HOME_URL + "../yunserver/yunImageUp.php",
        imagePath: "http://",

        scrawlUrl: UEDITOR_HOME_URL + "../yunserver/yunScrawlUp.php",
        scrawlPath: "http://",

        fileUrl: UEDITOR_HOME_URL + "../yunserver/yunFileUp.php",
        filePath: "http://",

        catcherUrl: UEDITOR_HOME_URL + "php/getRemoteImage.php",
        catcherPath: UEDITOR_HOME_URL + "php/",

        imageManagerUrl: UEDITOR_HOME_URL + "../yunserver/yunImgManage.php",
        imageManagerPath: "http://",

        snapscreenHost: 'ueditor.baidu.com',
        snapscreenServerUrl: UEDITOR_HOME_URL + "../yunserver/yunSnapImgUp.php",
        snapscreenPath: "http://",

        wordImageUrl: UEDITOR_HOME_URL + "../yunserver/yunImageUp.php",
        wordImagePath: "http://", //

        getMovieUrl: UEDITOR_HOME_URL + "../yunserver/getMovie.php",

        lang: /^zh/.test(navigator.language || navigator.browserLanguage || navigator.userLanguage) ? 'zh-cn' : 'en',
        langPath: UEDITOR_HOME_URL + "lang/",

        webAppKey: "9HrmGf2ul4mlyK8ktO2Ziayd",
        initialFrameWidth: 860,
        initialFrameHeight: 420,
        focus: true
    };
    var ue = UE.getEditor('editor', options);
    var domUtils = UE.dom.domUtils;

    ue.addListener("ready", function () {
        ue.focus(true);
    });
    
</script>

</body>

</html>
