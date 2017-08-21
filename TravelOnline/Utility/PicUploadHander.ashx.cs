using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

namespace TestMvc.Utility
{
    /// <summary>
    /// Summary description for PicUploadHander
    /// </summary>
    public class PicUploadHander : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            //验证上传的权限TODO
            string _fileNamePath = "";
            try
            {
                _fileNamePath = context.Request.Files[0].FileName;
                //开始上传
                string _savedFileResult = UpLoadImage(_fileNamePath, context);
                context.Response.Write(_savedFileResult);
            }
            catch
            {
                context.Response.Write("上传提交出错");
            }
        }

        public string UpLoadImage(string fileNamePath, HttpContext context)
        {
            string ChildPath = context.Request.QueryString["ChildPath"];

            string PathSet = context.Request.QueryString["PathSet"]; //保存路径
            string Thumb = context.Request.QueryString["Thumb"]; //是否生成缩略图，为0 不生成，为数字，表示缩略图高度
            string flag = context.Request.QueryString["flag"];
            try
            {
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                string toFilePath = "";
                if (ChildPath != null)
                {
                    toFilePath = Path.Combine(serverPath, string.Format(@"Upload\{0}\{1}\", PathSet, ChildPath)); //string.Format(@"Upload\{0}\", PathSet)
                    PathSet = PathSet + "/" + ChildPath;
                }
                else
                {
                    toFilePath = Path.Combine(serverPath, string.Format(@"Upload\{0}\", PathSet)); //string.Format(@"Upload\{0}\", PathSet)
                }

                if (System.IO.Directory.Exists(toFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(toFilePath);
                }

                //获取要保存的文件信息
                FileInfo file = new FileInfo(fileNamePath);
                //获得文件扩展名
                string fileNameExt = file.Extension;

                //验证合法的文件
                if (CheckImageExt(fileNameExt))
                {
                    //生成将要保存的随机文件名
                    string fileName = GetImageName() + fileNameExt;

                    //获得要保存的文件路径
                    string serverFileName = toFilePath + fileName;
                    //物理完整路径                    
                    string toFileFullPath = serverFileName; //HttpContext.Current.Server.MapPath(toFilePath);

                    //将要保存的完整文件名                
                    string toFile = toFileFullPath;//+ fileName;

                    ///创建WebClient实例       
                    WebClient myWebClient = new WebClient();
                    //设定windows网络安全认证   方法1
                    myWebClient.Credentials = CredentialCache.DefaultCredentials;
                    ////设定windows网络安全认证   方法2
                    context.Request.Files[0].SaveAs(toFile);

                    //上传成功后网站内源图片相对路径
                    string relativePath = System.Web.HttpContext.Current.Request.ApplicationPath
                                          + string.Format(@"Upload/{0}/{1}", PathSet, fileName);

                    if (Thumb == null) //生成缩略图
                    {
                        relativePath = string.Format("{0},{1}", relativePath, relativePath);
                    }
                    else
                    {
                        /*
                        比例处理
                        微缩图高度（DefaultHeight属性值为 400）
                        */
                        int DefaultHeight = Convert.ToInt32(Thumb);
                        System.Drawing.Image img = System.Drawing.Image.FromFile(toFile);
                        int width = img.Width;
                        int height = img.Height;
                        float ratio = (float)width / height;

                        //微缩图高度和宽度
                        int newHeight = height <= DefaultHeight ? height : DefaultHeight;
                        int newWidth = height <= DefaultHeight ? width : Convert.ToInt32(DefaultHeight * ratio);

                        FileInfo generatedfile = new FileInfo(toFile);
                        string newFileName = "Thumb_" + generatedfile.Name;
                        if (flag == "viewpic")
                        {
                            newFileName = "S_" + generatedfile.Name;
                        }
                        
                        string newFilePath = Path.Combine(generatedfile.DirectoryName, newFileName);

                        PictureHandler.CreateThumbnailPicture(toFile, newFilePath, newWidth, newHeight);

                        if (flag == "viewpic")
                        {
                            string new600Name = "M_" + generatedfile.Name;
                            int Height600 = height <= GetHeight600 ? height : GetHeight600;
                            int Width600 = height <= GetHeight600 ? width : Convert.ToInt32(GetHeight600 * ratio);
                            string new600Path = Path.Combine(generatedfile.DirectoryName, new600Name);
                            PictureHandler.CreateThumbnailPicture(toFile, new600Path, Width600, Height600);

                            //string new300Name = "M_" + generatedfile.Name;
                            //int Height300 = height <= GetHeight300 ? height : GetHeight300;
                            //int Width300 = height <= GetHeight300 ? width : Convert.ToInt32(GetHeight300 * ratio);
                            //string new300Path = Path.Combine(generatedfile.DirectoryName, new300Name);
                            //PictureHandler.CreateThumbnailPicture(toFile, new300Path, Width300, Height300);
                        }

                        string thumbRelativePath = System.Web.HttpContext.Current.Request.ApplicationPath
                                              + string.Format(@"Upload/{0}/{1}", PathSet, newFileName);

                        //返回原图和微缩图的网站相对路径
                        relativePath = string.Format("{0},{1}", relativePath, thumbRelativePath);
                    }
                    return relativePath;
                }
                else
                {
                    return "文件格式非法，请上传gif或jpg格式的文件。";
                    //throw new Exception("文件格式非法，请上传gif或jpg格式的文件。");
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Private Methods
        /// <summary>
        /// 检查是否为合法的上传图片
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        public static bool CheckImageExt(string imageExt)
        {
            string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
            //for (int i = 0; i < allowExt.Length; i++)
            //{
            //    if (allowExt[i] == _ImageExt) { return true; }
            //}

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            return allowExt.Any(c => stringComparer.Equals(c, imageExt));

        }

        public static string GetImageName()
        {
            Random rd = new Random();
            StringBuilder serial = new StringBuilder();
            serial.Append(DateTime.Now.ToString("yyyyMMddHHmmssff"));
            serial.Append(rd.Next(0, 999999).ToString());
            return serial.ToString();

        }

        public int GetDefaultHeight
        {
            get
            {
                //此处硬编码了，可以写入配置文件中。
                return 100;
            }
        }
        
        public int GetHeight600
        {
            get
            {
                //此处硬编码了，可以写入配置文件中。
                return 480;
            }
        }

        public int GetHeight300
        {
            get
            {
                //此处硬编码了，可以写入配置文件中。
                return 240;
            }
        }

        #endregion
    }
}