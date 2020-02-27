using System;

namespace ChromySharp.Plugin
{
    internal class Bookmark
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public Icon Icon { get; set; }

        public Bookmark(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string GetIconPath(BookmarkSaver bookmarkSaver)
        {
            try
            {
                IconDownloader.DownloadFromUrl(this);
            }
            catch (Exception)
            {
            }
            bookmarkSaver.SaveIconToFile(this);
            return bookmarkSaver.BookmarkIconLocalPath(this);
        }
    }
}
