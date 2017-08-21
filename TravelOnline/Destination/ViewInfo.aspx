<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewInfo.aspx.cs" Inherits="TravelOnline.Destination.ViewInfo" %>
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
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <style>
        .journals-content {width:940px}
        .journals-content .txt-input{width:940px;height:30px;line-height:30px;padding:0 4px;border:1px solid #c2c2c2;vertical-align:top;font-size:14px; color:#666;}
        .journals-content textarea{width:940px;height:100px;line-height:20px;padding:0 4px;border:1px solid #c2c2c2;vertical-align:top;font-size:14px; color:#666;}
        .journals-content img{CURSOR: pointer;}
        .journals-content .tags-list{width:940px;}
    </style>
</head>
<body id="none">
<uc1:Header ID="Header1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<DIV class="w main">
<div id="order_title">
<h2 class="headline"><SPAN class=headstep><%=pagetitle%></span></h2>
</div>
<DIV class=clr></DIV>
<form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input id="desid" name="desid" type="hidden" value="<%=desid%>"/>
        <input id="id" name="id" type="hidden" value="<%=id%>"/>
        <input id="uid" name="uid" type="hidden" value="<%=uid%>"/>
        <input id="OldName" type="OldName" value="<%=OldName %>"/>
    </DIV>
    <table style="width: 100%;" border="0">
        <tr>
            <td valign="top">
                <div class="journals-content" name="wtitle" id="wtitle">
	                <label style="color:#000"> <i></i>景点名称：<span class="memo">(最多输入50个汉字)</span></label>
	                <input type="text" class="txt-input" id="viewname" name="viewname" maxlength="50" value="<%=viewname%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div14">
	                <label style="color:#000"> <i></i>页面标题seo：<span class="memo">(最多输入100个汉字)</span></label>
	                <input type="text" class="txt-input" id="SeoName" name="SeoName" maxlength="100" value="<%=SeoName%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div13">
	                <label style="color:#000"> <i></i>拼音：<span class="memo">(拼音自动生成，如有错误请手工修改)</span></label>
	                <input type="text" class="txt-input" style="width:300px;" id="PinYin" name="PinYin" maxlength="200" value="<%=PinYin%>"/> <span class="memo">(完整拼音)</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" class="txt-input" style="width:100px;" id="SortPinYin" name="SortPinYin" maxlength="50" value="<%=SortPinYin%>"/> <span class="memo">(拼音首字母)</span>
                </div>
                <div class="journals-content" name="wtitle" id="Div1">
	                <label style="color:#000"> <i></i>景点地址：<span class="memo">(最多输入100个汉字)</span></label>
	                <input type="text" class="txt-input" id="address" name="address" maxlength="100" value="<%=address%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div3">
	                <label style="color:#000"> <i></i>联系电话：<span class="memo">(换行用 / 隔开，例如：电话：123456/传真：654321/手机：135123)</span></label>
	                <input type="text" class="txt-input" id="tel" name="tel" maxlength="100" value="<%=tel%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div4">
	                <label style="color:#000"> <i></i>门票价格：<span class="memo">(换行用 / 隔开，例如：成人：100元/儿童：50元/团队：30元)</span></label>
	                <input type="text" class="txt-input" id="ticket" name="ticket" maxlength="250" value="<%=ticket%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div6">
	                <label style="color:#000"> <i></i>票价备注：<span class="memo">(例如：持有军官证及12岁以下儿童免费 )</span></label>
	                <input type="text" class="txt-input" id="ticketmemo" name="ticketmemo" maxlength="250" value="<%=ticketmemo%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div9">
	                <label style="color:#000"> <i></i>开放时间：<span class="memo">(换行用 / 隔开，例如：10月1日到5月31日 9:00~16:00/6月1日到9月30日 8:00~15:00)</span></label>
	                <input type="text" class="txt-input" id="opentime" name="opentime" maxlength="250" value="<%=opentime%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div7">
	                <label style="color:#000"> <i></i>推荐游览季节：<span class="memo">(换行用 / 隔开，例如：1月~10月)</span></label>
	                <input type="text" class="txt-input" id="visitseason" name="visitseason" maxlength="250" value="<%=visitseason%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div8">
	                <label style="color:#000"> <i></i>一般游览用时：<span class="memo">(换行用 / 隔开，例如：从正门进入游览1~2小时/乘坐缆车游览1小时)</span></label>
	                <input type="text" class="txt-input" id="visittime" name="visittime" maxlength="250" value="<%=visittime%>"/>
                </div>
                <div class="journals-content" name="wtitle" id="Div5">
	                <label style="color:#000"> <i></i>景点介绍：<span class="memo">(回车换行，字数不限)</span>&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="扩展输入框高度" onclick="AddIt('intro')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('intro')" alt="" src="../../Images/iup.gif" align="absMiddle"></label>
	                <textarea name="intro" id="intro"><%=intro%></textarea>
                </div>
                <div class="journals-content" name="wtitle" id="Div10">
	                <label style="color:#000"> <i></i>看点推荐：<span class="memo">(回车换行，字数不限)</span>&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="扩展输入框高度" onclick="AddIt('viewpoint')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('viewpoint')" alt="" src="../../Images/iup.gif" align="absMiddle"></label>
	                <textarea name="viewpoint" id="viewpoint"><%=viewpoint%></textarea>
                </div>
                <div class="journals-content" name="wtitle" id="Div11">
	                <label style="color:#000"> <i></i>景点交通：<span class="memo">(回车换行，字数不限)</span>&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="扩展输入框高度" onclick="AddIt('traffic')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('traffic')" alt="" src="../../Images/iup.gif" align="absMiddle"></label>
	                <textarea name="traffic" id="traffic"><%=traffic%></textarea>
                </div>
                <div class="journals-content" name="wtitle" id="Div12">
	                <label style="color:#000"> <i></i>特别提示：<span class="memo">(回车换行，字数不限)</span>&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="扩展输入框高度" onclick="AddIt('memo')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('memo')" alt="" src="../../Images/iup.gif" align="absMiddle"></label>
	                <textarea name="memo" id="memo"><%=memo%></textarea>
                </div>
                <div class="journals-content" name="wtitle" id="Div2">
	                <label style="color:#000"> <i></i>百度地图经纬度：&nbsp;&nbsp;<span class="memo"><a href="http://api.map.baidu.com/lbsapi/creatmap/index.html" target=_blank>创建地图页面点这里</a></span></label>
	                <input type="text" class="txt-input" style="width:100px;" id="map_x" name="map_x" maxlength="50" value="<%=map_x%>"/> <span class="memo">(X经度，例如：116.395645)</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" class="txt-input" style="width:100px;" id="map_y" name="map_y" maxlength="50" value="<%=map_y%>"/> <span class="memo">(Y纬度，例如：39.929986)</span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input type="text" class="txt-input" style="width:40px;" id="map_size" name="map_size" maxlength="10" value="<%=map_size%>"/> <span class="memo">(地图级别，例如：16)</span>
                </div>
                <div class="journals-content" name="#wtag" id="wtag" style="color:#000">
	                <label> 标签：<span class="memo">(多个标签请用逗号,分隔，最多10个)</span><span id="tagwarn" class="warn"></span></label>
	                <div  class="tags-list" id="tags-avilable">
                        <em>自然景观</em><em>建筑人文</em><em>古迹</em><em>博物馆</em><em>购物</em><em>河湖</em><em>海滨</em><em>古镇/古村</em><em>纪念</em><em>宗教场所</em><em>山岳</em><em>温泉</em>
                        <em>主题乐园</em><em>滑雪</em><em>水族馆</em><em>植物园</em><em>公园</em><em>户外运动</em><em>室内运动</em><em>水上运动</em><em>园林</em><em>艺术馆</em><em>河湖漂流</em><em>游船</em><em>表演</em><em>艺术馆</em>
                    </div>
	                <input id="tag" name="tag" type="text" class="txt-input" value="<%=tags %>"/>
                </div>
            </td>
        </tr>
    </table>
</form>
<div class="gotonext">
<span style="padding-left: 30px">&nbsp;</span><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>
<%=BuyButton %>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#viewname").blur(function () {
            $(this).GetPinYin();
        });
    });

    jQuery.fn.GetPinYin = function () {
        var cnname = $.trim($(this).val());
        if (cnname.length < 2 || cnname == "") {
            return false;
        }
        var oldname = $("#OldName").val();
        if (oldname != cnname) {
            var url = "../Management/AjaxService.aspx?action=GetPinYin&CnName=" + escape(cnname) + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                $("#PinYin").val(date.py);
                $("#SortPinYin").val(date.sortpy);
            })
            $("#OldName").val(cnname);
        }
    };

    function AddNew() {
        window.location.reload();
    }

    function AddIt(tname) {
        var this_obj = eval('document.all.' + tname);
        var h = this_obj.offsetHeight
        this_obj.style.height = h + 400;
    }

    function DecIt(tname) {
        var this_obj = eval('document.all.' + tname);
        this_obj.style.height = "100";
    }


    $(document).ready(function () {
        $("#tags-avilable em").click(function () {
            $(this).SetTag();
        });
    });

    jQuery.fn.SetTag = function () {
        var tagv = $("#tag").val();
        if ($.inArray($(this).html(), tagv.split(/\s*[,|，]\s*/)) == -1) {
            tagv += (tagv != '' && tagv.split('').pop() != ',') ? ',' : '';
            $("#tag").val(tagv + $(this).html());
        }
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
    
    function Save(flag) {
        if ($("#viewname").val() == "" || $("#intro").val() == "") {
            jError('<strong>请输入景点名称和景点介绍!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
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
        $("#AddBtn").hide();
        $("#OrderBtn").hide();
        $("#islogin").show();
        var url = "/Management/AjaxService.aspx?action=SaveView&r=" + Math.random();
        $.post(url, $("#form_data").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                $("#uid").val(obj.success);
                jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                $("#islogin").hide();
                $("#OrderBtn").show();
                $("#AddBtn").show();
            }
            else {
                $("#islogin").hide();
                $("#OrderBtn").show();
                $("#AddBtn").show();
                jError('<strong>保存失败，请稍后再试！</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
        });
    }
</script>
<DIV class=clr></DIV>
</DIV>
<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>