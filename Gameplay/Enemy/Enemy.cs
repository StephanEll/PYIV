using UnityEngine;
using System.Collections;
using PYIV.Gameplay.Character.Weapon;

namespace PYIV.Gameplay.Enemy
{

    public class Enemy : MonoBehaviour
    {

        private EnemyData enemyData;

        private Score score;

        private float boundaryLeft;

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

        void Start()
        {
            boundaryLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        }

        // Update is called once per frame
        public void Update()
        {
            transform.Translate(-MoveSpeed/300, 0, 0);
            if (transform.position.x < boundaryLeft)
                Attack();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                this.LivePoints -= collision.gameObject.GetComponent<Bullet>().Strength;
                if (this.LivePoints <= 0)
                    Die();
            }
        }

        public static void AddAsComponentTo(GameObject go, EnemyData enemyData, Score score)
        {
            Enemy enemy = go.AddComponent<Enemy>();
            enemy.enemyData = enemyData;
            enemy.score = score;
            if (go.gameObject.GetComponent<BoxCollider2D>() == null)
                go.gameObject.AddComponent<BoxCollider2D>();
        }

        private void Die()
        {
            score.AddKill(this);
            Destroy(gameObject);
        }

        private void Attack()
        {
            score.SubtractLivepoints(AttackPower);
            Destroy(gameObject);
        }
    }

}
