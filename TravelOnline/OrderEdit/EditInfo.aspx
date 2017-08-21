<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInfo.aspx.cs" Inherits="TravelOnline.OrderEdit.EditInfo" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - 在线预订</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>    
    <script type="text/javascript" src="/Scripts/CheckIdcard.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
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
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep>填写信息</SPAN>
           <p class="fontcolor02"><%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
        <ul class="base_step base_step3" style="display:block;">
            <li class="view">选择线路 </li>
	        <li class="selects">选择价格</li>
	        <li class="book">填写信息</li>
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
    </DL></DIV></DIV></div></DIV>

<form id="DetailForm" name="DetailForm">
 <div class="right-extra">
    <div class="m detail">
        <UL class=tab><LI class=curr>联系人信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="titles" class="mc tabcon">请准确填写联系人信息(手机号码、E-mail)，以便我们与您联系。</div>
            <ul id=orderinfo class=order>
                <li><div class=oname><span class=xh>*</span>联系人：</div><div class=oinfo><input id="Order_Name" type="text" maxlength="30" style="width: 100px" value="<%=User_Name %>" dataType="Require" msg="联系人不能为空"/>&nbsp;</div></li>
                <li><div class=oname><span class=xh>*</span>手机号码：</div><div class=oinfo><input id="Order_Mobile" type="text"  maxlength="20" style="width: 200px" value="<%=User_Mobile %>" dataType="Mobile" msg="手机号码格式不正确" require="false"/>&nbsp;<span class=hs>手机号码和联系电话至少填写一项</span></div></li>
                <li><div class=oname><span class=xh>*</span>联系电话：</div><div class=oinfo><input id="Order_Tel" type="text" maxlength="20" style="width: 200px" value="<%=User_Tel %>" dataType="Phone" msg="联系电话格式不正确" require="false"/>&nbsp;</li>
                <li><div class=oname>传真号码：</div><div class=oinfo><input id="Order_Fax" type="text"  maxlength="20" style="width: 200px" value="<%=User_Fax %>" dataType="Phone" msg="传真号码格式不正确" require="false"/>&nbsp;</div></li>
                <li><div class=oname><span class=xh>*</span>电子邮件：</div><div class=oinfo><input id="Order_Email" type="text"  maxlength="50" style="width: 200px" value="<%=User_Email %>" dataType="Email" msg="电子邮件不能为空或格式不正确"/>&nbsp;</div></li>
                <li class=memo><div class=oname>特别说明：</div><div class="oinfo memo"><textarea name="Order_Memo" rows="2" cols="20" id="Order_Memo" style="width: 500px; height: 50px;" onkeydown="limitChars(this, 100)" onchange="limitChars(this, 100)" onpropertychange="limitChars(this, 100)"><%=User_Memo %></textarea>&nbsp;<span class=hs>100字以内</span></div></li>
            </ul>
        </div>
    </div>

    <div class="m detail" >
        <UL class=tab><LI class=curr>游客信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
        <div id="titles" class="mc tabcon">请详细填写每一位参加旅游的人员信息。</div>
        <%=GuestListInfo %>
        </div>
    </div>

    <div class="m detail">
        <UL class=tab><LI class=curr>旅游合同<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="ht_title" class="mc tabcon">旅游合同签约方式：
                <input id="Radio1" type="radio" name="ht" value="1" <%=RB1%>/><label for="Radio1" class=radiobtn>传真签约</label>
                <input id="Radio2" type="radio" name="ht" value="2" <%=RB2%>/><label for="Radio2" class=radiobtn>快递签约</label>
                <%--<input id="Radio3" type="radio" name="ht" value="3" <%=RB3%>/><label for="Radio3" class=radiobtn>上门签约</label>--%>
                <%--<input id="Radio4" type="radio" name="ht" value="4" <%=RB4%>/><label for="Radio4" class=radiobtn>门店签约</label>--%>
            </div>
            <ul id="Ht_List" class=order>
                <li><div class=oname>合同范本：</div><div class=oinfo><%=HTurl %><span class=htspan>如果您选择了传真或快递签约方式，预订成功并在线支付后，可下载正式合同</span></div></li>
                <li><div class=oname>签约说明：</div><div class=oinfo>请将旅游合同打印、填写并签字盖章后，传真到：<%=FaxNumber %>，我们的客服人员会及时进行处理。</div></li>
                <li class=hide><div class=oname>签约说明：</div><div class=oinfo>请将旅游合同打印、填写并签字盖章后，快递到：上海市徐汇区衡山路2号 电子商务部收，邮编：200031。</div></li>
                <li class=hide><div class=oname><span class=xh>*</span>您的地址：</div><div class=oinfo><input id="Ht_Address" type="text" maxlength="100" style="width: 500px" value="<%=User_Address %>"/>&nbsp;</div></li>
                <li class=hide><div class=oname>门店地址：</div><div class=oinfo>
                    <select id=Ht_Branch style="width: 500px">
                        <%=BranchOption %>
                    </select>&nbsp;
                </div></li>
            </ul>
        </div>
    </div>

    <div class="m detail">
        <UL class=tab><LI class=curr>发票信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01" id="NeedInvoiceSelect"><input onclick="iNeedInvoice()" type=checkbox name="NeedInvoice" id="NeedInvoice" <%=RC0%> />需要发票
        </div>
        <div class="mc tabcon borders01 <%=InvoiceShow%>" id="InvoiceInfo">
            <div id="fp_title" class="mc tabcon">发票领取方式：
                <input id="Radio5" type="radio" name="fp" value="1" <%=RC1%>/><label for="Radio5" class=radiobtn>门店领取</label>
                <input id="Radio6" type="radio" name="fp" value="3" <%=RC2%>/><label for="Radio6" class=radiobtn>快递发票</label>
            </div>
            <ul id="FP_List" class=order>
                <li><div class=oname>发票说明：</div><div class=oinfo>如果您选择了传真或快递签约方式，请留下您的地址、邮编等信息，预订成功并付款后旅游发票将快递给您</div></li>
                <li><div class=oname>发票抬头：</div><div class=oinfo><input id="Fp_Title" type="text" maxlength="50" style="width: 500px" value="<%=FpInfo1 %>"/>&nbsp;</div></li>
                <li><div class=oname>发票内容：</div><div class=oinfo>
                <select id="Fp_Content" style="width: 300px"><option value="旅游费">旅游费</option></select>
                <input class=hide id="Fp_Content_text" type="text" maxlength="50" style="width: 50px" value="<%=FpInfo2 %>"/>&nbsp;</div></li>
                <li class=hide><div class=oname>快递信息：</div><div class="oinfo memo"><textarea name="Fp_Kuaidi" rows="2" cols="20" id="Fp_Kuaidi" style="width: 500px; height: 40px;" onkeydown="limitChars(this, 100)" onchange="limitChars(this, 100)" onpropertychange="limitChars(this, 100)"><%=FpInfo3 %></textarea>&nbsp;<span class=hs>100字以内</span></div></li>
            </ul>       
        </div>
    </div>

    <div class="gotonext">
         <A class="<%=hide1 %>" href="javascript:void(0);" onclick="javascript:history.go(-1)">上一步</A> <A class="btn-link btn-personal" href="javascript:void(0);" onclick="GoToNext()">保存订单</A>
    </div>
