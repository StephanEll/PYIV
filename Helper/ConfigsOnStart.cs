using UnityEngine;
using System;
using System.Collections;
using PYIV.Menu;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;
using System.Net;
using PYIV.Gameplay.Enemy;

namespace PYIV.Helper{
	public class ConfigsOnStart : MonoBehaviour
	{
		GameData gameData;

		Camera camera;

		private float playingFieldWidth =26;
		private float playingFieldHeight = 16;
		



		//Configuration Camera at startup
		void Awake(){

			camera = Camera.main;

			Camera.main.orthographicSize = (playingFieldWidth/Camera.main.aspect)/2;
			Camera.main.gameObject.transform.Translate(new Vector2 (0,  -(playingFieldHeight - 2*Camera.main.orthographicSize))/2);

		}

	
		//Configuration/Initializationcode at startup
		void Start ()
		{
			ServicePointManager.ServerCertificateValidationCallback = (p1, p2, p3, p4) => true;

			Debug.Log (EnemyDataCollection.Instance.enemyData[0].ToString());
			//must be created once from the main thread
			var dispatcher = UnityThreadHelper.Dispatcher;
			
			//Player.Fetch("123456", null, null);
			
			//ViewRouter.TheViewRouter.ShowView(typeof(OpponentSelectionView));
            CreateTestData();
			
		}
		
		private void CreateTestData(){
			TestData data = new TestData(OnSucess);
			
		}
		
		
		
		private void OnSucess(GameData data){
			Debug.Log (data.ToString());
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameView), data);
			
		}
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}
	
	}

}