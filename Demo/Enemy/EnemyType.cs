using System;
using UnityEngine;
using PYIV.Demo.Character;

namespace PYIV.Demo.Enemy
{
    public class EnemyType : MonoBehaviour
    {
        public float speed = 0.05f;

        void Start()
        {
            
        }

        void Update()
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y);
            CheckDestroy();
        }

        protected void CheckDestroy()
        {
            if (this.transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect )
            {
                GameObject.FindObjectOfType<ScoreController>().addDamage();
                Destroy(this.gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D collision) 
        {
            if (collision.gameObject.GetComponent<ShootableObject>())
            {
                Debug.Log("Collision With Arrow");
                Destroy(collision.gameObject);
                GameObject.FindObjectOfType<ScoreController>().addHit();
            }
            
            Destroy(this.gameObject);
        }
    }
}
