using System;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper.GCM;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class SyncCommand : ICommand
	{
		
		private CommandQueue commandQueue;
		private bool doInBackground;
		private DateTime timestamp;
		
		public SyncCommand (bool doInBackground, CommandQueue commandQueue, DateTime timestamp=default(DateTime))
		{
			this.commandQueue = commandQueue;
			this.doInBackground = doInBackground;
			this.timestamp = timestamp;
		}
		
		public void Execute(){
			bool syncNecessary = true;
			if(timestamp != default(DateTime)){
				syncNecessary = timestamp > LoggedInPlayer.Instance.GameList.LatestSync;
			}
			
			if(syncNecessary){
				LoggedInPlayer.Instance.GameList.Sync(OnSuccess, OnError, doInBackground);
			}
			else{
				Debug.Log("skip sync, already up to date");
			}
		}
		
		private void OnSuccess(GameSyncResponse response){
			
		}
		
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}
		
		
	}
}

