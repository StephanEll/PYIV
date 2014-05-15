using System;
using PYIV.Helper;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence.Validators
{
	public class PasswordValidator : SingletonBase<PasswordValidator>, Validator
	{
		private const int MIN_PASSWORD_LENGTH = 6;
		private const string ERROR_MESSAGE = "Your password must be at least 6 digits long";
		
		public PasswordValidator ()
		{
		}
		
		public void Validate(string userInput){
			if(!IsValidPassword(userInput))
				throw new InvalidPasswordException(ERROR_MESSAGE);
		}
		
		private bool IsValidPassword(string rawPassword)
		{
			return rawPassword.Length >= MIN_PASSWORD_LENGTH;
		}
	}
}

