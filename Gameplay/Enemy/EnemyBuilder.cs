using UnityEngine;
using System.Collections;
using PYIV.Helper;
using PYIV.Gameplay.Score;
using PYIV.Gameplay.Character;

namespace PYIV.Gameplay.Enemy
{
  public class EnemyBuilder
  {

    public static Enemy CreateEnemy(EnemyData enemyData, Transform parent, Score.Score score, Vector3 startPosition)
    {

      Vector3 ec = GameObject.FindObjectOfType<Indian>().IndianData.ColorateEnemys;
      Color enemyColor = new Color(ec.x, ec.y, ec.z, 1.0f);


      GameObject preafab = Resources.Load<GameObject>(enemyData.PreafabPath);

      GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

      enemy.name += " ID: " + enemyData.Id;
            
      Enemy.AddAsComponentTo(enemy, enemyData, score, enemyColor);

      enemy.transform.parent = parent;
      enemy.transform.localPosition = enemy.transform.position + startPosition;


      if(enemy.transform.childCount == 0)
        enemy.GetComponent<SpriteRenderer>().color = enemyColor;
      else
        foreach (SpriteRenderer sr in enemy.transform.GetComponentsInChildren<SpriteRenderer>())
          sr.color = enemyColor;


      return enemy.GetComponent<Enemy>();
            
    }
  }

}