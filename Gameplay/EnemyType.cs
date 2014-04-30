using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay
{

    public class EnemyType : ProductType
    {

        public int Count { get; set; }
        public EnemyData EnemyData { get; set; }

        public EnemyType(string id, int price, string name, string description, int count, EnemyData enemyData)
            : base(id, price, name, description)
        {
            this.Count = count;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
