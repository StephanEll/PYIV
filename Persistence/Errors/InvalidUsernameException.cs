using System;

namespace PYIV.Persistence.Errors
{
	public class InvalidUsernameException : Exception
	{
		public InvalidUsernameException (string message) : base(message)
		{
		}
	}
}

