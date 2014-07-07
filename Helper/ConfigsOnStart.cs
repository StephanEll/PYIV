using UnityEngine;
using System;
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
		private GameData gameData;

    private Player player;


		//Configuration Camera at startup
		void Awake(){

			
			 
		}

	
		//Configuration/Initializationcode at startup
		void Start ()
		{
			//ignores certificate check when dealing with ssl
			ServicePointManager.ServerCertificateValidationCallback = (p1, p2, p3, p4) => true;

			//must be created once from the main thread
			var dispatcher = UnityThreadHelper.Dispatcher;
			
			//Push notification initializer
			var gcm = GoogleCloudMessageService.instance;
			gcm.SetNotificationEnabled(false);
			
            ShowStartScreen();
            //CreateTestData();
			
		}

        private void ShowStartScreen()
        {
            player = new Player();
            try
            {
                player.GetByAuthData(OnPlayerAuthenticated, OnPlayerAuthenticationFailed);

            }
            catch (Exception e)
            {
                Debug.Log(e);
                ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
            }
        }
		
		
		

        private void OnPlayerAuthenticated(Player player)
        {
            LoggedInPlayer.Login(this.player);
            ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
        }

        private void OnPlayerAuthenticationFailed(RestException e)
        {
            ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
        }
		
		void OnApplicationPause(bool paused){
			if(paused == true){
				ApplicationLostFocus();
			}
			else{
				ApplicationGotFocus();
			}
		}
		
		private void ApplicationLostFocus(){
			GoogleCloudMessageService.instance.SetNotificationEnabled(true);
		}
		
		private void ApplicationGotFocus(){
			GoogleCloudMessageService.instance.SetNotificationEnabled(false);
			if(LoggedInPlayer.IsLoggedIn()){
				LoggedInPlayer.Instance.NotificationHandler.LoadNotificationsFromStore();
			}
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