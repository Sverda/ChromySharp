using LaunchySharp;
using System;
using System.IO;

namespace ChromySharp.Plugin
{
    internal class BookmarkSaver
    {
        private readonly ILaunchyPaths _launchyPaths;

        public BookmarkSaver(ILaunchyPaths launchyPaths)
        {
            _launchyPaths = launchyPaths;
        }

        public void SaveIconToFile(Bookmark bookmark)
        {
            try
            {
                IconLoader.LoadFromUrl(bookmark);
                var iconPath = BookmarkIconFilePath(bookmark);
                File.WriteAllBytes(iconPath, bookmark.Icon);
            }
            catch (ArgumentNullException)
            {
            }
        }

        public string BookmarkIconFilePath(Bookmark bookmark) => Path.Combine(BookmarksIconsPath, $"{bookmark.Name}.ico");

        private string BookmarksIconsPath => Path.Combine(_launchyPaths.getIconsPath(), "bookmarks");
    }
}
