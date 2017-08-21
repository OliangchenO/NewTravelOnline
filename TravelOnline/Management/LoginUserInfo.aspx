<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUserInfo.aspx.cs" Inherits="TravelOnline.Management.LoginUserInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style>
        .select {WIDTH: 420px;}
        .select DL {WIDTH: 400px;}
        .select DT {WIDTH: 100px;}
        .select DD {PADDING-TOP: 3px;WIDTH: 300px;}
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>注册用户信息</STRONG></DIV>
    <DL class=fore><DT>登录用户名：</DT>
        <DD>
            <SPAN><%=UserEmail%></SPAN>
        </DD>
    </DL>
    <DL><DT>会员类型：</DT><DD><SPAN>普通用户</SPAN></DD></DL>
    <DL><DT>真实姓名：</DT><DD><SPAN><%=UserName%></SPAN></DD></DL>
    <DL><DT>性别：</DT><DD>
        <input id="Radio1" type="radio" name="sex" value="1" <%=Sex1%> /><label for="Radio1" class=radiobtn >男</label>
        <input id="Radio2" type="radio" name="sex" value="0" <%=Sex2%> /><label for="Radio2">女</label>
    </DD></DL>
    <DL><DT>出生日期：</DT><DD><%=UserBirthDay %></DD></DL>
    <DL><DT>单位名称：</DT><DD><%=Company %></DD></DL>
    <DL><DT>固定电话：</DT><DD><SPAN><%=Tel%></SPAN></DD></DL>
    <DL><DT>手机：</DT><DD><SPAN><%=Mobile%></SPAN></DD></DL>
    <DL><DT>联系地址：</DT><DD><SPAN><%=Address%></SPAN></DD></DL>
    <DL><DT>邮编：</DT><DD><SPAN><%=ZipCode%></SPAN></DD></DL>
    <DL><DT>婚姻状况：</DT><DD>
        <select name="marriage" class="sele" id="marriage" tabIndex=6 style="width: 110px">
            <option value="0">保密</option>
            <option value="1">未婚</option>
            <option value="2">已婚</option>
        </select>
    </DD></DL>
    <DL><DT>月收入情况：</DT><DD>
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
    </DD></DL>
    <DL><DT>职业类型：</DT><DD>
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
    </DD></DL>
    <DL><DT>兴趣爱好：</DT><DD><SPAN><%=Remark%></SPAN></DD></DL>

    </DIV>
    <script type="text/javascript" src="/Scripts/login.birthday.js"></script>
    <script type="text/javascript">
        setTimeout(function () { $("#career").val(<%=Career%>); }, 1); 
        setTimeout(function () { $("#marriage").val(<%=Marriage%>); }, 1); 
        setTimeout(function () { $("#income").val(<%=Income%>); }, 1); 
    </script>
</body>
</html>
