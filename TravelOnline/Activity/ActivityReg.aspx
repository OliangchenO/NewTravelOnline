<%@ Page Language="C#" AutoEventWireup="true" codeBehind="ActivityReg.aspx.cs" Inherits="TravelOnline.ActivityReg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>活动注册页面</title>
    <script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>
</head>

<body>

<style>
html, body, div, h1, h2, h3, h4, h5, h6, ul, ol, dl, li, dt, dd, p, blockquote, pre, form, fieldset, table, th, td {
	margin:0;
	padding:0;
}
ul{list-style:none;}
.warp{ background:#025388 url(images/bbg.jpg) top center no-repeat scroll; height:900px;}
.content{ width:1280px; margin:0 auto;} 
/*.tijiao-form{ float:right; margin-top:430px; margin-right:150px;_margin-right:60px;}*/
.biaodan{
	 width:400px;
	 _width:350px;
	 border:#CCFF99 3px solid ;
	 border-radius:15px; padding:30px;
    /*margin-top:15%;*/
    background:#FFFFFF;
	box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);
	box-sizing: border-box;
	float:right; margin-top:430px; margin-right:150px; display:inline;
	}
.text_default{color: #999;}
#txbOrderNo, #txbPhone {
  width:300px;
  border:#CCCCCC 1px solid;
  outline: none;
  padding: 12px 5px;
  font-weight: 400;
  font-family: "微软雅黑";
  font-size:18px;
  background:#FFFFFF
 }
