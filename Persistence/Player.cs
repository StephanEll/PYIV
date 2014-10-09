using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RestSharp;
using System;
using System.Runtime.Serialization;
using PYIV.Persistence.Errors;
using PYIV.Persistence.Validators;
using PYIV.Menu.Commands;
using PYIV.Helper.GCM;

namespace PYIV.Persistence
{
	/// <summary>
	/// Represents a player infront of the phone
	/// </summary>
	[DataContract]
	[Serializable]
	public class Player : ServerModel<Player>
	{
		[DataMember]
		public string Name {
			get{
				
				return Auth_ids.Count == 0 ? "" : Auth_ids[0];
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
		[DataMember]
		public List<String> Auth_ids {
			get{
				return auth_ids;
			}
			set{
				auth_ids = value;
			}
		}
		
		[IgnoreDataMember]
		public List<string> Wins {get; set;}
		[IgnoreDataMember]
		public List<string> Defeats {get; set;}

		[DataMember]
		public String AuthToken { get; set;	}
		[DataMember]
		public string Mail { get; set;	}
		[DataMember]
		public string Password { get; set;	}
		
		[IgnoreDataMember]
		public int Score { get; set; }
		
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
		
		public void Logout(Request<Player>.SuccessDelegate OnSuccess, Request<Player>.ErrorDelegate OnError){
			var logoutRequest = new Request<Player>(ComputeResourceName(typeof(Player))+"/login", Method.DELETE);
			logoutRequest.OnSuccess += OnSuccess;
			logoutRequest.OnError += OnError;
			
			GcmData gcmData = GcmToServerCommand.CreateGcmData(PlayerPrefs.GetString(NotificationHandler.GCM_ID_KEY));
			
			logoutRequest.AddBody(gcmData);
			logoutRequest.ExecuteAsync();
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
			bool hasPersistedAuthData = PlayerPrefs.HasKey(AuthData.KEY_TOKEN) && PlayerPrefs.HasKey(AuthData.KEY_ID);
			
			Debug.Log("HAS AUTH DATA?: " + hasPersistedAuthData);
			
			if(hasPersistedAuthData){
				this.AuthToken = PlayerPrefs.GetString(AuthData.KEY_TOKEN);
				Debug.Log (this.AuthToken);
				this.Id = PlayerPrefs.GetString(AuthData.KEY_ID);
				Debug.Log(this.Id);
			}
			else{
				throw new Exception("No auth data available. Player needs to login");
			}
		}
		
		public void GetByAuthData(Request<Player>.SuccessDelegate OnSuccess, Request<Player>.ErrorDelegate OnError){
			this.LoadAuthData();
			var authRequest = new Request<Player>(ComputeResourceName(typeof(Player))+"/login", Method.GET);
			authRequest.OnSuccess += ParseOnCreate;
			authRequest.OnSuccess += OnSuccess;
			authRequest.OnError += delegate {
				DeleteAuthData();
			};
			authRequest.OnError += OnError;
			authRequest.AddBody (this);
			authRequest.ExecuteAsync();
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
			this.Score = responseObject.Score;
			
			if(responseObject.AuthToken != null)
				this.AuthToken = responseObject.AuthToken;	

		}
		
		public override string ToString ()
		{
			return string.Format ("[Player: Name={0}, Auth_ids={1}, Wins={2}, Defeats={3}, Mail={4}, Password={5}]", Name, Auth_ids, Wins, Defeats, Mail, Password);
		}
		
	}

}