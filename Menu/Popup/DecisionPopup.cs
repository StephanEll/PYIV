using System;
using UnityEngine;


namespace PYIV.Menu.Popup
{
	public class DecisionPopup : BasePopupView
	{
		
		private GameObject acceptButton;
		private GameObject declineButton;
		
		public DecisionPopup () : base("PopupYesNo")
		{
			
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			acceptButton = sprite.transform.FindChild("yes_button").gameObject;
			declineButton = sprite.transform.FindChild("no_button").gameObject;
			
		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			DecisionPopupParam param = parameter as DecisionPopupParam;
			
			UIEventListener.Get(acceptButton).onClick += param.OnAccept;
			UIEventListener.Get(acceptButton).onClick += OnClose;
			UIEventListener.Get(declineButton).onClick += param.OnDecline;
			UIEventListener.Get(declineButton).onClick += OnClose;
			
		}
	}
}

