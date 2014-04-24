using UnityEngine;
using System.Collections;
namespace Assets.Scripts.ProtectVillage
{
    public abstract class ShootableObject : MonoBehaviour
    {
        public abstract void SwipeHandler(Vector2 delta, float duration);
    }
}
