using System;
using PYIV.Menu.Commands;
using PYIV.Menu.Popup;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;
using PYIV.Gameplay.Character;

namespace PYIV.Menu.Commands
{
	public class CreateNewGameCommand : ICommand
	{
		
		private Player opponent;
		private Player challenger;
		private GameData newGame;
		
		public CreateNewGameCommand (Player opponent, Player challenger)
		{
			this.opponent = opponent;
			this.challenger = challenger;			
			newGame = new GameData(challenger, opponent);
			
			//TODO: Auswahl-View hierfür erstellen
			newGame.MyStatus.IndianData = IndianDataCollection.Instance.IndianData[1];			
			newGame.OpponentStatus.IndianData = IndianDataCollection.Instance.IndianData[1];	
			newGame.MyStatus.AddRound(new Round());
			newGame.OpponentStatus.AddRound(new Round());
			
			newGame.IsSynced = false;
			
		}

		
		public void Execute(){
			if(this.opponent.Equals(this.challenger)){
				ViewRouter.TheViewRouter.ShowTextPopup(StringConstants.SELF_MATCH);
				return;
			}
			
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(IndianSelectionView), new AttackConfigurationModel(newGame));
			
			
			//newGame.Save(NewGameCreated, ErrorCreatingGame);
			
		}
		
		/*
		private void NewGameCreated(GameData gameData){
			
			LoggedInPlayer.Instance.GameList.AddModel(newGame);
			//next screen
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
			
		}
		
		private void ErrorCreatingGame(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}*/
	}
}

