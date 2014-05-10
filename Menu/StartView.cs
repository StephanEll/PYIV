using System;
using UnityEngine;
namespace PYIV.Menu
{
	public class StartView : GuiView
	{
		private GameObject sprite;
		private GameObject Login_Button;
		private GameObject Register_Button;
		
		public StartView () : base("StartPrefab")
		{
			
		}
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent) {
			base.AddToScreen(guiParent, sceneParent);
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();

			sprite = panel.transform.FindChild("Sprite").gameObject;
			Login_Button = sprite.transform.FindChild("Login_Button").gameObject;
			Register_Button = sprite.transform.FindChild("Register_Button").gameObject;

			UIEventListener.Get(Register_Button).onClick += OnClick;
			
		}
		
		private void OnClick(GameObject button){
			this.GetViewRouter().ShowView(typeof(RegisterView));
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

