(function () {
    var a = {
        getMenusList: function () {
            var b = {};
            jQuery("#my360buy").find("dl[tag]").each(function () {
                var e = jQuery(this);
                var d = e.find("dt[tag]").html(),
                    c = e.find("a[tag]");
                b[d] = [];
                c.each(function () {
                    var g = jQuery(this),
                        f = g.attr("tag") + "|" + g.html() + "|" + g.attr("href");
                    b[d].push(f);
                });
            });
            return b;
        },
        templates: {
            0: '<div id="menu-userdefined"class="form hide"><iframe scrolling="no"frameborder="0"marginheight="0"marginwidth="0"class="hack-iframe"></iframe><div class="widget"><div class="corner corner-left"><b class="b1"></b><b class="b2"></b></div><div class="close"id="jdmenus-close"></div><div class="i-widget"><div class="widget-t clearfix"><h3>设置常用菜单</h3><div class="extra clearfix"><input type="checkbox"id="jdmenus-state"{1}/><label for="jdmenus-state">默认收起常用菜单</label></div></div><div class="widget-c"><div class="prompt">勾选您经常使用的菜单，最多可选5个。<a href="javascript:void(0)"id="jdmenus-clear">清空已选项</a></div><div class="clearfix">{0}</div><div class="btns clearfix"><input type="button"value="设置"class="button"id="jdmenus-submit"/><a href="javascript:void(0)"id="jdmenus-cancer">取消</a></div></div></div></div><span class="clr"></span></div>',
            1: '<dl id="{2}" class="{3}"><dt>{0}<b></b></dt><dd>{1}</dd></dl>',
            2: '<div class="item"><input type="checkbox" value="{0}" name="jdmenus" id="jdmenus-{0}" /><label for="jdmenus-{0}">{1}</label></div>',
            3: '<div class="item"><a href="{0}">{1}</a></div>'
        },
        getCheckedMenusData: function (c) {
            var b = this;
            jQuery.getJSON("http://home.360buy.com/left/show.action?callback=?", function (d) {
                b.checkedMenusData = [d.retractFlag, d.code];
                if (c) {
                    c(d);
                }
            });
        },
        checkedMenusData: [],
        setDefaultCheckbox: function () {
            var d = this,
                b, c = document.getElementsByName("jdmenus");
            d.getCheckedMenusData(function (g) {
                b = g.code.split(",");
                for (var f = 0; f < b.length; f++) {
                    for (var e = 0; e < c.length; e++) {
                        if (c[e].value == b[f]) {
                            c[e].checked = "checked";
                            c[e].parentNode.className = "item checked";
                        }
                    }
                }
            });
        },
        formatString: function (b, e) {
            if (!typeof e == "object" && !e.length) {
                return;
            }
            var f, d;
            for (var c = 0; c < e.length; c++) {
                d = new RegExp("\\{" + c + "\\}", "g");
                f = f ? f.replace(d, e[c]) : b.replace(d, e[c]);
            }
            return f;
        },
        formatSettings: function (j) {
            var d = [],
                g = this.getMenusList(),
                f = (this.checkedMenusData[0] == 1) ? "checked='checked'" : "";
            for (i in g) {
                var k = [],
                    c;
                for (var b = 0; b < g[i].length; b++) {
                    c = g[i][b].split("|");
                    k.push(this.formatString(this.templates[2], [c[0], c[1]]));
                }
                k = k.join("");
                d.push(this.formatString(this.templates[1], [i, k, ""]));
            }
            d = d.join("");
            d = this.formatString(this.templates[0], [d, f]);
            jQuery(document.body).append(d);
            var j = j ? j : window.event,
                h = j.srcElement ? j.srcElement : j.target;
            this.setPosi(h);
            $(window).bind("resize", function () {
                _this.setPosi(h);
            });
            jQuery("#menu-userdefined").show().find("iframe").css({
                height: jQuery("#menu-userdefined").height()
            });
        },
        setPosi: function (b) {
            jQuery("#menu-userdefined").css({
                top: jQuery(b).offset().top - 8 + "px",
                left: jQuery(b).offset().left + 50 + "px"
            });
        },
        formatMenu: function () {
            var b = this;
            b.getCheckedMenusData(function (g) {
                var j = b.getMenusList();
                var f = [],
                    e, d = g.code,
                    h = (g.retractFlag == 0) ? "" : "close";
                for (i in j) {
                    for (var c = 0; c < j[i].length; c++) {
                        e = j[i][c].split("|");
                        if (d.indexOf(e[0]) != -1) {
                            f.push(b.formatString(b.templates[3], [e[2], e[1]]));
                        }
                    }
                }
                f = f.join("");
                f = b.formatString(b.templates[1], ["常用设置", f, "manage-userdefined", h]);
                if (jQuery("#manage-userdefined").length > 0) {
                    jQuery("#manage-userdefined").remove();
                }
                if (d) {
                    jQuery("#my360buy .mc").prepend(f);
                }
            });
        },
        checkedAmount: null,
        setCheckboxChange: function () {
            var d = this,
                c = document.getElementsByName("jdmenus");
            for (var b = 0; b < c.length; b++) {
                c[b].onclick = function () {
                    d.checkedAmount = (d.checkedAmount != null) ? d.checkedAmount : (d.checkedMenusData[1] == "") ? 0 : d.checkedMenusData[1].split(",").length;
                    if (this.checked) {
                        d.checkedAmount++;
                        this.parentNode.className = "item checked";
                    } else {
                        d.checkedAmount--;
                        this.parentNode.className = "item";
                    }
                    if (d.checkedAmount > 5) {
                        alert("对不起，您最多可选择5项");
                        this.parentNode.className = "item";
                        d.checkedAmount = 5;
                        return false;
                    }
                };
            }
        },
        clear: function () {
            a.checkedAmount = 0;
            var c = document.getElementsByName("jdmenus");
            for (var b = 0; b < c.length; b++) {
                c[b].checked = false;
                c[b].parentNode.className = "item";
            }
        },
        submit: function () {
            var d = document.getElementsByName("jdmenus"),
                c = [],
                f;
            for (var b = 0; b < d.length; b++) {
                if (d[b].checked) {
                    c.push(d[b].value);
                }
            }
            var e = document.getElementById("jdmenus-state");
            if (e.checked) {
                e.value = 1;
            } else {
                e.value = 0;
            }
            jQuery.getJSON("http://home.360buy.com/left/setMenu.action?code=" + c.join(",") + "&retractFlag=" + e.value + "&callback=?", function (g) {
                if (g.code == "success") {
                    a.formatMenu();
                }
            });
            a.cancer();
        },
        cancer: function () {
            jQuery("#menu-userdefined").hide();
        },
        setMenuState: function () {
            var b = jQuery(document.body).attr("myjd"),
                c = readCookie("myjd");
            if (jQuery("#" + b).length > 0) {
                jQuery("#" + b).addClass("curr");
            }
            if (c) {
                c = c.split(".");
                jQuery.each(c, function (e) {
                    var f = c[e],
                        d = f.substr(0, 1);
                    _b = f.substr(1, 2);
                    if (_b == 1) {
                        return;
                    }
                    jQuery("#my360buy").find("dl[tag^=" + d + "]").addClass("close");
                });
            }
        },
        setMenuEvent: function () {
            var c = jQuery(this).parent(),
                b = [];
            if (c.hasClass("close")) {
                c.removeClass("close");
            } else {
                c.addClass("close");
            }
            jQuery("#my360buy").find("dl[tag]").each(function () {
                var f = jQuery(this),
                    d = f.hasClass("close"),
                    e = (d) ? (f.attr("tag") + 0) : (f.attr("tag") + 1);
                b.push(e);
            });
            b = b.join(".");
            jQuery.cookie("myjd", b, {
                expires: 360,
                path: "/",
                domain: "360buy.com",
                secure: false
            });
        },
        init: function () {
            var b = this;
            this.formatMenu();
            this.setMenuState();
            jQuery("#my360buy dt").livequery("click", this.setMenuEvent);
            jQuery("#jdmenus-setting").html("常用设置").bind("click", function (c) {
                if (jQuery("#menu-userdefined").length) {
                    jQuery("#menu-userdefined").show();
                    return;
                }
                b.formatSettings(c);
                b.setDefaultCheckbox();
                b.setCheckboxChange();
                jQuery("#jdmenus-clear").bind("click", b.clear);
                jQuery("#jdmenus-cancer").bind("click", b.cancer);
                jQuery("#jdmenus-close").bind("click", b.cancer);
                jQuery("#jdmenus-submit").bind("click", b.submit);
            });
        }
    };
    a.getUserData = function () {
        jQuery.getJSON("http://jd2008.360buy.com/JdHome/AjaxService.aspx?callback=?&action=ShowCouponCount", function (b) {
            if (b.Coupon) {
                jQuery("#_MYJD_ticket").append("<span style='color:red;font-family:arial;'>(" + b.Coupon + ")</span>");
            }
        });
    };
    a.init();
    a.getUserData();
})();
(function () {
    if ($(document.body).attr("myjd") != "_MYJD_ordercenter") {
        return
    };
    var v = {
        "overflow": "visible"
    };
    $(".left").css(v);
    $("#my360buy").css(v);
    $("#my360buy .mc").css(v);
    $("#_MYJD_favorite").css({
        "position": "relative",
        "z-index": "10"
    }).append("<img src='http://misc.360buyimg.com/jd2008/skin/df/i/tip_favorite1.gif' style='position:absolute;top:-9px;left:70px;' onclick='$(this).fadeOut()' />");
})();
