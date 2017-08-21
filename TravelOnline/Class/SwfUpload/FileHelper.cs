using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///FileHelper 的摘要说明
/// </summary>
public class FileHelper
{
	public FileHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
     public static string GetUploadPath()
        {
          string path = HttpContext.Current.Server.MapPath("~/");
          string dirname = GetDirName();
          string uploadDir = path + "\\" + dirname;
          CreateDir(uploadDir);
          return uploadDir;
        }
       
    
        private static string GetDirName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["uploaddir"];
        }

        public static void CreateDir(string path)
        {
                if (!System.IO.Directory.Exists(path))
                {                    
                    System.IO.Directory.CreateDirectory(path);
                }
        }
}