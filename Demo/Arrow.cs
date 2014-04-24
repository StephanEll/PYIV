using UnityEngine;
using System.Collections;

namespace PYIV.Demo
{
    public class Arrow : ShootableObject  {

	
	    void Update () {
            //float angle = Mathf.Atan(this.rigidbody2D.velocity.y / this.rigidbody2D.velocity.x) * (180.0f / Mathf.PI);
            Vector2 angle = this.rigidbody2D.velocity;
            angle.Normalize();
            this.transform.localRotation = Quaternion.EulerAngles(angle.x, 0, angle.y );
            Destroy();
	    }

        override
        public void SwipeHandler(Vector2 delta, float time)
        {
            this.rigidbody2D.AddForce(delta * 5);
        }

        private void Destroy()
        {
            if (this.transform.position.y < - Camera.main.orthographicSize || this.transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
