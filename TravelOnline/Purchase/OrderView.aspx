<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderView.aspx.cs" Inherits="TravelOnline.Purchase.OrderView" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - <%=AutoId %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style> 
    #itimeslide{height:60px;position:relative;margin:0 10px;padding:15px 0}
    #itimeslide #date{display:none;position:absolute;left:0px;top:3px;width:100px}
    #itimeslide #date span{display:block;height:14px;padding:0 3px;background:#4e7db3;color:#fff;font-size:12px;line-height:14px}
    #itimeslide #date em{display:block;width:5px;height:3px;margin:0 auto;background:url(/images/20101224sprite.gif) no-repeat -61px 0}
    #itimeslide #timeline{overflow:visible;width:100%;height:2px;margin:0px 0 20px;background:#c7c7c7}
    #itimeslide #timeline li{display:block;position:absolute;left:0;top:8px;width:17px;height:17px;background:url(/images/20101224sprite.gif) no-repeat 0 0;text-indent:-999px;cursor:pointer}
    #itimeslide #timeline li.hover{background-position:-20px 0}
    #itimeslide span#titletop{display:none;position:absolute;top:29px;width:12px;height:8px;background:url(/images/20101224sprite.gif) no-repeat -88px -21px;z-index:1}
    #itimeslide #title{display:none;position:absolute;top:36px;padding:5px 5px;background:#f8f8ff;border:1px solid #708bab;border-radius:5px;-weblit-border-radius:5px;-moz-border-radius:5px;-webkit-box-shadow:3px 3px 3px #c7c7c7;
    -moz-box-shadow:3px 3px 3px #c7c7c7}
    .select {BORDER-LEFT: #e6e6e6 0px solid; BORDER-RIGHT: #e6e6e6 0px solid}
    </style>
</head>
<body id="none">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="TempOrderId" type="hidden" value="<%=OrderId %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep><%=AutoId %></SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
    </div>
    <DIV class=clr></DIV>
    <DIV class=left>
    <div id="pricebar"  style="width:150px;">
    <DIV id=mymenu class=m>
    <DIV class=mc style="BACKGROUND: #ffffff;">
    <DL tag="1">
    <DT tag="1">人数与价格<B></B></DT>
    <DD>
        <div class="package_pepleprise">
        <p>总价:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount"><%=AllPrice %></span></p>
        <div id="divshow"><p>人均:&nbsp;<span class="base_price02">&yen;</span><span class="base_price02" id="spanAve"><%=AvePrice %></span></p>
        <p>人数:&nbsp;<span class="base_price02" id="spanNums"><%=Nums %></span></p></div>
        </div>  
    </DD>
    </DL>
    <DL>
    <DT>热线电话<B></B></DT>
    <DD><div class="package_pepleprise"><span class="base_price03">4006-777-666</span></DIV></DD>
    </DL>
    </DIV></DIV></div></DIV>

 <div class="right-extra">
    <DIV class="m select">
        <DIV class=mt>
            <H1></H1><STRONG>订单状态</STRONG>
        </DIV>
        <div id="itimeslide"> 
		    <div id="date"></div> 
		    <div id="timeline"></div> 
		    <span id="titletop"></span> 
		    <div id="title"></div> 
	    </div> 
    </DIV>
    <div class="m detail <%=hide%>">
        <UL class=tab><LI class=curr>联系信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="checkinfo" class="mc tabcon"><%=User_Info %></div>
            <ul class=checks>
                <li class=memo><div class=oname>特别说明：</div><div class="oinfo"><%=User_Memo %></div></li>
            </ul>
        </div>
    </div>

    <%=CuisesList%>

    <div class="m detail">
        <UL class=tab><LI class=curr>费用明细<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <%=PriceList %>
        </div>
    </div>
    <%=DinnerInfo %>
    <div class="m detail <%=hide%>">
        <UL class=tab><LI class=curr>游客信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01"><div class="VisitList">
            <%=GuestList %>
        </div></div>
    </div>

    <div class="m detail <%=hide%> <%=hide1%>">
        <UL class=tab><LI class=curr>旅游合同<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order><%=Contract %></ul>  
        </div>
    </div>

    <div class="m detail <%=hide%> <%=hide1%>">
        <UL class=tab><LI class=curr>发票信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order><%=Invoice %></ul>  
        </div>
    </div>

    <div class="gotonext <%=PayHide %>">
         <A class="hide btn-link btn-personal" href="/Pay/PayNow.aspx?OrderId=<%=OrderId %>" target=_blank>立刻付款</A>
    </div>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/TimePoint.js"></script>
    <script type="text/javascript">
        <%=JSONData %>

        window.onscroll = function () {
            var top = "260";
            var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
            if (scrollTop > top) {
                $("#pricebar").attr({ "class": "package_ptfix" });
            } else {
                $("#pricebar").removeAttr("class");
            }
        };

        window.onload = function () {
            iTimePoint('itimeslide', 'date', 'timeline', 'titletop', 'title');
        }
    </script>
</body>
</html>




