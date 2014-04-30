using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay
{

    public abstract class ProductType
    {

        public string Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected ProductType(string id, int price, string name, string description)
        {
            this.Id = id;
            this.Price = price;
            this.Name = name;
            this.Description = description;
        }

    }
}
