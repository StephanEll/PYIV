using System;

namespace PYIV.Persistence.Errors
{
	public class NotFoundException : RestException
	{
		public NotFoundException (ErrorType type, string message) : base (type, message)
		{
		}
	}
}

