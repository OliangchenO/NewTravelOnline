
function ScrollTo(href) {
    var pos = $("#"+href).offset().top;
    $("html,body").animate({ scrollTop: pos }, 700);
}

function LoginNow(flag) {
    $.jdThickBox({
        type: "iframe",
        title: "您还没有登录",
        source: "/login/loginnow.aspx?flag=" + flag,
        width: 420,
        height: 460,
        _title: "thicktitler",
        _close: "thickcloser",
        _con: "thickconr"
    })
}

//pin
$.fn.smartFloat = function () {
    var position = function (element) {
        var top = element.position().top; //当前元素对象element距离浏览器上边缘的距离 
        var pos = element.css("position"); //当前元素距离页面document顶部的距离 
        $(window).scroll(function () { //侦听滚动时 
            var scrolls = $(this).scrollTop();
            if (scrolls > top) { //如果滚动到页面超出了当前元素element的相对页面顶部的高度 
                if (window.XMLHttpRequest) { //如果不是ie6 
                    element.css({ //设置css 
                        position: "fixed", //固定定位,即不再跟随滚动 
                        top: 0 //距离页面顶部为0 
                    }).addClass("shadow"); //加上阴影样式.shadow 
                } else { //如果是ie6 
                    element.css({
                        top: scrolls  //与页面顶部距离 
                    });
                }
                $("#nav_order").show();
            } else {
                $("#nav_order").hide();
                element.css({ //如果当前元素element未滚动到浏览器上边缘，则使用默认样式 
                    position: pos,
                    top: top
                }).removeClass("shadow"); //移除阴影样式.shadow 
            }
        });
    };
    return $(this).each(function () {
        position($(this));
    });
};

