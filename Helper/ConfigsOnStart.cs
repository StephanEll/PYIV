using UnityEngine;
using System;
using System.Collections;
using PYIV.Menu;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;
using System.Net;


namespace PYIV.Helper{
	public class ConfigsOnStart : MonoBehaviour
	{
		GameData gameData;
	
		//Configuration/Initializationcode at startup
		void Start ()
		{
			ServicePointManager.ServerCertificateValidationCallback = (p1, p2, p3, p4) => true;
			NGUIDebug.Log("Application start 2");
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