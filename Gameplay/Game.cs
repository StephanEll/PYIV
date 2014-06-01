using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence;
using PYIV.Gameplay.Enemy;

namespace PYIV.Gameplay{

	public class Game : MonoBehaviour
	{
		
		private GameObject background;
		
		private GameData gameData;
		private SpawnController spawnController;
		
		public GameData GameData { 
			
			get {
				return gameData;
			}
			set {
				gameData = value;
				Init();
			}
		}
		
		private void Init(){
			List<EnemyType> enemyTypes = GameData.OpponentStatus.LatestRound.SentAttackers;
			this.spawnController = SpawnController.AddAsComponentTo(this.gameObject, enemyTypes);
		}
		
		void Start ()
		{
            Debug.Log("path: " + gameData.MyStatus.Id);

			var bgPrefab = Resources.Load(gameData.MyStatus.IndianData.BackgroundPreafabPath);
			background = Instantiate(bgPrefab) as GameObject;
			
			background.transform.parent = this.transform;

		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}

}