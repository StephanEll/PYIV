using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Character
{
    public abstract class ShootableObject : MonoBehaviour
    {
        public abstract void SwipeHandler(Vector2 startPos, Vector2 endPos, float duration);

        void Start()
        {
            gameObject.AddComponent<PolygonCollider2D>().isTrigger = false;
        }

        protected void CheckDestroy()
        {
            if (this.transform.position.y < -Camera.main.orthographicSize || this.transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
            {
                GameObject.FindObjectOfType<ScoreController>().addMissed();
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            //float angle = Mathf.Atan(this.rigidbody2D.velocity.y / this.rigidbody2D.velocity.x) * (180.0f / Mathf.PI);
            Vector2 angle = this.rigidbody2D.velocity;
            angle.Normalize();
            this.transform.localRotation = Quaternion.EulerAngles(angle.x, 0, angle.y * 1.4f);
            CheckDestroy();
        }

    }

    
}
