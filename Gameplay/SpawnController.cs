﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;
using System.Linq;
using PYIV.Helper;

namespace PYIV.Gameplay
{
    public class SpawnController : MonoBehaviour
    {
        private List<string> EnemyIdQueue = new List<string>();
        private Dictionary<string, EnemyData> EnemyDataQueue = new Dictionary<string, EnemyData>();
        private float deltaSpawnTime;
        private float nextSpawntime;
        private float countTime = 0;

        private GameObject EnemyContainer;

        void Start()
        {
            EnemyContainer = new GameObject("Enemy Container");
            EnemyContainer.transform.parent = GameObject.Find("Game").transform;

            EnemyContainer.transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Screen.width, 
                    Screen.height * ConfigReader.Instance.GetSettingAsFloat("game", "spawning-height")
                    )
                );

        }

        // Update is called once per frame
        void Update()
        {

            if (nextSpawntime < Time.time && EnemyIdQueue.Count != 0) {
                Spawn();
                ComputeNextSpawnTime();
            }

        }

        private void GenerateEnemyIdQueue(List<EnemyType> enemyTypes)
        {
            
            foreach (EnemyType et in enemyTypes)
            {
                EnemyDataQueue.Add(et.EnemyData.Id, et.EnemyData);
                for (int i = 0; i < et.Count; i++ )
                {
                    EnemyIdQueue.Add(et.EnemyData.Id);
                }
            }

            EnemyIdQueue = EnemyIdQueue.OrderBy(emp => Guid.NewGuid()).ToList();
        }

        private void Spawn()
        {
            EnemyData ed;
            EnemyDataQueue.TryGetValue( EnemyIdQueue[0] , out ed);
            EnemyBuilder.CreateEnemy(
                ed, 
                EnemyContainer.transform, 
                EnemyContainer.transform.parent.GetComponent<Score>()
                );
            EnemyIdQueue.RemoveAt(0);
        }

        public static SpawnController AddAsComponentTo(GameObject go, List<EnemyType> enemyTypes){
            go.AddComponent<SpawnController>();
            SpawnController spawnController = go.GetComponent<SpawnController>();
            spawnController.GenerateEnemyIdQueue(enemyTypes);
            spawnController.ComputeDeltaSpawnTime();
            spawnController.ComputeNextSpawnTime();
			      return spawnController;
        }

        private void ComputeDeltaSpawnTime()
        {
            float maxSpawnTime;
            float.TryParse(ConfigReader.Instance.GetSetting("game", "max-spawn-time"), out maxSpawnTime);
            deltaSpawnTime = maxSpawnTime / EnemyIdQueue.Count();
        }

        private void ComputeNextSpawnTime()
        {
            nextSpawntime = Time.time + deltaSpawnTime + UnityEngine.Random.Range (-deltaSpawnTime,deltaSpawnTime);
        }

        public GameObject GetEnemyContainer()
        {
            return EnemyContainer;
        }

        public int GetSpawnQueueCount()
        {
            return EnemyIdQueue.Count();
        }

    }
}
