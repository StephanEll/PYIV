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
			nameField = sprite.transform.FindChild("Name_Textfield").gameObject.GetComponent<UIInput>();
			emailField = sprite.transform.FindChild("Email_Textfield").gameObject.GetComponent<UIInput>();
			passwordField = sprite.transform.FindChild("Password_Textfield").gameObject.GetComponent<UIInput>();


			UIEventListener.Get(registerButton).onClick += OnClick;

			
		}
		
		private void OnClick(GameObject button){
			
			Player registeringPlayer = new Player();
			registeringPlayer.Name = nameField.value;	
			if(emailField.value != "")
				registeringPlayer.Mail = emailField.value;
			registeringPlayer.Password = passwordField.value;
			
			try{	
				registeringPlayer.Validate();
			}
			catch(InvalidMailException e){
				Debug.Log(e.Message);
			}
			catch(InvalidUsernameException e){
				Debug.Log(e.Message);
			}
			catch(InvalidPasswordException e){
				Debug.Log(e.Message);
			}
			
			registeringPlayer.Save(OnSuccessfulRegistration, OnErrorAtRegistration);
			

		}
		
		private void OnSuccessfulRegistration(ServerModel player){
			this.GetViewRouter().ShowView(typeof(StartView));
			Debug.Log("successfully registered");
		}
		private void OnErrorAtRegistration(ServerModel player, RestException e){
			Debug.Log(e.Message);
		}
		

		public override bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

