<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm8.aspx.cs" Inherits="TravelOnline.Test.WebForm8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:mudoo>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>POPHint & statInput 整合效果</title>
<style type="text/css">
<!--
* {padding: 0; margin: 0}
body {margin: 3em; font: 12px Tahoma; background: #EAEAEA; color: #333333; line-height: 20px}
input, textarea {font: 12px Tahoma; color: #666666; padding: 2px; border: solid 1px #DBDBDB}
textarea {padding: 5px; line-height: 20px}
p {margin: 1em 0}
ul {}
li {height: 1%; overflow: hidden; list-style-type: none}
a {color: #666666; text-decoration: none}
a:hover {color: #333333}
.r {float: right}
.l {float: left}
.b {font-weight: bold}
.gray {color: #666666; margin-top: 8px}
.light {color:#FF6600; margin: 0 5px}
.case {display: block; padding: 0 2em 2em 2em; border: solid 1px #EAEAEA; background: #FFFFFF; margin-bottom: 2em; height: 1%; overflow: hidden}
.title {display:block; padding: .5em 2em .5em 1em; margin: 0 -2em 2em -2em; font-weight: bold; color: #000000; background: #FAFAFA; border-bottom: solid 1px #EAEAEA}
.call {display:block;}
.key {display: block; width: 6em; float: left}
.type {display: block; width: 6em; float: left}
.info {padding-left: 2em}
.demo {margin-bottom: 2em}
/* popHint Style */
#popHint {position: absolute; line-height: normal}
#popHint .popLeft, #popHint .popRight, #popHint .popAngle span, #popHintText, #popHint .popIcon {background-image: url(http://i.namipan.com/files/48ee6e2804bb4b1d84ecea96218e4a50081349ba960100008f09/0/PopHint.gif)}
#popHint .popHeader {height: 1%; overflow: hidden !important; overflow /**/: visible}
 #popHint .popLeft {float: left; width: 5px; height: 24px; background-position: 0 0; background-repeat: no-repeat}
 #popHint .popRight {float: left; width: 5px; height: 24px; background-position: -10px -25px; background-repeat: no-repeat}
 #popHint .popAngle {clear: both; position: relative; height: 10px}
 #popHint .popAngle span {position: absolute; top: -3px; left: 15px; width: 7px; height: 13px; background-position: 0 -75px; background-repeat: no-repeat}
#popHintText {float: left; color: #975400; height: 19px !important; height /**/: 24px; padding: 5px 0 0 0; white-space: nowrap; background-position: 0 -50px; background-repeat: repeat-x; overflow: hidden}
    #popHintText .popIcon {float: left; width: 15px; height: 10px; margin: 1px 3px 0 0}
 #popHint .wrong {background-position: 0 -90px; background-repeat: no-repeat}
 #popHint .right {background-position: 0 -105px; background-repeat: no-repeat}
 /* 这里可以自己扩展图标.(15*10) */
-->
</style>
<script language="javascript" type="text/javascript">
<!--
    // 这里都是公用函数，挺多的...
    var 
    // 获取元素
$ = function (element) {
    return (typeof (element) == 'object' ? element : document.getElementById(element));
},
    // 判断浏览器
brower = function () {
    var ua = navigator.userAgent.toLowerCase();
    var os = new Object();
    os.isFirefox = ua.indexOf('gecko') != -1;
    os.isOpera = ua.indexOf('opera') != -1;
    os.isIE = !os.isOpera && ua.indexOf('msie') != -1;
    os.isIE7 = os.isIE && ua.indexOf('7.0') != -1;
    return os;
},
    // 生成元素到refNode
appendElement = function (tagName, Attribute, strHtml, refNode) {
    var cEle = document.createElement(tagName);
    // 属性值
    for (var i in Attribute) {
        cEle.setAttribute(i, Attribute[i]);
    }
    cEle.innerHTML = strHtml;

    refNode.appendChild(cEle);
    return cEle;
},
    // 获取元素坐标
getCoords = function (node) {
    var x = node.offsetLeft;
    var y = node.offsetTop;
    var parent = node.offsetParent;
    while (parent != null) {
        x += parent.offsetLeft;
        y += parent.offsetTop;
        parent = parent.offsetParent;
    }
    return { x: x, y: y };
},
    // 事件操作(可保留原有事件)
eventListeners = [],
findEventListener = function (node, event, handler) {
    var i;
    for (i in eventListeners) {
        if (eventListeners[i].node == node && eventListeners[i].event == event && eventListeners[i].handler == handler) {
            return i;
        }
    }
    return null;
},
myAddEventListener = function (node, event, handler) {
    if (findEventListener(node, event, handler) != null) {
        return;
    }
    if (!node.addEventListener) {
        node.attachEvent('on' + event, handler);
    } else {
        node.addEventListener(event, handler, false);
    }
    eventListeners.push({ node: node, event: event, handler: handler });
},
removeEventListenerIndex = function (index) {
    var eventListener = eventListeners[index];
    delete eventListeners[index];
    if (!eventListener.node.removeEventListener) {
        eventListener.node.detachEvent('on' + eventListener.event,
  eventListener.handler);
    } else {
        eventListener.node.removeEventListener(eventListener.event,
  eventListener.handler, false);
    }
},
myRemoveEventListener = function (node, event, handler) {
    var index = findEventListener(node, event, handler);
    if (index == null) return;
    removeEventListenerIndex(index);
},
cleanupEventListeners = function () {
    var i;
    for (i = eventListeners.length; i > 0; i--) {
        if (eventListeners[i] != undefined) {
            removeEventListenerIndex(i);
        }
    }
};
-->
</script>
<script language="javascript" type="text/javascript">
<!--
    /*======================================================
    - statInput 输入限制统计
    - By Mudoo 2008.5
    - 长度超出_max的话就截取...貌似没有更好的办法了
    ======================================================*/
    function statInput(e, _max, _exp) {
        e = $(e);
        _max = parseInt(_max);
        _max = isNaN(_max) ? 0 : _max;
        _exp = _exp == undefined ? {} : _exp;

        if (e == null || _max == 0) {
            alert('statInput初始化失败！');
            return;
        }

        var 
        // 浏览器
 _brower = brower();
        // 输出对象
        _objMax = _exp._max == undefined ? null : $(_exp._max),
 _objTotal = _exp._total == undefined ? null : $(_exp._total),
 _objLeft = _exp._left == undefined ? null : $(_exp._left),
        // 弹出提示
 _hint = _exp._hint == undefined ? null : _exp._hint;

        // 初始统计
        if (_objMax != null) _objMax.innerHTML = _max;
        if (_objTotal != null) _objTotal.innerHTML = 0;
        if (_objLeft != null) _objLeft.innerHTML = 0;

        // 设置监听事件
        // 输入这个方法比较好.
        // 但是Opera下中文输入跟粘贴不能正确统计...相当BT的东西...
        // 如果不考虑Opera的话就用这个吧.否则就老老实实用计时器.
        if (_brower.isIE) {
            myAddEventListener(e, "propertychange", stat);
        } else {
            myAddEventListener(e, "input", stat);
        }
        /*
        // 用计时器的话就什么浏览器都支持了.
        var _intDo = null;
        myAddEventListener(e, "focus", setListen);
        myAddEventListener(e, "blur", remListen);
        function setListen() {
        _intDo = setInterval(stat, 10);
        }
        function remListen() {
        clearInterval(_intDo);
        }
        */

        // 统计函数
        var _len, _olen, _lastRN, _sTop;
        _olen = _len = 0;
        function stat() {
            _len = e.value.length;
            if (_len == _olen) return;  // 防止用计时器监听时做无谓的牺牲...
            if (_len > _max) {
                _sTop = e.scrollTop;
                // 避免IE最后俩字符为'\r\n'.导致崩溃...
                _lastRN = (e.value.substr(_max - 1, 2) == "\r\n");
                e.value = e.value.substr(0, (_lastRN ? _max - 1 : _max));
                if (_hint == true) popHint(e, "悟空你也太调皮了，为师跟你说过，叫你不要输那么多字~~.");
                // 解决FF老是跑回顶部
                if (_brower.isFirefox) e.scrollTop = e.scrollHeight;
            }
            _olen = _len = e.value.length;

            // 显示已输入字数
            if (_objTotal != null) _objTotal.innerHTML = _len;
            // 显示剩余可输入字数
            if (_objLeft != null) _objLeft.innerHTML = (_max - _len) < 0 ? 0 : (_max - _len);
        }

        stat();
    }
    /*********************************************
    - POPHint 弹出提示框
    - By Mudoo 2008.5
    **********************************************/
    function popHint(obj, msg, initValues) {
        var 
 _obj = $(obj),
 _objHint = $("popHint"),
 _msg = msg,
 _init = initValues;

        // 初始化失败...
        if (_obj == undefined || _msg == undefined || _msg == "") return;

        // 设置初始值
        _init = _init == undefined ? { _type: "wrong", _event: "click"} : _init;
        // obj如果不可见。设置弹出对象为obj父元素
        if (_obj.style.display == 'none' || _obj.style.visibility == 'hidden' || _obj.getAttribute('type') == 'hidden') _obj = _obj.parentNode;

        var 
 _type = null,
 _event = null,
 _place = getCoords(_obj),
 _marTop = null,
 _objText = $("popHintText"),

        // 初始化
 init = function () {
     var _hint = _obj.getAttribute("hint");
     if (_hint == "false") return;

     // 有的时候initValues不为空.但是只设置一个值...避免发生错误.再次设置初始值...
     _type = _init._type == undefined ? "wrong" : _init._type;
     _type = _type.toLowerCase();
     _event = _init._event == undefined ? "click" : _init._event;
     _event = _event.toLowerCase();

     /*
     ******************************************
     popHtml
     ******************************************
     <div id="popHint">
     <div id="popHeader">
     <div class="popLeft"></div>
     <div id="popHintText"><span class=\"popIcon wrong></span>请输入您的用户名！</div>
     <div class="popRight"></div>
     </div>
     <div class="popAngle"><span></span></div>
     </div>
     */

     // 好了.输出...
     var _Html = "<div id=\"popHeader\">" +
     " <div class=\"popLeft\"></div>" +
     " <div id=\"popHintText\"></div>" +
     " <div class=\"popRight\"></div>" +
     "</div>" +
     "<div class=\"popAngle\"><span></span></div>"

     if (_objHint == null) {
         _objHint = appendElement("div", { "id": "popHint" }, _Html, document.body);
         _objHint.style.display = "none";
         _objText = $("popHintText");
     }

     show();
 },
        // 显示
 show = function () {
     _objHint.style.display = "";
     _marTop = _objHint.offsetHeight;

     _msg = "<span class=\"popIcon " + _type + "\"></span>" + _msg;
     _objText.innerHTML = _msg;

     _objHint.style.left = _place.x + "px";
     _objHint.style.top = (_place.y - _marTop + 8) + "px";

     // 关闭触发事件
     switch (_event) {
         case "blur":
             myAddEventListener(_obj, 'blur', hide);
             break;
         //default : 
         case "click":
             myAddEventListener(document, 'mousedown', hide);
             break;
         //这里可以自己扩展很多事件... 
     }
 },
        // 关闭
 hide = function () {
     _objHint.style.display = "none";
     _objText.innerHTML = "";
     // 移除关闭触发事件
     myRemoveEventListener(_obj, 'blur', hide);
     myRemoveEventListener(document, 'mousedown', hide);
 };

        init();
    }
-->
</script>
<script language="javascript" type="text/javascript">
<!--
    /*********************************************
    - statInput 演示函数
    *********************************************/
    myAddEventListener(window, "load", testStatInput);
    function testStatInput() {
        statInput('Test_1', 100, { _max: 'stat_max', _total: 'stat_total', _left: 'stat_left', _hint: true });
    }
    /*********************************************
    - popHint 演示函数
    *********************************************/
    function testPopHint() {
        if ($('Test2').value == '') {
            popHint($('Test2'), 'Test2不能为空...', { _event: 'blur' });
            $('Test2').focus();
            return false;
        }
        if ($('Test3').value == '') {
            popHint($('Test3'), 'Test3也不能为空...', 'blur');
            $('Test3').focus();
            return false;
        }
        if ($('Test4').value == '') {
            popHint($('Test4'), 'Test4虽然看不见,但也不能为空...');
            $('Test4').value = '填一点进去...';
            return false;
        }
        if ($('Test5').value == '') {
            popHint($('Test5'), 'Test5也一样...');
            return false;
        }
    }
-->
</script>
</head>
<body>
<div class="case">
 <div class="title"><a href="#" class="r">Top</a>statInput 调用方法</div>
 <div class="b">statInput(Element, MaxLen, {objMax, objTotal, objLeft, Hint});</div>
 <ul class="info gray">
  <li><span class="key">Element：</span><span class="type">Object</span>限制统计对象 (*必须)</li>
  <li><span class="key">MaxLen：</span><span class="type">Number</span>最大可输入长度 (*必须)</li>
  <li><span class="key">objMax：</span><span class="type">Object</span>显示最大输入长度对象 (*可选)</li>
  <li><span class="key">objTotal：</span><span class="type">Object</span>显示输入统计对象 (*可选)</li>
  <li><span class="key">objLeft：</span><span class="type">Object</span>显示可输入长度对象 (*可选)</li>
  <li><span class="key">Hint：</span><span class="type">Boolean</span>当长度超出上限时，是否提示 (*可选)</li>
 </ul>
</div>
<div class="case">
 <div class="title"><a href="#" class="r">Top</a>statInput 演示</div>
 <textarea name="Test_1" id="Test_1" rows="10" style="width: 99%" >悟空你也太调皮了，我跟你说过，叫你不要乱扔东西， 乱扔东西这么做…… （咣当，悟空棍子掉在地上） 你看我还没有说完呢你把棍子又给扔掉了！月光宝盒是宝物，你把他扔掉会污染环境，唉，要是砸到小朋友那怎么办？就算没有砸到小朋友砸到那些花花草草也是不对的呀！</textarea>
 <div class="gray">最多可输入<span id="stat_max" class="b light"></span>，当前共<span id="stat_total" class="b light"></span>，还可输入<span id="stat_left" class="b light"></span></div>
</div>
<br />
<br />
<br />
<br />
<br />
<div class="case">
 <div class="title"><a href="#" class="r">Top</a>popHint 调用方法(目前只支持单行)</div>
 <div class="b">popHint(Element, Hint, {Type, Event});</div>
 <ul class="info gray">
  <li><span class="key">Element：</span><span class="type">Object</span>弹出对象。根据它来定位的。  (*必须)</li>
  <li><span class="key">Hint：</span><span class="type">String</span>弹出的信息，支持HTML可是不能换行。  (*必须)</li>
  <li><span class="key">Type：</span><span class="type">String</span>弹出类型。其实说类型是不对的。只是定义个图标而已...(可自己在样式里加很多很多"类型")  (*可选)</li>
  <li><span class="key">Event：</span><span class="type">String</span>关闭触发事件。目前只能绑定单个事件(默认click=document.onmousedown,blur=Element.onblur)  (*可选)</li>
 </ul>
 <br /><span style="color: #333333" class="b">详见：<a href="http://bbs.blueidea.com/thread-2856572-1-1.html" target="_blank" >http://bbs.blueidea.com/thread-2856572-1-1.html</a></span>
</div>
<div class="case">
 <div class="title"><a href="#" class="r">Top</a>popHint 演示</div>
 <ul class="demo gray">
  <li>测试blur不关闭：<input name="Test1_0" id="Test1_0" type="text" size="20" maxlength="20" onfocus="popHint(this, '失去焦点不会关闭提示.按TAB键看看');" value="" />
  <a href="###"> </a></li>
  <li>测试blur关闭：<input name="Test1_1" id="Test1_1" type="text" size="20" maxlength="20" onfocus="popHint(this, '文本框失去焦点就关闭.', {_event : 'blur'});" value="" /></li>
 </ul>
 <ul class="demo gray">
  <li>Test2：<input name="Test2" id="Test2" type="text" size="20" maxlength="20" value="" /></li>
  <li>Test3：<input name="Test3" id="Test3" type="text" size="20" maxlength="20" value="" /></li>
  <li>Test4：<input name="Test4" id="Test4" type="hidden" size="20" maxlength="20" value="" /></li>
  <li>Test5：<input name="Test5" id="Test5" type="text" size="20" maxlength="20" value="" style="display:none" /></li>
  <li><input name="" type="button" onclick="testPopHint();" value="来测试啦" /></li>
 </ul>
</div>
</body>
</html>
<a href="http://js.alixixi.com/">欢迎访问阿里西西网页特效代码站，js.alixixi.com</a>
