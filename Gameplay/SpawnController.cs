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
    private Dictionary<string, int> EnemySpawnStartPos = new Dictionary< string, int>();
    private float deltaSpawnTime;
    private float nextSpawntime;
    private float countTime = 0;
    private float lastEnemySpawnTime;
    private Vector3 enemyStartPosition;
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
      
      enemyStartPosition = new Vector3(3, 0, 0);
      
    }
    
    // Update is called once per frame
    void Update()
    {
      
      if (nextSpawntime < Time.time && EnemyIdQueue.Count != 0)
      {
        Spawn();
        if (EnemyIdQueue.Count != 0)
          ComputeNextSpawnTime();
      }
      
    }
    
    private void GenerateEnemyIdQueue(List<EnemyType> enemyTypes)
    {
      
      foreach (EnemyType et in enemyTypes)
      {
        EnemyDataQueue.Add(et.EnemyData.Id, et.EnemyData);
        EnemySpawnStartPos.Add(et.EnemyData.Id, et.Count);
    
        for (int i = 0; i < et.Count; i++)
        {
          EnemyIdQueue.Add(et.EnemyData.Id);


        }
      }

      int spawningPosCounter = 1;

      if (EnemyIdQueue.Exists(x => x.Contains("Rat")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Rat"];
        EnemySpawnStartPos ["Rat"] = spawningPosCounter;
      }
      if (EnemyIdQueue.Exists(x => x.Contains("Tarantula")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Tarantula"];
        EnemySpawnStartPos ["Tarantula"] = spawningPosCounter;

      }
      if (EnemyIdQueue.Exists(x => x.Contains("Eagle")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Eagle"];
        EnemySpawnStartPos ["Eagle"] = spawningPosCounter;
        
      }
      if (EnemyIdQueue.Exists(x => x.Contains("Panther")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Panther"];
        EnemySpawnStartPos ["Panther"] = spawningPosCounter;
        
      }
      if (EnemyIdQueue.Exists(x => x.Contains("Elephant")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Elephant"];
        EnemySpawnStartPos ["Elephant"] = spawningPosCounter;
        
      }
      if (EnemyIdQueue.Exists(x => x.Contains("Rhino")))
      {
        spawningPosCounter += EnemySpawnStartPos ["Rhino"];
        EnemySpawnStartPos ["Rhino"] = spawningPosCounter;
        
      }
    
      
      EnemyIdQueue = EnemyIdQueue.OrderBy(emp => Guid.NewGuid()).ToList();
    }
    
    private void Spawn()
    {
      EnemyData ed;
      EnemyDataQueue.TryGetValue(EnemyIdQueue [0], out ed);
  
      int enemyStartPos;
      EnemySpawnStartPos.TryGetValue(EnemyIdQueue [0], out enemyStartPos);
    
      //Debug.Log ("SpawnTime: " + Time.time + " ID " + ed.Id);
      switch (ed.Id)
      {
        case "Rat":

          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Rat"] = enemyStartPos;
          break;
        case "Tarantula":
          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Tarantula"] = enemyStartPos;
          break;
        case "Eagle":
          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Eagle"] = enemyStartPos;
          break;
        case "Panther":
          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Panther"] = enemyStartPos;
          break;
        case "Elephant":
          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Elephant"] = enemyStartPos;
          break;
        case "Rhino":
          enemyStartPosition.z = enemyStartPos;
          enemyStartPos --;
          EnemySpawnStartPos ["Rhino"] = enemyStartPos;
          break;
        default:
          enemyStartPosition.z = 1;
          break;
          
      }
      
      
      EnemyBuilder.CreateEnemy(
        ed, 
        EnemyContainer.transform,
        EnemyContainer.transform.parent.GetComponent<Score.Score>(),
        enemyStartPosition
      );
      lastEnemySpawnTime = ed.SpawnTime;
      EnemyIdQueue.RemoveAt(0);
    
    }
    
    public static SpawnController AddAsComponentTo(GameObject go, List<EnemyType> enemyTypes)
    {
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
      EnemyData ed;
      EnemyDataQueue.TryGetValue(EnemyIdQueue [0], out ed);
      //nextSpawntime = Time.time + deltaSpawnTime; 
      float DeltaCalcu = UnityEngine.Random.Range(-(deltaSpawnTime * lastEnemySpawnTime), deltaSpawnTime * (1.0f - ed.SpawnTime));
      nextSpawntime = Time.time + deltaSpawnTime + DeltaCalcu;
      
    }
    
    public GameObject GetEnemyContainer()
    {
      return EnemyContainer;
    }
    
    public int GetSpawnQueueCount()
    {

      return EnemyIdQueue.Count;

    }
    
  }
}