using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;

namespace PYIV.Menu
{
	public class LoginView : GuiView
	{

		UIInput nameField;
		UIInput passwordField;

		public LoginView () : base("LoginPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();			
		}
		
		private void OnLoginButtonClicked(GameObject button){
			Player playerToBeLoggedIn = new Player();
			
			try{
				playerToBeLoggedIn.Name = nameField.value;
				playerToBeLoggedIn.Password = passwordField.value;
				playerToBeLoggedIn.Login(OnSuccessfulLogin, OnErrorAtLogin);
			}
			catch(Exception e){
				Debug.Log(e.Message);
			}
			
		}
		
		private void OnSuccessfulLogin(Player player){
			LoggedInPlayer.Login(player);
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
		

		
		private void OnErrorAtLogin(RestException e){
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(BasePopupView), PopupParam.FromText(e.Message));
		}
		
		private void OnRegisterButtonClicked(GameObject button){
			ViewRouter.TheViewRouter.ShowView(typeof(RegisterView));
		}
		
		private void OnForgotPasswordClicked(GameObject button){
			PopupParam param = new PopupParam(StringConstants.FORGOT_PASSWORD);
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(BasePopupView), param);
			
		}

		private void InitViewComponents() {

			GameObject sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject bottomAnchorLinks = sprite.transform.FindChild("BottomAnchorLinks").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;

			GameObject loginButton = topAnchorInteraction.transform.FindChild("Login_Button").gameObject;

			GameObject registerLink = bottomAnchorLinks.transform.FindChild("register_link").gameObject;
			GameObject forgotPwLink = bottomAnchorLinks.transform.FindChild("lost_link").gameObject;
			
			nameField = topAnchorInteraction.transform.FindChild("name_input").gameObject.GetComponent<UIInput>();
			passwordField = topAnchorInteraction.transform.FindChild("password_input").gameObject.GetComponent<UIInput>();
			
			UIEventListener.Get(loginButton).onClick += OnLoginButtonClicked;
			UIEventListener.Get(registerLink).onClick += OnRegisterButtonClicked;
			UIEventListener.Get(forgotPwLink).onClick += OnForgotPasswordClicked;
		}
		
		public override bool ShouldBeCached ()
		{
			//immer neu erstellen, um keine Vorbelegung zu haben
			return false;
		}
		
		public override void Back ()
		{

		}
	}
}

