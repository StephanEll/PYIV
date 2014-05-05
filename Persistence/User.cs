using System.Collections;
using System.Collections.Generic;


namespace PYIV.Persistence{

	public class User : ServerModel
	{
		
		public string Name {get; set;}
		public IList<string> Wins {get; set;}
		public IList<string> Defeats {get; set;}
	
		public User(){
			resource = "user";
		}
		
	}

}



