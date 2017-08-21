var state = { action: $("#viewflag").val(), title: $("#titlename").html(), url: "" };
var scroll_top = 0;
var detail_top = 0;
var loadflag = 0;
//alert(window.location.hash);
jQuery(document).ready(function () {
    if ($.cookie("view_flag") == null) {
        $.cookie("view_flag", "2", { expires: 90, path: '/WeChat' });
        $("#viewflag2").addClass("cur");
        $("#s_navbar").val("2");
    }
    else {
        $("#s_navbar").val($.cookie("view_flag"));
        if ($.cookie("view_flag") == "2") {
            $("#viewflag2").addClass("cur");
        }
        else {
            $("#viewflag1").addClass("cur");
        }
    };
    if ($.cookie("lineid") != null) {
        if ($('#s_lineid').val() == $.cookie("lineid")) {
            $('#s_planid').val($.cookie("planid"));
            $('#s_plandate').val($.cookie("plandate"));
        }
    }
    App.init();
    //App.initTouchspin();
    App.blockUI({ boxed: true });
    //App.smartFloat('.nav-tabs');
    calendarNums = 0;

    switch ($("#viewflag").val()) {
        case "page":
            $("#page_view").show();
            App.unblockUI();
            break;
        case "list":
            state = { action: "list", title: $("#titlename").html(), url: $("#s_url").val() };
            $("#linelist_view").show();
            LoadList();
            LoadDestination();
            break;
        case "line":
            state = { action: "line", title: $("#titlename").html(), url: $("#s_url").val() };
            $("#line_view").show();
            LoadLineInfo();
            PicSlide();
            break;
        case "search":
            state = { action: "search", title: $("#titlename").html(), url: $("#s_url").val() };
            $("#search_view").show();
            LoadList();
            break;
        default:
            App.unblockUI();
            break;
    }

    history.replaceState(state);

    $('.navbar-toggle').click(function () {
        $("#s_navbar").val($(this).val());
        if ($.cookie("view_flag") == $(this).val()) return false;
        $.cookie("view_flag", $(this).val(), { expires: 30, path: '/WeChat' });
        $('.navbar-toggle').removeClass("cur");
        $(this).addClass("cur");
        if ($('#linelist .product-item').hasClass('product-list')) {
            $('#linelist .product-item').removeClass("product-list");
        } else {
            $('#linelist .product-item').addClass("product-list");
        }
    });

    $('.pre-footer .px').click(function () {
        var filter = $(this).attr("tag")
        if ($(this).attr("tag") == "2") {
            if ($("#s_filter").val() == "2") filter = "3";
        }
        if (filter == $("#s_filter").val()) return;
        $('.pre-footer .px').removeClass("cur");
        $(this).addClass("cur");
        $("#s_filter").val(filter);
        if ($("#s_filter").val() == "3") {
            $('#sort-days').removeClass("fa-arrow-up");
            $('#sort-days').addClass("fa-arrow-down");
        }
        if ($("#s_filter").val() == "2") {
            $('#sort-days').removeClass("fa-arrow-down");
            $('#sort-days').addClass("fa-arrow-up");
        }
        $("#s_pages").val("1");
        App.blockUI({ boxed: true });
        LoadList();
        scroll(0, 0);
    });

    $('#serch-next').click(function () {
        $("#serch-next").html("<img src='/images/ajax-loader.gif'> 努力加载中...");
        LoadList()
    });

    $('#s-next').click(function () {
        $("#s-next").html("<img src='/images/ajax-loader.gif'> 努力加载中...");
        LoadList()
    });

    $(".cart-block").hover(function () {
        $(".cart-content").show();
    }, function () {
        $(".cart-content").hide();
    });

    $(".quantity-down").html("<i class='fa fa-minus'></i>");
    $(".quantity-up").html("<i class='fa fa-plus'></i>");
});

function PicSlide() {
    var bullets = document.getElementById('position').getElementsByTagName('li');

    var banner = Swipe(document.getElementById('mySwipe'), {
        auto: 2000,
        continuous: true,
        disableScroll: false,
        callback: function (pos) {
            var i = bullets.length;
            while (i--) {
                bullets[i].className = ' ';
            }
            bullets[pos].className = 'cur';
        }
    });
}

