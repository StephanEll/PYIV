using UnityEngine;
using System.Collections;
using PYIV.Helper;
using PYIV.Gameplay.Score;

namespace PYIV.Gameplay.Character.Weapon {

	public class Bullet : MonoBehaviour {


        public int Strength { get; private set; }

        private Score.Score score;


		void Start () {
            if(gameObject.GetComponent<BoxCollider2D>() == null)
                gameObject.AddComponent<BoxCollider2D>();

		}
		
		void Update () {
            if (rigidbody2D != null)
            {
                Vector2 angle = this.rigidbody2D.velocity;
                angle.Normalize();
                this.transform.localRotation = Quaternion.EulerAngles(angle.x, 0, angle.y * 1.4f);
            }
            if (transform.position.y < PlayingFieldBoundarys.Bottom)
                Miss();

		}

        public static void AddAsComponentTo(GameObject go, int Strength, Score.Score score)
        {
            go.AddComponent<Bullet>().Strength = Strength;
            go.GetComponent<Bullet>().score = score;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>())
            {
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<BoxCollider2D>());

                if (collision.transform.FindChild("back") != null)
                    transform.parent = collision.transform.FindChild("back");
                else
                    transform.parent = collision.transform.GetChild(0);

                score.AddHit(collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>());
            }
        }

        public void Miss()
        {
            score.AddMissed();
            Destroy(gameObject);
        }


	}
}
