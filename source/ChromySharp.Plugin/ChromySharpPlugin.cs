using LaunchySharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChromySharp.Plugin
{
    public class ChromySharpPlugin : IPlugin
    {
        private IPluginHost _pluginHost;
        private ICatItemFactory _catFactory;
        private const string _pluginName = "ChromeBookmarksPlugin";

        public void init(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost;
            _catFactory = _pluginHost?.catItemFactory();
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
            resultsList.Add(_catFactory.createCatItem("Hello World!", "SimplePlugin", getID(), getName()));
        }

        public void getCatalog(List<ICatItem> catalogItems)
        {
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
