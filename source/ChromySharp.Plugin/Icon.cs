namespace ChromySharp.Plugin
{
    internal class Icon
    {
        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public static string GetDefaultIconUrl(string url)
        {
            string iconPath;
            if (url.EndsWith("/"))
            {
                iconPath = $"{url}favicon.ico";
            }
            else
            {
                iconPath = $"{url}/favicon.ico";
            }

            return iconPath;
        }
    }
}
