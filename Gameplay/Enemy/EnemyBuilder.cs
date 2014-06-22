using UnityEngine;
using System.Collections;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy
{

    public class EnemyBuilder {

        public static Enemy CreateEnemy(EnemyData enemyData, Transform parent, Score score)
        {

            GameObject preafab = Resources.Load<GameObject>(enemyData.PreafabPath);

            GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

            enemy.name += " ID: " + enemyData.Id;

            Enemy.AddAsComponentTo(enemy, enemyData, score);

            enemy.transform.parent = parent;
            enemy.transform.localPosition = enemy.transform.position;


            return enemy.GetComponent<Enemy>();
            
        }
    }

}