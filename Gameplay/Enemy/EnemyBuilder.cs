using UnityEngine;
using System.Collections;
using PYIV.Helper;

namespace PYIV.Gameplay.Enemy
{

    public class EnemyBuilder {

        public static Enemy CreateEnemy(EnemyData enemyData, Transform parent)
        {

            GameObject preafab = Resources.Load<GameObject>(enemyData.PreafabPath);

            GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

            enemy.AddComponent<Enemy>();
            enemy.GetComponent<Enemy>().Init(enemyData);

            enemy.transform.parent = parent;
            enemy.transform.localPosition = new Vector3(0, 0, 0);


            return enemy.GetComponent<Enemy>();
            
        }
    }

}