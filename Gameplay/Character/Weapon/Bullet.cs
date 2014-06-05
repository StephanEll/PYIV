using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character.Weapon {

	public class Bullet : MonoBehaviour {


        public int Strength { get; private set; }

		void Start () {
            gameObject.AddComponent<BoxCollider2D>();
		}
		
		void Update () {
            Vector2 angle = this.rigidbody2D.velocity;
            angle.Normalize();
            this.transform.localRotation = Quaternion.EulerAngles(angle.x, 0, angle.y * 1.4f);
		}

        public static void AddAsComponentTo(GameObject go, int Strength)
        {
            go.AddComponent<Bullet>().Strength = Strength;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>())
            {
                Destroy(rigidbody2D);
                transform.parent = collision.transform;
            }

        }
	}
}
