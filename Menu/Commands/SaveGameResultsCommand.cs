using System;
using PYIV.Persistence;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class SaveGameResultsCommand : ICommand
	{
		
		private GameData game;
		private TweenPosition tp;
		
    public SaveGameResultsCommand (GameData saveGame, TweenPosition tp)
		{
			this.game = saveGame;
      		this.tp = tp;
		}
		
		public void Execute(){
			
			game.MyStatus.Gold += game.MyStatus.LatestRound.ScoreResult.Gold;
			game.Save(OnSaveSuccess, null, true);
		}
		
		private void OnSaveSuccess(GameData data){
			tp.enabled = true;
		}
		
		
		
	}
}

