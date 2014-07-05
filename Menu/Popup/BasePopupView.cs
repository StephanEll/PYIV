using System;
using UnityEngine;
using PYIV.Menu.Popup;


namespace PYIV.Menu.Popup
{
	public class BasePopupView : GuiView
	{
		protected GameObject sprite;
		protected GameObject closeButton;
		protected UILabel textLabel;
		protected UIEventListener.VoidDelegate onCloseCallback;
		
		public BasePopupView () : base("PopupPrefab")
		{
			
		}
		
		public BasePopupView(string prefabName) : base(prefabName){
		}

		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			closeButton = sprite.transform.FindChild("Close_Button").gameObject;
			textLabel = sprite.transform.FindChild("Popup_Text").GetComponent<UILabel>();

			UIEventListener.Get(closeButton).onClick += OnClose;

			
		}
		
		protected void OnClose(GameObject button){
			this.RemoveFromScreen();
			if(onCloseCallback != null)
				onCloseCallback(button);
		}
		
		
		//Gets called after Panel is initialized
		public override void UnpackParameter (object parameter)
		{
			PopupParam param = parameter as PopupParam;
			textLabel.text = param.Text;
			onCloseCallback = param.OnClose;
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
		
		public override void Back ()
		{
			OnClose(null);
		}
		
	}
}

