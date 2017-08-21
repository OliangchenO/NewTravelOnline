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
	authcode:{
		onFocus:"请输您收到的短信验证码",
		succeed:"",
		isNull:"请输您收到的短信验证码",
		error:{
			badLength:"验证码长度只能是6位字符"
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
	authcode:function(option){
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
		$("#mobile").jdValidate(validatePrompt.mobile,validateFunction.mobile,true);
		$("#address").jdValidate(validatePrompt.address,validateFunction.address,true);
        $("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode,true);
		return validateFunction.FORM_submit(["#authcode","#truename","#mobile","#address"]);
	}
});

setTimeout(function(){$("#truename").get(0).focus();},0);
$("#truename").jdValidate(validatePrompt.truename,validateFunction.truename);
$("#mobile").jdValidate(validatePrompt.mobile,validateFunction.mobile);
$("#address").jdValidate(validatePrompt.address,validateFunction.address);
$("#authcode").jdValidate(validatePrompt.authcode,validateFunction.authcode);

function verc(){
	$("#JD_Verification1").click();
}

$("#authcode").bind('keyup',function(event) {
    if(event.keyCode==13){
        $("#loginsubmit").click();
    }     
});

var wait=60;
function time(o) {
	if (wait == 0) {
		o.removeAttribute("disabled");
		o.value="发送验证码";
		wait = 60;
	} else {
		o.setAttribute("disabled", true);
		o.value="重新发送(" + wait + ")";
		wait--;
		setTimeout(function() {
			time(o)
		},
		1000)
	}
}


$("#Button1").click(function() {
    $("#mobile").jdValidate(validatePrompt.mobile,validateFunction.mobile,true);
    var flag = validateFunction.FORM_submit(["#mobile"]);
	if(!flag){
		jError('<strong>手机号码填写不正确，请检查!</strong>',{autoHide : false,clickOverlay : true,HorizontalPosition : 'center',VerticalPosition : 'center'});
	}
    if (flag) {
        time(this);
        var url = "/Login/AjaxService.aspx?action=SendAuthcodeSMS&mobile=" + $("#mobile").val() + "&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                jSuccess('<strong>发送成功，请查收您的短信，验证码30分钟内有效!</strong>',{TimeShown:4000,ShowOverlay : false,HorizontalPosition : 'center',VerticalPosition : 'center'});
            }
            else {
                jError("<strong>" + date.content + "</strong>", { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                wait=0;
                time(this);
            }
        })
    }
});

$("#loginsubmit").click(function() {
	var flag = validateFunction.FORM_validate();
	if(!flag){
		ValidateError();
	}
    $("#loginsubmit").attr({ "disabled": "disabled" }).attr({ "value": "验证中..." });
    if (flag) {
        $.post("/Login/AjaxService.aspx?action=JoinInfo&r=" + Math.random(), $("#formlogin").serialize(),function (data) {
            var obj = eval(data);
            if (obj.success==0) {
                $("#loginsubmit").attr({ "disabled": "disabled" }).attr({ "value": "加入会员成功" });
                jSuccess('<strong>加入会员成功!</strong>',{ShowOverlay : false,HorizontalPosition : 'center',VerticalPosition : 'center'});
                location.reload();
            }
            else
            {
                $("#loginsubmit").removeAttr("disabled");
                $("#loginsubmit").attr({ "value": "加入积分会员" });
                jError("<strong>" + obj.content + "</strong>", { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
        });
    }
});