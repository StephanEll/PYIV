using System;

namespace PYIV.Persistence.Errors
{
	public class LoginFailedException : RestException
	{
		public LoginFailedException (ErrorType type, string message) : base(type, message)
		{
		}
	}
}

