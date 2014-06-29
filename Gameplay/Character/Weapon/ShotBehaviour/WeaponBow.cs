using UnityEngine;
using System.Collections;
namespace PYIV.Gameplay.Character.Weapon {
    public class WeaponBow : ShotBehaviour {


        public override void EndSwipeHandler(Bullet bullet, Vector2 startPos, Vector2 endPos, float duration)
        {
            //Debug.Log("end swipe");

            bullet.transform.position = this.transform.position;
            Invoke("GoToStartPosition", 0.5f);
            
            this.GetComponent<Animator>().SetTrigger("shot");

            Vector2 delta = startPos - endPos;

            if (delta.x < 0)
            {
                Destroy(bullet.gameObject);
            }

            bullet.rigidbody2D.AddForce(delta);

            
        }

        
    
        public override void StartSwipeHandler(Vector2 startPos)
        {
            //Debug.Log("start swipe");
            
        }

        public override void UpdateSwipeHandler(Vector2 startPos, Vector2 endPos, float duration)
        {
            //Debug.Log("update swipe");

            if (startPos.x > endPos.x)
            {
                this.GetComponent<Animator>().SetBool("aim", true);

                float angle = Mathf.Atan2(-endPos.y, endPos.x) - Mathf.Atan2(-startPos.y, endPos.x);

                transform.FindChild("aim_group").localRotation = Quaternion.EulerAngles(0, 0, angle);
            }
        }
    }
}
