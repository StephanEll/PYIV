using System;
using PYIV.Menu.Commands;
using PYIV.Menu.Popup;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;

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
		}

		
		public void Execute(){
			if(this.opponent.Id == this.challenger.Id){
				ViewRouter.TheViewRouter.ShowTextPopup(StringConstants.SELF_MATCH);
				return;
			}
			
			newGame.Save(NewGameCreated, ErrorCreatingGame);
		}
		
		private void NewGameCreated(GameData gameData){
			LoggedInPlayer.Instance.GameList.AddModel(newGame);
			
			NGUIDebug.Log("TODO: To indian selection screen");
			//next screen
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
			
		}
		
		private void ErrorCreatingGame(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
	}
}