(function ($) {
    $.fn.extend({
        showRenderCalendar: function (s) {
            var currentCalenderPanel = $(this);
            s = $.extend({}, $.fn.params, s);
            Date.dayNames = ['日', '一', '二', '三', '四', '五', '六'];
            var eventsDict = {};
            var arr = defaultStartDate.split("-");
            var endarr = defaultEndDate.split("-");
            var defaultDate = new Date(arr[0], arr[1] - 1, arr[2]);
            var PlanEndDate = new Date(endarr[0], endarr[1] - 1, endarr[2]);
            var today = new Date();
            if (defaultDate < today) {
                defaultDate = new Date();
            }

            var month = s.month >= 0 ? s.month : defaultDate.getMonth();
            var year = s.year ? s.year : defaultDate.getFullYear();
            var day = s.day ? s.day : defaultDate.getDate();
            var lastday = 1;
            var currentDate = new Date(year, month, day);
            if (s.events != null) {

                for (var i = 0; i < s.events.length; i++) {
                    eventsDict[s.events[i].date] = { "planid": s.events[i].planid, "date": s.events[i].date, "price": s.events[i].price, "content": s.events[i].content, "route": s.events[i].route };
                }
            }

            var myranderCallback = function ($td, thisDate, month, year) {
                if (thisDate.getMonth() != month) {
                    $td.html("&nbsp;");
                }
                var planDay = eventsDict[thisDate.asString("yyyy-mm-dd")];
                if (planDay) {
                    if (thisDate > today) {
                        if (thisDate.getMonth() == month) {
                            $td.addClass(s.hoverClass);
                            var currentDay = $td.text();
                            if (planDay.content == "已满") {
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + planDay.price + "</span><br/><span class='route_5'>已满</span>"; //title='" + planDay.planid + "'
                            }
                            else {
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + planDay.price + "</span><br/><span class='route_2'>" + planDay.content + "</span>"; //title='" + planDay.planid + "'
                            }
                            if (planDay.route != "-1") {

                                if (planDay.route == "0") {
                                    hrefString += "<br><div class=pa><a class=pa onclick='showroute()' target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'><span class='route_3'>行程打印</span></a></div>";
                                }
                                else {
                                    hrefString += "<br><div class=pa><a onclick='showroute()' target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'><span class='route_4'>行程打印</span></a></div>";
                                }
                            }
                            $td.attr({ "tag": planDay.planid });
                            $td.attr({ "date": thisDate.asString("yyyy-mm-dd") });
                            $td.attr({ "price": planDay.price });
                            $td.html(hrefString);
                        }
                        else {
                            $td.html("&nbsp;");
                        }
                    }
                }
                if (thisDate <= today) {
                    $td.addClass(s.outClass);
                }
            }

            $(currentCalenderPanel).empty();

            for (var i = 0; i < s.showNum; i++) {
                var currentTitleDate = new Date(year, month, day).addMonths(i);
                var lastTitleDate = new Date(year, month, lastday).addMonths(i+1);
                //var currentTitleDate = new Date(year, month, day);

                $(this).append("<div class='calendarPanel'></div>");
                var currentPanel = $(this).find(".calendarPanel:eq(" + i + ")");
                $(currentPanel).append("<div class='monthTitle'></div>");
                $(currentPanel).find(".monthTitle").append("<table class='monthTable'><tr></tr></table>");
                var currentMonthTitle = $(currentPanel).find(".monthTitle > .monthTable tr");
                if (currentTitleDate > defaultDate) {
                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
                        $(currentMonthTitle).append("<td id='prevMonth' class='prevMonth'><a href='javascript:void(0);' title='上一个月'><img src=\"/images/mbi_003.gif\">上月</a></td>");
                    }
                }
                else {
                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
                        $(currentMonthTitle).append("<td class='prevMonth'><img src=\"/images/mbi_004.gif\">上月</td>");
                    }
                }

                $(currentMonthTitle).append("<td class='monthTitle'>" + currentTitleDate.asString("yyyy年mm月") + "</td>");

                if (lastTitleDate < PlanEndDate) {
                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
                        $(currentMonthTitle).append("<td id='nextMonth' class='nextMonth'><a href='javascript:void(0);' title='下一个月'>下月<img src=\"/images/mbi_005.gif\"></a></td>");
                    }
                }
                else {
                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
                        $(currentMonthTitle).append("<td class='nextMonth'>下月<img src=\"/images/mbi_006.gif\"></td>");
                    }
                }
                //alert(currentTitleDate.asString("yyyy-mm-dd") + " / " + lastTitleDate.asString("yyyy-mm-dd") + " / " + PlanEndDate.asString("yyyy-mm-dd"))

                $(currentPanel).append("<div id='showCalendarPanel" + i + "' class='showCalendarPanel' ></div>");
                $("#showCalendarPanel" + i).renderCalendar({ month: currentTitleDate.getMonth(), year: currentTitleDate.getFullYear(), renderCallback: myranderCallback });
            }

            var prevButton = s.showPrevMonthPanel ? $(s.showPrevMonthPanel) : $(this).find("#prevMonth");
            var nextButton = s.showNextMonthPanel ? $(s.showNextMonthPanel) : $(this).find("#nextMonth");
            $(prevButton).bind('click', function () {
                var prevMonthDate = currentDate.addMonths(-ShowMonthNum);
                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: prevMonthDate.getFullYear(), month: prevMonthDate.getMonth() });
                initTips();
            });

            $(nextButton).bind('click', function () {
                var nextMonthDate = currentDate.addMonths(ShowMonthNum);
                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: nextMonthDate.getFullYear(), month: nextMonthDate.getMonth() });
                initTips();
            });
        }
    });

    $.fn.params = {
        showNum: 1,
        showMonthTitle: true,
        showNextMonthButton: true,
        showPrevMonthButton: true,
        showNextMonthPanel: undefined,
        showPrevMonthPanel: undefined,
        month: undefined,
        year: undefined,
        day: undefined,
        hoverClass: 'hasEvent',
        outClass: 'outdays',
        events: null
    };
})(jQuery);


var initTips = function () {
    $("#plandate .hasEvent").bind("click", function () {
        $("#select_plandate").html($(this).attr("date") + "  " + $(this).attr("price") + "元起");
        $("#select_plandate").attr("tag", $(this).attr("tag"));
        $("#select_plandate").attr("date", $(this).attr("date"));
        Order();
    });

    $(".CruisesDate1").click(function () {
        $("#select_plandate").html($(this).attr("date") + "  " + $(this).attr("price") + "元起");
        $("#select_plandate").attr("tag", $(this).attr("tag"));
        $("#select_plandate").attr("date", $(this).attr("date"));
        Order();
    });
};

