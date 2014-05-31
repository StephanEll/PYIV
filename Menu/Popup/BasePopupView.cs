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
		
		public BasePopupView () : base("PopupPrefab")
		{
			
		}
		
		public BasePopupView(string prefabName) : base(prefabName){
		}

		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			Debug.Log ("created");
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			closeButton = sprite.transform.FindChild("Close_Button").gameObject;
			textLabel = sprite.transform.FindChild("Popup_Text").GetComponent<UILabel>();

			UIEventListener.Get(closeButton).onClick += OnClose;

			
		}
		
		protected void OnClose(GameObject button){
			this.RemoveFromScreen();
		}
		
		
		//Gets called after Panel is initialized
		public override void UnpackParameter (object parameter)
		{
			PopupParam param = parameter as PopupParam;
			textLabel.text = param.Text;
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

