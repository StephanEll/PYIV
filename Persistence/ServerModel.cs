using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;
using System.Runtime.Serialization;

namespace PYIV.Persistence
{
	/// <summary>
	/// Abstract baseclass to be inherited from by models which
	/// communicate with server
	/// </summary>
	/// 
	[DataContract]
	public abstract class ServerModel{
		
		[DataMember]
		public string Id { get; set; }

		protected string resource;
		
		private static string urlRoot;

		private RestSharp.Deserializers.JsonDeserializer deserializer;
		
		public ServerModel (){	}
		
		public static string UrlRoot {
			get {
				if (urlRoot == null) {
					urlRoot = ConfigReader.Instance.GetSetting ("server", "url");
				}
				return urlRoot;
			}
		}
		
		protected RestSharp.Deserializers.JsonDeserializer Deserializer {
			get {
				if (deserializer == null) {
					deserializer = new RestSharp.Deserializers.JsonDeserializer ();
				}
				return deserializer;
			}
			
		}
	}
	
	
	/// <summary>
	/// Generic version of ServerModel
	/// </summary>
	public abstract class ServerModel<T> : ServerModel where T : ServerModel<T>, new()
	{
		
		

		/**
		 * Syncs the model with the Server. 
		 * Will create a new Model on the server if it has no Id,
		 * otherwise it will update the existing one
		 * */
		public void Save (Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError)
		{
			
			
			if (this.Id == null) {
				Create (OnSuccess, OnError);
			}
			
		}
		
		public void Fetch(Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError){
			if (this.Id != null) {
				Load (OnSuccess, OnError);
			}
			else{
				throw new ModelNotInitializedException();
			}
		}
		
		
		
		private void Create (Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError)
		{
			var postRequest = new Request<T>(resource,Method.POST);
			postRequest.OnError += OnError;
			postRequest.OnSuccess += OnSuccess;
			postRequest.OnSuccess += ParseOnCreate;
			
			postRequest.AddBody(this);
			postRequest.ExecuteAsync();
		}
		
		protected void ParseOnCreate (T responseObject)
		{		
			this.Id = responseObject.Id;
		}
		
		private void Load (Request<T>.SuccessDelegate OnSuccess, Request<T>.ErrorDelegate OnError)
		{
			var getRequest = new Request<T>(resource+"/{id}",Method.GET);
			getRequest.OnError += OnError;
			getRequest.OnSuccess += OnSuccess;
			getRequest.OnSuccess += PopulateModel;
			
			getRequest.AddId(this.Id);
			getRequest.ExecuteAsync();
		}
		
		protected abstract void PopulateModel (T responseObject);
		
		
		
		
	}
}
