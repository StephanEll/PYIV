using System;

namespace PYIV.Persistence.Errors
{
	public class ServerError
	{
		
		public string Message { get; set; }
		
		private ErrorType type;

		public ErrorType Type {
			get { return type; }
			set { type = (ErrorType)value; }
		}

	}
}

