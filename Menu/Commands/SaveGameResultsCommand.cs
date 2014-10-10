using System;
using PYIV.Persistence;
using UnityEngine;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;


namespace PYIV.Menu.Commands
{
	public class SaveGameResultsCommand : ICommand
	{
		
		private GameData game;
		private GameObject button;
		private TweenPosition tweenPos;
		
    public SaveGameResultsCommand (GameData saveGame, GameObject button)
		{
			this.game = saveGame;
      		this.button = button;
			tweenPos = button.GetComponent<TweenPosition>();
		}
		
		public void Execute(){
			
			game.MyStatus.Gold += game.MyStatus.LatestRound.ScoreResult.Gold;
			game.Save(OnSaveSuccess, OnSaveError, true);
		}
		
		private void OnSaveSuccess(GameData data){
			tweenPos.enabled = true;
			
			UIEventListener.Get(button).onClick += (g) => {
				ViewRouter.TheViewRouter.ShowViewWithParameter (typeof(GameResultView), game);
			};
			
		}
		
		private void OnSaveError(RestException e){
			tweenPos.enabled = true;
			
			UIEventListener.Get(button).onClick += (g) => {
				ViewRouter.TheViewRouter.ShowView (typeof(GameListView));
			};
			
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
			
		}
		
		
		
	}
}

