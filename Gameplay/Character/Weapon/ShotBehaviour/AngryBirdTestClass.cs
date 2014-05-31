using UnityEngine;
using System.Collections;
namespace PYIV.Gameplay.Character.Weapon {
    public class AngryBirdTestClass : ShotBehaviour {


        public override void EndSwipeHandler(Bullet bulletPrefab, Vector2 startPos, Vector2 endPos, float duration)
        {
            // implement flight behaviour here
        }
    
        public override void StartSwipeHandler(Vector2 startPos)
        {
        
        }

        public override void UpdateSwipeHandler(Vector2 startPos, Vector2 endPos, float duration)
        {

        }
    }
}
