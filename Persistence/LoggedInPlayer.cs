using System;
using UnityEngine;

namespace PYIV.Persistence
{
	public class LoggedInPlayer
	{
		
		private static volatile LoggedInPlayer instance;
		private static object syncRoot = new object();
		
		private ServerCollection<GameData> gameList;
		
		
		public Player Player { get; set; }
		
		private LoggedInPlayer(Player player){
			lock(syncRoot){
				Player = player;
			}
		}
		
		public static LoggedInPlayer Instance {
			get {
				lock(syncRoot){
					return instance;
				}
			}
		}
		
		public static void Login(Player player){
			lock(syncRoot){
				if(instance == null){
					instance = new LoggedInPlayer(player);
					player.PersistAuthData();
				}
			}
		}
		
		public static bool IsLoggedIn(){
			return Instance != null;
		}
		
		public static void LogOut(){
			lock(syncRoot){
				PlayerPrefs.DeleteAll();
				instance = null;
			}
		}
		
		public void GetOrFetchGameList(Request<ServerCollection<GameData>>.SuccessDelegate OnSuccess, Request<ServerCollection<GameData>>.ErrorDelegate OnError){
			
			if(gameList != null){
				OnSuccess(gameList);
			}
			else{
				ServerCollection<GameData>.FetchAll((responseObject) => {
					this.gameList = responseObject;
					OnSuccess(this.gameList);
				}, OnError);
			}
			
		}
		
		public ServerCollection<GameData> GameList{
			get { return gameList; }
		}

		
		
		
	}
}

