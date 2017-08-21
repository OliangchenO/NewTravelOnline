var tip_brithday = "yyyy-mm-dd";
var tip_guestname = "所选证件的中文姓名";
var tip_enname = "所选证件的拼音姓名";
var C_Area, C_Sex, C_BirthDay;
var CheckFlag = "0";
    
function limitChars(obj, count) {
    if (obj.value.length > count){   
    obj.value = obj.value.substr(0, count);
    }   
};

$(function () {
    $('.Guest_IdType').change(function () {
        var parent = $(this).parents("ul");
        var parentid = "#" + $(this).parents("ul").attr("id");
        GetDrop(this, parentid);
    });
});

function GetDrop(obj,parentid) {
    $(parentid + " div:eq(6)").show();
    $(parentid + " div:eq(7)").show();
    $(parentid + " div:eq(10)").show();
    $(parentid + " div:eq(11)").show();
	$(parentid + " li:eq(4)").show();
	$(parentid + " li:eq(5)").show();
	$(parentid + " li:eq(6)").show();
    if ($(obj).val() == "1") {
        $(parentid + " li:eq(4)").hide();
        $(parentid + " li:eq(5)").hide();
        $(parentid + " li:eq(6)").hide();
    }
    else if ($(obj).val() == "11") {
        $(parentid + " li:eq(4)").hide();
        $(parentid + " li:eq(5)").hide();
        $(parentid + " li:eq(6)").hide();
        $(parentid + " div:eq(6)").hide();
        $(parentid + " div:eq(7)").hide();
        $(parentid + " div:eq(10)").hide();
        $(parentid + " div:eq(11)").hide();
    }
    else if ($(obj).val() == "2") {

    }
    else if ($(obj).val() == "3") {
        $(parentid + " li:eq(5)").hide();
    }
    else {
        $(parentid + " li:eq(5)").hide();
        $(parentid + " li:eq(6)").hide();
    }
};

window.onscroll = function () {
    var top = "260";
    var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    if (scrollTop > top) {
        $("#pricebar").attr({ "class": "package_ptfix" });
    } else {
        $("#pricebar").removeAttr("class");
    }
};

jQuery.fn.smartFocus = function (text) {
    $(this).focus(function () {
        if ($(this).val() == text) {
            $(this).val("");
            $(this).css('color', '#000');
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val(text);
            $(this).css('color', '#999');
        }
    });
    if ($(this).val() != "" && $(this).val() != text) {
        $(this).css('color', '#000');
    }
    else {
        $(this).val(text);
        $(this).css('color', '#999');
    }
};


jQuery.fn.CleanIt = function () {
    var parentid = "#" + $(this).parents("ul").attr("id");
    $(parentid + " input").each(function () {
        $(this).val("");
    });
    $(parentid + " input:eq(0)").smartFocus(tip_guestname);
    $(parentid + " input:eq(1)").val("");
    $(parentid + " input:eq(2)").val("");
    $(parentid + " input:eq(3)").smartFocus(tip_brithday);
    $(parentid + " input:eq(4)").smartFocus(tip_enname);
    $(parentid + " input:eq(5)").smartFocus(tip_brithday);
    $(parentid + " input:eq(6)").smartFocus(tip_brithday);
    $(parentid + " input:eq(7)").val("");
    $(parentid + " input:eq(8)").val("");
};

jQuery.fn.GetPinYin = function () {
    var parentid = "#" + $(this).parents("ul").attr("id");
    var cnname = $.trim($(this).val());
    if (cnname.length < 2 || cnname == "") {
        $(this).attr({ "oldname":""});
        $(parentid + " input:eq(4)").val("");
        return false;
    }            
    var oldname = $(this).attr("oldname")
    if (oldname != cnname) {
        var url = "/Purchase/AjaxService.aspx?action=GetPinYin&CnName=" + escape(cnname) + "&r=" + Math.random();
        $.getJSON(url, function (date) {
            $(parentid + " input:eq(4)").val(date.success);
            $(parentid + " input:eq(4)").css('color', '#000');
        })
        $(this).attr({ "oldname": $(this).val() });
    }
};

jQuery.fn.CheckIdCard = function () {
    var parentid = "#" + $(this).parents("ul").attr("id");
    var idcard = $.trim($(this).val());
    
    if (idcard.length == 15 || idcard.length == 18) {
        checkIdcard(idcard);
        $(parentid + " input:eq(3)").val(C_BirthDay);
        $(parentid + " input:eq(3)").css('color', '#000');
    }
};

$(document).ready(function () {
	$(".guest").click(function () {
        $(this).addClass("divOver").siblings().removeClass("divOver");
    });
    $(".guest ul").each(function () {
        var parentid = "#" + $(this).attr("id");
        GetDrop($(parentid + " .Guest_IdType"), parentid);
    });
    $(".icon_del").click(function () {
        $(this).CleanIt();
    });
    $(".Guest_Name").blur(function () {
        $(this).GetPinYin();
    });
    $(".Guest_IdNum").blur(function () {
    	$(this).CheckIdCard();
    });
    
    $("#ht_title :radio").click(function () {
        RadioSet($(this).val());
    });
    $("#fp_title :radio").click(function () {
        RadioSet1($(this).val());
    });
    
    if ($("#NeedInvoice").prop("checked") == true) {
        $("#InvoiceInfo").show();
    }
    else {
        $("#InvoiceInfo").hide();
    }

    RadioSet($("#ht_title :radio:checked").val());
    RadioSet1($("#fp_title :radio:checked").val());

    $(".guest :input").each(function () {
        var classname = $(this).attr("class");
        if (classname == "Guest_BirthDay" || classname == "Guest_PassBgn" || classname == "Guest_PassEnd") $(this).smartFocus(tip_brithday);
        if (classname == "Guest_Name") $(this).smartFocus(tip_guestname);
        if (classname == "Guest_EnName") $(this).smartFocus(tip_enname);
    });
});


