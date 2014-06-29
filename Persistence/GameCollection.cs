using System;
using System.Linq;
using System.Collections.Generic;
using RestSharp;


namespace PYIV.Persistence
{
	public class GameCollection
	{
		
		private const string RESOURCE = "gameDataCollection";
		
		public List<GameData> ModelList { get; set; }
		public delegate void ModelAddedDelegate(List<GameData> newList, GameData newModel);
		public event ModelAddedDelegate OnModelAdded;
		
		private List<GameData> unacceptedGames = new List<GameData>();
		
		
		public GameCollection ()
		{
		}
		
		public static void FetchAll(Request<GameCollection>.SuccessDelegate OnSuccess, Request<GameCollection>.ErrorDelegate OnError){
			var getRequest = new Request<GameCollection>(RESOURCE,Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			getRequest.ExecuteAsync();
			
		}
		
		public void AddModel(GameData model){
			this.ModelList.Add(model);
			if(OnModelAdded != null){
				OnModelAdded(this.ModelList, model);
			}
		}
		
		
		public void Sync(Request<GameSyncResponse>.SuccessDelegate OnSuccess, Request<GameSyncResponse>.ErrorDelegate OnError){
			var unsyncedGames = FindUnsyncedGames();
			
			var syncRequest = new Request<GameSyncResponse>(RESOURCE, Method.PUT);
			syncRequest.AddBody(unsyncedGames);
			syncRequest.OnSuccess += ParseChanges;
			
			syncRequest.OnError += OnError;
			syncRequest.OnSuccess += OnSuccess;
			
			syncRequest.ExecuteAsync();
			
		}

		void ParseChanges (GameSyncResponse gameSyncResponse)
		{
			
			List<GameData> newGames = new List<GameData>();
			
			foreach(GameData updatedGame in gameSyncResponse.ModelList){
				foreach(GameData existingGame in ModelList){
					if(updatedGame.Equals(existingGame)){
						existingGame.OpponentStatus.ParseOnCreate(updatedGame.OpponentStatus);
						break;
					}
					
					//if this point is reached it must be a new game
					newGames.Add(updatedGame);
					
				}
			}
			
			this.unacceptedGames = newGames;

		}
		
		public List<GameData> GetAndResetUnacceptedGames(){
			var tmpUnacceptedGames = this.unacceptedGames;
			this.unacceptedGames = new List<GameData>();
			return tmpUnacceptedGames;
		}
		
		private List<GameData> FindUnsyncedGames(){
			return new List<GameData>();
		}
		
		
	}
}

