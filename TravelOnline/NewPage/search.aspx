<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TravelOnline.NewPage.search" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="/NewPage/SearchBanner.ascx" tagname="SearchBanner" tagprefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =ThisTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.OutBoundKeywords %>" />
    <link rel="shortcut icon" href="">
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <script src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/common.js"></script>
    <script type="text/javascript" src="/newjs/jquery.pagination.js"></script>
    <script type="text/javascript" src="/newjs/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/newjs/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/newjs/jquery.cookie.js"></script>
    <script type="text/javascript" src="/newjs/setcookie.js"></script>
    <style>
        .browsed-mod .record .record-con .td {background: #90C7A4;}
        .browsed-mod .record .record-con .qz {background: #ea5843;}
        .browsed-mod .record .record-con .yl {background: #64AFD8;}
    </style>
	<!--[IF IE 6]>
		<script type="text/javascript" src="js/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
<body id="<%=BodyId %>">
	<!--页头Begin-->
	<uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
		<div class="left-content fl">
			<div class="link-nav">
				<ul class="clearfix">
					<li><a href="/index.html">首页</a></li>
					<span>></span>
					<li><%=BreadCrumb %></li>
					<span>></span>
					<h1><%=BreadCrumbName%></h1>
				</ul>
			</div>
			<!--搜索框-->
			<uc3:SearchBanner ID="SearchBanner1" runat="server" />
			<!--筛选条件-->
			<div class="species-wrap">
				<ul class="seach-nav clearfix">
                    <%=Tab_PlanType%>
				</ul>
				<div class="prop-list">
					<div class="prop-item action">
						<%=Tab_Destination%>
                        <%=Tab_City %>
						<dl class="clearfix">
							<dt rel="nofollow">行程天数：</dt><dd class='clearfix fl'>
							<%=Tab_Days%></dd>
						</dl>
						<dl class="clearfix">
							<dt rel="nofollow">旅游主题：</dt><dd class='clearfix fl'>
							<%=Tab_Topic %></dd>
						</dl>
					</div>
				</div>
			</div>
			<!--搜索列表-->
			<div class="screening-wrap clearfix">
				<%=SortCss%>
				<div class="price-mod">
					<div class="content fl clearfix">
						<p class="fl">价格范围</p>
						<div class="chose fl">
							<input type="text" value="<%=s_p1 %>" id="price_chose01" />
							<span>~</span>
							<input type="text" value="<%=s_p2 %>" id="price_chose02"  />
						</div>
						<input type="button" value="确定" class="sure" />
					</div>
					<%--<div class="line fl"></div>--%>
					<%--<div class="sale fl">
						<label><input type="checkbox" class="relative-box">特价优惠</label>
					</div>--%>
				</div>
			</div>
			<!--产品列表-->
			<div class="searchresult_left">
				<%=LineListString %>
			</div>
			<!--页码-->
			<div class="pagination-wrap">
				<div id="Pagination" class="pagination"></div>
			</div>
		</div>
		<div class="right-content">
			<div class="theme-mod">
				<h3 class="relative-box"><b></b>当季主题推荐</h3>
                <%=TravelOnline.NewPage.Class.CacheClass.LineList_Ad_Season()%>
			</div>
			<div class="note-mod">
				<h3 class="relative-box"><b></b><%=DestinationName %>游记</h3>
				<ul>
                    <%=Journal%>
				</ul>
			</div>
			<div class="browsed-mod">
				<h3 class="relative-box"><b></b>我已浏览过的产品</h3>
                <div id="CookieView">
                </div>
			</div>
		</div>
	</div>
	<!--正文内容End-->
	<div id="urlstring" tag="<%=UrlQuery %>" stag="<%=UrlQuery1 %>" des="<%=SearchName %>"></div>
    <!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
    <script type="text/javascript">
        $(function () {
            $("img").lazyload({ threshold: 400, effect: "fadeIn", placeholder: "img/grey.gif" });
        });
        $('.sure').click(function () {
            if (Number($("#price_chose01").val()) == 0 || Number($("#price_chose02").val()) == 0 ) {
                alert("请输入价格范围!");
                return false;
            }
            window.location = "search.html?p=1&p1=" + $("#price_chose01").val() + "&p2=" + $("#price_chose02").val()+ $("#urlstring").attr("stag");
        })

        $('#search_keyword').click(function () {
            window.location = "search.html?key=" + encodeURIComponent($("#searchPageInput").val());
        })

        function pageselectCallback(page_id, jq) {
            var page = page_id + 1;
            window.location = "search.html?p=" + page + $("#urlstring").attr("tag");
        }

        $(document).ready(function () {
            $("#searchPageInput").val($("#urlstring").attr("des"));
            // 创建分页元素
            $("#Pagination").pagination(<%=PageCount %>, {   //设置页数
                current_page:<%=current_page %>,
                num_edge_entries: 1,
                num_display_entries: 6, 		//当前页码后的省略号
                callback: pageselectCallback  //回调函数
            });

            // 设置处理程序设置分页选项通过形式
            $("#setoptions").click(function () {
                var opt = { callback: pageselectCallback };
                // 收集选项从文本字段,字段的命名就像他们的选择同行
                $("input[@type=text]").each(function () {
                    opt[this.name] = this.className.match(/numeric/) ? parseInt(this.value) : this.value;
                });
                // 提取maxitems
                var maxitems = opt.maxitems;
                delete opt.maxitems;
                // 避免在这个演示注射html
                var htmlspecialchars = { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" }
                $.each(htmlspecialchars, function (k, v) {
                    opt.prev_text = opt.prev_text.replace(k, v);
                    opt.next_text = opt.next_text.replace(k, v);
                })
                $("#Pagination").pagination(maxitems, opt);
            });
            //HistoryRecord();
            BindHistory();

        });
	</script>
</body>
</html>
