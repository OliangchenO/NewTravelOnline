$.extend(validatePrompt,{
	username:{
	    onFocus: "请输入电子邮箱或手机号码",
		succeed:"",
		isNull: "请输入电子邮箱或手机号码",
		error:"不存在此用户名"
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
	username:function(option){
		var format=validateRules.isNull(option.value);
		if (!format){
            validateSettings.succeed.run(option);			
		}
		else{
		    validateSettings.error.run(option,option.prompts.error);
		}
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
	pwd:function(option){
		validateSettings.succeed.run(option);
	},
	FORM_validate:function(){
		$("#loginname").jdValidate(validatePrompt.username,validateFunction.username,true);
		$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd,true);
		return validateFunction.FORM_submit(["#loginname","#loginpwd"]);
	}
});

//setTimeout(function() { if (!$("#loginname").val()) { $("#loginname").get(0).focus(); } }, 0); 
setTimeout(function() { 
	if (!$("#loginname").val()) 
	{ $("#loginname").get(0).focus(); }
	else
	{$("#loginpwd").get(0).focus();}
 }, 0); 
$("#loginname").jdValidate(validatePrompt.username,validateFunction.username);
$("#loginpwd").jdValidate(validatePrompt.loginpwd,validateFunction.loginpwd);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);

function verc(){
	$("#JD_Verification1").click();
}

$("#loginname,#loginpwd, #authcode").bind('keyup',function(event) {
    if(event.keyCode==13){
        $("#loginsubmit").click();
    }     
});

$("#loginsubmit").click(function() {
    var flag = validateFunction.FORM_validate();
    if (flag) {
        var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
        $(this).attr({ "disabled": "disabled" });
        $.ajax({
            type: "POST",
            url: "LoginService.aspx?uuid=" + uuid + "&" + location.search.substring(1) + "&r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formlogin").serialize(),
            error: function() {
                $("#loginpwd").attr({ "class": "text highlight2" });
                $("#loginpwd_error").html("系统忙，请稍候").show().attr({ "class": "error" }); $("#loginsubmit").removeAttr("disabled");
            },
            success: function(result) {
                if (result) {
                    var obj = eval(result);
                    var urls = $.cookie("pageurl");
                    if (urls== null) urls="/index.html";
                    if (obj.success)
                    	window.location = urls;
                    if (obj.username) {
                        $("#loginname").attr({ "class": "text highlight2" });
                        $("#loginname_error").html(obj.username).show().attr({ "class": "error" });
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
                    verc();
                }
            }
        });
    }
});
$("#loginsubmitframe").click(function() {
    var flag = validateFunction.FORM_validate();
    if (flag) {
        var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
        $(this).attr({ "disabled": "disabled" });
        $.ajax({
            type: "POST",
            url: "LoginService.aspx?uuid=" + uuid + "&" + location.search.substring(1) + "&r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formloginframe").serialize(),
            error: function() {
                $("#loginpwd").attr({ "class": "text highlight2" });
                $("#loginpwd_error").html("系统忙，请稍候").show().attr({ "class": "error" }); $("#loginsubmitframe").removeAttr("disabled");
            },
            success: function(result) {
                if (result) {
                    var obj = eval(result);
                    if (obj.success || obj.transfer) {
                        window.parent.jdModelCallCenter.init();
                    }
                    if (obj.verifycode) {
                        verc();
                        $("#o-authcode").show();
                    }
                    if (obj.username) {
                        $("#loginname").attr({ "class": "text highlight2" });
                        $("#loginname_error").html(obj.username).show().attr({ "class": "error" });
                    }
                    if (obj.pwd) {
                        $("#loginpwd").attr({ "class": "text highlight2" });
                        $("#loginpwd_error").html(obj.pwd).show().attr({ "class": "error" });
                    }
                    if (obj.authcode) {
                        verc();
                        $("#authcode").attr({ "class": "text text-1 highlight2" });
                        $("#authcode_error").html(obj.authcode).show().attr({ "class": "error" });
                    }
                    $("#loginsubmitframe").removeAttr("disabled");
                }
            }
        });
    }
});
$("#loginname,#loginpwd, #authcode").bind('keyup', function(event) {
    if (event.keyCode == 13) {
        $("#loginsubmitframe").click();
    }
});