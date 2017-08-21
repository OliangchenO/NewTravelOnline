<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="TravelOnline.Users.UserInfo" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心 - 修改资料</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body id="none">

    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <DIV class="w main">

        <DIV class=left>
            <uc4:UserCenterMenu ID="UserCenterMenu1" runat="server" />
        </DIV>

<div class="right-extra">
    <DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A href="/users/UserHome.aspx">会员中心</A>&nbsp;&gt;&nbsp;<SPAN>修改资料</SPAN></DIV>
    <DIV class="m select"><DIV class=mt><H1></H1><STRONG>修改您的账户详细信息</STRONG></DIV></DIV>

    <FORM  id=formlogin onsubmit="return false;" method=post>
        <DIV id=explain class="m m1">
        <DIV class=o-mt></DIV>
        <DIV class=mc>
        <DIV id=regist class=w>
        <DIV class=form>

        <DIV class=item>
            <SPAN class=label>登录用户名：</SPAN> 
            <DIV class=fl>
                <SPAN class=labela><%=UserEmail%></SPAN> 
            </DIV>
        </DIV>
        <%=UserInfos %>
        <DIV class=item>
            <SPAN class=label><em>*</em>真实姓名：</SPAN> 
            <DIV class=fl>
                <INPUT type="text" id=truename class=text tabIndex=1 name=truename value="<%=UserName%>"> 
                <LABEL id=truename_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN>
                <LABEL id=truename_error></LABEL>
            </DIV>
        </DIV>

        <DIV class=item>
            <SPAN class=label>性别：</SPAN> 
            <DIV class=fl>
                <input id="Radio1" type="radio" name="sex" value="1" <%=Sex1%> /><label for="Radio1" class=radiobtn >男</label>
                <input id="Radio2" type="radio" name="sex" value="0" <%=Sex2%> /><label for="Radio2">女</label>
            </DIV>
        </DIV>

        <DIV class=item><SPAN class=label>出生日期：</SPAN> 
            <DIV class=fl>
                <input id="UserBirthday" type="hidden" value="<%=UserBirthDay %>" />
                <select name="birthdayYear" class="sele" id="birthdayYear" style="width: 60px"></select><label class=radiobtn > 年</label>
                <select name="birthdayMonth" class="sele" id="birthdayMonth" style="width: 40px"></select><label class=radiobtn > 月</label>
                <select name="birthdayDay" class="sele" id="birthdayDay" style="width: 40px"></select><label class=radiobtn > 日</label>
            </DIV>
        </DIV>   


        <DIV class=item>
            <SPAN class=label><em>*</em>固定电话：</SPAN> 
            <DIV class=fl>
                <INPUT id=tel type="text" class=text maxLength=20 tabIndex=2 name=tel value="<%=Tel%>">
                <LABEL id=tel_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=tel_error></LABEL>
            </DIV>
        </DIV>

        <DIV class=item>
            <SPAN class=label><em>*</em>手机：</SPAN> 
            <DIV class=fl>
                <INPUT id=mobile type="text" class=text maxLength=11 tabIndex=3 name=mobile value="<%=Mobile%>"> 
                <LABEL id=mobile_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=mobile_error></LABEL>
            </DIV>
        </DIV>

        <DIV class=item>
            <SPAN class=label><em>*</em>联系地址：</SPAN> 
            <DIV class=fl>
                <INPUT id=address type="text" class=text tabIndex=4 name=address value="<%=Address%>" maxlength="100"> 
                <LABEL id=address_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=address_error></LABEL>
            </DIV>
        </DIV>

        <DIV class=item>
            <SPAN class=label><em>*</em>邮编：</SPAN> 
            <DIV class=fl>
                <INPUT id=zipcode type="text" class="text text-1" maxLength=6 tabIndex=5 name=zipcode value="<%=ZipCode%>"> 
                <LABEL id=zipcode_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=zipcode_error></LABEL>
            </DIV>
        </DIV>

        <DIV class=item><SPAN class=label>婚姻状况：</SPAN> 
            <DIV class=fl>
                <select name="marriage" class="sele" id="marriage" tabIndex=6 style="width: 110px">
                    <option value="0">保密</option>
                    <option value="1">未婚</option>
                    <option value="2">已婚</option>
                </select>
            </DIV>
        </DIV> 

        <DIV class=item><SPAN class=label>月收入情况：</SPAN> 
            <DIV class=fl>
                <select name="income" class="sele" id="income" tabIndex=7 style="width: 110px">
                    <option value="0">保密</option>
					<option value="1">2000元以下</option>
					<option value="2">2000-3999元</option>
					<option value="3">4000-5999元</option>
					<option value="4">6000-7999元</option>
					<option value="5">8000-9999元</option>
                    <option value="6">10000以上</option>
                    <option value="7">20000以上</option>
                    <option value="8">50000以上</option>
                </select>
            </DIV>
        </DIV> 

        <DIV class=item><SPAN class=label>职业类型：</SPAN> 
            <DIV class=fl>
                <select name="career" class="sele" id="career" tabIndex=8 style="width: 200px">
                    <option value="0">保密</option>
					<option value="1">计算机、互联网、通信</option>
                    <option value="2">销售、市场、广告</option>
                    <option value="3">财务、审计、统计</option>
                    <option value="4">商贸、金融业</option>
                    <option value="5">生产、制造、营运</option>
                    <option value="6">职员、管理人员</option>
                    <option value="7">自由职业者、企业主</option>
                    <option value="8">教育、培训</option>
                    <option value="9">政府机关</option>
                    <option value="10">旅游、酒店、景区</option>
                    <option value="11">学生</option>
                    <option value="12">退休</option>
                    <option value="13">其他</option>
                </select>
            </DIV>
        </DIV>

        <div class="item1" style="height: 80px">
			<span class="label">兴趣爱好：</span>
			<div class="fl">
				<textarea name="remark" id="remark" cols="" rows="" tabIndex=9 style="width: 300px;height:60px"><%=Remark%></textarea>
                <LABEL id=remark_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=remark_error></LABEL>
            </div>
		</div>

       <%-- <DIV class=item>
            <SPAN class=label>验证码：</SPAN> 
            <DIV class=fl>
                <INPUT id=authcode class="text text-1" tabIndex=10 maxLength=4 name=authcode autocomplete="off">
                <LABEL id=authcode_succeed class="blank"></LABEL>
                <LABEL class=img>
                    <IMG style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" id=JD_Verification1 onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" alt="" src="/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" > 
                </LABEL>
                <LABEL class=ftx23>&nbsp;看不清？<A class=flk13 onclick="verc()" href="###">换一张</A></LABEL> 
                <SPAN class=clr></SPAN><LABEL id=authcode_error></LABEL>
            </DIV>
        </DIV>--%>
        <div class="item" id="o-authcode"> 
	        <span class="label">验证码：</span> 
	        <div class="fl"> 
		        <input type="text" id="authcode" name="authcode" class="text text-1" tabindex="10" sta="0" maxlength="6"> 
		        <label id="authcode_succeed" class="blank"></label> 
		        <label class="img"> 
                <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="/login/VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
		        </label> 
		        <label class="ftx23">&nbsp;看不清？<a href="###" onclick="verc()" class="flk13">换一张</a></label> 
		        <span class="clr"></span> 
		        <label id="authcode_error" class="null"></label> 
	        </div> 
        </div> 

        <DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=loginsubmit class="btn-img btn-entry" tabIndex=11 value=修改信息 type=button>  
        </DIV>
        </DIV></DIV>
        </DIV></DIV><!--explain end-->

    </FORM><!--form end-->
</DIV><!--right-extra end-->

<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.EditInfos.js"></script>
    <script type="text/javascript" src="/Scripts/login.birthday.js"></script>
    <script type="text/javascript">
        setTimeout(function () { $("#career").val(<%=Career%>); }, 1); 
        setTimeout(function () { $("#marriage").val(<%=Marriage%>); }, 1); 
        setTimeout(function () { $("#income").val(<%=Income%>); }, 1); 
    </script>

</body>
</html>
