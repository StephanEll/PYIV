using UnityEngine;
using System.Collections;
using PYIV.Gameplay.Character.Weapon;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy
{

    public class Enemy : MonoBehaviour
    {

        private EnemyData enemyData;

        private Score score;

        private bool dead = false;

        private float fateOutFrames;
        private float fateOutFramesMax;

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
            fateOutFrames = ConfigReader.Instance.GetSettingAsFloat("game", "enemy-die-frames");
            fateOutFramesMax = fateOutFrames;
        }

        // Update is called once per frame
        public void Update()
        {
            if (dead)
            {
                FateOut();
            }
            else
            {
                Move();
            }
           
        }

        private void Move()
        {
            if (enemyData.Fly)
            {

            }
            else
            {
                transform.Translate(-MoveSpeed / 300, 0, 0);
            }

            if (transform.position.x < PlayingFieldBoundarys.Left)
                Attack();
        }

        private void FateOut()
        {
            fateOutFrames--;
            if (fateOutFrames == 0)
                DestroßGameobject();
            else
                foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = new Vector4(1.0f, 1.0f, 1.0f, fateOutFrames / fateOutFramesMax);
                }
            
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

            this.GetComponent<Animator>().SetTrigger("dead");

            dead = true;
        }

        private void DestroßGameobject()
        {
            Destroy(gameObject);
        }

        private void Attack()
        {
            score.SubtractLivepoints(AttackPower);
            Destroy(gameObject);
        }
    }

}
