using System;
using System.Xml;
using System.Xml.XPath;
using UnityEngine;


namespace PYIV.Helper
{
	public static class XMLHelper
	{
		public static XPathDocument LoadXPathDocFromResource(string resourceFolderPath){
			
			
			
            return new XPathDocument(LoadXMLReaderFromResource(resourceFolderPath));
		}
		
		public static XmlReader LoadXMLReaderFromResource(string resourceFolderPath){
			TextAsset textAsset = Resources.Load<TextAsset>(resourceFolderPath);
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(textAsset.text);
			
			return new XmlNodeReader(xmlDoc);
		}
	}
}