window.onpopstate = function (e) {
    document.title = $("#s_titlename").val();
    App.unblockUI();
    loadflag = 0;
    switch (e.state.action) {
        default:
            document.title = $("#s_titlename").val();
            //$(".bx").css("display", "none");
            //$(".bx").removeAttr("style")
            areahide();
            $("#page_view").show();
            scroll(scroll_top, 0);
            if ($("#search_hide").val() != "") {
                $("#page_view").hide();
            }
            break;
        case "list":
            $("#viewflag").val("list");
            $("#titlename").html(e.state.title);
            document.title = e.state.title;
            areahide();
            $("#linelist_view").show();
            if ($("#s_lineclass").val() == "") {
                $("#s_lineclass").val(e.state.url);
            }
            if ($("#linelist").html() == "") {
                $("#s_pages").val("1");
                App.blockUI({ boxed: true });
                LoadList();
                LoadDestination();
            }
            scroll(scroll_top, 0);
            break;
        case "line":
            $("#viewflag").val("line");
            document.title = e.state.title;
            $("#titlename").html("线路详情");
            areahide();
            $("#line_view").show();
            if ($("#s_lineid").val() == "") {
                var lineid = e.state.url.split("?");
                $("#s_lineid").val(lineid[1]);
            }
            if ($("#tab_1_1").html() == "") {
                LoadLineInfo();
                PicSlide();
            }
            break;
        case "plan":
            $("#viewflag").val("plan");
            document.title = e.state.title;
            $("#titlename").html("出发日期");
            areahide();
            if ($("#plandate").html() == "") LoadCalender();
            $("#PlanDate_view").show();
            loadflag == 1;
            break;
        case "order":
            $("#viewflag").val("order");
            document.title = e.state.title;
            $("#titlename").html("立即预订");
            areahide();
            $("#Order_view").show();
            loadflag = 1;
            break;
        case "page":
            $("#titlename").html(e.state.title);
            document.title = e.state.title;
            areahide();
            $("#page_view").show();
            break;
        case "search":
            $("#titlename").html(e.state.title);
            document.title = e.state.title;
            areahide();
            if ($("#searchlist").html() == "") {
                $("#s_pages").val("1");
                $("#search_hide").val($.cookie("search"));
                $("#search").val($.cookie("search"));
                App.blockUI({ boxed: true });
                LoadList();
            }
            $("#search_view").show();
            scroll(scroll_top, 0);
            break;
    }

}

function DoiCheck() {
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-grey',
        radioClass: 'iradio_square-grey',
        increaseArea: '20%'
    });

    $('input').on('ifChecked', function (event) {
        //RadioSet($(this).val());
    });
}

function areahide() {
    $("#page_view").hide()
    $("#line_view").hide();
    $("#linelist_view").hide();
    $("#PlanDate_view").hide();
    $("#Order_view").hide();
    $("#search_view").hide();
}

$('#linelist a').live("click", function () {
    App.blockUI({ boxed: true });
    document.title = $(this).attr("linename");
    $("#titlename").html("线路详情");
    var userId = $("#UserId").val();
    state = { action: "line", title: $(this).attr("linename"), url: "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId };
    window.location.href = "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId;
    //history.pushState(state, $(this).attr("linename"), "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId);
    scroll_top = document.body.scrollTop;
    $("#linelist_view").hide();
    if ($("#s_lineid").val() == $(this).attr("lineid")) {
        App.unblockUI();
        //$("#PlanDate_view").hide();
        areahide();
        $("#line_view").show();
    }
    else {
        $("#line_view").show();
        $("#plandate").html("");
        $("#s_lineid").val($(this).attr("lineid"));
        calendarNums = 0;
        LoadLineInfo();
    }
});

$('.b-color .name').each(function () {
    $(this).click(function () {
    });
})

$('#searchlist a').live("click", function () {
    App.blockUI({ boxed: true });
    document.title = $(this).attr("linename");
    $("#titlename").html("线路详情");
    var userId = $("#UserId").val();
    state = { action: "line", title: $(this).attr("linename"), url: "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId };
    window.location.href = "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId;
    //history.pushState(state, $(this).attr("linename"), "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId);
    scroll_top = document.body.scrollTop;
    $("#search_view").hide();
    if ($("#s_lineid").val() == $(this).attr("lineid")) {
        App.unblockUI();
        //$("#PlanDate_view").hide();
        areahide();
        $("#line_view").show();
    }
    else {
        $("#line_view").show();
        $("#plandate").html("");
        $("#s_lineid").val($(this).attr("lineid"));
        calendarNums = 0;
        LoadLineInfo();
    }
});

$('.tiles a').live("click", function () {
    App.blockUI({ boxed: true });
    $("#titlename").html($(this).attr("tname"));
    document.title = $(this).attr("tname");
    var userId = $("#UserId").val();
    if (userId != "") {
        state = { action: "list", title: $(this).attr("tname"), url: "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId };
        history.pushState(state, $(this).attr("tname"), "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") + "&userId=" + userId);
    } else {
        state = { action: "list", title: $(this).attr("tname"), url: "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag") };
        history.pushState(state, $(this).attr("tname"), "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag"));
    }
    window.location.href = "/WeChat/linelist.aspx?linetype=" + $(this).attr("tag");
});

