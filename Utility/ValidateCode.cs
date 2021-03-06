﻿using System;
using System.Drawing;

namespace Utility
{
    public class ValidateCode
    {
        //将字符串生成为图片格式
        public static void CreateCheckCodeImage()
        {
            string checkCode = RandomHelper.GenNumAndChar();
            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 20);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线
                for (int i = 0; i < 2; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);
                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                g.Dispose();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(WebConfig.ValidateImgPath, System.Drawing.Imaging.ImageFormat.Gif);
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }

}
