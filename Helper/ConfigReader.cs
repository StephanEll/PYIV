using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace PYIV.Helper
{

    public class ConfigReader
    {

        private const string URI = ".\\Assets\\Scripts/config.xml";

        private XPathNavigator navigator;

        private static volatile ConfigReader instance;

        private static object syncRoot = new Object();

        private ConfigReader()
        {
            XPathDocument document = new XPathDocument(URI);
            navigator = document.CreateNavigator();
        }

        public static ConfigReader Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ConfigReader();
                        }
                    }
                }

                return instance;
            }
        }

        public string GetSetting(string group, string key)
        {
            return (string) navigator.SelectSingleNode("//" + group + "/" + key).GetAttribute("value", "");
        }
    }
}