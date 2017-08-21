$(function () {
    $('.psel').change(function () {
        var parent = $(this).parents("li");
        var parentid = "#" + $(this).parents("li").attr("id");
        $(".priceli").each(function () {
            var pid = "#" + $(this).attr("id");
            if ($(this).attr("tps") == "Rebate" || $(this).attr("tps") == "Coupon") {
                if (pid != parentid) {
                    $(pid + " .psel").val("0");
                    $(pid + " .sumprice").html("0");
                    $(pid + " div:last").attr({ "class": "fnpic" });
                }
            }
        });

        var sums = 0;
        sums = Number($(this).val()) * Number($(parentid + " .sellprice").html());
        $(parentid + " .sumprice").html(sums);
        if ($(this).val() == "0") {
            $(parentid + " div:last").attr({ "class": "fnpic" });
        }
        else {
            $(parentid + " div:last").attr({ "class": "fpic" });
        }
    });
});

window.onscroll = function () {
    var top = "260";
    var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    if (scrollTop > top) {
        $("#pricebar").attr({ "class": "package_ptfix" });
    } else {
        $("#pricebar").removeAttr("class");
    }
}

$(document).ready(function () {
    onload_handler();
});

function onload_handler()    
{
    $(".priceli").each(function () {
        var parentid = "#" + $(this).attr("id");
        var sums = 0;
        sums = Number($(parentid + " .psel").val()) * Number($(parentid + " .sellprice").html());
        $(parentid + " .sumprice").html(sums);
        if ($(parentid + " .psel").val() == "0") {
            $(parentid + " div:last").attr({ "class": "fnpic" });
        }
        else {
            $(parentid + " div:last").attr({ "class": "fpic" });
        }
    });
}

