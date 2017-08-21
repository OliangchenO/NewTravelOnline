<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="TravelOnline.Test.WebForm4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <SCRIPT src="/Scripts/jquery-1.6.min.js" type=text/javascript></SCRIPT>

    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js"></script>
<SCRIPT type=text/javascript>
    var testDG3, testDG5;
    $(function () {
        $('#btn10').dialog({ id: 'test10', page: 'http://www.qq.com', link: true, width: 800, height: 600, title: 'QQ首页' }); //dialog({ id: 'test14', page: 'ManageHome.aspx', title: '增加后台管理新用户', autoSize: true, skin: 'aero', iconTitle: false, rang: true, maxBtn: false, cover: true});
        $('#btn11').dialog({ id: 'test10', page: '/management/RightInfo.aspx', width: 800, height: 600, title: 'QQ首页' });
        $('#AddNew').dialog({ id: 'test10', page: 'http://www.qq.com', link: true, width: 800, height: 600, title: 'QQ首页' });
    });
    
	</SCRIPT>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <P><BUTTON class=runcode id=btn9>运行»</BUTTON></P>
  <LI>
  <H3>内容页参数为page且内容为外部链接qq.com，此时注意link参数一定要设为true</H3><PRE class=prettyprint>J('#btn10').dialog({ id:'test10', page:'http://www.qq.com', link:true, width:800, height:600, title:'QQ首页' });
</PRE>
  <P><BUTTON class=runcode id=btn10>运行»</BUTTON></P>
  <LI>
  <H3>内容页参数为html且html值为DOM对象</H3><PRE class=prettyprint>J('#btn11').dialog({ id:'test11', html:J('#obj')[0] });
</PRE>
  <P><BUTTON class=runcode id=btn11>运行»</BUTTON></P>
   <a id="AddNew" class="easyui-linkbutton" plain="true" iconCls="icon-add">新增</a>
    </div>
    </form>
</body>
</html>
