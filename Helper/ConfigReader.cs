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
            return (string) navigator.SelectSingleNode("//" + group + "/" + key).GetAttribute("value", "");
        }

        public float GetSettingAsFloat(string group, string key)
        {
            float setting;
            float.TryParse(GetSetting(group,key), out setting );
            return setting;
        }

        public int GetSettingAsInt(string group, string key)
        {
            int setting;
            int.TryParse(GetSetting(group, key), out setting);
            return setting;
        }
    }
}