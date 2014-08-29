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
      if (Time.timeScale != 0)
      {
        if (!hit)
        {
          Vector2 angle = this.rigidbody2D.velocity;
          angle.Normalize();
          this.transform.rotation = Quaternion.Euler(0, 0, angle.y * 1.41421356237f * 45.0f);
        }

        if (transform.position.y < PlayingFieldBoundarys.Bottom)
          Miss();
      }

    }

    public static void AddAsComponentTo(GameObject go, int Strength, Score.Score score)
    {
      go.AddComponent<Bullet>().Strength = Strength;
      go.GetComponent<Bullet>().score = score;
    }


    /*
     * Is Handeling The collision for Enemys and Bullets
     * because of order of different events
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {

      if (collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>() 
          && collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>().Dead == false )
      {

        // Stick Bullet to Enemy

        PYIV.Gameplay.Enemy.Enemy hitEnemy = collision.gameObject.GetComponent<PYIV.Gameplay.Enemy.Enemy>();

        if (hitEnemy.transform.childCount == 0)
          transform.parent = hitEnemy.transform;
        else
          transform.parent = hitEnemy.transform.GetChild(0);


        hit = true;

        // Destroy BulletCollider
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());



        score.AddHit(hitEnemy);

        //Reduce Enemy Livepoints here

        if (! hitEnemy.Dead)
        {
          hitEnemy.LivePoints -= this.Strength;
          if (hitEnemy.LivePoints <= 0){
            hitEnemy.Die();
          }
        }

      }
    }

    public void Miss()
    {
      score.AddMissed();
      Destroy(gameObject);
    }


  }
}
