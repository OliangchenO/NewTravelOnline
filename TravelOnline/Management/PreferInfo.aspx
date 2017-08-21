<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferInfo.aspx.cs" Inherits="TravelOnline.Management.PreferInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.form.js"></script> 
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <style>
        .select {WIDTH: 400px;}
        .select DL {WIDTH: 400px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 300px;}
    </style>
    <style>
        .upp {WIDTH: 220px;}
        .width120 {WIDTH: 100px;}
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
        <script type="text/javascript">
            function myrefresh() {
                window.location.reload();
            }
	</script>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>优惠券详情</STRONG></DIV>
        <form id="form_data" onsubmit="return false;" method="post">
        <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=id %>"/>
        <input id="logourl" name="logourl" type="hidden" value="<%=logourl %>"/>
    </DIV>
    <div>发放说明：公共券号，请在发放类型后输入框填写券代码，邮轮领券，请在发放类型后输入框填写舱位分配的id，类型或产品id输入框填入线路的id</div>
    <DL class=fore><DT>发放类型：</DT>
        <DD>
            <select name="sellflag" id="sellflag" style="width:100px;">
	            <option value="1">赠送</option>
	            <option value="2">销售</option>
                <option value="3">公共券号</option>
                <option value="4">邮轮领券</option>
                <option value="5">邮轮销售</option>
            </select>
            <input id="pre_no" name="pre_no" type="text" style="width: 100px;" maxlength="20" value="<%=pre_no %>"/>
        </DD>
    </DL>
    <DL><DT>抵扣方式：</DT>
        <DD>
            <select name="deduction" id="deduction" style="width:200px;">
                <option value="1">按订单整单优惠</option>
	            <option value="2">按人数每人优惠</option>
                <option value="3">按舱房每间优惠</option>
            </select>
        </DD>
    </DL>
    <DL><DT>使用范围：</DT>
        <DD>
            <select name="range" id="range" style="width:200px;">
                <option value="1">全部产品</option>
	            <option value="2">出境游产品</option>
                <option value="3">国内游产品</option>
                <option value="4">单项服务产品</option>
                <option value="5">邮轮产品</option>
                <option value="8">按线路类型</option>
                <option value="9">按指定产品</option>
            </select>
        </DD>
    </DL>
    <DL><DT>类型或产品id：</DT>
        <DD>
            <input id="product" name="product" type="text" style="width: 240px;" maxlength="500" value="<%=product %>"/>
            <br>以半角（,）分隔，例如：,21,30,
        </DD>
    </DL>
    <DL><DT>使用日期：</DT>
        <DD>
            <input value="<%=begindate %>" class="iconDate" type="text" name="begindate" id="begindate" maxlength="10" style="width: 110px;" readonly="readonly"/>&nbsp;至&nbsp;
            <input value="<%=enddate %>" class="iconDate" type="text" name="enddate" id="enddate" maxlength="10" style="width: 110px;" readonly="readonly"/>
        </DD>
    </DL>
    <DL><DT>出团日期：</DT>
        <DD>
            <input value="<%=pbdate %>" class="iconDate" type="text" name="pbdate" id="pbdate" maxlength="10" style="width: 110px;" readonly="readonly"/>&nbsp;至&nbsp;
            <input value="<%=pedate %>" class="iconDate" type="text" name="pedate" id="pedate" maxlength="10" style="width: 110px;" readonly="readonly"/>
        </DD>
    </DL>
    <DL><DT>面值：</DT>
        <DD>
            <input id="par" name="par" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=par %>"/>&nbsp;&nbsp;
            销售价：<input id="sellprice" name="sellprice" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=sellprice %>"/>
        </DD>
    </DL>
    <DL><DT>消费额度：</DT>
        <DD>
            <input id="amount" name="amount" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=amount %>"/>
            达到额度方可使用，为0无限制</DD>
    </DL>
    <DL><DT>销售数量：</DT>
        <DD>
            <input id="sellnums" name="sellnums" type="text" class="easyui-numberbox" precision="0" max="999999" size="10" maxlength="10" style="text-align:center;" value="<%=sellnums %>"/>
            </DD>
    </DL>
    <DL><DT>使用说明：</DT>
        <DD>
            <textarea name="memo" id="memo" cols="" rows="" style="width: 240px;height:60px"><%=memo%></textarea>
        </DD>
    </DL>
   </form>
   <form id="UpLoadForm1" name="UpLoadForm1" method="post" enctype="multipart/form-data" action="/Utility/PicUploadHander.ashx?PathSet=Coupon&Thumb=55">
        <DL><DT>展示图片：</DT>
        <DD>
            <table border="0" cellpadding="0" cellspacing="0" class="upp">
                <tr>
                    <td  width="150px">
                        <img id="Img1" alt="展示图片 宽度500px以内" <%=ThumbSrc %>/>
                    </td>
                    <td width="60px" valign="middle">
                        <p class="bnP1" id="bnPs1">
                            <input onmouseover="javascript:$('#bnPs1').attr('class', 'bnP2');" onmouseout="javascript:$('#bnPs1').attr('class', 'bnP1');" type="file" id="Upload1" name="Upload1" onchange="checkit(this,'1')"/>
                        </p>         
                    </td>                
                </tr>
            </table>
        </DD>
    </DL>
    </form>
     <a id="A1" onclick="myrefresh()" class="easyui-linkbutton" plain="true" iconCls="icon-reload" style="margin-left: 120px;margin-top: 10px;">新增</a>
     <%=buttoninfo %>
    </DIV>
    <script type="text/javascript">
        var ImgObj1 = new Image();

        UpLoadFileCheck = function () {
            this.AllowExt = ".jpg,.gif"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
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
                        alert(html);
                        var result = html.replace("<pre>", "");
                        result = result.replace("</pre>", "");
                        var array = result.split(',');
                        var PicUrlAdd = array[0].replace('<PRE>', '');
                        $("#logourl").val(PicUrlAdd);
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
        $(function () {
            $('#begindate').calendar({ maxDate: '#enddate', btnBar: false });
            $('#enddate').calendar({ minDate: '#begindate', btnBar: false });
            $('#pbdate').calendar({ maxDate: '#pedate', btnBar: false });
            $('#pedate').calendar({ minDate: '#pbdate', btnBar: false });
            setTimeout(function () { $("#sellflag").val("<%=sellflag%>"); }, 1);
            setTimeout(function () { $("#deduction").val("<%=deduction%>"); }, 1);
            setTimeout(function () { $("#range").val("<%=range%>"); }, 1);
        
        });

        function check_null() {

//            if ($("#sellflag").val() == "3") {
//                if ($("#range").val() == "5" || $("#range").val() == "9") {
//                }
//                else {
//                    jNotify('<strong>公共券号，使用范围只能选邮轮产品或按指定产品两种</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
//                    return false;
//                }
//            }

            if ($("#range").val() == "8" || $("#range").val() == "9") {
                if ($("#product").val() == "") {
                    jNotify('<strong>您选择了按线路类型或产品使用，请输入线路类型或产品id</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    return false;
                }
            }

            if ($("#par").val() == "" || $("#sellprice").val() == "") {
                jNotify('<strong>面值和销售价均不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=SavePrefer&r=" + Math.random();
            $.post(url, $("#form_data").serialize(),function (data) {
                var obj = eval(data);
                if (obj.success) {
                    jSuccess('<strong>保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                if (obj.error) {
                    jError('<strong>' + obj.error + '!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }

        function savenums() {
            var url = "AjaxService.aspx?action=SavePreferNums&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
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
