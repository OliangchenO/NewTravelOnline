<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="TravelOnline.Company.UserDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>船队信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .firstinput {text-align: right;width:100px;FLOAT: left;}
    </style>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="companyid" id="companyid" type="hidden" value="<%=companyid %>"/>
        <input name="deptid" id="deptid" type="hidden" value="<%=deptid %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" class="tools <%=hide %>" id="addnew" onclick="AddNew()"><img src="../images/icon/add.png" class=img20>新增</a>
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <div class=line_input>
        <div class=firstinput60>公司名称：</div>
        <input value="<%=company %>" class=sel type="text" name="company" id="company" maxlength="50" style="width: 300px"/>
    </div>
    <div class=line_input>
        <div class=firstinput60>部门：</div>
        <input value="<%=deptname %>" class=sel type="text" name="deptname" id="deptname" maxlength="50" style="width: 300px" readonly="readonly" />
    </div>
    <div class=line_input>
        <div class=firstinput60>用户姓名：</div>
        <input id="username" name="username" type="text" class="ipt" style="width: 160px;" maxlength="30" value="<%=username %>"/>&nbsp;&nbsp;
        畅游代码：<input id="misid" name="misid" type="text" class="ipt easyui-numberbox" precision="0" max="99999999" style="width: 80px;text-align:center;" maxlength="8" value="<%=misid %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput60>邮件地址：</div>
        <input id="useremail" name="useremail" type="text" class="ipt" style="width: 320px;" maxlength="30" value="<%=useremail %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput60>移动电话：</div>
        <input id="Mobile" name="mobile" type="text" class="ipt" style="width: 320px;" maxlength="11" value="<%=mobile %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput60>注意：</div>
        邮件地址为该用户的登陆账号，密码为移动电话的后六位，修改信息时，邮件地址、公司信息将不保存。
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        function AddNew() {
            $("#UserName").val("");
            $("#UserEmail").val("");
            $("#Mobile").val("");
        }

        $('#company').bind('keyup', function () {
            $("#companyid").val("");
            var url = "../Common/GetAutoDropList.aspx?action=CompanyInfo&SerchName=" + encodeURI(this.value);
            if (this.value.length > 2) show(this, "companyid", url, "");
        });

        $('#deptname').bind('click', function () {
            if ($("#companyid").val() == "") {
                jError('<strong>公司名称不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }
            var url = "../Common/GetAutoDropList.aspx?action=DeptInfo&SerchName=" + $("#companyid").val();
            //window.open(url);        
            show(this, "deptid", url, "");
        });

        function SaveInfo() {
            if ($("#companyid").val() == "" || $("#deptid").val() == "") {
                jError('<strong>公司名称和部门都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }

            if ($("#username").val() == "" || $("#useremail").val() == "" || $("#mobile").val() == "") {
                jNotify('<strong>用户姓名、邮件地址和移动电话都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var url = "AjaxService.aspx?action=UserInfo&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        $("#username").val("");
                        $("#useremail").val("");
                        $("#mobile").val("");
                        parent.$('#GridView_Serch_Button').click();
                    }
                    else {
                        parent.$('#GridView_Refresh_Button').click();
                    }
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }
    </script> 
</body>
</html>


