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
    [XmlElement()]
    public string Type {get; set; }

    [XmlElement()]
    public int ExtraPoints { get; set; }

    [XmlElement()]
    public int Count {get; set; }

    [XmlElement()]
    public string Message {get; set; }

  }

}
