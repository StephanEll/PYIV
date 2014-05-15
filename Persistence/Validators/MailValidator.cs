using System;
using PYIV.Helper;
using PYIV.Persistence.Errors;
using System.Text.RegularExpressions;

namespace PYIV.Persistence.Validators
{
	public class MailValidator : SingletonBase<MailValidator>, Validator
	{
		private const string ERROR_MESSAGE = "Your Mail isn't a valid address.";
		
		public MailValidator(){	}
		
		public void Validate(string userInput){
			if(!IsValidEmailAddress(userInput))
				throw new InvalidMailException(ERROR_MESSAGE);
		}
		
		private bool IsValidEmailAddress (string mailAddress)
		{
			Regex mailIDPattern = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}", RegexOptions.IgnoreCase);
			return (!string.IsNullOrEmpty (mailAddress) && mailIDPattern.IsMatch (mailAddress)) ? true : false;
		}
		
	}
}

