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
			case ErrorType.INVALID_LOGIN:
				return new LoginFailedException(error.Type, error.Message);
			case ErrorType.NOT_FOUND:
				return new NotFoundException(error.Type, error.Message);
				
			}
			return new RestException(error.Message);
			
		}
		
		public static RestException CreateNoConnectionException(){
			return new NoConnectionException();
		}
		
	}
}

