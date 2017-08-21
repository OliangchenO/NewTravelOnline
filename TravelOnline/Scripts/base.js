﻿(function ($) {
    $.extend($.browser, {
        client: function () {
            return {
                width: document.documentElement.clientWidth,
                height: document.documentElement.clientHeight,
                bodyWidth: document.body.clientWidth,
                bodyHeight: document.body.clientHeight
            };
        },
        scroll: function () {
            return {
                width: document.documentElement.scrollWidth,
                height: document.documentElement.scrollHeight,
                bodyWidth: document.body.scrollWidth,
                bodyHeight: document.body.scrollHeight,
                left: document.documentElement.scrollLeft + document.body.scrollLeft,
                top: document.documentElement.scrollTop + document.body.scrollTop
            };
        },
        screen: function () {
            return {
                width: window.screen.width,
                height: window.screen.height
            };
        },
        isIE6: $.browser.msie && $.browser.version == 6,
        isMinW: function (val) {
            return Math.min($.browser.client().bodyWidth, $.browser.client().width) <= val;
        },
        isMinH: function (val) {
            return $.browser.client().height <= val;
        }
    })
})(jQuery);
(function ($) {
    $.widthForIE6 = function (option) {
        var s = $.extend({
            max: null,
            min: null,
            padding: 0
        }, option || {});
        var init = function () {
                var w = $(document.body);
                if ($.browser.client().width >= s.max + s.padding) {
                    w.width(s.max + "px");
                } else if ($.browser.client().width <= s.min + s.padding) {
                    w.width(s.min + "px");
                } else {
                    w.width("auto");
                }
            };
        init();
        $(window).resize(init);
    }
})(jQuery);
(function ($) {
    $.fn.hoverForIE6 = function (option) {
        var s = $.extend({
            current: "hover",
            delay: 10
        }, option || {});
        $.each(this, function () {
            var timer1 = null,
                timer2 = null,
                flag = false;
            $(this).bind("mouseover", function () {
                if (flag) {
                    clearTimeout(timer2);
                } else {
                    var _this = $(this);
                    timer1 = setTimeout(function () {
                        _this.addClass(s.current);
                        flag = true;
                    }, s.delay);
                }
            }).bind("mouseout", function () {
                if (flag) {
                    var _this = $(this);
                    timer2 = setTimeout(function () {
                        _this.removeClass(s.current);
                        flag = false;
                    }, s.delay);
                } else {
                    clearTimeout(timer1);
                }
            })
        })
    }
})(jQuery);
(function ($) {
    $.extend({
        _jsonp: {
            scripts: {},
            counter: 1,
            charset: "utf-8",
            head: document.getElementsByTagName("head")[0],
            name: function (callback) {
                var name = '_jsonp_' + (new Date).getTime() + '_' + this.counter;
                this.counter++;
                var cb = function (json) {
                        eval('delete ' + name);
                        callback(json);
                        $._jsonp.head.removeChild($._jsonp.scripts[name]);
                        delete $._jsonp.scripts[name];
                    };
                eval(name + ' = cb');
                return name;
            },
            load: function (url, name) {
                var script = document.createElement('script');
                script.type = 'text/javascript';
                script.charset = this.charset;
                script.src = url;
                this.head.appendChild(script);
                this.scripts[name] = script;
            }
        },
        getJSONP: function (url, callback) {
        	//alert(url);
            var name = $._jsonp.name(callback);
            //alert(name);
            var url = url.replace(/{callback};/, name);
            //alert(url);
            
            $._jsonp.load(url, name);
            return this;
        }
    });
})(jQuery);
(function (a) {
    a.fn.jdTab = function (d, i) {
        if (typeof d == "function") {
            i = d;
            d = {};
        }
        var k = a.extend({
            type: "static",
            auto: true,
            source: "data",
            event: "mouseover",
            currClass: "curr",
            tab: ".tab",
            content: ".tabcon",
            itemTag: "li",
            stay: 5000,
            delay: 100,
            mainTimer: null,
            subTimer: null,
            index: 0
        }, d || {});
        var f = a(this).find(k.tab).eq(0).find(k.itemTag),
            b = a(this).find(k.content);
        if (f.length != b.length) {
            return false;
        }
        var c = k.source.toLowerCase().match(/http:\/\/|\d|\.aspx|\.ascx|\.asp|\.php|\.html\.htm|.shtml|.js|\W/g);
        var j = function (m, l) {
                k.subTimer = setTimeout(function () {
                    e();
                    if (l) {
                        k.index++;
                        if (k.index == f.length) {
                            k.index = 0;
                        }
                    } else {
                        k.index = m;
                    }
                    k.type = (f.eq(k.index).attr(k.source) != null) ? "dynamic" : "static";
                    h();
                }, k.delay);
            };
        var g = function () {
                k.mainTimer = setInterval(function () {
                    j(k.index, true);
                }, k.stay);
            };
        var h = function () {
                f.eq(k.index).addClass(k.currClass);
                switch (k.type) {
                default:
                case "static":
                    var l = "";
                    break;
                case "dynamic":
                    var l = (c == null) ? f.eq(k.index).attr(k.source) : k.source;
                    f.eq(k.index).removeAttr(k.source);
                    break;
                }
                if (i) {
                    i(l, b.eq(k.index), k.index);
                }
                b.eq(k.index).show();
            };
        var e = function () {
                f.eq(k.index).removeClass(k.currClass);
                b.eq(k.index).hide();
            };
        f.each(function (l) {
            a(this).bind(k.event, function () {
                clearTimeout(k.subTimer);
                clearInterval(k.mainTimer);
                j(l, false);
                return false;
            }).bind("mouseleave", function () {
                if (k.auto) {
                    g();
                } else {
                    return;
                }
            });
        });
        if (k.type == "dynamic") {
            j(k.index, false);
        }
        if (k.auto) {
            g();
        }
    };
})(jQuery);

