using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System;
using System.Linq;

namespace PYIV.Gameplay.Enemy
{

  [XmlRoot("root")]
  public class EnemyTypeCollection
  {
    [XmlArray("EnemyTypeCollection")]
    [XmlArrayItem("EnemyType", typeof(EnemyType))]
    public EnemyType[] EnemyTypes { get; set; }

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
      EnemyTypeCollection eTC = (EnemyTypeCollection)serializer.Deserialize(reader);

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

      foreach (EnemyType et in EnemyTypes)
      {
        EnemyData ed;

        bool foundEdInDict = eDDict.TryGetValue(et.EnemyDataId, out ed);
        if (foundEdInDict)
        {
          et.EnemyData = ed;
        } else
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

    /* Gibt EnemyTypes für ein Array von IDs zurück und 
     * summiert für doppelte IDs EnemyType.Count auf so dass keine
     * doppelten EnemyTypes zurück gegeben werden.
     * 
     * @ToDo: Evtl müssen (je nach spawn implementierung )auch EnemyTypes 
     * die das gleiche EnemyData beinhalten aufsummiert werden.
     */

    public EnemyType[] GetSubCollection(string[] ids)
    {

      Dictionary<string, EnemyType> dict = EnemyTypes.ToDictionary(item => item.Id, item => item);

      Dictionary<string, EnemyType> result = new Dictionary<string, EnemyType>();

      foreach (string id in ids)
      {
        EnemyType value;

        if(dict.TryGetValue(id, out value)){

          EnemyType resultValue;

          if(result.TryGetValue(id, out resultValue)){
            resultValue.Count += value.Count;
          } else {
            result.Add(id, value);
          }
        }
      }
      return result.Values.ToArray();

    }

  }

    
}
