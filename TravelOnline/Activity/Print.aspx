<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="TravelOnline.Activity.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
html, body, div, h1, h2, h3, h4, h5, h6, ul, ol, dl, li, dt, dd, p, blockquote, pre, form, fieldset, table, th, td {
	margin:0;
	padding:0;
}
.content{ width:756px; margin:0 auto;}
#tablePrint{ border-top:2px solid #000;border-left:2px solid #000;}
#tablePrint td{ border-right:2px solid #000;border-bottom:2px solid #000; font-family:"微软雅黑"; text-align:center}
h2{ text-align:center;}
div{ margin:5px 10px;}
    .style1
    {
        height: 180px;
    }
    .style2
    {
        height: 33px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
<p align="center"><img src="Images/logo.jpg"/></p>
<h2>参与活动确认单</h2>
<table width="756" border="0" align="center" cellpadding="0" cellspacing="0" id="tablePrint">
  <tr>
    <td width="17%"><div>姓名</div></td>
    <td width="34%"><div><asp:Label ID="lblGuestName" runat="server"></asp:Label></div></td>
    <td width="15%"><div>房号</div></td>
    <td width="34%"><div><asp:Label ID="lblRoomNoID" runat="server"></asp:Label></div></td>
  </tr>
  <tr>
    <td><div>参加活动名称</div></td>
    <td colspan="3"><div><asp:Label ID="lblActName" runat="server"></asp:Label></div></td>
  </tr>
    <tr>
    <td width="17%" class="style2"><div>活动时间</div></td>
    <td width="34%" class="style2"><div><asp:Label ID="lblActivityRunSTime" 
            runat="server"></asp:Label>~<asp:Label ID="lblActivityRunETime" 
            runat="server"></asp:Label></div></td>
    <td width="15%" class="style2"><div>活动地点</div></td>
    <td width="34%" class="style2"><div><asp:Label ID="lblPlace" runat="server"></asp:Label></div></td>
  </tr>
  <tr>
    <td colspan="4" style="text-align:left" class="style1">
    <div>
        <p class="MsoNormal">
            <span lang="EN-US">1. </span>
            <span style="font-family:宋体;
mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri">
            活动“欢天喜地蹦蹦跳”，将在皇家大道举办。为了宝贝们的安全，请各位家长及宝贝遵守活动现场的安全规定，避免推挤。让我们的宝贝们安全、快乐的开始邮轮之旅。</span><span 
                lang="EN-US"><o:p></o:p></span></p>
        <p class="MsoNormal">
            <span lang="EN-US">2. </span>
            <span style="font-family:宋体;
mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri">
            活动“童心协力”，感谢宝贝们捐献出自己心爱的物品，你的爱心会通过“慈善基金会”传递给许多需要帮助的小朋友。宝贝，请记得在</span><span 
                lang="EN-US">7</span><span style="font-family:宋体;mso-ascii-font-family:Calibri;
mso-hansi-font-family:Calibri">月</span><span lang="EN-US">19</span><span 
                style="font-family:宋体;mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri">日</span><span 
                lang="EN-US">18:00~20:00</span><span style="font-family:宋体;mso-ascii-font-family:
Calibri;mso-hansi-font-family:Calibri">将你捐献的物品交给上海青旅服务台的叔叔阿姨。（位于甲板第五层，船方服务柜台旁）。</span><span 
                lang="EN-US"><o:p></o:p></span></p>
        <p class="MsoNormal">
            <span lang="EN-US">3. </span>
            <span style="font-family:宋体;
mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri">
            活动“童言童语”，宝贝们，这次在邮轮上你可以把自己的心里话和金炜哥哥好好聊聊！记得可要说出你的小秘密哦。</span><span lang="EN-US"><o:p></o:p></span></p>
        <span lang="EN-US" style="font-size:10.5pt;mso-bidi-font-size:11.0pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:宋体;mso-bidi-font-family:&quot;Times New Roman&quot;;
mso-font-kerning:1.0pt;mso-ansi-language:EN-US;mso-fareast-language:ZH-CN;
mso-bidi-language:AR-SA">4. </span>
        <span style="font-size:10.5pt;mso-bidi-font-size:
11.0pt;font-family:宋体;mso-ascii-font-family:Calibri;mso-hansi-font-family:Calibri;
mso-bidi-font-family:&quot;Times New Roman&quot;;mso-font-kerning:1.0pt;mso-ansi-language:
EN-US;mso-fareast-language:ZH-CN;mso-bidi-language:AR-SA">
        活动“童心可嘉颁奖会”，奉献过爱心物品及拍卖成功的小宝贝们将被邀请参加“颁奖会”，“慈善基金会”将给宝贝们颁发很有意义的奖状！</span><p class="MsoNormal">
            &nbsp;</p>
        </div></td>
  </tr>
  </table>
    </form>
</body>
</html>
