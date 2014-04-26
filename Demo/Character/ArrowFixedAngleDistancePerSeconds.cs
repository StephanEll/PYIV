using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Character
{
    public class ArrowFixedAngleDistancePerSeconds : ShootableObject  {

        public Vector2 angle;

        override
        public void SwipeHandler(Vector2 startPos, Vector2 endPos, float time)
        {
            Vector2 delta = endPos - startPos;
            float distance = Vector2.Distance(startPos, endPos);

            if (delta.x < 2)
            {
                Destroy(this.gameObject);
            }

            this.rigidbody2D.AddForce(angle * distance/time);
        }

        
    }
}
