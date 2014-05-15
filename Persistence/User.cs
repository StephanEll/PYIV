using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence.Errors;
using PYIV.Persistence.Validators;

namespace PYIV.Persistence{

	public class User<S> : ServerModel<User<S>>
	{
		public string Name {get; set;}
		
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}
	
		public User(){
			resource = "user";
		}
		

		
		public virtual void Validate(){
			NicknameValidator.Instance.Validate(Name);
				
		}
		
		
	}

}



