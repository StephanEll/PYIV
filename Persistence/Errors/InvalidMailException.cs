using System;

namespace PYIV.Persistence.Errors
{
	public class InvalidMailException : Exception
	{
		
		public InvalidMailException (string message) : base(message)
		{
		}
	}
}

