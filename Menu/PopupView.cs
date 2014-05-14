using System;
using UnityEngine;
namespace PYIV.Menu
{
	public class PopupView : GuiView
	{
		private GameObject sprite;
		private GameObject closeButton;
		private UILabel popupLabel;
		
		public PopupView () : base("PopupPrefab")
		{
			
		}

		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			closeButton = sprite.transform.FindChild("Close_Button").gameObject;
			popupLabel = sprite.transform.FindChild("Popup_Text").GetComponent<UILabel>();

			popupLabel.text = "Das indianische Wort fuer Windows? 'Weisser-Mann-starrt-wartend-auf-Sanduhr'";
			
			UIEventListener.Get(closeButton).onClick += OnClick;

			
		}
		
		private void OnClick(GameObject button){
			if(button.name == "Close_Button") {
				this.GetViewRouter().DestroyPopup(this);
			} 
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

