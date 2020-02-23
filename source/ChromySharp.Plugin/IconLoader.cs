using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace ChromySharp.Plugin
{
    internal static class IconLoader
    {
        public static void LoadFromUrl(Bookmark bookmark)
        {
            Stream stream;
            var iconPath = string.Empty;

            var url = bookmark.GetDefaultIconPath();
            var request = WebRequest.Create(url);
            var response = request.GetResponse();

            if (response.ContentLength > 0)
            {
                stream = response.GetResponseStream();
            }
            else
            {
                var doc = GetHtml(bookmark.Url);
                var collect = doc.DocumentNode.SelectNodes("//foo:link");
                foreach (var element in collect)
                {
                    if (element.GetAttributeValue("SHORTCUT ICON", string.Empty) != string.Empty ||
                        element.GetAttributeValue("shortcut icon", string.Empty) != string.Empty)
                    {
                        iconPath = element.GetAttributeValue("href", string.Empty);
                        break;
                    }
                }

                request = WebRequest.Create(iconPath);
                response = request.GetResponse();
                stream = response.GetResponseStream();
            }

            var result = new byte[stream.Length];
            stream.Read(result, 0, (int)stream.Length);
            bookmark.Icon = result;
        }

        private static HtmlDocument GetHtml(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (var response = request.GetResponse())
            using (var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), System.Text.Encoding.UTF8))
            {
                var result = streamReader.ReadToEnd();
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load(result);
                return htmlDocument;
            }
        }
    }
}
