using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using PYIV.Helper;
using System.Linq;


namespace PYIV.Gameplay.Score {
  
  [XmlRoot("root")]
  public class FlyNoteDataCollection {
    
    [XmlArray("FlyNoteDataCollection")]
    [XmlArrayItem("FlyNoteData", typeof(FlyNoteData))]
    public FlyNoteData[] flyNoteData { get; set; }
    
    private static volatile FlyNoteDataCollection instance;
    
    private static object syncRoot = new object();
    
    
    private FlyNoteDataCollection()
    {
      
    }
    
    private static FlyNoteDataCollection DeserializeFlyNoteDataCollection(string filename)
    {
      
      // Create an instance of the XmlSerializer specifying type and namespace.
      XmlSerializer serializer = new XmlSerializer(typeof(FlyNoteDataCollection));
      
      XmlReader reader = XMLHelper.LoadXMLReaderFromResource(filename);
      
      // Use the Deserialize method to restore the object's state.
      FlyNoteDataCollection eDC = (FlyNoteDataCollection) serializer.Deserialize(reader);
      
      return eDC;
    }
    
    public FlyNoteData[] GetSubCollection(string type)
    {

      var fld = from data in flyNoteData where data.Type == type select data;

      return fld.ToArray<FlyNoteData>();
      
    }
    
    
    public static FlyNoteDataCollection Instance
    {
      get
      {
        if (instance == null)
        {
          lock (syncRoot)
          {
            if (instance == null)
            {
              instance = DeserializeFlyNoteDataCollection(ConfigReader.Instance.GetSetting("directory", "fly-note-config-xml"));
            }
          }
        }
        
        return instance;
      }
    }
    
    
    
  }
  
  
  
}
