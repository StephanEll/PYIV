using System;

namespace PYIV.Persistence.Errors
{
	public class ServerError
	{
		
		public string ErrorMessage { get; set; }
		
		private ErrorType errorType;

		public ErrorType ErrorType {
			get { return errorType; }
			set { errorType = (ErrorType)value; }
		}

	}
}

