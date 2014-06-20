using System;
using PYIV.Helper;

namespace PYIV.Persistence.Errors
{
	public class NoConnectionException : RestException
	{
		
		public NoConnectionException () : base(StringConstants.SERVER_NOT_AVAILABLE)
		{
		}
	}
}

