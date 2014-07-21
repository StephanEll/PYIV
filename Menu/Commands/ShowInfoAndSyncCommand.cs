using System;
using PYIV.Helper.GCM;
using PYIV.Menu.Popup;
using PYIV.Menu;

namespace PYIV.Menu.Commands
{
	public class ShowInfoAndSyncCommand : QueuedCommand
	{
		private PopupParam popupParams;
		private PushNotificationData pushNotificationData;
		
		public ShowInfoAndSyncCommand (PushNotificationData pushNotificationData, CommandQueue commandQueue) : base(commandQueue)
		{
			this.pushNotificationData = pushNotificationData;	
			popupParams = new PopupParam(pushNotificationData.message);
			popupParams.OnClose = (go) => HandleNextCommand();
		}
		
		public override void Execute(){
			base.Execute();
			
			var syncCommand = new SyncCommand(true, CommandQueue, this.pushNotificationData.timestamp);
			syncCommand.Execute();
			
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(BasePopupView), popupParams);
		}
		
		
	}
}

