function ShowDeck(ship, deck) {
    var url = "/travel/ShowDeck.aspx?ship=" + ship + "&deck=" + deck;
    var dg = new $.dialog({ id: 'No1', page: url, title: deck + '层甲板示意图', width: 800, height: 500, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
    dg.ShowDialog();
}

//function ScrollTo(href) {
//    var pos = $("#"+href).offset().top;
//    $("html,body").animate({ scrollTop: pos }, 700);
//}

$(function () {
    $('.psel').live("change", function () {
        var parent = $(this).parents("div");
        var parentid = "#" + parent.attr("id");
        berth = parseInt(parent.attr("mnum"));
        adult = parseInt($(parentid + " select:eq(0)").val());
        childs = parseInt($(parentid + " select:eq(1)").val());
        if ((adult + childs) == 0 || adult == 0) {
            $(parentid + " .pcount").html("&yen;0");
            $(parentid + " .hide").html("0");
            $(parentid + " select:eq(2)").html("<option value=\"0\">0</option>");
            return;
        }
        range = [], str = ""
        if (adult != 0) {
            range.push(Math.ceil((adult + childs) / berth));
            range.push(Math.ceil(adult + childs));
            if (range[0] != 0) {
                for (var i = range[0]; i <= range[1]; i++) {
                    str += "<option value=\"" + i + "\">" + i + "</option>";
                };
                if (berth > 2) {
                    str = "<option value=\"" + range[0] + "\">" + range[0] + "</option>";
                }
            }
        }
        else {
            str = "<option value=\"0\">0</option>";
        }
        $(parentid + " select:eq(2)").html(str);
        calTotal(parentid);

    })
});

$(function () {
    $('.fjs').live("change", function () {
        var parent = $(this).parents("div");
        var parentid = "#" + parent.attr("id");
        calTotal(parentid);
    })
});

function calTotal(pid) {
    berth = parseInt($(pid).attr("mnum"));
    price1 = parseInt($(pid).attr("p1"));
    price2 = parseInt($(pid).attr("p2"));
    price3 = parseInt($(pid).attr("p3"));
    adult = parseInt($(pid + " select:eq(0)").val());
    childs = parseInt($(pid + " select:eq(1)").val());
    hourseNum = parseInt($(pid + " select:eq(2)").val());
    allPeple = adult + childs;
    total = 0;
    switch (berth) {
        case 1:
            total = allPeple * price1;
            break
        case 2:
            total = hourseNum * price1 * 2;
            break
        default:
            total = hourseNum * price1 * 2;
            if (childs >= hourseNum * (berth - 2)) {
                total += hourseNum * (berth - 2) * price3;
            }
            else {
                total += childs * price3;
                total += (hourseNum * (berth - 2) - childs) * price2;
            }
            break
    }
    $(pid + " .hide").html(total);

    if (total == 0) {
        $(pid + " .pcount").html("&yen;0");
    } else {
        $(pid + " .pcount").html("&yen;" + total + "<font size=2>起</font>");
    }
}

function RoomClick(id) {
    //$(".htr").hide();
    $("#h" + id).toggle();
    var s = $("#show" + id).html();
    if (s.length < 5) {
        var url = "/Travel/AjaxService.aspx?action=LoadCruisesRoom&Id=" + id;
        //window.open(url);
        $("#show" + id).html("<div class=iloading>正在加载中，请稍候...</div>");
        $.getJSON(url, function (date) {
            $("#show" + id).html(date.content);
        })
    }
}

function ShowSight(id) {
    if ($("#Sight" + id).is(":visible") == false) {
        $(".viewtr").hide();
        $("#Sight" + id).toggle();
    }
    else {
        $("#Sight" + id).hide()
    }
}

$(function () {
    $('.RoomSelectButton  strong').live("click", function () {
        allotid = $(this).parents("div").attr("tag");

        if ($(this).hasClass("radioBoxOn")) {
            DeleteRoom(allotid);
            //$(this).attr("class", "radioBox");
            return
        }
        pid = "#" + $(this).parents("div").attr("id");
        roomname = $(pid).attr("tps");
        berth = parseInt($(pid).attr("mnum"));
        haverooms = parseInt($(pid).attr("haveroom"));
        allprice = parseInt($(pid + " .hide").html());
        price1 = parseInt($(pid).attr("p1"));
        price2 = "&yen;" + $(pid).attr("p2");
        price3 = "&yen;" + $(pid).attr("p3");
        adult = parseInt($(pid + " select:eq(0)").val());
        childs = parseInt($(pid + " select:eq(1)").val());
        hourseNum = parseInt($(pid + " select:eq(2)").val());
        if (price2 == "&yen;0") price2 = "--";
        if (price3 == "&yen;0") price3 = "--";

        if (hourseNum < 1) {
            alert("房间数量必须选择");
            return;
        }
        if (hourseNum > haverooms) {
            alert("您选择的" + roomname + "只剩余" + haverooms + "间，请重新选择");
            return;
        }
        if (adult < hourseNum) {
            alert(hourseNum + "间" + roomname + "都必须入住一位成人");
            return;
        }
        allPeple = adult + childs;
        if (berth > 2) {
            if (allPeple != hourseNum * berth) {
                alert("您选择的是" + hourseNum + "间" + berth + "人间，入住人数必须是" + hourseNum * berth + "人");
                return;
            }
        }

        var href = "#OrderHere";
        var pos = $(href).offset().top-50;
        $("html,body").animate({ scrollTop: pos }, 1000);

        $(this).attr("class", "radioBoxOn");
        str = "<tr>";
        str += "<td>" + roomname + "</td><td>" + adult + "</td><td>" + childs + "</td><td>" + hourseNum + "间</td><td class=tdn>&yen;" + price1 + "</td><td class=tdn>" + price2 + "</td><td class=tdn>" + price3 + "</td><td class=tds>&yen;" + allprice + "起"
        str += "<input class='RS_ID' name='RS_ID' type='hidden' value='" + allotid + "'/><input class='RS_CR' name='RS_CR' type='hidden' value='" + adult + "'/><input class='RS_ET' name='RS_ET' type='hidden' value='" + childs + "'/>";
        str += "<input class='RS_ROOM' name='RS_ROOM' type='hidden' value='" + hourseNum + "'/><input class='RS_PRICE' name='RS_PRICE' type='hidden' value='" + allprice + "'/><input class='RS_NUM' name='RS_NUM' type='hidden' value='" + allPeple + "'/>";
        str += "</td>";
        str += "<td><IMG id=IMG_" + allotid + " onclick=\"DeleteRoom('" + allotid + "')\" src='/Images/icons/no.png' class=img title='删除此房型'></td>";
        str += "</tr>";
        //alert(str);selectmore
        $("#RoomSelectList").show();
        $("#RoomSelectList").append(str);
        $("#selectmore").show();
        CountPrice();
    })
});

function DeleteRoom(id) {
    if (confirm('确认要删除选中的房型吗？')) {
    }
    else
    { return false; }
    trs = $("#IMG_" + id).parents("td").parents("tr");
    trs.remove();
    $("#RB_" + id).attr("class", "radioBox");
    CountPrice();
}

function CountPrice() {
    Adult = 0;
    Childs = 0;
    Rooms = 0;
    SumPrice = 0;
    SumPeople = 0;

    $(".RS_CR").each(function () { Adult += Number($(this).val()); });
    $(".RS_ET").each(function () { Childs += Number($(this).val()); });
    $(".RS_ROOM").each(function () { Rooms += Number($(this).val()); });
    $(".RS_PRICE").each(function () { SumPrice += Number($(this).val()); });
    SumPeople = (Adult + Childs);

    $("#AllPeople").val(SumPeople);
    $("#AllAdult").val(Adult);
    $("#AllChilds").val(Childs);
    $("#AllRoom").val(Rooms);
    $("#AllPrice").val(SumPrice);

    $("#ShowPriceCount").html("&yen;" + SumPrice + "<font size=2>起</font>");
    $("#ShowPriceText").html("成人" + Adult + "人&nbsp;&nbsp;儿童" + Childs + "人&nbsp;&nbsp;共" + SumPeople + "人&nbsp;&nbsp;&nbsp;&nbsp;舱房：" + Rooms + "间&nbsp;&nbsp;&nbsp;&nbsp;价格合计：");
    if (Rooms == 0) {
        $("#selectmore").hide();
        $("#RoomSelectList").hide();
        $("#ShowPriceCount").html("");
        $("#ShowPriceText").html("当前没有选择任何舱房，请选择后再预定");
        ScrollTo("room_order");
    }

}