using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace PYIV.Gameplay.Enemy
{
    public class EnemyData : IIdentifiable
    {
        [XmlElement()]
        public string Id {get; set; }
        [XmlElement()]
        public float MoveSpeed {get; set; }
        [XmlElement()]
        public int AttackPower { get; set; }
        [XmlElement()]
        public int LivePoints { get; set; }
        [XmlElement()]
        public string PreafabPath {get; set; }
        [XmlElement()]
        public bool Fly { get; set; } // evlt ändern in move behaviour


        public EnemyData()
        {
            
        }

    }
}
