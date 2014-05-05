using System;
using UnityEngine;
using System.Collections;

namespace PYIV.Menu
{
	public class RegisterView : UIPanel, BaseView{
		
		private GameObject login;
		
		
		void Start()
		{
			login = Instantiate((GameObject)Resources.Load("login")) as GameObject;
			login.transform.parent = this.transform;
		}
		
		
		public void OnAddedToScreen (UIAnchor guiParent, UnityEngine.GameObject sceneParent)
		{
			throw new NotImplementedException ();
		}

		public void OnRemovedFromScreen ()
		{
			throw new NotImplementedException ();
		}

		public bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

