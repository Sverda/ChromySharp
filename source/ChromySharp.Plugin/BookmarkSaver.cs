using LaunchySharp;
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
            if (bookmark.Icon is null)
            {
                return;
            }

            var iconPath = BookmarkIconLocalPath(bookmark);
            File.WriteAllBytes(iconPath, bookmark.Icon.Data);
        }

        public string BookmarkIconLocalPath(Bookmark bookmark) => Path.Combine(BookmarksIconsPath, $"{bookmark.Icon.FileName}");

        private string BookmarksIconsPath => Path.Combine(_launchyPaths.getIconsPath(), "bookmarks");
    }
}
