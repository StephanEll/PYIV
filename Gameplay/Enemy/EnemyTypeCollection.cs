using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System;
using System.Linq;

namespace PYIV.Gameplay.Enemy {

	[XmlRoot("root")]
	public class EnemyTypeCollection {
		[XmlArray("EnemyTypeCollection")]
		[XmlArrayItem("EnemyType", typeof(EnemyType))]
		public EnemyType[] EnemyType { get; set; }

        private static volatile EnemyTypeCollection instance;

        private static object syncRoot = new object();

        private EnemyTypeCollection()
        {
            
        }

        private static EnemyTypeCollection DeserializeEnemyTypeCollection(string filename)
        {

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof(EnemyTypeCollection));

            XmlReader reader = XMLHelper.LoadXMLReaderFromResource(filename);

            // Use the Deserialize method to restore the object's state.
            EnemyTypeCollection eTC = (EnemyTypeCollection) serializer.Deserialize(reader);

            //!!!!
			eTC.AddEnemyData();
            return eTC;
        }

        private void AddEnemyData()
        {
            Dictionary<string, EnemyData> eDDict = new Dictionary<string, EnemyData>();
            foreach (EnemyData ed in EnemyDataCollection.Instance.enemyData)
            {
                eDDict.Add(ed.Id, ed);
            }

            foreach (EnemyType et in EnemyType)
            {
                EnemyData ed;

                bool foundEdInDict = eDDict.TryGetValue(et.EnemyDataId, out ed);
                if (foundEdInDict)
                {
                    et.EnemyData = ed;
                }
                else
                {
                    Debug.Log("EnemyData not found for ID " + et.EnemyDataId + ". There could be a Problem with the reference object in your XML file");
                }
            }
        }

        public static EnemyTypeCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = DeserializeEnemyTypeCollection(ConfigReader.Instance.GetSetting("directory", "enemy-config-xml"));
                        }
                    }
                }

                return instance;
            }
        }

        public EnemyType[] GetSubCollection(string[] ids)
        {

            return EnemyType.Where(data => Array.IndexOf(ids, data.Id) >= 0).ToArray<EnemyType>();

        }

	}

    
}
