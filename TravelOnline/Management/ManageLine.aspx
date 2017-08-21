<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLine.aspx.cs" Inherits="TravelOnline.Management.ManageLine" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅游线路管理</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript">

	</script>
    <style type="text/css">
        #explain {WIDTH: 100%;MARGIN-TOP: 0px; }
        .fixColleft {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; }
        .fix_hidden {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; overflow:hidden; white-space:nowrap;}
        .headline { font-size:16px; background:url(/images/bg_function.png) no-repeat;padding:0 0 10px 45px; margin:20px 5px 0;}
    </style>
</head>
<body id="none">

    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <DIV class="w main">

        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

<DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>出境及国内旅游线路管理</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>出境及国内旅游线路管理</STRONG>
    </DIV>
    <div class="serchbar">
        类型：<asp:DropDownList ID="DropDownList1" runat="server" DataTextField="ProductName" DataValueField="MisClassId">
              </asp:DropDownList> <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="0">销售</asp:ListItem>
            <asp:ListItem Value="1">暂停</asp:ListItem>
            <asp:ListItem Value="all">全部</asp:ListItem>
              </asp:DropDownList> &nbsp; 线路名称：<asp:TextBox ID="tb_cname" runat="server" Width="150"></asp:TextBox>&nbsp;
              
              <asp:CheckBox ID="CheckBox3" runat="server" Text="有开班" /> &nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        
    </div>
    <div class="toolbar">
        <asp:CheckBox ID="CheckBox1" runat="server" class=hide Text="优惠推荐" /> &nbsp;
        <asp:CheckBox ID="CheckBox2" runat="server" Text="专家推荐" /> &nbsp;
        <%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-redo" onclick="DoIt('Top')">置顶</a>--%>
		<%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-remove" onclick="DoIt('Preferences')">特惠推荐</a>--%>
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-ok" onclick="DoIt('Recommend')">专家推荐</a>
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-edit" onclick="CreateSort()">生成展现数据</a>
		<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		<%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>--%>
        <%--<a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DoIt('CancelPreferences')">取消特惠</a>--%>
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-cancel" onclick="DoIt('CancelRecommend')">取消专家</a>
        <DIV id="inputs" style="DISPLAY:none">
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
        <asp:ScriptManager ID="sm1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <DIV id=explain class="datagrid">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="822px" AllowPaging="True" PageSize="20" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="EditTime" SortDirection="DESC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "MisLineId") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TypeName" HeaderText="类型" SortExpression="LineClass">
		                    <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="旅游线路名称" SortExpression="LineName">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="30%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PlanDate" HeaderText="开班" SortExpression="PlanDate" DataFormatString="{0:yy-MM-dd}">
		                    <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="价格" SortExpression="Price">
		                    <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="优惠" SortExpression="Preferences" Visible="False">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="专家" SortExpression="Recommend" Visible="False">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="目的地" SortExpression="FirstDestination">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="景点" SortExpression="viewids">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
		            </Columns>
                    <PagerTemplate>
                        <div class="pagination"><span class="pagecounts">
                            第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;&nbsp;
                            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="首页"><img src="/images/icons/pagination_first.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="上一页"><img src="/images/icons/pagination_prev.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="下一页"><img src="/images/icons/pagination_next.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="尾页"><img src="/images/icons/pagination_last.gif" alt="" /></asp:LinkButton>&nbsp;&nbsp;
                                到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
                            <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
                    </PagerTemplate>
	                <PagerStyle BackColor="#F7F7F7" ForeColor="#333333" HorizontalAlign="right" Font-Size="12px" />
	                <HeaderStyle BackColor="#4BA41B" Font-Bold="True" ForeColor="White" />
	                <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </DIV>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView_Refresh_Button" />
            <asp:AsyncPostBackTrigger ControlID="GridView_Serch_Button" />
        </Triggers>
        </asp:UpdatePanel>
        <SCRIPT language="javascript" src="../Scripts/NowrapSet.js"></SCRIPT>
    </form>
</DIV>
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        $(function () {
            var objSelect = document.getElementById("DropDownList1");
            checkUserRight(objSelect);
            $('#DropDownList1').change(function () {
                $('#GridView_Serch_Button').click()
            });
        });

