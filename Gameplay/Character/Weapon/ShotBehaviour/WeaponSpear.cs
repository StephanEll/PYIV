using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character.Weapon
{
  public class WeaponSpear : ShotBehaviour
  {


    public override void EndSwipeHandler(Bullet bullet, Vector2 startPos, Vector2 endPos, float duration)
    {
      
      bullet.transform.position = this.transform.position;
      bullet.transform.Translate(new Vector3(0.0f, 0.8f, 0.0f));
      Invoke("GoToStartPosition", 0.5f);

      this.GetComponent<Animator>().SetTrigger("shot");
      this.GetComponent<Animator>().SetBool("aim", false);

      Vector2 delta = endPos - startPos;

      if (delta.x < 2)
      {
        Destroy(bullet);
      }

      bullet.rigidbody2D.AddForce(delta / duration);

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
