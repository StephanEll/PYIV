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
		
		public string Name {
			get{
				return Auth_ids[0];
			}
			
			set{
				if(Auth_ids.Count == 0){
					Auth_ids.Add(value);
				}
				else{
					Auth_ids[0] = value;
				}
			}
		}
		
		private List<String> auth_ids;
		public List<String> Auth_ids {
			get{
				return auth_ids;
			}
			set{
				auth_ids = value;
			}
		}
		
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}

		public string Mail { get; set;	}
		
		public string Password { get; set;	}
		
		public Player ()
		{
			Auth_ids = new List<string>(1);
		}
		
		
		public void Login(Request<AuthData>.SuccessDelegate OnSuccess, Request<AuthData>.ErrorDelegate OnError){
			var loginRequest = new Request<AuthData>(ComputeResourceName()+"/login", Method.POST);
			loginRequest.OnSuccess += ParseAndSaveAuthData;
			loginRequest.OnSuccess += OnSuccess;
			
			loginRequest.OnError += OnError;
			
			loginRequest.AddBody (this);
			loginRequest.ExecuteAsync();
		}

		
		private void ParseAndSaveAuthData(AuthData authData){
			
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetString (AuthData.KEY_ID, authData.Id);
			PlayerPrefs.SetString (AuthData.KEY_TOKEN, authData.Token);
			PlayerPrefs.Save ();
			this.Id = authData.Id;
		}

		public void Validate(){
			NicknameValidator.Instance.Validate(Name);	
			
			PasswordValidator.Instance.Validate(Password);

			if(Mail != null)
				MailValidator.Instance.Validate(Mail);
					
			
		}
		
		public override void ParseOnCreate (Player responseObject)
		{
			base.ParseOnCreate (responseObject);
			this.Auth_ids = responseObject.Auth_ids;
			this.Mail = responseObject.Mail;
			this.Wins = responseObject.Wins;
			this.Defeats = responseObject.Defeats;
			
		}

		public override string ToString ()
		{
			return string.Format ("[Player: Name={0}, Auth_ids={1}, Wins={2}, Defeats={3}, Mail={4}, Password={5}]", Name, Auth_ids, Wins, Defeats, Mail, Password);
		}
		
	}

}