var CloseToolTip = function () {
    //$("#plantooltip").hide();
};

function PlanDropDownListBind() {
    var newtemp = eval(json);
    var newtemp_ = ""; //"<option value=\"\">请选择出发日期</option>";
    var its = 0;
    var today = new Date();


    for (var n = 0; n < newtemp.length; n++) {
        var arr = newtemp[n].date.split("-");
        var defaultDate = new Date(arr[0], arr[1] - 1, arr[2]);
        if (defaultDate > today) {
            if (newtemp[n].content != "已满") {
                newtemp_ += "<a tag=" + newtemp[n].planid + " date=" + newtemp[n].date + ">" + newtemp[n].date + "  " + newtemp[n].price + "元起</a>"
                if (its == 0) {
                    $("#select_plandate").html(newtemp[n].date + "  " + newtemp[n].price + "元起");
                    $("#select_plandate").attr("tag", newtemp[n].planid);
                    $("#select_plandate").attr("date", newtemp[n].date);
                    its = 1;
                }
            }
        }

    }
    $("#plandate_droplist").html(newtemp_);
}

(function ($) {
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


/*jdThickbox*/
(function ($) {
    $.fn.jdPosition = function (option) {
        var s = $.extend({
            mode: null
        },
        option || {});
        switch (s.mode) {
            default:
            case "center":
                var ow = $(this).outerWidth(),
            oh = $(this).outerHeight();
                var w = $.browser.isMinW(ow),
            h = $.browser.isMinH(oh);
                $(this).css({
                    left: $.browser.scroll().left + Math.max(($.browser.client().width - ow) / 2, 0) + "px",
                    top: (!$.browser.isIE6) ? (h ? $.browser.scroll().top : ($.browser.scroll().top + Math.max(($.browser.client().height - oh) / 2, 0) + "px")) : (($.browser.scroll().top <= $.browser.client().bodyHeight - oh) ? ($.browser.scroll().top + Math.max(($.browser.client().height - oh) / 2, 0) + "px") : ($.browser.client().bodyHeight - oh) + "px")
                });
                break;
            case "auto":
                break;
            case "fixed":
                break
        }
    }
})(jQuery); 

(function ($) {
    $.fn.jdThickBox = function (option, callback) {
        if (typeof option == "function") {
            callback = option;
            option = {}
        };
        var s = $.extend({
            type: "text",
            source: null,
            width: null,
            height: null,
            title: null,
            _frame: "",
            _div: "",
            _box: "",
            _con: "",
            _loading: "thickloading",
            close: false,
            _close: "",
            _fastClose: false,
            _close_val: "×",
            _titleOn: true,
            _title: "",
            _autoReposi: false,
            _countdown: false
        },
        option || {});
        var object = (typeof this != "function") ? $(this) : null;
        var timer;
        var close = function () {
            clearInterval(timer);
            $(".thickframe").add(".thickdiv").hide();
            $(".thickbox").empty().remove();
            if (s._autoReposi) {
                $(window).unbind("resize.jdThickBox").unbind("scroll.jdThickBox")
            }
        };
        if (s.close) {
            close();
            return false
        };
        var reg = function (str) {
            if (str != "") {
                return str.match(/\w+/)
            } else {
                return ""
            }
        };
        var init = function (element) {
            if ($(".thickframe").length == 0 || $(".thickdiv").length == 0) {
                $("<iframe class='thickframe' id='" + reg(s._frame) + "' marginwidth='0' marginheight='0' frameborder='0' scrolling='no'></iframe>").appendTo($(document.body));
                $("<div class='thickdiv' id='" + reg(s._div) + "'></div>").appendTo($(document.body))
            } else {
                $(".thickframe").add(".thickdiv").show()
            };
            $("<div class='thickbox' id='" + reg(s._box) + "'></div>").appendTo($(document.body));
            if (s._titleOn) initTitle(element);
            $("<div class='thickcon' id='" + reg(s._con) + "' style='width:" + s.width + "px;height:" + s.height + "px;'></div>").appendTo($(".thickbox"));
            if (s._countdown) initCountdown();
            $(".thickcon").addClass(s._loading);
            reposi();
            initClose();
            inputData(element);
            if (s._autoReposi) {
                $(window).bind("resize.jdThickBox", reposi).bind("scroll.jdThickBox", reposi)
            };
            if (s._fastClose) {
                $(document.body).bind("click.jdThickBox",
                function (e) {
                    e = e ? e : window.event;
                    var tag = e.srcElement ? e.srcElement : e.target;
                    if (tag.className == "thickdiv") {
                        $(this).unbind("click.jdThickBox");
                        close()
                    }
                })
            }
        };
        var initCountdown = function () {
            var x = s._countdown;
            $("<div class='thickcountdown' style='width:" + s.width + "'><span id='jd-countdown'>" + x + "</span>秒后自动关闭</div>").appendTo($(".thickbox"));
            timer = setInterval(function () {
                x--;
                $("#jd-countdown").html(x);
                if (x == 0) {
                    x = s._countdown;
                    close()
                }
            },
            1000)
        };
        var initTitle = function (element) {
            s.title = (s.title == null && element) ? element.attr("title") : s.title;
            $("<div class='thicktitle' id='" + reg(s._title) + "' style='width:" + s.width + "'><span>" + s.title + "</span></div>").appendTo($(".thickbox"))
        };
        var initClose = function () {
            if (s._close != null) {
                $("<a href='#' class='thickclose' id='" + reg(s._close) + "'>" + s._close_val + "</a>").appendTo($(".thickbox"));
                $(".thickclose").one("click",
                function () {
                    close();
                    return false
                })
            }
        };
        var inputData = function (element) {
            s.source = (s.source == null) ? element.attr("href") : s.source;
            switch (s.type) {
                default:
                case "text":
                    $(".thickcon").html(s.source);
                    $(".thickcon").removeClass(s._loading);
                    if (callback) {
                        callback()
                    };
                    break;
                case "html":
                    $(s.source).clone().appendTo($(".thickcon")).show();
                    $(".thickcon").removeClass(s._loading);
                    if (callback) {
                        callback()
                    };
                    break;
                case "image":
                    s._index = (s._index == null) ? object.index(element) : s._index;
                    $(".thickcon").append("<img src='" + s.source + "' width='" + s.width + "' height='" + s.height + "'>");
                    s.source = null;
                    $(".thickcon").removeClass(s._loading);
                    if (callback) {
                        callback()
                    };
                    break;
                case "ajax":
                case "json":
                    if (callback) {
                        callback(s.source, $(".thickcon"),
                    function () {
                        $(".thickcon").removeClass(s._loading)
                    })
                    };
                    break;
                case "iframe":
                    $("<iframe src='" + s.source + "' marginwidth='0' marginheight='0' frameborder='0' scrolling='no' style='width:" + s.width + "px;height:" + s.height + "px;border:0;'></iframe>").appendTo($(".thickcon"));
                    $(".thickcon").removeClass(s._loading);
                    if (callback) {
                        callback()
                    };
                    break
            }
        };
        var reposi = function () {
            var w1 = $(".thickcon").outerWidth(),
            h1 = (s._titleOn ? $(".thicktitle").outerHeight() : 0) + $(".thickcon").outerHeight();
            $(".thickbox").css({
                width: w1 + "px",
                height: h1 + "px"
            });
            $(".thickbox").jdPosition({
                mode: "center"
            });
            if ($.browser.isIE6) {
                var ow = $(".thickbox").outerWidth(),
                oh = $(".thickbox").outerHeight();
                var w2 = $.browser.isMinW(ow),
                h2 = $.browser.isMinH(oh);
                $(".thickframe").add(".thickdiv").css({
                    width: w2 ? ow : "100%",
                    height: Math.max($.browser.client().height, $.browser.client().bodyHeight) + "px"
                })
            }
        };
        if (object != null) {
            object.click(function () {
                init($(this));
                return false
            })
        } else {
            init()
        }
    };
    $.jdThickBox = $.fn.jdThickBox
})(jQuery);

function jdThickBoxclose() {
    $(".thickclose").trigger("click");
};