$('.scroller a').live("click", function () {
    $(".cart-content").hide();
    $("#linkname").html($(this).html());
    $("#linelist").html("");
    $("#s_dest").val($(this).attr("tag"));
    App.blockUI({ boxed: true });
    $("#s_pages").val("1");
    LoadList();
});

$(".showdate,.BeginOrder").live("click", function () {
    $.cookie("lineflag", $('#s_lineflag').val(), { expires: 7, path: '/WeChat' });
    switch ($("#s_lineflag").val()) {
        case "2":
            //            var val = $('input:radio[name="iCheck"]:checked').val();
            //            if (val == null) {
            //                showmessage("请选择您要预订的舱型");
            //                return false;
            //            }
            //            else {
            //                alert(val);
            //            }
            showmessage("请电话咨询<br>4006777666");
            return false;
            break;
        case "3":
            document.title = "立即预订";
            $("#titlename").html("立即预订");
            state = { action: "order", title: "立即预订", url: "/WeChat/linelist.aspx?linetype=line?" + $("#s_lineid").val() + "#order" };
            history.pushState(state, "立即预订", "/WeChat/linelist.aspx?linetype=line?" + $("#s_lineid").val() + "#order");
            $("#PlanDate_view").hide();
            $("#line_view").hide();
            $("#Order_view").show();
            $("#viewflag").val("order");
            scroll(0, 0);
            break;
        default:
            document.title = "出发日期";
            $("#titlename").html("出发日期");
            state = { action: "plan", title: "出发日期", url: "/WeChat/linelist.aspx?linetype=line?" + $("#s_lineid").val() + "#plan" };
            history.pushState(state, "出发日期", "/WeChat/linelist.aspx?linetype=line?" + $("#s_lineid").val() + "#plan");
            $("#viewflag").val("plan");
            if (calendarNums > 0) {
                $("#line_view").hide();
                $("#PlanDate_view").show();
                scroll(0, 0);
            }
            else {
                LoadCalender();
            }
            break;
    }

});

function LoadList() {
    var search = $("#search_hide").val();
    if ($("#search_hide").val() != "") {
    }
    var url = "../../WeChat/AjaxService.aspx?action=LoadLineList&navbar=" + $("#s_navbar").val() + "&linetype=" + $("#s_linetype").val() + "&lineclassname=" + $("#s_lineclassname").val() + "&lineclass=" + $("#s_lineclass").val() + "&filter=" + $("#s_filter").val() + "&dest=" + $("#s_dest").val() + "&pages=" + $("#s_pages").val() + "&search=" + escape(search);
    
    $.getJSON(url, function (date) {
        if (date.pagecount == 0) {
            if ($("#search_hide").val() != "") {
                $("#searchlist").append(date.content);
                $("#s-next").hide();
            }
            else {
                $("#linelist").append(date.content);
                $("#serch-next").hide();
            }
        }
        else {
            if ($("#search_hide").val() != "") {
                if ($("#s_pages").val() == "1") $("#searchlist").html("");
                $("#s-next").html("点击继续加载...");
                $("#s-next").show();
                if (date.pages > date.pagecount) $("#s-next").hide();
                $("#s_pages").val(date.pages);
                $("#searchlist").append(date.content);
            }
            else {
                if ($("#s_pages").val() == "1") $("#linelist").html("");
                $("#serch-next").html("点击继续加载...");
                $("#serch-next").show();
                if (date.pages > date.pagecount) $("#serch-next").hide();
                $("#s_pages").val(date.pages);
                $("#linelist").append(date.content);
            }
        }
        App.unblockUI();
    })
}

