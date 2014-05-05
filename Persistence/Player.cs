using UnityEngine;
using System.Collections;

namespace PYIV.Persistence
{

	public class Player : User
	{
		public string Mail {get; set;}
		public string Password { get; set;}
		
		public Player(){
			resource = "player";
		}
		
		
	}

}