using System;
using PYIV.Persistence;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class SaveGameResultsCommand : ICommand
	{
		
		private GameData game;
		
		public SaveGameResultsCommand (GameData saveGame)
		{
			this.game = saveGame;
			
		}
		
		public void Execute(){
			
			game.MyStatus.Gold += game.MyStatus.LatestRound.ScoreResult.Gold;
			game.Save(OnSaveSuccess, null);
		}
		
		private void OnSaveSuccess(GameData data){
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameResultView), game);
		}
		
		
		
	}
}

