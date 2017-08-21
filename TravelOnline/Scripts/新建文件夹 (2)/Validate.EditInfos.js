$.extend(validatePrompt,{
	truename:{
		onFocus:"2-20位字符，可由中文、英文组成",
		succeed:"",
		isNull:"请输入真实名",
		error:{
			badLength:"用户名长度只能在2-20位字符之间",
			badFormat:"用户名只能由中文、英文组成"
		}
	},
	tel:{
		onFocus:"请输入固定电话号码，7-18位字符",
		succeed:"",
		isNull:"请输入固定电话号码，7-18位字符",
		error:{
			badLength:"电话号码长度只能在7-18位字符之间",
			badFormat:"电话号码只能由数字、括号及“-”组成，7-18位字符之间"
		}
	},
	mobile:{
		onFocus:"请输入手机号码",
		succeed:"",
		isNull:"请输入手机号码",
		error:{
			badLength:"手机号码长度只能是11位字符",
			badFormat:"手机号码不符合规则，只能是11位数字"
		}
	},
	address:{
		onFocus:"地址长度不能超过100位字符",
		succeed:"",
		isNull:"地址不能为空",
		error:{
			badLength:"地址长度在5-100位字符之间",
			badFormat:"地址只能由英文、数字、汉字、括号及“#”组成"
		}
	},
	zipcode:{
		onFocus:"邮编为6位数字",
		succeed:"",
		isNull:"邮编不能为空",
		error:{
			badFormat:"邮编格式不正确",
			badLength:"邮编只能为6位数字"
		}
	},
	remark:{
		onFocus:"兴趣爱好只能在100个字符以内",
		succeed:"",
		isNull:"",
		error:{
			badLength:"您填写的兴趣爱好过长，只能在100个字符以内"
		}
	}
});

$.extend(validateFunction,{
	truename:function(option){
		var format=validateRules.isRealName(option.value);
		var length=validateRules.betweenLength(option.value,2,20);
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
	},
	tel:function(option){
		var format=validateRules.isTel(option.value);
		var length=validateRules.betweenLength(option.value,7,18);
		if(!length){
			validateSettings.error.run(option,option.prompts.error.badLength);
		}
		else {
			if(!format){
				validateSettings.error.run(option,option.prompts.error.badFormat);
			}
			else{
				validateSettings.succeed.run(option);
			}
		}
	},
	mobile:function(option){
		var format=validateRules.isMobile(option.value);
		var length=validateRules.betweenLength(option.value,11,11);
		if(!length){
			validateSettings.error.run(option,option.prompts.error.badLength);
		}
		else {
			if(!format){
				validateSettings.error.run(option,option.prompts.error.badFormat);
			}
			else{
				validateSettings.succeed.run(option);
			}
		}
	},
	address:function(option){
		var format=validateRules.isCompanyaddr(option.value);
		var length=validateRules.betweenLength(option.value,5,100);
		if(!length){
			validateSettings.error.run(option,option.prompts.error.badLength);
		}
		else {
			if(!format){
				validateSettings.error.run(option,option.prompts.error.badFormat);
			}
			else{
				validateSettings.succeed.run(option);
			}
		}
	},
	zipcode:function(option){
		var format=validateRules.isCompanyaddr(option.value);
		var length=validateRules.betweenLength(option.value,6,6);
		if(!length){
			validateSettings.error.run(option,option.prompts.error.badLength);
		}
		else {
			if(!format){
				validateSettings.error.run(option,option.prompts.error.badFormat);
			}
			else{
				validateSettings.succeed.run(option);
			}
		}
	},
	FORM_validate:function(){
		$("#truename").jdValidate(validatePrompt.truename,validateFunction.truename,true);
		$("#tel").jdValidate(validatePrompt.tel,validateFunction.tel,true);
		$("#mobile").jdValidate(validatePrompt.mobile,validateFunction.mobile,true);
		$("#address").jdValidate(validatePrompt.address,validateFunction.address,true);
		$("#zipcode").jdValidate(validatePrompt.zipcode,validateFunction.zipcode,true);
		$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#authcode","#truename"]);
	}
});

setTimeout(function(){$("#truename").get(0).focus();},0);
$("#truename").jdValidate(validatePrompt.truename,validateFunction.truename);
$("#tel").jdValidate(validatePrompt.tel,validateFunction.tel);
$("#mobile").jdValidate(validatePrompt.mobile,validateFunction.mobile);
$("#address").jdValidate(validatePrompt.address,validateFunction.address);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);
$("#zipcode").jdValidate(validatePrompt.zipcode,validateFunction.zipcode);

function verc(){
	$("#JD_Verification1").click();
}

$("#authcode").bind('keyup',function(event) {
    if(event.keyCode==13){
        $("#loginsubmit").click();
    }     
});

$("#loginsubmit").click(function() {
	var flag = validateFunction.FORM_validate();
	if(!flag){
		//alert("信息填写不正确，请检查");
		ValidateError();
	}
    if (flag) {
    	$("#islogin").show();
    	var uuid = $("#JD_Verification1").attr("src").split("&uid=")[1].split("&")[0];
    	//$(this).attr({ "disabled": "disabled" });
        $.ajax({
            type: "POST",
            url: "/Login/AjaxService.aspx?action=EditInfo&uuid=" + uuid + "&r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#formlogin").serialize(),
            error: function() {
                $("#authcode").attr({ "class": "text highlight2" });
                $("#authcode_error").html("系统忙，请稍候...").show().attr({ "class": "error" }); 
                $("#loginsubmit").removeAttr("disabled");
            },
            success: function(result) {
            	if (result) {
                	var obj = eval(result);
                	if (obj.logout) {
                		alert("您的验证信息已失效，请登录后再操作！");
                		//window.location = "/Login/Login.aspx";
                	}
                	if (obj.info) {
                		SaveError();
                	}
                    if (obj.authcode) {
                        $("#authcode").attr({ "class": "text text-1 highlight2" });
                        $("#authcode_error").html(obj.authcode).show().attr({ "class": "error" });
                    }
                    $("#loginsubmit").removeAttr("disabled");
                    $("#islogin").hide();
                    if (obj.success) {
                		$("#loginsubmit").attr({ "disabled": "disabled" }).attr({ "value": "修改成功" });
                		SaveSuccess();
                	}
                	
                }
            }
        });
    }
});

function SaveSuccess() {
    jSuccess('<strong>信息保存成功!</strong>',{ShowOverlay : false,HorizontalPosition : 'center',VerticalPosition : 'center'});
}

function ValidateError() {
    jNotify('<strong>信息填写不正确，请检查!</strong>',{autoHide : false,clickOverlay : true,HorizontalPosition : 'center',VerticalPosition : 'center'});
}

function SaveError() {
    jError('<strong>信息保存失败，请稍后再试!</strong>',{autoHide : false,clickOverlay : true,HorizontalPosition : 'center',VerticalPosition : 'center'});
}
