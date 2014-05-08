using System;
using PYIV.Persistence.Errors;
namespace PYIV.Persistence.Errors
{
	public static class RestExceptionFactory
	{
		public static RestException CreateExceptionFromError(ServerError error){
			switch(error.Type){
				
			case ErrorType.NOT_UNIQUE:
				return new NotUniqueException(error.Type, error.Message);
				
				
			}
			return null;
			
		}
		
		public static RestException CreateNoConnectionException(){
			return new NoConnectionException();
		}
		
	}
}

