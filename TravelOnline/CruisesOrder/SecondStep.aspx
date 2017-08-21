<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondStep.aspx.cs" Inherits="TravelOnline.CruisesOrder.SecondStep" %>
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
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>    
    <script type="text/javascript" src="/Scripts/CheckIdcard.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <style>
        .iptchk {padding-top:10px;margin-top:15px;BORDER-top: #ECECEC 1px solid;}
    </style>
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
        <input id="AgeLimit" type="hidden" value="<%=AgeLimit %>"/>
        <input id="BeginDate" type="hidden" value="<%=BeginDate %>"/>
        <input id="VisitSell" type="VisitSell" value="<%=VisitSell %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>录入人员名单</SPAN>
           <p class="fontcolor02"><%=BeginDateInfo%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step2" style="display:block;">
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

<form id="DetailForm" name="DetailForm">
 <div class="right-extra">
    <div class="m detail">
        <UL class=tab><LI class=curr>联系人信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="titles" class="mc tabcon">请准确填写联系人信息(手机号码、E-mail)，以便我们与您联系。</div>
            <ul id=orderinfo class=order>
                <li><div class=oname><span class=xh>*</span>联系人：</div><div class=oinfo><input class=oinfodetail id="Order_Name" name="Order_Name" type="text" maxlength="30" style="width: 100px" value="<%=User_Name %>" dataType="Require" msg="联系人不能为空"/>&nbsp;<div class=hide><input type=checkbox name="SaveInfos" id="SaveInfos" <%=SaveInfo %> class="<%=hide2 %>"/>保存联系人信息</div></div></li>
                <li><div class=oname><span class=xh>*</span>手机号码：</div><div class=oinfo><input class=oinfodetail id="Order_Mobile" name="Order_Mobile" type="text"  maxlength="20" style="width: 200px" value="<%=User_Mobile %>" dataType="Mobile" msg="手机号码格式不正确" require="false"/>&nbsp;<span class=hs>手机号码和联系电话至少填写一项</span></div></li>
                <li><div class=oname><span class=xh></span>联系电话：</div><div class=oinfo><input class=oinfodetail id="Order_Tel" name="Order_Tel" type="text" maxlength="20" style="width: 200px" value="<%=User_Tel %>" dataType="Phone" msg="联系电话格式不正确" require="false"/>&nbsp;<span class=hs>示例：021-64330000</span></div></li>
                <li><div class=oname>传真号码：</div><div class=oinfo><input class=oinfodetail id="Order_Fax" name="Order_Fax" type="text"  maxlength="20" style="width: 200px" value="<%=User_Fax %>" dataType="Phone" msg="传真号码格式不正确" require="false"/>&nbsp;</div></li>
                <li><div class=oname><span class=xh>*</span>电子邮件：</div><div class=oinfo><input class=oinfodetail id="Order_Email" name="Order_Email" type="text"  maxlength="50" style="width: 200px" value="<%=User_Email %>" dataType="Email" msg="电子邮件不能为空或格式不正确"/>&nbsp;</div></li>
                <li class=memo><div class=oname>特别说明：</div><div class="oinfo memo"><textarea name="Order_Memo" rows="2" cols="20" id="Order_Memo" name="Order_Memo" style="width: 500px; height: 50px;" onkeydown="limitChars(this, 100)" onchange="limitChars(this, 100)" onpropertychange="limitChars(this, 100)"><%=User_Memo %></textarea>&nbsp;<span class=hs>100字以内</span></div></li>
            </ul>
        </div>
    </div>

    <div class="m detail" >
        <UL class=tab><LI class=curr>游客信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
        <div id="titles" class="mc tabcon">请详细填写每一位参加旅游的人员信息，每间舱房都必须至少有一位<%=AgeLimit%>周岁以上成人。</div>
        <%=GuestListInfo %>
<%--            <div class=guest>
                <ul id=guestinfo_1 class=order>
                    <li class=cur>第1位游客 <a class="icon_del" href="javascript:void(0);">清空填写内容</a></li>
                    <li><div class=oname><span class=xh>*</span>姓名：</div><div class=ginfo><input  type="text" class=Guest_Name maxlength="30" style="width: 150px" dataType="Require" msg="联系人不能为空"/>&nbsp;</div><div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style="width: 60px"><option value="M">男</option><option value="F">女</option></select>&nbsp;</div></li>
                    <li><div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType style="width: 155px"><option value="身份证">身份证</option><option value="护照">护照</option><option value="港澳通行证">港澳通行证</option><option value="台湾通行证">台湾通行证</option><option value="军官证">军官证</option><option value="回乡证">回乡证</option><option value="台胞证">台胞证</option><option value="国际海员证">国际海员证</option><option value="外国人永久居住证">外国人永久居住证</option><option value="其他">其他</option><option value="稍后提供">稍后提供</option></select>&nbsp;</div><div class=oname><span class=xh>*</span>证件号码：</div><div class=ginfo><input  type="text" class=Guest_IdNum maxlength="30" dataType="Require" msg="证件号码不能为空" style="width: 150px"/>&nbsp;</div></li>
                    <li><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel type="text"  maxlength="30" style="width: 150px"/>&nbsp;</div><div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type="text" maxlength="10" class=Guest_BirthDay style="width: 150px"/>&nbsp;</div></li>
                    <li class=hide><div class=oname><span class=xh>*</span>英文姓名：</div><div class=ginfo><input  type="text" class=Guest_EnName maxlength="30" style="width: 150px" />&nbsp;</div><div class=oname><span class=xh>*</span>证件有效期：</div><div class=ginfo><input type="text" maxlength="10" class=Guest_PassEnd style="width: 150px" />&nbsp;</div></li>
                    <li class=hide><div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style="width: 155px"><option value="P">因私护照(P)</option><option value="I">因公护照(I)</option><option value="D">外交护照(D)</option></select>&nbsp;</div><div class=oname><span class=xh>*</span>签发日期：</div><div class=ginfo><input type="text" maxlength="10" class=Guest_PassBgn style="width: 150px"/>&nbsp;</div></li>
                    <li class=hide><div class=oname><span class=xh>*</span>签发地：</div><div class=ginfo><input class=Guest_Sign type="text"  maxlength="30" style="width: 150px"/>&nbsp;</div><div class=oname><span class=xh>*</span>出生地：</div><div class=ginfo><input class=Guest_Home type="text" maxlength="30" style="width: 150px"/>&nbsp;</div></li>
                </ul>
            </div>--%>
        </div>
    </div>

    <div class="m detail <%=hide2%>">
        <UL class=tab><LI class=curr>旅游合同<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">旅游合同签约方式：
                <input id="Radio3" type="radio" name="ht" value="3" <%=RB3%> class="<%=rb_hide3%>"/><label for="Radio3" class="radiobtn <%=rb_hide3%>">在线签约</label>
                <input id="Radio1" type="radio" name="ht" value="1" <%=RB1%> class="<%=rb_hide1%>"/><label for="Radio1" class="radiobtn <%=rb_hide1%>">传真签约</label>
                <input id="Radio2" type="radio" name="ht" value="2" <%=RB2%> /><label for="Radio2" class="radiobtn ">快递签约</label>
                <%--<input id="Radio4" type="radio" name="ht" value="4" <%=RB4%>/><label for="Radio4" class=radiobtn>门店签约</label>--%>
            </div>
            <ul id="Ht_List" class=order>
                <li><div class=oname>合同范本：</div><div class=oinfo><%=HTurl %><span class=htspan id="Ht_Script">如果您选择了传真或快递签约方式，预订成功并在线支付后，可下载正式合同</span></div></li>
                <li><div class=oname>签约说明：</div><div class=oinfo>请将旅游合同打印、填写并签字盖章后，传真到：<%=FaxNumber %>，我们的客服人员会及时进行处理。</div></li>
                <li class=hide><div class=oname><span class=xh>*</span>您的地址：</div><div class=oinfo><input id="Ht_Address" type="text" maxlength="100" style="width: 500px" value="<%=User_Address %>"/>&nbsp;</div></li>
                <li class=hide><div class=oname>签约说明：</div><div class=oinfo>在线支付全额团费后，在我的订单中自动生成合同文本，请打印保存即可。</div></li>
                <%--
                <li class=hide><div class=oname>签约说明：</div><div class=oinfo>请将旅游合同打印、填写并签字盖章后，快递到：<%=CompanyAddress %> 电子商务部收，邮编：200031。</div></li>
                <li class=hide><div class=oname><span class=xh>*</span>您的地址：</div><div class=oinfo><input id="Ht_Address" type="text" maxlength="100" style="width: 500px" value="<%=User_Address %>"/>&nbsp;</div></li>
                <li class=hide><div class=oname>门店地址：</div><div class=oinfo>
                    <select id=Ht_Branch style="width: 500px">
                        <%=BranchOption %>
                    </select>&nbsp;
                </div></li>--%>
            </ul>
        </div>
    </div>

    <div class="m detail <%=hide2%>">
        <UL class=tab><LI class=curr>发票信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01" id="NeedInvoiceSelect"><input onclick="iNeedInvoice()" type=checkbox name="NeedInvoice" id="NeedInvoice"  />需要发票
        </div>
        <div class="mc tabcon borders01 <%=InvoiceShow%>" id="InvoiceInfo">
            <div id="fp_title" class="mc tabcon">发票领取方式：
                <input id="Radio5" type="radio" name="fp" value="1" <%=RC1%> class="<%=rb_hide1%>"/><label for="Radio5" class="radiobtn <%=rc_hide1%>">门店领取</label>
                <input id="Radio6" type="radio" name="fp" value="3" <%=RC2%>/><label for="Radio6" class="radiobtn ">快递发票</label>
            </div>
            <ul id="FP_List" class=order>
                <li><div class=oname>发票说明：</div><div class=oinfo>如果您选择了传真或快递签约方式，请留下您的地址、邮编等信息，预订成功并付款后旅游发票将快递给您</div></li>
                <li><div class=oname>发票抬头：</div><div class=oinfo><input id="Fp_Title" name="fp_title" type="text" maxlength="50" style="width: 500px" value="<%=FpInfo1 %>"/>&nbsp;</div></li>
                <li><div class=oname>发票内容：</div><div class=oinfo>
                <select id="Fp_Content" style="width: 300px"><option value="旅游费">旅游费</option></select>
                <input class=hide id="Fp_Content_text" name="Fp_Content_text" type="text" maxlength="50" style="width: 50px" value="<%=FpInfo2 %>"/>&nbsp;</div></li>
                <li class=hide><div class=oname>快递信息：</div><div class="oinfo memo"><textarea name="Fp_Kuaidi" rows="2" cols="20" id="Fp_Kuaidi" style="width: 500px; height: 40px;" onkeydown="limitChars(this, 100)" onchange="limitChars(this, 100)" onpropertychange="limitChars(this, 100)"><%=FpInfo3 %></textarea>&nbsp;<span class=hs>100字以内</span></div></li>
            </ul>       
        </div>
    </div>
    <div class="gotonext">
         <A id=upstep class="<%=hide1%>" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A id=nextstep class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">下一步</A><A id=islogin style="display: none;" class="btn-link btn-personal" href="javascript:void(0);" >正在提交，请稍候...</A>
    </div>
</DIV>
</form>
<form id="form_data" onsubmit="return false;" method="post">
    <input id="GuestInfo" name="GuestInfo" type="hidden" value=""/>
    <input id="OrderInfos" name="OrderInfos" type="hidden" value=""/>
    <input id="RoomListInfos" name="RoomListInfos" type="hidden" value=""/>
    <input id="PeopleListInfos" name="PeopleListInfos" type="hidden" value=""/>
    <input id="CruisesFlag" name="CruisesFlag" type="hidden" value="1"/>
</form>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/CruisesSecondStep.js"></script>
    <script type="text/javascript" src="/Scripts/Validator.js"></script>
    <SCRIPT type="text/javascript">var OrderJson = [<%=RoomOrder %>];</SCRIPT>
    <script type="text/javascript">
        //处理键盘事件   
        function doKey(e) {
            var ev = e || window.event; //获取event对象   
            var obj = ev.target || ev.srcElement; //获取事件源   
            var t = obj.type || obj.getAttribute('type'); //获取事件源类型   
            if (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea") {
                return false;
            }
        }
        //禁止后退键 作用于Firefox、Opera   
        document.onkeypress = doKey;
        //禁止后退键  作用于IE、Chrome
        document.onkeydown = doKey;

        function getMonthNum(date1, date2) {
            var d1 = date1.split("-"), d2 = date2.split("-");
            return (d2[0] - d1[0]) * 12 + (d2[1] - d1[1]) + 1;
        }

        function DateDiff(d1, d2) {
            var arr = d1.split("-");
            var endarr = d2.split("-");
            var defaultDate = new Date(arr[0], arr[1] - 1, arr[2]);
            var AgeDate = new Date(endarr[0], endarr[1] - 1, endarr[2]);
            if (defaultDate >= AgeDate) {
                return "1";
            }
            else {
                return "0";
            }
        }


        function SubmitOrder() {
            var RoomList = "";
            var PeopleList = "";
            var adults = 0;
            var childs = 0;
            var alladults = 0;
            var DoNext = "0";
            $(".guest .order .cur").each(function () {
                RoomList += $(this).attr("tgs") + "|";
                RoomList += $(this).children(".bedtype").val();
                RoomList += "@";

                var roomname = $(this).html();
                var lis = $(this).attr("id");
                adults = 0;
                childs = 0;
                $("." + lis).each(function () {
                    if (DateDiff($("#BeginDate").val(), $(this).val()) == "1") {
                        adults += 1;
                    }
                    else {
                        childs += 1;
                    }
                });
                if (adults == 0) {
                    DoNext = "1";
                    alert(roomname + "，必须有一位" + $("#AgeLimit").val() + "周岁以上成人");
                }
                alladults += adults;
                PeopleList += adults + "|" + childs;
                PeopleList += "@";
                if (DoNext == "1") return;
            });

            for (var i = 0; i < OrderJson.length; i++) {
                adults = 0;
                childs = 0;
                $(".RoomChk" + OrderJson[i].roomid).each(function () {
                    if (DateDiff($("#BeginDate").val(), $(this).val()) == "1") {
                        adults += 1;
                    }
                    else {
                        childs += 1;
                    }
                });
                if (adults != OrderJson[i].adult || childs != OrderJson[i].childs)
                {
                    DoNext = "1";
                    alert(OrderJson[i].roomname + "，您录入的 成人" + adults + "人 儿童" + childs + "人 和该房型预订的 成人" + OrderJson[i].adult + "人 儿童" + OrderJson[i].childs + "人 不符合，请检查");
                    return;
                }
            }

            if (DoNext == "1") return;
            if (Number(alladults) != Number($("#Adults").val()))
            {
                alert("您录入的成人数量 " + alladults + "人 和预定的成人数量 " + $("#Adults").val() + "人 不符合，请检查");
                return;
            }

            var OrderInfo = "";
            OrderInfo += $.trim($("#Order_Name").val()) + "@@";
            OrderInfo += $.trim($("#Order_Mobile").val()) + "@@";
            OrderInfo += $.trim($("#Order_Tel").val()) + "@@";
            OrderInfo += $.trim($("#Order_Fax").val()) + "@@";
            OrderInfo += $.trim($("#Order_Email").val()) + "@@";
            OrderInfo += $.trim($("#Order_Memo").val());

            var Parms = "";
            $(".GuestDiv").each(function () {
                var pid = "#" + $(this).attr("id");
                Parms += $(pid + " .Guest_Name").val().replace("中文姓名", "") + "@@";
                Parms += $(pid + " .Guest_EnName").val().replace("所选证件的拼音姓名", "") + "@@";
                Parms += $(pid + " .Guest_Sex").val() + "@@";
                Parms += $(pid + " .Guest_IdType").val() + "@@";
                Parms += $(pid + " .Guest_IdNum").val() + "@@";
                Parms += $(pid + " .Guest_BirthDay").val().replace("yyyy-mm-dd", "") + "@@";
                Parms += $(pid + " .Guest_PassType").val() + "@@";
                Parms += $(pid + " .Guest_PassBgn").val().replace("yyyy-mm-dd", "") + "@@";
                Parms += $(pid + " .Guest_PassEnd").val().replace("yyyy-mm-dd", "") + "@@";
                Parms += $(pid + " .Guest_Sign").val() + "@@";
                Parms += $(pid + " .Guest_Home").val() + "@@";
                Parms += $(pid + " .Guest_Tel").val() + "@@";
                Parms += $(pid + " .Guest_Allotid").val() + "@@";
                Parms += $(pid + " .Guest_Roomid").val() + "@@";
                Parms += $(pid + " .Guest_Listid").val();
                Parms += "||";
            });
            Parms = Parms.substr(0, Parms.length - 2);

            var HT = $("#ht_title :radio:checked").val();
            switch (HT) {
                case "1":
                    HT += "@@";
                    break;
                case "2":
                    HT += "@@" + $("#Ht_Address").val();
                    break;
                case "3":
                    HT += "@@";
                    break;
                case "4":
                    HT += "@@" + $("#Ht_Branch").val();
                    break;
                default:
                    HT = "1@@";
            }
            
            var FP = $("#fp_title :radio:checked").val();
            FP += "@@" + $("#Fp_Title").val();
            FP += "||" + $("#Fp_Content").val();
            FP += "||" + $("#Fp_Kuaidi").val();
            
            var SaveFlag = "0";
            if ($("#SaveInfos").prop("checked") == true) SaveFlag = "1";

            var NeedInvoice = "0";
            if ($("#NeedInvoice").prop("checked") == true) NeedInvoice = "1";

            $("#GuestInfo").val(Parms);
            $("#RoomListInfos").val(RoomList);
            $("#OrderInfos").val(OrderInfo);
            $("#PeopleListInfos").val(PeopleList);
            
            //alert($("#form_data").serialize()); //RoomListInfos  PeopleListInfos
            $("#islogin").show();
            $("#nextstep").hide();
            $("#upstep").hide();
            var url = "/Purchase/AjaxService.aspx?action=SecondStep&SaveFlag=" + SaveFlag + "&NFP=" + NeedInvoice + "&TempOrderId=" + $('#TempOrderId').val() + "&HT=" + escape(HT) + "&FP=" + escape(FP) + "&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    if ($("#VisitSell").val() == "1") {
                        top.location = "/CruisesOrder/ThirdStep/" + $('#TempOrderId').val() + ".html";
                     }
                    else {
                        top.location = "/CruisesOrder/FourthStep/" + $('#TempOrderId').val() + ".html";
                    }
                }
                else {
                    $("#islogin").hide();
                    $("#nextstep").show();
                    $("#upstep").show();
                    alert(obj.error);
                }
            });
        }

        function iNeedInvoice() {
            if ($("#NeedInvoice").prop("checked") == true) {
                $("#InvoiceInfo").show();
            }
            else {
                $("#InvoiceInfo").hide();
            }
        }
    </script>
</body>
</html>
