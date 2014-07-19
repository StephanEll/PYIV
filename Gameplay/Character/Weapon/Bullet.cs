using UnityEngine;
using System.Collections;
using PYIV.Helper;
using PYIV.Gameplay.Score;

namespace PYIV.Gameplay.Character.Weapon
{

  public class Bullet : MonoBehaviour
  {


    public int Strength { get; private set; }

    private Score.Score score;

    private bool hit = false;



    void Start()
    {
      if (gameObject.GetComponent<BoxCollider2D>() == null)
        gameObject.AddComponent<BoxCollider2D>();

    }
		
    void Update()
    {

      if (!hit)
      {
        Vector2 angle = this.rigidbody2D.velocity;
        angle.Normalize();
        this.transform.rotation = Quaternion.EulerAngles(0, 0, angle.y * 1.41421356237f);
      }

      if (transform.position.y < PlayingFieldBoundarys.Bottom)
        Miss();

    }

    public static void AddAsComponentTo(GameObject go, int Strength, Score.Score score)
    {
      go.AddComponent<Bullet>().Strength = Strength;
      go.GetComponent<Bullet>().score = score;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

      float rememberRotation = transform.localRotation.eulerAngles.z;

      if (collision.transform.FindChild("back") != null)
        transform.parent = collision.transform.FindChild("back");
      else
        transform.parent = collision.transform.GetChild(0);
      
      if (collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>())
      {
        hit = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());

        if(collision.transform.localScale.x < 0){
          if(rememberRotation > 180.0f)
            transform.localRotation = Quaternion.Euler(0,0, 540.0f - rememberRotation);
          else
            transform.localRotation = Quaternion.Euler(0,0, 180.0f - rememberRotation);
        }

        score.AddHit(collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>());
      }
    }

    public void Miss()
    {
      score.AddMissed();
      Destroy(gameObject);
    }


  }
}