function LoadCalender() {
    var url = "../../WeChat/AjaxService.aspx?action=LoadLineDateJson&lineid=" + $("#s_lineid").val();
    calendarNums = 0;
    App.blockUI({ boxed: true });
    $.get(url, function (data) {
        App.unblockUI();
        $("#LineDateJson").html(data);
        $("#line_view").hide();
        $("#PlanDate_view").show();
        scroll(0, 0);
        if (calendarNums > 0) {
            $("#plandate").showRenderCalendar({ events: eval(json), showNum: calendarNums });

            if ($.cookie("lineid") != null) {
                if ($('#s_lineid').val() == $.cookie("lineid")) {
                    $("#plandate .hasEvent").each(function () {
                        if ($(this).attr("date") == $.cookie("plandate")) $(this).addClass("curdate");
                    });
                }
            }
            $("#plandate .hasEvent").bind("click", function () {
                $("#plandate .hasEvent").removeClass("curdate");
                $(this).addClass("curdate");
                $("#s_plandate").val($(this).attr("date"));
                $("#s_planid").val($(this).attr("tag"));
            });
        }
    });
}

function LoadDestination() {
    var url = "../../WeChat/AjaxService.aspx?action=LoadDestinationList&lineclass=" + $("#s_lineclass").val() + "&linetype=" + $("#s_linetype").val();
    $.getJSON(url, function (date) {
        $(".scroller").html(date.content);
        $("#linkname").html(date.area);
    })
}

function LoadLineInfo() {
    var url = "../../WeChat/AjaxService.aspx?action=LoadLineInfo&lineid=" + $("#s_lineid").val();
    $.get(url, function (data) {
        App.unblockUI();
        $("#linedetail").html(data);
        $("#line_view").show();
        PicSlide();
        $(".nav-tabs").pin({ containerSelector: ".portlet-body", fromtop: 45 });
        if (loadflag == 1) {
            $("#line_view").hide();
        }
        scroll(0, 0);
        if ($("#s_lineflag").val() == "2") DoiCheck();
    });
}

$('#SelectPlanDate').click(function () {
    window.event.cancelBubble = true;
    if ($("#s_plandate").val() == "") {
        showmessage("请选择出发日期");
        return false;
    }
    document.title = "立即预订";
    $("#titlename").html("立即预订");
    state = { action: "order", title: "立即预订", url: "line?" + $("#s_lineid").val() + "#order" };
    history.pushState(state, "立即预订", "line?" + $("#s_lineid").val() + "#order");
    $("#PlanDate_view").hide();
    $("#Order_view").show();
    $("#viewflag").val("order");
    scroll(0, 0);
});

$('#share').click(function () {
    showmessage("run");
    WeixinJSBridge.invoke("onMenuShareAppMessage", data, callback)
})

$('#ordernow').click(function () {
    window.event.cancelBubble = true;
    if ($("#s_plandate").val() == "" && $.cookie("lineflag") != "3") {
        showmessage("请选择出发日期");
        return false;
    }
    var url = "../../WeChat/AjaxService.aspx?action=TempOrder&lineid=" + $('#s_lineid').val() + "&planid=" + $('#s_planid').val() + "&begindate=" + $('#s_plandate').val() + "&adults=" + Number($('#adult_num').val()) + "&childs=" + Number($('#child_num').val()) + "&r=" + Math.random();
    $.getJSON(url, function (date) {
        if (date.success == 0) {
            $.cookie("orderuid", date.orderuid, { expires: 30, path: '/WeChat' });
            $.cookie("lineid", $('#s_lineid').val(), { expires: 7, path: '/WeChat' });
            $.cookie("planid", $('#s_planid').val(), { expires: 7, path: '/WeChat' });
            $.cookie("plandate", $('#s_plandate').val(), { expires: 7, path: '/WeChat' });
            top.location = "/WeChat/order.aspx#first";
        }
        else {
            showmessage(date.error);
            return false;
        }
    })
});

function showmessage(msg) {
    App.blockUI({
        message: msg,
        boxed: true,
        textOnly: true
    });
    window.setTimeout(function () {
        App.unblockUI();
    }, 2000);
}

$(document).bind("click", function () {
    if ($("#PlanDate_view").css("display") != "none") App.unblockUI();
    if ($("#Order_view").css("display") != "none") App.unblockUI();
});

$(document).live('touchstart', function () {
    if ($("#PlanDate_view").css("display") != "none") App.unblockUI();
    if ($("#Order_view").css("display") != "none") App.unblockUI();
});

function check_null() {
    if ($("#search").val() == "" || $("#search").val() == "搜索关键字或目的地名称") {
        showmessage("请输入搜索关键字");
        return false;
    }
    $.cookie("search", $("#search").val(), { expires: 90, path: '/WeChat' });
}