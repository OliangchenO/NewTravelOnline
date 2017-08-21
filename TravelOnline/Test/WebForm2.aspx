<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="TravelOnline.Test.WebForm2" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出境旅游</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages1.js"></script>
    
</head>
<body>
    <uc1:Header ID="Header1" runat="server" />
    <div class="w main">

    <DIV class=w>
    <DIV><A href="../index.html"><IMG alt=上海中国青年旅行社 src="/Images/logo.gif" 
    width=300 height=57></A></DIV></DIV>
   
   <DIV id=entry class=w>
<DIV class=mt>
<H2>用户登录</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
<form name="form1" action="regx.htm">
        <input type="text" value="aaa" />
        <input type="text" value="bbb" />
        <input type="text" value="" />
        <select>
            <option value="aaa">aaa</option>
            <option value="bbb" selected>bbb</option>
            <option value="ccc">ccc</option>
        </select>
        <input type="reset" value="Reset" />
    </form>
<form id="formpersonal" method="POST" onsubmit="return false;">
		
		    <div class="form">
			    <div class="item">
				    <span class="label">用户名：</span>
				    <div class="fl">
					    <input type="text" id="username" name="username" class="text highlight1" tabindex="1" sta="0">
					    <label id="username_succeed" class="blank"></label>
					    <span class="clr"></span>
					    <div id="username_error" class="focus">4-20位字符，可由中文、英文、数字及“_”、“-”组成</div>
                        
				    </div>
			    </div>
			    <div id="o-password">
				    <div class="item">
					    <span class="label">设置密码：</span>
					    <div class="fl">
						    <input type="password" id="pwd" name="pwd" class="text" tabindex="2">
						    <label id="pwd_succeed" class="blank"></label>
						    <input type="checkbox" class="checkbox" id="viewpwd">
						    <label class="ftx23" for="viewpwd">显示密码字符</label>
						    <span class="clr"></span>
						    <label class="hide" id="pwdstrength"><span class="fl">安全程度：</span><b></b></label>
						    <label id="pwd_error"></label>
    						
					    </div>
				    </div>
				    <div class="item">
					    <span class="label">确认密码：</span>
					    <div class="fl">
						    <input type="password" id="pwd2" name="pwd2" class="text" tabindex="3">
						    <label id="pwd2_succeed" class="blank"></label>
						    <span class="clr"></span>
						    <label id="pwd2_error"></label>
    						
					    </div>
				    </div>
			    </div>
			    <div class="item">
				    <span class="label">邮箱：</span>
				    <div class="fl">
					    <input type="text" id="mail" name="mail" class="text" tabindex="4">
					    <label id="mail_succeed" class="blank"></label>
					    <label class="ftx23">免费邮箱：</label><a target="_blank" href="http://login.mail.sohu.com/rapidReg/cooregister.jsp?form=360buy" class="flk13">搜狐</a> <a target="_blank" href="http://reg.email.163.com/mailregAll/reg0.jsp?from=360buy" class="flk13">网易</a>					
					    <span class="clr"></span>
					    <div id="mail_error"></div>
    					
				    </div>
			    </div>
			    <div class="item">
				    <span class="label">推荐人用户名：</span>
				    <div class="fl">
					    <input type="text" id="referrer" name="referrer" class="text" value="可不填" tabindex="5">
					    <label id="referrer_succeed" class="blank invisible"></label>
					    <span class="clr"></span>
					    <label id="referrer_error"></label>
    					
				    </div>
			    </div>
			    <div class="item">
				    <span class="label">验证码：</span>
				    <div class="fl">
					    <input type="text" id="authcode" name="authcode" class="text text-1" tabindex="6" autocomplete="off" maxlength="6">
					    <label class="img">
                          <img id="JD_Verification1" ver_colorofnoisepoint="#888888" onclick="this.src=&#39;JDVerification.aspx?&amp;uid=4447a953-ff80-4f7b-b57c-10db2074cdc8&amp;yys=&#39;+new Date().getTime()" src="https://passport.360buy.com/new/JDVerification.aspx?&uid=4447a953-ff80-4f7b-b57c-10db2074cdc8" alt="" style="cursor:pointer;width:100px;height:26px;">
					    </label>
					    <label class="ftx23">&nbsp;看不清？<a href="javascript:void(0)" onclick="verc()" class="flk13">换一张</a></label>
					    <label id="authcode_succeed" class="blank invisible"></label>
					    <span class="clr"></span>
					    <label id="authcode_error"></label>
                       
			    </div>
			    </div>
			    <div class="item">
				     <span class="label">&nbsp;</span>
				     <input type="button" class="btn-img btn-regist" id="registsubmit" value="同意以下协议，提交" tabindex="8">
			    </div>
    			
		    </div>
		
		</form>
<DIV id=guide>
<H5>还不是青旅商城用户？</H5>
<DIV class=content>现在免费注册成为青旅商城用户，便能立刻享受便宜又放心的环球之旅。</DIV><A 
class="btn-link btn-personal" href="javascript:regist();">注册新用户</A> 
</DIV><SPAN 
class=clr></SPAN></DIV></DIV>
</DIV>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="jdValidate.js"></script>
    <script type="text/javascript" src="jdValidate.personal.js"></script>

</body>
</html>
