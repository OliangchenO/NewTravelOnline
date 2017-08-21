$.extend(validateFunction,{
	FORM_validate:function(){
		$("#pwd").jdValidate(validatePrompt.pwd,validateFunction.pwd,true);
		$("#pwd2").jdValidate(validatePrompt.pwd2,validateFunction.pwd2,true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#pwd","#pwd2","#authcode"])
	}		 
});

//调用
setTimeout(function () { $("#pwd").get(0).focus(); }, 0); 
$("#pwd").bind("keyup",function(){
	validateFunction.pwdstrength();
}).jdValidate(validatePrompt.pwd,validateFunction.pwd)
$("#pwd2").jdValidate(validatePrompt.pwd2,validateFunction.pwd2);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);
$("#viewpwd").bind("click",function(){
	if ($(this).is(":checked")==true){
		validateFunction.showPassword("text");
		//alert($(this).attr("checked"));
		$("#o-password").addClass("pwdbg");
	}else{
		validateFunction.showPassword("password");
		$("#o-password").removeClass("pwdbg");
		//alert($(this).attr("checked"));
	}
});

$("#registsubmit").click(function () {
    var flag = validateFunction.FORM_validate();
    if (flag) {
        $(this).attr({ "disabled": "disabled" }).attr({ "value": "提交中,请稍等" });
        var uid = $("#uid").val();
        $.ajax({
            type: "POST",
            url: "AjaxService.aspx?action=RestePwd&uid=" + uid,
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
                        $("#registsubmit").attr({ "value": "确认重设密码" });
                        alert(obj.info);
                        if (obj.info == "验证码错误") {
                            $("#authcode").val("")
                            setTimeout(function () { $("#authcode").get(0).focus(); }, 0);
                            verc();
                        }
                    }
                    if (obj.success) {
                        window.location = "ResetSuccess.aspx";
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

