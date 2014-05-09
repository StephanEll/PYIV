using UnityEngine;
using System.Collections;
using RestSharp;
namespace PYIV.Persistence
{

	public class Player : User
	{
		public string Mail {get; set;}
		public string Password { get; set;}
		
		public Player(){
			resource = "player";
		}
		
		protected override void ParseOnCreate (RestSharp.IRestResponse response)
		{
			base.ParseOnCreate(response);	
			AuthData authData = this.Deserializer.Deserialize<AuthData>(response);
			
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetString(AuthData.KEY_ID, authData.Id);
			PlayerPrefs.SetString(AuthData.KEY_TOKEN, authData.Token);
			
			
			
		}
		
		
	}

}