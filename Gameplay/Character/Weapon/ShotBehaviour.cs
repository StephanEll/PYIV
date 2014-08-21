using UnityEngine;
using System.Collections;
using System;
using PYIV.Gameplay.Score;

namespace PYIV.Gameplay.Character.Weapon
{

  public abstract class ShotBehaviour : MonoBehaviour
  {

    private Vector2 startPosition = new Vector2(0, 0);
    private float startTime;

    private string bulletPreafabPath;
    private int bulletStrength;

    protected Score.Score score;
    private GameObject game;


    void Start()
    {
      game = GameObject.Find("Game");
      score = game.GetComponent<Score.Score>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Time.timeScale != 0)
      {
        HandleSwipeGesture();
        HandleDebugShotWithKeys();
      }
    }

    /* 
         * change this method if you add a new shot behaviour
        */
    public static void AddAsComponentFactory(GameObject go, string bulletPreafabPath, string ShotBehaviourClassName, int bulletStrenght)
    {
      switch (ShotBehaviourClassName)
      {
        case "WeaponBow":
          go.AddComponent<WeaponBow>().bulletPreafabPath = bulletPreafabPath;
          go.GetComponent<WeaponBow>().bulletStrength = bulletStrenght;
          break;
        case "WeaponSpear":
          go.AddComponent<WeaponSpear>().bulletPreafabPath = bulletPreafabPath;
          go.GetComponent<WeaponSpear>().bulletStrength = bulletStrenght;
          break;
        case "WeaponBlowTube":
          go.AddComponent<WeaponBlowTube>().bulletPreafabPath = bulletPreafabPath;
          go.GetComponent<WeaponBlowTube>().bulletStrength = bulletStrenght;
          break;
      }
    }

    public abstract void EndSwipeHandler(Bullet bulletPrefab, Vector2 startPos, Vector2 endPos, float duration);

    public abstract void StartSwipeHandler(Vector2 startPos);

    public abstract void UpdateSwipeHandler(Vector2 startPos, Vector2 endPos, float duration);
        

    private void HandleDebugShotWithKeys()
    {
      if (Input.GetKeyUp(KeyCode.S))
      {
        Vector2 startPosition = new Vector2(0, 0);
        Vector2 endPosition = new Vector2(-400, -200);
        Vector2 delta = endPosition - startPosition;
      
        //float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));
      
        float duration = 1.0f;
        //float speed = dist / duration;
        if (gameObject.GetComponent<Indian>().ShotRequest())
        {
          GameObject bullet = Instantiate(Resources.Load<GameObject>(bulletPreafabPath)) as GameObject;
          bullet.transform.parent = game.transform;
          Bullet.AddAsComponentTo(bullet, bulletStrength, score);
        
          EndSwipeHandler(
            bullet.GetComponent<Bullet>(),
            (startPosition / Screen.width) * 1000,
            (endPosition / Screen.width) * 1000,
            duration);
        } else
        {
          GoToStartPosition();
        }
      }
    }
    private void HandleSwipeGesture()
    {
      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
      {
        Vector2 endPosition = Input.GetTouch(0).position;
        Vector2 delta = endPosition - startPosition;

        //float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));

        float duration = Time.time - startTime;
        //float speed = dist / duration;
        if (gameObject.GetComponent<Indian>().ShotRequest())
        {
          GameObject bullet = Instantiate(Resources.Load<GameObject>(bulletPreafabPath)) as GameObject;
          bullet.transform.parent = game.transform;
          Bullet.AddAsComponentTo(bullet, bulletStrength, score);

          EndSwipeHandler(
                        bullet.GetComponent<Bullet>(),
                        (startPosition / Screen.width) * 1000,
                        (endPosition / Screen.width) * 1000,
                        duration);
        } else
        {
          GoToStartPosition();
        }

      }

      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
      {
        UpdateSwipeHandler(
          (startPosition / Screen.width) * 1000 , 
          (Input.GetTouch(0).position / Screen.width) * 1000 , 
          Time.time - startTime);
      }

      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
      {
        startPosition = Input.GetTouch(0).position;
        startTime = Time.time;
        StartSwipeHandler((startPosition / Screen.width) * 1000);
      }
    }

    protected void GoToStartPosition()
    {
      this.GetComponent<Animator>().SetBool("aim", false);
      transform.FindChild("aim_group").localRotation = Quaternion.EulerAngles(0, 0, 0);
    }
  }
}
