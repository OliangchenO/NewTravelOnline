window.onscroll = function () {
    var top = "260";
    var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    if (scrollTop > top) {
        $("#pricebar").attr({ "class": "package_ptfix" });
    } else {
        $("#pricebar").removeAttr("class");
    }
};

$(document).ready(function () {
    onload_handler();
});

function onload_handler() {
    $(".CheckBoxList").each(function () {
        var id = $(this).attr("tgs");
        $("#Visit_Span" + id).html($("#V_NM" + id).val());
    });
}

function SelectGuest(id, flag) {
    if (flag == "0") {
        if ($("#h" + id).is(":visible") == false) {
            $(".htr").hide();
            $("#h" + id).toggle();
        }
        else {
            $("#h" + id).hide()
        }
    }
    else {
        $(".htr").hide();
        $("#h" + id).toggle();
    }


    //            var s = $("#show" + id).html();
    //            if (s.length < 5) {
    //                var url = "/CruisesOrder/AjaxService.aspx?action=LoadVisitGuest&Id=" + id + "&orderid=" + $('#TempOrderId').val() + "&VNO=@@" + $("#V_NO" + id).val();
    //                window.open(url);
    //                $("#show" + id).html("<img src=\"/images/Please_wait.gif\">");
    //                $.get(url, function (result) {
    //                    $("#show" + id).html(result);
    //                });
    //            }
}

function chkall(obj, id) {
    var arrChk = "";
    var arrName = ""
    var arrGuestid = "";
    var nums = Number($("#V_Nums" + id).val());
    var i = 0;
    var HaveSelect = "@";
    var dayid = $(obj).closest(".VisitList").attr("tids");
    if (obj.checked) {
        //取全部已选择的
        $(".Day" + dayid + " .ChkIt").each(function () {
            if ($(this).prop("checked") == true) {
                HaveSelect += this.value + "@";
            }
        });
        $("input[name$='CheckBox" + id + "']").each(function () {
            if ($(this).prop("checked") == true) {
                i = i + 1;
            }
        });
        $("input[name$='CheckBox" + id + "']").each(function () {
            if (HaveSelect.indexOf("@" + this.value + "@") == -1) {
                if (i < nums) {
                    this.checked = true;
                }
                i = i + 1;
            }
            if ($(this).prop("checked") == true) {
                arrGuestid += $(this).attr("gid") + "@";
                arrChk += this.value + "@";
                arrName += $(this).attr("tgs") + "，";
            }
        });
        arrName = arrName.substr(0, arrName.length - 1);
        $("#Visit_Span" + id).html(arrName);
        $("#V_NO" + id).val(arrChk);
        $("#V_NM" + id).val(arrName);
        $("#V_GID" + id).val(arrGuestid);
    } else {
        $("input[name$='CheckBox" + id + "']").each(function () { this.checked = false; });
        $("#Visit_Span" + id).html("");
        $("#V_NO" + id).val("");
        $("#V_NM" + id).val("");
        $("#V_GID" + id).val("");
    }
}

function SelectIts(obj, id) {
    var HaveSelect = "@";
    var dayid = $(obj).closest(".VisitList").attr("tids");
    var lines = $(obj).closest(".VisitList").attr("tgs");
    var names = $(obj).closest(".CheckBoxList").attr("tns");
    //取全部已选择的
    $(".Day" + dayid + " .ChkIt").each(function () {
        if ($(this).prop("checked") == true && $(this).attr("name") != $(obj).attr("name")) {
            HaveSelect += this.value + "@";
        }
    });
    if ($(obj).prop("checked") == true) {
        if (HaveSelect.indexOf("@" + obj.value + "@") > -1) {
            obj.checked = false;
            alert(lines + "，" + $(obj).attr("tgs") + "只能参加一条线路！");
            return;
        }
    }

    var arrChk = "";
    var arrName = "";
    var nums = Number($("#V_Nums" + id).val());
    var i = 0;
    $("input[name$='CheckBox" + id + "']").each(function () {
        if ($(this).prop("checked") == true) i = i + 1;
    });

    if (i > nums) {
        obj.checked = false;
        alert(names + "，参加人数不能多于" + nums + "人");
        return;
    }
    else {
        SelAll(id);
    }
}

function SelAll(id) {
    var arrChk = "";
    var arrName = "";
    var arrGuestid = "";
    $("input[name$='CheckBox" + id + "']").each(function () {
        if ($(this).prop("checked") == true) {
            arrGuestid += $(this).attr("gid") + "@";
            arrChk += this.value + "@";
            arrName += $(this).attr("tgs") + "，";
        }
    });
    arrName = arrName.substr(0, arrName.length - 1);
    $("#Visit_Span" + id).html(arrName);
    $("#V_NO" + id).val(arrChk);
    $("#V_NM" + id).val(arrName);
    $("#V_GID" + id).val(arrGuestid);
}

//        $(document).ready(function () {
//            $("input[name$='CheckBox']").live("click", function () {
//                arrChk += this.value + "@";
//                arrName += $(this).attr("tgs") + ",";
//                alert(arrChk + " " + arrName);
//            })
//        });

function GoToNext() {
    $(".VisitList").each(function () {
        var HaveSelect = "@";
        var id = $(this).attr("tids");
        var visits = $(this).attr("tgs");
        $(".Day" + id + " .ChkIt").each(function () {
            if ($(this).prop("checked") == true) {
                if (HaveSelect.indexOf("@" + this.value + "@") == -1) {
                    HaveSelect += this.value + "@";
                }
                else {
                    alert(visits + "," + $(this).attr("tgs") + "只能参加一条线路！");
                    CheckFlag = "1";
                    return false;
                }
            }
        });
    });

    CheckFlag = "0";
    $(".CheckBoxList").each(function () {
        var id = $(this).attr("tgs");
        var names = $(this).attr("tns");
        var nums = Number($("#V_Nums" + id).val());
        var ids = $("#V_NO" + id).val();
        var array = ids.split('@');
        var haves = array.length - 1;
        if (haves != nums) {
            alert(names + "必须选择" + nums + "位参加人员");
            SelectGuest(id, '1');
            CheckFlag = "1";
            return false;
        }
    });

    if (CheckFlag == "1") {
        //alert($("#form_data").serialize());
    }
    else {
        SubmitOrder();
    }
}