using System;
using PYIV.Menu.Popup;
using PYIV.Persistence;
using PYIV.Persistence.Errors;

namespace PYIV.Menu.Commands
{
	public class FinishConfigurationCommand : ICommand
	{
		
		private AttackConfigurationModel attackConfigurationModel;
		
		public FinishConfigurationCommand (AttackConfigurationModel attackConfigurationModel)
		{
			this.attackConfigurationModel = attackConfigurationModel;
		}
		
		public void Execute(){
			attackConfigurationModel.ApplyToPlayerStatus();
			attackConfigurationModel.GameData.Save(OnSuccess, OnError);
		}
		
		private void OnSuccess(GameData responseData){
			
			
			LoggedInPlayer.Instance.GameList.AddOrUpdateGame(attackConfigurationModel.GameData);
			
			if(attackConfigurationModel.GameData.State == GameState.READY_TO_PLAY){
				ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameView), attackConfigurationModel.GameData);
			}
			else{
				ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
			}
		}
		
		private void OnError(RestException e){
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
		
	}
}

