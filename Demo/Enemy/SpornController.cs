using UnityEngine;
using System.Collections;

namespace PYIV.Demo.Enemy
{
    public class SpornController : MonoBehaviour
    {
        public GameObject spornContainer;
        public EnemyType enemyType;
        public float distanceBetweenEnemysInSec = 3.0f;

        private float frameCounter = 0;
        private float countTime = 0;
        private float distanceBetweenSeconds = 0;

        // Use this for initialization
        void Start()
        {
            spornContainer.transform.position = new Vector3(
                Camera.main.orthographicSize * Camera.main.aspect, 
                spornContainer.transform.position.y, 
                0);
            distanceBetweenEnemysInSec = Random.Range(0.8f, 3.0f);
        }

        private void CreateNewEnemy()
        {
            GameObject enemy = (GameObject)Instantiate(
                    enemyType.gameObject,
                    spornContainer.transform.position,
                    Quaternion.EulerAngles(0, 0, 0));
            enemy.transform.parent = spornContainer.transform;
            
        }

        void Update()
        {
            frameCounter += Time.deltaTime;
            countTime += Time.deltaTime;
            if (frameCounter > distanceBetweenEnemysInSec && countTime < 60.0f)
            {
                distanceBetweenEnemysInSec = Random.Range(0.8f, 3.0f);
                frameCounter = 0;
                CreateNewEnemy();
            }
        }

        
    }
}
