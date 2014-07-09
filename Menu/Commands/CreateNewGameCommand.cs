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
			
			newGame.MyStatus.IndianData = IndianDataCollection.Instance.IndianData[1];
			newGame.OpponentStatus.IndianData = IndianDataCollection.Instance.IndianData[1];
			
			List<EnemyType> types = new List<EnemyType>();
            types.Add(EnemyTypeCollection.Instance.EnemyType[0]);
            types.Add(EnemyTypeCollection.Instance.EnemyType[2]);
            types.Add(EnemyTypeCollection.Instance.EnemyType[3]);
            types.Add(EnemyTypeCollection.Instance.EnemyType[5]);

			Round round = new Round();
			round.SentAttackers = types;
			newGame.OpponentStatus.AddRound(round);
			newGame.MyStatus.AddRound(round);
			
			newGame.IsSynced = false;
			
		}

		
		public void Execute(){
			if(this.opponent.Equals(this.challenger)){
				ViewRouter.TheViewRouter.ShowTextPopup(StringConstants.SELF_MATCH);
				return;
			}
			
			newGame.Save(NewGameCreated, ErrorCreatingGame);
			
		}
		
		private void NewGameCreated(GameData gameData){
			
			LoggedInPlayer.Instance.GameList.AddModel(newGame);
			//next screen
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
			
		}
		
		private void ErrorCreatingGame(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
	}
}