#txbOrderNo{
	border-radius:5px;
	/*border-radius:5px 5px 0 0;*/
}
#ddlGuestName{
	border-radius:5px;
	/*border-radius:0 0 5px 5px;*/
}
#txbPhone{
	border-radius:5px;
}
#btnCheck,#btnNext,#btnSave,#btnCancel,#btnPrint1,#btnPrint2{  
  font-family: 'Lato', sans-serif;
  font-weight: 400;
  background: #5f5fb4;
  background: -moz-linear-gradient(top, #5f5fb4 0%, #6666CC 100%);
  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #5f5fb4), color-stop(100%, #6666CC));
  background: -webkit-linear-gradient(top, #5f5fb4 0%, #6666CC 100%);
  background: -o-linear-gradient(top, #5f5fb4 0%, #6666CC 100%);
  background: -ms-linear-gradient(top, #5f5fb4 0%, #6666CC 100%);
  background: linear-gradient(to bottom, #5f5fb4 0%, #6666CC 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#5f5fb4', endColorstr='#6666CC',GradientType=0 );
  display: block;
  margin: 20px auto 0 auto;
 /* width:310px;*/
  border: none;
  border-radius: 3px;
  padding: 8px 0;
  font-size: 17px;
  color: #FFF;
  text-shadow: 0 1px 0 rgba(255, 255, 255, 0.45);
  font-weight: 700;
  box-shadow: 0 1px 3px 1px rgba(0, 0, 0, 0.17), 0 1px 0 rgba(255, 255, 255, 0.36) inset;}
#btnCheck:hover,#btnNext:hover,#btnSave:hover,#btnCancel:hover,#btnPrint1,##btnPrint2{
  background: #6d6df5;
  cursor:pointer;
}
#btnCheck:active,#btnNext:active,#btnSave:active,#btnCancel:active,#btnPrint1,#btnPrint2 {
  padding-top: 9px;
  padding-bottom: 7px;
  background: #7d7dff;
}
#tijiao{ display:none; font-size:18px; font-family:"微软雅黑"}
#btnCheck{width:310px }
#btnNext, #btnSave{ cursor:pointer; width:310px}
#btnSave{ height:36px;}
#btnCancel,#btnPrint2,#btnPrint1,#btnPrint2{height:36px;}
#tijiao td{ height:30px; font-size:14px;}
td h4{ color: #009 ; font-size:16px;}
td strong{ font-size:16px;}
.xize{width:600px; float:left; display:inline; margin-top:470px; margin-left:60px;  color:#FFFFFF;font-family:"微软雅黑"}
.buy{ font-size:26px; color:#F00; font-weight:bold; margin-top:6px; margin-left:-3px;}
.buy a { color:#FFFF99; text-decoration:none}
.buy a:hover{ color:#FF0000; text-decoration:underline;}
.rest{ margin-left:10px; color:#C00}

select{ display:none }
.select_box{font-family: "宋体"; font-size: 12px;color: #999999;width: 178px;position:relative;zoom:1;}
.select_showbox{
  border:#CCCCCC 1px solid;
  border-radius: 5px;
  padding: 12px 5px;
  width:300px;
  margin-left:11px;
  _margin-left:19px;
  font-weight: 400;
  font-family: "微软雅黑";
  font-size:18px;
  background:#FFFFFF;
  background: url(/Activity/Images/icon.png) no-repeat  top right;
  position:relative;
  }
.select_option{ background:#FFFFFF;border:#CCCCCC 1px solid;border-radius:0 0 5px 5px;border-top: none;display: none; position:absolute; width:310px; left:11px;_left:19px;}
.select_option li{width:300px;padding: 12px 5px;font-weight: 400;font-size:18px;font-family: "微软雅黑";}
.select_option li.selected{background: #F3F3F3;}
.select_option li.hover{background: #5f5fb4; color:#FFFFFF;outline:none;}
</style>
    <form id="form1" runat="server">
        <div class="warp">
    <div class="content">
        <div class="xize">
<h3>活动细则</h3>
<p>第一步：预订舱房，请注意，预订时必须输入儿童正确的出生年月日信息。预订完成后，将生成此订单的订单号，请记录下订单号码，用于活动报名页面。</p>
<p>第二步：预订完成后，登录活动报名页面，输入订单号及此订单预订时的预留手机号，选择儿童姓名后，选择意向报名的活动，4个限制名额的活动只能选其中一个，不能多选。选择完成后会有相关确认单可提供留存。</p>
<p>友情提示：参与““童心协力””的游客，在7月1日前需提供用于拍卖的物品，拍品须是儿童用品、玩具、文具等类型，价值在50~200元内。</p>
<p class="buy"><span ><a href="http://www.scyts.com/line/12916.html" target="_blank"><img src="images/buy.gif" alt="立即前往预订" width="142" height="47" border="0" /></a></span></p>
</div>

        <div class="biaodan">
            <div id="divCheck" runat="server">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td style="text-align:center; height:60px" colspan="2"><asp:TextBox ID="txbOrderNo" runat="server" class="text_default">订单号</asp:TextBox></td>
        </tr>
        <tr>
          <td style="text-align:center; height:60px" colspan="2"><asp:TextBox ID="txbPhone" runat="server" class="text_default">联系电话</asp:TextBox></td>
        </tr>
        <tr>
          <td style="height:60px;" visible="false" id="td" runat="server" colspan="2">
            <asp:DropDownList ID="ddlGuestName" runat="server" DataTextField="GuestName" 
                  DataValueField="GuestName" AutoPostBack="True" 
                  onselectedindexchanged="ddlGuestName_SelectedIndexChanged"></asp:DropDownList>
          </td>
        </tr>
        <tr>
        <td colspan="2"><asp:Label ID="lblCheckAlter" runat="server" ForeColor="Red"></asp:Label>
              <br />
              <asp:Label ID="lblActivityName" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td style="">
              
              <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                  Text="取消改报名" Visible="False" Width="140px" />
                  
          </td>
          <td><asp:Button ID="btnPrint1" runat="server" Text="打印凭证" Visible="False" 
                  Width="140px" onclick="btnPrint1_Click1"/></td>
        </tr>
        <tr>
          <td style="text-align:center; height:30px;" colspan="2">
              <asp:Button ID="btnCheck" runat="server" Text="验 证" onclick="btnCheck_Click" />
              <asp:Button ID="btnNext" runat="server" Text="下 一 步" onclick="btnNext_Click" Visible="False" />
          </td>
        </tr>
      </table>
    </div>

            <div id="divSave" runat="server" Visible="False">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td colspan="2"><h4>请选择需要参加的活动。名额有限，每位小朋友只可参加一项活动哦O(∩_∩)O~</h4></td>
        </tr>
        <tr>
          <td style="border-bottom:1px solid #CCC" class="style1" colspan="2"><strong>姓名：</strong><asp:Label 
                  ID="lblGuestName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
          <td>
              <asp:RadioButtonList ID="rblActivityInfo" runat="server" 
                  DataTextField="ActivityName" DataValueField="ActInfoMain_ID" DataMember="0" 
                  Width="100%">
                             </asp:RadioButtonList>
          </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="lblSaveAlter" runat="server" ForeColor="Red"></asp:Label>
        </td>
        </tr>
        <tr>
          <td>
              <asp:Label ID="lblActivityID" runat="server" Visible="False"></asp:Label>
              <asp:Label ID="lblGuestID" runat="server" Visible="False"></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="提 交"  onclick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnPrint2" runat="server" Text="打印凭证" Visible="False" 
                  onclick="btnPrint1_Click1" />
          <td>
        </tr>
      </table>
    </div>
  </div>

        <div id="div_print" style="display:none">

<div class="content">
<h2>7月19号海洋航行者号邮轮亲子活动确认单确认单</h2>
<table width="756" border="0" align="center" cellpadding="0" cellspacing="0" id="table1" style="border-top:2px solid #000;border-left:2px solid #000;">
  <tr>
    <td width="17%"><div>姓名</div></td>
    <td width="32%"><div>
        <asp:Label ID="lblPrintName" runat="server" Text=""></asp:Label></div></td>
    <td width="15%"><div>房号</div></td>
    <td width="36%"><div>
        <asp:Label ID="lblPringRoomNumber" runat="server" Text=""></asp:Label></div></td>
  </tr>
  <tr>
    <td><div>
        参加活动名称</div></td>
    <td colspan="3"><div>
        <asp:Label ID="lblPrintActivityName" runat="server" Text=""></asp:Label></div></td>
  </tr>
  <tr>
    <td width="17%"><div>活动时间</div></td>
    <td width="32%"><div>这里活动时间</div></td>
    <td width="15%"><div>活动地点</div></td>
    <td width="36%"><div>这里活动地点</div></td>
  </tr>
  <tr>
    <td colspan="4" style="text-align:left">
    <div>
      <p>活动注意事项</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
      <p>&nbsp;</p>
    </div></td>
  </tr>
  </table>
</div>

</div>
</div>
</div>

        <script type="text/javascript">
            $(function () {
                $("#txbOrderNo,#txbPhone").focus(function () {
                    if ($(this).val() == this.defaultValue) {
                        $(this).val("");
                        $(this).removeClass("text_default");
                    };

                }).blur(function () {
                    if ($(this).val() == '') {
                        $(this).val(this.defaultValue);
                        $(this).addClass("text_default");
                    };

                });

                $("#chaxun").click(function () {
                    $("#information").hide();
                    $("#tijiao").fadeIn();
                    return false;
                });
                $("#prev").click(function () {
                    $("#information").fadeIn();
                    $("#tijiao").hide();
                    return false;
                });
            })
</script>
        
        <script type="javascript">
        function printdiv(printpage) {
            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
</script>

        <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="js/jquery.select.js"></script>
    </form>
</body>
</html>
