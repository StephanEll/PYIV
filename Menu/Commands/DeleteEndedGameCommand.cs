using System;
using PYIV.Persistence;
using PYIV.Menu.Popup;
using PYIV.Persistence.Errors;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class DeleteEndedGameCommand : ICommand
	{
		public delegate void OnDeleteSuccess();
		private GameData gameData;
		private OnDeleteSuccess onSuccess;
		
		
		public DeleteEndedGameCommand (GameData gameData, OnDeleteSuccess onSuccess)
		{
			this.gameData = gameData;
			this.onSuccess = onSuccess;
		}
		
		public void Execute(){
			gameData.Delete(OnSuccess, OnError);
			Debug.Log ("delete the game");
		}
		
		private void OnSuccess(object empty){
			LoggedInPlayer.Instance.GameList.RemoveModel(gameData);
			Debug.Log ("successful");
			onSuccess();
		}
		
		private void OnError(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
		
	}
}

