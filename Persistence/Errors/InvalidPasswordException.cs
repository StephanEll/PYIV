using System;

namespace PYIV.Persistence.Errors
{
	public class InvalidPasswordException : Exception
	{
		
		public InvalidPasswordException (string message) : base(message)
		{
		}
	}
}

