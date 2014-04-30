using UnityEngine;
using System.Collections;
using PYIV.Helper;

namespace PYIV.Gameplay {

    public class EnemyBuilder : MonoBehaviour {

        public static Enemy CreateEnemy(EnemyType enemyType)
        {

            string path = ConfigReader.Instance.GetSetting("directory", "enemy");
            
            
            GameObject preafab = Resources.Load<GameObject>(path + enemyType.EnemyData.PreafabName);

            GameObject enemy = GameObject.Instantiate(preafab) as GameObject;

            enemy.AddComponent<Enemy>();
            enemy.GetComponent<Enemy>().Init(enemyType.EnemyData);


            return enemy.GetComponent<Enemy>();
            
        }
    }

}