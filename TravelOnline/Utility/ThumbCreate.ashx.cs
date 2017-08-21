using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;
using TravelOnline.GetCombineKeys;
using TestMvc;


namespace TravelOnline.Utility
{
    /// <summary>
    /// ThumbCreate 的摘要说明
    /// </summary>
    public class ThumbCreate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            System.Drawing.Image original_image = null;
            
            try
            {
                string OriginalFileName = context.Request.Files["Filedata"].FileName;//获取文件名
                string FileFex = GetFex(OriginalFileName);//获取后缀名
                string RandomName = GetImageName();

                if (CheckImageExt(FileFex)==false)
                {
                    context.Response.StatusCode = 500;
                    context.Response.Write("An error occured");
                    context.Response.End();
                }

                //创建目录
                string userid = context.Request.QueryString["upid"];
                //context.Request.QueryString["Journalid"] + "/" + 
                string ChildPath = DateTime.Now.ToString("yyMMdd"); //context.Request.QueryString["ChildPath"];
                string PathSet = context.Request.QueryString["PathSet"]; //保存路径
                string SavePathFlag = context.Request.QueryString["PathSet"]; //保存路径
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                string toFilePath = "";
                if (PathSet == "journals")
                {
                    toFilePath = Path.Combine(serverPath, string.Format(@"Upload\{0}\{1}\{2}\", PathSet, userid, ChildPath)); //string.Format(@"Upload\{0}\", PathSet)
                    PathSet = PathSet + "/" + userid + "/" + ChildPath;
                }
                else
                {
                    toFilePath = Path.Combine(serverPath, string.Format(@"Upload\{0}\{1}\", PathSet, ChildPath)); //string.Format(@"Upload\{0}\", PathSet)
                    PathSet = PathSet + "/" + ChildPath;
                }
                

                if (System.IO.Directory.Exists(toFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(toFilePath);
                }

                //生成将要保存的随机文件名
                string SavefileName = System.Web.HttpContext.Current.Request.ApplicationPath + string.Format(@"Upload/{0}/{1}", PathSet, RandomName + FileFex);

                Guid ucode = CombineKeys.NewComb();
                string PicId = "";
                //保存文件到数据库
                if (SavePathFlag == "journals")
                {
                    string SqlQueryText = string.Format("insert into OL_JournalImg (uid,Journalid,userid,originalname,uploadname,inputdate) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                        ucode,
                        context.Request.QueryString["Journalid"],
                        userid,
                        OriginalFileName,
                        SavefileName,
                        DateTime.Now.ToString()
                    );
                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                    PicId = MyDataBaseComm.getScalar("select id from OL_JournalImg where uid='" + ucode + "' and Journalid='" + context.Request.QueryString["Journalid"] + "'");
                }
                //获取上传的图片
                HttpPostedFile jpeg_image_upload = context.Request.Files["Filedata"];

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                // Calculate the new width and height
                int width = original_image.Width;
                int height = original_image.Height;

                if (width < 200 || height < 200)
                {
                    context.Response.StatusCode = 500;
                    context.Response.Write("An error occured");
                    context.Response.End();
                }


                string flag = "W";
                if (height > width) flag = "H";

                if (SavePathFlag == "journals")
                {
                    //原图保存为1200分辨率
                    PictureHandler.MakeThumbnail(original_image, toFilePath + RandomName + FileFex, width, height, 1200, 1200, flag);

                    //在1200图片基础上生成缩略图
                    System.Drawing.Image img = System.Drawing.Image.FromFile(toFilePath + RandomName + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + "_T800" + FileFex, img.Width, img.Height, 800, 800, flag);

                    img = System.Drawing.Image.FromFile(toFilePath + RandomName + "_T800" + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + "_T600" + FileFex, img.Width, img.Height, 600, 600, flag);

                    img = System.Drawing.Image.FromFile(toFilePath + RandomName + "_T600" + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + "_T300" + FileFex, img.Width, img.Height, 300, 300, flag);

                    img = System.Drawing.Image.FromFile(toFilePath + RandomName + "_T300" + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + "_T100" + FileFex, img.Width, img.Height, 100, 100, flag);
                
                }
                else
                {
                    //System.Drawing.Image img = System.Drawing.Image.FromFile(toFilePath + RandomName + FileFex);
                    PictureHandler.MakeThumbnail(original_image, toFilePath + RandomName + "_T800" + FileFex, width, height, 800, 800, flag);

                    //原图保存为600分辨率
                    System.Drawing.Image img = System.Drawing.Image.FromFile(toFilePath + RandomName + "_T800" + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + FileFex, img.Width, img.Height, 600, 600, flag);
                    
                    img = System.Drawing.Image.FromFile(toFilePath + RandomName + FileFex);
                    PictureHandler.MakeThumbnail(img, toFilePath + RandomName + "_T100" + FileFex, img.Width, img.Height, 100, 100, flag);
                
                }
                
                //100,320,640,800,1200
                string thumbRelativePath = "";
                if (width > height)
                {
                    thumbRelativePath = "<div class='uppic'><IMG src='" + System.Web.HttpContext.Current.Request.ApplicationPath + string.Format(@"Upload/{0}/{1}", PathSet, RandomName + "_T100" + FileFex) + "' width='76' tag='" + width + "' picid='" + PicId + "'></div>";
                }
                else
                {
                    thumbRelativePath = "<div class='uppic'><IMG src='" + System.Web.HttpContext.Current.Request.ApplicationPath + string.Format(@"Upload/{0}/{1}", PathSet, RandomName + "_T100" + FileFex) + "' height='76' tag='" + width + "' picid='" + PicId + "'></div>";
                }
                context.Response.StatusCode = 200;
                context.Response.Write(thumbRelativePath);
            }
            catch 
            {
                // If any kind of error occurs return a 500 Internal Server error
                context.Response.StatusCode = 500;
                context.Response.Write("An error occured");
                context.Response.End();
            }
            finally
            {
                // Clean up
                if (original_image != null) original_image.Dispose();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetImageName()
        {
            Random rd = new Random();
            StringBuilder serial = new StringBuilder();
            serial.Append(DateTime.Now.ToString("yyyyMMddHHmmssff"));
            serial.Append(rd.Next(10000, 99999).ToString());
            return serial.ToString();
        }

        private string GetFex(string filename)
        {
            return "." + filename.Substring(filename.LastIndexOf(".") + 1);
        }

        private bool CheckImageExt(string imageExt)
        {
            string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            return allowExt.Any(c => stringComparer.Equals(c, imageExt));

        }
    }
}