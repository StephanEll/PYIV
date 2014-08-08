using System;
using UnityEngine;
using PYIV.Menu.Commands;
using PYIV.Persistence;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Helper.GCM;
using PYIV.Menu;
using RestSharp;
using PYIV.Persistence.Errors;

namespace PYIV.Menu.Commands
{
	public class ShowAcceptChallengeCommand : QueuedCommand
	{
		
		private GameData newGame;
		private DecisionPopupParam param;
		
		public ShowAcceptChallengeCommand (GameData newGame, CommandQueue commandQueue) : base(commandQueue)
		{
			this.newGame = newGame;
			
			string message = String.Format(StringConstants.NEW_CHALLENGE, newGame.OpponentStatus.Player.Name);
			param = new DecisionPopupParam(message, OnAccept, OnDecline);
			
		}
		
		public override void Execute ()
		{
			base.Execute ();
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(DecisionPopup), param);
		}
		
		private void OnAccept(GameObject button){
			Debug.Log ("GameAccepted");
			newGame.MyStatus.IsChallengeAccepted = true;
			
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(IndianSelectionView), new AttackConfigurationModel(newGame));
		}
		
		
		private void OnError(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
		
		private void OnDecline(GameObject button){
			Debug.Log("GameDeclined");
			newGame.Delete(OnDeclineSuccess, OnError);
			HandleNextCommand();
		}
		
		private void OnDeclineSuccess(GameData d){
			LoggedInPlayer.Instance.GameList.RemoveModel(newGame);
		}
		
	}
}

