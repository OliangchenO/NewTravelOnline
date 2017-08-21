<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManagerFooter.ascx.cs" Inherits="TravelOnline.Master.ManagerFooter" %>
<script src="../manage/hello.aspx?uid=<%=ucode %>" type="text/javascript"></script>
<%--<DIV class=w>
<DIV id=service>
<DIV class=clr></DIV>
<UL>
  <LI class=fore><A class=blink1 href="javascript:void(0)"></A></LI>
  <LI><A class=blink2 href="javascript:void(0)"></A></LI>
  <LI><A class=blink3 href="javascript:void(0)"></A></LI>
  <LI><A class=blink4 href="javascript:void(0)"></A></LI>
</UL>
</DIV></DIV>--%>

<DIV id=footer class=w>
<%--<DIV class=flinks>
<A href="/Service/AboutUs" target=_blank>关于我们</A>|
<A href="/Service/ContactUs" target=_blank>联系我们</A>|
<A href="/Service/JoinUs" target=_blank>人才招聘</A>|
<A href="#" target=_blank>同行分销</A>|
<A href="#" target=_blank>广告服务</A>|
<A href="#" target=_blank>服务终端</A>|
<A href="#" target=_blank>友情链接</A>|
<A href="#" target=_blank>销售联盟</A>
</DIV>--%>
<DIV 
class=copyright>沪ICP备案编号：05016600&nbsp;&nbsp;Copyright&copy;2006-2011&nbsp;&nbsp;上海中国青年旅行社&nbsp;版权所有</DIV>
<DIV class=ilinks><%--<A 
href="http://www.hd315.gov.cn/beian/view.asp?bianhao=111111" 
target=_blank><IMG alt=经营性网站备案中心 src="/Images/108_40_zZOKnl.gif" 
width=108 height=40></A>--%>
<SCRIPT type=text/JavaScript>    function change(eleId) { var str = document.getElementById(eleId).href; var str1 = str.substring(0, (str.length - 6)); str1 += RndNum(6); document.getElementById(eleId).href = str1; } function RndNum(k) { var rnd = ""; for (var i = 0; i < k; i++) { rnd += Math.floor(Math.random() * 10); } return rnd; }</SCRIPT>
 <A id=urlknet tabIndex=-1 
href="https://ss.cnnic.cn/verifyseal.dll?sn=2011042500100008043&ct=df&pa=340789" 
target=_blank><IMG oncontextmenu="return false;" onclick="change('urlknet')" 
border=true name=seal alt=可信网站 src="/Images/112_40_EAWZul.jpg" 
width=112 height=40></A></DIV></DIV>