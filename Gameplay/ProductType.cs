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
		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(ProductType))
				return false;
			PYIV.Gameplay.ProductType other = (PYIV.Gameplay.ProductType)obj;
			return Id == other.Id;
		}


		public override int GetHashCode ()
		{
			unchecked {
				return (Id != null ? Id.GetHashCode () : 0);
			}
		}


    }
}
