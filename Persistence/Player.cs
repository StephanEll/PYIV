using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RestSharp;
using System;

using PYIV.Persistence.Errors;
using PYIV.Persistence.Validators;

namespace PYIV.Persistence
{
	/// <summary>
	/// Represents the player sitting infront of the phone
	/// </summary>
	public class Player : ServerModel<Player>
	{
		
		public string Name {get; set;}
		
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}

		public string Mail { get; set;	}
		
		public string Password { get; set;	}
		
		public Player ()
		{
			resource = "player";
		}
		
		
		public void Login(Request<AuthData>.SuccessDelegate OnSuccess, Request<AuthData>.ErrorDelegate OnError){
			var loginRequest = new Request<AuthData>(resource+"/login", Method.POST);
			loginRequest.OnSuccess += OnSuccess;
			loginRequest.OnSuccess += ParseAndSaveAuthData;
			loginRequest.OnError += OnError;
			
			loginRequest.AddBody (this);
			loginRequest.ExecuteAsync();
		}

		
		private void ParseAndSaveAuthData(AuthData authData){
			
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetString (AuthData.KEY_ID, authData.Id);
			PlayerPrefs.SetString (AuthData.KEY_TOKEN, authData.Token);
			PlayerPrefs.Save ();
		}

		public void Validate(){
			NicknameValidator.Instance.Validate(Name);	
			
			PasswordValidator.Instance.Validate(Password);

			if(Mail != null)
				MailValidator.Instance.Validate(Mail);
					
			
		}
		
		protected override void UpdateModel (Player responseObject)
		{
			
		}
		
	}

}