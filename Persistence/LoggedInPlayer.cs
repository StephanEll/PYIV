using System;
using UnityEngine;
using PYIV.Helper;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Persistence
{
	public class LoggedInPlayer
	{

		
		private static volatile LoggedInPlayer instance;
		private static object syncRoot = new object();
		
		private ServerCollection<GameData> gameList;
		public NotificationHandler NotificationHandler { get; set; }
		
		
		public Player Player { get; set; }
		
		private LoggedInPlayer(Player player){
			lock(syncRoot){
				Player = player;
				this.NotificationHandler = new NotificationHandler();
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
					instance.NotificationHandler.InitPushNotificationSupport();
				}
			}
		}
		
		
		public static bool IsLoggedIn(){
			return Instance != null;
		}
		
		public void LogOut(){
			lock(syncRoot){
				Player.DeleteAuthData();
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

