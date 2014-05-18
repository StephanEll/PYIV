using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Enemy
{

    public class Enemy : MonoBehaviour
    {

        private EnemyData enemyData;

        public float MoveSpeed { 
            get
            {
                return enemyData.MoveSpeed;
            } 
        }

        public int AttackPower { 
            get 
            {
                return enemyData.AttackPower;
            } 
        }

        public int LivePoints { 
            get
            {
                return enemyData.LivePoints;  
            } 
            private set 
            {
                enemyData.LivePoints = value;
            }
        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void OnCollisionEnter2D(Collision2D collision)
        {

        }

        public void Init(EnemyData enemyData)
        {
            this.enemyData = enemyData;
        }
    }

}
