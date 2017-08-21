<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesPicAdd.aspx.cs" Inherits="TravelOnline.Cruises.CruisesPicAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公司信息</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/MyAuto.js"></script>
    <style>
        .upp {WIDTH: 300px;}
        .width120 {WIDTH: 250px;}
        .bnP1{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad1.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        .bnP2{
         width:60px;
         height:26px;
         line-height:26px;
         background:url(../images/UpLoad.jpg) no-repeat left top scroll transparent;
         float:left;
        }
        input[type='file'] {
         width:60px;
         height:26px;
         CURSOR: hand;
         line-height:26px;
         position:relative;
         opacity:0;                        /*设置它的透明度为0，即完全透明。这个语句，对付除IE以外的浏览器*/
         filter:alpha(opacity=0);    /*设置它的透明度为0，即完全透明。这个语句，对付IE浏览器。*/
        }
    </style>
</head>
<body>
<div class="page_input">
    <div class="main_input">
        <div class=toolbar_inputa>
            <a href="javascript:void(0)" class="tools <%=hide %>" id="addnew" onclick="myrefresh()"><img src="../images/icon/add.png" class=img20>新增</a>
            <a href="javascript:void(0)" class=tools id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>
        </div>
        <div class="clear"></div>
        <form id="form_data" onsubmit="return false;" method="post">
            <DIV id="inputs" style="DISPLAY:none">
                <input id="Cid" name="Cid" type="hidden" value="<%=Cid %>"/>
                <input id="shipid" name="shipid" type="hidden" value="<%=shipid %>"/>
                <input id="roomid" name="roomid" type="hidden" value="<%=roomid %>"/>
                <input id="picurl" name="picurl" type="hidden" value="<%=picurl %>"/>
            </DIV>
            <div class=line_input>
                <div class=firstinput>图片类型：</div><select id="pictype" name="pictype"  style="width: 150px">
                <option selected="selected" value="ship">邮轮外观</option>
                <option value="others">各项设施</option>
                <option value="deck">甲板</option>
                <option value="room">舱房</option>
                </select>&nbsp;&nbsp;
                甲板所在层数：<select id="deck" name="deck"  style="width: 50px">
                <option selected="selected" value="0"></option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
                <option value="6">6</option>
                <option value="7">7</option>
                <option value="8">8</option>
                <option value="9">9</option>
                <option value="10">10</option>
                <option value="11">11</option>
                <option value="12">12</option>
                <option value="13">13</option>
                <option value="14">14</option>
                <option value="15">15</option>
                <option value="16">16</option>
                <option value="17">17</option>
                <option value="18">18</option>
                <option value="19">19</option>
                <option value="20">20</option>
                </select>
            </div> 
            <div class=line_input>
                <div class=firstinput>图片名称：</div><input value="<%=cname %>" class=ipt type="text" name="cname" id="cname" maxlength="50"  style="width: 380px;" />
            </div>
            <div class=line_input>
                <div class=firstinput>房型关联：</div><input value="<%=roomtype %>" class=sel type="text" name="roomtype" id="roomtype" maxlength="50"  style="width: 360px;" readonly="readonly" />
            </div>
        </form>
        <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=Cruises&ChildPath=<%=shipid %>&Thumb=120">
        <div class=line_input>
            <div class=firstinput>图片上传：</div>
            <table border="0" cellpadding="0" cellspacing="0" class="upp">
                <tr>
                    <td  width="220px">
                        <img id="Img1" alt="推荐分辨率 800*600或以上" <%=ThumbSrc %>/>
                    </td>
                    <td width="60px" valign="middle">
                        <p class="bnP1" id="bnPs1">
                            <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                        </p>         
                    </td>                
                </tr>
            </table>
        </div>
        </form>
    </div>
</div>
<script type="text/javascript">
    var ImgObj1 = new Image();

    UpLoadFileCheck = function () {
        this.AllowExt = ".jpg,.gif,.png"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
    }

    UpLoadFileCheck.prototype.CheckExt = function (obj, flag) {
        this.FileExt = obj.value.substr(obj.value.lastIndexOf(".")).toLowerCase();
        if (this.AllowExt != 0 && this.AllowExt.indexOf(this.FileExt) == -1)//判断文件类型是否允许上传 
        {
            jNotify('<strong>该文件类型不允许上传，请上传jpg或gif图片</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return false;
        }
        else {
            $("#Img" + flag).attr('src', "/images/Please_wait.gif");
            $('#UpLoadForm' + flag).ajaxSubmit({
                success: function (html, status) {
                    var result = html.replace("<pre>", "");
                    result = result.replace("</pre>", "");
                    var array = result.split(',');
                    var PicUrlAdd = array[0].replace('<PRE>', '');
                    $("#picurl").val(PicUrlAdd);
                    var thumbPath = array[1].replace("//", "/").replace("</PRE>", "");
                    $("#Img" + flag).attr('src', thumbPath);
                    ImgObj1.src = PicUrlAdd;
                }
            });
        }
    }

    function checkpic() {
        alert('图片壹\n宽度: ' + ImgObj1.width + '\n高度: ' + ImgObj1.height);
    }

    function checkit(obj, flag) {
        var d = new UpLoadFileCheck();
        d.CheckExt(obj, flag)
    }

    function sleep(num) {
        var tempDate = new Date();
        var tempStr = "";
        var theXmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        while ((new Date() - tempDate) < num) {
            tempStr += "\n" + (new Date() - tempDate);
            try {
                theXmlHttp.open("get", "about:blank?JK=" + Math.random(), false);
                theXmlHttp.send();
            }
            catch (e) { ; }
        }
        return;
    }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#pictype").val("<%=pictype%>");
        $("#deck").val("<%=deck%>");
    });

    function myrefresh() {
        window.location.reload();
    }

    $('#roomtype').bind('click', function () {
        if ($("#pictype").val() != "room") {
            jError('<strong>只有图片类型为舱房的才可选择!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        var url = "../Common/GetAutoDropList.aspx?action=CruisesRoom&SerchName=" + $("#shipid").val();
        show(this, "roomid", url, "");
    });

    $('#serchit').click(function () {
        if ($("#pictype").val() != "room") {
            jError('<strong>只有图片类型为房间的才可选择!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        var obj = eval('document.all.roomtype');
        var url = "../Common/GetAutoDropList.aspx?action=HotelRoom&SerchName=" + $("#shipid").val();
        show(obj, "rid", url, "");
    });

    $("#save").click(function () {
        if ($("#pictype").val() == "deck") {
            if ($("#deck").val() == "0") {
                jError('<strong>甲板所在层数必须选择!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return;
            }
        }
        else {
            $("#deck").val("0");
        }

        if ($("#cname").val() == "") {
            jError('<strong>图片名称不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            $("#cname").focus();
            return;
        }


        if ($("#picurl").val() == "") {
            jError('<strong>您必须上传图片!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            return;
        }
        
        $(this).hide();
        $.post("AjaxService.aspx?action=CruisesPic&r=" + Math.random(), $("#form_data").serialize(),
            function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
                        parent.$('#GridView_Serch_Button').click();
                    }
                    else {
                        parent.$('#GridView_Refresh_Button').click();
                    }
                    jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        $(this).show();
    });

</script>
</body>
</html>
