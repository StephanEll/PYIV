using System;

namespace PYIV.Persistence.Errors
{
	public class NotUniqueException : RestException
	{
		public NotUniqueException (ErrorType type, string message) : base (type, message)
		{
		}
	}
}

