using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System.Linq;

namespace PYIV.Gameplay.Character {

	[XmlRoot("root")]
	public class IndianDataCollection {
		
        [XmlArray("IndianDataCollection")]
		[XmlArrayItem("IndianData", typeof(IndianData))]
		public IndianData[] IndianData { get; set; }

        private static volatile IndianDataCollection instance;

        private static object syncRoot = new object();


        private IndianDataCollection()
        {
            
        }

        private static IndianDataCollection DeserializeIndianDataCollection(string filename)
        {

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof(IndianDataCollection));

            XmlReader reader = XMLHelper.LoadXMLReaderFromResource(filename);

            // Use the Deserialize method to restore the object's state.
            IndianDataCollection eDC = (IndianDataCollection)serializer.Deserialize(reader);
            return eDC;
        }


        public static IndianDataCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = DeserializeIndianDataCollection(ConfigReader.Instance.GetSetting("directory", "indian-config-xml"));
                        }
                    }
                }

                return instance;
            }
        }

        public IndianData[] GetSubCollection(string[] ids)
        {

            return IndianData.Where(data => Array.IndexOf(ids, data.Id) >= 0).ToArray<IndianData>();

        }
		
		public IndianData GetById(string id){
			return (from indianData in IndianData where indianData.Id == id select indianData).ToArray()[0];
		}
		

	}
	
	




}
