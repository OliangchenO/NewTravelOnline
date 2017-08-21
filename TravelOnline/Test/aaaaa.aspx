﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaaaa.aspx.cs" Inherits="TravelOnline.Test.aaaaa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<style type="text/css">
.mod_select{position:absolute;left:30%;top:100px;font-familY:Arial, Helvetica, sans-serif;}
.mod_select ul{margin:0;padding:0;}
.mod_select ul li{list-style-type:none;float:left;margin-left:20px;height:24px;}
.select_label{color:#982F4D;float:left;line-height:24px;padding-right:10px;font-size:12px;font-weight:700;}
.select_box{float:left;border:solid 1px #EDE7D6;color:#444;position:relative;cursor:pointer;width:165px;background:url(../select_bg.jpg) repeat-x;font-size:12px;}
.selet_open{display:inline-block;border-left:solid 1px #E5E5E5;position:absolute;right:0;top:0;width:30px;height:24px;background:url(../select_up.jpg) no-repeat center center;}
.select_txt{display:inline-block;padding-left:10px;width:135px;line-height:24px;height:24px;cursor:text;overflow:hidden;}
.option{width:165px;;border:solid 1px #EDE7D6;position:absolute;top:24px;left:-1px;z-index:2;overflow:hidden;display:none;}
.option a{display:block;height:26px;line-height:26px;text-align:left;padding:0 10px;width:100%;background:#fff;}
.option a:hover{background:#FDE0E5;}
</style>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".select_box").click(function (event) {
            event.stopPropagation();
            $(this).find(".option").toggle();
            $(this).parent().siblings().find(".option").hide();
        });
        $(document).click(function (event) {
            var eo = $(event.target);
            if ($(".select_box").is(":visible") && eo.attr("class") != "option" && !eo.parent(".option").length)
                $('.option').hide();
        });
        /*赋值给文本框*/
        $(".option a").click(function () {
            var value = $(this).text();
            $(this).parent().siblings(".select_txt").text(value);
            $("#select_value").val(value)
        })
    })
</script>
</head>
<body>
<div class="mod_select">
<ul>
<li>
<span class="select_label">sort buy:</span>
<div class="select_box">
<span class="select_txt"></span><a class="selet_open"><b></b></a>
<div class="option">
<a>1</a>
<a>2</a>
<a>3</a>
</div>
</div>
<br clear="all" />
</li>
<li>
<span class="select_label">View:</span>
<div class="select_box">
<span class="select_txt"></span><a class="selet_open"></a>
<div class="option">
<a>1</a>
<a>2</a>
<a>3</a>
</div>
</div>
</li>
</ul>
<input type="hidden" id="select_value" />
</div>
    <table style="width:100%;">
        <tr>
            <td height="40px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</body>
</html>
