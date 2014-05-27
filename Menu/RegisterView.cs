using System;
using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;

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

			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			registerButton = sprite.transform.FindChild("Register_Button").gameObject;
			loginLink = sprite.transform.FindChild("login_link").gameObject;
			nameField = sprite.transform.FindChild("Name_Textfield").gameObject.GetComponent<UIInput>();
			emailField = sprite.transform.FindChild("Email_Textfield").gameObject.GetComponent<UIInput>();
			passwordField = sprite.transform.FindChild("Password_Textfield").gameObject.GetComponent<UIInput>();


			UIEventListener.Get(registerButton).onClick += OnRegisterButtonClick;
			UIEventListener.Get(loginLink).onClick += OnLoginLinkClick;

			
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

			// Defer to Gamelist-View
			Debug.Log("successfully registered");
		}
		private void OnErrorAtRegistration(RestException e){
			Debug.Log(e.Message);
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
		

		public override bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

