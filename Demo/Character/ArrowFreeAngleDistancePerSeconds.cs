using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Character
{
    public class ArrowFreeAngleDistancePerSeconds : ShootableObject  {

        override
        public void SwipeHandler(Vector2 startPos, Vector2 endPos, float time)
        {
            Vector2 delta = endPos - startPos;

            if (delta.x < 2)
            {
                Destroy(this.gameObject);
            }

            this.rigidbody2D.AddForce(delta/time);
        }

        
    }
}
