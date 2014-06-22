using UnityEngine;
using System;
using System.Collections;
using PYIV.Menu;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;
using System.Net;
using PYIV.Gameplay.Enemy;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Helper{
	public class ConfigsOnStart : MonoBehaviour
	{
		GameData gameData;

		Camera camera;
		private Player player;

		
		



		//Configuration Camera at startup
		void Awake(){

			camera = Camera.main;
			
			 
		}

	
		//Configuration/Initializationcode at startup
		void Start ()
		{
			//ignores certificate check when dealing with ssl
			ServicePointManager.ServerCertificateValidationCallback = (p1, p2, p3, p4) => true;

			//must be created once from the main thread
			var dispatcher = UnityThreadHelper.Dispatcher;
			
			
			ShowStartScreen();            


			
		}
		
		private void ShowStartScreen(){
			player = new Player();
			try{
				player.GetByAuthData(OnPlayerAuthenticated, OnPlayerAuthenticationFailed);
				
			}
			catch(Exception e){
				Debug.Log(e);
				ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
			}
		}
		
		private void OnPlayerAuthenticated(Player player){
			LoggedInPlayer.Login(this.player);
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
		
		private void OnPlayerAuthenticationFailed(RestException e){
			ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
		}
		
		private void CreateTestData(){
			TestData data = new TestData(OnSucess);
			
		}
		
		
		
		private void OnSucess(GameData data){
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameView), data);
			
		}
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}

	
	}

}