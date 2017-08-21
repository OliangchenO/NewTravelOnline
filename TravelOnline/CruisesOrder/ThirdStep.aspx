<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThirdStep.aspx.cs" Inherits="TravelOnline.CruisesOrder.ThirdStep" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 在线预订</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
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
    <DIV id="inputs" style="DISPLAY:none">
        <input id="Nums" type="hidden" value="<%=Nums %>"/>
        <input id="Adults" type="hidden" value="<%=Adults %>"/>
        <input id="Childs" type="hidden" value="<%=Childs %>"/>
        <input id="TempOrderId" type="hidden" value="<%=OrderId %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>岸上观光</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step3" style="display:block;">
            <li class="view">选择价格 </li>
	        <li class="selects">录入名单</li>
	        <li class="book">岸上观光</li>
	        <li class="check">核对订单</li>
	        <li class="submit">成功提交</li>
        </ul>
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
<form id="form_data" onsubmit="return false;" method="post">
<%--    <div class="m detail" >
        <UL class=tab><LI class=curr>岸上观光<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div class=VisitList>
                
            </div>
        </div>
    </div>--%> 

    <%=VisitListInfo %>
    
<%--    <div class="m detail" >
        <UL class=tab><LI class=curr>济州岛岸上观光<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div class="VisitList Day1" tgs="济州岛岸上观光" tids="1">
                <table border="0" cellpadding="0" cellspacing="0" id="RoomSelectList" style="width: 100%;">
                    <tr class=tit1><td width="40%">观光线路</td><td width="5%">人数</td><td width="45%">参加人员</td><td width="10%">&nbsp;</td></tr>
                    <tr><td>111</td><td>4</td><td><span class=Visit_Span id="Visit_Span1"></span></td><td><A class=order href="javascript:void(0)" onclick="SelectGuest('1','0')">选择人员</A></td><td><input id="V_ID1" name="V_ID" type="hidden" value="1"/><input id="V_NO1" name="V_NO" type="hidden" value=""/><input id="V_NM1" name="V_NM" type="hidden" value=""/><input id="V_Nums1" name="V_Nums" type="hidden" value="4"/></td></tr>
                    <tr class="hide htr" id=h1><td colspan="5"><div tgs=1 tns="111" id=show1 class="CheckBoxList"><div><input type='checkbox' onclick='chkall(this,1)' name='chk_all' id='chk_all'>全选</div><div><input class="ChkIt" type=checkbox id="CB1-1" name=CheckBox1 tgs="1" onclick="SelectIts(this,1)" value="1"  />1</div><div><input class="ChkIt" type=checkbox id="CB1-2" name=CheckBox1 tgs="1" onclick="SelectIts(this,1)" value="2"  />1</div><div><input class="ChkIt" type=checkbox id="CB1-3" name=CheckBox1 tgs="1" onclick="SelectIts(this,1)" value="3"  />1</div><div><input class="ChkIt" type=checkbox id="CB1-4" name=CheckBox1 tgs="1" onclick="SelectIts(this,1)" value="4"  />1</div></div></td></tr>
                </table>
            </div>
        </div>
    </div>
    
    <div class="m detail" >
        <UL class=tab><LI class=curr>福冈岸上观光<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div class="VisitList Day3" tgs="福冈岸上观光" tids="3">
                <table border="0" cellpadding="0" cellspacing="0" id="RoomSelectList" style="width: 100%;">
                    <tr class=tit1><td width="40%">观光线路</td><td width="5%">人数</td><td width="44%">参加人员</td><td width="10%">&nbsp;</td><td width="1%">&nbsp;</td></tr>
                    <tr><td>dgsdfgdfsgdsfg</td><td>4</td><td><span class=Visit_Span id="Visit_Span4"></span></td><td><A class=order href="javascript:void(0)" onclick="SelectGuest('4','0')">选择人员</A></td><td><input id="V_ID4" name="V_ID" type="hidden" value="4"/><input id="V_NO4" name="V_NO" type="hidden" value=""/><input id="V_NM4" name="V_NM" type="hidden" value=""/><input id="V_Nums4" name="V_Nums" type="hidden" value="4"/></td></tr>
                    <tr class="hide htr" id=h4><td colspan="5"><div tgs=4 tns="dgsdfgdfsgdsfg" id=show4 class="CheckBoxList"><div><input type='checkbox' onclick='chkall(this,4)' name='chk_all' id='chk_all'>全选</div><div><input class="ChkIt" type=checkbox id="CB4-1" name=CheckBox4 tgs="1" onclick="SelectIts(this,4)" value="1"  />1</div><div><input class="ChkIt" type=checkbox id="CB4-2" name=CheckBox4 tgs="1" onclick="SelectIts(this,4)" value="2"  />1</div><div><input class="ChkIt" type=checkbox id="CB4-3" name=CheckBox4 tgs="1" onclick="SelectIts(this,4)" value="3"  />1</div><div><input class="ChkIt" type=checkbox id="CB4-4" name=CheckBox4 tgs="1" onclick="SelectIts(this,4)" value="4"  />1</div></div></td></tr>
                </table>
            </div>
        </div>
    </div>--%>

    <%--<div class="m detail hide">
        <UL class=tab><LI class=curr>用餐时间<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="Dinner_Time" class="mc tabcon">请选择晚餐时间：
                <%=dinnerstring %>
            </div>
        </div>
    </div>--%>
    <div class="gotonext">
         <A id=upstep class="btn-link btn-personal" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">下一步</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</form>
</DIV>
    <DIV class=clr></DIV>
    
    </DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/CruisesThirdStep.js"></script>
    <script type="text/javascript">
        function SubmitOrder() {
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            var url = "/CruisesOrder/AjaxService.aspx?action=ThirdStep&TempOrderId=" + $('#TempOrderId').val() + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                //alert(obj.success);
                if (obj.success) {
                    top.location = "/CruisesOrder/FourthStep/" + $('#TempOrderId').val() + ".html";
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(obj.error);
                }
            });
        }
    </script>
</body>
</html>
