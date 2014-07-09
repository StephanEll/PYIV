using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Helper.GCM;

namespace PYIV.Menu.Commands
{
	public class GcmToServerCommand : ICommand
	{
		private string gcmId;
		
		public GcmToServerCommand (string gcmId)
		{
			this.gcmId = gcmId;
		}
		
		public void Execute(){
			
			var req = new Request<object>("gcm", RestSharp.Method.POST);
			var data = CreateGcmData(gcmId);
			
			req.AddBody(data);
			Debug.Log ("sending gcm save request to 3rd party server");
			req.ExecuteAsync();
		}
		
		public static GcmData CreateGcmData(string id){
			GcmData data = new GcmData();
			data.DeviceId = SystemInfo.deviceUniqueIdentifier;
			data.GcmId = id;
			return data;
		}
		
	}
}