$("#DetailForm").submit(function () {
    Validator.Validate(this, 3);
    return false;
});

function GoToNext() {
    $("#ErrorMessagePanel").remove();
    CheckFlag = "0";
    $("#DetailForm").submit()
    if (CheckFlag == "1") {
        jError('<strong>信息填写不正确，请检查！</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
        return false;
    }
    if ($("#Order_Mobile").val() == "" && $("#Order_Tel").val() == "") {
        alert("联系人手机号码和联系电话至少填写一项");
        return false;
    }
    CheckForm();
    if (CheckFlag == "1") {
        jError('<strong>信息填写不正确，请检查！</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
    }
    else {
    	SubmitOrder();
    }
};

function CheckForm() {
    $(".guest ul").each(function () {
        var parentid = "#" + $(this).attr("id");
        if ($(parentid + " input:eq(0)").val() == "所选证件的中文姓名") {
            $(parentid + " input:eq(0)").SetErrorInfo("中文姓名必须填写");
            CheckFlag = "1";
            return false;
        }
        if ($(parentid + " div:eq(7)").is(":hidden") == false) {
            if ($.trim($(parentid + " input:eq(1)").val()) == "") {
                $(parentid + " input:eq(1)").SetErrorInfo("证件号码必须填写");
                CheckFlag = "1";
                return false;
            }
        }
        if ($(parentid + " div:eq(11)").is(":hidden") == false) {
            if ($(parentid + " input:eq(3)").val() == "yyyy-mm-dd") {
	            $(parentid + " input:eq(3)").SetErrorInfo("出生日期必须填写");
	            CheckFlag = "1";
	            return false;
	        }
	        else {
	            if ($(parentid + " input:eq(3)").DateErrorInfo("出生日期填写错误") == false) return false;
	        }
        }
        if ($(parentid + " li:eq(4)").is(":hidden") == false) {
            if ($(parentid + " input:eq(4)").val() == "所选证件的拼音姓名") {
                $(parentid + " input:eq(4)").SetErrorInfo("拼音姓名必须填写");
                CheckFlag = "1";
                return false;
            }
            if ($(parentid + " input:eq(5)").val() == "yyyy-mm-dd") {
                $(parentid + " input:eq(5)").SetErrorInfo("证件有效期必须填写");
                CheckFlag = "1";
                return false;
            }
            else {
                if ($(parentid + " input:eq(5)").DateErrorInfo("证件有效期填写错误") == false) return false;
            }
        }
        if ($(parentid + " li:eq(5)").is(":hidden") == false) {
            if ($(parentid + " input:eq(6)").val() == "yyyy-mm-dd") {
                $(parentid + " input:eq(6)").SetErrorInfo("签发日期必须填写");
                CheckFlag = "1";
                return false;
            }
            else {
                if ($(parentid + " input:eq(6)").DateErrorInfo("签发日期填写错误") == false) return false;
            }
        }
        if ($(parentid + " li:eq(6)").is(":hidden") == false) {
            if ($.trim($(parentid + " input:eq(7)").val()) == "") {
                $(parentid + " input:eq(7)").SetErrorInfo("签发地必须填写");
                CheckFlag = "1";
                return false;
            }
            if ($.trim($(parentid + " input:eq(8)").val()) == "") {
                $(parentid + " input:eq(8)").SetErrorInfo("出生地必须填写");
                CheckFlag = "1";
                return false;
            }
        }
        if ($(":radio:checked").val()=="2")
        {
        	if ($.trim($("#Ht_Address").val()) == "") {
                $("#Ht_Address").SetErrorInfo("地址必须填写");
                CheckFlag = "1";
                return false;
            }
        }
    });
};

function dateParse(str) {
    var hash = dateParse.hash || (dateParse.hash = {});
    if (str in hash)
        return hash[str];
    var ret = null;
    var m = str.match(/^(\d{4})-([01]?\d)-([0123]?\d)$/);
    if (m) {
        var d = new Date(parseInt(m[1], 10), parseInt(m[2], 10) - 1, parseInt(m[3], 10));
        if ([d.getFullYear(), d.getMonth() + 1, d.getDate()].join('-') == str.replace(/-0/g, '-'))
            ret = d;
    }
    return hash[str] = ret;
}

jQuery.fn.SetErrorInfo = function (ErrorMessage) {
    var span = "<span id=ErrorMessagePanel>" + ErrorMessage + "</span>";
    $(this).after(span);
    this.focus();
    //alert($(this).attr("class"));
};

jQuery.fn.DateErrorInfo = function (ErrorMessage) {
    if (!dateParse($(this).val())) {
        var span = "<span id=ErrorMessagePanel>" + ErrorMessage + "</span>";
        $(this).after(span);
        this.focus();
        CheckFlag = "1";
        return false;
    }
    else {
        return true;
    }
};

function RadioSet(vals) {
    $("#Ht_List li").hide();
    $("#Ht_List li:eq(0)").show();
    $("#Ht_List li:eq(" + vals + ")").show();
    if (vals == "2") {
        $("#Ht_Script").html("您选择了快递签约，请在下列地址栏输入您的地址，我们将把合同快递给您");
    }
    else {
        $("#Ht_Script").html("您选择了传真或快递签约方式，预订成功并在线支付后，可下载正式合同");
    }
};

function RadioSet1(vals) {
	$("#FP_List li:eq(3)").hide();
    $("#FP_List li:eq(" + vals + ")").show();
};