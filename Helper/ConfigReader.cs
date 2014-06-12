using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace PYIV.Helper
{

    public class ConfigReader : SingletonBase<ConfigReader>
    {

        private string URI = "_configs/config";

        private XPathNavigator navigator;

        public ConfigReader()
        {
			XPathDocument document = XMLHelper.LoadXPathDocFromResource(URI);
            navigator = document.CreateNavigator();
        }

        

        public string GetSetting(string group, string key)
			
        {
			Debug.Log ("Group: "+group + " Key: " + key);
            return (string) navigator.SelectSingleNode("//" + group + "/" + key).GetAttribute("value", "");
        }
    }
}