<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesShipAdd.aspx.cs" Inherits="TravelOnline.Cruises.CruisesShipAdd" %>
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
        .select {WIDTH: 720px;}
        .select DL {WIDTH: 720px;}
        .select DT {WIDTH: 100px;}
        .select DD {WIDTH: 600px;}
    </style>
    <script type="text/javascript">
        function myrefresh() {
            window.location.reload();
        }
	</script>
</head>
<body>
    <SPAN class=clr></SPAN>
    <DIV class="main_input">
    <form id="form_data" onsubmit="return false;" method="post">
    <DIV id="inputs" style="DISPLAY:none">
        <input name="Cid" id="Cid" type="hidden" value="<%=Cid %>"/>
        <input name="comid" id="comid" type="hidden" value="<%=comid %>"/>
        <input name="series" id="series" type="hidden" value="<%=series %>"/>
        <input id="none" name="none" type="hidden" />
    </DIV>
    <div class=toolbar_inputa>
        <a href="javascript:void(0)" onclick="myrefresh()" class=tools><img src="../images/icon/add.png" class=img20>新增</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" onclick="SaveInfo()" class="tools" id="save"><img src="../images/icon/Spell.png" class=img20>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
        
    <div class=line_input>
        <div class=firstinput>船队名称：</div>
        <input class=ipt id="cname" name="cname" type="text" style="width: 562px;" maxlength="100" value="<%=cname %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>英文名称：</div>
        <input class=ipt id="ename" name="ename" type="text" style="width: 562px;" maxlength="100" value="<%=ename %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>船队系列：</div>
        <input value="<%=seriesname %>" class=sel type="text" name="seriesname" id="seriesname" maxlength="50" readonly="readonly" style="width: 130px"/>&nbsp;&nbsp;
        吨位：<input class=ipt id="tonnage" name="tonnage" type="text" style="width: 150px;" maxlength="50" value="<%=tonnage %>"/>&nbsp;&nbsp;
        船籍：<input class=ipt id="native" name="native" type="text" style="width: 150px;" maxlength="50" value="<%=native %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>载客量：</div>
        <input class=ipt id="capacity" name="capacity" type="text" style="width: 150px;" maxlength="50" value="<%=capacity %>"/>&nbsp;&nbsp;
        长度：<input class=ipt id="length" name="length" type="text" style="width: 150px;" maxlength="50" value="<%=length %>"/>&nbsp;&nbsp;
        宽度：<input class=ipt id="width" name="width" type="text" style="width: 150px;" maxlength="50" value="<%=width %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>吃水深度：</div>
        <input class=ipt id="waterline" name="waterline" type="text" style="width: 150px;" maxlength="50" value="<%=waterline %>"/>&nbsp;&nbsp;
        甲板楼层：<input id="deck" name="deck" type="text" class="ipt easyui-numberbox" precision="0" max="99" style="width: 126px;text-align:center;" maxlength="2" value="<%=deck %>"/>&nbsp;&nbsp;
        平均航速：<input class=ipt id="speed" name="speed" type="text" style="width: 126px;" maxlength="50" value="<%=speed %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>首航时间：</div>
        <input class=ipt id="firstseaway" name="firstseaway" type="text" style="width: 150px;" maxlength="50" value="<%=firstseaway %>"/>&nbsp;&nbsp;
        房间数量：<input id="rooms" name="rooms" type="text" class="ipt easyui-numberbox" precision="0" max="99999" style="width: 126px;text-align:center;" maxlength="5"  value="<%=rooms %>"/>&nbsp;&nbsp;
        电源电压：<input class=ipt id="voltage" name="voltage" type="text" style="width: 126px;" maxlength="50" value="<%=voltage %>"/>
    </div>
    <div class=line_input>
        <div class=firstinput>特色介绍：<BR>
		<IMG title="扩展输入框高度" onclick="AddIt('feature')" alt="" src="../../Images/idown.gif" align="absMiddle">&nbsp;&nbsp;&nbsp;&nbsp;<IMG title="还原输入框高度" onclick="DecIt('feature')" alt="" src="../../Images/iup.gif" align="absMiddle">&nbsp;&nbsp;</div>
        <textarea class=iptm name="feature" id="feature" style="width: 562px;height:60px"><%=feature%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>主要餐厅：</div>
        <textarea class=iptm name="restaurant" id="restaurant" style="width: 562px;height:60px"><%=restaurant%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>邮轮集锦：</div>
        <textarea class=iptm name="collection" id="collection" style="width: 562px;height:60px"><%=collection%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>会议设施：</div>
        <textarea class=iptm name="meeting" id="meeting" cols="" rows="" style="width: 562px;height:60px"><%=meeting%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>酒吧夜总会：</div>
        <textarea class=iptm name="bar" id="bar" cols="" rows="" style="width: 562px;height:60px"><%=bar%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>休闲娱乐：</div>
        <textarea class=iptm name="amusement" id="amusement" cols="" rows="" style="width: 562px;height:60px"><%=amusement%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>其他设施：</div>
        <textarea class=iptm name="others" id="others" cols="" rows="" style="width: 562px;height:60px"><%=others%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>免费项目：</div>
        <textarea class=iptm name="free" id="free" cols="" rows="" style="width: 562px;height:60px"><%=free%></textarea>
    </div>
    <div class=line_input>
        <div class=firstinput>收费项目：</div>
        <textarea class=iptm name="charge" id="charge" cols="" rows="" style="width: 562px;height:60px"><%=charge%></textarea>
    </div>
    </form>
    </DIV>
    <script type="text/javascript">
        $('#seriesname').bind('click', function () {
            var url = "../Common/GetAutoDropList.aspx?action=InitData&SerchName=SeriesType";
            //window.open(url);
            show(this, "series", url, "");
        });

        function AddIt(tname) {
            var this_obj = eval('document.all.' + tname);
            var h = this_obj.offsetHeight
            this_obj.style.height = h + 200;
        }

        function DecIt(tname) {
            var this_obj = eval('document.all.' + tname);
            this_obj.style.height = "60";
        }

        function SaveInfo() {
            if ($("#cname").val() == "" || $("#ename").val() == "") {
                jNotify('<strong>中文名称和英文名称都不能为空!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }

            var url = "AjaxService.aspx?action=CruisesShip&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#Cid").val() == "") {
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
