using UnityEngine;
using System.Collections;

namespace PYIV.Menu
{
	public abstract class BaseView
	{
	
		private const string VIEW_ROUTER_TAG = "ViewRouter";
		
		public abstract void AddToScreen(GameObject guiParent, GameObject sceneParent);
		public abstract void RemoveFromScreen ();
		public abstract bool ShouldBeCached();
		
		protected ViewRouter ViewRouter{
			
			get{
				return GameObject.FindGameObjectWithTag(VIEW_ROUTER_TAG).GetComponent<ViewRouter>();
			}
			
			
		}
		
		
	}

}