(function (a) {
    a.fn.jdSlide = function (k) {
        var p = a.extend({
            width: null,
            height: null,
            pics: [],
            index: 0,
            type: "num",
            current: "curr",
            delay1: 100,
            delay2: 5000
        }, k || {});
        var i = this;
        var g, f, d, h = 0,
            e = true,
            b = true;
        var n = p.pics.length;
        var o = function () {
                var q = "<ul style='position:absolute;top:0;left:0;'><li><a href='" + p.pics[0].href + "' target='_blank'><img src='" + p.pics[0].src + "' width='" + p.width + "' height='" + p.height + "' /></a></li></ul>";
                i.css({
                    position: "relative"
                }).html(q);
                a(function () {
                    c();
                });
            };
        o();
        var j = function () {
                var s = [];
                s.push("<div>");
                var r;
                var q;
                for (var t = 0; t < n; t++) {
                    r = (t == p.index) ? p.current : "";
                    span_width = "<span style='width:" + ((p.width/p.pics.length).toFixed(4)-1) +"px' class='"
                    switch (p.type) {
                    case "num":
                        q = t + 1;
                    	s.push("<span class='");
                        break;
                    case "string":
                        q = p.pics[t].alt;
                    	s.push(span_width);
                    	break;
                    case "image":
                        q = "<img src='" + p.pics[t].breviary + "' />";
                    	s.push("<span class='");
                    default:
                        break;
                    }
                    
                    s.push(r);
                    s.push("'><a href='");
                    s.push(p.pics[t].href);
                    s.push("' target='_blank'>");
                    s.push(q);
                    s.push("</a></span>");
                }
                s.push("</div>");
                i.append(s.join(""));
                i.find("span").bind("mouseover", function () {
                    b = false;
                    clearTimeout(g);
                    clearTimeout(d);
                    var u = i.find("span").index(this);
                    if (p.index == u) {
                        return;
                    } else {
                        d = setInterval(function () {
                            if (e) {
                                l(u);
                            }
                        }, p.delay1);
                    }
                }).bind("mouseleave", function () {
                    b = true;
                    clearTimeout(g);
                    clearTimeout(d);
                    g = setTimeout(function () {
                        l(p.index + 1, true);
                    }, p.delay2);
                });
            };
        var l = function (r, q) {
                if (r == n) {
                    r = 0;
                }
                f = setTimeout(function () {
                    i.find("span").eq(p.index).removeClass(p.current);
                    i.find("span").eq(r).addClass(p.current);
                    m(r, q);
                }, 20);
            };
        var m = function (u, q) {
                var s = parseInt(h);
                var v = Math.abs(s + p.index * p.height);
                var t = Math.abs(u - p.index) * p.height;
                var r = Math.ceil((t - v) / 4);
                if (v == t) {
                    clearTimeout(f);
                    if (q) {
                        p.index++;
                        if (p.index == n) {
                            p.index = 0;
                        }
                    } else {
                        p.index = u;
                    }
                    e = true;
                    if (e && b) {
                        clearTimeout(g);
                        g = setTimeout(function () {
                            l(p.index + 1, true);
                        }, p.delay2);
                    }
                } else {
                    if (p.index < u) {
                        h = s - r;
                        i.find("ul").css({
                            top: h + "px"
                        });
                    } else {
                        h = s + r;
                        i.find("ul").css({
                            top: h + "px"
                        });
                    }
                    e = false;
                    f = setTimeout(function () {
                        m(u, q);
                    }, 20);
                }
            };
        var c = function () {
                var q = [];
                for (var r = 1; r < n; r++) {
                    q.push("<li><a href='");
                    q.push(p.pics[r].href);
                    q.push("' target='_blank'><img src='");
                    q.push(p.pics[r].src);
                    q.push("' width='");
                    q.push(p.width);
                    q.push("' height='");
                    q.push(p.height);
                    q.push("' /></a></li>");
                }
                i.find("ul").append(q.join(""));
                g = setTimeout(function () {
                    l(p.index + 1, true);
                }, p.delay2);
                if (p.type) {
                    j();
                }
            };
    };
})(jQuery);

