using System;
using PYIV.Menu.Popup;
using PYIV.Persistence;
using PYIV.Helper;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class LogOutCommand : ICommand
	{
		
		private DecisionPopupParam popupParam;
		
		public LogOutCommand ()
		{
			
			var message = "";
			if(LoggedInPlayer.Instance.GameList.HasUnsyncedGames()){
				message += StringConstants.UNSYNCED_GAME_RESULTS;
			}
			message += StringConstants.WANT_TO_LOGOUT;
			
			popupParam = new DecisionPopupParam(message, OnAccept, null);
			
		}
		
		public void Execute()
		{
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(DecisionPopup), popupParam);
			
		}
		
		private void OnAccept(GameObject go){
			LoggedInPlayer.Instance.Player.Logout(LogoutComplete, null);
		}
		
		private void LogoutComplete(Player none){
			LoggedInPlayer.Instance.LogOut();
			ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
		}
		
	}
}

