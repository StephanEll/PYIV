using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;
using UnityEngine;

namespace PYIV.Persistence
{
	/// <summary>
	/// Performes a async REST-Request an calls success- or failure-callbacks 
	/// with a response-object or a exception 
	/// </summary>
	public class Request<T>
	{
		public delegate void SuccessDelegate (T responseObject);

		public delegate void ErrorDelegate(RestException error);
		
		public event ErrorDelegate OnError;
		
		public event SuccessDelegate OnSuccess;
		
		private IRestClient restClient;
		private IRestRequest request;
		
		public Request (string resource, RestSharp.Method method)
		{
			restClient = new RestClient (ServerModel.UrlRoot);
			
			
			AddAuthentication();
			
			request = new RestRequest (resource, method);
			
			request.AddHeader("content-type", "application/json; charset=utf-8");
			request.RequestFormat = DataFormat.Json;
			

		}
			
		private void AddAuthentication(){
			
			if(LoggedInPlayer.IsLoggedIn()){
				string auth_id = LoggedInPlayer.Instance.Id;
				string auth_token = LoggedInPlayer.Instance.AuthToken;
				
				restClient.Authenticator = new SimpleAuthenticator("user_id", auth_id, "token", auth_token);
			}
		}
		
		//Add payload, the object will be json-serialized
		public void AddBody(object model){
			
			request.AddParameter("model", SimpleJson.SimpleJson.SerializeObject(model));
		
		}
		
		public void AddId(string id){
			request.AddParameter("id", id, ParameterType.UrlSegment);
			
		}
		
		public void ExecuteAsync(){
			restClient.ExecuteAsync (request, (response) => {
				OnAsyncRequestComplete (response);
			});
		}
		
		private void OnAsyncRequestComplete (IRestResponse response)
		{
			
			UnityThreadHelper.Dispatcher.Dispatch (() => {
				Debug.Log ("CONTENT: "+response.Content);
				RestException exception = new ResponseErrorHandler(response).HandlePossibleErrors();
				if (exception == null)
					ParseResponse (response);
				else if(OnError != null)
					OnError(exception);
			});
		}
		private void ParseResponse(IRestResponse response){
			var deserializer = new RestSharp.Deserializers.JsonDeserializer ();
			T responseObject = deserializer.Deserialize<T> (response);
			if(OnSuccess != null)
				OnSuccess(responseObject);
		}
		
	}
}

