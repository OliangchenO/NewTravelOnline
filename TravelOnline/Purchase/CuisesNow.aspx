<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuisesNow.aspx.cs" Inherits="TravelOnline.Purchase.CuisesNow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript">
        if (top.location == location) {
            window.location = "/index.html";
        }
    </script>
    <style type="text/css"> 
        A:link {
	        FONT-SIZE: 14px;COLOR: #c00; TEXT-DECORATION: none
        }
        A:visited {
	        COLOR: #c00; TEXT-DECORATION: none
        }
        A:hover {
	        COLOR: #c00; TEXT-DECORATION: underline
        }
        A:active {
	        COLOR: #c00
        }
        .btns{
	CURSOR: hand;BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px;BORDER-BOTTOM: 0px;WIDTH: 64px; BACKGROUND: url(/images/bg_hotsale.gif) no-repeat 0px -466px; HEIGHT: 25px; COLOR: #fff; FONT-SIZE: 12px; TEXT-DECORATION: none
}
.Cruises li {HEIGHT: 18px;}
.Cruises .title {FONT-WEIGHT: bold;BACKGROUND: #60C72A; COLOR: #fff; TEXT-ALIGN: center}
.Cruises .title div {HEIGHT: 18px;BORDER-TOP: #ffffff 0px solid;FONT-WEIGHT: bold;FLOAT: left;}
.Cruises div { BORDER-TOP: #e6e6e6 1px solid; HEIGHT: 20px;OVERFLOW: hidden;FONT-WEIGHT: normal;FLOAT: left;TEXT-ALIGN: center}
.Cruises .t1 {WIDTH: 220px;FLOAT: left;}
.Cruises .t2 {WIDTH: 100px;FLOAT: left;}
.Cruises .r2 {WIDTH: 105px;FLOAT: left;FONT-WEIGHT: bold;TEXT-ALIGN: right;color:#E56700;FONT-FAMILY: Arial Unicode MS;}
.Cruises .t3 {WIDTH: 70px;FLOAT: left;}
.Cruises .t4 {WIDTH: 70px;FLOAT: left;}
.Cruises .t5 {WIDTH: 70px;FLOAT: left;}
    </style> 
</head>
<body>
    <DIV id="inputs" style="DISPLAY:none">
        <input id="PlanNums" type="hidden" value="<%=Seats %>"/>
        <input id="LineId" type="hidden" value="<%=lineid %>"/>
        <input id="PlanId" type="hidden" value="<%=planid %>"/>
        <input id="BeginDate" type="hidden" value="<%=begindate %>"/>
        <input id="TempOrderId" type="hidden" value="<%=ucode %>"/>
        <input id="RouteFlag" type="hidden" value="<%=routeflag %>"/>
        <input id="PlanNo" type="hidden" value="<%=planno %>"/>
        <input id="AdultNums" type="hidden" value="<%=nums %>"/>
        <input id="ChildNums" type="hidden" value="<%=etnums %>"/>
    </DIV>
    <div id="PlanDate" class="m detail">
    <UL class=tab><LI class=curr>舱位信息<SPAN></SPAN></LI></UL>
        <div id="step1" <%=step1 %> class="mc tabcon borders">
            <div style=" WIDTH: 560px; HEIGHT: 90px; color:#000000; OVERFLOW: auto; ">
                <%=step2 %>
            </div>
        </div>
        <div id="step3" <%=step3 %> class="mc tabcon borders">
            <div id="Div2" class="mc tabcon">
                <SPAN style="LINE-HEIGHT: 55px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;"><%=begindate %> 出发的已报名截止，请重新选择！</SPAN>
            </div>
        </div>
    </div>
    <DIV id=select class="m select">
    <DIV class=mt><H1></H1><STRONG>旅游出行安全告知书
        <%--&nbsp;&nbsp;&nbsp;&nbsp;产品专家热线：021-34014501--%></STRONG></DIV>
        <DL class=fore>
            <div id=memo style=" WIDTH: 575px; HEIGHT: 170px; color:#000000; OVERFLOW: auto; ">
<p>尊敬的嘉宾：您好！感谢您参加由上海中国青年旅行社组织的旅游活动,在旅游活动前,请您仔细阅读这份我们特别为您和同行人员提供的《旅游出行安全告知书》。在旅游行程中随团领队和导游员的友情提示及各地旅游设施的安全告知、各处接待人员的口头告知，在此不再做额外说明。为了您和同行人员旅游途中的安全，我们特别请您和您的同行人员在出行前阅读下列事项（如您认为合同所附的一份不够，可以向我们销售人员索取），这是我们应尽告知的责任，也是保障您的权益。希望您细心留意。衷心祝愿您：旅游愉快！<br />
一、乘机告知：<br />
1.&nbsp;请注意航班号、起飞时间。于飞机起飞前二小时抵达机场的指定集合地点，以免拥挤及影响办理登机等相关手续。<br />
2.&nbsp;国内旅游的，请带好有效的身份证件，出境的请带好有效的护照和机票。且不能随包托运。<br />
3.&nbsp;大件行李和刀具、尖锐物品、液体类（包括化妆品）请办理托运。易碎物品请注意包装。手机和数码相机、摄像机等的电池只能随身携带，不能托运。<br />
4.&nbsp;进入海关、安检后，请把握好时间，按登机牌上指定时间或机场广播通知前往登机口准时登机。<br />
5.&nbsp;搭乘飞机时，请按机长和乘务员要求随时扣紧安全带，以免气流突变等因素影响安全。<br />
6.&nbsp;飞行途中，请严格遵守机上的一切规章制度，听从机长和乘务员的指令。<br />
7.&nbsp;飞机降落后，在飞机没有停稳之前，请勿提前解开安全扣和站立起来以及拿取行李。<br />
二、乘车告知：<br />
1.&nbsp;在旅游车启动前，请将自己的小件行李放在行李架上，最好是不要离开自己的视线，放好放稳，避免落下，造成物品损坏或不必要的砸伤。大件行李请放在行李舱中或空闲的座位上。<br />
2.&nbsp;乘坐旅游车时，车上座位有安全带的请系上安全带，防止遇上颠簸发生身体撞伤。<br />
3.&nbsp;乘坐旅游车时，老人和未成年儿童要有成年人陪护，年龄比较小的最好是由成年人抱在怀中，在行车途中不要在车内走动、追逐嬉戏，以免在紧急制动和颠簸时发生危险。<br />
4.&nbsp;车辆在颠簸路段行驶过程中请不要喝水或吃东西（主要是坚果类）。以免发生呛水或卡咽等情况发生危险。为保持车内卫生，旅游车上一般不允许吃水果及瓜子等带皮食品。<br />
5.&nbsp;车辆在行驶中请勿任意更换坐位，勿将身体伸向车窗外。<br />
6.&nbsp;有晕车、高血压、心脏病病症的旅游者，请提前服用有效的药物，在旅途中若是有不舒服的症状，请及时告诉司机或导游。<br />
7.&nbsp;下车参观、游览时，请将现金、护照等其他贵重物品随身携带。不要将现金、机票、有价证券、贵重物品（数码相机/摄像机、钱包、手机等）放在行李里，更不要放在车上，以免丢失。<br />
8.&nbsp;上下车时，请注意其他方向来车等路面不安全因素以以免发生危险。<br />
9.&nbsp;请遵守当地的交通法规与行走习惯，过马路请走人行横道线，如景区不设人行道及横道线的，请注意往来车辆。<br />
三、住宿告知：<br />
1.&nbsp;入住宾馆时妥善保管好自己财物，贵重物品交前台保管或存于保险箱内，如随身携带，切勿离手，小心被窃。<br />
2.&nbsp;当您进入客房后的时间段内，应作为您的自行安排活动时间段，您将享有充分的个人私密空间，在此时间段内请您注意自己的安全。<br />
3.&nbsp;请您仔细阅读住宿须知，爱护宾馆内的各种物品，损坏需照价赔偿。请勿在房间内乱涂乱写乱倒垃圾。并请保管好房间钥匙，不要丢失，在房内请随时将房门扣上安全锁。<br />
4.&nbsp;在客房洗澡时，注意区分冷热水龙头，避免烫伤，以及注意地面和浴盆内湿滑，避免滑到摔伤。<br />
5.&nbsp;请勿在床上吸烟和饮酒，以及在灯具等照明设施上晾衣物。<br />
6.&nbsp;在酒店中行走时应注意台阶高低和各类障碍物，以及注意走道中是否有水渍等，注意防滑，以免摔伤。<br />
7.&nbsp;请仔细查看酒店的紧急出口及逃生标志，听到警报器响，请由紧急出口迅速离开。如遇火灾，请用湿被子护身，湿毛巾捂住口鼻逃生。利用床单、被子、毛巾等房内物品和设施，做好防烟防火措施、尽快疏散、积极自救。<br />
8.&nbsp;若要使用健身房健身的，请量力而行，以免发生事故。<br />
9.&nbsp;游泳池未开放时间，请勿擅自入池，并切记无救生员在场请勿入池。<br />
10.&nbsp;退房前，仔细检查所携带的行李物品，特别注意证件和贵重财物（如：手机、相机、首饰、手表等物品）是否有遗忘。按规定时间前往酒店柜台办理退房手续（如有额外消费或需赔偿的请自行结清款项）<br />
四、餐饮告知：<br />
1.&nbsp;用餐前要先洗手，不食用不卫生、不合格的食品和饮料。<br />
2.&nbsp;一般团队餐的餐标较低，都是以吃饱为原则，以卫生为根本。因每个人的宗教信仰、口味要求不同，若有特殊要求应提前向领队、导游或餐厅负责人说明。<br />
3.&nbsp;旅游团队餐饮以食品卫生安全为首，旅游团队的用餐一般都是在旅游局指定的定点餐厅用餐，如果客人自己安排用餐一定检查卫生是否安全达标。切勿在外随意用餐，尤其是生冷海鲜等水产品、冰饮品。<br />
4.&nbsp;食用海鲜应根据自身情况适量食用，切忌暴食暴饮。应注意：暴吃海鲜会中毒，忌海鲜加啤酒（加速尿酸形成）、海鲜加水果、海鲜配茶水（海鲜的钙与水果、茶中的鞣酸结合，易引起腹痛、恶心、呕吐）。<br />
5.&nbsp;用餐时请遵守餐厅的规章制度，切勿损坏餐厅的物品，如有赔偿由游客自行承担。<br />
五、旅游安全告知：<br />
1.&nbsp;旅游中请注意人身、财产和卫生安全，时刻注意防治传染病、流行病等，不到流行病高发区进行旅游活动，不与高危人群接触。<br />
2.&nbsp;请注意自觉遵守目的地国家（地区）的法律法规和宗教和风俗习惯,若违反当地国（地区）的法律，法规或破坏当地风俗会给您带来不必要的经济纠纷或法律责任。<br />
3.&nbsp;遵守所有观光区、餐厅、饭店、游乐设施等各种场所的安全注意事项。<br />
4.&nbsp;患有心脏病、高血压、哮喘、恐高症、残疾、精神病等疾病的游客，以及孕妇、酒后、不能自理、自控的游客，应在旅游过程中特别注意安全，做到只参加自己身体能承受的项目和游览点。<br />
5.&nbsp;景点游览，特别是登山旅游，出发前应检查好携带的各种装备，根据自身的身体情况，做好旅游安排，谨遵“走路看脚下、看景不走路、摄影照相要站稳、所用器械要带牢”的安全教诲。行走雪地或陡峭道路时，请小心谨慎。<br />
6.&nbsp;玩海，看大海时，请在景区允许并且导游指定的安全区域内活动。如果您想参与海边或海上娱乐活动，请您让领队或导游陪同前往，在尽情玩耍嬉水和垂钓时，请您多注意身边的海浪及脚下的青苔以防不测，同时请照顾好身边未成年人。有的海滩潮水湍急，海况、海边的地理环境都比较复杂，请您让水性比较好的人陪同前往，同时也请您穿好救生衣或带好救生圈。如果您想在海边拍照留影，特别要注意身后海浪的侵袭。但是遇到有风浪的天气，请您不要参与海边或海上活动。海边戏水请勿超越安全警戒线。<br />
7.&nbsp;乘船，出海乘坐休闲渔船观光、垂钓，在船上，请您遵守乘船规定，服从工作人员管理，照顾好身边小孩。如果您会晕船，您可以在开船前半小时服用晕船药。船靠岸时不要拥挤，在工作人员的帮助下上下船，游客上船后一定要穿好救生衣并坐稳。搭乘快艇请扶紧把手或坐稳，勿随意移动。<br />
8.&nbsp;在旅游中带孩子的家长要关照孩子，不要在严禁嬉戏的公共场所内随意嬉戏，叫喊，乱跑。儿童及老人须有照应能力的人陪同。<br />
9.&nbsp;搭乘缆车时，请依序上下，听从工作人员指挥，切勿超载时强行搭乘。<br />
10.&nbsp;环保，如果您吸烟或在野外进行野餐、烧烤等活动，离开时请您务必把火种熄灭，以免发生火灾。如果您在游玩时找不到垃圾筒，返回时请您把它带回，并放在沿途设立的垃圾筒内。为保护生态环境请不要进入围栏践踏花草、采摘等。<br />
11.&nbsp;请保管好个人的钱包、证件以及贵重物品。数额较大的现金最好分开存放，口袋里预留小额现金，做到钱财不露眼，人多时背包、拎包放前面，以防被盗。<br />
12.&nbsp;若有突发情况需求助时，请及时联系领队、导游或拨打当地的应急报警电话。<br />
六、自由活动<br />
1.&nbsp;您参加自费活动和购物活动的时间段内，各位游客都按自己的喜好分别活动，都享有自己的充分自由支配空间，故都应作为自由活动期间。故在此时间内您应当注意自己的人身、财产安全。<br />
2.&nbsp;在自由活动期间，请大家注意安全，并根据自己的身体条件应当选择自己能够控制风险的活动项目，并在自己能够控制风险的范围内活动。旅行社不建议您参加“潜水、游泳、高速摩托艇、降落伞、高空弹跳、攀崖、漂流、骑马、骑大象、骑骆驼、直升机、大峡谷小飞机、吉普车越野驾驶等高危娱乐活动。<br />
3.&nbsp;如您一定要参加高危娱乐项目的，当您参加浮潜时，请务必穿着救生衣，接受浮潜教练、救生员、领队、导游的讲解，并于岸边练习使用呼吸面具方得下水，并不可超越安全区域活动。如参加自费骑马、骑骆驼等活动时，请切记务须在服务人员之伴随或伴骑之下方得进行，万勿自行脱队或策马狂奔，以维自身安全。如您参加骑马、漂流、探险、自驾车等其他高危、高刺激娱乐项目需在活动前了解具体的活动常识，请您挑选适合自己的各种项目，活动前检查好各种器械，注意安全，服从指导人员的指挥，永远记住“安全第一” 旅游人身意外伤害险不含这些项目的赔偿，需单独增加旅游娱乐保险费。对参加此类高危娱乐活动的，发生危险时，旅行社不承担任何责任。<br />
4.&nbsp;参加娱乐活动要服从领队、导游统一安排，要选择当地旅游部门核准的娱乐场地及娱乐项目。团体活动时应紧跟团队，不参加集体组织娱乐活动的人员，请先行确认您对环境熟悉，确认您能自行确保自身安全的情况下进行自由活动，以免发生意外，并请告知领队、导游。<br />
5.&nbsp;请勿参加黄、毒、赌此类的娱乐活动。<br />
6.&nbsp;在自由活动期间，您最好至少保持两、三人一起活动，最好与熟悉的人结伴。切忌单独外出。最好与熟悉的人结伴，既可增添旅游的乐趣，又能互相照顾。<br />
7.&nbsp;夜间应避免单独外出。夜间或自由活动时间内需自行外出的，请告知领队、导游，并应特别注意安全。<br />
七、购物<br />
1.&nbsp;购物时需在看到自己需要的物品和相应的发票及质保书等票据后再行付款。<br />
2.&nbsp;旅游中的购物，应以“量财而出、喜欢才买。不买就不摸，不讨价还价，不品头论足”为原则，以防恶商欺负。理智消费、切忌摆阔；购物中应时刻注意随身携带的物品，谨防偷窃、购物后检查随身携带的物品，勿遗忘丢失。<br />
3.&nbsp;购买贵重物品和首饰、食用类保健品、药材时，应多听少动，做到要能辨别真伪、注意计量器具是否正确、并了解市场价格后理智消费。在旅行社的定点商店购买物品时，请索取发票以及珠宝首饰的质保书（成份说明书），还应注意商家恶意调包的恶劣手法。<br />
4.&nbsp;切记在公共场合财不露白，购物时也勿当众取出整叠钞票、也勿当众数钞票。<br />
八、天气变化等安全告知：<br />
1.&nbsp;如去草原、湿地、山地或高原等地区旅游气候昼夜温差大，请多带衣物，骑马、登山请穿旅游鞋或软底鞋，穿长裤戴手套等。<br />
2.&nbsp;遇有紧急情况（地震、火情、飓风等），不要慌张，镇定地判断情况。注意当地的天气预警报告，提高自我保护意识，主动地实行自救，服从公共执政部门的指挥，即使在最危险的时刻，也要保持镇静，等候救援人员的救助。<br />
3.&nbsp;如遇海拔高氟点低请大家根据自己的身体最好饮用自备矿泉水以防肠胃不适。因昼夜温差较大洗浴时要根据自己体能酌情而已以防感冒。旅游出发前应事先了解当地的气候、气温，携带相应的用品（如雨伞、遮阳帽、墨镜等）和衣物及自需药品。<br />
4.&nbsp;注意高原雷雨天气的雷电的科学躲避，不要到树下、民房下避雨。不要走动，应原地蹲下，关闭带电的器具及金属杆雨伞。<br />
5.&nbsp;出发前请注意查询旅游目的地的气候情况，注意带好相应的衣物和防护用品。如无法查询到的，可在出发前向领队等相关人员咨询。<br />
希望全体旅游嘉宾积极配合领队和导游的工作，互相关爱，互相帮助，团结协作，共同完成这次愉快的旅行。如果您结束旅游时，脸上依然还带着阳光般的笑容，那就是您对我们最高的奖赏。<br />
</p>
    </div>
        </DL>
    </DIV>
    <div style="padding-left: 200px;padding-top: 15px;LINE-HEIGHT: 30px;">
        <input id="Checkbox1" type="checkbox" />我同意安全告知书内容
        <span style="padding-left: 30px">&nbsp;</span><span id="islogin" style="display: none;" class="iloading1">正在提交，请稍候...</span>
        <input id="OrderBtn" class=btns type="button" value="下一步" <%=Btn1 %> onclick="BuyNow()" />
    </div>
    <script type="text/javascript">
        //$(function () {
        //    var specialmemo = window.parent.$('#specialmemo').html();
        //    $('#memo').html(specialmemo);
        //});

        function BuyNow() {
            //var adults = $('#AdultNums').val();
            //var childs = $('#ChildNums').val();
            if ($("#Checkbox1").prop("checked") == false) {
                alert("您必须同意安全告知书内容后才可继续报名！");
                return false;
            }
            var nums = Number($('#AdultNums').val()) + Number($('#ChildNums').val());
            $("#islogin").show();
            $("#OrderBtn").hide();
            var url = "AjaxService.aspx?action=TempOrder&TempOrderId=" + $('#TempOrderId').val() + "&PlanNo=" + $('#PlanNo').val() + "&routeflag=" + $('#RouteFlag').val() + "&begindate=" + $('#BeginDate').val() + "&planid=" + $('#PlanId').val() + "&lineid=" + $('#LineId').val() + "&nums=" + nums + "&adults=" + Number($('#AdultNums').val()) + "&childs=" + Number($('#ChildNums').val()) + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    top.location = "/Order/FirstStep/" + $('#TempOrderId').val() + ".html";
                }
                else {
                    $("#islogin").hide();
                    $("#OrderBtn").show();
                    alert("报名失败，请稍后重试！");
                }
            })
        }
    </script> 
</body>
</html>


