using System;
using System.Linq;
using System.Collections.Generic;
using RestSharp;
using PYIV.Menu.Commands;
using UnityEngine;
using System.Runtime.Serialization;


namespace PYIV.Persistence
{
	[Serializable]
	public class GameCollection
	{
		
		private const string RESOURCE = "gameDataCollection";
		
		public List<GameData> ModelList { get; set; }
		
		
		public List<GameData> RunningGames { 
			get {
				return (from game in ModelList where game.MyStatus.IsChallengeAccepted select game).ToList();
			}
		}
		
		
		public List<GameData> UnacceptedGames { 
			get {
				return (from game in ModelList where !game.MyStatus.IsChallengeAccepted select game).ToList();
			}
		}
		
		public List<GameData> UnsyncedGames {
			get {	
				return (from game in ModelList where !game.IsSynced select game).ToList();
			}
		}
		
		public delegate void ChangeDelegate();
		
		[field:NonSerialized] 
		public event ChangeDelegate OnChange;
		
		
		public GameCollection ()
		{
		}
		
		public GameCollection(List<GameData> unsyncedGames){
			this.ModelList = unsyncedGames;
		}
		
		public static void FetchAll(Request<GameCollection>.SuccessDelegate OnSuccess, Request<GameCollection>.ErrorDelegate OnError){
			var getRequest = new Request<GameCollection>(RESOURCE,Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += (responseObject) => responseObject.CreateAcceptGameCommandsAndAddToQueue(LoggedInPlayer.Instance.NotificationHandler.CommandQueue);
			getRequest.OnSuccess += OnSuccess;
			getRequest.ExecuteAsync();
			
		}
		
		public void AddModel(GameData model){
			this.ModelList.Insert(0, model);
			Update ();
		}
		
		public void RemoveModel(GameData model){
			this.ModelList.Remove(model);
			Update();
		}
		
		
		public void Sync(Request<GameSyncResponse>.SuccessDelegate OnSuccess, Request<GameSyncResponse>.ErrorDelegate OnError, bool doInBackground){
			
			
			var syncRequest = new Request<GameSyncResponse>(RESOURCE, Method.PUT, doInBackground);
			syncRequest.AddBody(UnsyncedGames);
			syncRequest.OnSuccess += ParseChanges;
			
			syncRequest.OnError += OnError;
			syncRequest.OnSuccess += OnSuccess;
			Debug.Log("execute sync request");
			syncRequest.ExecuteAsync();
			
		}

		void ParseChanges (GameSyncResponse gameSyncResponse)
		{
			
			Debug.Log("vor response : " + ModelList.Count);
			
			foreach(GameData updatedGame in gameSyncResponse.ModelList){
				
				bool isGameExisting = false;
				
				foreach(GameData existingGame in ModelList){
					if(updatedGame.Equals(existingGame)){
						Debug.Log("update existing game with id: " + updatedGame.Id);
						existingGame.OpponentStatus.ParseOnCreate(updatedGame.OpponentStatus);
						existingGame.IsSynced = true;
						isGameExisting = true;
						break;
					}					
				}
				
				//add new games
				if(!isGameExisting){
					Debug.Log("add new game with id: "+updatedGame.Id);
					ModelList.Add(updatedGame);
				}
			}
			
			//Delete games from ModelList
			List<GameData> deletedGames = ModelList.Except(gameSyncResponse.ModelList).ToList();
			foreach(GameData gameData in deletedGames){
				Debug.Log("Delete existing game");
				ModelList.Remove(gameData);
			}
			
			CreateAcceptGameCommandsAndAddToQueue(LoggedInPlayer.Instance.NotificationHandler.CommandQueue);
			
			Debug.Log("nach response : " + ModelList.Count);
			
			Update ();

		}
		
		public void Update(){
			if(OnChange != null){
				OnChange();
			}
		}
		
		public void CreateAcceptGameCommandsAndAddToQueue(CommandQueue commandQueue){
			foreach(GameData unacceptedGame in UnacceptedGames){
				var acceptChallengeCommand = new ShowAcceptChallengeCommand(unacceptedGame, commandQueue);
				commandQueue.Enqueue(acceptChallengeCommand);
			}
		}
		
		
		
		public bool HasUnsyncedGames(){
			return UnsyncedGames.Count > 0;
		}
		
		
	}
}

