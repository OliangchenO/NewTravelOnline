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
        SumAllPrice();
    });

    $('#Ht_Branch').change(function () {
        $('#BranchInfo').html("<br><span class=iloading>正在加载门店地图，请稍候...</span>");
        var url = "../../Users/AjaxService.aspx?action=BranchMapInfo&BranchId=" + $(this).val();
        $.getJSON(url, function (date) {
            $('#BranchInfo').html(date.success);
        })
    });
});

function SumAllPrice() {
    //计算岸上观光总金额
    var ViewPriceCount = 0;
    var ViewRebateCount = 0;
    $(".priceli").each(function () {
        var pid = "#" + $(this).attr("id");
        if ($(this).attr("tps") == "ShipVisit") {
            ViewPriceCount += Number($(pid + " .sumprice").html());
        }
        if ($(this).attr("tps") == "ViewsRebate") {
            ViewRebateCount += Number($(this).attr("tag"));
        }
    });

    if (ViewRebateCount > 0) {
        //if (ViewRebateCount > ViewPriceCount) ViewRebateCount = ViewPriceCount;
        $("#VR0 .sumprice").html("-" + ViewRebateCount);
        $("#VR0 .viewrebate").html("-" + ViewRebateCount);
        $("#VR0 .sellprice").html("-" + ViewRebateCount);
    }

    var PriceSum = 0;
    var PriceAve = 0;
    var RoomGather = Number($("#Room_Gather").val());
    $(".sumprice").each(function () { PriceSum += Number($(this).html()); });
    if ($("#ht_title :radio:checked").val() == "1") PriceSum += Number($("#SumPre_Price").html());
    if ($("#ht_title :radio:checked").val() == "2" && $("#LineId").val() == "12509") PriceSum += Number($("#SumPre_Price").html());
    PriceSum += RoomGather;
    PriceAve = parseInt(PriceSum / Number($("#Nums").val()));
    $("#spanAmount").html(PriceSum);
    $("#spanAve").html(PriceAve);
}

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

//window.onload = onload_handler;   
function onload_handler()    
{
    $("#ht_title :radio").click(function () {
    	RadioSet($(this).val());
    });
    RadioSet($("#ht_title :radio:checked").val());
    
    $('#BranchInfo').html("<br><span class=iloading>正在加载门店地图，请稍候...</span>");
    var url = "../../Users/AjaxService.aspx?action=BranchMapInfo&BranchId=" + $("#Ht_Branch").val();
    $.getJSON(url, function (date) {
        $('#BranchInfo').html(date.success);
    })
    	
    $(".priceli").each(function () {
        var parentid = "#" + $(this).attr("id");
        //alert($(parentid + " .fname").html());
        var sums = 0;
        
        var rooms = 0;
        var adults = 0;
        var childs = 0;
        var price = 0;
        var adultprice = 0;
        var childprice = 0;
        var price1 = 0;
        var price2 = 0;
        var price3 = 0;
        if ($(parentid).attr("Cuises")=="1")
        {
        	rooms = Number($(parentid + " .froom").html());
        	adults = Number($(parentid).attr("AdultNums"));
        	childs = Number($(parentid).attr("ChildNums"));
        	price  =Number($(parentid + " .sellprice").html());
        	adultprice = Number($(parentid + " .adultprice").html());
        	childprice = Number($(parentid + " .childprice").html());
        	if (adultprice == 0) adultprice = price;
        	if (childprice == 0) childprice = price; 
        	if ((adults+childs)==rooms*2)
        	{
        		sums = Number($(parentid + " .psel").val()) * Number($(parentid + " .sellprice").html());
        	}
        	else
        	{
        		price1 = rooms*2*price;
        		if (childs >= rooms)
        		{
        			price2 = rooms*childprice;
        		}
        		else
        		{
        			price2 = childs*childprice;
        			price3 = (rooms-childs)*adultprice;
        		}
        		sums = price1 + price2 + price3;
        	}
 
        }
        else
        {
        	sums = Number($(parentid + " .psel").val()) * Number($(parentid + " .sellprice").html());
        }
        //var sums = Number($(parentid + " .psel").val()) * Number($(parentid + " .sellprice").html());
        $(parentid + " .sumprice").html(sums);
        if ($(parentid + " .psel").val() == "0") {
            $(parentid + " div:last").attr({ "class": "fnpic" });
        }
        else {
            $(parentid + " div:last").attr({ "class": "fpic" });
        }
        SumAllPrice();
    });
}

function GoToNext() {
    var Nums = 0;
    var OrderNums = Number($("#Nums").val());
    $("#SellPrice .psel").each(function () {
        Nums += Number(this.value);
    });
    var RoomGather = Number($("#Room_Gather").val());
    if (RoomGather == 0) { 
        if (Nums != OrderNums)
        {
            alert("所选基本费用人数与订单预订人数不符，请检查！");
            return false;
        }
    }
    SubmitOrder()
}
