$("#PlanDate").jdTab({ auto: false, event: "click" });
    $("#detail").jdTab({
        type: "dynamic",
        auto: false,
        event: "click",
        source: "data"
    }, function (url, object, n) {
        if (!url) {
            return
        }
        if (url != "d-all") { 
            getDetailComment(url)
        }
    })

    function getDetailComment(url) {
        var DivName = "#" + url;
        var DivCount = "#" + url + "-ct";
        $(DivName).html($(DivCount).html());
}

$(function () {
    $(".piczoom").jqueryzoom({
        xzoom: PicRoomWidth,
        yzoom: 300,
        offset: 10,
        position: "right",
        preload: 1,
        lens: 1
    });
    $("#spec-list").picMarquee({
        deriction: "left",
        width: 290,
        height: 56,
        step: 2,
        speed: 4,
        delay: 10,
        control: true,
        _front: "#spec-right",
        _back: "#spec-left"
    });
    $("#spec-list img").bind("mouseover", function () {
        $(this).css({
            "border": "2px solid #50A423",
            "padding": "1px"
        });
    }).bind("mouseout", function () {
        $(this).css({
            "border": "1px solid #ccc",
            "padding": "2px"
        });
    }).bind("click", function () {
        var src = $(this).attr("src");
        $("#spec-n1 img").eq(0).attr({
            src: src.replace("\/S_", "\/M_"),
            jqimg: src.replace("\/S_", "\/")
        });
        //alert(src.replace("\/s_", "\/m_"))
    });
});
(function ($) {
    $.fn.jqueryzoom = function (options) {
        var settings = {
            xzoom: 200,
            yzoom: 200,
            offset: 10,
            position: "right",
            lens: 1,
            preload: 1
        };
        if (options) {
            $.extend(settings, options);
        };
        var noalt = '';
        $(this).hover(function () {
            var imageLeft = $(this).offset().left;
            var imageTop = $(this).offset().top;
            var imageWidth = $(this).children('img').get(0).offsetWidth;
            var imageHeight = $(this).children('img').get(0).offsetHeight;
            noalt = $(this).children("img").attr("alt");
            var bigimage = $(this).children("img").attr("jqimg");
            $(this).children("img").attr("alt", '');
            if ($("div.zoomdiv").get().length == 0) {
                $(this).after("<div class='zoomdiv'><img class='bigimg' src='" + bigimage + "'/></div>");
                $(this).append("<div class='jqZoomPup'>&nbsp;</div>");
            };
            if (settings.position == "right") {
                if (imageLeft + imageWidth + settings.offset + settings.xzoom > screen.width) {
                    leftpos = imageLeft - settings.offset - settings.xzoom;
                } else {
                    leftpos = imageLeft + imageWidth + settings.offset;
                }
            } else {
                leftpos = imageLeft - settings.xzoom - settings.offset;
                if (leftpos < 0) {
                    leftpos = imageLeft + imageWidth + settings.offset;
                }
            };
            $("div.zoomdiv").css({
                top: imageTop,
                left: leftpos
            });
            $("div.zoomdiv").width(settings.xzoom);
            $("div.zoomdiv").height(settings.yzoom);
            $("div.zoomdiv").show();
            if (!settings.lens) {
                $(this).css('cursor', 'crosshair');
            };
            $(document.body).mousemove(function (e) {
                mouse = new MouseEvent(e);
                var bigwidth = $(".bigimg").get(0).offsetWidth;
                var bigheight = $(".bigimg").get(0).offsetHeight;
                var scaley = 'x';
                var scalex = 'y';
                if (isNaN(scalex) | isNaN(scaley)) {
                    var scalex = (bigwidth / imageWidth);
                    var scaley = (bigheight / imageHeight);
                    $("div.jqZoomPup").width((settings.xzoom) / (scalex * 1));
                    $("div.jqZoomPup").height((settings.yzoom) / (scaley * 1));
                    if (settings.lens) {
                        $("div.jqZoomPup").css('visibility', 'visible');
                    }
                };
                xpos = mouse.x - $("div.jqZoomPup").width() / 2 - imageLeft;
                ypos = mouse.y - $("div.jqZoomPup").height() / 2 - imageTop;
                if (settings.lens) {
                    xpos = (mouse.x - $("div.jqZoomPup").width() / 2 < imageLeft) ? 0 : (mouse.x + $("div.jqZoomPup").width() / 2 > imageWidth + imageLeft) ? (imageWidth - $("div.jqZoomPup").width() - 2) : xpos;
                    ypos = (mouse.y - $("div.jqZoomPup").height() / 2 < imageTop) ? 0 : (mouse.y + $("div.jqZoomPup").height() / 2 > imageHeight + imageTop) ? (imageHeight - $("div.jqZoomPup").height() - 2) : ypos;
                };
                if (settings.lens) {
                    $("div.jqZoomPup").css({
                        top: ypos,
                        left: xpos
                    });
                };
                scrolly = ypos;
                $("div.zoomdiv").get(0).scrollTop = scrolly * scaley;
                scrollx = xpos;
                $("div.zoomdiv").get(0).scrollLeft = (scrollx) * scalex;
            });
        }, function () {
            $(this).children("img").attr("alt", noalt);
            $(document.body).unbind("mousemove");
            if (settings.lens) {
                $("div.jqZoomPup").remove();
            };
            $("div.zoomdiv").remove();
        });
        count = 0;
        if (settings.preload) {
            $('body').append("<div style='display:none;' class='jqPreload" + count + "'>TravelOnline</div>");
            $(this).each(function () {
                var imagetopreload = $(this).children("img").attr("jqimg");
                var content = jQuery('div.jqPreload' + count + '').html();
                jQuery('div.jqPreload' + count + '').html(content + '<img src=\"' + imagetopreload + '\">');
            });
        }
    }
})(jQuery);

