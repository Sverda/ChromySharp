﻿using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;

namespace ChromySharp.Plugin
{
    internal static class IconLoader
    {
        public static void LoadFromUrl(Bookmark bookmark)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var web = new HtmlWeb();
            var html = web.Load(bookmark.Url);
            var node = html.DocumentNode.SelectSingleNode("/html/head/link[@rel='icon' and @href]")
                       ?? html.DocumentNode.SelectSingleNode("/html/head/link[@rel='shortcut icon' and @href]")
                       ?? html.DocumentNode.SelectSingleNode("/html/head/link[@rel='SHORTCUT ICON' and @href]")
                       ?? throw new ArgumentNullException("node", "Icon node has not been found");

            var favicon = node.Attributes["href"].Value;

            var request = WebRequest.Create(favicon);
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            _ = stream ?? throw new ArgumentNullException(nameof(stream), "Icon data has not been found");

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                bookmark.Icon = memoryStream.ToArray();
            }
        }
    }
}
