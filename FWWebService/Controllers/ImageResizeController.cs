using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FWWebService.Controllers
{
    public class ImageResizeController : AsyncController
    {
        // GET: ImageResize
        public async void IndexAsync(string Uri)
        {
            AsyncManager.OutstandingOperations.Increment();
            var hc = new HttpClient();
            using (var stream = await hc.GetStreamAsync(Uri))
            {
                using (var ms = new MemoryStream())
                {
                    ResizeImage(stream, ms, 320, 180, true, false);
                    var bytes = ms.GetBuffer();
                    AsyncManager.Parameters["imagebytes"] = bytes;
                }
            }

            AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult IndexCompleted(byte[] imagebytes)
        {
            return base.File(imagebytes, "image/jpg");
        }



        private static void ResizeImage(Stream originalStream, Stream newStream, int maximumWidth, int maximumHeight, bool enforceRatio, bool addPadding)
        {
            var image = Image.FromStream(originalStream);
            var format = image.RawFormat;
            var imageEncoders = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            var canvasWidth = maximumWidth;
            var canvasHeight = maximumHeight;
            var newImageWidth = maximumWidth;
            var newImageHeight = maximumHeight;
            var xPosition = 0;
            var yPosition = 0;


            if (enforceRatio)
            {
                var ratioX = maximumWidth / (double)image.Width;
                var ratioY = maximumHeight / (double)image.Height;
                var ratio = ratioX < ratioY ? ratioX : ratioY;
                newImageHeight = (int)(image.Height * ratio);
                newImageWidth = (int)(image.Width * ratio);

                if (addPadding)
                {
                    xPosition = (int)((maximumWidth - (image.Width * ratio)) / 2);
                    yPosition = (int)((maximumHeight - (image.Height * ratio)) / 2);
                }
                else
                {
                    canvasWidth = newImageWidth;
                    canvasHeight = newImageHeight;
                }
            }

            var thumbnail = new Bitmap(canvasWidth, canvasHeight);
            var graphic = Graphics.FromImage(thumbnail);

            if (enforceRatio && addPadding)
            {
                graphic.Clear(Color.White);
            }

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.DrawImage(image, xPosition, yPosition, newImageWidth, newImageHeight);

            thumbnail.Save(newStream, imageEncoders[1], encoderParameters);
        }

    }
}