using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace PYIV.Gameplay.Enemy
{
    public class EnemyData : IComparer<EnemyData>
    {
        [XmlElement()]
        public string Id;
        [XmlElement()]
        public float MoveSpeed;
        [XmlElement()]
        public int AttackPower;
        [XmlElement()]
        public int LivePoints;
        [XmlElement()]
        public string PreafabPath;


        public EnemyData()
        {
            
        }

        int IComparer<EnemyData>.Compare(EnemyData x, EnemyData y)
        {
            
            Debug.Log(x.Id);
            string cmpstr = (string)x.Id;
            return cmpstr.CompareTo((string)y.Id);
        }
    }
}
