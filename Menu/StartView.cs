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
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();

			sprite = panel.transform.FindChild("Sprite").gameObject;
			Login_Button = sprite.transform.FindChild("Login_Button").gameObject;
			Register_Button = sprite.transform.FindChild("Register_Button").gameObject;

			UIEventListener.Get(Register_Button).onClick += OnClick;
			UIEventListener.Get(Login_Button).onClick += OnClick;
			
		}
		
		private void OnClick(GameObject button){
			if(button.name == "Register_Button") {
				this.GetViewRouter().ShowView(typeof(RegisterView));
			} else if(button.name == "Login_Button") {
				this.GetViewRouter().ShowView(typeof(LoginView));
			}
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

