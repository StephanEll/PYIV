using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay
{
    public class EnemyData
    {

        public float MoveSpeed { get; private set; }
        public int AttackPower { get; private set; }
        public int LivePoints { get; set; }
        public string PreafabName { get; set; }

        public EnemyData(float moveSpeed, int attackPower, int livePoints)
        {
            this.MoveSpeed = moveSpeed;
            this.AttackPower = attackPower;
            this.LivePoints = livePoints;
        }
    }
}
