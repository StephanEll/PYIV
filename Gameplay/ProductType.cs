using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace PYIV.Gameplay
{

    public abstract class ProductType
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
