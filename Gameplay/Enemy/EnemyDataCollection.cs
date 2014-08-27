using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System.Linq;


namespace PYIV.Gameplay.Enemy
{

  [XmlRoot("root")]
  public class EnemyDataCollection
  {
		
    [XmlArray("EnemyDataCollection")]
    [XmlArrayItem("EnemyData", typeof(EnemyData))]
    public EnemyData[] enemyData { get; set; }

    private static volatile EnemyDataCollection instance;

    private static object syncRoot = new object();


    private EnemyDataCollection()
    {
            
    }
		
		
    private static EnemyDataCollection DeserializeEnemyDataCollection(string filename)
    {

      // Create an instance of the XmlSerializer specifying type and namespace.
      XmlSerializer serializer = new XmlSerializer(typeof(EnemyDataCollection));
						
      XmlReader reader = XMLHelper.LoadXMLReaderFromResource(filename);

      // Use the Deserialize method to restore the object's state.
      EnemyDataCollection eDC = (EnemyDataCollection)serializer.Deserialize(reader);

      return eDC;
    }

    public EnemyData[] GetSubCollection(string[] ids)
    {

      return enemyData.Where(data => Array.IndexOf(ids, data.Id) >= 0).ToArray<EnemyData>();

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
		
    public int MaxLifePoints()
    {
      return enemyData.OrderByDescending(data => data.LivePoints).FirstOrDefault().LivePoints;
    }
		
    public int MaxAttackPower()
    {
      return enemyData.OrderByDescending(data => data.AttackPower).FirstOrDefault().AttackPower;
    }

    public float MaxMoveSpeed()
    {
      return enemyData.OrderByDescending(data => data.MoveSpeed).FirstOrDefault().MoveSpeed;
    }
        

  }



}
