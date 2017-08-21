using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Common;
using TravelOnline.Destination.Class;
using TravelOnline.Class.Travel;

namespace TravelOnline.Destination
{
    public partial class SightDetail : System.Web.UI.Page
    {
        public string flag, id, pagetitle, cname, ename, breadcrumb, map_x, map_y, map_size, MapScript, MapScriptFile, PlacePicUrl, ShowPic, NavString, NewsContent;
        public string HotPlaceView, HotDestination, desid, desname, PlaceTitle, PlaceUrl, PlaceList, ViewInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            if (id != null)
            {
                LoadInfo();
            }
            else
            {

            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select *,(select DestinationName from OL_Destination where id=OL_View.desid) as DestinationName,(select ClassPath from OL_Destination where id=OL_View.desid) as ClassPath from OL_View where id='{0}'", id);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                StringBuilder Strings = new StringBuilder();
                
                id = DS.Tables[0].Rows[0]["id"].ToString();
                pagetitle = DS.Tables[0].Rows[0]["SeoName"].ToString();
                if (pagetitle == "") pagetitle = DS.Tables[0].Rows[0]["viewname"].ToString();
                cname = DS.Tables[0].Rows[0]["viewname"].ToString();
                desid = DS.Tables[0].Rows[0]["desid"].ToString();
                desname = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                map_x = DS.Tables[0].Rows[0]["map_x"].ToString();
                map_y = DS.Tables[0].Rows[0]["map_y"].ToString();
                map_size = DS.Tables[0].Rows[0]["map_size"].ToString();

                Strings.Append(string.Format("<li><dl><dt>联系电话：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["tel"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>地  址：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["address"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>开放时间：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["opentime"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>最佳游览季节：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["visitseason"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>一般游览用时：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["visittime"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>门票信息：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["ticket"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>票价备注：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["ticketmemo"].ToString()));
                Strings.Append(string.Format("<li><dl><dt>景点分类：</dt><dd><div>{0}</div></dd></dl></li>", DS.Tables[0].Rows[0]["tags"].ToString()));
                ViewInfo = Strings.ToString();
                Strings.Clear();

                PlaceTitle = desname + "热门景点";
                PlaceUrl = desname + "景点";
                PlaceList = PlaceClass.DestinationCache("PlaceViewList", "", desid, 10, "");

                HotDestination = PlaceClass.DestinationCache("PlaceHotDestination", "", "", 45, "");
                HotPlaceView = PlaceClass.DestinationCache("PlaceHotView", "", "", 45, "");
                breadcrumb = "";


                SqlQueryText = string.Format("select * from OL_Destination where ClassLevel>1 and id in ({0},{1}) order by ClassLevel", DS.Tables[0].Rows[0]["ClassPath"].ToString(), DS.Tables[0].Rows[0]["desid"].ToString());
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                    {
                        breadcrumb += string.Format("<li><a href=\"/place/{0}.html\">{1}</a> <span class=\"divider\">/</span></li>", DS1.Tables[0].Rows[i]["id"].ToString(), DS1.Tables[0].Rows[i]["DestinationName"].ToString());
                    }
                }

                breadcrumb += string.Format("<li class=\"active\">{0}</li", cname);
                
                SqlQueryText = "SELECT top 5 * from OL_ViewPic where viewid = '" + id + "' order by newId()";
                DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<ul>");
                    string ThumbSrc = "", PicUrl = "";
                    string[] sArray;
                    for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                    {
                        sArray = DS1.Tables[0].Rows[i]["picurl"].ToString().Split('/');
                        PicUrl = string.Format("src=\"/Upload/View/{0}/{1}/M_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                        ThumbSrc = string.Format("thumb=\"/Upload/View/{0}/{1}/S_{2}\"", sArray[3].ToString(), sArray[4].ToString(), sArray[5].ToString());
                        Strings.Append(string.Format("<li><a href=\"javascript:void(0);\"><img {0} /></a></li>",
                            PicUrl
                        ));
                    }
                    Strings.Append("</ul>");
                    PlacePicUrl = Strings.ToString();
                    ShowPic = "yes";
                }
                else
                {
                    PlacePicUrl = "<ul><li><a href=\"javascript:void(0);\"><img src=\"/images/none_view.gif\" /></a></li></ul>";
                    ShowPic = "yes";
                }

                Strings.Clear();
                NavString = "";
                NavString += string.Format("<li class=\"current\"><a href=\"#summarys_1\">{0}</a></li>", "景点介绍");
                Strings.Append("<DIV class=mtc id=\"summarys_1\"><STRONG>景点介绍</STRONG><DIV class=extra></DIV></DIV>");
                Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[0]["intro"].ToString()));

                int count = 0;
                if (DS.Tables[0].Rows[0]["viewpoint"].ToString().Length > 10)
                {
                    count = count+1;
                    NavString += string.Format("<li><a href=\"#summarys_2\">{0}</a></li>", "看点推荐");
                    Strings.Append("<DIV class=mtc id=\"summarys_2\"><STRONG>看点推荐</STRONG><DIV class=extra></DIV></DIV>");
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[0]["viewpoint"].ToString()));
                }

                if (DS.Tables[0].Rows[0]["traffic"].ToString().Length > 10)
                {
                    count = count + 1;
                    NavString += string.Format("<li><a href=\"#summarys_3\">{0}</a></li>", "景点交通");
                    Strings.Append("<DIV class=mtc id=\"summarys_3\"><STRONG>景点交通</STRONG><DIV class=extra></DIV></DIV>");
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[0]["traffic"].ToString()));
                }

                if (DS.Tables[0].Rows[0]["memo"].ToString().Length > 10)
                {
                    count = count + 1;
                    NavString += string.Format("<li><a href=\"#summarys_4\">{0}</a></li>", "特别提示");
                    Strings.Append("<DIV class=mtc id=\"summarys_4\"><STRONG>特别提示</STRONG><DIV class=extra></DIV></DIV>");
                    Strings.Append(string.Format("<DIV class=mct>{0}</DIV>", DS.Tables[0].Rows[0]["memo"].ToString()));
                }
                
                if (map_x.Length > 2)
                {
                    count = count + 1;
                    if (MyConvert.ConToInt(map_size) == 0) map_size = "12";
                    NavString += string.Format("<li><a href=\"#summarys_6\">{0}</a></li>", "景点地图");
                    Strings.Append("<DIV class=mtc id=\"summarys_6\"><STRONG>景点地图</STRONG><DIV class=extra></DIV></DIV>");
                    Strings.Append("<DIV class=mct><div id=\"allmap\"></div></DIV>");
                    MapScriptFile = "<script type=\"text/javascript\" src=\"http://api.map.baidu.com/api?v=2.0&ak=1hBsSkjOVNVr7WwRs0tqwMTl\"></script>";
                    MapScript = string.Format("<script type=\"text/javascript\">var map = new BMap.Map(\"allmap\");map.centerAndZoom(new BMap.Point({0}, {1}), {2});map.addControl(new BMap.NavigationControl());map.addControl(new BMap.ScaleControl());map.addControl(new BMap.OverviewMapControl());map.enableScrollWheelZoom();map.addControl(new BMap.MapTypeControl());</script>", map_x, map_y, map_size);
                }

                NavString += "<li class=\"last" + count + "\"></li>";
                NewsContent = Strings.ToString();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

    }
}