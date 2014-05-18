using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Character
{
    public class ArrowFreeAngleDistancePerSecondsThreshold : ShootableObject  {

        override
        public void SwipeHandler(Vector2 startPos, Vector2 endPos, float time)
        {
            Vector2 delta = endPos - startPos;

            float speed =  Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y)) / time;

            if (delta.x < 2 || speed < 1800.0f)
            {
                Destroy(this.gameObject);
            }

            this.rigidbody2D.AddForce(delta.normalized * 10.0f);
        }

        
    }
}
