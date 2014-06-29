using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace PYIV.Gameplay.Character
{

    public class IndianData : IIdentifiable
    {
		[XmlElement()]
        public string Id { get; set; }
        [XmlElement()]
        public string Name { get; set; }
        [XmlElement()]
		public string Description { get; set; }
		[XmlElement()]
		public string SpriteImageName { get; set; }
        [XmlElement()]
        public int Stamina { get; set; }
        [XmlElement()]
        public int Strength { get; set; }
        [XmlElement()]
        public int Accuracy { get; set; }
        [XmlElement()]
        public string ShotBehaviourClassName { get; set; }
        [XmlElement()]
        public string PreafabPath { get; set; }
        [XmlElement()]
        public string BulletPreafabPath { get; set; }
        [XmlElement()]
        public string BackgroundPreafabPath { get; set; }
    }
}
