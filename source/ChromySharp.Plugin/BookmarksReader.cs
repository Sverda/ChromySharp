using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChromySharp.Plugin
{
    internal static class BookmarksReader
    {
        public static IEnumerable<Bookmark> GetBookmarks()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var bookmarksFilePath = Path.Combine(appData, @"Google\Chrome\User Data\Default\Bookmarks");
            var bookmarksJson = File.ReadAllText(bookmarksFilePath);
            var json = JObject.Parse(bookmarksJson);
            var bookmarksUrls = json.SelectTokens("$.roots.bookmark_bar.children[*].url");
            var bookmarksNames = json.SelectTokens("$.roots.bookmark_bar.children[?(@.url)].name");
            return bookmarksNames.Zip(bookmarksUrls, (name, url) => new Bookmark(name.ToString(), url.ToString()));
        }
    }
}
