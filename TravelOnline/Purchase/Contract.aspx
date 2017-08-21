<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contract.aspx.cs" Inherits="TravelOnline.Purchase.Contract" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - <%=AutoId %> - 旅游合同</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/order.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <style> 
    #itimeslide{height:60px;position:relative;margin:0 10px;padding:15px 0}
    #itimeslide #date{display:none;position:absolute;left:0px;top:3px;width:100px}
    #itimeslide #date span{display:block;height:14px;padding:0 3px;background:#4e7db3;color:#fff;font-size:12px;line-height:14px}
    #itimeslide #date em{display:block;width:5px;height:3px;margin:0 auto;background:url(/images/20101224sprite.gif) no-repeat -61px 0}
    #itimeslide #timeline{overflow:visible;width:100%;height:2px;margin:0px 0 20px;background:#c7c7c7}
    #itimeslide #timeline li{display:block;position:absolute;left:0;top:8px;width:17px;height:17px;background:url(/images/20101224sprite.gif) no-repeat 0 0;text-indent:-999px;cursor:pointer}
    #itimeslide #timeline li.hover{background-position:-20px 0}
    #itimeslide span#titletop{display:none;position:absolute;top:29px;width:12px;height:8px;background:url(/images/20101224sprite.gif) no-repeat -88px -21px;z-index:1}
    #itimeslide #title{display:none;position:absolute;top:36px;padding:5px 5px;background:#f8f8ff;border:1px solid #708bab;border-radius:5px;-weblit-border-radius:5px;-moz-border-radius:5px;-webkit-box-shadow:3px 3px 3px #c7c7c7;
    -moz-box-shadow:3px 3px 3px #c7c7c7}
    .select {BORDER-LEFT: #e6e6e6 0px solid; BORDER-RIGHT: #e6e6e6 0px solid}
    </style>
</head>
<body id="none">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="TempOrderId" type="hidden" value="<%=OrderId %>"/>
    </DIV>
    <DIV class="w main">
    <div id="order_title">
        <h2 class="headline"><%=LineName %><SPAN class=headstep><%=AutoId %></SPAN>
           <p class="fontcolor02">出发日期：<%=BeginDate%> &nbsp; &nbsp; <%=NumsInfo %></p>
        </h2>
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

 <div class="right-extra">
    <div class="m detail">
        <UL class=tab><LI class=curr>联系信息<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id="checkinfo" class="mc tabcon"><%=User_Info %></div>
            <ul class=checks>
                <li class=memo><div class=oname>特别说明：</div><div class="oinfo"><%=User_Memo %></div></li>
            </ul>
        </div>
    </div>

    <div class="m detail">
        <UL class=tab><LI class=curr>旅游合同<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <ul class=order><%=Contracts %></ul>  
        </div>
    </div>

    <div class="m detail" id="InLandContract" <%=step1 %>>
        <UL class=tab><LI class=curr>合同内容预览<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id=memo style=" WIDTH: 800px; HEIGHT: 350px; color:#000000; OVERFLOW: auto;">
            <p>上海市国内旅游合同示范文本<br />
（2009版）</p>
<p>一、旅游者参加国内旅游应选择具有合法经营旅游业务资格的旅行社。旅行社应具有旅游行政管理部门统一颁发的《旅行社业务经营许可证》和工商行政管理部门统一颁发的《营业执照》。<br />
二、旅游前，旅行社应当与旅游者签订书面旅游合同，旅游者在交纳费用后，旅行社应开具发票。旅游者与旅行社签订合同可采用本合同示范文本。<br />
三、旅行社对旅游过程中可能危及旅游者人身安全的项目以及其他须注意的问题，应当事先向旅游者作真实说明和明确警示。旅游者应结合自身身体状况选择旅游项目。<br />
四、旅行社委托组团的，须事先告知并征得旅游者书面同意。<br />
五、在填写本合同第二条“游程与标准”时，旅行社应以准确、明晰的语言表述，不得出现“准X星级”、“相当于X星级”等模糊用语。<br />
六、旅行社在签订合同时应真实注明该旅游项目所包含的景点门票及住宿、餐饮、交通标准、导游服务等相关内容。自选项目应安排在自由活动时间，并不得影响旅游整体行程。旅行社不得强制旅游者参加其安排的自选项目。<br />
七、本合同示范文本是《上海市国内旅游合同(2004年A版和B版)》合并修改版本，自本通知下发之日起使用，今后凡未制定新的版本前，本版本延续使用。<br />
八、旅游咨询与投诉机构：<br />
上海市&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 区（县）旅游部门地址：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
旅游质量监督电话：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 邮编：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
</u>上海市旅游质量监督所地址：中山西路2525号&nbsp;&nbsp;&nbsp; 邮编：200030<br />
投诉电话：64393615、962020<br />
上海市旅游局地址：大沽路100号&nbsp;&nbsp;&nbsp; 邮编：200003<br />
质量监督电话：962020<br />
国家旅游局地址：北京市建国门内大街甲九号&nbsp;&nbsp;&nbsp; 邮编：100740<br />
质量监督电话：（010）65275315<br />
上海市消费者申（投）诉举报中心<br />
举报投诉电话：12315<br />
合同编号：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>
<p>上海市国内旅游合同<br />
（2009版）</p>
<p>甲方（旅游者或旅游团体）：<u>&nbsp;&nbsp;&nbsp;<%=OrderName%>&nbsp;&nbsp;&nbsp; <br />
</u>乙方（旅行社）：<u>&nbsp;&nbsp;&nbsp;上海中国青年旅行社&nbsp;&nbsp;&nbsp;</u> 经营许可证编号：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
</u>&nbsp;&nbsp;&nbsp;&nbsp; 经营范围：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u></p>
<p>为保证旅游服务质量，明确合同当事人的权利义务，根据《中华人民共和国合同法》及《中华人民共和国消费者权益保护法》等有关法律法规的规定，双方经过协商一致，达成如下协议：<br />
第一条&nbsp;合同标的<br />
旅游路线名称<u>&nbsp;&nbsp;&nbsp;<%=LineName %>&nbsp;&nbsp;&nbsp;</u>团号：<u>&nbsp;&nbsp;&nbsp;<%=PlanNo %>&nbsp;&nbsp;&nbsp;</u>（或在行前说明会告知）。<br />
组团方式 □自行组团&nbsp; □委托组团（被委托方<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>）。<br />
行程共计<u>&nbsp;&nbsp;&nbsp;<%=LineDays%>&nbsp;&nbsp;&nbsp;</u>天&nbsp;&nbsp;&nbsp; 夜（含在途时间）。<br />
出发日期<u>&nbsp;&nbsp;&nbsp;<%=BeginDate%>&nbsp;&nbsp;&nbsp;</u>，出发地点<u>&nbsp;&nbsp;上海&nbsp; </u>。<br />
途径地点<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
目的地<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
结束日期<u>&nbsp;&nbsp;&nbsp;<%=EndDate%>&nbsp;&nbsp;&nbsp;</u>，返回地点<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
第二条&nbsp; 游程与标准（如附行程单，须含下列要素）<br />
旅行社统一安排的游览项目名称<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
游览时间<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
景点名称<u>&nbsp; &nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
景点游览时间<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
往返交通<u>&nbsp; &nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，标准<u>&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
游览交通<u>&nbsp; &nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，标准<u>&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
住宿天数<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，旅游饭店名称<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，标准<u>&nbsp;&nbsp; 见行程 </u>。<br />
用餐次数<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，标准<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
自选项目名称<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，费用<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
旅游者自由活动时间<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，次数<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
购物场所名称<u>&nbsp;&nbsp; 见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
购物次数<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>，时间<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
地接社名称<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
导游服务（□全陪；□地陪）。<br />
第三条&nbsp; 旅游者保险<br />
乙方应提示甲方购买旅游意外险。经乙方推荐，甲方<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>（应填同意或不同意，打勾无效）委托乙方办理个人投保的旅游意外保险。<br />
保险公司及产品名称：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
保险金额：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 元 ，保险费<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>元。&nbsp; <br />
第四条&nbsp; 旅游费用及其支付<br />
甲方应交纳旅游费用总额<u>&nbsp;&nbsp;&nbsp;<%=AllPrice%>&nbsp;&nbsp;&nbsp;</u>元，费用付款期限<u>&nbsp;&nbsp;出发前付清&nbsp; </u>。<br />
旅游费用支付方式 □现金；□支票；□信用卡；□其他&nbsp;<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
第五条&nbsp; 双方的权利义务 <br />
（一）甲方的权利与义务<br />
1．甲方在旅游活动中应遵守团队纪律，配合导游完成合同约定的旅游行程；甲方应遵守国家法律、法规，尊重旅游目的地的宗教信仰、民族习惯和风土人情。<br />
2．甲方在履行本合同过程中有权拒绝参加合同未约定的其他旅游消费项目。<br />
3．甲方在本合同安排的购物点所购物品系假冒伪劣商品时，有权要求乙方协助进行索赔。自购物之日起90日内，甲方无法从购物点获得赔偿的，可凭有效凭证要求乙方先行赔付。<br />
4．在旅游过程中，甲方应保管好随身携带的财物。<br />
5．在自由活动期间，甲方应在自己能够控制风险的范围内活动，选择自己能够控制风险的活动项目，并对自己的安全负责。<br />
6．行程中发生纠纷，甲方应与乙方平等协商解决，采取适当措施防止损失扩大，不得以拒绝登机（车、船）等行为拖延行程或者脱团。<br />
7．甲方签订合同或者填写各种报名材料时，应当使用有效身份证件，并对填写信息的真实性负责。甲方不能成行的，可以让具备参加本次旅游条件的第三人代为履行合同，并及时通知旅行社。因代为履行合同增加或减少的费用，双方应按实结算。<br />
（二）乙方的权利与义务<br />
1．乙方应如实介绍相关旅游服务项目和标准，向甲方推荐购买个人旅游意外保险，并在合同中予以注明。<br />
2．乙方应按本合同约定的标准提供服务。对在旅游过程中可能危及甲方人身、财产安全的情况，乙方应事先说明或者明确警示，并采取防止危害发生的措施。<br />
3．甲方属老年人等特殊群体凭有效证件可享受旅游景点门票优惠的，双方对该旅游景点门票价格可在补充条款中另行约定。<br />
4．因航空、轮船、铁路运输费用遇国家政策性调价导致合同总价发生变更的，双方应按实结算。<br />
5．在行程中，遇到不可抗力或者意外事件无法继续履行合同的，旅行社在征得旅游团队多数成员同意后可以对相应内容予以变更，变更后增加或减少的费用，双方按实结算。<br />
第六条&nbsp; 违约责任<br />
（一）旅游出行前，一方当事人因违约不能成行的，按照下列标准承担违约责任。<br />
1．违约方在出发前72小时通知对方的，应当支付旅游合同总价5%的违约金。<br />
2．违约方在出发前72小时内通知对方的，应当支付旅游合同总价10%的违约金。<br />
3．以上违约责任如涉及航空、陆运、水运票务等损失，可参照相关部门有关条款另行赔偿，违约金或赔偿金总额不超过旅游费用总额。<br />
（二）旅游途中甲方或乙方承担的违约责任。<br />
1．甲方违反合同约定造成自身或他人损失的，责任由甲方承担。<br />
2．乙方未按合同标准提供交通、住宿、餐饮等相关服务，或者未经甲方同意调整旅游行程，给甲方造成损失的，责任由乙方承担。<br />
（三）其它违约责任&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 。<br />
第七条&nbsp; 争议解决方式<br />
双方发生争议的，可协商解决，或向有关部门申请调解；也可提请上海仲裁委员会仲裁（不愿意仲裁而选择向法院提起诉讼的，请双方在签署合同时将此仲裁条款划去）。<br />
第八条&nbsp; 合同生效<br />
本合同自双方签字或盖章之日起生效，行程单和补充条款均为合同的附件，与本合同具有同等的法律效力。<br />
补 充 条 款</p>
<p>签约地点：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
甲方签字（盖章）：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 乙方签字（盖章）：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
住&nbsp;&nbsp;&nbsp; 所：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 营业场所：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
甲方代表：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 乙方代表（经办人）：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
联系电话：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 联系电话：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
邮&nbsp;&nbsp;&nbsp; 编：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 邮&nbsp;&nbsp;&nbsp; 编：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
日&nbsp;&nbsp;&nbsp; 期：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 日&nbsp;&nbsp;&nbsp; 期：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
</p>
            </div>
        </div>
    </div>

    <div class="m detail" id="OutBoundContract" <%=step2 %>>
        <UL class=tab><LI class=curr>合同内容预览<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id=Div2 style=" WIDTH: 800px; HEIGHT: 350px; color:#000000; OVERFLOW: auto;">
            <p> 上海市出境旅游合同示范文本<br />
（2004版）</p>
<p>上海市旅游事业管理委员会&nbsp;&nbsp; 制定<br />
上海市工商行政管理局&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 监制<br />
上海市出境旅游合同示范文本<br />
（2004版）<br />
合同编号：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</u></p>
<p>特 别 告 知</p>
<p>本合同文本是根据《中华人民共和国合同法》和《上海市合同格式条款监督条例》及有关法律、法规制定的示范文本，供双方当事人约定采用，签订合同前请仔细阅读。<br />
依据我国法律、法规的规定，旅游者在旅游活动中享有下列权利，并应当履行下列义务：<br />
一、旅游者的权利<br />
（一）旅游者享有自主选择旅行社的权利。我国出境旅游实行特许经营制度，因此，旅游者有权要求旅行社出示出境旅游经营许可证明，并与旅行社协商签订旅游合同，约定双方的权利和义务。<br />
（二）旅游者享有知悉旅行社服务的真实情况的权利。旅游者有权要求旅行社提供行程时间表和赴有关国家（地区）的旅行须知，提供旅行社服务价格、住宿标准、餐饮标准、交通标准等旅游服务标准和境外接待旅行社名称等有关情况。<br />
（三）旅游者享有人身、财物不受损害的权利。旅游者有权要求旅行社提供符合保障人身、财物安全要求的旅行服务。旅行社应当推荐旅游者购买旅游期间相关的旅游者个人保险。<br />
（四）旅游者享有要求旅行社提供约定服务的权利。旅游者有权要求旅行社按照合同约定和行程时间表安排旅行游览;旅游者有权要求旅行社为旅行团委派持有《领队证》的专职领队人员，代表旅行社安排境外旅游活动，协调处理旅游事宜。<br />
（五）旅游者享有自主购物和公平交易的权利。境外购物纯属自愿，购物务必谨慎。旅游者有权要求旅行社带团到旅游目的地国家或地区旅游管理当局指定的商店购物；有权拒绝超合同约定的购物行程安排；有权拒绝到非指定商店购物；有权拒绝旅行社的强迫购物要求。<br />
（六）旅游者享有自主选择自费项目的权利。参加自费项目纯属个人自愿，旅游者有权拒绝旅行社、导游或领队推荐的各种形式的自费项目，有权拒绝自费风味餐等。<br />
（七）旅游者享有依法获得赔偿的权利。出境旅游活动过程中，旅游者的权利受到法律保护，旅游者可以要求赔偿。旅游者和旅行社已有约定的，按照约定承担，没有约定的，按照下列协议承担违约责任：<br />
1．因旅行社原因不能成行造成违约的：<br />
（1）旅行社在出团7天前（含7天）通知的，旅游者可获得旅游合同总价5%的违约金；<br />
（2）旅行社在7天之内通知的，旅游者可获得旅游合同总价10%的违约金。<br />
因违约造成的损失，按有关法律、法规和规章的规定，承担赔偿责任。<br />
2．旅行社安排合同约定以外需要收费的旅游项目，应征得旅游者的同意。旅行社擅自增加或减少旅游项目，给旅游者的合法权益造成损害的，旅游者有权向旅游行政管理等部门投诉或通过其他法律途径依法获得赔偿。<br />
旅行社组团不成的，经征得旅游者同意后转至其他旅行社合并组团时，原合约即告终止，新合约同时生效，双方均不再系争。<br />
（八）旅游者享有人格尊严、民族风俗习惯得到尊重的权利。旅游者的人格尊严不受侵犯，民族风俗习惯应当得到尊重，这是我国法律的规定。在选择出境旅行社和出境旅游活动中，旅游者的人格尊严和民族风俗习惯受到损害的，旅游者有权得到法律救助。<br />
（九）旅游者享有对旅行社服务进行监督的权利。旅游者有权抵制旅行社侵害旅游者权益的行为，有权对保护旅游者权益工作提出批评、建议。旅游者有权将组团旅行社发给旅游者的征求意见表寄给组团旅行社所在地的省级旅游行政管理部门，如有必要也可以直接寄给国家旅游局旅游质量监督管理所。<br />
二、旅游者的义务<br />
（一）旅游者有维护祖国的安全、荣誉和利益的义务。在出境旅游中，不得有危害祖国的安全、荣誉和利益的行为。<br />
（二）旅游者有合法保护自己权益的权利，也有不得侵害他人权利的义务。当旅游者在行使权利的时候，不得损害国家的、社会的、集体的利益和其他旅游者的合法权利。<br />
（三）旅游者必须遵守国家的法律、法规。在出境旅游申办和实施过程中，必须提供真实情况，如实填写有关申请资料，履行合法手续。否则,将承担由此产生的一切经济和法律责任。旅游者参加旅游应确保自身身体条件能够完成旅游活动，并有义务在签订合同时将自身健康状况告知旅行社。要保守国家秘密，遵守公共秩序，遵守社会公德，尊重领队人格和服务，服从旅游团体安排，不得擅自离团活动，不得擅自滞留不归。如在境外擅自离团或非法滞留，所产生的一切后果均由当事者承担。<br />
（四）旅游者应当遵守合同约定，自觉履行合同义务。非经旅行社同意，不得单方变更、解除旅游合同，但法律、法规另有规定的除外。因旅游者的原因不能成行造成违约的，旅游者应当提前7天（含7天）通知对方，但旅游者和组团旅行社也可以另行约定提前告知的时间。对于违约责任，旅游者和旅行社已有约定的，从其约定承担，没有约定的，按照下列协议承担违约责任：<br />
1．旅游者按规定时间通知对方的，应当支付旅游合同总价5%的违约金；<br />
2．旅游者未按规定时间通知对方的，应当支付旅游合同总价10%的违约金。<br />
旅行社已办理的护照成本手续费、订房损失费、实际签证费、国际国内交通票损失费按实计算。因违约造成的其他损失，按有关法律、法规和规章的规定承担赔偿责任。<br />
旅行社与旅游者订立合同后，因不可抗力不能履行合同的，根据不可抗力的影响，部分或者全部免除责任，但法律另有规定的除外。<br />
（五）旅游者应当遵守旅游目的地国家（地区）的法律，尊重当地的民族风俗习惯，不得有损害两国友好关系的行为。<br />
（六）旅游者应当自尊、自重、自爱。维护祖国和中国公民的尊严和形象，不得有损害国格、人格的行为，不得涉足不健康的场所。<br />
（七）旅游者应当努力掌握旅行所需的知识，提高自我保护意识。旅游者必须参加旅行社组织的行前说明会。<br />
（八）旅游者要保存好旅游行程中的有关票据、证明和资料，以便当旅游者的合法权益受到侵害时，作为投诉凭据、索赔证据。<br />
（九）出境旅游过程中，旅游者与旅行社之间发生纠纷，应当本着平等协商的原则解决或在回国后通过法律途径解决。旅游者不得以服务质量等问题为由，在境外拒绝登机（车、船）、实施违反行程国家或者地区法律、法规的行为或采取其他措施强迫旅行社接受其提出的条件。<br />
（十）旅游者所携带的行李物品应当符合我国和旅游目的地国家（地区）的法律规定。携带货币出境，应当按照国家有关部门的规定，不准携带违禁物品出入境。<br />
三、旅游咨询与投诉<br />
上海市旅游事业管理委员会通讯地址：中山西路2525号，邮编：200030。<br />
上海市旅游质量监督所投诉电话：64393615。<br />
国家旅游局通讯地址：北京市建国门内大街甲九号，邮编：100740。<br />
国家旅游局旅游质量监督管理所投诉电话：（010）65275315。<br />
协 议 条 款</p>
<p>甲方：<u>&nbsp;&nbsp;&nbsp;<%=OrderName%>&nbsp;&nbsp;&nbsp;</u>（旅游者或团体），联系电话：&nbsp;&nbsp;&nbsp;<%=Tel%>&nbsp;&nbsp;&nbsp;<br />
乙方：<u>&nbsp;&nbsp;&nbsp;上海中国青年旅行社&nbsp;&nbsp;&nbsp;</u>（组团旅行社），联系电话：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
甲方自愿购买乙方所销售的出境游旅游服务，为保障双方权利和履行义务，本着平等协商的原则，现就有关事项达成如下协议：<br />
第一条&nbsp; 促销与咨询<br />
（一）乙方保证其具有国家认可的出境游组团资格。<br />
（二）乙方广告及其他宣传制品内容属实。<br />
（三）甲方为我国法律、法规所规定的允许出境游的大陆公民。<br />
（四）甲方应就出境游旅游服务情况作详尽的了解。<br />
第二条&nbsp; 销售与成交<br />
（一）甲方向乙方表明旅游需要、购买意向。<br />
（二）乙方对订单中的日程、标准、项目、旅游者须知如实介绍、报价。<br />
（三）对旅游服务费用所含项目，双方达成共识。<br />
（四）甲方确定所购买的旅游服务并交齐所需费用。<br />
（五）乙方出具发票、成交订单等文件。<br />
（六）乙方必须召开行前说明会，要向甲方交待并提供书面形式的出发时间、地点及提醒注意事项，并推荐旅游者购买旅游个人保险。<br />
（七）乙方要向甲方介绍领队，并建立领队与甲方的联系。<br />
（八）双方约定由于甲方或乙方责任未成行的处理方式。<br />
（九）“特别告知”为本合同的一部分，签订合同前甲方应当仔细阅读“特别告知”内容。<br />
第三条&nbsp; 成交订单<br />
（一）内容<br />
姓名<u>&nbsp;&nbsp;&nbsp;<%=TourstName%>&nbsp;&nbsp;&nbsp;</u>&nbsp; 性别<u>&nbsp;&nbsp;<%=Sex %>&nbsp;&nbsp;</u>&nbsp; 年龄<u>&nbsp;&nbsp;<%=Age %>&nbsp;&nbsp;</u>&nbsp; 人数<u>&nbsp;&nbsp;<%=Nums%>&nbsp;&nbsp;</u> <br />
旅游路线名称<u>&nbsp;&nbsp;&nbsp;<%=LineName %>&nbsp;&nbsp;&nbsp;</u> 团号<u>&nbsp;&nbsp;&nbsp;<%=PlanNo %>&nbsp;&nbsp;&nbsp;</u>（或在行前说明会告知）<br />
行程共计&nbsp;&nbsp;&nbsp; 晚&nbsp;&nbsp;<%=LineDays%>&nbsp;&nbsp;天（飞机、车、船在途时间以及前往目的地和返回境内时间包括在行程天数之内）<br />
出发、返回地点、日期<u>&nbsp;&nbsp;&nbsp;<%=BeginDate%>&nbsp;&nbsp;<%=EndDate%>&nbsp;&nbsp;&nbsp;<br />
</u>行走国家（地区）及主要游览点<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
</u>交通工具<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
</u>住宿次数<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>（见行程或在行前说明会告知）<br />
标准<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
</u>购物次数、内容<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
</u>团费总价<u>&nbsp;&nbsp;&nbsp;<%=AllPrice%>&nbsp;&nbsp;&nbsp;元<br />
</u>注明：不包含的费用<u>&nbsp;&nbsp;&nbsp;见行程&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u><br />
以上条款经签字或盖章确认后，甲乙双方应恪守约定，不得擅自更改。<br />
（二）甲方在旅游服务提供期间应服从乙方的统一安排要求，乙方旅游服务提供应符合国家标准和行业标准的规定。<br />
（三）乙方广告、宣传制品的内容符合要约规定的，视为本合同的一部分，对乙方具约束力。<br />
第四条&nbsp;&nbsp;&nbsp; 违约责任<br />
（一）乙方在下列情形下承担赔偿责任:<br />
1．因乙方的原因未达到合同规定的要求，造成甲方损失；<br />
2．乙方旅游服务的提供未达到国家或行业标准的规定；<br />
3．乙方代理甲方办理旅游所需手续时，遗失或损毁甲方证件的。<br />
（二）甲方在下列情况下责任自负或承担赔偿责任:<br />
1．甲方违约，自身损失自负，给乙方造成损失的，要承担赔偿责任；<br />
2．甲方违反我国或前往目的地国家（地区）的法律、法规，产生的后果由甲方自负；<br />
3．由于甲方提供给乙方的联系渠道有误，导致乙方有关旅游信息未及时传达到甲方的；<br />
4．超出本合同约定的订单内容进行个人活动而造成损失的，责任自负。<br />
（三）不承担违约责任的情形:<br />
1．因不可抗力造成甲、乙双方不能履约，已成行的，应提供不能履约的证据，未成行的，应及时通知对方；<br />
2．本合同双方已经就可能出现的问题约定免责处理措施的。<br />
（四）乙方在旅游质量问题出现前后已采取下列措施的，可减轻或者免除责任：<br />
1．对发生的违约已采取了预防性措施；<br />
2．乙方及时采取了善后处理措施；<br />
3．由于甲方自身过错造成的质量问题。<br />
第五条&nbsp; 争议的解决<br />
（一）本合同在履行中如发生争议，双方应协商解决，甲方可向有管辖权的旅游质监所申请调解、提出投诉和赔偿请求。<br />
（二）当事人不愿通过协商、调解解决或协商、调解不成时，依法向上海仲裁委员会申请仲裁；若不同意通过上海仲裁委员会仲裁的，可采取下列方式：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 。<br />
第六条&nbsp; 本合同一式两份，合同双方各执一份，具有同等效力。<br />
第七条&nbsp; 本合同自签订之日起生效。<br />
补&nbsp; 充&nbsp; 条&nbsp; 款<br />
双方约定以下补充条款：</p>
<p><br />
甲方签字或盖章：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 乙方签字或盖章：<br />
年&nbsp;&nbsp; 月&nbsp;&nbsp; 日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 年&nbsp;&nbsp; 月&nbsp;&nbsp; 日<br />
</p>
            </div>
        </div>
    </div>

    <div class="m detail" id="OtherContract" <%=step3 %>>
        <UL class=tab><LI class=curr>合同内容预览<SPAN></SPAN></LI></UL>
        <div class="mc tabcon borders01">
            <div id=Div3 style=" WIDTH: 800px; HEIGHT: 350px; color:#000000; OVERFLOW: auto;">
            <p> 代办机票、代订酒店、代办游轮等委托合同</p>
<p>甲方：<u>&nbsp;&nbsp;&nbsp;<%=OrderName%>&nbsp;&nbsp;&nbsp;</u> 联系电话：<u>&nbsp;&nbsp;&nbsp;<%=Tel%>&nbsp;&nbsp;&nbsp;</u> 住址：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
</u>乙方：上海中国青年旅行社&nbsp;&nbsp; （营业执照号码：3101041023790）<br />
经营范围：入境旅游业务，国内旅游业务，特许经营中国公民自费出国旅游业务。国内国际航空运输销售代理，会展服务。<br />
地址：上海市衡山路2号； 总机64330000； 旅游热线：4006777666<br />
经营许可证号码：L-SH-CJ00004<br />
旅游服务监督、投诉电话：021-64315207<br />
――本协议签约地点：上海市衡山路2号</p>
<p>第一条.&nbsp;甲方要求代办项目仅限于：<br />
一.&nbsp;甲方人员信息：<br />
1.&nbsp;甲方人员姓名、身份证号（护照号）：<br />
姓名：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；拼音：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；身份证（护照）号：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；<br />
姓名：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；拼音：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；身份证（护照）号：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；<br />
姓名：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；拼音：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；身份证（护照）号：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
（以上信息可使用身份证、护照的复印件）<br />
2.&nbsp;甲方保证所提供的证件及相关资料真实有效，并保证其在有效期内。<br />
二.&nbsp;订票服务：<br />
1.&nbsp;机票和机场费、燃油税:<br />
出发地：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</u> 目的地：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</u> 航班：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;</u> 数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
是否往返：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 航班：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</u>。<br />
2.&nbsp;游轮船票：<br />
出发地：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 目的地：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 航班：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>， <br />
游轮船名：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 数量及等级：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u> 签证：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
本合同所附的岸上游程全部由船公司负责安排，除自行承担的费用外，包含在船票中。乙方仅承担代卖船票的责任。<br />
3.&nbsp;甲方<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>（委托/不委托）乙方代办前往国家/地区<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>的签证/签注。<br />
4.&nbsp;双方其他约定：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
三.&nbsp;订房服务：<br />
1.&nbsp;酒店（单房差价另外需明示）：<br />
目的地1：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；入住日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；离店时间：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；<br />
标准：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；房间数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
目的地2：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；入住日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；离店时间：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；<br />
标准：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；房间数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
目的地3：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；入住日期：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；离店时间：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；<br />
标准：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；房间数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
标准：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>；房间数量：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
（如机程和目的地过多时，可以采用附件）<br />
2.&nbsp;双方其他约定：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>。<br />
3.&nbsp;乙方在接到委托件后应立即开始业务操作，并于&nbsp;&nbsp;&nbsp;&nbsp; 日内给予甲方书面确认。确认中，包含酒店名称、星级、房价、房型、地址、是否含早餐（及其几份）、客人姓名、入住/离店日期、预订记录编号、以及变更信息。<br />
4.&nbsp;甲方入住酒店一天的时间范围一般为当天下午2点至第二天中午12点。<br />
5.&nbsp;除人力不可抗拒的因素外（包含酒店被政府使用等），如因甲方之原因导致的任何变更所产生的追加费用由甲方负责，并由甲方负责赔偿乙方由于甲方原因导致损失的费用。如因乙方之原因导致的任何变更所产生的追加费用由乙方负责（机票、酒店标准提高的除外），并由乙方负责赔偿甲方由于乙方原因导致损失的费用。<br />
四.&nbsp;双方其他约定：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </u>。<br />
五.&nbsp;总费用：甲方应付费用为<u>&nbsp;&nbsp;&nbsp;<%=AllPrice%>&nbsp;&nbsp;&nbsp;</u>元（人民币<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u>元整）。<br />
第二条.&nbsp;付款及违约责任：<br />
1.&nbsp;乙方违约：<br />
甲方一旦签署，本合同自动生效。自合同生效后，乙方在出发前3天（含3天）通知甲方无法成行的，乙方不承担任何责任，并全额退还款；乙方在出发前3天以内通知甲方的，甲方可收取机票、酒店全部费用的10%为违约金。<br />
2.&nbsp;甲方违约：<br />
a)&nbsp;甲方在出发前3天（含3天）通知乙方取消成行的，甲方除支付乙方已发生的全部费用后，还应向乙方支付机票、酒店全部费用的5%为违约金；<br />
b)&nbsp;甲方在出发前3天以内通知乙方取消成行的，甲方除支付乙方已发生的全部费用后，还应向乙方支付机票、酒店全部费用的10%为违约金；<br />
c)&nbsp;即便甲方在上述第2点的时间段内提出改变行程的，如果甲方的机票已经出票或得到航空公司确认以及订房已经得到酒店确认的，并且乙方已经或者必须向航空公司和酒店支付费用的，乙方将不退回甲方的所有费用。甲方可以自行向航空公司和酒店协商退款。<br />
3.&nbsp;由于甲方仅委托乙方单订机票、单订房业务，所以，乙方仅承担单订机票、单订房的责任。甲方人员在旅途发生任何人身伤害、财产损害时，应向航空公司或酒店进行索赔，乙方不承担任何赔偿责任。<br />
第三条.&nbsp;不可抗力<br />
1.&nbsp;甲乙双方因不可抗力不能履行合同的，部分或者全部免除责任，但法律另有规定的除外。<br />
2.&nbsp;由于甲方原因延迟履行本合同后发生不可抗力的，不能免除责任。<br />
第四条.&nbsp;合同效力和争议解决<br />
1.&nbsp;本合同书一式二份，甲乙双方各执一份，经双方授权代表签字盖章后立即生效，双方一致确认：当甲方收到机票（包含电子机票信息）以及订房确认单之时，乙方已经全部履行完毕其责任义务，本合同终止，<br />
2.&nbsp;甲、乙双方为甲方订票、订房业务而往来的确认件，如：信件、传真件、电子邮件、msn通话记录等均为本协议的附件，本协议书和协议书的附件为不可分割的整体，协议书正文与协议书附件的条款具有同等的效力。<br />
3.&nbsp;本合同适用于中华人民共和国《合同法》及相关法律，双方在执行合同过程中发生的争议应通过友好协商加以解决，协商不成的，双方同意由中国上海徐汇区人民法院管辖。</p>
<p>甲&nbsp; 方：____________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 乙&nbsp; 方：上海中国青年旅行社<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;签署人：<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></p>
<p>日&nbsp; 期:_______年____月___日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 日&nbsp; 期:______年______月____日</p>
            </div>
        </div>
    </div>

    <div class="gotonext">
         <A class="btn-link btn-personal" href="../../Purchase/WordOutPut.aspx?Action=<%=ProType%>&Cid=<%=QueryId%>" target="_blank">合同下载</A> <A class="btn-link btn-personal" href="../../Purchase/WordOutPut.aspx?Action=OrderRoute&Cid=<%=QueryId%>" target="_blank">行程下载</A>
    </div>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/TimePoint.js"></script>
    <script type="text/javascript">
        window.onscroll = function () {
            var top = "260";
            var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
            if (scrollTop > top) {
                $("#pricebar").attr({ "class": "package_ptfix" });
            } else {
                $("#pricebar").removeAttr("class");
            }
        };

        window.onload = function () {
            iTimePoint('itimeslide', 'date', 'timeline', 'titletop', 'title');
        }
    </script>
</body>
</html>