//        function Cruises(id) {
//            var url = "../Cruises/CruisesLineSet.aspx?Id=" + id;
//            //alert("aa");
//            var dg = new $.dialog({ id: 'No1', page: url, title: '包船线路设置', width: 400, height: 280, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
//            dg.ShowDialog();
        //        }
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        function checkUserRight(objSelect) {
            var LineType = getQueryString("LineType");
            var userRight = '<%=Session["Manager_UserRight"] %>';
            if (LineType == 'OutBound') {
                if (userRight.indexOf('$1$0') < 0) {
                    jsRemoveItemFromSelect(objSelect, "全部类型");
                    if (userRight.indexOf('$1$1') < 0) {
                        jsRemoveItemFromSelect(objSelect, "东南亚");
                    }
                    if (userRight.indexOf('$1$2') < 0) {
                        jsRemoveItemFromSelect(objSelect, "美加");
                    }
                    if (userRight.indexOf('$1$3') < 0) {
                        jsRemoveItemFromSelect(objSelect, "欧洲");
                    }
                    if (userRight.indexOf('$1$4') < 0) {
                        jsRemoveItemFromSelect(objSelect, "南美");
                    }
                    if (userRight.indexOf('$1$5') < 0) {
                        jsRemoveItemFromSelect(objSelect, "港澳");
                    }
                    if (userRight.indexOf('$1$6') < 0) {
                        jsRemoveItemFromSelect(objSelect, "台湾");
                    }
                    if (userRight.indexOf('$1$7') < 0) {
                        jsRemoveItemFromSelect(objSelect, "日韩");
                    }
                    if (userRight.indexOf('$1$8') < 0) {
                        jsRemoveItemFromSelect(objSelect, "澳新");
                    }
                    if (userRight.indexOf('$1$9') < 0) {
                        jsRemoveItemFromSelect(objSelect, "中东非洲");
                    }
                    if (userRight.indexOf('$1$10') < 0) {
                        jsRemoveItemFromSelect(objSelect, "德铁欧洲");
                    }
                    if (objSelect.options.length == 0) {
                        var varItem = new Option("无权限", "-1");
                        objSelect.options.add(varItem);
                    }
                }
                
            }
            if (LineType == 'InLand') {
                if (userRight.indexOf('$2$0') < 0) {
                    jsRemoveItemFromSelect(objSelect, "全部类型");
                    if (userRight.indexOf('$2$1') < 0) {
                        jsRemoveItemFromSelect(objSelect, "华南");
                    }
                    if (userRight.indexOf('$2$2') < 0) {
                        jsRemoveItemFromSelect(objSelect, "华东");
                    }
                    if (userRight.indexOf('$2$3') < 0) {
                        jsRemoveItemFromSelect(objSelect, "华中");
                    }
                    if (userRight.indexOf('$2$4') < 0) {
                        jsRemoveItemFromSelect(objSelect, "北方");
                    }
                    if (userRight.indexOf('$2$5') < 0) {
                        jsRemoveItemFromSelect(objSelect, "西南");
                    }
                    if (userRight.indexOf('$2$6') < 0) {
                        jsRemoveItemFromSelect(objSelect, "西北");
                    }
                }
            }
            if (LineType == 'FreeTour') {
                if (userRight.indexOf('$3$0') < 0) {
                    jsRemoveItemFromSelect(objSelect, "全部类型");
                    if (userRight.indexOf('$3$1') < 0) {
                        jsRemoveItemFromSelect(objSelect, "国内");
                    }
                    if (userRight.indexOf('$3$2') < 0) {
                        jsRemoveItemFromSelect(objSelect, "港澳台");
                    }
                    if (userRight.indexOf('$3$3') < 0) {
                        jsRemoveItemFromSelect(objSelect, "日韩");
                    }
                    if (userRight.indexOf('$3$4') < 0) {
                        jsRemoveItemFromSelect(objSelect, "东南亚");
                    }
                    if (userRight.indexOf('$3$5') < 0) {
                        jsRemoveItemFromSelect(objSelect, "美加");
                    }
                    if (userRight.indexOf('$3$6') < 0) {
                        jsRemoveItemFromSelect(objSelect, "欧洲");
                    }
                    if (userRight.indexOf('$3$7') < 0) {
                        jsRemoveItemFromSelect(objSelect, "中东非洲");
                    }
                    if (userRight.indexOf('$3$8') < 0) {
                        jsRemoveItemFromSelect(objSelect, "澳新");
                    }
                    if (userRight.indexOf('$3$9') < 0) {
                        jsRemoveItemFromSelect(objSelect, "FIT");
                    }
                }
            }
            if (LineType == 'Cruises') {
                if (userRight.indexOf('$4$0') < 0) {
                    jsRemoveItemFromSelect(objSelect, "全部类型");
                    if (userRight.indexOf('$4$1') < 0) {
                        jsRemoveItemFromSelect(objSelect, "日韩航线");
                    }
                    if (userRight.indexOf('$4$2') < 0) {
                        jsRemoveItemFromSelect(objSelect, "东南亚线");
                    }
                    if (userRight.indexOf('$4$3') < 0) {
                        jsRemoveItemFromSelect(objSelect, "台湾航线");
                    }
                    if (userRight.indexOf('$4$4') < 0) {
                        jsRemoveItemFromSelect(objSelect, "欧美航线");
                    }
                }
            }

            if (LineType == 'Visa') {
                if (userRight.indexOf('$5$0') < 0) {
                    jsRemoveItemFromSelect(objSelect, "全部类型");
                    if (userRight.indexOf('$5$1') < 0) {
                        jsRemoveItemFromSelect(objSelect, "亚洲");
                    }
                    if (userRight.indexOf('$5$2') < 0) {
                        jsRemoveItemFromSelect(objSelect, "入台证");
                    }
                    if (userRight.indexOf('$5$3') < 0) {
                        jsRemoveItemFromSelect(objSelect, "欧洲");
                    }
                    if (userRight.indexOf('$5$4') < 0) {
                        jsRemoveItemFromSelect(objSelect, "美洲");
                    }
                    if (userRight.indexOf('$5$5') < 0) {
                        jsRemoveItemFromSelect(objSelect, "中东非洲");
                    }
                    if (userRight.indexOf('$5$6') < 0) {
                        jsRemoveItemFromSelect(objSelect, "大洋洲");
                    }
                }
            }
        }

        function jsRemoveItemFromSelect(objSelect, objItemValue) {
            //判断是否存在        
            if (jsSelectIsExitItem(objSelect, objItemValue)) {
                for (var i = 0; i < objSelect.options.length; i++) {
                    if (objSelect.options[i].text == objItemValue) {
                        objSelect.options.remove(i);
                        break;
                    }
                }
            }
        }

        function jsSelectIsExitItem(objSelect, objItemValue) {
            var isExit = false;
            for (var i = 0; i < objSelect.options.length; i++) {
                if (objSelect.options[i].text == objItemValue) {
                    isExit = true;
                    break;
                }
            }
            return isExit;
        }

        function Cruises(id) {
            var url = "../Cruises/CruisesLineSet.aspx?Cid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '包船线路设置', width: 600, height: 400, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function EditDes(id) {
            var url = "LineDestination.aspx?Cid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '线路目的地设置', width: 600, height: 400, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function EditViews(id) {
            var url = "LineDestination.aspx?flag=view&Cid=" + id;
            var dg = new $.dialog({ id: 'No1', page: url, title: '景点设置', width: 600, height: 400, skin: 'aero', btnBar: false, iconTitle: false, cover: true });
            dg.ShowDialog();
        }

        function chkall(obj) {
            if (obj.checked) {
                $("input[name$='CheckBox']").each(function () { this.checked = true; });
            } else {
                $("input[name$='CheckBox']").each(function () { this.checked = false; });
            }
        }

        function allchks() {
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            //获取name为check的一组元素,然后选取它们中选中(checked)的。
            if (items.length == 0) {
                return false;
            }
            //alert("选中的个数为：" + items.length)
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            alert(arrChk);
        }

        function CreateSort() {
            var url = "AjaxService.aspx?action=ManageLine&DoFlag=CreatePreferencesJs&r=" + Math.random();
            //window.open(url);
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>Js文件创建成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
                else {
                    jError('<strong>Js文件创建失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            });
        }

        function DoIt(DoFlag) {
            if ($("#DropDownList1").val() == "全部类型") {
                jNotify('<strong>为避免误操作，请选择类型后再操作!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            var arrChk = "";
            var items = $("input[name='CheckBox']:checked");
            if (items.length == 0) {
                jNotify('<strong>请选择要操作的数据!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                return false;
            }
            $("input[name$='CheckBox']:checked").each(function () { arrChk += this.value + ','; });
            arrChk = arrChk.substr(0, arrChk.length - 1);
            var url = "AjaxService.aspx?action=ManageLine&LineType=<%=Types %>&DoFlag=" + DoFlag + "&LineClass=" + $("#DropDownList1").val() + "&Id=" + arrChk + "&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    jSuccess('<strong>操作成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    $('#GridView_Refresh_Button').click();
                } else {
                    jError('<strong>操作失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                }
            })
        }

</script>
</body>
</html>


