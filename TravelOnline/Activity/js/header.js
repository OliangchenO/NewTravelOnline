$(function () {
    $('#more_search_date').calendar({ maxDate: '#more_search_dateend', btnBar: false });
    $('#more_search_dateend').calendar({ minDate: '#more_search_date', btnBar: false });

    $('#topbar_dropmenu').mouseover(function () {
        $("#topbar_usercenter i").attr({ "class": "icon-chevron-up" });
        $("#topbar_dropmenu_a").attr({ "class": "mod_dropmenu_hd_on" });
        $(".mod_dropmenu_pop").show();
    }).mouseout(function () {
        $("#topbar_usercenter i").attr({ "class": "icon-chevron-down" });
        $("#topbar_dropmenu_a").attr({ "class": "mod_dropmenu_hd" });
        $(".mod_dropmenu_pop").hide();
    }).mousemove(function (e) {
        $("#topbar_usercenter i").attr({ "class": "icon-chevron-up" });
        $("#topbar_dropmenu_a").attr({ "class": "mod_dropmenu_hd_on" });
        $(".mod_dropmenu_pop").show();
    })
});

function addToFavorite() {
    var a = "http://www.scyts.com/";
    var b = "上海中国青年旅行社在线商城";
    try {
        if (document.all) {
            window.external.AddFavorite(a, b)
        } else if (window.sidebar) {
            window.sidebar.addPanel(b, a, "")
        } else {
            alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。")
        }
    }
    catch (e) {
        alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。")
    }

}

function login() {
    location.href = "/login/login.aspx?returnurl=" + EnEight(location.href);
    return false;
}
function regist() {
    location.href = "/login/register.aspx";
    return false;
}

$('#search_key').focus(function () {
    if ($(this).val() == "请输入线路编号、名称或目的地") $(this).val("");
}).blur(function () {
    if ($(this).val() == "") $(this).val("请输入线路编号、名称或目的地");
})
$('#more_search_key').focus(function () {
    if ($(this).val() == "请输入线路编号、名称或目的地") $(this).val("");
}).blur(function () {
    if ($(this).val() == "") $(this).val("请输入线路编号、名称或目的地");
})

function checkform() {
    if ($("#search_key").val() == "请输入线路编号、名称或目的地") return false;
}

function hide_more_search() {
    $("#more_search_toolbars").hide();
}

function more_search() {
    if ($("#more_search_date").val() == "" && $("#more_search_dateend").val() == "") {
        if ($("#more_search_key").val() == "请输入线路编号、名称或目的地") {
            alert("请输入关键字");
            return false;
        }
    }
    
    var arrChk = "";
    var url = "";
    if ($("#more_search_key").val() != "请输入线路编号、名称或目的地") {
        url = "/search.html?key=" + escape($("#more_search_key").val());
    }
    else {
        url = "/search.html?key=";
    }
    if ($("#more_search_date").val() != "") url += "&date=" + $("#more_search_date").val();
    if ($("#more_search_dateend").val() != "") url += "&dateend=" + $("#more_search_dateend").val();
    if (Number($("#more_search_price1").val()) > 0) url += "&p1=" + $("#more_search_price1").val();
    if (Number($("#more_search_price2").val()) > 0) url += "&p2=" + $("#more_search_price2").val();
    if ($("input[name='type']:checked").length > 0) {
        $("input[name$='type']:checked").each(function () { arrChk += this.value + ","; });
        arrChk = arrChk.substr(0, arrChk.length - 1);
        url += "&type=" + arrChk;
        arrChk = "";
    }
    if ($("input[name='day']:checked").length > 0) {
        $("input[name$='day']:checked").each(function () { arrChk += this.value + ","; });
        arrChk = arrChk.substr(0, arrChk.length - 1);
        url += "&day=" + arrChk;
        arrChk = "";
    }
    $("#more_search_toolbars").hide();
    $("#more_search_now").attr("href", url);
}

/*8进制加密*/
function EnEight(txt) {
    var monyer = new Array(); var i, s;
    for (i = 0; i < txt.length; i++)
        monyer += "\\" + txt.charCodeAt(i).toString(8);
    return monyer;
}
/*8进制解密*/
function DeEight(txt) {
    var monyer = new Array(); var i;
    var s = txt.split("\\");
    for (i = 1; i < s.length; i++)
        monyer += String.fromCharCode(parseInt(s[i], 8));
    return monyer;
}

$('#more_search_btn').bind('click', function () {
    $("#more_search_toolbars").toggle();
    $("#more_search_toolbars").css("left", $(this).offset().left - 370);
    $("#more_search_toolbars").css("top", $(this).offset().top + $(this).height() + 8);
});