using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoCompression
{
    class Program {
        public static void Main()
        {
            VaryQualityLevel();
        }

        private static void VaryQualityLevel()
        {

            List<string> images =new List<string>();
              images.AddRange( Directory.GetFiles(@"D:\images"));
            foreach (var item in images)
            {
                string filename = Path.GetFileName(item);
                using (Bitmap bmp1 = new Bitmap(item))
                {
                    long length = new FileInfo(item).Length;
                    if (length > 614400)// 614400 byte =600 kb
                    {
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                        Encoder myEncoder = Encoder.Quality;

                        EncoderParameters myEncoderParameters = new EncoderParameters(1);

                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 20L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        bmp1.Save(@"D:\images2" + filename, jpgEncoder,myEncoderParameters);//must be different from main source path
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }
}
