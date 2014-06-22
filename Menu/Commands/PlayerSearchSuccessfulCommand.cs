using System;
using PYIV.Persistence;
using PYIV.Menu.Popup;
using PYIV.Helper;
using UnityEngine;


namespace PYIV.Menu.Commands
{
	public class PlayerSearchSuccessfulCommand : ICommand
	{
		private Player searchedPlayer;
		
		public PlayerSearchSuccessfulCommand (Player player)
		{
			searchedPlayer = player;
			
		}
		
		public void Execute(){
			
			string message = String.Format(StringConstants.REALLY_ATTACK, searchedPlayer.Name);
			DecisionPopupParam param = new DecisionPopupParam(message, OnAccept, OnDecline);
			
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(DecisionPopup), param);
		}
		
		private void OnAccept(GameObject button){

			var newGameCommand = new CreateNewGameCommand(searchedPlayer, LoggedInPlayer.Instance.Player);
			newGameCommand.Execute();
			
			
		}

		
		
		
		private void OnDecline(GameObject button){
			Debug.Log ("then search on");	
		}
	}
}