function getRandomDomain(a) {
    var b, a = String(a);
    switch (a.match(/(\d)$/)[1] % 5) {
    case 0:
        b = 10;
        break;
    case 1:
        b = 11;
        break;
    case 2:
        b = 12;
        break;
    case 3:
        b = 13;
        break;
    case 4:
        b = 14;
        break;
    default:
        b = 10;
    }
    return "http://img{0}.xxx.com/".replace("{0}", b);
}
function ResumeError() {
    return true;
}
window.onerror = ResumeError;
if ($.browser.isIE6) {
    try {
        document.execCommand("BackgroundImageCache", false, true);
    } catch (err) {}
};
var calluri = "WebService.asmx/MarkEx?callback=?";
var loguri = "WebService.asmx";
callback1 = function (data) {;
};
log = function (type1, type2, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) {
    var data = '';
    for (i = 2; i < arguments.length; i++) {
        data = data + arguments[i] + '|||';
    }
    var url = loguri.replace(/\$type1\$/, escape(type1));
    url = url.replace(/\$type2\$/, escape(type2));
    url = url.replace(/\$data\$/, escape(data));
    $.getJSON(url, callback1);
};
mark = function (sku, type) {
    $.getJSON(calluri, {
        sku: sku,
        type: type
    }, callback1);
    log(1, type, sku);
};

