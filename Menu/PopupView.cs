using System;
using UnityEngine;
namespace PYIV.Menu
{
	public class PopupView : GuiView
	{
		private GameObject sprite;
		private GameObject Close_Button;
		private UILabel Popup_Text;
		
		public PopupView () : base("PopupPrefab")
		{
			
		}
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent) {
			base.AddToScreen(guiParent, sceneParent);
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			Close_Button = sprite.transform.FindChild("Close_Button").gameObject;
			Popup_Text = sprite.transform.FindChild("Popup_Text").GetComponent<UILabel>();

			Popup_Text.text = "Das indianische Wort fuer Windows? 'Weisser-Mann-starrt-wartend-auf-Sanduhr'";
			
			UIEventListener.Get(Close_Button).onClick += OnClick;

			
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

