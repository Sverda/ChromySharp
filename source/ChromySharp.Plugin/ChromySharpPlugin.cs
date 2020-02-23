using LaunchySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChromySharp.Plugin
{
    public class ChromySharpPlugin : IPlugin
    {
        private IPluginHost _pluginHost;
        private ICatItemFactory _catFactory;
        private IEnumerable<(string name, string url)> _bookmarks;
        private const string _pluginName = "ChromeBookmarksPlugin";

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
            if (!inputDataList.Any(i => _bookmarks.Any(b => b.name == i.getText())))
            {
                return;
            }

            var urlNamePairs = _bookmarks.Where(b => inputDataList.Any(i => i.getText() == b.name));
            resultsList.AddRange(urlNamePairs.Select(pair => _catFactory.createCatItem(pair.url, pair.name, getID(), getName())));
        }

        public void getCatalog(List<ICatItem> catalogItems)
        {
            catalogItems.AddRange(_bookmarks.Select(pair => _catFactory.createCatItem(pair.url, pair.name, getID(), getName())));
        }

        public void launchItem(List<IInputData> inputDataList, ICatItem item)
        {
            ICatItem catItem = inputDataList[inputDataList.Count - 1].getTopResult();
            MessageBox.Show("I was asked to launch: " + item.getFullPath());
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
