using System;
using UnityEngine;
namespace PYIV.Menu
{
	public class LoginView : GuiView
	{
		private GameObject sprite;
		private GameObject Login_Button;
		private GameObject Register_Link;
		private GameObject ForgotPW_Link;
		
		public LoginView () : base("LoginPrefab")
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
			Register_Link = sprite.transform.FindChild("register_link").gameObject;
			ForgotPW_Link = sprite.transform.FindChild("lost_link").gameObject;
			
			UIEventListener.Get(Login_Button).onClick += OnClick;
			UIEventListener.Get(Register_Link).onClick += OnClick;
			UIEventListener.Get(ForgotPW_Link).onClick += OnClick;
			
		}
		
		private void OnClick(GameObject button){
			if(button.name == "Login_Button") {
				this.GetViewRouter().ShowView(typeof(StartView));
			} else if(button.name == "register_link") {
				this.GetViewRouter().ShowView(typeof(RegisterView));
			} else if(button.name == "lost_link") {
				this.GetViewRouter().ShowPopup(typeof(PopupView));
			}
		}
		
		public override bool ShouldBeCached ()
		{
			return true;
		}
	}
}

