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
		
		public SyncCommand (CommandQueue commandQueue)
		{
			this.commandQueue = commandQueue;
		}
		
		public void Execute(){
			if(LoggedInPlayer.Instance.GameList != null){
				LoggedInPlayer.Instance.GameList.Sync(OnSuccess, OnError);
			}
		}
		
		private void OnSuccess(GameSyncResponse response){
			
			LoggedInPlayer.Instance.GameList.CreateAcceptGameCommandsAndAddToQueue(commandQueue);
			
		}
		
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}
		
		
	}
}

