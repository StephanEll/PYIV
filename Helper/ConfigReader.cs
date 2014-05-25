using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace PYIV.Helper
{

    public class ConfigReader : SingletonBase<ConfigReader>
    {

        private const string URI = ".\\Assets\\Scripts/_configs/config.xml";

        private XPathNavigator navigator;

        public ConfigReader()
        {
            XPathDocument document = new XPathDocument(URI);
            navigator = document.CreateNavigator();
        }

        

        public string GetSetting(string group, string key)
        {
            return (string) navigator.SelectSingleNode("//" + group + "/" + key).GetAttribute("value", "");
        }
    }
}