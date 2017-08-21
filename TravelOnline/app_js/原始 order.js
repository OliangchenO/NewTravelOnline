var state = { action: "first", url: "#first" };
if ($.cookie("orderuid") == null) top.location = "/app/main";
var pagehash = window.location.hash;

jQuery(document).ready(function () {
    App.init();
    App.blockUI({ boxed: true });
    history.replaceState(state);

    window.addEventListener('popstate', function (e) {
        if (e.state) {
            $(".sub_view").hide();
            //alert("是返回页面");
            LoadPage(e.state.action);
//            switch (e.state.action) {
//                default:
//                    //if ($("#first_view").length > 0) alert("已经有第一页面");
//                    //$("#titlename").html("第一页面");
//                    LoadPage();
//                    break;
//                case "second":
//                    if ($("#Order_view").length > 0) alert("已经有第二页面");
//                    //$("#titlename").html("第二页面");
//                    break;
//            }
        }
        else {
            //alert("新页面");
            LoadPage(pagehash..replace("#", ""););
        }
    });
});

function LoadPage(flag) {
    if ($("#" + flag).length > 0) {
        $("#" + flag).show();
    }
    else {
        var url = "";
        App.blockUI({ boxed: true });
        switch (flag) {
            default:
                url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                break;
            case "second":
                url = "../../WeChat/AjaxService.aspx?action=OrderSecondStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random(); ;
                break;
        }
        alert(url);
//        var url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random(); ;
//        $.get(url, function (data) {
//            App.unblockUI();
//            $("#main_view").append(data);
//        });
    }
}

//function LoadFirstStep() {
//    if ($("#first_view").length > 0) {
//        $("#first_view").show();
//    }
//    else {
//        App.blockUI({ boxed: true });
//        var url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random(); ;
//        $.get(url, function (data) {
//            App.unblockUI();
//            $("#main_view").append(data);
//        });
//    }
//}

//function LoadSecondStep() {
//    if ($("#second_view").length > 0) {
//        $("#second_view").show();
//    }
//    else {
//        App.blockUI({ boxed: true });
//        var url = "../../WeChat/AjaxService.aspx?action=OrderSecondStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random(); ;
//        $.get(url, function (data) {
//            App.unblockUI();
//            $("#main_view").append(data);
//        });
//    }
//}

$('#ordernow').click(function () {
    state = { action: "second", url: "#second" };
    history.pushState(state, "aaa", "#second");
    //$("#titlename").html("第二页面");
});

function showmessage(msg) {
    App.blockUI({ message: msg,
        boxed: true,
        textOnly: true
    });
    window.setTimeout(function () {
        App.unblockUI();
    }, 2000);
}

//$(document).bind("click", function () {
//    if ($("#PlanDate_view").css("display") != "none") App.unblockUI();
//    if ($("#Order_view").css("display") != "none") App.unblockUI();
//});

//$(document).live('touchstart', function () {
//    if ($("#PlanDate_view").css("display") != "none") App.unblockUI();
//    if ($("#Order_view").css("display") != "none") App.unblockUI();
//});