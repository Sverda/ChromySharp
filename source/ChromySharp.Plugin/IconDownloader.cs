using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace ChromySharp.Plugin
{
    internal static class IconDownloader
    {
        public static void DownloadFromUrl(Bookmark bookmark)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var web = new HtmlWeb();
            var html = web.Load(bookmark.Url);
            var node = html.DocumentNode.SelectSingleNode("/html/head/link[@rel='icon' and @href]")
                       ?? html.DocumentNode.SelectSingleNode("/html/head/link[@rel='shortcut icon' and @href]")
                       ?? html.DocumentNode.SelectSingleNode("/html/head/link[@rel='SHORTCUT ICON' and @href]");

            var faviconUrl = node is null
                ? Icon.GetDefaultIconUrl(bookmark.Url)
                : node.Attributes["href"].Value;
            faviconUrl = RemoveOverlappingBackslash(bookmark, faviconUrl);

            var request = WebRequest.Create(faviconUrl);
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            _ = stream ?? throw new ArgumentNullException(nameof(stream), "Icon data has not been found");

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                bookmark.Icon = new Icon
                {
                    FileName = $"{bookmark.Name}.{faviconUrl.GetExtension()}",
                    Data = memoryStream.ToArray()
                };
            }
        }

        private static string RemoveOverlappingBackslash(Bookmark bookmark, string favicon)
        {
            if (!favicon.StartsWith("/"))
            {
                return favicon;
            }

            if (bookmark.Url.EndsWith("/"))
            {
                favicon = favicon.TrimStart('/');
            }

            return $"{bookmark.Url}{favicon}";
        }
    }
}
