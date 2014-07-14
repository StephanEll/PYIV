using UnityEngine;
using System.Collections;
using PYIV.Gameplay.Character.Weapon;
using PYIV.Helper;
using PYIV.Gameplay.Score;

namespace PYIV.Gameplay.Enemy
{

  public class Enemy : MonoBehaviour
  {
    private float timeDelta = 0;

    private EnemyData enemyData;

    private Score.Score score;

    private bool dead = false;

    private float fateOutFrames;
    private float fateOutFramesMax;

    private bool flightSwitch = false;
    private float flightHight = 20.0f;

    public float MoveSpeed { 
      get
      {
        return enemyData.MoveSpeed;
      } 
    }

    public int AttackPower { 
      get 
      {
        return enemyData.AttackPower;
      } 
    }

    public int LivePoints { 
      get
      {
        return enemyData.LivePoints;  
      } 
      private set 
      {
        enemyData.LivePoints = value;
      }
    }

    public string Type {
      get
      {
        return enemyData.Id;  
      }
    }

    void Start()
    {
      fateOutFrames = ConfigReader.Instance.GetSettingAsFloat("game", "enemy-die-frames");
      fateOutFramesMax = fateOutFrames;
      //enemyData.print();
      if (enemyData.Fly)
      {
        fateOutFramesMax = fateOutFramesMax / 2;
      }
    }

    // Update is called once per frame
    public void Update()
    {
      if (dead)
      {
        FateOut();
      }
      else
      {

        if (enemyData.Fly)
          Fly();
        else
          Move();

        if (transform.position.x < PlayingFieldBoundarys.Left)
          Attack();

      }
       
    }

    private void Move()
    {
      transform.Translate(-MoveSpeed / 250, 0, 0);
    }

    private void Fly()
    {
      timeDelta += Time.deltaTime;

      float ySin = Mathf.Sin( timeDelta );

      if (ySin > 0 && flightSwitch)
      {
        flightHight = Random.Range(20.0f,40.0f);
        flightSwitch = false;
      } else if (ySin < 0 && !flightSwitch)
      {
        flightHight = Random.Range(20.0f,40.0f);
        flightSwitch = true;
      }

      transform.Translate(-MoveSpeed / 300, ySin / flightHight, 0);

    }

    private void FateOut()
    {
      fateOutFrames--;
      if (fateOutFrames == 0)
        DestroyGameobject();
      else
        foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
        {
          sr.color = new Vector4(1.0f, 1.0f, 1.0f, fateOutFrames / fateOutFramesMax);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.GetComponent<Bullet>())
      {
        this.LivePoints -= collision.gameObject.GetComponent<Bullet>().Strength;
        if (this.LivePoints <= 0)
          Die();
      }
    }

    public static void AddAsComponentTo(GameObject go, EnemyData enemyData, Score.Score score)
    {
      Enemy enemy = go.AddComponent<Enemy>();
      enemy.enemyData = enemyData;
      enemy.score = score;
      if (go.gameObject.GetComponent<BoxCollider2D>() == null)
        go.gameObject.AddComponent<BoxCollider2D>();
    }

    private void Die()
    {
      score.AddKill(this);

      this.GetComponent<Animator>().SetTrigger("dead");

      dead = true;

      if (enemyData.Fly)
      {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.AddComponent<Rigidbody2D>();
      }
    }

    private void DestroyGameobject()
    {
      gameObject.SetActive(false);
    }

    private void Attack()
    {
      score.SubtractLivepoints(AttackPower);
      gameObject.SetActive(false);
    }
  }

}
