using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace PYIV.Gameplay.Score {



  [Serializable]
  public class FlyNoteData {
    [XmlIgnore()]
    public static string HitsTypeSpecific = "HitsTypeSpecific";
    [XmlIgnore()]
    public static string HitsNotTypeSpecific = "HitsNotTypeSpecific";
    [XmlIgnore()]
    public static string KillsTypeSpecific = "KillsTypeSpecific";
    [XmlIgnore()]
    public static string KillsNotTypeSpecific = "KillsNotTypeSpecific";
    [XmlIgnore()]
    public static string NoSpecialEvent = "NoSpecialEvent";

    [XmlElement()]
    public string Type {get; set; }

    [XmlElement()]
    public int ExtraPoints { get; set; }

    [XmlElement()]
    public int Count {get; set; }

    [XmlElement()]
    public string Message {get; set; }

    public FlyNoteData(){

    }

    public FlyNoteData(String Message){
      this.Message = Message;
      Type = NoSpecialEvent;
      ExtraPoints = 0;
      Count = 0;
    }

  }

}
