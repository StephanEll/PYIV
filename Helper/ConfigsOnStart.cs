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
using PYIV.Helper.GCM;
using PYIV.Menu.Commands;
using PYIV.Gameplay.Score;

namespace PYIV.Helper
{
  public class ConfigsOnStart : MonoBehaviour
  {
    private GameData gameData;
    private Player player;

  
    //Configuration/Initializationcode at startup
    void Start()
    {
			
      EnemyType[] et = EnemyTypeCollection.Instance.GetSubCollection(new[] { "Rat1", "Rat1", "Rat1" });
      Debug.Log("Count First: " + et[0].Count);
      
      et = EnemyTypeCollection.Instance.GetSubCollection(new[] { "Rat1", "Rat1", "Rat1" });
      Debug.Log("Count Second: " + et[0].Count);

      //ignores certificate check when dealing with ssl
      ServicePointManager.ServerCertificateValidationCallback = (p1, p2, p3, p4) => true;

      //must be created once from the main thread
      var dispatcher = UnityThreadHelper.Dispatcher;
      
      //Push notification initializer
      var gcm = GoogleCloudMessageService.instance;
      gcm.SetNotificationEnabled(false);

      //CreateTestData();
			
			
		
			

      ShowStartScreen();
      gameObject.AddComponent<MusicHelper>();
      
    }
    
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        ViewRouter.TheViewRouter.GoBack();
    }

    private void ShowStartScreen()
    {
      player = new Player();
      try
      {
        player.GetByAuthData(OnPlayerAuthenticated, OnPlayerAuthenticationFailed);

      } catch (Exception e)
      {
        Debug.Log(e);
        ViewRouter.TheViewRouter.ShowView(typeof(RegisterView));
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
    
    void OnApplicationPause(bool paused)
    {
      if (paused == true)
      {
        ApplicationLostFocus();
      } else
      {
        ApplicationGotFocus();
      }
    }
    
    private void ApplicationLostFocus()
    {
      GoogleCloudMessageService.instance.SetNotificationEnabled(true);
     
    }
    
    private void ApplicationGotFocus()
    {
      GoogleCloudMessageService.instance.SetNotificationEnabled(false);

      if (LoggedInPlayer.IsLoggedIn())
      {
        SyncMemoryDataWithServer();
      }
    }
    
    private void SyncMemoryDataWithServer()
    {
      if (LoggedInPlayer.Instance.GameList != null && LoggedInPlayer.Instance.GameList.HasUnsyncedGames())
      {
        Debug.Log("Sync games in memory");
        var syncCommand = new SyncCommand(true, LoggedInPlayer.Instance.NotificationHandler.CommandQueue);
        
        syncCommand.Execute();
      }
    }
    
    private void CreateTestData()
    {
      TestData data = new TestData(OnSucess);
      
    }
    
    private void OnSucess(GameData data)
    {
			
		//var saveResultsCommand = new SaveGameResultsCommand(data);
		//saveResultsCommand.Execute();
		ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameListView), data);
      
    }

    private void OnError(RestException e)
    {
      Debug.Log(e.Message);
    }

  
  }

}