$(function () {
    $('.psel').change(function () {
        var parent = $(this).parents("li");
        var parentid = "#" + $(this).parents("li").attr("id");
        var sums = 0;
        
        //邮轮价格计算
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
        	alert("1");
        	rooms = Number($(parentid + " .froom").html());
        	adults = Number($(parentid).attr("AdultNums"));
        	childs = Number($(parentid).attr("ChildNums"));
        	price  =Number($(parentid + " .sellprice").html());
        	adultprice = Number($(parentid + " .adultprice").html();
        	childprice = Number($(parentid + " .childprice").html();
        	if (adultprice == 0) adultprice = price;
        	if (childprice == 0) childprice = price; 
        	alert("2");
        	if ((adults+childs)==rooms*2)
        	{
        		sums = Number($(this).val()) * Number($(parentid + " .sellprice").html());
        	}
        	else
        	{}
        }
        else
        {
        	sums = Number($(this).val()) * Number($(parentid + " .sellprice").html());
        }
        $(parentid + " .sumprice").html(sums);
        if ($(this).val() == "0") {
            $(parentid + " div:last").attr({ "class": "fnpic" });
        }
        else {
            $(parentid + " div:last").attr({ "class": "fpic" });
        }
        SumAllPrice();
    });
});

function SumAllPrice() {
    var PriceSum = 0;
    var PriceAve = 0;
    $(".sumprice").each(function () { PriceSum += Number($(this).html()); });
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
    $(".priceli").each(function () {
        var parentid = "#" + $(this).attr("id");
        //alert($(parentid + " .fname").html());
        var sums = 0;
        
        //邮轮价格计算
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
        	alert("1");
        	rooms = Number($(parentid + " .froom").html());
        	adults = Number($(parentid).attr("AdultNums"));
        	childs = Number($(parentid).attr("ChildNums"));
        	price  =Number($(parentid + " .sellprice").html());
        	adultprice = Number($(parentid + " .adultprice").html();
        	childprice = Number($(parentid + " .childprice").html();
        	if (adultprice == 0) adultprice = price;
        	if (childprice == 0) childprice = price; 
        	alert("2");
        	if ((adults+childs)==rooms*2)
        	{
        		sums = Number($(parentid + " .psel").val()) * Number($(parentid + " .sellprice").html());
        	}
        	else
        	{}
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
    if (Nums != OrderNums)
    {
        alert("所选基本费用人数与订单预订人数不符，请检查！");
        return false;
    }
    SubmitOrder()
}