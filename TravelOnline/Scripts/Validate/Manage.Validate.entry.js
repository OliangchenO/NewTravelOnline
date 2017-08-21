$.extend(validatePrompt,{
	managename:{
		onFocus:"4-20位字符，由中文、英文、数字及“_”、“-”组成",
		succeed:"",
		isNull:"请输入用户名",
		error:{
			badLength:"用户名长度只能在4-20位字符之间",
			badFormat:"用户名只能由中文、英文、数字及“_”、“-”组成"
		}
	},
	loginpwd:{
		onFocus:"请输入密码，6-18位字符",
		succeed:"",
		isNull:"请输入密码，6-18位字符",
		error:{
			badLength:"密码长度只能在6-18位字符之间",
			badFormat:"密码只能由英文、数字及“_”、“-”组成"
		}
	}
});

$.extend(validateFunction,{
	managename:function(option){
		var format=validateRules.isUid(option.value);
		var length=validateRules.betweenLength(option.value.replace(/[^\x00-\xff]/g,"**"),4,20);
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
		}
		//validateSettings.succeed.run(option);
	},
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
		$("#managename").jdValidate(validatePrompt.managename,validateFunction.managename,true);
		$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd,true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#managename","#loginpwd","#authcode"]);
	},
	FORM_validate_authcode:function(){
	    $("#managename").jdValidate(validatePrompt.managename,validateFunction.managename,true);
		$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd,true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#loginname","#loginpwd","#authcode"]);
	}
});
//setTimeout(function(){$("#loginname").get(0).focus();},0);
setTimeout(function() { 
	if (!$("#managename").val()) 
	{ $("#managename").get(0).focus();}
	else
	{$("#loginpwd").get(0).focus();}
 }, 0); 

$("#managename").jdValidate(validatePrompt.managename,validateFunction.managename);
$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);

function verc(){
	$("#JD_Verification1").click();
}

$("#managename,#loginpwd, #authcode").bind('keyup',function(event) {
    if(event.keyCode==13){
        $("#loginsubmit").click();
    }     
});

$("#loginsubmit").click(function() {
	//alert($("#formlogin").serialize());
	var flag = validateFunction.FORM_validate();
    if (flag) {
    	$("#islogin").show();
    	$("#loginsubmit").hide();
    	var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
    	$(this).attr({ "disabled": "disabled" });
        $.ajax({
            type: "POST",
            url: "/Manage/LoginService.aspx?uuid=" + uuid + "&r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formlogin").serialize(),
            error: function() {
                $("#loginpwd").attr({ "class": "text highlight2" });
                $("#loginpwd_error").html("系统忙，请稍候...").show().attr({ "class": "error" }); $("#loginsubmit").removeAttr("disabled");
            },
            success: function(result) {
            	if (result) {
                	//var obj = eval("("+result+")");
                	var obj = eval(result);
                    if (obj.success) {
                    	window.location = "/Management/ManageHome.aspx";
                    }
                    else {
	                    if (obj.username) {
	                        $("#managename").attr({ "class": "text highlight2" });
	                        $("#managename_error").html(obj.username).show().attr({ "class": "error" });
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
	                    $("#loginsubmit").show();
                    }
                }
            }
        });
    }
});
