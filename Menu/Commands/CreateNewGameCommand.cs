using System;
using PYIV.Menu.Commands;
using PYIV.Persistence;
using PYIV.Persistence.Errors;

namespace PYIV.Menu.Commands
{
	public class CreateNewGameCommand : ICommand
	{
		
		private Player opponent;
		private Player challenger;
		private ServerCollection<GameData> gameList;
		private GameData newGame;
		
		public CreateNewGameCommand (Player opponent, Player challenger, ServerCollection<GameData> gameList)
		{
			this.opponent = opponent;
			this.challenger = challenger;
			this.gameList = gameList;
			
			newGame = new GameData(challenger, opponent);
		}

		
		public void Execute(){
			newGame.Save(NewGameCreated, ErrorCreatingGame);
		}
		
		private void NewGameCreated(GameData gameData){
			gameList.AddModel(newGame);
			
			NGUIDebug.Log("TODO: To indian selection screen");
			//next screen
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
			
		}
		
		private void ErrorCreatingGame(RestException e){
			
		}
	}
}

