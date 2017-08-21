using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace TravelOnline.Class.Manage
{
    public class CreateAfficheHtml
    {
        public static string CreateAffiche(string flag)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("AfficheHtml{0}", flag)]) == "")
            {
                string SqlQueryText = "";
                switch (flag)
                {
                    case "Index":
                        SqlQueryText = "select top 8 * from OL_Affiche where AfficheType='Index' order by EditTime desc";
                        break;
                    default:
                        SqlQueryText = string.Format("select top 7 * from OL_Affiche where AfficheType='{0}' order by id desc", flag);
                        break;
                }

                DataSet DS = new DataSet();
                DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);

                StringBuilder AdScriptr = new StringBuilder();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        AdScriptr.Append(string.Format("<LI>· <A href=\"/News/{0}.html\" target=_blank>{1}</A></LI>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["AfficheName"].ToString()));
                    }
                }
                HttpContext.Current.Cache.Insert(string.Format("AfficheHtml{0}", flag), AdScriptr.ToString());
                DS.Clear();
                DS.Dispose();
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("AfficheHtml{0}", flag)]);
        }

        public static string CreateFriendLink()
        {
            if (Convert.ToString(HttpContext.Current.Cache["FriendLink"]) == "")
            {
                string SqlQueryText = "";
                SqlQueryText = "select * from OL_FriendLink order by rankid desc";
                        
                DataSet DS = new DataSet();
                DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);

                StringBuilder AdScriptr = new StringBuilder();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        AdScriptr.Append(string.Format("<A href=\"{0}\" target=_blank>{1}</A>", DS.Tables[0].Rows[i]["LinkUrl"].ToString(), DS.Tables[0].Rows[i]["LinkName"].ToString()));
                    }
                }
                HttpContext.Current.Cache.Insert("FriendLink", AdScriptr.ToString());
                DS.Clear();
                DS.Dispose();
            }
            return Convert.ToString(HttpContext.Current.Cache["FriendLink"]);
        }
    }
}