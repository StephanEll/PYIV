using System;
using UnityEngine;
using PYIV.Helper;
using PYIV.Helper.GCM;
using System.Collections;
using System.Collections.Generic;

namespace PYIV.Persistence
{
	public class NotificationHandler
	{
		
				
		private const string GCM_ID_KEY = "gcmIdKey";
		
		public NotificationHandler ()
		{
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
			GoogleCloudMessageService.instance.GetStoredNotifications();
		}
		
		private void OnStoredNotificationsLoaded(CEvent e){
			var notifications = e.data as List<PushNotificationData>;
			foreach(PushNotificationData notificationData in notifications){
				ProcessNotification(notificationData);
			}
			
		}
		
		private void ProcessNotification(PushNotificationData notificationData){
			
		}
		
		private void OnNotificationReceived(CEvent e){
			NGUIDebug.Log(e.data as string);
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

