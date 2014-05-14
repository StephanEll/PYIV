using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence{

	public class User : ServerModel
	{
		public string Name {get; set;}
		
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}
	
		public User(){
			resource = "user";
		}
		
		
		public static bool IsValidUsername(string username){
			return username.Length >= 4;
		}
		
		public virtual void Validate(){
			if(this.Name != null && !User.IsValidUsername(Name))
				throw new InvalidUsernameException("Your nickname must be at least 4 digits long.");
				
		}
		
		
	}

}



