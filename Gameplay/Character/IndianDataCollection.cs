using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System;
using System.Linq;

namespace PYIV.Gameplay.Character {

	[XmlRoot("root")]
	public class IndianDataCollection {
		
        [XmlArray("IndianDataCollection")]
		[XmlArrayItem("IndianData", typeof(IndianData))]
		public IndianData[] indianData { get; set; }

        private static volatile IndianDataCollection instance;

        private static object syncRoot = new Object();


        private IndianDataCollection()
        {
            
        }

        private static IndianDataCollection DeserializeIndianDataCollection(string filename)
        {

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof(IndianDataCollection));

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Use the Deserialize method to restore the object's state.
            IndianDataCollection eDC = (IndianDataCollection)serializer.Deserialize(reader);
            fs.Close();
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

            return indianData.Where(data => Array.IndexOf(ids, data.Id) >= 0).ToArray<IndianData>();

        }

	}




}
