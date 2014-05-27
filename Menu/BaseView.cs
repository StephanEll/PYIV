using UnityEngine;
using System.Collections;

namespace PYIV.Menu
{
	public interface BaseView
	{
		
		void AddToScreen(GameObject guiParent, GameObject sceneParent);
		void RemoveFromScreen ();
		
		void UnpackParameter(object initParameter);
		
		bool ShouldBeCached();
		
		
		
		
	}

}