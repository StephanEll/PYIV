using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;

namespace PYIV.Gameplay.Character
{
  [Serializable]
  public class IndianData : IIdentifiable
  {
    [XmlElement()]
    public string Id { get; set; }

    [XmlElement()]
    public string Name { get; set; }

    [XmlElement()]
    public string Description { get; set; }

    [XmlElement()]
    public string SpriteImageName { get; set; }

    [XmlElement()]
    public int Stamina { get; set; }

    [XmlElement()]
    public int Strength { get; set; }

    [XmlElement()]
    public int Accuracy { get; set; }

    [XmlElement()]
    public string ShotBehaviourClassName { get; set; }

    [XmlElement()]
    public string PreafabPath { get; set; }

    [XmlElement()]
    public string BulletPreafabPath { get; set; }

    [XmlElement()]
    public string BackgroundPreafabPath { get; set; }

    [XmlElement()]
    public string GameSound { get; set; }

    [XmlElement()]
    public Vector3 ColorateEnemys { get; set; }
    
    public override string ToString()
    {
      return string.Format("[IndianData: Id={0}, Name={1}, Description={2}, SpriteImageName={3}, Stamina={4}, Strength={5}, Accuracy={6}, ShotBehaviourClassName={7}, PreafabPath={8}, BulletPreafabPath={9}, BackgroundPreafabPath={10}]", Id, Name, Description, SpriteImageName, Stamina, Strength, Accuracy, ShotBehaviourClassName, PreafabPath, BulletPreafabPath, BackgroundPreafabPath);
    }
  }
}
