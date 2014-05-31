using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace PYIV.Gameplay.Enemy
{
    public class EnemyData
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
    }
}
