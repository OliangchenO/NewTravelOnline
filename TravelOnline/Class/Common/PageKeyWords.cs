using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace TravelOnline.Class.Common
{
    public static class PublicPageKeyWords
    {
        public static string PublicTitle = Convert.ToString(ConfigurationManager.AppSettings["PublicTitle"]);
        public static string PublicDescription = "\"" + Convert.ToString(ConfigurationManager.AppSettings["PublicDescription"]) + "\"";
        public static string PublicKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["PublicKeywords"]) + "\"";

        public static string OutBoundKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["OutBoundKeywords"]) + "\"";
        public static string InLandKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["InLandKeywords"]) + "\"";
        public static string CruisesKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["CruisesKeywords"]) + "\"";
        public static string FreeTourKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["FreeTourKeywords"]) + "\"";
        public static string VisaKeywords = "\"" + Convert.ToString(ConfigurationManager.AppSettings["VisaKeywords"]) + "\"";

        public static string OutBoundTitle = Convert.ToString(ConfigurationManager.AppSettings["OutBoundTitle"]);
        public static string InLandTitle = Convert.ToString(ConfigurationManager.AppSettings["InLandTitle"]);
        public static string CruisesTitle = Convert.ToString(ConfigurationManager.AppSettings["CruisesTitle"]);
        public static string FreeTourTitle = Convert.ToString(ConfigurationManager.AppSettings["FreeTourTitle"]);
        public static string VisaTitle = Convert.ToString(ConfigurationManager.AppSettings["VisaTitle"]);
        public static string PublicLineListTitle = Convert.ToString(ConfigurationManager.AppSettings["PublicLineListTitle"]);
        public static string NewPage = Convert.ToString(ConfigurationManager.AppSettings["NewPage"]);
        public static string CruisesRooms = Convert.ToString(ConfigurationManager.AppSettings["CruisesRooms"]);
    }
}