function search(id, val) {
	

	if ($("#Text1").val() != "" || $("#Text2").val() != "") {
		url = "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val();
		window.location = url;
		return false;
    }
    
    var selKey = document.getElementById(id).value.replace(/#/g, "%23").replace(/\+/g, "%2b"),
        id, url = "/Search.aspx?{0}keyword=",
        str;
selKey=selKey.replace(/(^\s*)|(\s*$)/g, "");
    switch (parseInt(val)) {
    case 0:
        id = null;
        break;
    default:
        id = document.body.id
    }
    if (id == "music" || id == "movie" || id == "education") {
        str = "mvd=" + id + "&"
    } else if (id == "book") {
        str = "book=y&"
    } else {
        str = ""
    }
    url = url.replace("{0}", str);
if (selKey=="")
{
alert("请输入关键字");
return false;
}

    window.location = url + escape(selKey)
}
function login() {
    location.href = "/login/login.aspx?ReturnUrl=" + EnEight(location.href);
    return false;
}
function regist() {
    location.href = "/login/register.aspx";
    return false;
}
function setWebBILinkCount(sType) {
    try {
        if (sType.length > 0) {
            var js = document.createElement('script');
            js.type = 'text/javascript';
            js.src = 'WebService.asmx?key=' + sType;
            document.getElementsByTagName('head')[0].appendChild(js);
        }
    } catch (e) {}
};


document.domain = "localhost:810";
var initScrollY = 0;
var proIDs = new Array();

function compare() {
	if ($("#compare").get(0) == null) {
		$("body").append("<div id=\"compare\" class=\"compare\"><div class=\"mt\"><h5>线路比较</h5><div class=\"extra\" onclick=\"clearCompare()\"></div></div><div class=\"comPro\"><ul class=\"mc\" id=\"comProlist\"></ul><div class=\"mb\"><input type=\"button\" value=\"对比所选线路\" class=\"btn\" id=\"compareImg\" onclick=\"openCompare()\"></div></div></div>");
        $("#compare").css({
            position: "absolute",
            top: "100px",
            right: "0px"
        });
        isCoo()
    }
    
    if ($.browser.msie) {
        var defaultY = document.documentElement.scrollTop;
        var perceH = 0.3 * (defaultY - initScrollY);
        if (perceH > 0) {
            perceH = Math.ceil(perceH)
        } else {
            perceH = Math.floor(perceH)
        }
        $("#compare").get(0).style.top = parseInt($("#compare").get(0).style.top) + perceH + "px";
        initScrollY = initScrollY + perceH;
        setTimeout("compare()", 50);
        
    } else {
        window.onscroll = function () {
            $("#compare").get(0).style.top = parseInt($("#compare").get(0).style.top) + "px";
            $("#compare").get(0).style.position = "fixed";
        }
    }
}
function clearCompare() {
    $("#comProlist").empty();
    $("#compare").hide();
    createCookie("compare", "");
    proIDs = new Array()
}
function addToCompare(checkobj, checkid, checkProName) {
    $("#compare").show();
    $(".comPro").show();
    var proIDsTemp = proIDs.join(".");
    if (proIDsTemp.indexOf(checkid) == -1) {
        if (proIDs.length < 2) {
            proIDs.push(checkid);
            $("#comProlist").append("<li id='check_" + checkid + "'><a title='删除' class='close' onclick='reduceCompare(" + checkid + ")'></a>" + checkProName + "</li>");
            writeCompare(checkid, checkProName)
        } else {
            alert("对不起，最多可以选择两条线路进行对比！")
        }
    } else {
        alert("对不起，您已经选择此线路！");
        return
    }
}
function reduceCompare(checkid) {
    $("#check_" + checkid).remove();
    $.each(proIDs, function (i, n) {
        if (checkid == n) {
            proIDs.splice(i, 1)
        }
    });
    var coo = readCookie("compare");
    var idindexstart = coo.indexOf(checkid);
    var idindexend = coo.indexOf("|||", idindexstart) + 3;
    var delStr = coo.substring(idindexstart, idindexend);
    var innerStr = coo.replace(delStr, "");
    createCookie("compare", innerStr);
    if (proIDs.length == 0) {
        $(".comPro").hide()
    }
}
function openCompare() {
    switch (proIDs.length) {
    case 1:
        alert("对不起，最少选择两条线路进行对比！");
        break;
    case 2:
        window.open("/pcompare.aspx?s1=" + proIDs[0] + "&s2=" + proIDs[1]);
        break;
    case 3:
        //window.open("/pcompare.aspx?s1=" + proIDs[0] + "&s2=" + proIDs[1] + "&s3=" + proIDs[2]);
        //break;
        alert("只能选择两条线路进行对比！");
        return
    default:
        alert("只能选择两条线路进行对比！");
        return
    }
}
function writeCompare(checkid, checkProName) {
    var compareList = readCookie("compare");
    if (compareList == null) {
        compareList = ""
    }
    compareList += checkid + "||" + escape(checkProName) + "|||";
    createCookie("compare", compareList)
}
function isCoo() {
	
    var coo = readCookie("compare");
    if (coo) {
    	var cootemp = coo.split("|||");
    	
        var compareListTemp = "";
        for (var i = 0; i < cootemp.length - 1; i++) {
            compareListTemp += "<li id='check_" + cootemp[i].split("||")[0] + "'><a title='删除' class='close' onclick='reduceCompare(" + cootemp[i].split("||")[0] + ")'></a>" + unescape(cootemp[i].split("||")[1]) + "</li>";
            proIDs.push(cootemp[i].split("||")[0]);
            	
        }
        $("#comProlist").html(compareListTemp);
        $("#compare").show();
        $(".comPro").show()
        	
    }
}
function createCookie(name, value, days, Tdom) {
    var Tdom = (Tdom) ? Tdom : "/";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString()
    } else {
        var expires = ""
    }
    document.cookie = name + "=" + value + expires + "; path=" + Tdom    
}
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length)
        }
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length)
        }
    }
    return null
}

