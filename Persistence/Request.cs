using System;
using RestSharp;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence
{
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
			request = new RestRequest (resource, Method.POST);
			request.RequestFormat = DataFormat.Json;

		}
		
		public void AddBody(object model){
			request.AddBody (model);
		}
		
		public void ExecuteAsync(){
			restClient.ExecuteAsync (request, (response) => {
				OnAsyncRequestComplete (response);
			});
		}
		
		private void OnAsyncRequestComplete (IRestResponse response)
		{
			
			UnityThreadHelper.Dispatcher.Dispatch (() => {
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

