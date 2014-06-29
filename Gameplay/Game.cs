using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence;
using PYIV.Gameplay.Enemy;
using PYIV.Gameplay.Character;
using PYIV.Helper;
using PYIV.Menu;

namespace PYIV.Gameplay{

	public class Game : MonoBehaviour
	{

    private float playingFieldWidth = 26;
    private float playingFieldHeight = 16;

		private GameObject background;
		
		private GameData gameData;
		private SpawnController spawnController;
    private float initialCameraSize;

		
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

			Score.AddAsComponentTo(
				gameObject, 
				ConfigReader.Instance.GetSettingAsInt("game", "start-village-livepoints"));
        IndianBuilder.CreateIndian(gameData.MyStatus, this.transform);
		}
		
		void Start ()
		{

      initialCameraSize = Camera.main.orthographicSize;
      Camera.main.orthographicSize = (playingFieldWidth/Camera.main.aspect)/2;
      Camera.main.gameObject.transform.Translate(new Vector2 (0,  -(playingFieldHeight - 2*Camera.main.orthographicSize))/2);

			var bgPrefab = Resources.Load(gameData.MyStatus.IndianData.BackgroundPreafabPath);
			background = Instantiate(bgPrefab) as GameObject;
      background.transform.parent = transform;
			
			background.transform.parent = this.transform;

		}

		// Update is called once per frame
		void Update ()
		{
      if (spawnController.GetSpawnQueueCount() == 0 && spawnController.GetEnemyContainer().transform.childCount == 0)
      {
         GameFinished();
      }
		}

    private void GameFinished()
    {
      Camera.main.orthographicSize = initialCameraSize;
      Camera.main.gameObject.transform.position = Vector3.zero;

      gameData.MyStatus.LatestRound.ScoreResult = GetComponent<Score>().GetScoreResult();

      ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(StatisticView), gameData);
    }
	}

}