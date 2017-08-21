<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePrint.aspx.cs" Inherits="TravelOnline.Travel.RoutePrint" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% =LineName%> - 行程打印</title>
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/boot.page.zp.js"></script>
    <style type="text/css">
	.title { FONT-WEIGHT: bold; FONT-SIZE: 16px }
	.text_14 { PADDING-TOP: 5px;FONT-WEIGHT: bold; FONT-SIZE: 13px }
	.text_12 { FONT-SIZE: 12px }
	.text_12px { PADDING-LEFT: 10px;FONT-SIZE: 12px }
	.t12px { LINE-HEIGHT: 18px; PADDING-LEFT: 10px;FONT-SIZE: 12px }
	.text_12bold { FONT-WEIGHT: bold; FONT-SIZE: 12px }
	.rtab { BORDER-TOP: #000000 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #000000 1px solid }
	.rctd { BORDER-RIGHT: #000000 1px solid; FONT-SIZE: 12px; LINE-HEIGHT: 18px; BORDER-BOTTOM: #000000 1px solid; TEXT-ALIGN: center }
	.rctd1 { BORDER-RIGHT: #000000 1px solid; FONT-SIZE: 12px; LINE-HEIGHT: 18px; BORDER-BOTTOM: #000000 1px solid; TEXT-ALIGN: left }
	.rctdtitle { BORDER-RIGHT: #000000 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 12px; LINE-HEIGHT: 18px; BORDER-BOTTOM: #000000 1px solid; TEXT-ALIGN: center }
	.btn-personal {LINE-HEIGHT: 35px; MARGIN: 20px auto; WIDTH: 137px; BACKGROUND: url(/images/bg_regist.jpg) no-repeat 0px -185px; HEIGHT: 35px; FONT-SIZE: 14px; FONT-WEIGHT: bold}
    A:link { COLOR: #333; TEXT-DECORATION: none}
    A:visited {COLOR: #333; TEXT-DECORATION: none}
    A:hover {COLOR: #c00; TEXT-DECORATION: underline}
    A:active { COLOR: #900}
    .rw {max-width:940px}
    .btn-link {MARGIN-RIGHT: 50px;TEXT-ALIGN: center; DISPLAY: inline-block; OVERFLOW: hidden}
    </style>
    <style media="print"> .Noprint { DISPLAY: none } .PageNext { PAGE-BREAK-AFTER: always } </style>
</head>
<body>
<div class="Noprint" style="margin-bottom:20px">
    <uc1:Header ID="Header1" runat="server" />
    <div id="menu">
        <div class="container" >
            <div class="row">
                <div class="span12" style="background:#01AA07;">
                    <uc4:menu ID="menu1" runat="server" />
                    <uc3:index_destination ID="index_destination1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container">
	<div class="row">
		<div class="span12">
            <div class="rw">
                <TABLE id="PrintA" cellSpacing="0" cellPadding="0" width="100%" align="center" bgColor="#ffffff" border="0">
                    <tr><td align="center" height="80" valign="middle"><IMG src="/Images/scytslogo.jpg" width=650 height=80><HR width="100%" SIZE="1"></td></tr>
                    <tr class="Noprint" ><td align="center"><A class="btn-link btn-personal" href="javascript:document.execCommand('print');void(0);">行程打印</A> <A class="btn-link btn-personal" href="../../Purchase/WordOutPut.aspx?Action=LineRoute&Cid=<%=Cid%>" target="_blank">行程下载</A> <A class="btn-link btn-personal" href="/line/<%=Cid%>.html">返回线路</A></td>
	                </tr>
	                <tr>
		                <td>
			                <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				                <TBODY>
					                <tr>
						                <td align="center" width="30"></td><td class="title" align="center" height="30"><% =LineName%></td>
						                <td align="center" width="30">
						                </td>
					                </tr>
				                </TBODY>
			                </TABLE>
		                </td>
	                </tr>
	                <tr><td class="text_14">【行程特色】</td></tr>
	                <tr><td class="text_12px"><P><% =RouteFeature%></P></td></tr>
	                <tr><td class="text_14">【行程安排】</td></tr>
	                <tr><td class="text_12"><P><% =RouteList%></P></td></tr>
	                <tr><td class="text_14">【服务内容】</td></tr>
	                <tr>
		                <td class="t12px">
			                <P><% =Traffic%>
			                <% =Hotel%>
			                <% =Foods%>
			                <% =Guide%>
			                <% =Insure%>
			                <% =Scenery%>
			                <% =Others%></P>
		                </td>
	                </tr>
                    <tr><td class="text_14">【报价包含】</td></tr>
	                <tr><td class="text_12px"><P><% =PriceIn%></P></td></tr>
                    <tr><td class="text_14">【报价不含】</td></tr>
	                <tr><td class="text_12px"><P><% =PriceOut%></P></td></tr>
	                <tr><td class="text_14">【自费项目】</td></tr>
	                <tr><td class="text_12px"><P><% =OwnExpense%></P></td></tr>
	                <tr><td class="text_14">【购物商店】</td></tr>
	                <tr><td class="text_12px"><P><% =Shopping%></P></td></tr>
	                <tr><td class="text_14">【注意事项】</td></tr>
	                <tr><td class="text_12px"><P><% =Attentions%></P></td></tr>
                </TABLE>
            </div>
        </div>
	</div>
</div>

<div class="Noprint">
    <uc2:Footer ID="Footer1" runat="server" />
</div>
</body>
</html>
