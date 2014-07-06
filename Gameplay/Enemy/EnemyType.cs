using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace PYIV.Gameplay.Enemy
{
	[Serializable]
    public class EnemyType : ProductType
    {
        [XmlElement()]
        public int Count { get; set; }

        [XmlElement()]
        public string EnemyDataId { get; set; }

        public EnemyData EnemyData { get; set; }


        public EnemyType()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
