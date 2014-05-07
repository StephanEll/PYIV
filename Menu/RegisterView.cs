using System;
using UnityEngine;
using System.Collections;

namespace PYIV.Menu
{
	public class RegisterView : GuiView{
		

		public RegisterView() : base("Login")
		{
			
		}
		
		
		

		public override bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

