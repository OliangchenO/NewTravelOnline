<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="TravelOnline.Test.WebForm7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" Enabled="False">
        </asp:DropDownList>
      <input id="Text1" name="Text1" type="text" value="112233"/>

        <%--<DIV class=pleft>
            <div class=p-title_tj>特别推荐</div>
            <div class=p-img><A href="/product/401785.html" target=_blank><IMG src="/images/i1.jpg" width=150></A></div>
            <div class=p-names><A href="/product/401785.html" target=_blank>香港迪斯尼纯玩5天</A></div>
            <div class=p-price>青旅价：<STRONG>￥39.00</STRONG></div>
        
            <div class=p-list>
                <div class=p-title>日韩目的地</div>
                <div class=arealist>
                    <DL>
                        <DD>
                            <EM><A href="products/1713-3258-000.html">大阪</A></EM>
                            <EM><A href="products/1713-3258-000.html">釜山</A></EM>
                            <EM><A href="products/1713-3258-000.html">京都</A></EM>
                            <EM><A href="products/1713-3258-000.html">济州岛</A></EM>
                            <EM><A href="products/1713-3258-000.html">仁川</A></EM>
                            <EM><A href="products/1713-3258-000.html">北海道</A></EM>
                            <EM><A href="products/1713-3258-000.html">首尔</A></EM>
                            <EM><A href="products/1713-3258-000.html">名古屋</A></EM>
                            <EM><A href="products/1713-3258-000.html">东京</A></EM>
                            <EM><A href="products/1713-3258-000.html">箱根</A></EM>
                        </DD>
                    </DL>
                </div>
            </div>
        </DIV>
        
        <DIV class=pright>
        <div class=pright-list>
            <UL>
            <LI><DIV class=p-name>·<A href="news.aspx?id=3838" target=_blank>“欢度春节~新马4晚5日·全程四-五星”新航直飞</A></DIV><div class=pl-price>￥99139.00</div></LI>
            <LI><DIV class=p-name>·<A href="news.aspx?id=3834" target=_blank>“轻松游泰国”-曼谷芭堤雅5晚6日游纯玩团</A></DIV><div class=pl-price>￥1239.00</div></LI>
            </UL>
        </div>
        </DIV>--%>


        <asp:TextBox ID="TextBox1" runat="server" Height="57px" MaxLength="20" Rows="2"></asp:TextBox>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    

    </div>
    <ul>
    <li>11</li>
    <li>22</li>
    <li>33</li>
    <li>44</li>
    <li>55</li>
    </ul>

    <DIV style="BORDER-RIGHT: #6495ed 1px solid; BORDER-TOP: #6495ed 3px solid; BORDER-LEFT: #6495ed 1px solid; BORDER-BOTTOM: #6495ed 1px solid; width:400px;margin-left:22px;">
   <DIV id=headerDiv style="OVERFLOW: hidden; WIDTH: 400px">
      <TABLE cellSpacing=1 cellPadding=0 width="100%" bgColor=#d0e7ef border=0>
        <TBODY>
         <TR style="TEXT-ALIGN: left">
           <Td width=140 height=20><strong>Server/Factions</strong></Td>
           <Td width=135><strong>Price</strong></Td>
           <Td width=60><strong>Status</strong></Td>
           <Td width=65><strong>Buy</strong></Td>
         </TR>
        </TBODY>
       </TABLE>
      </DIV>
      <DIV style="OVERFLOW-y: auto;OVERFLOW-x: hidden; WIDTH: 400px; HEIGHT: 105px" onscroll="document.getElementById('headerDiv').scrollLeft = scrollLeft">
      <TABLE cellSpacing=1 cellPadding=0 width="100%" bgColor=#f9f9f9 border=0>
        <TBODY>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
<TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
    <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        <TR>
          <TD width=140 height=20>Server/Factions</TD>
          <TD width=150>PricePricePrice</TD>
          <TD width=55>Status</TD>
    <TD width=55>BuyBuy</TD>
        </TR>
        </TR>
        </TBODY>
        </TABLE>
        </DIV>
        </DIV>

        <div style="PADDING-RIGHT: 10px; 
             OVERFLOW-Y: auto; PADDING-LEFT: 10px;
             SCROLLBAR-FACE-COLOR: #ffffff; FONT-SIZE: 11pt; PADDING-BOTTOM: 0px; 
             SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; OVERFLOW: auto; WIDTH: 200px; 
             SCROLLBAR-SHADOW-COLOR: #919192; COLOR: blue; 
             SCROLLBAR-3DLIGHT-COLOR:#ffffff; LINE-HEIGHT: 100%; 
             SCROLLBAR-ARROW-COLOR: #919192; PADDING-TOP: 0px; 
             SCROLLBAR-TRACK-COLOR: #ffffff; FONT-FAMILY: 宋体; 
             SCROLLBAR-DARKSHADOW-COLOR: #ffffff; LETTER-SPACING: 1pt; HEIGHT: 500px; TEXT-ALIGN: left; background-repeat: no-repeat;">
             
             你需要的放置的内容
            </div>

            <div style="
     PADDING-LEFT: 36px;
     PADDING-BOTTOM: 0px;
     WIDTH: 526px; HEIGHT: 100px;
     color:#000000;
     margin-top:36px;
     /*---------滚动条样式开始------------*/
        /*OVERFLOW-Y: auto; 垂直滚动条，OVERFLOW-X: auto; 左右滚动条*/
     OVERFLOW: auto;
              SCROLLBAR-FACE-COLOR: #6D4110;  
              SCROLLBAR-HIGHLIGHT-COLOR: #3C140C;
              SCROLLBAR-SHADOW-COLOR: #3C140C;
              SCROLLBAR-3DLIGHT-COLOR:#3C140C; 
              SCROLLBAR-ARROW-COLOR: #919192; 
              SCROLLBAR-TRACK-COLOR: #3C140C;
              SCROLLBAR-DARKSHADOW-COLOR: #3C140C;
     /*---------滚动条样式结束------------*/
     ">
     信息内容信息内容信息内容信息内容信息内容信
   </div>
    </form>
<%--<SCRIPT> 
var imagepath="/images/weibo.jpg" 
var imagewidth=150 //这两行写图片的大小 
var imageheight=50 
var speed=2;
var imageclick = "http://weibo.com/wzotc " //这里写点击图片连接到的地址 
var hideafter=0 
var isie=0; 
if(window.navigator.appName=="Microsoft Internet Explorer"&&window.navigator.appVersion.substring(window.navigator.appVersion.indexOf("MSIE")+5,window.navigator.appVersion.indexOf("MSIE")+8)>=5.5) { 
isie=1; 
} 
else { 
isie=0; 
} 
if(isie){ 
var preloadit=new Image() 
preloadit.src=imagepath 
} 
function pop() { 
if(isie) { 
x=x+dx;y=y+dy; 
oPopup.show(x, y, imagewidth, imageheight); 
if(x+imagewidth+5>screen.width) dx=-dx; 
if(y+imageheight+5>screen.height) dy=-dy; 
if(x<0) dx=-dx; 
if(y<0) dy=-dy; 
startani=setTimeout("pop();",50); 
} 
} 
function dismisspopup(){ 
clearTimeout(startani) 
oPopup.hide() 
} 
function dowhat(){ 
if (imageclick=="dismiss") 
dismisspopup() 
else 
window.open(imageclick); 
} 
if(isie) { 
var x=0,y=0,dx=speed,dy=speed; 
var oPopup = window.createPopup(); 
var oPopupBody = oPopup.document.body; 
oPopupBody.style.cursor="hand" 
oPopupBody.innerHTML = '<IMG SRC="'+preloadit.src+'">'; 
oPopup.document.body.onmouseover=new Function("clearTimeout(startani)") 
oPopup.document.body.onmouseout=pop 
oPopup.document.body.onclick=dowhat 
pop(); 
if (hideafter>0) 
setTimeout("dismisspopup()",hideafter*1000) 
} 
</SCRIPT>--%>
</body>
</html>
