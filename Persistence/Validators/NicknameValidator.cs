using System;
using PYIV.Helper;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence.Validators
{
	public class NicknameValidator : SingletonBase<NicknameValidator>, Validator
	{
		private const int MIN_NAME_LENGTH = 4;
		private string ERROR_MESSAGE = "Your nickname must be at least "+ MIN_NAME_LENGTH +" digits long";
		
		public NicknameValidator ()
		{
		}
		
		public void Validate(string userInput){
			if(!IsValidUsername(userInput))
				throw new InvalidUsernameException(ERROR_MESSAGE);
		}
		
		private bool IsValidUsername(string name)
		{
			return name.Length >= MIN_NAME_LENGTH;
		}
	}
}

