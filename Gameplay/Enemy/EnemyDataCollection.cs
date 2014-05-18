using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy {

	[XmlRoot("root")]
	public class EnemyDataCollection {
		
        [XmlArray("EnemyDataCollection")]
		[XmlArrayItem("EnemyData", typeof(EnemyData))]
		public EnemyData[] enemyData { get; set; }

        private static volatile EnemyDataCollection instance;

        private static object syncRoot = new Object();


        private EnemyDataCollection()
        {
            
        }

        private static EnemyDataCollection DeserializeEnemyDataCollection(string filename)
        {

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof(EnemyDataCollection));

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Use the Deserialize method to restore the object's state.
            EnemyDataCollection eDC = (EnemyDataCollection) serializer.Deserialize(reader);
            fs.Close();
            return eDC;
        }


        public static EnemyDataCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = DeserializeEnemyDataCollection(ConfigReader.Instance.GetSetting("directory", "enemy-config-xml"));
                        }
                    }
                }

                return instance;
            }
        }

	}




}
