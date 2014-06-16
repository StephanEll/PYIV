using System;
using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;
using PYIV.Menu.Popup;

namespace PYIV.Menu
{
	public class RegisterView : GuiView{

		private GameObject sprite;
		private GameObject registerButton;
		private GameObject loginLink;
		private UIInput nameField;
		private UIInput emailField;
		private UIInput passwordField;


		public RegisterView() : base("RegisterPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}


		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();
						
		}
		
		private void OnRegisterButtonClick(GameObject button){

			SetBackHighlighting();
			
			Player registeringPlayer = new Player();
			registeringPlayer.Name = nameField.value;	
			if(emailField.value != "") {
				registeringPlayer.Mail = emailField.value;
			} 
			registeringPlayer.Password = passwordField.value;
			
			try{	
				registeringPlayer.Validate();
			}
			catch(InvalidMailException e){
				highlightTextfield(emailField);
				Debug.Log(e.Message);
			}
			catch(InvalidUsernameException e){
				highlightTextfield(nameField);
				Debug.Log(e.Message);
			}
			catch(InvalidPasswordException e){
				highlightTextfield(passwordField);
				Debug.Log(e.Message);
			}
			
			registeringPlayer.Save(OnSuccessfulRegistration, OnErrorAtRegistration);
			

		}
		
		private void OnSuccessfulRegistration(Player serverResponseObject){
			LoggedInPlayer.Login(serverResponseObject);
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
		private void OnErrorAtRegistration(RestException e){
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(BasePopupView), PopupParam.FromText(e.Message));
			
		}


		private void OnLoginLinkClick(GameObject button){
			ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
		}


		private void highlightTextfield(UIInput textfield) {
			UISprite emailSprite = textfield.GetComponent<UISprite>();
			emailSprite.spriteName = "textfield_error";
		}

		private void SetBackHighlighting() {
			UISprite sprite = nameField.GetComponent<UISprite>();
			sprite.spriteName = "textfield";

			sprite = emailField.GetComponent<UISprite>();
			sprite.spriteName = "textfield";

			sprite = passwordField.GetComponent<UISprite>();
			sprite.spriteName = "textfield";
		}


		private void InitViewComponents() {

			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject bottomAnchorButton = sprite.transform.FindChild("BottomAnchorButton").gameObject;
			GameObject bottomAnchorLinks = sprite.transform.FindChild("BottomAnchorLinks").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;

			registerButton = bottomAnchorButton.transform.FindChild("Register_Button").gameObject;
			loginLink = bottomAnchorLinks.transform.FindChild("login_link").gameObject;
			nameField = topAnchorInteraction.transform.FindChild("name_input").gameObject.GetComponent<UIInput>();
			emailField = topAnchorInteraction.transform.FindChild("email_input").gameObject.GetComponent<UIInput>();
			passwordField = topAnchorInteraction.transform.FindChild("password_input").gameObject.GetComponent<UIInput>();
			
			
			UIEventListener.Get(registerButton).onClick += OnRegisterButtonClick;
			UIEventListener.Get(loginLink).onClick += OnLoginLinkClick;
		}

		public override bool ShouldBeCached ()
		{
			//immer neu erstellen, um keine Vorbelegung zu haben
			return false;
		}
		
	}
}

