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
		
				
		public const string GCM_ID_KEY = "gcmIdKey";
		public CommandQueue CommandQueue { get; set; }
		 
		
		public NotificationHandler ()
		{
			CommandQueue = new CommandQueue();
		}
		
		
		public void InitPushNotificationSupport(){
			
			GoogleCloudMessageService.instance.addEventListener(GoogleCloudMessageService.CLOUD_MESSAGE_LOADED, (e) => ProcessNotification(e.data as PushNotificationData));
			GoogleCloudMessageService.instance.addEventListener(GoogleCloudMessageService.STORED_NOTIFICATIONS_LOADED, OnStoredNotificationsLoaded);

			
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
		
		
		public void LoadNotificationsFromStore(){
			Debug.Log("Load notifications from store");
			GoogleCloudMessageService.instance.GetStoredNotifications();
		}
		
		private void OnStoredNotificationsLoaded(CEvent e){
			
			Debug.Log ("on stored notifications loaded");
			
			var notifications = e.data as List<PushNotificationData>;
			
			
			bool containsSyncNotification = false;
			
			foreach(PushNotificationData notificationData in notifications){
				
				if(notificationData.NotificationType == NotificationType.SYNC && !containsSyncNotification){
					containsSyncNotification = true;
					ProcessNotification(notificationData, true);
				}
				else if(notificationData.NotificationType != NotificationType.SYNC){
					ProcessNotification(notificationData, true);
				}
				
			}
			
		}
		
		private void ProcessNotification(PushNotificationData notificationData, bool isFromStore = false){
			Debug.Log("received notification: " + notificationData.message + "::" + notificationData.timestamp);
			if(notificationData.NotificationType == NotificationType.SYNC) {
				HandleSyncNotification(notificationData);
			}
			else if(notificationData.NotificationType == NotificationType.CHALLENGE_DENIED || notificationData.NotificationType == NotificationType.CONTINUE){
				var infoCommand = new ShowInfoAndSyncCommand(notificationData, CommandQueue);
				CommandQueue.Enqueue(infoCommand);
			}
		}
		
		private void HandleSyncNotification(PushNotificationData notificationData){

			var syncCommand = new SyncCommand(true, CommandQueue, notificationData.timestamp);
			syncCommand.Execute();
			
		}

		
		private void ActivateGcmIdOnServer(string id){
			
			ICommand gcmCommand = new GcmToServerCommand(id);
			gcmCommand.Execute();
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
		
		
		
	}
}

