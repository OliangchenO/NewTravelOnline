using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace TravelOnline.Class.Manage
{
    public class SaveScriptToFile
    {
        public static void SaveScript(string JsScript, string Directory, string Flag)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\ProductDetail\" + Flag + ".js";
            //string path = string.Format("{0}Js\\{1}\\{2}.js", AppDomain.CurrentDomain.BaseDirectory, Directory, Flag);
            string path = string.Format(@"{0}Js\{1}\{2}.js", AppDomain.CurrentDomain.BaseDirectory, Directory, Flag);

            try
            {
                StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(JsScript);
                writer.Close();
            }
            catch (Exception exception)
            {
                JsScript = exception.Message;
            }
        }
        
        public static void SaveXml(string JsScript, string Directory, string Flag)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\ProductDetail\" + Flag + ".js";
            string path = string.Format(@"{0}XML\{1}\{2}.xml", AppDomain.CurrentDomain.BaseDirectory, Directory, Flag);

            try
            {
                StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(JsScript);
                writer.Close();
            }
            catch (Exception exception)
            {
                JsScript = exception.Message;
            }
        }
    }
}