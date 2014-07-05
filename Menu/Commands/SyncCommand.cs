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
		
		public SyncCommand (bool doInBackground, CommandQueue commandQueue)
		{
			this.commandQueue = commandQueue;
			this.doInBackground = doInBackground;
		}
		
		public void Execute(){
			if(LoggedInPlayer.Instance.GameList != null){
				LoggedInPlayer.Instance.GameList.Sync(OnSuccess, OnError, doInBackground);
			}
		}
		
		private void OnSuccess(GameSyncResponse response){
			
		}
		
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}
		
		
	}
}

