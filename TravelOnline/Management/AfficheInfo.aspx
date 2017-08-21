<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AfficheInfo.aspx.cs" Inherits="TravelOnline.Management.AfficheInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script charset="utf-8" src="/kindeditor/kindeditor.js"></script>
    <style>
        .inputdiv {PADDING-BOTTOM: 5px; MARGIN: 0px; PADDING-LEFT: 10px; PADDING-RIGHT: 0px; PADDING-TOP: 5px}
    </style>
    <script type="text/javascript">
        KE.show({
            id: 'editor_id',
            imageUploadJson: '../../asp.net/upload_json.ashx',
            filterMode : true,
            allowPreviewEmoticons: false,
            allowUpload: true,
            items: [
				'source', 'fullscreen', '|', 'fontname', 'fontsize', '|', 'textcolor', 'bgcolor', 'bold', 'italic', 'underline',
				'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
				'insertunorderedlist', '|', 'emoticons', 'image', 'link']
        });

        function resetit() {
            $("#AfficheName").val("");
            KE.html('editor_id', '');
        }
    </script>
</head>
<body>
<FORM  id=form1 onsubmit="return false;" method=post>
    <div class=inputdiv>
    版块：<select name="DropDownList1" id="DropDownList1" Width="80" >
    <%=options %>
    </select>&nbsp;
    标题：<input name="AfficheName" type="text" id="AfficheName" style="width:380px;" value="<%=AfficheName %>" maxlength="100" />
        <a id="SaveInfo" onclick="SaveAllInfo()" class="easyui-linkbutton" plain="true" iconCls="icon-save">保存</a>
    </div>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="Cid" name="Cid" type="hidden" value="<%=id %>"/>
        <input id="AfficheFlag" name="AfficheFlag" type="hidden" value="<%=AfficheFlag %>"/>
    </DIV>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG><%=InfoTitle %></STRONG></DIV>
    <textarea id="editor_id" name="content" style="width:100%;height:365px;">
    <%=AfficheContent%>
    </textarea>
    </DIV>
</FORM>
    <script type="text/javascript">
        setTimeout(function () { $("#DropDownList1").val("<%=AfficheType%>"); }, 1);

        function SaveAllInfo() {
            KE.sync('editor_id');
            //alert($("#form1").serialize());
            if ($("#AfficheName").val() == "" || $("#editor_id").val() == "") {
                jNotify('<strong>请输入标题和公告内容!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            $.ajax({
                type: "POST",
                url: "AfficheAjaxService.aspx",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                data: $("#form1").serialize(),
                error: function () {
                    jNotify('<strong>信息提交错误，保存失败!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                },
                success: function (result) {
                    if (result) {
                        var obj = eval(result);
                        if (obj.success == 0) {
                            jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                            if ($("#Cid").val() == "") {
                                resetit();
                            }
                        }
                        else {
                            jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                        }
                    }

                }
            });
            
        }
    </script> 
</body>
</html>

