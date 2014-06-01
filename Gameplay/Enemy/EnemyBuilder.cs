using UnityEngine;
using System.Collections;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy
{

    public class EnemyBuilder {

        public static Enemy CreateEnemy(EnemyData enemyData)
        {

            GameObject preafab = Resources.Load<GameObject>(enemyData.PreafabPath);

            GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

            enemy.AddComponent<Enemy>();
            enemy.GetComponent<Enemy>().Init(enemyData);


            return enemy.GetComponent<Enemy>();
            
        }
    }

}