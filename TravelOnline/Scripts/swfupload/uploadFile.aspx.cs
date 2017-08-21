using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class net_uploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      

	     Response.CacheControl = "no-cache";
         string s_rpath = FileHelper.GetUploadPath();//@"E:\My Documents\Visual Studio 2008\WebSites\SWFUpload\demos\applicationdemo.net";
      
              
                string Datedir = DateTime.Now.ToString("yy-MM-dd"); 
                string updir = s_rpath + "\\" + Datedir;
                if (this.Page.Request.Files.Count > 0)
                {
                    try
                    {

                        for (int j = 0; j < this.Page.Request.Files.Count; j++)
                        {

                            HttpPostedFile uploadFile = this.Page.Request.Files[j];

                            if (uploadFile.ContentLength > 0)
                            {
                                if (!Directory.Exists(updir))
                                {
                                    Directory.CreateDirectory(updir);
                                }
                                string extname = Path.GetExtension(uploadFile.FileName);
                                string fullname=DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+ DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString();
                                string filename = uploadFile.FileName;

                                uploadFile.SaveAs(string.Format("{0}\\{1}", updir, filename));
                                Response.Write(filename);
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Message"+ ex.ToString());
                    }

           
        }
	}
    }

