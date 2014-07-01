using System;
using UnityEngine;
using PYIV.Menu.Commands;
using PYIV.Persistence;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Helper.GCM;
using PYIV.Menu;

namespace PYIV.Menu.Commands
{
	public class ShowAcceptChallengeCommand : QueuedCommand
	{
		
		private Player player;
		private GameData newGame;
		private DecisionPopupParam param;
		
		public ShowAcceptChallengeCommand (GameData newGame, CommandQueue commandQueue) : base(commandQueue)
		{
			this.player = player;
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
			//newGame.Save();
			HandleNextCommand();
		}
		
		private void OnDecline(GameObject button){
			Debug.Log("GameDeclined");
			//newGame.Delete();
			HandleNextCommand();
		}
		
	}
}

