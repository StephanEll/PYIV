using System;
using UnityEngine;
using PYIV.Helper;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PYIV.Persistence
{
	public class LoggedInPlayer
	{

		
		private static volatile LoggedInPlayer instance;
		private static object syncRoot = new object ();
		private GameCollection gameList;

		public NotificationHandler NotificationHandler { get; set; }
		
		public Player Player { get; set; }
		
		private LoggedInPlayer (Player player)
		{
			lock (syncRoot) {
				Player = player;
				this.NotificationHandler = new NotificationHandler ();
			}
		}
		
		public static LoggedInPlayer Instance {
			get {
				lock (syncRoot) {
					return instance;
				}
			}
		}
		
		public static void Login (Player player)
		{
			lock (syncRoot) {
				if (instance == null) {
					instance = new LoggedInPlayer (player);
					player.PersistAuthData ();
					instance.NotificationHandler.InitPushNotificationSupport ();
				}
			}
		}
		
		public static bool IsLoggedIn ()
		{
			return Instance != null;
		}
		
		public void LogOut ()
		{
			lock (syncRoot) {
				Player.DeleteAuthData ();
				LocalDataPersistence.DeleteFile(LocalDataPersistence.GAMES_FILENAME);
				instance = null;
			}
		}
		
		public void GetOrFetchGameList (Request<GameCollection>.SuccessDelegate OnSuccess, Request<GameCollection>.ErrorDelegate OnError)
		{
			
			if (gameList != null) {
				OnSuccess (gameList);
			} else {
				try {
					SyncLocalChangesAndSetGameList(OnSuccess, OnError);
				} catch (IOException e) {
					FetchCompleteGameList(OnSuccess, OnError);
				}
			}
		}
		
		private void SyncLocalChangesAndSetGameList(Request<GameCollection>.SuccessDelegate OnSuccess, Request<GameCollection>.ErrorDelegate OnError){
			var unsyncedGames = LocalDataPersistence.Load<List<GameData>> (LocalDataPersistence.GAMES_FILENAME);
			LocalDataPersistence.DeleteFile(LocalDataPersistence.GAMES_FILENAME);
			GameCollection gameCollection = new GameCollection (unsyncedGames);
			gameCollection.Sync ((responseObject) => {
				this.gameList = gameCollection;
				OnSuccess (this.gameList);
			}, OnError, false);
		}
		
		private void FetchCompleteGameList(Request<GameCollection>.SuccessDelegate OnSuccess, Request<GameCollection>.ErrorDelegate OnError){
			GameCollection.FetchAll ((responseObject) => {
				this.gameList = responseObject;
				OnSuccess (this.gameList);
			}, OnError);
		}
		
		public GameCollection GameList {
			get { return gameList; }
		}

		
		
		
	}
}