function MouseEvent(e) {
    this.x = e.pageX;
    this.y = e.pageY;
}


var setAmount = {
    min: 1,
    max: 9,
    reg: function (x) {
        return new RegExp("^[1-9]\\d*$").test(x);
    },
    amount: function (obj, mode) {
        var x = $(obj).val();
        if (this.reg(x)) {
            if (mode) {
                x++;
            } else {
                x--;
            }
        } else {
            alert("请输入正确的数量！");
            $(obj).val(1);
            $(obj).focus();
        }
        return x;
    },
    reduce: function (obj) {
        var x = this.amount(obj, false);
        if (x >= this.min) {
            $(obj).val(x);
        } else {
            alert("商品数量最少为" + this.min);
            $(obj).val(1);
            $(obj).focus();
        }
    },
    add: function (obj) {
        var x = this.amount(obj, true);
        if (x <= this.max) {
            $(obj).val(x);
        } else {
            alert("商品数量最多为" + this.max);
            $(obj).val(9);
            $(obj).focus();
        }
    },
    modify: function (obj) {
        var x = $(obj).val();
        if (x < this.min || x > this.max || !this.reg(x)) {
            alert("请输入正确的数量！");
            $(obj).val(1);
            $(obj).focus();
        }
    }
}

function BuyUrl(wid) {
    var pcounts = $("#pamount").val();
    var patrn = /^[0-9]{1,2}$/;
    if (!patrn.exec(pcounts)) {
        pcounts = 1;
    }
    else {
        if (pcounts <= 0)
            pcounts = 1;
        if (pcounts >= 9)
            pcounts = 9;
    }
    GoToOrder();
    //$("#InitCartUrl").attr("href", "http://127.0.0.1/purchase/InitCart.aspx?pid=" + wid + "&pcount=" + pcounts + "&ptype=1");
}

