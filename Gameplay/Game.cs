using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence;
using PYIV.Gameplay.Enemy;
using PYIV.Gameplay.Character;
using PYIV.Helper;
using PYIV.Menu;
using PYIV.Gameplay.Score;
using PYIV.Menu.Commands;

namespace PYIV.Gameplay
{
	public class Game : MonoBehaviour
	{

		private float playingFieldWidth = 26;
		private float playingFieldHeight = 16;
		private GameObject background;
		private GameData gameData;
		private SpawnController spawnController;
		private float initialCameraSize;
		private int spawnCount;
    private Indian indian;
		
		public GameData GameData { 
			
			get {
				return gameData;
			}
			set {
				gameData = value;
				Init ();
			}
		}
		
		private void Init ()
		{
			List<EnemyType> enemyTypes = GameData.OpponentStatus.LatestRound.SentAttackers;
			this.spawnController = SpawnController.AddAsComponentTo (this.gameObject, enemyTypes);

			Score.Score score = Score.Score.AddAsComponentTo (
				gameObject, 
				ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints"));
			
      indian = IndianBuilder.CreateIndian (gameData.MyStatus, this.transform);
			score.OnScoreChanged += HandleOnScoreChanged;
		}

		void HandleOnScoreChanged (int newScore)
		{
			if (newScore <= 0)
				GameFinished ();
		}
		
		void Start ()
		{
			spawnCount = spawnController.GetSpawnQueueCount ();
			initialCameraSize = Camera.main.orthographicSize;
			Camera.main.orthographicSize = (playingFieldWidth / Camera.main.aspect) / 2;
			Camera.main.gameObject.transform.Translate (new Vector2 (0, -(playingFieldHeight - 2 * Camera.main.orthographicSize)) / 2);
			Camera.main.farClipPlane = spawnCount + 50;



			var bgPrefab = Resources.Load (gameData.MyStatus.IndianData.BackgroundPreafabPath);
			background = Instantiate (bgPrefab) as GameObject;
			background.transform.parent = transform;
			
			background.transform.parent = this.transform;
			background.transform.position += new Vector3 (0, 0, spawnCount + 10);

      float screenWidthPercent = Screen.width / 4.0f; 
      
      float height = Camera.main.ScreenToWorldPoint(
        new Vector3(0, Screen.width / 2.65f, 0)
        ).y;

      indian.transform.position = new Vector3(
          indian.transform.localPosition.x,
          indian.transform.localPosition.y+height,
          indian.transform.localPosition.z);

		}

		// Update is called once per frame
		void Update ()
		{
			if (spawnController.GetSpawnQueueCount () == 0) {
				bool finished = true;
				foreach (Transform tr in spawnController.GetEnemyContainer().transform) {
					if (tr.gameObject.activeInHierarchy)
						finished = false;
				}
				if (finished)
					GameFinished ();
			}
		}

		private void GameFinished ()
		{
			Camera.main.orthographicSize = initialCameraSize;
			Camera.main.gameObject.transform.position = Vector3.zero;

			gameData.MyStatus.LatestRound.ScoreResult = GetComponent<Score.Score> ().GetScoreResult ();
			ICommand saveResultsCommand = new SaveGameResultsCommand(gameData);
			saveResultsCommand.Execute();
			
			this.gameObject.SetActive(false);
		}
	}

}