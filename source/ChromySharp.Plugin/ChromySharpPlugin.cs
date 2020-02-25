using LaunchySharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChromySharp.Plugin
{
    public class ChromySharpPlugin : IPlugin
    {
        private IPluginHost _pluginHost;
        private ICatItemFactory _catFactory;
        private IEnumerable<Bookmark> _bookmarks;
        private const string _pluginName = "ChromySharp";

        public void init(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost;
            _catFactory = _pluginHost?.catItemFactory();
            _bookmarks = BookmarksReader.GetBookmarks();
        }

        public uint getID()
        {
            return _pluginHost.hash(_pluginName);
        }

        public string getName()
        {
            return _pluginName;
        }

        public void getLabels(List<IInputData> inputDataList)
        {
        }

        public void getResults(List<IInputData> inputDataList, List<ICatItem> resultsList)
        {
            if (!inputDataList.Any(i => _bookmarks.Any(b => b.Name == i.getText())))
            {
                return;
            }

            var urlNamePairs = _bookmarks.Where(b => inputDataList.Any(i => i.getText() == b.Name));
            //TODO: Use icon downloaded from url
            resultsList.AddRange(urlNamePairs.Select(pair => _catFactory.createCatItem(pair.Url, pair.Name, getID(), getName())));
        }

        public void getCatalog(List<ICatItem> catalogItems)
        {
            catalogItems.AddRange(_bookmarks.Select(pair => _catFactory.createCatItem(pair.Url, pair.Name, getID(), getName())));
        }

        public void launchItem(List<IInputData> inputDataList, ICatItem item)
        {
            var catItem = inputDataList[inputDataList.Count - 1].getTopResult();
            System.Diagnostics.Process.Start(item.getFullPath());
        }

        public bool hasDialog()
        {
            return true;
        }

        public IntPtr doDialog()
        {
            return IntPtr.Zero;
        }

        public void endDialog(bool acceptedByUser)
        {
        }

        public void launchyShow()
        {
        }

        public void launchyHide()
        {
        }

        public void setPath(string pluginPath)
        {

        }
    }
}
