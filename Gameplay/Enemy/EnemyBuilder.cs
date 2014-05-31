using UnityEngine;
using System.Collections;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy
{

    public class EnemyBuilder {

        public static Enemy CreateEnemy(EnemyType enemyType)
        {
            
            GameObject preafab = Resources.Load<GameObject>(enemyType.EnemyData.PreafabPath);

            GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

            enemy.AddComponent<Enemy>();
            enemy.GetComponent<Enemy>().Init(enemyType.EnemyData);


            return enemy.GetComponent<Enemy>();
            
        }
    }

}