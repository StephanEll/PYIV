using UnityEngine;
using System.Collections;
using PYIV.Gameplay.Character.Weapon;

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
            transform.Translate(-MoveSpeed/300, 0, 0);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                this.LivePoints -= collision.gameObject.GetComponent<Bullet>().Strength;
                if (this.LivePoints <= 0)
                    Die();
            }

            Destroy(this.gameObject);
        }

        public void Init(EnemyData enemyData)
        {
            this.enemyData = enemyData;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }

}
