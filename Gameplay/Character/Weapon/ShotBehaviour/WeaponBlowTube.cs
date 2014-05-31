using UnityEngine;
using System.Collections;
namespace PYIV.Gameplay.Character.Weapon {
    public class WeaponBlowTube : ShotBehaviour {


        public override void EndSwipeHandler(Bullet bulletPrefab, Vector2 startPos, Vector2 endPos, float duration)
        {
            this.GetComponent<Animator>().SetTrigger("shot");
            this.GetComponent<Animator>().SetBool("aim", false);
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
