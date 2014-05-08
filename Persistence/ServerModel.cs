using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence{

	public abstract class ServerModel {
		
		
		public string Id { get; set; }
		protected string resource;
		private static string urlRoot;
		private static string UrlRoot 
		{
			get
			{
				if(urlRoot == null){
					urlRoot = ConfigReader.Instance.GetSetting("server", "url");
					Debug.Log (urlRoot);
				}
				return urlRoot;
			}
		}
		
		
		public ServerModel(){
		}
		
		protected string Url(){
			return "/"+resource;
			
		}
		
		public void Save(){
			IRestClient restClient = new RestClient(UrlRoot);
			
			if(this.Id == null){
				Create(restClient);
			}
			
		}
		
		
		
		private void Create(IRestClient restClient){
			IRestRequest request = new RestRequest(resource, Method.POST);
			request.RequestFormat = DataFormat.Json;
			
			
			request.AddBody(this);
			IRestResponse response = restClient.Execute(request);
			
			HandlePossibleErrors(response);
			ParseOnCreate (response);
			
			
		}
		
		protected void ParseOnCreate(IRestResponse response){
			var dict = SimpleJson.DeserializeObject<Dictionary<string, string>>(response.Content);
			this.Id = dict["id"];
		}
		
		private void HandlePossibleErrors(IRestResponse response){
			if(response.StatusCode == 0){
				throw RestExceptionFactory.CreateNoConnectionException();
			}
			else if(response.StatusCode == (System.Net.HttpStatusCode.BadRequest)){
				var deserializer = new RestSharp.Deserializers.JsonDeserializer();
				ServerError error = deserializer.Deserialize<ServerError>(response);
				
				throw RestExceptionFactory.CreateExceptionFromError(error);
				
			}
		}
		
		
	}
}
