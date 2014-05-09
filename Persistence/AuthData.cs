using System;

namespace PYIV.Persistence
{
	public class AuthData
	{
		
		public const string KEY_ID = "userId";
		public const string KEY_TOKEN = "authToken";
		
		public string Id {get; set;}
		public string Token {get; set;}
		
		public override string ToString ()
		{
			return string.Format ("[AuthData: Id={0}, Token={1}]", Id, Token);
		}
	}
}

