using System;
using RestSharp;
using PYIV.Helper;
using PYIV.Persistence.Errors;
using UnityEngine;
using System.Runtime.Serialization;
using PYIV.Menu;
using PYIV.Menu.Popup;

namespace PYIV.Persistence
{
	
	
	public class Request{

		public delegate void RequestCompletedDelegate();
		public event RequestCompletedDelegate OnRequestCompleted;
		
		protected bool doInBackground;
		
		protected IRestClient restClient;
		protected IRestRequest request;
		
		public delegate void ErrorDelegate(RestException error);
		public event ErrorDelegate OnError;
		
		public Request(string resource, RestSharp.Method method, bool doInBackground = false){
			restClient = new RestClient (ServerModel.UrlRoot);
			this.doInBackground = doInBackground;
			
			AddAuthentication();
			
			request = new RestRequest (resource, method);
			
			request.AddHeader("content-type", "application/json; charset=utf-8");
			request.RequestFormat = DataFormat.Json;
		}
		
		private void AddAuthentication(){
			
			if(LoggedInPlayer.IsLoggedIn()){
				string auth_id = LoggedInPlayer.Instance.Player.Id;
				string auth_token = LoggedInPlayer.Instance.Player.AuthToken;
				
				restClient.Authenticator = new SimpleAuthenticator("user_id", auth_id, "token", auth_token);
			}
		}
		
		//Add payload, the object will be json-serialized
		public void AddBody(object model){
			request.AddParameter("model", SimpleJson.SimpleJson.SerializeObject(model));
		
		}
		
		public void AddParameter(string param, string paramValue){
			request.AddParameter(param, paramValue, ParameterType.UrlSegment);
			
		}

		public void ExecuteAsync(){
			if(!doInBackground){
				ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(LoadingView), new LoadingPopupParam(this));
			}
			restClient.ExecuteAsync (request, (response) => {				
				OnAsyncRequestComplete (response);
			});
		}
		
		protected void OnAsyncRequestComplete (IRestResponse response)
		{
			
			UnityThreadHelper.Dispatcher.Dispatch (() => {
				
				Debug.Log ("RAW SERVER RESPONSE CONTENT: "+response.Content);
				
				if(OnRequestCompleted != null)
					OnRequestCompleted();
				
				RestException exception = new ResponseErrorHandler(response).HandlePossibleErrors();
				if (exception == null)
					ParseResponse (response);
				else if(OnError != null)
					OnError(exception);
				
			});
		}
		
		protected virtual void ParseResponse(IRestResponse response){ }
		
	}
	
	/// <summary>
	/// Performes a async REST-Request an calls success- or failure-callbacks 
	/// with a response-object or a exception 
	/// </summary>
	public class Request<T> : Request
	{
		public delegate void SuccessDelegate (T responseObject);

		
		
		public event SuccessDelegate OnSuccess;
		
		
		public Request(string resource, RestSharp.Method method, bool doInBackground = false) : base ( resource, method, doInBackground){
		}
		
		protected override void ParseResponse(IRestResponse response){
			var deserializer = new RestSharp.Deserializers.JsonDeserializer ();
			T responseObject;
			try{
				responseObject = deserializer.Deserialize<T> (response);
			}
			catch(SerializationException e){
				responseObject = default(T);
			}
			
			if(OnSuccess != null)
				OnSuccess(responseObject);
			
			
				
				
		}
		
	}
}

