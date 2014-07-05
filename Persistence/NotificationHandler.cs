using System;
using UnityEngine;
using PYIV.Helper;
using PYIV.Helper.GCM;
using System.Collections;
using System.Collections.Generic;
using PYIV.Menu.Commands;
using PYIV.Menu;

namespace PYIV.Persistence
{
	public class NotificationHandler
	{
		
				
		private const string GCM_ID_KEY = "gcmIdKey";
		public CommandQueue CommandQueue { get; set; } 
		
		public NotificationHandler ()
		{
			CommandQueue = new CommandQueue();
		}
		
		
		public void InitPushNotificationSupport(){
			
			GoogleCloudMessageService.instance.addEventListener(GoogleCloudMessageService.CLOUD_MESSAGE_LOADED, (e) => ProcessNotification(e.data as PushNotificationData));
			GoogleCloudMessageService.instance.addEventListener(GoogleCloudMessageService.STORED_NOTIFICATIONS_LOADED, OnStoredNotificationsLoaded);

			
			if(PlayerPrefs.HasKey(GCM_ID_KEY)){
				NGUIDebug.Log ("device has already a registration id");
				
				string gcmId = PlayerPrefs.GetString(GCM_ID_KEY);
				ActivateGcmIdOnServer(gcmId);
			}
			else{
				RegisterForGCM();
				NGUIDebug.Log("requesting a new registration id from google");
			}
			
		}
		
		
		public void LoadNotificationsFromStore(){
			NGUIDebug.Log("Load notifications from store");
			GoogleCloudMessageService.instance.GetStoredNotifications();
		}
		
		private void OnStoredNotificationsLoaded(CEvent e){
			var notifications = e.data as List<PushNotificationData>;
			foreach(PushNotificationData notificationData in notifications){
				ProcessNotification(notificationData, true);
			}
			
		}
		
		private void ProcessNotification(PushNotificationData notificationData, bool isFromStore = false){
			NGUIDebug.Log("NOTIFICATION RECEIVED" + notificationData.message);
			switch (notificationData.NotificationType) {
			case NotificationType.SYNC:
				Debug.Log ("Process sync notification");
				var syncCommand = new SyncCommand(CommandQueue);
				syncCommand.Execute();
				break;
			case NotificationType.CHALLENGE_DENIED:
				var challengeDeniedCommand = new ShowChallengeDeniedCommand(notificationData, CommandQueue);
				CommandQueue.Enqueue(challengeDeniedCommand);
				break;
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
			GoogleCloudMessageService.instance.addEventListener(GoogleCloudMessageService.CLOUD_MESSAGE_SERVICE_REGISTRATION_RECIVED, RegistrationIdReceived);
			string senderId = ConfigReader.Instance.GetSetting("server", "sender-id");
			GoogleCloudMessageService.instance.RgisterDevice(senderId);
		
		}
				
		private void RegistrationIdReceived(CEvent e){
			string regId = GoogleCloudMessageService.instance.registrationId;
			PlayerPrefs.SetString(GCM_ID_KEY, regId);
			PlayerPrefs.Save();
			
			Debug.Log ("Registration Id received: "+regId);
			
			ActivateGcmIdOnServer(regId);
		}
		
		
		
		private class GcmData{
			
			public string DeviceId { get; set; }
			public string GcmId { get; set; }
			
		}
		
		
		
	}
}

