using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using System.Xml;
using System.Text;
using System.Configuration;
using TravelOnline.Class.Travel;
using System.Text.RegularExpressions;
using TravelOnline.Class.Purchase;

namespace TravelOnline.Test
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Server.UrlEncode(Request.QueryString["keyword"]));

            Response.Write(Server.UrlDecode(Request.QueryString["keyword"]));
            //if (Request.QueryString["aa"] == null)
            //{
            //    Response.Write(Request.QueryString["keyword"]);
            //}
            //else
            //{
            //    Response.Write("bbb");
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PurchaseAutoRun.OrderAutoAdjuest();
            Response.Write("OK");
            DateTime date;
            date = DateTime.Now;
            Response.Write(date);
            date = date.AddHours(-1);
            Response.Write(date);

            //GetProductClass.BindSortList();
            //Convert.ToString(ConfigurationManager.ConnectionStrings["connstring"]);
            //Response.Write(Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]));

            //string aa = "121@fdsf@gfgd@gfdgfd";
            //Response.Write(aa.Split('@').Length);

            //Response.Write("aabb".IndexOf("33"));
            //Response.Write("aabb".IndexOf("aa"));
            //LineClass.CreateLineList("OutBound", "231");
            //LinePreferences.CreatePreferencesJs();
            //Response.Write(Convert.ToString(HttpContext.Current.Cache[string.Format("LineSpecialRecommend{0}", this.TextBox1.Text)]));
            //Response.Write(MyConvert.ConToInt("6"));
            //DateTime begindate = Convert.ToDateTime("2011-03-25");
            //string aa = string.Format("{0}/{1},", begindate.ToString("yy"), begindate.ToString("MM"));
            //aa = "a";
            //aa = aa.Substring(0, aa.Length - 1);
            //Response.Write(aa);

            //string bb;
            //string keyword = @"\150\164\164\160\72\57\57\154\157\143\141\154\150\157\163\164\72\70\61\60\57\117\165\164\102\157\165\156\144\56\150\164\155\154";//Server.UrlEncode(Request.QueryString["keyword"]);
            //string[] aa = keyword.Split("\\".ToArray());
            //for (int i = 1; i < aa.Length; i++)
            //{
            //    bb = Convert.ToString(2, 2);
            //}

            //Response.Write(Convert.ToChar(150));
            //Response.Write(Convert.ToInt32("h", 16));
            //Response.Write(keyword.Replace("@","/"));

            //List<string> a = new List<string>();
            //a.Add("we");
            //a.Add("bb");
            //a.Add("we");
            //string[] bb = a.ToArray();
            //Response.Write(bb.Length);
            //MessageBox.Show(a[0]);
            //aa.l

            //string[] aa = "SellPrice#@249492#@成人价#@3100.00#@2#@62@00".Split("#@".ToArray());
            //Response.Write(aa.Length);

            //string s = "abcdeabcdeabcde";
            //string[] sArray1 = s.Split(new char[3] { 'c', 'd', 'e' });
            ////Response.Write(sArray1.Length);

            //string s = "abcdeabcdeabcde";
            //string[] sArray1 = s.Split(new char[3] { 'c', 'd', 'e' });
            //foreach (string i in sArray1)
            //    Console.WriteLine(i.ToString());

            //string strBreak = "||";
            //string str = "SellPrice#@249492#@成人价#@3100.00#@2#@62@00";
            //string[] arr1 = Regex.Split(str, @"\#\@", RegexOptions.IgnoreCase);
            //Response.Write(arr1.Length);

            //string[] Branch = { "", "营业总部 衡山路2号", "卢湾营业部 成都南路124号", "张杨路营业部 张杨路1363号(国际华城楼下)", 
            //                    "普陀营业部 武宁路225号西宫内（河边大道4号）","南方商城营业部 沪闵路7388号（南方商城一楼正门）",
            //                    "打浦桥营业部 徐家汇路669号海华商厦120室（打浦路8号）","徐汇营业部 虹桥路808号",
            //                    "虹口营业部 四川北路1755号","莘庄营业部 莘建东路216号","五角场营业部 四平路2500号东方商厦底楼（近黄兴路口）",
            //                    "天山营业部 天山路762号泓鑫时尚广场2楼20室（肯德基旁）","人民广场营业部 黄陂北路228号","洛川东路营业部 共和新路1481号1幢116室"};
            //Response.Write(Branch.ToArray().Length);
            //Response.Write(Branch[5]);

            //Response.Write(string.Format("UPDATE OL_LoginUser set LoginCount=LoginCount+1,LastLoginTime='{0}',LoginIp='{1}' and", DateTime.Now.ToString(), Page.Request.UserHostAddress.ToString()));
        }
    }
}