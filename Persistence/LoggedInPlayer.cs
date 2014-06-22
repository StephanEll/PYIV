using System;
using UnityEngine;
using PYIV.Helper;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Persistence
{
	public class LoggedInPlayer
	{
		
		private const string GCM_ID_KEY = "gcmIdKey";
		
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
					instance.ManagePushNotificationSupport();
				}
			}
		}
		
		private void ManagePushNotificationSupport(){
			
			GCM.Initialize ();
			GCM.SetMessageCallback ((Dictionary<string, object> table) => {
            	Debug.Log ("Message!!! " + table.ToString ());
				NGUIDebug.Log ("Message!!! " + table.ToString ());
        	});
			GCM.SetErrorCallback ((string errorId) => {
            	Debug.Log ("Error!!! " + errorId);
        	});
			
			if(PlayerPrefs.HasKey(GCM_ID_KEY)){
				Debug.Log ("device has already a registration id");
				
				string gcmId = PlayerPrefs.GetString(GCM_ID_KEY);
				ActivateGcmIdOnServer(gcmId);
			}
			else{
				RegisterForGCM();
				Debug.Log("requesting a new registration id from google");
			}
			
		}
		
		private void ActivateGcmIdOnServer(string id){
			
			var req = new Request<object>("gcm", RestSharp.Method.POST);
			GcmData data = new GcmData();
			data.DeviceId = SystemInfo.deviceUniqueIdentifier;
			data.GcmId = id;
			
			req.AddBody(data);
			Debug.Log ("sending gcm save request to 3rd party server");
			req.ExecuteAsync();
			
			
			
			
		}

		
		private void RegisterForGCM(){
			GCM.SetRegisteredCallback (RegistrationIdReceived);
			string[] senderIds = {ConfigReader.Instance.GetSetting("server", "sender-id")};
			GCM.Register (senderIds);
		}
				
		private void RegistrationIdReceived(string regId){

			PlayerPrefs.SetString(GCM_ID_KEY, regId);
			PlayerPrefs.Save();
			
			Debug.Log ("Registration Id received: "+regId);
			
			ActivateGcmIdOnServer(regId);
		}
		
		
		public static bool IsLoggedIn(){
			return Instance != null;
		}
		
		public static void LogOut(){
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

		private class GcmData{
			
			public string DeviceId { get; set; }
			public string GcmId { get; set; }
			
		}
		
		
	}
}

