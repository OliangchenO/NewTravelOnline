using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TravelOnline.NewPage.Class;

namespace TravelOnline
{
    public partial class _404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/html";
            string url = Request.RawUrl.ToLower();
            
            if (url.IndexOf(".jpg") > 0 && url.IndexOf("_c_") > 0)
            {
                string[] path_arr = url.Split('.');
                string[] arr = path_arr[0].ToString().Split('/');
                string[] fname = arr[4].ToString().Split('_');
                if (fname[1] != "c" || MyConvert.ConToInt(fname[2]) == 0 || MyConvert.ConToInt(fname[3]) == 0)
                {
                    Response.StatusCode = 404;
                }
                else
                {
                    string FileName = fname[0];
                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                    string FilePath = Path.Combine(serverPath, string.Format(@"images\{0}\{1}\{2}_s.jpg", FileName.Substring(0, 4), FileName.Substring(4, 4), FileName));
                    string ToFilePath = Path.Combine(serverPath, string.Format(@"images\{0}\{1}\{2}_c_{3}_{4}.jpg", FileName.Substring(0, 4), FileName.Substring(4, 4), FileName, fname[2], fname[3]));
                    if (System.IO.File.Exists(FilePath) == false)
                    {
                        Response.StatusCode = 404;
                    }
                    else
                    {
                        Response.ContentType = "image/jpeg";
                        ImageService.CutImage(FilePath, ToFilePath, MyConvert.ConToInt(fname[2]), MyConvert.ConToInt(fname[3]), "");
                        Server.Transfer(string.Format("/images/{0}/{1}/{2}_c_{3}_{4}.jpg", FileName.Substring(0, 4), FileName.Substring(4, 4), FileName, fname[2], fname[3]));
                    }


                }
            }
            else
            {
                Response.StatusCode = 404;
            }
        }
    }
}