using System.Collections;
using System.Collections.Generic;
using PYIV.Persistence.Errors;

namespace PYIV.Persistence{

	public class User : ServerModel
	{
		private string name;
		public string Name {
			get{
				return name;
			}
			set{
				if(User.IsValidUsername(value)){
					name = value;
				}
				else{
					throw new InvalidUsernameException("Your nickname must be at least 4 digits long.");
				}
			}
		}
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}
	
		public User(){
			resource = "user";
		}
		
		
		public static bool IsValidUsername(string username){
			return username.Length >= 4;
		}
		
	}

}



