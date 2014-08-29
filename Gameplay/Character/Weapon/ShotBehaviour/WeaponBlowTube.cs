using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character.Weapon
{
  public class WeaponBlowTube : ShotBehaviour
  {


    public override void EndSwipeHandler(Bullet bullet, Vector2 startPos, Vector2 endPos, float duration)
    {

      bullet.transform.position = this.transform.position;
      bullet.transform.Translate(new Vector3(2.0f, 0.5f, 0.0f));
      Invoke("GoToStartPosition", 0.5f);
            
      this.GetComponent<Animator>().SetTrigger("shot");

      Vector2 delta = endPos - startPos;

      float speed = Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y)) / duration;

      if (delta.x < 2 || speed < 1800.0f)
      {
        Destroy(bullet.gameObject);
      }

      bullet.rigidbody2D.AddForce(delta.normalized * 10.0f);
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
