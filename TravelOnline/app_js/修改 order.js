if ($.cookie("orderuid") == null) top.location = "/app/main";
var pagehash = window.location.hash.replace("#", "").toLowerCase();
var state = { action: pagehash, url: "#" + pagehash };

jQuery(document).ready(function () {
    App.init();
    App.blockUI({ boxed: true });
    history.replaceState(state);

    window.addEventListener('popstate', function (e) {
        if (e.state) {
            //$(".sub_view").hide();
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
            alert("新页面");
            LoadPage(pagehash);
        }
    });
});

function LoadPage(flag) {
    alert(flag);
    $(".sub_view").hide();
    if ($("#" + flag + "_view").length > 0) {
        $("#" + flag + "_view").show();
        App.unblockUI();
    }
    else {
        var url = "";
        App.blockUI({ boxed: true });
        switch (flag) {
            default:
                url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                $("#main_view").append("<div class=\"sub_view\" id=\"first_view\">第一步</div>");
                break;
            case "second":
                url = "../../WeChat/AjaxService.aspx?action=OrderSecondStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                $("#main_view").append("<div class=\"sub_view\" id=\"second_view\">第二步</div>");
                break;
        }

        alert(url);
        App.unblockUI();
//        var url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random(); ;
//        $.get(url, function (data) {
//            App.unblockUI();
//            $("#main_view").append(data);
//        });
    }
}

$('#ordernow').click(function () {
    state = { action: "second", url: "#second" };
    history.pushState(state, "aaa", "#second");
    //$("#titlename").html("第二页面");
    LoadPage("second");
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