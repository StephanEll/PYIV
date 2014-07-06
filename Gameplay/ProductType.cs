using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System;
using System.Runtime.Serialization;

namespace PYIV.Gameplay
{
	[Serializable]
    public abstract class ProductType : IIdentifiable
    {
        [XmlElement()]
        public string Id { get; set; }
        [XmlElement()]
        public int Price { get; set; }
        [XmlElement()]
        public string Name { get; set; }
        [XmlElement()]
        public string Description { get; set; }

        protected ProductType()
        {

        }

    }
}