function asyncScript(A, B) {
    if (typeof A == "function") {
        var B = A,
            A = null
    }
    if (A) {
        if (typeof A != "string") {
            return
        }
        var x = document.createElement('script');
        x.type = 'text/javascript';
        x.async = true;
        x.src = A;
        var s = document.getElementsByTagName('head')[0];
        s.appendChild(x);
        if (B) {
            if (typeof B != "function") {
                return
            }
            if (! /*@cc_on!@*/ 0) {
                x.onload = function () {
                    B()
                }
            } else {
                x.onreadystatechange = function () {
                    if (x.readyState == 'loaded' || x.readyState == 'complete') {
                        B()
                    }
                }
            }
        }
    } else {
        if (!B) {
            return
        }
        setTimeout(function () {
            B()
        }, 0)
    }
};
var pannel = {};
pannel.gotop = {
    settings: {
        element: null,
        target: "#header"
    },
    init: function (option, callback) {
        var _this = this;
        $.extend(this.settings, option || {});
        if (callback) {
            callback()
        }
        var fn = function () {
                if (screen.width >= 1280) {
                    _this.render(0)
                } else {
                    _this.render(1)
                }
            };
        fn();
        $(window).bind("scroll", function () {
            fn()
        }).bind("resize", function () {
            fn()
        })
    },
    render: function (a) {
        var ele = $(this.settings.element),
            target = $(this.settings.target);
        var left, width, top = $.browser.scroll().top + $.browser.client().height - ele.height() - 10 + "px";
        switch (a) {
        case 0:
            width = (target.width() >= 1200) ? 1200 : 980;
            left = target.offset().left + width + "px";
            if ($.browser.isIE6) {
                ele.css({
                    "left": left,
                    "top": top
                })
            } else {
                ele.css({
                    "left": left,
                    "bottom": "0"
                })
            }
            break;
        case 1:
            if ($.browser.isIE6) {
                ele.css({
                    "right": "0",
                    "top": top
                })
            } else {
                ele.css({
                    "right": "0",
                    "bottom": "0"
                })
            }
            break
        }
    }
};

function getRandomObj(A, R) {
    var x = 0;
    for (var i = 0; i < A.length; i++) {
        x += R[i] || 1;
        if (!R[i]) R.push(1)
    }
    var y = Math.ceil(Math.random() * x),
        z = [],
        m = [];
    for (var i = 1; i < x + 1; i++) {
        z.push(i)
    }
    for (var i = 0; i < A.length; i++) {
        m[i] = z.slice(0, R[i]);
        z.splice(0, R[i])
    }
    for (var i = 0; i < m.length; i++) {
        for (var j = 0; j < m[i].length; j++) {
            if (y == m[i][j]) {
                return A[i]
            }
        }
    }
}
function setRandomAds(A, R, O, flag) {
    var obj = getRandomObj(A, R),
        ele = document.getElementById(O),
        img;
    if (!obj) {
        return
    }
    if (flag && screen.width >= 1280) {
        obj.width = obj.width2;
        obj.url = obj.url2
    } else {
        obj.width = obj.width;
        obj.url = obj.url
    }
    img = "<a href='" + obj.link + "' target='_blank'><img width='" + obj.width + "' height='" + obj.height + "' alt='" + obj.alt + "' app='image:poster' src='" + obj.url + "' /></a>";
    if (ele) ele.innerHTML = img
    //alert(img)
};
/*
	@Last-Modified:2011/04/11
*/

/*8进制加密*/
function EnEight(txt){
    var monyer = new Array();var i,s;
    for(i=0;i<txt.length;i++)
        monyer+="\\"+txt.charCodeAt(i).toString(8); 
    return monyer;
}
/*8进制解密*/
function DeEight(txt){
    var monyer = new Array();var i;
    var s=txt.split("\\");
    for(i=1;i<s.length;i++)
        monyer+=String.fromCharCode(parseInt(s[i],8));
    return monyer;
}