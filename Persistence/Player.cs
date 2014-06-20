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
	/// Represents a player infront of the phone
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
		
		public String AuthToken { get; set;	}

		public string Mail { get; set;	}
		
		public string Password { get; set;	}
		
		public Player ()
		{
			Auth_ids = new List<string>(1);
		}
		
		
		public void Login(Request<Player>.SuccessDelegate OnSuccess, Request<Player>.ErrorDelegate OnError){
			var loginRequest = new Request<Player>(ComputeResourceName(typeof(Player))+"/login", Method.POST);
			loginRequest.OnSuccess += ParseOnCreate;
			loginRequest.OnSuccess += OnSuccess;
			
			loginRequest.OnError += OnError;
			
			loginRequest.AddBody (this);
			loginRequest.ExecuteAsync();
		}
		
		
		
		public void PersistAuthData(){
			if(this.Id != null && this.AuthToken != null){
				DeleteAuthData();
				PlayerPrefs.SetString (AuthData.KEY_ID, this.Id);
				PlayerPrefs.SetString (AuthData.KEY_TOKEN, this.AuthToken);
				PlayerPrefs.Save ();
			}
		}
		
		public void DeleteAuthData(){
			PlayerPrefs.DeleteKey(AuthData.KEY_ID);
			PlayerPrefs.DeleteKey(AuthData.KEY_TOKEN);
			PlayerPrefs.Save ();
		}
		
		public void LoadAuthData(){
			this.AuthToken = PlayerPrefs.GetString(AuthData.KEY_TOKEN);
			this.Id = PlayerPrefs.GetString(AuthData.KEY_ID);
		}

		public void Validate(){
			NicknameValidator.Instance.Validate(Name);	
			
			PasswordValidator.Instance.Validate(Password);

			if(Mail != null)
				MailValidator.Instance.Validate(Mail);
					
			
		}
		
		public static void FetchByName(string name, Request<Player>.SuccessDelegate OnSuccess, Request<Player>.ErrorDelegate OnError){
			var getRequest = new Request<Player>("player/search/{name}", Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			
			getRequest.AddParameter("name", name);
			getRequest.ExecuteAsync();
		}
		
		public override void ParseOnCreate (Player responseObject)
		{
			base.ParseOnCreate (responseObject);
			this.Auth_ids = responseObject.Auth_ids;
			this.Mail = responseObject.Mail;
			this.Wins = responseObject.Wins;
			this.Defeats = responseObject.Defeats;
			
			if(responseObject.AuthToken != null)
				this.AuthToken = responseObject.AuthToken;	

		}
		
		public override bool Equals (object obj)
		{
			if(obj is Player){
				Player player = obj as Player;
				if(player.Name.ToLower() == this.Name.ToLower()){
					return true;
				}
			}
			return false;
		}
		
		public override string ToString ()
		{
			return string.Format ("[Player: Name={0}, Auth_ids={1}, Wins={2}, Defeats={3}, Mail={4}, Password={5}]", Name, Auth_ids, Wins, Defeats, Mail, Password);
		}
		
	}

}