$.extend(validatePrompt,{
	loginpwd:{
		onFocus:"请输入密码，6-18位字符",
		succeed:"",
		isNull:"请输入密码，6-18位字符",
		error:{
			badLength:"密码长度只能在6-18位字符之间",
			badFormat:"密码只能由英文、数字及“_”、“-”组成"
		}
	},
});
$.extend(validateFunction,{
	loginpwd:function(option){
		var format=validateRules.isPwd(option.value);
		var length=validateRules.betweenLength(option.value,6,18);
		if(!length&&format){
			validateSettings.error.run(option,option.prompts.error.badLength);
		}
		else if(!length&&!format){
			validateSettings.error.run(option,option.prompts.error.badFormat);
		}
		else if(length&&!format){
			validateSettings.error.run(option,option.prompts.error.badFormat);
		}
		else{
			validateSettings.succeed.run(option);	
			//validateFunction.pwdstrength();
		}
		//validateSettings.succeed.run(option);
	},
	FORM_validate:function(){
		$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd,true);
		$("#pwd").jdValidate(validatePrompt.pwd,validateFunction.pwd,true);
		$("#pwd2").jdValidate(validatePrompt.pwd2,validateFunction.pwd2,true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#loginpwd","#pwd","#pwd2","#authcode"])
	}		 
});

//调用
setTimeout(function(){$("#loginpwd").get(0).focus();},0); 
//$("#username").jdValidate(validatePrompt.username,validateFunction.username);
$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd);
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

$("#loginsubmit").click(function() {
	//alert($("#formlogin").serialize());
	var flag = validateFunction.FORM_validate();
    if (flag) {
    	$("#islogin").show();
    	var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
    	//$(this).attr({ "disabled": "disabled" });
        $.ajax({
            type: "POST",
            url: "/Login/AjaxService.aspx?action=ChangePassWord&uuid=" + uuid + "&r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formlogin").serialize(),
            error: function() {
                $("#loginpwd").attr({ "class": "text highlight2" });
                $("#loginpwd_error").html("系统忙，请稍候...").show().attr({ "class": "error" }); 
                $("#loginsubmit").removeAttr("disabled");
            },
            success: function(result) {
            	//alert(result);
            	if (result) {
                	var obj = eval(result);
                	if (obj.logout) {
                		alert("您的验证信息已失效，请登录后再操作！");
                		window.location = "/Login/Login.aspx";
                	}
                	if (obj.info) {
                		SaveError();
                	}
                    if (obj.pwd) {
                        $("#loginpwd").attr({ "class": "text highlight2" });
                        $("#loginpwd_error").html(obj.pwd).show().attr({ "class": "error" });
                    }
                    if (obj.authcode) {
                        $("#authcode").attr({ "class": "text text-1 highlight2" });
                        $("#authcode_error").html(obj.authcode).show().attr({ "class": "error" });
                    }
                    $("#loginsubmit").removeAttr("disabled");
                    $("#islogin").hide();
                    if (obj.success) {
                		$("#loginsubmit").attr({ "disabled": "disabled" });
                		$("#loginsubmit").attr({ "value": "修改成功" });
                		SaveSuccess();
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


function SaveSuccess() {
    jSuccess('<strong>密码修改成功!</strong>',{ShowOverlay : false,HorizontalPosition : 'center',VerticalPosition : 'center'});
}

function ValidateError() {
    jNotify('<strong>信息填写不正确，请检查!</strong>',{autoHide : false,clickOverlay : true,HorizontalPosition : 'center',VerticalPosition : 'center'});
}

function SaveError() {
    jError('<strong>密码修改失败，请稍后再试!</strong>',{autoHide : false,clickOverlay : true,HorizontalPosition : 'center',VerticalPosition : 'center'});
}