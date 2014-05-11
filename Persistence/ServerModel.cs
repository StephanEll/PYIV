using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence
{
	public abstract class ServerModel
	{
		
		
		public string Id { get; set; }

		protected string resource;
		
		
		public delegate void OnSuccess (ServerModel model);

		public delegate void OnError (ServerModel model,RestException error);
		
		private static string urlRoot;

		protected static string UrlRoot {
			get {
				if (urlRoot == null) {
					urlRoot = ConfigReader.Instance.GetSetting ("server", "url");
				}
				return urlRoot;
			}
		}
		
		private RestSharp.Deserializers.JsonDeserializer deserializer;

		protected RestSharp.Deserializers.JsonDeserializer Deserializer {
			get {
				if (deserializer == null) {
					deserializer = new RestSharp.Deserializers.JsonDeserializer ();
				}
				return deserializer;
			}
			
		}
		
		public ServerModel ()
		{
			
		}
		
		/**
		 * Syncs the model with the Server. 
		 * Will create a new Model on the server if it has no Id,
		 * otherwise it will update the existing one
		 * */
		public void Save (OnSuccess successCallback, OnError errorCallback)
		{
			IRestClient restClient = new RestClient (UrlRoot);
			
			if (this.Id == null) {
				Create (restClient, successCallback, errorCallback);
			}
			
		}
		
		protected virtual void ParseOnCreate (IRestResponse response, OnSuccess successCallback)
		{
			var dict = SimpleJson.DeserializeObject<Dictionary<string, string>> (response.Content);
			this.Id = dict ["id"];
			
			if (successCallback != null) {
				successCallback (this);	
			}
		}
		
		private void Create (IRestClient restClient, OnSuccess successCallback, OnError errorCallback)
		{
			IRestRequest request = new RestRequest (resource, Method.POST);
			request.RequestFormat = DataFormat.Json;
			
			
			request.AddBody (this);
			restClient.ExecuteAsync (request, (response) => {
				OnAsyncRequestComplete (response, successCallback, errorCallback);
			});
			
			
		}
		
		private void OnAsyncRequestComplete (IRestResponse response, OnSuccess successCallback, OnError errorCallback)
		{
			UnityThreadHelper.Dispatcher.Dispatch (() => {
				bool hasNoErrors = HandlePossibleErrors (response, errorCallback);
				if (hasNoErrors)
					ParseOnCreate (response, successCallback);
			});
		}
		
		
		
		protected bool HandlePossibleErrors (IRestResponse response, OnError errorCallback)
		{
			RestException exception = CreateException(response);

			if (exception == null) {
				return true;
			} else if (errorCallback != null) {	
				errorCallback (this, exception);	
			}
			return false;	

		}
		
		private RestException CreateException(IRestResponse response){
			if (response.StatusCode == 0) {
				return RestExceptionFactory.CreateNoConnectionException ();
			} else if (response.StatusCode == (System.Net.HttpStatusCode.BadRequest)) {
				var deserializer = new RestSharp.Deserializers.JsonDeserializer ();
				ServerError error = deserializer.Deserialize<ServerError> (response);
				return RestExceptionFactory.CreateExceptionFromError (error);
			}
			return null;
		}
		
	}
}
