using System;
using PYIV.Persistence;

namespace PYIV.Menu.Commands
{
	public class SaveGameResultsCommand : ICommand
	{
		
		private GameData savedGame;
		
		public SaveGameResultsCommand (GameData saveGame)
		{
			this.savedGame = savedGame;
			
		}
		
		public void Execute(){
			savedGame.MyStatus.Gold = savedGame.MyStatus.LatestRound.ScoreResult.Gold;
			savedGame.Save(OnSaveSuccess, null);
		}
		
		private void OnSaveSuccess(GameData data){
			
		}
		
		
		
	}
}

