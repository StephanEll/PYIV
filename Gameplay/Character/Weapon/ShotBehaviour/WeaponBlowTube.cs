using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character.Weapon
{
  public class WeaponBlowTube : ShotBehaviour
  {


    public override void EndSwipeHandler(Bullet bullet, Vector2 startPos, Vector2 endPos, float duration)
    {

			bullet.transform.position = this.transform.position;
			bullet.transform.Translate (new Vector3 (1.1f, 1.3f, 0.0f));
			Invoke("GoToStartPosition", 0.5f);
            
      this.GetComponent<Animator>().SetTrigger("shot");

      Vector2 delta = endPos - startPos;

      float speed = Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y)) / duration;


      if (delta.x < - 300 ){ //|| speed < 800.0f) {
				//do nothing
				Destroy(bullet.gameObject);
			} else {
				bullet.rigidbody2D.AddForce(delta.normalized * 1000.0f);
			}

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
