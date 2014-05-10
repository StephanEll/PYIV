using System;
using UnityEngine;
using System.Collections;

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
			Debug.Log("Name:" + name.value);
			Debug.Log("Email:" + email.value);
			Debug.Log("Password:" + password.value);
		}
		
		
		

		public override bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