function GetRecentList()
{
	var pro_url = location.href;
	var pro_name = $("title").html();
	var canAdd = true;
	var hisProduct = readCookie("hisProduct");
	var len = 0;
	if (hisProduct) {
	    hisProduct = eval('(' + hisProduct + ')');
	    len = hisProduct.length;
	}

	$(hisProduct).each(function (i) {
	    if (this.proname == pro_name) {
	        canAdd = false;
	        return;
	    }
	})
	if (pro_name.length<2) canAdd = false;
	if (canAdd) {
	    var temp = "[";
	    var startNum = 0;
	    if (len > 8) { startNum = 1; }
	    for (var m = startNum; m < len; m++) {
	        temp = temp + "{\"url\":\"" + hisProduct[m].url + "\",\"proname\":\"" + hisProduct[m].proname + "\"},";
	    }
	    temp = temp + "{\"url\":\"" + pro_url + "\",\"proname\":\"" + pro_name + "\"}]";
	    createCookie("hisProduct", temp, 90);
	}

	var newtemp = eval('(' + readCookie("hisProduct") + ')');
	var newtemp_ = "";
	i = 1;
	for (var n = newtemp.length - 1; n > -1; n--) {
	    newtemp_ = newtemp_ + "<li><span>" + i + "</span><div class=p-name><a target='_blank' href='" + newtemp[n].url + "'>" + newtemp[n].proname + "<\/a></div></li>";
	    i++;
	}
	$("#recent ul").html(newtemp_);
}

function LoginNow() {
    $.jdThickBox({
        type: "iframe",
        title: "您还没有登录",
        source: "/login/loginnow.aspx",
        width: 420,
        height: 360,
        _title: "thicktitler",
        _close: "thickcloser",
        _con: "thickconr"
    })
}

mlazyload({
    defObj: "#recent",
    defHeight: 50,
    fn: function() {
        GetRecentList();
    }
});
/*getrecent*/
/*
var jdRecent = {
    element: $("#recent ul"),
    jsurl: "/lishiset.aspx?callback=jdRecent.setData&id=",
    cookiename: "_recent",
    list: $.cookie("_recent"),
    url: location.href,
    init: function() {
        var _matchStr = this.url.match(/\/(\d*).html/);
        var _urls = this.url
        var _lname =$("title").html()//attr("title")
        var _id = (_matchStr != null && _matchStr[0].indexOf("html") != -1) ? _matchStr[1] : "";
        if (!this.list || this.list == null || this.list == "") {
            if (_id == "") {
                return this.getData(0);
            } else {
                this.list = _id;
            }
        } else {
            if (_id == "" || this.list.indexOf(_id) != -1) {
                this.list = this.list;
            } else {
                if (this.list.split(".").length >= 10) {
                    this.list = this.list.replace(/.\d+$/, "");
                }
                this.list = _id + "." + this.list;
            }
        }
        //alert($(document)[0].title);
        //alert(_lname);
        //alert(_urls);
        createCookie("_recent", this.list, 90);
        $.cookie(this.cookiename, this.list, {
            expires: 7,
            path: "/",
            domain: "127.0.0.1",
            secure: false
        });
        this.getData(this.list);
    },
    clear: function() {
        $.cookie(this.cookiename, "", {
            expires: 7,
            path: "/",
            domain: "127.0.0.1",
            secure: false
        });
    },
    getData: function(list) {
        if (list == 0) {
            this.element.html("<li><div class='norecode'>暂无记录!</div></li>");
            return;
        }
        var rec = list.split(".");
        for (i in rec) {
            if (i == 0) {
                this.element.empty()
            };
            $.getJSONP(this.jsurl + rec[i], this.setData);
        }
    },
    setData: function(result) {
        this.element.append("<li><div class='p-img'><a href='" + result.url + "'><img src='" + result.img + "' /></a></div><div class='p-name'><a href='" + result.url + "'>" + decodeURIComponent(result.name) + "</a></div></li>");
    }
};
$("#clearRec").click(function() {
    jdRecent.clear();
    jdRecent.getData(0);
});
mlazyload({
    defObj: "#recent",
    defHeight: 50,
    fn: function() {
        if (jdRecent.element.length == 1) {
            jdRecent.init();
        }
    }
});
*/
