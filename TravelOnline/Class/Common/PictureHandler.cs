using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;

namespace TestMvc
{
    public static class PictureHandler
    {
        /// <summary>
        /// 图片微缩图处理
        /// </summary>
        /// <param name="srcPath">源图片</param>
        /// <param name="destPath">目标图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void CreateThumbnailPicture(string srcPath, string destPath, int width, int height)
        {
            //根据图片的磁盘绝对路径获取 源图片 的Image对象
            System.Drawing.Image img = System.Drawing.Image.FromFile(srcPath);

            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(width, height);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight)
                        , GraphicsUnit.Pixel);

            ImageFormat format = img.RawFormat;
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(i => i.FormatID == format.Guid);
            EncoderParameter param = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = param;
            img.Dispose();

            //保存已生成微缩图，这里将GIF格式转化成png格式。
            if (format == ImageFormat.Gif)
            {
                destPath = destPath.ToLower().Replace(".gif", ".png");
                bmp.Save(destPath, ImageFormat.Png);
            }
            else
            {
                if (info != null)
                {
                    bmp.Save(destPath, info, parameters);
                }
                else
                {

                    bmp.Save(destPath, format);
                }
            }

            img.Dispose();
            g.Dispose();
            bmp.Dispose();
        }


        /**/
        /// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        public static void MakeThumbnail(System.Drawing.Image orgImage, string thumbnailPath, int orgwidth, int orgheight, int towidth, int toheight, string mode)
        {
            float ratio = 1;
            int thumbwidth = 0;
            int thumbheight = 0;

            int x = 0;
            int y = 0;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形
                    thumbwidth = towidth;
                    thumbheight = toheight;
                    break;
                case "W"://指定宽，高按比例                     
                    ratio = (float)orgheight / orgwidth;
                    thumbwidth = orgwidth <= towidth ? orgwidth : towidth;
                    thumbheight = orgwidth <= towidth ? orgheight : Convert.ToInt32(towidth * ratio);
                    break;
                case "H"://指定高，宽按比例 
                    ratio = (float)orgwidth / orgheight;
                    thumbwidth = orgheight <= towidth ? orgwidth : Convert.ToInt32(towidth * ratio);
                    thumbheight = orgheight <= towidth ? orgheight : towidth;
                    break;
                case "Cut"://指定高宽裁减（不变形）                 
                    //if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    //{
                    //    oh = originalImage.Height;
                    //    ow = originalImage.Height * towidth / toheight;
                    //    y = 0;
                    //    x = (originalImage.Width - ow) / 2;
                    //}
                    //else
                    //{
                    //    ow = originalImage.Width;
                    //    oh = originalImage.Width * height / towidth;
                    //    x = 0;
                    //    y = (originalImage.Height - oh) / 2;
                    //}
                    break;
                default:
                    break;
            }

            System.Drawing.Image img = orgImage;

            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(thumbwidth, thumbheight);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, thumbwidth, thumbheight), new System.Drawing.Rectangle(x, y, orgwidth, orgheight)
                        , GraphicsUnit.Pixel);

            //g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumbwidth, thumbheight),
            //    new System.Drawing.Rectangle(x, y, orgwidth, orgheight),
            //    System.Drawing.GraphicsUnit.Pixel);

            ImageFormat format = img.RawFormat;
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(i => i.FormatID == format.Guid);
            EncoderParameter param = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = param;

            try
            {
                if (format == ImageFormat.Gif)
                {
                    thumbnailPath = thumbnailPath.ToLower().Replace(".gif", ".png");
                    bmp.Save(thumbnailPath, ImageFormat.Png);
                }
                else
                {
                    if (info != null)
                    {
                        bmp.Save(thumbnailPath, info, parameters);
                    }
                    else
                    {

                        bmp.Save(thumbnailPath, format);
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                img.Dispose();
                g.Dispose();
                bmp.Dispose();
            }
            //System.Drawing.Image originalImage = orgImage;
            
            ////新建一个bmp图片 
            //System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumbwidth, thumbheight);

            ////新建一个画板 
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            ////设置高质量插值法 
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            ////设置高质量,低速度呈现平滑程度 
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            ////清空画布并以透明背景色填充 
            //g.Clear(System.Drawing.Color.Transparent);

            ////在指定位置并且按指定大小绘制原图片的指定部分 
            //g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumbwidth, thumbheight),
            //    new System.Drawing.Rectangle(x, y, orgwidth, orgheight),
            //    System.Drawing.GraphicsUnit.Pixel);

            //ImageFormat format = bitmap.RawFormat;
            //try
            //{
            //    //以jpg格式保存缩略图 
            //    //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    if (format == ImageFormat.Gif)
            //    {
            //        thumbnailPath = thumbnailPath.ToLower().Replace(".gif", ".png");
            //        bitmap.Save(thumbnailPath, ImageFormat.Png);
            //    }
            //    else
            //    {
            //        bitmap.Save(thumbnailPath, format);
            //    }
            //}
            //catch (System.Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    img.Dispose();
            //    g.Dispose();
            //    bmp.Dispose();
            //}
        } 
    }
}