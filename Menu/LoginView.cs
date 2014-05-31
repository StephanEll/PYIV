using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;

namespace PYIV.Menu
{
	public class LoginView : GuiView
	{
		private GameObject sprite;
		private GameObject loginButton;
		private GameObject registerLink;
		private GameObject forgotPwLink;
		private UIInput nameField;
		private UIInput passwordField;
		private Player playerToBeLoggedIn;
		
		public LoginView () : base("LoginPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			loginButton = sprite.transform.FindChild("Login_Button").gameObject;
			registerLink = sprite.transform.FindChild("register_link").gameObject;
			forgotPwLink = sprite.transform.FindChild("lost_link").gameObject;
			
			nameField = sprite.transform.FindChild("Name_Textfield").gameObject.GetComponent<UIInput>();
			passwordField = sprite.transform.FindChild("Password_Textfield").gameObject.GetComponent<UIInput>();
			
			UIEventListener.Get(loginButton).onClick += OnLoginButtonClicked;
			UIEventListener.Get(registerLink).onClick += OnRegisterButtonClicked;
			UIEventListener.Get(forgotPwLink).onClick += OnForgotPasswordClicked;
			
		}
		
		private void OnLoginButtonClicked(GameObject button){
			playerToBeLoggedIn = new Player();
			
			try{
				playerToBeLoggedIn.Name = nameField.value;
				playerToBeLoggedIn.Password = passwordField.value;
				playerToBeLoggedIn.Login(OnSuccessfulLogin, OnErrorAtLogin);
			}
			catch(Exception e){
				Debug.Log(e.Message);
			}
			
		}
		
		private void OnSuccessfulLogin(AuthData authData){

		}
		

	
		private void OnSucess(GameData data){
			Debug.Log(data.ToString());
		}
		private void OnError(RestException e){
			Debug.Log(e.Message);
		}
		
		private void OnErrorAtLogin(RestException e){
			Debug.Log(e.Message);
		}
		
		private void OnRegisterButtonClicked(GameObject button){
			ViewRouter.TheViewRouter.ShowView(typeof(RegisterView));
		}
		
		private void OnForgotPasswordClicked(GameObject button){
			PopupParam param = new DecisionPopupParam("Passwort vergessen", OnAccept, null);
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(DecisionPopup), param);
			
		}
		void OnAccept(GameObject o){
			Debug.Log("accept");
		}
		
		
		
		public override bool ShouldBeCached ()
		{
			return true;
		}
	}
}

