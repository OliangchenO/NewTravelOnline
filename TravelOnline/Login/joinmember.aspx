<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="joinmember.aspx.cs" Inherits="TravelOnline.Login.joinmember" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心 - 加入积分会员</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script>
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
    <DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A href="/users/UserHome.aspx">会员中心</A>&nbsp;&gt;&nbsp;<SPAN>加入积分会员</SPAN></DIV>
    <DIV class="m select"><DIV class=mt><H1></H1><STRONG>请填写您的真实信息</STRONG></DIV></DIV>

    <FORM  id=formlogin onsubmit="return false;" method=post>
        <DIV id=explain class="m m1">
        <DIV class=o-mt></DIV>
        <DIV class=mc>
        <DIV id=regist class=w>
        <DIV class=form>
        <div class="<%=buttonhide %>">
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
                <input id="email" name="email" type="hidden" value="<%=UserEmail%>" />
                <select name="birthdayYear" class="sele" id="birthdayYear" style="width: 60px"></select><label class=radiobtn > 年</label>
                <select name="birthdayMonth" class="sele" id="birthdayMonth" style="width: 40px"></select><label class=radiobtn > 月</label>
                <select name="birthdayDay" class="sele" id="birthdayDay" style="width: 40px"></select><label class=radiobtn > 日</label>
            </DIV>
        </DIV>
        <DIV class=item>
            <SPAN class=label><em>*</em>手机：</SPAN> 
            <DIV class=fl>
                <INPUT id=mobile type="text" class=text maxLength=11 tabIndex=3 name=mobile value="<%=Mobile%>"> <INPUT id="Button1" name="Button1" class="<%=buttonhide %>" tabIndex=4 value=发送验证码 type=button>
                <LABEL id=mobile_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=mobile_error></LABEL>
            </DIV>
        </DIV>
        <DIV class=item>
            <SPAN class=label><em>*</em>联系地址：</SPAN> 
            <DIV class=fl>
                <INPUT id=address type="text" class=text tabIndex=5 name=address value="<%=Address%>" maxlength="100"> 
                <LABEL id=address_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=address_error></LABEL>
            </DIV>
        </DIV>
        <DIV class=item>
            <SPAN class=label><em>*</em>验证码：</SPAN> 
            <DIV class=fl>
                <INPUT id=authcode type="text" class="text text-1" tabIndex=6 name=authcode value="" maxlength="6"> 
                <LABEL id=authcode_succeed class="blank invisible"></LABEL>
                <SPAN class=clr></SPAN><LABEL id=authcode_error></LABEL>
            </DIV>
        </DIV>
        </div>
        <DIV class=item><SPAN class=label>&nbsp;</SPAN><DIV class=fl><INPUT id="loginsubmit" class="btn-img btn-entry <%=buttonhide %>" tabIndex="11" value="加入积分会员" type="button"></DIV></DIV>
        <DIV class=item><SPAN style="padding-left:50px;LINE-HEIGHT: 25px; color: #009900; font-size: 24px; font-weight: bold;"><%=infos %></SPAN></DIV>
        </DIV></DIV>
        </DIV></DIV><!--explain end-->

    </FORM><!--form end-->
</DIV><!--right-extra end-->

<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.JoinInfos.js"></script>
    <script type="text/javascript" src="/Scripts/login.birthday.js"></script>
    <script type="text/javascript">

    </script>

</body>
</html>
