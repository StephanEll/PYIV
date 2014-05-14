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
		private GameObject button;
		private UIInput name;
		private UIInput email;
		private UIInput password;


		public RegisterView() : base("RegisterPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}

		public void AddToScreen (GameObject guiParent, GameObject sceneParent) {
			base.AddToScreen(guiParent, sceneParent);
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();

			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			button = sprite.transform.FindChild("Register_Button").gameObject;
			name = sprite.transform.FindChild("Name_Textfield").gameObject.GetComponent<UIInput>();
			email = sprite.transform.FindChild("Email_Textfield").gameObject.GetComponent<UIInput>();
			password = sprite.transform.FindChild("Password_Textfield").gameObject.GetComponent<UIInput>();


			UIEventListener.Get(button).onClick += OnClick;

			
		}
		
		private void OnClick(GameObject button){
			
			Player registeringPlayer = new Player();
			
			try{	
				registeringPlayer.Name = name.value;
				
				if(email.value != "")
					registeringPlayer.Mail = email.value;
				
				registeringPlayer.Password = password.value;
				
				registeringPlayer.Save(OnSuccessfulRegistration, OnErrorAtRegistration);
				
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
			
			
			
			
			
			
		}
		
		private void OnSuccessfulRegistration(ServerModel player){
			UnityThreadHelper.Dispatcher.Dispatch(()=>{
				this.GetViewRouter().ShowView(typeof(StartView));
				Debug.Log("successfully registered");
			});
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

