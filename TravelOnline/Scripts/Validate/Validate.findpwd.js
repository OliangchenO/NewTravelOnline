$.extend(validateFunction,{
	FORM_validate:function(){
	    $("#mail").jdValidate(validatePrompt.findmail, validateFunction.findmail, true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#mail","#authcode"])
	}		 
});

//调用
setTimeout(function(){$("#mail").get(0).focus();},0);
$("#mail").jdValidate(validatePrompt.findmail, validateFunction.findmail);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);

$("#registsubmit").click(function () {
    var flag = validateFunction.FORM_validate();
    if (flag) {
        $(this).attr({ "disabled": "disabled" }).attr({ "value": "提交中,请稍等" });
        var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
        $.ajax({
            type: "POST",
            url: "AjaxService.aspx?action=SendEmail&uid=" + uuid,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formpersonal").serialize(),
            error: function () {
                $("#registsubmit").attr({ "value": "提交失败，请重试" });
                $("#registsubmit").removeAttr("disabled");
            },
            success: function (result) {
                if (result) {
                    var obj = eval(result);
                    if (obj.info) {
                        $("#registsubmit").removeAttr("disabled");
                        $("#registsubmit").attr({ "value": "发送密码找回邮件" });
                        alert(obj.info);
                        if (obj.info == "验证码错误") {
                            $("#authcode").val("")
                            setTimeout(function () { $("#authcode").get(0).focus(); }, 0);
                            verc();
                        }
                        if (obj.info == "邮件地址不存在") {
                            setTimeout(function () { $("#mail").get(0).focus(); }, 0);
                            verc();
                        }
                    }
                    if (obj.success) {
                        window.location = "SendSuccess.aspx?uid=" + obj.success;
                    }
                }
            }
        });
    }
});

function verc(){
	$("#JD_Verification1").click();
}

$("#authcode").bind('keyup',function(event) {  
    if(event.keyCode==13){  
        $("#registsubmit").click();
    }
});

