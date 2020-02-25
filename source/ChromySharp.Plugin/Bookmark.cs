namespace ChromySharp.Plugin
{
    internal class Bookmark
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public byte[] Icon { get; set; }

        public Bookmark(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string GetIconUrl()
        {
            string iconPath;
            if (Url.EndsWith("/"))
            {
                iconPath = $"{Url}favicon.ico";
            }
            else
            {
                iconPath = $"{Url}/favicon.ico";
            }

            return iconPath;
        }

        public string GetIconPath(BookmarkSaver bookmarkSaver)
        {
            bookmarkSaver.SaveIconToFile(this);
            return bookmarkSaver.BookmarkIconFilePath(this);
        }
    }
}
