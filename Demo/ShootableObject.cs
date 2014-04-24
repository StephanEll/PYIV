using UnityEngine;
using System.Collections;

namespace PYIV.Demo
{
    public abstract class ShootableObject : MonoBehaviour
    {
        public abstract void SwipeHandler(Vector2 delta, float duration);
    }
}
