using System;
using PYIV.Persistence;
using UnityEngine;

namespace PYIV.Menu.Commands
{
	public class SaveGameResultsCommand : ICommand
	{
		
		private GameData game;
    private GameObject okButton;
		
    public SaveGameResultsCommand (GameData saveGame, GameObject okButton)
		{
			this.game = saveGame;
      this.okButton = okButton;
		}
		
		public void Execute(){
			
			game.MyStatus.Gold += game.MyStatus.LatestRound.ScoreResult.Gold;
			game.Save(OnSaveSuccess, null, true);
		}
		
		private void OnSaveSuccess(GameData data){
      okButton.SetActive(true);
		}
		
		
		
	}
}

