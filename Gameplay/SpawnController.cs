using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;

namespace PYIV.Gameplay
{
    public class SpawnController : MonoBehaviour
    {
		
        private List<int> EnemyIdQueue = new List<int>();

        // Update is called once per frame
        void Update()
        {
			
        }

        private void GenerateEnemyIdQueue(List<EnemyType> enemyTypes)
        {
            // fill EnemyIdQueue here
        }

        private void Spawn()
        {
            // use EnemyBuilder to generate the next element from EnemyIDQueue
        }

        public static SpawnController AddAsComponentTo(GameObject go, List<EnemyType> enemyTypes){
            go.AddComponent<SpawnController>();
            SpawnController spawnController = go.GetComponent<SpawnController>();
            spawnController.GenerateEnemyIdQueue(enemyTypes);
			return spawnController;
        }

    }
}
