using System.Drawing.Imaging;
using System.IO;
using FormsIcon = System.Drawing.Icon;

namespace ChromySharp.Plugin
{
    internal static class IconConverter
    {
        public static void ConvertIconToPngFormat(Icon icon)
        {
            if (!icon.FileName.EndsWith(".ico"))
            {
                return;
            }

            var pngName = icon.FileName.Replace("ico", "png");
            var pngData = ConvertIcoDataToPngData(icon.Data);

            icon.FileName = pngName;
            icon.Data = pngData;
        }

        private static byte[] ConvertIcoDataToPngData(byte[] ico)
        {
            using (var icoStream = new MemoryStream(ico))
            using (var pngStream = new MemoryStream())
            {
                var icon = new FormsIcon(icoStream);
                icon.ToBitmap().Save(pngStream, ImageFormat.Png);
                var png = pngStream.ToArray();
                return png;
            }
        }
    }
}