</DIV>
</form>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/SecondStep.js"></script>
    <script type="text/javascript" src="/Scripts/Validator.js"></script>
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

        function SubmitOrder() {
            var OrderInfo = "";
            OrderInfo += $.trim($("#Order_Name").val()) + "@@";
            OrderInfo += $.trim($("#Order_Mobile").val()) + "@@";
            OrderInfo += $.trim($("#Order_Tel").val()) + "@@";
            OrderInfo += $.trim($("#Order_Fax").val()) + "@@";
            OrderInfo += $.trim($("#Order_Email").val()) + "@@";
            OrderInfo += $.trim($("#Order_Memo").val());

            var Parms = "";
            $(".guest ul").each(function () {
                var pid = "#" + $(this).attr("id");
                Parms += $(pid + " .Guest_Name").val().replace("所选证件的中文姓名", "") + "@@";
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
                Parms += $(pid + " .Guest_Tel").val();
                Parms += "||";
            });
            Parms = Parms.substr(0, Parms.length - 2);

            var HT = $("#ht_title :radio:checked").val();
            switch (HT) {
                case "1":
                    HT += "@@";
                    break;
                case "2":
                    HT += "@@";
                    break;
                case "3":
                    HT += "@@" + $("#Ht_Address").val();
                    break;
                case "4":
                    HT += "@@" + $("#Ht_Branch").val();
                    break;
                default:
                    HT = "1@@";
            }
            //            alert(HT);
            var FP = $("#fp_title :radio:checked").val();
            FP += "@@" + $("#Fp_Title").val();
            FP += "||" + $("#Fp_Content").val();
            FP += "||" + $("#Fp_Kuaidi").val();

            var NeedInvoice = "0";
            if ($("#NeedInvoice").prop("checked") == true) NeedInvoice = "1";

            var url = "/Purchase/AjaxService.aspx?action=SecondStep&EditFlag=Manage&SaveFlag=0&TempOrderId=" + $('#TempOrderId').val() + "&NFP=" + NeedInvoice + "&OrderInfo=" + escape(OrderInfo) + "&GuestInfo=" + escape(Parms) + "&HT=" + escape(HT) + "&FP=" + escape(FP) + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    alert("订单修改成功！");
                }
                else {
                    alert("提交 失败，请稍后重试！");
                }
            })
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



