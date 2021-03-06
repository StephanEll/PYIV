using System;
using RestSharp;

namespace PYIV.Persistence.Errors
{
	public class ResponseErrorHandler
	{
		private IRestResponse response;
		
		public ResponseErrorHandler (IRestResponse response)
		{
			this.response = response;
		}
		
		
		public RestException HandlePossibleErrors ()
		{
			RestException exception = CreateException();

			return exception;	

		}
		
		private RestException CreateException(){
			if (response.ResponseStatus != ResponseStatus.Completed) {
				
				return RestExceptionFactory.CreateNoConnectionException ();
				
			} else if (response.StatusCode == (System.Net.HttpStatusCode.BadRequest)) {
				
				var deserializer = new RestSharp.Deserializers.JsonDeserializer ();
				ServerError error = deserializer.Deserialize<ServerError> (response);
				return RestExceptionFactory.CreateExceptionFromError (error);
			
			}
			else if(response.StatusCode == System.Net.HttpStatusCode.OK){
				return null;
			}
			else{
				return RestExceptionFactory.CreateUnknownError();
			}
		}
		
		
	}
}

