<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SortListNew.ascx.cs" Inherits="TravelOnline.Master.SortListNew" %>
<%--<DIV id=header class=w>
<DIV id=logo><A href="/"><IMG src="/Images/logo.gif" width=300 height=57 alt="上海青旅官网"></A></DIV>
<DIV id=nav>
<DIV id=tel400><IMG src="/Images/400800.gif" width=377 height=21></DIV>
<DIV id=nav-index><A 
href="/index.html">首页</A></DIV>

<DIV id=nav-extra>
<UL>
  <LI id=nav-pop class=fore><A 
  href="/OutBound.html">出境旅游</A><B></B></LI>
  <LI id=nav-tuan><A 
  href="/InLand.html">国内旅游</A></LI>
  <LI id=nav-auction><A 
  href="/FreeTour.html">自由行</A></LI>
  <LI id=nav-category><A 
  href="/Visa.html">单办签证</A><B></B></LI></UL>
<DIV class=corner></DIV></DIV></DIV>
<SPAN class=clr></SPAN>
<DIV id=o-search>
<DIV class=nosort>
</DIV>
<DIV id=search class=form>
<DIV id=i-search><INPUT 
onkeydown="javascript:if(event.keyCode==13) search('key');" id=key type=text 
autocomplete="off">
<UL id=tie class=hide></UL></DIV><INPUT id=btn-search onclick="search('key');return false;" value=搜&nbsp;索 type=submit></DIV>
<DIV id=hotwords></DIV>
<DIV id=mycart></DIV><SPAN 
class=clr></SPAN></DIV></DIV>
<SCRIPT type=text/javascript>
    function getRandomObj(A, R) {
        var x = 0;
        for (var i = 0; i < A.length; i++) {
            x += R[i] || 1;
            if (!R[i]) R.push(1)
        }
        var y = Math.ceil(Math.random() * x),
        z = [],
        m = [];
        for (var i = 1; i < x + 1; i++) {
            z.push(i)
        }
        for (var i = 0; i < A.length; i++) {
            m[i] = z.slice(0, R[i]);
            z.splice(0, R[i])
        }
        for (var i = 0; i < m.length; i++) {
            for (var j = 0; j < m[i].length; j++) {
                if (y == m[i][j]) {
                    return A[i]
                }
            }
        }
    }
</SCRIPT>--%>


