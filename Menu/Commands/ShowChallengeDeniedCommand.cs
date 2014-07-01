using System;
using PYIV.Helper.GCM;
using PYIV.Menu.Popup;
using PYIV.Menu;

namespace PYIV.Menu.Commands
{
	public class ShowChallengeDeniedCommand : QueuedCommand
	{
		private PopupParam popupParams;
		
		public ShowChallengeDeniedCommand (PushNotificationData pushNotificationData, CommandQueue commandQueue) : base(commandQueue)
		{
			
			popupParams = new PopupParam(pushNotificationData.message);
			popupParams.OnClose = (go) => HandleNextCommand();
		}
		
		public override void Execute(){
			base.Execute();
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(BasePopupView), popupParams);
		}
		
		
	}
}

