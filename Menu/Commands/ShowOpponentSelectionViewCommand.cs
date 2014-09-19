using System;
using PYIV.Persistence;
using System.Collections.Generic;
using RestSharp;

namespace PYIV.Menu.Commands
{
	public class ShowOpponentSelectionViewCommand : ICommand
	{
		public ShowOpponentSelectionViewCommand ()
		{
		}
		
		public void Execute(){
			Request<List<Player>> randomPlayers = new Request<List<Player>>("randomPlayers", Method.GET);
			randomPlayers.OnSuccess += OnSuccess;
			randomPlayers.ExecuteAsync();
			
		}
		
		private void OnSuccess(List<Player> randomPlayers){
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(OpponentSelectionView), randomPlayers);
		}
		
	}
}

