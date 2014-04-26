using System;
using UnityEngine;

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
        }

        void OnCollisionEnter2D(Collision2D collision) 
        {
            Destroy(collision.gameObject);
            GameObject.FindObjectOfType<ScoreController>().addHit();
            Destroy(this.gameObject);
        }
    }
}
