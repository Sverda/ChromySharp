using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChromySharp.Plugin
{
    internal class BookmarksReader
    {
        public IEnumerable<(string name, string url)> GetBookmarks()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var bookmarksFilePath = Path.Combine(appData, @"Google\Chrome\User Data\Default\Bookmarks");
            var bookmarksJson = File.ReadAllText(bookmarksFilePath);
            var json = JObject.Parse(bookmarksJson);
            var bookmarksUrls = json.SelectTokens("$.roots.bookmark_bar.children[*].url");
            var bookmarksNames = json.SelectTokens("$.roots.bookmark_bar.children[?(@.url)].name");
            return bookmarksNames.Zip(bookmarksUrls, (name, url) => (name: name.ToString(), url: url.ToString()));
        }
    }
}
