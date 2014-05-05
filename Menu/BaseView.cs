using UnityEngine;
using System.Collections;

namespace PYIV.Menu
{
	public interface BaseView
	{
	
		void OnAddedToScreen (UIAnchor guiParent, GameObject sceneParent);
		void OnRemovedFromScreen ();
		bool ShouldBeCached();
	}

}