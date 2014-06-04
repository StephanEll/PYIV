using UnityEngine;
using System.Collections;
namespace PYIV.Gameplay.Character.Weapon {
    public class WeaponBlowTube : ShotBehaviour {


        public override void EndSwipeHandler(Bullet bullet, Vector2 startPos, Vector2 endPos, float duration)
        {
            this.GetComponent<Animator>().SetTrigger("shot");
            this.GetComponent<Animator>().SetBool("aim", false);

            Vector2 delta = endPos - startPos;

            float speed = Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y)) / duration;

            if (delta.x < 2 || speed < 1800.0f)
            {
                Destroy(this.gameObject);
            }

            bullet.rigidbody2D.AddForce(delta.normalized * 10.0f);
        }
    
        public override void StartSwipeHandler(Vector2 startPos)
        {
            this.GetComponent<Animator>().SetBool("aim", true);
        }

        public override void UpdateSwipeHandler(Vector2 startPos, Vector2 endPos, float duration)
        {

        }
    }
}
