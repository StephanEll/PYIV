using UnityEngine;
using System.Collections;
using System;
using RestSharp;
using PYIV.Helper;

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
			urlRoot = "http://localhost:9080/";
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
			
			
			HandleError(response);
			
			
		}
		
		private void HandleError(IRestResponse response){
			
		}
		
		
	}
}
