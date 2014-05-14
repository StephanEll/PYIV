using UnityEngine;
using System.Collections;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence
{
	public class Player : User
	{

		public string Mail { get; set;	}
		
		public string Password { get; set;	}
		
		public Player ()
		{
			resource = "player";
		}
		
		protected override void ParseOnCreate (RestSharp.IRestResponse response, OnSuccess successCallback)
		{
			base.ParseOnCreate (response, successCallback);	
			this.ParseAndSaveAuthData(response);
			
		}
		
		public void Login(OnSuccess successCallback, OnError errorCallback){
			IRestClient restClient = new RestClient (UrlRoot);
			IRestRequest request = new RestRequest (resource+"/login", Method.POST);
			request.RequestFormat = DataFormat.Json;
			
			
			request.AddBody (this);
			restClient.ExecuteAsync (request, (response) => {
				OnLoginRequestComplete (response, successCallback, errorCallback);
			});
		}
		
		private void OnLoginRequestComplete(IRestResponse response, OnSuccess successCallback, OnError errorCallback){
			UnityThreadHelper.Dispatcher.Dispatch (() => {
				bool hasNoErrors = HandlePossibleErrors (response, errorCallback);
				if (hasNoErrors){
					ParseAndSaveAuthData (response);
					successCallback(this);	
				}
			});
		}
		
		private void ParseAndSaveAuthData(IRestResponse response){
			AuthData authData = this.Deserializer.Deserialize<AuthData> (response);
			
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetString (AuthData.KEY_ID, authData.Id);
			PlayerPrefs.SetString (AuthData.KEY_TOKEN, authData.Token);
			PlayerPrefs.Save ();
		}
		
		
		
		
		public static bool IsValidEmailAddress (string mailAddress)
		{
			Regex mailIDPattern = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}", RegexOptions.IgnoreCase);
			return (!string.IsNullOrEmpty (mailAddress) && mailIDPattern.IsMatch (mailAddress)) ? true : false;
		}
		
		public static bool IsValidPassword(string rawPassword)
		{
			return rawPassword.Length >= 6;
		}
		
		public override void Validate(){
			base.Validate();
			
			bool passwordIsSetButNotValid = Password != null && !Player.IsValidPassword(Password);
			if(passwordIsSetButNotValid)
				throw new InvalidPasswordException("Your password must be at least 6 digits long");
			
			bool mailIsSetButNotValid = Mail != null && !Player.IsValidEmailAddress(Mail);
			if(mailIsSetButNotValid)
				throw new InvalidMailException("Your Mail isn't a valid address.");
					
			
		}
		
		
	}

}