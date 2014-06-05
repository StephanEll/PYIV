using UnityEngine;
using System;
using System.Collections;
using PYIV.Menu;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using RestSharp;

namespace PYIV.Helper{
	public class ConfigsOnStart : MonoBehaviour
	{
		GameData gameData;
	
		//Configuration/Initializationcode at startup
		void Start ()
		{
			//must be created once from the main thread
			var dispatcher = UnityThreadHelper.Dispatcher;
			
			
			
			//ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
            CreateTestData();
			
		}
		
		private void CreateTestData(){
			TestData data = new TestData(OnSucess);
			
		}
		
		
		private void PlayerSaved(Player player){
			Debug.Log (player.Id);
			/*Round round = new Round();
			round.RemainingVillageLifepoints = 99;
			round.SentAttackerIds = new System.Collections.Generic.List<int>();
			round.SentAttackerIds.Add(43);
			
			PlayerStatus status = new PlayerStatus((Player)player);
			status.AddRound(round);
			status.Save(OnSucess, OnError);
			*/
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