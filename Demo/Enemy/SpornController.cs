using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Enemy
{
    public class SpornController : MonoBehaviour
    {
        public GameObject spornContainer;
        public EnemyType enemyType;
        public float distanceBetweenEnemysInSec = 3.0f;

        // Use this for initialization
        void Start()
        {
            spornContainer.transform.position = new Vector3(
                Camera.main.orthographicSize * Camera.main.aspect, 
                spornContainer.transform.position.y, 
                0);
            InvokeRepeating("CreateNewEnemy", distanceBetweenEnemysInSec, distanceBetweenEnemysInSec);
        }

        private void CreateNewEnemy()
        {
            GameObject enemy = (GameObject)Instantiate(
                    enemyType.gameObject,
                    spornContainer.transform.position,
                    Quaternion.EulerAngles(0, 0, 0));
            enemy.transform.parent = spornContainer.transform;
            
        }

        
    }
}
