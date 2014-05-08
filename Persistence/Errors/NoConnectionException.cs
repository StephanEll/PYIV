using System;

namespace PYIV.Persistence.Errors
{
	public class NoConnectionException : RestException
	{
		private const string ERROR_MESSAGE = "Server currently not available";
		
		public NoConnectionException () : base(ERROR_MESSAGE)
		{
		}
	}
}

